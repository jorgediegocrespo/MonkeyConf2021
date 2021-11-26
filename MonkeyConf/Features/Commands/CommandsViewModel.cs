using Microsoft.Maui.Controls;
using MonkeyConf.Base;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace MonkeyConf.Features.Commands
{
    public class CommandsViewModel : BaseViewModel
    {
        private ObservableAsPropertyHelper<bool> isDoingSomethingQuick;
        private ObservableAsPropertyHelper<bool> isDoingSomethingSlow;

        public CommandsViewModel()
        { }

        public ReactiveCommand<Unit, Unit> DoSomethingQuickCommand { get; private set; }
        public bool IsDoingSomethingQuick  => isDoingSomethingQuick.Value;

        public ReactiveCommand<Unit, Unit> DoSomethingSlowlyCommand { get; private set; }
        public bool IsDoingSomethingSlow => isDoingSomethingSlow.Value;

		public override Task OnAppearingAsync()
		{
			return base.OnAppearingAsync();
        }

		protected override void CreateCommands()
		{
			base.CreateCommands();

            DoSomethingQuickCommand = ReactiveCommand.CreateFromTask(DoSomethingQuickAsync);
            DoSomethingSlowlyCommand = ReactiveCommand.CreateFromTask(DoSomethingSlowlyAsync);

            disposables.Add(DoSomethingQuickCommand.ThrownExceptions.Subscribe(LogError));
            disposables.Add(DoSomethingQuickCommand.IsExecuting.ToProperty(this, x => x.IsDoingSomethingQuick, out isDoingSomethingQuick));

            disposables.Add(DoSomethingSlowlyCommand.ThrownExceptions.Subscribe(LogError));
            disposables.Add(DoSomethingSlowlyCommand.IsExecuting.ToProperty(this, x => x.IsDoingSomethingSlow, out isDoingSomethingSlow));
        }

		private Task DoSomethingSlowlyAsync()
        {
            return Task.Delay(5000);
        }

        private async Task DoSomethingQuickAsync()
        {
            await Task.Delay(2000);
        }
    }
}
