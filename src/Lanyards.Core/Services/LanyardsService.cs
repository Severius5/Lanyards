using CloudinaryDotNet;
using Lanyards.Core.Options;
using Lanyards.DTO.Models;
using Lanyards.Storage.Stores;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lanyards.Core.Services
{
	internal class LanyardsService : ILanyardsService
	{
		private readonly Cloudinary _cloudinary;
		private readonly ILanyardStore _lanyardStore;

		public LanyardsService(ILanyardStore lanyardStore, IOptions<CloudinaryOptions> cloudinaryOptions)
		{
			_lanyardStore = lanyardStore ?? throw new ArgumentNullException(nameof(lanyardStore));
			var cloudinaryOpt = cloudinaryOptions?.Value ?? throw new ArgumentNullException(nameof(cloudinaryOptions));

			_cloudinary = new Cloudinary(new Account(cloudinaryOpt.CloudName, cloudinaryOpt.ApiKey, cloudinaryOpt.ApiSecret));
		}

		public Task<string> CreateLanyard(Lanyard lanyard)
		{
			lanyard.CreationDate = DateTime.UtcNow;

			return _lanyardStore.Add(lanyard);
		}

		public Task DeleteLanyard(string id)
		{
			return _lanyardStore.Delete(id);
		}

		public Task<Lanyard> GetLanyard(string id)
		{
			return _lanyardStore.Get(id);
		}

		public Task<(List<Lanyard> Lanyards, int Total)> GetLanyards(string filter, int page, int pageSize)
		{
			return _lanyardStore.Paginate(filter, page, pageSize);
		}

		public Task<bool> UpdateLanyard(Lanyard lanyard)
		{
			return _lanyardStore.Update(lanyard);
		}

		public string SignImageUploadParameters(Dictionary<string, string> parameters)
		{
			var sorted = new SortedDictionary<string, object>();
			foreach (var d in parameters)
				sorted.Add(d.Key, d.Value);

			return _cloudinary.Api.SignParameters(sorted);
		}
	}
}
