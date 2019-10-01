using Lanyards.Core.Services;
using Lanyards.DTO.Models;
using Lanyards.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Lanyards.Controllers
{
	public class HomeController : Controller
	{
		private const int LanyardsPerPage = 6;

		private readonly ILanyardsService _lanyardsService;

		public HomeController(ILanyardsService lanyardsService)
		{
			_lanyardsService = lanyardsService ?? throw new ArgumentNullException(nameof(lanyardsService));
		}

		[HttpGet]
		public async Task<IActionResult> Index(string filter, int page)
		{
			if (page < 1) page = 1;

			ViewData["Filter"] = filter;

			var results = await _lanyardsService.GetLanyards(filter, page, LanyardsPerPage);
			var lanyardsVm = results.Lanyards.Select(ConvertToVM);
			var viewModel = new StaticPagedList<LanyardViewModel>(lanyardsVm, page, LanyardsPerPage, results.Total);

			return View(viewModel);
		}

		[HttpGet("create")]
		public IActionResult Create()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		private LanyardViewModel ConvertToVM(Lanyard lanyard)
		{
			return new LanyardViewModel
			{
				BackImgUrl = lanyard.BackImgAddress,
				CreationDate = lanyard.CreationDate,
				Description = lanyard.Description,
				FrontImgUrl = lanyard.FronImgAddress,
				Id = lanyard.Id,
				Text = lanyard.Text,
				Type = (LanyardTypeViewModel)lanyard.Type
			};
		}
	}
}
