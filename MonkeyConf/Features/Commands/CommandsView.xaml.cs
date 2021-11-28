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

            IObservable<int> param = this.WhenAnyValue(x => x.ViewModel.OpValue);

            disposables.Add(this.Bind(ViewModel, vm => vm.OpValue, v => v.enValue.Text));
            disposables.Add(this.OneWayBind(ViewModel, vm => vm.Result, v => v.lbResult.Text));
            disposables.Add(this.BindCommand(ViewModel, vm => vm.GenerateErrorCommand, v => v.btError));
            disposables.Add(this.BindCommand(ViewModel, vm => vm.CalculateCommand, v => v.btCalculate, param));
        }

		protected override void ObserveValues(CompositeDisposable disposables)
		{
			base.ObserveValues(disposables);

            disposables.Add(this.WhenAnyValue(
                x => x.ViewModel.IsGeneratingError, 
                x => x.ViewModel.IsCalculating)
                .Subscribe(x => Device.BeginInvokeOnMainThread(() =>
                {
                    bool isBusy = x.Item1 || x.Item2;
                    aiBusy.IsVisible = isBusy;
                    aiBusy.IsRunning = isBusy;
                    lbStatus.IsVisible = isBusy;
                }), ViewModel.LogError));


            disposables.Add(this.WhenAnyValue(
                x => x.ViewModel.IsAdding, 
                x => x.ViewModel.IsMultipliying)
                .Select(x => new { isAdding = x.Item1, isMultipliying = x.Item2 })
                .Subscribe(x => Device.BeginInvokeOnMainThread(() =>
                {
                    lbStatus.Text = x.isAdding ? "Adding" : x.isMultipliying ? "Multipliying" : String.Empty;
                }), ViewModel.LogError));
        }
	}
}