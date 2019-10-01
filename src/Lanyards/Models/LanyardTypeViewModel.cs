using System.ComponentModel.DataAnnotations;

namespace Lanyards.Models
{
	/// <summary>
	/// View model for Lanyard Type
	/// </summary>
	public enum LanyardTypeViewModel
	{
		/// <summary>
		/// Unknown
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Standard ribbon lanyard
		/// </summary>
		Normal = 1,

		/// <summary>
		/// Double layered ribbon lanyard
		/// </summary>
		Double = 2,

		/// <summary>
		/// Metail chain
		/// </summary>
		Metal = 10,

		/// <summary>
		/// Flate shoelace lanyard
		/// </summary>
		[Display(Name = "Flat shoelace")]
		FlatShoelace = 20,

		/// <summary>
		/// Round shoelace lanyard
		/// </summary>
		[Display(Name = "Round shoelace")]
		RoundShoelace = 21,

		/// <summary>
		/// Cable
		/// </summary>
		Cable = 30,

		/// <summary>
		/// Zipper
		/// </summary>
		Zipper = 40
	}
}
