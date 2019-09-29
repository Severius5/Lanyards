using Lanyards.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lanyards.Storage.Stores
{
	/// <summary>
	/// Lanyards store
	/// </summary>
	public interface ILanyardStore
	{
		/// <summary>
		/// Adds new lanyard
		/// </summary>
		/// <param name="lanyard"><see cref="Lanyard"/> to add</param>
		/// <returns>ID of added <see cref="Lanyard"/></returns>
		Task<string> Add(Lanyard lanyard);

		/// <summary>
		/// Gets lanyard
		/// </summary>
		/// <param name="id">Lanyard's ID</param>
		/// <returns><see cref="Lanyard"/> or null</returns>
		Task<Lanyard> Get(string id);

		/// <summary>
		/// Gets paginated lanyards
		/// </summary>
		/// <param name="filter">Text filter</param>
		/// <param name="page">Which page to fetch</param>
		/// <param name="pageSize">How many elements per page</param>
		/// <returns>Filtered result</returns>
		Task<(List<Lanyard> Lanyards, int TotalElements)> Paginate(string filter, int page, int pageSize);
	}
}
