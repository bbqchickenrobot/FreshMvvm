using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Xamarui.Forms.Mvvm
{
    public class SimpleMasterDetailNavigation : Xamarin.Forms.MasterDetailPage, ISimpleNavigationService
    {
        Dictionary<string, Page> _pages = new Dictionary<string, Page> ();
        ContentPage _menuPage;
        ObservableCollection<string> _pageNames = new ObservableCollection<string> ();

        protected Dictionary<string, Page> Pages { get { return _pages; } }
        protected ObservableCollection<string> PageNames { get { return _pageNames; } }

        public void Init (string menuTitle, string menuIcon = null)
        {
            CreateMenuPage (menuTitle, menuIcon);
            RegisterNavigation ();
        }

        protected virtual void RegisterNavigation ()
        {
            //IoCWrapper.Register<ISimpleNavigationService> (this);
        }

        public virtual void AddPage<T> (string title, object data = null) where T : ISimpleBasePageModel
        {
            //var page = FreshPageModelResolver.ResolvePageModel<T> (data);
            var page = IoCWrapper.Resolve<IBaseContentPage<T>>() as Page;
            ((IBaseContentPage) page).NavigationService = this;
            var navigationContainer = CreateContainerPage (page);
            _pages.Add (title, navigationContainer);
            _pageNames.Add (title);
            if (_pages.Count == 1)
                Detail = navigationContainer;
        }

        protected virtual Page CreateContainerPage (Page page)
        {
            return new NavigationPage (page);
        }

        protected virtual void CreateMenuPage (string menuPageTitle, string menuIcon = null)
        {
            _menuPage = new ContentPage
            {
                Title = menuPageTitle
            };
            var listView = new ListView
            {
                ItemsSource = _pageNames
            };
            // todo - here's where to add the icons, etc...
            listView.ItemSelected += (sender, args) =>
            {
                if (_pages.ContainsKey((string)args.SelectedItem))
                {
                    Detail = _pages[(string)args.SelectedItem];
                }

                IsPresented = false;
            };

            _menuPage.Content = listView;

            var navPage = new NavigationPage(_menuPage) { Title = "Menu" };

            if (!string.IsNullOrEmpty(menuIcon))
                navPage.Icon = menuIcon;

            Master = navPage;
        }

        public async virtual Task PushPage<T>(BaseContentPage<T> page, bool modal = false, bool animate = true) where T : SimpleBasePageModel, new()
        {
            await PushPage(page.ToPage(), modal, animate);
        }

        public async Task PushPage (Page page, bool modal = false, bool animate = true)
             {
			if (modal)
				await Navigation.PushModalAsync (new NavigationPage (page));
			else
				await (Detail as NavigationPage).PushAsync (page, animate); //TODO: make this better
		}

		public async Task PopPage (bool modal = false, bool animate = true)
		{
            if (modal)
				await Navigation.PopModalAsync (animate);
			else
				await (Detail as NavigationPage).PopAsync (animate); //TODO: make this better
		}

        public async Task PopToRoot (bool animate = true)
        {
            await (Detail as NavigationPage).PopToRootAsync (animate);
        }
    }
}

