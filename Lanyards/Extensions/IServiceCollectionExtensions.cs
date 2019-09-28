using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Okta.AspNetCore;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
	/// <summary>
	/// Extensions for <see cref="IServiceColletcion"/>
	/// </summary>
	public static class IServiceCollectionExtensions
	{
		/// <summary>
		/// Configures app authentication
		/// </summary>
		/// <param name="services"><see cref="IServiceCollection"/></param>
		/// <param name="configuration">Authentication configuration</param>
		/// <returns>Modified <see cref="IServiceCollection"/></returns>
		public static IServiceCollection ConfigureAppAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = OktaDefaults.MvcAuthenticationScheme;
			})
				.AddCookie()
				.AddOktaMvc(new OktaMvcOptions
				{
					OktaDomain = configuration["Okta:Domain"],
					ClientId = configuration["Okta:ClientId"],
					ClientSecret = configuration["Okta:ClientSecret"],
					Scope = new[] { "lanyards_manage", "openid", "profile" },
					ClockSkew = TimeSpan.FromMinutes(2),
					GetClaimsFromUserInfoEndpoint = true
				});


			return services;
		}
	}
}
