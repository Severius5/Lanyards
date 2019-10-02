using Lanyards.Core.Services;
using Lanyards.DTO.Models;
using Lanyards.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Lanyards.Controllers
{
	[AutoValidateAntiforgeryToken]
	public class HomeController : Controller
	{
		private const int LanyardsPerPage = 6;

		private readonly ILanyardsService _lanyardsService;

		public HomeController(ILanyardsService lanyardsService)
		{
			_lanyardsService = lanyardsService ?? throw new ArgumentNullException(nameof(lanyardsService));
		}

		[HttpGet("")]
		public async Task<IActionResult> Index(string filter, int page)
		{
			if (page < 1) page = 1;

			ViewData["Filter"] = filter;

			var results = await _lanyardsService.GetLanyards(filter, page, LanyardsPerPage);
			var lanyardsVm = results.Lanyards.Select(ConvertToVM);
			var viewModel = new StaticPagedList<LanyardViewModel>(lanyardsVm, page, LanyardsPerPage, results.Total);

			return View(viewModel);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Details(string id)
		{
			var lanyard = await _lanyardsService.GetLanyard(id);
			if (lanyard == null)
				return View("LanyardNotFound");

			return View(ConvertToVM(lanyard));
		}

		[Authorize]
		[HttpGet("create")]
		public IActionResult Create()
		{
			return View("ModifyLanyard");
		}

		[Authorize]
		[HttpPost("create")]
		public async Task<IActionResult> Create(LanyardViewModel model)
		{
			if (!ModelState.IsValid)
				return View("ModifyLanyard", model);

			var id = await _lanyardsService.CreateLanyard(ConvertToDto(model));

			return RedirectToAction(nameof(Details), new { id });
		}

		[Authorize]
		[HttpGet("{id}/edit")]
		public async Task<IActionResult> Edit(string id)
		{
			var lanyard = await _lanyardsService.GetLanyard(id);
			if (lanyard == null)
				return View("LanyardNotFound");

			return View("ModifyLanyard", ConvertToVM(lanyard));
		}

		[Authorize]
		[HttpPost("{id}/edit")]
		public async Task<IActionResult> Edit(LanyardViewModel model)
		{
			if (!ModelState.IsValid)
				return View("ModifyLanyard", model);

			var isSuccess = await _lanyardsService.UpdateLanyard(ConvertToDto(model));
			if (!isSuccess)
			{
				ModelState.AddModelError(string.Empty, "Could not update lanyard");
				return View("ModifyLanyard", model);
			}

			return RedirectToAction(nameof(Details), new { model.Id });
		}

		[Authorize]
		[HttpPost("{id}/delete")]
		public async Task<IActionResult> Delete(string id)
		{
			await _lanyardsService.DeleteLanyard(id);

			return RedirectToAction(nameof(Index));
		}

		[Authorize]
		[HttpGet("signature")]
		public string GenerateSignature(Dictionary<string, string> data)
		{
			return _lanyardsService.SignImageUploadParameters(data);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		[HttpGet("error")]
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
				FrontImgUrl = lanyard.FrontImgAddress,
				Id = lanyard.Id,
				Text = lanyard.Text,
				Type = (LanyardTypeViewModel)lanyard.Type
			};
		}

		private Lanyard ConvertToDto(LanyardViewModel vm)
		{
			return new Lanyard
			{
				BackImgAddress = vm.BackImgUrl,
				CreationDate = vm.CreationDate,
				Description = vm.Description,
				FrontImgAddress = vm.FrontImgUrl,
				Id = vm.Id,
				Text = vm.Text,
				Type = (LanyardType)vm.Type
			};
		}
	}
}
