using MonkeyConf.Base;
using ReactiveUI;
using System.Reactive.Disposables;

namespace MonkeyConf.Features.ObservingChanges
{
    public partial class ObservingChangesView : BaseContentPage<ObservingChangesViewModel>
    {
        public ObservingChangesView()
        {
            InitializeComponent();
        }

        protected override void CreateBindings(CompositeDisposable disposables)
        {
            base.CreateBindings(disposables);

            disposables.Add(this.Bind(ViewModel, vm => vm.ProductName, v => v.enProduct.Text));
            disposables.Add(this.Bind(ViewModel, vm => vm.Units, v => v.enUnits.Text));
            disposables.Add(this.Bind(ViewModel, vm => vm.ProductPrice, v => v.enPrice.Text));
            disposables.Add(this.Bind(ViewModel, vm => vm.TaxesPercentage, v => v.enTaxes.Text));
            disposables.Add(this.Bind(ViewModel, vm => vm.DiscountPercentage, v => v.enDiscountPercentage.Text));
            disposables.Add(this.Bind(ViewModel, vm => vm.DiscountValue, v => v.enDiscountValue.Text));
            disposables.Add(this.OneWayBind(ViewModel, vm => vm.Total, v => v.enTotal.Text));
        }
    }
}