using System;
using System.Reflection;
using System.Web.Mvc;

namespace NavigationGmail
{
	public class ActionSelectorAttribute : ActionNameSelectorAttribute
	{
		public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
		{
			var action = controllerContext.Controller.ValueProvider.GetValue("action").AttemptedValue;
			if (!controllerContext.IsChildAction && action != null)
				actionName = action;
			return StringComparer.OrdinalIgnoreCase.Compare(actionName, methodInfo.Name) == 0;
		}
	}
}