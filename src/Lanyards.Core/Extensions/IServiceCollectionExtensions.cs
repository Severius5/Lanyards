using Lanyards.Core.Options;
using Lanyards.Core.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
	/// <summary>
	/// Extensions for <see cref="IServiceCollection"/>
	/// </summary>
	public static class IServiceCollectionExtensions
	{
		/// <summary>
		/// Add core services
		/// </summary>
		/// <param name="services"><see cref="IServiceCollection"/></param>
		/// <param name="configuration"><see cref="IConfiguration"/></param>
		/// <returns>Modified <see cref="IServiceCollection"/></returns>
		public static IServiceCollection AddLanyards(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAppStorage(configuration.GetSection("Storage"));

			services.AddTransient<ILanyardsService, LanyardsService>();

			services.Configure<CloudinaryOptions>(configuration.GetSection("Cloudinary"));

			return services;
		}
	}
}
