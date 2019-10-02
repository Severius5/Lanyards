using Lanyards.DTO.Models;
using Lanyards.Storage.Entities;
using MongoDB.Bson;
using System;

namespace Lanyards.Storage
{
	internal static class MongoMapper
	{
		public static Lanyard MapToDTO(MongoLanyard entity)
		{
			if (entity == null)
				return null;

			return new Lanyard
			{
				BackImgAddress = entity.BackImgAddress,
				FrontImgAddress = entity.FronImgAddress,
				Description = entity.Description,
				Text = entity.Text,
				Type = entity.Type,
				CreationDate = entity.Id.CreationTime,
				Id = entity.Id.ToString()
			};
		}

		public static MongoLanyard MapToEntity(Lanyard lanyard, bool mapId = true)
		{
			if (lanyard == null)
				throw new ArgumentNullException(nameof(lanyard));

			var id = mapId && ObjectId.TryParse(lanyard.Id, out var objectId)
				? objectId
				: default;

			return new MongoLanyard
			{
				BackImgAddress = lanyard.BackImgAddress,
				FronImgAddress = lanyard.FrontImgAddress,
				Description = lanyard.Description,
				Text = lanyard.Text,
				Type = lanyard.Type,
				Id = id
			};
		}
	}
}
