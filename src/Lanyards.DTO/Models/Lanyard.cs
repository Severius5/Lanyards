using System;

namespace Lanyards.DTO.Models
{
	/// <summary>
	/// Lanyard model
	/// </summary>
	public class Lanyard
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
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Lanyard type
		/// </summary>
		public LanyardType Type { get; set; }

		/// <summary>
		/// Address of image for front/outer lanyard side 
		/// </summary>
		public string FronImgAddress { get; set; }

		/// <summary>
		/// Address of image for back/inner lanyard side
		/// </summary>
		public string BackImgAddress { get; set; }
	}
}
