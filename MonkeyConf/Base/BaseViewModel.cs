using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace MonkeyConf.Base
{
    public abstract class BaseViewModel : ReactiveObject
    {
        protected CompositeDisposable disposables;

        public BaseViewModel()
        {
            CreateCommands();
        }

        public virtual Task OnAppearingAsync()
        {
            disposables = disposables ?? new CompositeDisposable();

            ObserveChanges();
            return Task.CompletedTask;
        }

        public virtual Task OnDisappearingAsync()
        {
            disposables?.Dispose();
            disposables = null;
            return Task.CompletedTask;
        }

        protected virtual void ObserveChanges()
        {
        }

        protected virtual void CreateCommands()
        {
        }

        protected virtual void LogError(Exception ex)
        {
            Console.WriteLine(ex?.Message);
        }
    }
}
