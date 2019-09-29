namespace System.Security.Claims
{
	/// <summary>
	/// Extensions for <see cref="ClaimsPrincipal"/>
	/// </summary>
	public static class ClaimsPrincipalExtensions
	{
		/// <summary>
		/// Chekcs whether user is admin
		/// </summary>
		/// <param name="principal"><see cref="ClaimsPrincipal"/></param>
		/// <returns>Whether is admin</returns>
		public static bool IsAdmin(this ClaimsPrincipal principal)
		{
			var adminValue = principal?.FindFirstValue("l_admin");

			return adminValue != null
				&& bool.TryParse(adminValue, out var isAdmin)
				&& isAdmin;
		}

		/// <summary>
		/// Gets user's display name
		/// </summary>
		/// <param name="principal"><see cref="ClaimsPrincipal"/></param>
		/// <returns>Display name</returns>
		public static string GetDisplayName(this ClaimsPrincipal principal)
		{
			var name = principal?.FindFirstValue("nickname");
			name ??= principal?.Identity.Name;
			name ??= "Anonymous";

			return name;
		}
	}
}
