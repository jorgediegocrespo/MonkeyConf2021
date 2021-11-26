using MonkeyConf.Base;
using ReactiveUI;

namespace MonkeyConf.Features.Bindings
{
	public class BindingsViewModel : BaseViewModel
    {
        public int progress;

        public BindingsViewModel()
        {
            progress = 50;
        }

        public int Progress
        {
            get => progress;
            set => this.RaiseAndSetIfChanged(ref progress, value);
        }
    }
}
