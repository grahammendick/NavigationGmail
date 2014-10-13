namespace NavigationGmail.Models
{
	public class Message
	{
		public int Id { get; set; }

		public bool Open { get; set; }

		public bool Latest { get; set; }

		public string From { get; set; }

		public string Text { get; set; }
	}
}