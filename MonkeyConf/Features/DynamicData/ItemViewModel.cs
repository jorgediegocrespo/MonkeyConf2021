using MonkeyConf.Base;
using ReactiveUI;

namespace MonkeyConf.Features.DynamicData
{
	public class ItemViewModel : ReactiveObject
    {
        private string name;
        private int age;

        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public int Age
        {
            get => age;
            set => this.RaiseAndSetIfChanged(ref age, value);
        }
    }
}
