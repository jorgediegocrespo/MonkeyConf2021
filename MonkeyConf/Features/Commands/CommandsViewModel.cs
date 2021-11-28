using MonkeyConf.Base;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace MonkeyConf.Features.Commands
{
    public class CommandsViewModel : BaseViewModel
    {
        private int opValue;
        private int result;

        private ObservableAsPropertyHelper<bool> isGeneratingError;
        private ObservableAsPropertyHelper<bool> isCalculating;
        private ObservableAsPropertyHelper<bool> isAdding;
        private ObservableAsPropertyHelper<bool> isMultipliying;

        public CommandsViewModel()
        {
            opValue = 0;
            result = 0;
        }

        public int OpValue
        {
            get => opValue;
            set => this.RaiseAndSetIfChanged(ref opValue, value);
        }

        public int Result
        {
            get => result;
            private set => this.RaiseAndSetIfChanged(ref result, value);
        }

        public ReactiveCommand<Unit, Unit> GenerateErrorCommand { get; private set; }
        public bool IsGeneratingError  => isGeneratingError.Value;

        public ReactiveCommand<int, Unit> CalculateCommand { get; private set; }
        public bool IsCalculating => isCalculating.Value;

        public ReactiveCommand<int, int> AddCommand { get; private set; }
        public bool IsAdding => isAdding.Value;

        public ReactiveCommand<int, int> MultiplyCommand { get; private set; }
        public bool IsMultipliying => isMultipliying.Value;

        public override Task OnAppearingAsync()
		{
			return base.OnAppearingAsync();
        }

		protected override void CreateCommands()
		{
			base.CreateCommands();

            GenerateErrorCommand = ReactiveCommand.CreateFromTask(DoSomethingQuickAsync);
            disposables.Add(GenerateErrorCommand.ThrownExceptions.Subscribe(LogError));
            disposables.Add(GenerateErrorCommand.IsExecuting.ToProperty(this, x => x.IsGeneratingError, out isGeneratingError));
            
            CalculateCommand = ReactiveCommand.CreateFromTask<int>(CalculateAsync, this.WhenAnyValue(x => x.OpValue).Select(x => x < 10));
            disposables.Add(CalculateCommand.ThrownExceptions.Subscribe(LogError));
            disposables.Add(CalculateCommand.IsExecuting.ToProperty(this, x => x.IsCalculating, out isCalculating));
            
            AddCommand = ReactiveCommand.CreateFromTask<int, int>(AddAsync);
            disposables.Add(AddCommand.ThrownExceptions.Subscribe(LogError));
            disposables.Add(AddCommand.IsExecuting.ToProperty(this, x => x.IsAdding, out isAdding));

            MultiplyCommand = ReactiveCommand.CreateFromTask<int, int>(MultiplyAsync);
            disposables.Add(MultiplyCommand.ThrownExceptions.Subscribe(LogError));
            disposables.Add(MultiplyCommand.IsExecuting.ToProperty(this, x => x.IsMultipliying, out isMultipliying));
        }

		private async Task DoSomethingQuickAsync()
        {
            Result = 0;
            await Task.Delay(2000);
        }

		private async Task CalculateAsync(int param)
        {
            int addResult = await AddCommand.Execute(param);
            Result = await MultiplyCommand.Execute(addResult);
        }

        private async Task<int> AddAsync(int param)
        {
            await Task.Delay(4000);
            return param + param;
        }

        private async Task<int> MultiplyAsync(int param)
        {
            await Task.Delay(4000);
            return param * param;
        }
    }
}
