using Navigation;

[assembly: WebActivatorEx.PreApplicationStartMethod(
    typeof(NavigationGmail.FluentStateInfoConfig), "Register")]
namespace NavigationGmail
{
    public class FluentStateInfoConfig
    {
        /// <summary>
        /// This method is where you configure your navigation. You can find out more
        /// about it by heading over to http://navigation.codeplex.com/documentation
        /// To get you started here's an example
        /// </summary>
        public static void Register()
        {
			StateInfoConfig.Fluent
				.Dialog("Mail", new
				{
					Page = new MvcState("{folder}/{start}/{*id}", "Mail", "Index")
						.Defaults(new { folder = "inbox", start = 0, id = typeof(int) })
						.Derived("sent")
						.TrackCrumbTrail(false)
				}, d => d.Page)
				.Build();
		}
    }
}