using Lanyards.DTO.Models;
using Lanyards.Storage.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lanyards.Core.Services
{
	internal class LanyardsService : ILanyardsService
	{
		private readonly ILanyardStore _lanyardStore;

		public LanyardsService(ILanyardStore lanyardStore)
		{
			_lanyardStore = lanyardStore ?? throw new ArgumentNullException(nameof(lanyardStore));
		}

		public async Task<string> CreateLanyard(Lanyard lanyard)
		{
			lanyard.CreationDate = DateTime.UtcNow;

			var id = await _lanyardStore.Add(lanyard);

			lanyard.Id = id;

			return id;
		}

		public Task<Lanyard> GetLanyard(string id)
		{
			return _lanyardStore.Get(id);
		}

		public Task<(List<Lanyard> Lanyards, int Total)> GetLanyards(string filter, int page, int pageSize)
		{
			return _lanyardStore.Paginate(filter, page, pageSize);
		}
	}
}
