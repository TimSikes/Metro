namespace MetroClient.Models
{
	public sealed class StopDto
	{
		// Nullable because stop at "Nebraska / Sepulveda" is missing an ID.
		public int? Id { get; set; }
		public string DisplayName { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
