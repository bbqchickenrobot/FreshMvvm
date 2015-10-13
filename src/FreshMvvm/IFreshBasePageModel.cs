using System.ComponentModel;
using Xamarin.Forms;

namespace Xamarui.Forms.Mvvm
{
    public interface IFreshBasePageModel //: INotifyPropertyChanged
    {
        ///// <summary>
        ///// The previous page model, that's automatically filled, on push
        ///// </summary>
        //IFreshBasePageModel PreviousPageModel { get; set; }

        /// <summary>
        /// A reference to the current page, that's automatically filled, on push
        /// </summary>
        Page CurrentPage { get; set; }

        /// <summary>
        /// This method is called when a page is Pop'd, it also allows for data to be returned.
        /// </summary>
        /// <param name="returndData">This data that's returned from </param>
        void ReverseInit (object returndData);

        /// <summary>
        /// This method is called when the PageModel is loaded, the initData is the data that's sent from pagemodel before
        /// </summary>
        /// <param name="initData">Data that's sent to this PageModel from the pusher</param>
        void Init (object initData);
    }
}