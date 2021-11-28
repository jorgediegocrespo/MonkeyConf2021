using MonkeyConf.Base;
using ReactiveUI;
using System.Reactive.Disposables;

namespace MonkeyConf.Features.DynamicData
{
	public partial class FodyDynamicDataView : BaseContentPage<FodyDynamicDataViewModel>
	{
		public FodyDynamicDataView()
		{
			InitializeComponent();
		}

		protected override void CreateBindings(CompositeDisposable disposables)
		{
			base.CreateBindings(disposables);

			disposables.Add(this.Bind(ViewModel, vm => vm.SearchText, v => v.enSearch.Text));

			disposables.Add(this.OneWayBind(ViewModel, vm => vm.SortByList, v => v.pkSort.ItemsSource));
			disposables.Add(this.Bind(ViewModel, vm => vm.SortBy, v => v.pkSort.SelectedItem));

			disposables.Add(this.OneWayBind(ViewModel, vm => vm.ItemList, v => v.cvAll.ItemsSource));
			disposables.Add(this.OneWayBind(ViewModel, vm => vm.LoadMoreItemListCommand, v => v.cvAll.RemainingItemsThresholdReachedCommand));
		}
	}
}