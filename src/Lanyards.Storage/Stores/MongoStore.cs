using Lanyards.DTO.Models;
using Lanyards.Storage.Entities;
using Lanyards.Storage.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
			var entity = MongoMapper.MapToEntity(lanyard, false);

			await _collection.InsertOneAsync(entity);

			return entity.Id.ToString();
		}

		public async Task Delete(string id)
		{
			if (!ObjectId.TryParse(id, out var objectId))
				return;

			await _collection.DeleteOneAsync(x => x.Id == objectId);
		}

		public async Task<bool> Update(Lanyard lanyard)
		{
			if (!ObjectId.TryParse(lanyard.Id, out var objectId))
				return false;

			var entity = MongoMapper.MapToEntity(lanyard);
			var result = await _collection.ReplaceOneAsync(x => x.Id == objectId, entity);

			return result.IsAcknowledged
				&& result.IsModifiedCountAvailable
				&& result.ModifiedCount > 0;
		}

		public Task<(List<Lanyard> Lanyards, int TotalElements)> Paginate(string filter, int page, int pageSize)
		{
			var builder = Builders<MongoLanyard>.Filter;
			var skip = (page - 1) * pageSize;

			if (string.IsNullOrEmpty(filter))
			{
				return Paginate(builder.Empty, skip, pageSize);
			}
			else
			{
				filter = Regex.Escape(filter);
				var find = builder.Or(
					builder.Regex(x => x.Text, $"/{filter}/i"),
					builder.Regex(x => x.Description, $"/{filter}/i"));

				return Paginate(find, skip, pageSize);
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
