namespace Lanyards.Storage.Options
{
	/// <summary>
	/// Mongo store options
	/// </summary>
	internal class MongoOptions
	{
		/// <summary>
		/// Connection string
		/// </summary>
		public string ConnectionString { get; set; }

		/// <summary>
		/// Database name
		/// </summary>
		public string DatabaseName { get; set; }
	}
}
