﻿using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xamarui.Forms.Mvvm
{
    // todo - rename class (remove Fresh)
    public class FreshTabbedNavigationContainer : TabbedPage, IFreshNavigationService
    {
        public FreshTabbedNavigationContainer ()
        {				
            RegisterNavigation ();
        }

        protected void RegisterNavigation ()
        {
            //FreshIoC.Register<IFreshNavigationService> (this);
        }

        public virtual Page AddTab<T> (string title, string icon, object data = null) where T : IFreshBasePageModel
        {
            //var page = FreshPageModelResolver.ResolvePageModel<T> (data);
            var page = FreshIoC.Resolve<IBaseContentPage<T>>() as Page;
            var baseContentPage = page as IBaseContentPage;
            if (baseContentPage != null) baseContentPage.NavigationService = this;
            var navigationContainer = CreateContainerPage (page);
            navigationContainer.Title = title;
            if (!string.IsNullOrWhiteSpace(icon))
                navigationContainer.Icon = icon;
            Children.Add (navigationContainer);
            return navigationContainer;
        }

        protected virtual Page CreateContainerPage (Page page)
        {
            return new NavigationPage (page);
        }

        public async virtual Task PushPage<T>(BaseContentPage<T> page, bool modal = false, bool animate = true) where T : FreshBasePageModel, new()
        {
            await PushPage(page.ToPage());
        }

        public async System.Threading.Tasks.Task PushPage (Xamarin.Forms.Page page, bool modal = false, bool animate = true)
        {
            if (modal)
                await this.CurrentPage.Navigation.PushModalAsync (CreateContainerPage (page));
            else
                await this.CurrentPage.Navigation.PushAsync (page);
        }

		public async System.Threading.Tasks.Task PopPage (bool modal = false, bool animate = true)
        {
            if (modal)
                await this.CurrentPage.Navigation.PopModalAsync (animate);
            else
                await this.CurrentPage.Navigation.PopAsync (animate);
        }

        public async Task PopToRoot (bool animate = true)
        {
            await this.CurrentPage.Navigation.PopToRootAsync (animate);
        }
    }
}

