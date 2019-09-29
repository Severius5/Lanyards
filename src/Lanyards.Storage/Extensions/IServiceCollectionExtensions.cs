using Lanyards.Storage.Options;
using Lanyards.Storage.Stores;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
	/// <summary>
	/// Extensions for <see cref="IServiceCollection"/>
	/// </summary>
	public static class IServiceCollectionExtensions
	{
		/// <summary>
		/// Adds application storage
		/// </summary>
		/// <param name="services"><see cref="IServiceCollection"/></param>
		/// <param name="configuration">Storage configuration</param>
		/// <returns>Modified <see cref="IServiceCollection"/></returns>
		public static IServiceCollection AddAppStorage(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<MongoOptions>(configuration);
			services.AddTransient<ILanyardStore, MongoStore>();

			return services;
		}
	}
}
