using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarui.Forms.Mvvm
{
    // todo - rename to BasicNavigationPage
    public class FreshNavigationContainer : Xamarin.Forms.NavigationPage, IFreshNavigationService
    {
        public FreshNavigationContainer (Page page) : base (page)
        {
            RegisterNavigation ();
            SetNavigation(page);
        }

        protected void RegisterNavigation ()
        {
            //FreshIoC.Register<IFreshNavigationService> (this);
        
        }

        protected virtual Page CreateContainerPage (Page page)
        {
            SetNavigation(page);  // shouldn't this pass in the page  param?
            return new NavigationPage (page);
        }

        protected void SetNavigation(Page page)
        {
            var nav = (IBaseContentPage)page;
            if (nav != null)
            {
                nav.NavigationService = this;
            }
        }

		public async virtual Task PushPage (Xamarin.Forms.Page page, bool modal = false, bool animate = true)
        {
            if (modal)
				await Navigation.PushModalAsync (CreateContainerPage (page), animate);
            else
				await Navigation.PushAsync (page, animate);
        }

        public async virtual Task PushPage<T>(BaseContentPage<T> page, bool modal = false, bool animate = true) where T : FreshBasePageModel, new()
        {
            await PushPage(page.ToPage());
        }

		public async virtual Task PopPage (bool modal = false, bool animate = true)
        {
            if (modal)
				await Navigation.PopModalAsync (animate);
            else
				await Navigation.PopAsync (animate);
        }

        public async Task PopToRoot (bool animate = true)
        {
            await Navigation.PopToRootAsync (animate); 
        }
    }
}

