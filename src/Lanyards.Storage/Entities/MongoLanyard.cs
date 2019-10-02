using Lanyards.DTO.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lanyards.Storage.Entities
{
	/// <summary>
	/// Lanyard entity for Mongo
	/// </summary>
	internal class MongoLanyard
	{
		/// <summary>
		/// ID
		/// </summary>
		[BsonId]
		public ObjectId Id { get; set; }

		/// <summary>
		/// Text
		/// </summary>
		[BsonIgnoreIfNull]
		public string Text { get; set; }

		/// <summary>
		/// Description
		/// </summary>
		[BsonIgnoreIfNull]
		[BsonElement("Desc")]
		public string Description { get; set; }

		/// <summary>
		/// Type
		/// </summary>
		[BsonRepresentation(BsonType.Int32)]
		public LanyardType Type { get; set; }

		/// <summary>
		/// From image address
		/// </summary>
		[BsonIgnoreIfNull]
		[BsonElement("Front")]
		public string FronImgAddress { get; set; }

		/// <summary>
		/// Back image address
		/// </summary>
		[BsonIgnoreIfNull]
		[BsonElement("Back")]
		public string BackImgAddress { get; set; }
	}
}
