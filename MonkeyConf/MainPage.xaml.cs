using Microsoft.Maui.Controls;
using MonkeyConf.Features.Bindings;
using MonkeyConf.Features.DynamicData;
using MonkeyConf.Features.ObservingChanges;
using System;

namespace MonkeyConf
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private async void BindingsClicked(object sender, EventArgs e)
		{
			var view = new ObservingChangesView() { ViewModel = new ObservingChangesViewModel() };
			NavigationPage.SetHasNavigationBar(view, false);
			await Navigation.PushAsync(view);
		}

		private async void CommandsClicked(object sender, EventArgs e)
		{
			var view = new BindingsView() { ViewModel = new BindingsViewModel() };
			NavigationPage.SetHasNavigationBar(view, false);
			await Navigation.PushAsync(view);
		}

		private async void ObservingChangesClicked(object sender, EventArgs e)
		{
			var view = new ObservingChangesView() { ViewModel = new ObservingChangesViewModel() };
			NavigationPage.SetHasNavigationBar(view, false);
			await Navigation.PushAsync(view);
		}

		private async void DynamicDataClicked(object sender, EventArgs e)
		{
			var view = new DynamicDataView() { ViewModel = new DynamicDataViewModel() };
			NavigationPage.SetHasNavigationBar(view, false);
			await Navigation.PushAsync(view);
		}
	}
}
