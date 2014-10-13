using Faker;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NavigationGmail.Models
{
	public class MailRepository
	{
		private static int _ConverstationId = 0;
		private static int _MessageId = 0;
		private static List<Conversation> _Conversations = new List<Conversation>();

		static MailRepository()
		{
			_Conversations.AddRange(GenerateConversations("inbox"));
			_Conversations.AddRange(GenerateConversations("sent"));
		}

		private static List<Conversation> GenerateConversations(string folder)
		{
			var conversations = new List<Conversation>();
			for (var i = 0; i < RandomNumber.Next(125, 275); i++)
			{
				var firstName = Name.First();
				var lastName = Name.Last();
				var messageCount = RandomNumber.Next(2, 9);
				var messages = new List<Message>();
				var start = RandomNumber.Next(0, 2);
				conversations.Add(new Conversation()
				{
					Id = _ConverstationId,
					People = i % 2 == start ? firstName + ", me" : "me, " + firstName,
					Description = string.Join(" ", Lorem.Words(RandomNumber.Next(5, 10))),
					Time = new DateTime(2014, RandomNumber.Next(1, 13), RandomNumber.Next(1, 29)).ToString("dd MMM"),
					Folder = folder,
					Messages = messages,
				});
				_ConverstationId++;
				for (var j = 0; j < messageCount; j++)
				{
					messages.Add(new Message
					{
						Id = _MessageId,
						From = (i + j) % 2 == start ? firstName + " " + lastName : "me",
						Text = string.Join("\n\n", Lorem.Paragraphs(RandomNumber.Next(1, 3)))
					});
					_MessageId++;
				}
			}
			return conversations;
		}


		public IQueryable<Conversation> Conversations
		{
			get
			{
				return _Conversations.AsQueryable();
			}
		}

		public void AddMessage(int id, string text)
		{
			var conversation = _Conversations.FirstOrDefault(c => c.Id == id);
			if (conversation != null)
			{
				conversation.Messages.Add(new Message
				{
					Id = _MessageId,
					From = "me",
					Text = text,
				});
				_MessageId++;
			}
		}
	}
}