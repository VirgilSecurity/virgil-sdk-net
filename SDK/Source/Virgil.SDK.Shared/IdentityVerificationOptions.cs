namespace Virgil.SDK
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// The <see cref="IdentityVerificationOptions"/> class provides additional options 
	/// for verification <see cref="VirgilCard"/>'s identity.
	/// </summary>
	public class IdentityVerificationOptions
	{
		/// <summary>
		/// Gets or sets a key/value dictionary that represents a user fields. In some cases it could be necessary 
		/// to pass some parameters to verification server and receive them back in an email. For this special 
		/// case an optional <see cref="ExtraFields"/> dictionary property can be used. If type of an 
		/// identity is email, all values passed in <see cref="ExtraFields"/> will be passed back in an email in a 
		/// hidden form with extra hidden fields.
		/// </summary>
		public IDictionary<string, string> ExtraFields { get; set; }

		/// <summary>
		/// Gets or sets the "time to live" value Token's time_to_live parameter is used to limit the lifetime of 
		/// the token in seconds (maximum value is 60 * 60 * 24 * 365 = 1 year). Default time_to_live value is 3600 
		/// and count_to_live default value is 1, which means that the token can be used at most one time during one hour.
		/// </summary>
		public TimeSpan TimeToLive { get; set; }

		/// <summary>
		/// Gets or sets the "count to live" parameter is used to restrict the number of <see cref="IdentityValidationToken"/> 
		/// usages (maximum value is 100).
		/// </summary>
		public int CountToLive { get; set; }
	}
}