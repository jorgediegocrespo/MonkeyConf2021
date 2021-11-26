using Microsoft.Maui.Controls;
using MonkeyConf.Base;
using ReactiveUI;
using System;
using System.Reactive.Disposables;

namespace MonkeyConf.Features.Bindings
{
    public partial class BindingsView : BaseContentPage<BindingsViewModel>
    {
        public BindingsView()
        {
            InitializeComponent();
        }

        protected override void CreateBindings(CompositeDisposable disposables)
        {
            base.CreateBindings(disposables);

            disposables.Add(this.Bind(ViewModel, vm => vm.Progress, v => v.slExample.Value));
            disposables.Add(this.OneWayBind(ViewModel, vm => vm.Progress, v => v.lbValue.Text, GetStringValue));
            disposables.Add(this.OneWayBind(ViewModel, vm => vm.Progress, v => v.imgSad.IsVisible, x => x < 30));
            disposables.Add(this.OneWayBind(ViewModel, vm => vm.Progress, v => v.imgNeutral.IsVisible, x => 30 <= x && x < 70));
            disposables.Add(this.OneWayBind(ViewModel, vm => vm.Progress, v => v.imgHappy.IsVisible, x => 70 <= x));
        }

		private string GetStringValue(int progress)
		{
            if (progress < 30)
                return "Sad";
            else if (progress < 70)
                return "Neutral";
            else
                return "Happy";
        }
    }
}