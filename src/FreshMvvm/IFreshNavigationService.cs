using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Xamarui.Forms.Mvvm
{
    public interface IFreshNavigationService
    {
        Task PopToRoot(bool animate = true);

		Task PushPage (Page page, bool modal = false, bool animate = true);

        Task PushPage<T>(BaseContentPage<T> page, bool modal = false, bool animate = true) where T : FreshBasePageModel, new();

		Task PopPage (bool modal = false, bool animate = true);        
    }
}

