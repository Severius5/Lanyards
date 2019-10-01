using Lanyards.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lanyards.Core.Services
{
	/// <summary>
	/// Lanyards service
	/// </summary>
	public interface ILanyardsService
	{
		/// <summary>
		/// Creates new lanyard
		/// </summary>
		/// <param name="lanyard"><see cref="Lanyard"/> to create</param>
		/// <returns>ID</returns>
		Task<string> CreateLanyard(Lanyard lanyard);

		/// <summary>
		/// Gets lanyard
		/// </summary>
		/// <param name="id">Lanyard's ID</param>
		/// <returns><see cref="Lanyard"/></returns>
		Task<Lanyard> GetLanyard(string id);

		/// <summary>
		/// Gets paginated lanyards
		/// </summary>
		/// <param name="filter">Search text</param>
		/// <param name="page">Page number</param>
		/// <param name="pageSize">Elements per page</param>
		/// <returns>Paginated lanyards</returns>
		Task<(List<Lanyard> Lanyards, int Total)> GetLanyards(string filter, int page, int pageSize);
	}
}
