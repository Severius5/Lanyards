using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lanyards.Models
{
	/// <summary>
	/// View model for Lanyard
	/// </summary>
	public class LanyardViewModel : IValidatableObject
	{
		/// <summary>
		/// Unique ID
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Text on lanyard
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Optional description of lanyard
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Creation date 
		/// </summary>
		[Display(Name = "Creation date")]
		[DataType(DataType.DateTime)]
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Lanyard type
		/// </summary>
		[BindRequired]
		[EnumDataType(typeof(LanyardTypeViewModel))]
		public LanyardTypeViewModel Type { get; set; }

		/// <summary>
		/// Address of image for front/outer lanyard side 
		/// </summary>
		[Url]
		[Required]
		[Display(Name = "Front side image")]
		public string FrontImgUrl { get; set; }

		/// <summary>
		/// Address of image for back/inner lanyard side
		/// </summary>
		[Url]
		[Display(Name = "Back side image")]
		public string BackImgUrl { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (string.IsNullOrWhiteSpace(Text) && string.IsNullOrWhiteSpace(Description))
			{
				yield return new ValidationResult(
					"One of the fields must be set",
					new[] { nameof(Text), nameof(Description) });
			}
		}
	}
}
