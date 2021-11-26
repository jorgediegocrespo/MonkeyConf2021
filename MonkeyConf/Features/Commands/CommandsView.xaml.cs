using Microsoft.Maui.Controls;
using MonkeyConf.Base;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace MonkeyConf.Features.Commands
{
	public partial class CommandsView : BaseContentPage<CommandsViewModel>
    {
        public CommandsView()
        {
            InitializeComponent();
        }

        protected override void CreateBindings(CompositeDisposable disposables)
        {
            base.CreateBindings(disposables);

            disposables.Add(this.OneWayBind(ViewModel, vm => vm.DoSomethingQuickCommand, v => v.btQuick.Command));
            disposables.Add(this.OneWayBind(ViewModel, vm => vm.DoSomethingSlowlyCommand, v => v.btSlow.Command));
        }

		protected override void ObserveValues(CompositeDisposable disposables)
		{
			base.ObserveValues(disposables);

            disposables.Add(this.WhenAnyValue(x => x.ViewModel.IsDoingSomethingQuick, x => x.ViewModel.IsDoingSomethingSlow)
                .Subscribe(x => Device.BeginInvokeOnMainThread(() =>
                {
                    bool isBusy = x.Item1 || x.Item2;
                    aiBusy.IsVisible = isBusy;
                    aiBusy.IsRunning = isBusy;
                }), ViewModel.LogError));
        }
	}
}