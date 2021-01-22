using Assets.Scrips.Common.Storage;
using Assets.Scripts.Common.UI;
using Assets.Scripts.Common.UI.Controller;
using Zenject;

namespace Assets.Scrips.Common.UI
{
    public class WindowFactoryRef: AddressableScriptableObject<WindowFactory>, IWindowFactory
    {
	    public WindowFactoryRef(DiContainer container): base(container)
	    {
	    }

        public T Create<T>() where T : class, IWindowController
        {
            return Owner?.Create<T>();
        }

        public void Remove(IWindowController window)
        {
            Owner?.Remove(window);
        }
    }
}
