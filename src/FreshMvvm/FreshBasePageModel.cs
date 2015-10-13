using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using Paxie.Core;
using Paxie.Core.Logging.Services;

namespace Xamarui.Forms.Mvvm
{
    public abstract class FreshBasePageModel : IFreshBasePageModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The previous page model, that's automatically filled, on push
        /// </summary>
        public IFreshBasePageModel PreviousPageModel { get; set; }  // todo - why does it need to know the current page model?

        /// <summary>
        /// A reference to the current page, that's automatically filled, on push
        /// </summary>
        public Page CurrentPage { get; set; }

        protected readonly ILogService _log;

        protected FreshBasePageModel()
        {
            _log = InstanceFactory.Current.GetInstance<ILogService>();
        }

        /// <summary>
        /// This method is called when a page is Pop'd, it also allows for data to be returned.
        /// </summary>
        /// <param name="returndData">This data that's returned from </param>
        public virtual void ReverseInit (object returndData)
        {
        }

        /// <summary>
        /// This method is called when the PageModel is loaded, the initData is the data that's sent from pagemodel before
        /// </summary>
        /// <param name="initData">Data that's sent to this PageModel from the pusher</param>
        public virtual void Init (object initData)
        {
        }

        protected void RaisePropertyChanged ([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) {
                handler (this, new PropertyChangedEventArgs (propertyName));
            }
        }

        internal void WireEvents()
        {
            if (CurrentPage == null) _log.Warn("The current pagemodel doesn't have a page associated with it");
            Ensure.IsNotNull(CurrentPage);
            CurrentPage.Appearing += ViewIsAppearing;
            CurrentPage.Disappearing += ViewIsDisappearing;
        }

        [Obsolete("Deprecated - page is already known", false)]
        internal void WireEvents (Page page)
        {
            page.Appearing += ViewIsAppearing;
            page.Disappearing += ViewIsDisappearing;
        }

        /// <summary>
        /// This method is called when the view is disappearing. 
        /// </summary>
        /// <param name="sender">todo: describe sender parameter on ViewIsDisappearing</param>
        /// <param name="e">todo: describe e parameter on ViewIsDisappearing</param>
        protected virtual void ViewIsDisappearing (object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This methods is called when the View is appearing
        /// </summary>
        /// <param name="sender">todo: describe sender parameter on ViewIsAppearing</param>
        /// <param name="e">todo: describe e parameter on ViewIsAppearing</param>
        protected virtual void ViewIsAppearing (object sender, EventArgs e)
        {
        }
    }
}

