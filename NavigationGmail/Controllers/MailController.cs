using Navigation;
using NavigationGmail.Models;
using System.Linq;
using System.Web.Mvc;

namespace NavigationGmail.Controllers
{
    public class MailController : Controller
    {
		private MailRepository _Repository = new MailRepository();

		public ActionResult Index()
		{
			return View();
		}

		[ChildActionOnly]
		public ActionResult _Content(string folder, int? id, int start)
		{
			var model = new MailViewModel();
			if (!id.HasValue)
				ShowList(model, folder, start);
			else
				ShowDetails(model, id.Value);
			return PartialView(model);
		}

		private void ShowList(MailViewModel model, string folder, int start)
		{
			ViewBag.Title = folder == "inbox" ? "Inbox" : "Sent Mail";
			model.Conversations = _Repository.Conversations.Where(c => c.Folder == folder);
			model.Count = model.Conversations.Count();
			model.Conversations = model.Conversations.Skip(start).Take(50);
		}

		private void ShowDetails(MailViewModel model, int id)
		{
			ViewBag.Title = model.Conversation.Description;
			model.Conversation = model.Conversations.FirstOrDefault(c => c.Id == id);
			var latest = model.Conversation.Messages.Last();
			latest.Latest = true;
			StateContext.Data["id" + latest.Id] = "o";
			foreach (var mail in model.Conversation.Messages)
				mail.Open = StateContext.Data["id" + mail.Id] != null;
		}

		public ActionResult Send(int id, Message message)
		{
			_Repository.AddMessage(id, message.Text);
			ModelState.Remove("message.Text");
			StateContext.Bag.sent = true;
			return View();
		}
	}
}