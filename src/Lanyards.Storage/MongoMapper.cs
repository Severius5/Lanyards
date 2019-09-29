using Lanyards.DTO.Models;
using Lanyards.Storage.Entities;

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
				FronImgAddress = entity.FronImgAddress,
				Description = entity.Description,
				Text = entity.Text,
				Type = entity.Type,
				CreationDate = entity.Id.CreationTime,
				Id = entity.Id.ToString()
			};
		}
	}
}
