using System.Collections.Generic;

namespace NavigationGmail.Models
{
	public class MailViewModel
	{
		public Conversation Conversation { get; set; }

		public IEnumerable<Conversation> Conversations { get; set; }

		public int Count { get; set; }
	}
}