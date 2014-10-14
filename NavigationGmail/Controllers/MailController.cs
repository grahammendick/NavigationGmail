using Navigation;
using NavigationGmail.Models;
using System.Linq;
using System.Web.Mvc;

namespace NavigationGmail.Controllers
{
    public class MailController : Controller
    {
		private MailRepository _Repository = new MailRepository();

		[ActionSelector]
		public ActionResult Index()
		{
			return View();
		}

		[ChildParentSync]
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
			model.Conversations = _Repository.Conversations.Where(c => c.Folder == folder);
			ViewBag.Title = folder == "inbox" ? "Inbox" : "Sent Mail";
			model.Count = model.Conversations.Count();
			model.Conversations = model.Conversations.Skip(start).Take(50);
		}

		private void ShowDetails(MailViewModel model, int id)
		{
			model.Conversation = _Repository.Conversations.First(c => c.Id == id);
			ViewBag.Title = model.Conversation.Description;
			var latest = model.Conversation.Messages.Last();
			StateContext.Data["id" + latest.Id] = "o";
			foreach (var message in model.Conversation.Messages)
			{
				message.Open = StateContext.Data["id" + message.Id] != null;
				message.Latest = message == latest;
			}
		}

		[ActionSelector]
		public ActionResult Send(int id, Message message)
		{
			_Repository.AddMessage(id, message.Text);
			ModelState.Remove("message.Text");
			StateContext.Bag.sent = true;
			return View();
		}
	}
}