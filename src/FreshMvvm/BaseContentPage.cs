using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paxie.Core;
using Paxie.Core.Logging.Services;
using Xamarin.Forms;

namespace Xamarui.Forms.Mvvm
{
    public interface IBaseContentPage
    {
        ISimpleNavigationService NavigationService { get; set; }        
    }

    public interface IBaseContentPage<T> : IBaseContentPage
    {
        T Model { get; }
        void SetModel(T model);
    }

    public class BaseContentPage<T> : ContentPage, IBaseContentPage<T> where T : ISimpleBasePageModel, new()
    {
        T _viewModel;
        ISimpleNavigationService _nav;
        protected ILogService _log;

        protected BaseContentPage()
        {
            var log = InstanceFactory.Container.GetInstance<ILogService>();
            Ensure.IsNotNull(log);
            _log = log;
            SetBinding(TitleProperty, new Binding("Title"));
            SetBinding(IconProperty, new Binding("Icon"));
            _log.Info(string.Format("Loading page {0}", this.GetType().ToString()));
            SetBinding(Page.TitleProperty, new Binding("Title"));
            SetBinding(Page.IconProperty, new Binding("Icon"));
            //this.SetBackground();
            _viewModel = new T
            {
                CurrentPage = this
            };
            BindingContext = _viewModel;
        }

        public T Model
        {
            get { return _viewModel; }
        }

        public ISimpleNavigationService NavigationService { get; set; }

        public virtual void SetModel(T model) => _viewModel = model;

        public virtual void OnAppearing(object sender, EventArgs e){}

        public virtual void OnDisappearing(object sender, EventArgs e) { }
    }

    public static class BaseContentPageExtensions
    {
        public static Page ToPage(this IBaseContentPage page) => (Page)page;
        public static ContentPage ToContentPage(this IBaseContentPage page) => (ContentPage)page;
        public static IBaseContentPage ToIBaseContentPage(this Page page) => page as IBaseContentPage;
        public static IBaseContentPage ToIBaseContentPage<T>(this Page page) where T: ISimpleBasePageModel, new() => page as IBaseContentPage<T>;
        public static BaseContentPage<T> ToBaseContentPage<T>(this Page page) where T: ISimpleBasePageModel, new() => page as BaseContentPage<T>;
        public static BaseContentPage<T> ResolveBaseContentPage<T>(this SimpleBasePageModel model) where T : ISimpleBasePageModel, new() => InstanceFactory.Current.GetInstance<IBaseContentPage<T>>() as BaseContentPage<T>;

        public static async Task PushPageModel<T>(this SimpleBasePageModel model) where T : SimpleBasePageModel, new() 
        {
            var page = model.CurrentPage.ToBaseContentPage<T>();
            await page?.NavigationService.PushPage(model.ResolveBaseContentPage<T>());
        }

        public static async Task PopPageModel<T>(this SimpleBasePageModel model) where T : ISimpleBasePageModel, new()
        {
            var page = model.CurrentPage.ToBaseContentPage<T>();
            await page?.NavigationService.PopPage();
        }

        public static async Task PopToRootPageModel<T>(this SimpleBasePageModel model) where T : ISimpleBasePageModel, new()
        {
            var page = model.CurrentPage.ToBaseContentPage<T>();
            await page?.NavigationService.PopToRoot();
        }
    }
}
