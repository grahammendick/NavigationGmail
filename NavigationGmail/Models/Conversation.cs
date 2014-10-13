using System.Collections.Generic;

namespace NavigationGmail.Models
{
	public class Conversation
	{
		public int Id { get; set; }

		public string People { get; set; }

		public string Description { get; set; }

		public string Time { get; set; }

		public string Folder { get; set; }

		public List<Message> Messages { get; set; }

		public Message Message { get; set; }
	}
}