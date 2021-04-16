namespace TimeTracker.Services.Models
{
	/// <summary>
	/// Geolocation.
	/// </summary>
	public readonly struct Geolocation
	{
		public Geolocation(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		/// <summary>
		/// Latitude.
		/// </summary>
		public double Latitude { get; }

		/// <summary>
		/// Longitude.
		/// </summary>
		public double Longitude { get; }
	}
}