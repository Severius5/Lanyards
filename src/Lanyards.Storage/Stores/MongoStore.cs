using Lanyards.DTO.Models;
using Lanyards.Storage.Entities;
using Lanyards.Storage.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lanyards.Storage.Stores
{
	internal class MongoStore : ILanyardStore
	{
		private const string CollectionName = "lanyards";

		private readonly IMongoCollection<MongoLanyard> _collection;

		public MongoStore(IOptions<MongoOptions> options)
		{
			var opt = options?.Value ?? throw new ArgumentNullException(nameof(options));

			_collection = new MongoClient(opt.ConnectionString)
				.GetDatabase(opt.DatabaseName)
				.GetCollection<MongoLanyard>(CollectionName);
		}

		public async Task<Lanyard> Get(string id)
		{
			if (!ObjectId.TryParse(id, out var objectId))
				return null;

			var entity = await _collection
				.Find(x => x.Id == objectId)
				.FirstOrDefaultAsync();

			return MongoMapper.MapToDTO(entity);
		}

		public async Task<string> Add(Lanyard lanyard)
		{
			var entity = new MongoLanyard
			{
				Description = lanyard.Description,
				Text = lanyard.Text,
				Type = lanyard.Type,
				BackImgAddress = lanyard.BackImgAddress,
				FronImgAddress = lanyard.FronImgAddress
			};

			await _collection.InsertOneAsync(entity);

			return entity.Id.ToString();
		}

		public async Task<(List<Lanyard> Lanyards, int TotalElements)> Paginate(string filter, int page, int pageSize)
		{
			var builder = Builders<MongoLanyard>.Filter;
			var skip = (page - 1) * pageSize;

			if (string.IsNullOrEmpty(filter))
			{
				return await Paginate(builder.Empty, skip, pageSize);
			}
			else
			{
				var find = builder.Or(
					builder.Regex(x => x.Text, $"/{filter}/i"),
					builder.Regex(x => x.Description, $"/{filter}/i"));

				return await Paginate(find, skip, pageSize);
			}
		}

		private async Task<(List<Lanyard> Lanyards, int TotalElements)> Paginate(FilterDefinition<MongoLanyard> filter, int skip, int take)
		{
			var query = _collection.Find(filter);
			var totalTask = query.CountDocumentsAsync();
			var elementsTask = query
				.Sort(Builders<MongoLanyard>.Sort.Descending(x => x.Id))
				.Skip(skip)
				.Limit(take)
				.ToListAsync();

			await Task.WhenAll(totalTask, elementsTask);

			var total = await totalTask;
			var elements = (await elementsTask).Select(MongoMapper.MapToDTO).ToList();

			return (elements, (int)total);
		}
	}
}
