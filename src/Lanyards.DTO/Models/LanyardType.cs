namespace Lanyards.DTO.Models
{
	/// <summary>
	/// Type of lanyard
	/// </summary>
	public enum LanyardType
	{
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
		FlatShoelace = 20,

		/// <summary>
		/// Round shoelace lanyard
		/// </summary>
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
