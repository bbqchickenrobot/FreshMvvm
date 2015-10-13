using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarui.Forms.Mvvm
{
    // todo - rename to BasicNavigationPage
    public class SimpleNavigation : Xamarin.Forms.NavigationPage, ISimpleNavigationService
    {
        public SimpleNavigation (Page page) : base (page)
        {
            SetNavigation(page);
        }


        protected virtual Page CreateContainerPage (Page page)
        {
            SetNavigation(page);  // shouldn't this pass in the page  param?
            return new NavigationPage (page);
        }

        protected void SetNavigation(Page page)
        {
            var cp = page.ToIBaseContentPage();
            
            if (cp != null)
            {
                cp.NavigationService = this;
            }
        }

		public async virtual Task PushPage (Xamarin.Forms.Page page, bool modal = false, bool animate = true)
        {
            if (modal)
				await Navigation.PushModalAsync (CreateContainerPage (page), animate);
            else
				await Navigation.PushAsync (page, animate);
        }

        public async virtual Task PushPage<T>(BaseContentPage<T> page, bool modal = false, bool animate = true) where T : SimpleBasePageModel, new()
        {
            await PushPage(page.ToPage(), modal, animate);
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

