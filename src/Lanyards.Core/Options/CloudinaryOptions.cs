using CloudinaryDotNet;

namespace Lanyards.Core.Options
{
	/// <summary>
	/// Options for <see cref="Cloudinary"/>
	/// </summary>
	internal class CloudinaryOptions
	{
		/// <summary>
		/// Cloud name
		/// </summary>
		public string CloudName { get; set; }

		/// <summary>
		/// Api key
		/// </summary>
		public string ApiKey { get; set; }

		/// <summary>
		/// Api secret
		/// </summary>
		public string ApiSecret { get; set; }
	}
}
