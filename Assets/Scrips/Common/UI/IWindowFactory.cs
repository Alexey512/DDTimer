using Assets.Scripts.Common.UI.Controller;

namespace Assets.Scripts.Common.UI
{
	public interface IWindowFactory 
	{
		T Create<T>() where T : class, IWindowController;

		void Remove(IWindowController window);
	}
}
