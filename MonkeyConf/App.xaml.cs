using Microsoft.Maui.Controls;
using Application = Microsoft.Maui.Controls.Application;

namespace MonkeyConf
{
    public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			var view = new MainPage();
			NavigationPage.SetHasNavigationBar(view, false);
			MainPage = new NavigationPage(view);
		}
	}
}
