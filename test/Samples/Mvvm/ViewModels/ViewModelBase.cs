using Common.Core.Classes;

namespace Sample.Mvvm.ViewModels;

public class ViewModelBase : ModelDataError, IDisposable
{
	protected  const string cEmailRegex = @".+@.+\..+";

	public virtual void Dispose() { GC.SuppressFinalize( this ); }
}