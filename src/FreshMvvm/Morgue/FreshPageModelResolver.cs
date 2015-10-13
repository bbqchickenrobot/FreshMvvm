using System;
using Xamarin.Forms;

namespace Xamarui.Forms.Mvvm
{
    [Obsolete("newer method to update this", true)]
    public static class FreshPageModelResolver
    {
        public static Page ResolvePageModel<T> () where T : SimpleBasePageModel
        {
            return ResolvePageModel<T> (null);
        }

        public static Page ResolvePageModel<T> (object initData) where T : SimpleBasePageModel
        {
            var pageModel = IoCWrapper.Resolve<T> ();

            return ResolvePageModel<T> (initData, pageModel);
        }

        public static Page ResolvePageModel<T> (object data, T pageModel) where T : SimpleBasePageModel
        {
            var type = pageModel.GetType ();
            var name = type.AssemblyQualifiedName.Replace ("Model", string.Empty);
            var pageType = Type.GetType (name);
            if (pageType == null)
                throw new Exception (name + " not found");

            var page = (Page)IoCWrapper.Resolve (pageType);

            page.BindingContext = pageModel;
            pageModel.WireEvents (page);
            pageModel.CurrentPage = page;
			//pageModel.CoreMethods = new PageModelCoreMethods (page, pageModel);
            pageModel.Init (data);

            return page;
        }

    }
}

