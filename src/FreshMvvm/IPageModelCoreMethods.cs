﻿using System;
using System.Threading.Tasks;

namespace Xamarui.Forms.Mvvm
{
    public interface IPageModelCoreMethods
    {
        Task DisplayAlert (string title, string message, string cancel);

        Task<string> DisplayActionSheet (string title, string cancel, string destruction, params string[] buttons);

        Task<bool> DisplayAlert (string title, string message, string accept, string cancel);

        Task PushPageModel<T> (object data, bool modal = false) where T : FreshBasePageModel;

        Task PopPageModel (bool modal = false);

        Task PopPageModel (object data, bool modal = false);

        Task PushPageModel<T> () where T : FreshBasePageModel;

        Task PushPageModel (Type pageModelType);

        Task PushPageModel (Type pageModelType, object data, bool modal = false);

        Task PopToRoot(bool animate);

		void BatchBegin();

		void BatchCommit();
    }
}

