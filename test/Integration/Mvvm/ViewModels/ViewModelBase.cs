using Common.Core.Classes;

namespace Sample.Mvvm.ViewModels;

public class ViewModelBase : ModelDataError, IDisposable
{
	public virtual void Dispose() { GC.SuppressFinalize( this ); }
}