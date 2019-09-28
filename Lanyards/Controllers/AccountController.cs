using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Okta.AspNetCore;

namespace Lanyards.Controllers
{
	[Route("account")]
	public class AccountController : Controller
	{
		[HttpGet("login")]
		public IActionResult Login()
		{
			if (HttpContext.User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Home");

			return Challenge(OktaDefaults.MvcAuthenticationScheme);
		}

		[HttpPost("logout")]
		public IActionResult Logout()
		{
			return SignOut(OktaDefaults.MvcAuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme);
		}
	}
}
