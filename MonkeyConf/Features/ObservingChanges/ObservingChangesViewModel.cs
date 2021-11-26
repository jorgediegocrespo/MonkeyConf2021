using Microsoft.Maui.Controls;
using MonkeyConf.Base;
using ReactiveUI;
using System;
using System.Reactive.Linq;

namespace MonkeyConf.Features.ObservingChanges
{
    public class ObservingChangesViewModel : BaseViewModel
    {
        private string productName;
        private int units;
        private double productPrice;
        private int taxesPercentage;
        private int discountPercentage;
        private double discountValue;
        private double total;

        public ObservingChangesViewModel()
        {
            productName = String.Empty;
            units = 0;
            productPrice = 0;
            taxesPercentage = 0;
            discountPercentage = 0;
            discountValue = 0;
            total = 0;
        }

        public string ProductName
        {
            get => productName;
            set => this.RaiseAndSetIfChanged(ref productName, value);
        }

        public int Units
        {
            get => units;
            set => this.RaiseAndSetIfChanged(ref units, value);
        }

        public double ProductPrice
        {
            get => productPrice;
            set => this.RaiseAndSetIfChanged(ref productPrice, value);
        }

        public int TaxesPercentage
        {
            get => taxesPercentage;
            set => this.RaiseAndSetIfChanged(ref taxesPercentage, value);
        }

        public int DiscountPercentage
        {
            get => discountPercentage;
            set => this.RaiseAndSetIfChanged(ref discountPercentage, value);
        }

        public double DiscountValue
        {
            get => discountValue;
            set => this.RaiseAndSetIfChanged(ref discountValue, value);
        }

        public double Total
        {
            get => total;
            private set => this.RaiseAndSetIfChanged(ref total, value);
        }

        protected override void ObserveChanges()
        {
            base.ObserveChanges();

            disposables.Add(this.WhenAnyValue(
                x => x.Units,
                x => x.ProductPrice,
                x => x.TaxesPercentage,
                x => x.DiscountPercentage,
                x => x.DiscountValue)
                .Throttle(TimeSpan.FromMilliseconds(500))
                .Subscribe(x => Device.BeginInvokeOnMainThread(() => CalculateTotal()), LogError));

            disposables.Add(this.WhenAnyValue(x => x.ProductName)
                .Throttle(TimeSpan.FromMilliseconds(500))
                .Subscribe(x => Device.BeginInvokeOnMainThread(() => ResetValues()), LogError));
        }

        private void CalculateTotal()
        {
            double priceUnits = ProductPrice * Units;
            double discountsPercentageResult = priceUnits * DiscountPercentage / 100;
            double taxesResult = priceUnits * TaxesPercentage / 100;

            Total = priceUnits - discountsPercentageResult - DiscountValue + taxesResult;
        }

        private void ResetValues()
        {
            Units = 1;
            ProductPrice = 0;
            TaxesPercentage = 21;
            DiscountPercentage = 0;
            DiscountValue = 0;
        }
    }
}
