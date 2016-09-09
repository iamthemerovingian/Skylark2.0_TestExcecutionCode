using Prism.Mvvm;
using Skylark2_TestExecutionCode.Models;
using Prism.Events;
using Skylark2_TestExecutionCode.Events;
using Prism.Regions;
using Prism.Commands;
using System;
using System.Windows;

namespace Skylark2_TestExecutionCode.ViewModels
{
    class ErrorCodeViewModel : BindableBase
    {
        private string _errorCode;
        public ErrorCodeViewModel()
        {
            ErrorCodes ErrorCodesObj = new ErrorCodes();
            ErrorCodesObj.ErrorCode = "Some Error Code";
            _errorCode = ErrorCodesObj.ErrorCode;
        }

        /// <summary>
        /// This is a propertu of the ErrorCodeViewModel.
        /// It holds a string variable.
        /// 
        /// SetProperty is used throgh BindableBase to wire up the UI to this property in a loosely coupled fashion.
        /// </summary>
        public string ErrorCodeData
        {
            get { return _errorCode; }
            set { SetProperty(ref _errorCode, value);}
        }

        /// <summary>
        /// Delegate Commands are the things that the UI binds to when they want to call an action.
        /// So if you want a button press to do something, you have to make a Delegate Command and follow the style for ExitApplication command.
        /// In the UI, you must Bind the Delegate Command Name and use the ViewModelLocator.AutoWireViewModel="True" line in the XAML.
        /// </summary>
        public DelegateCommand WriteErrorCode { get; set; }
        public DelegateCommand ExitApplication { get; set; }

        /// <summary>
        /// This is the method that holds the execution and validation scenarios of certain methods.
        /// the IEventAggregator is for communicating between different view models.
        /// 
        /// </summary>
        /// <param name="eventAggregator"></param>

        private readonly IEventAggregator _eventAggregator;
        public ErrorCodeViewModel(IEventAggregator eventAggregator)
        {
            ///The eventAggregator can cath events and publish them so other people can subscribe to them.
            ///This is used to communicate between different viewmodels.
            ///
            _eventAggregator = eventAggregator;

            ///This is getting the updatedevent and waiting for any messages that are sent to it.
            ///So when ExceuteWriteErrorCode is called it sends a massage to us and we will recieve it here.
            ///After Recieving it we will call the Updated method.
            eventAggregator.GetEvent<UpdatedEvent>().Subscribe(Updated);

            /// WriteErrorCode object is loaded with the object of type delegate command.
            /// This delegate command will execute the ExecuteWriteErrorCode if the CanExecuteWriteErrorCode method returns a true.
            /// The ObservesProperty method keeps executing the CanExecuteWriteErrorCode metod every time ErrorCodeData method is run.
            /// 
            /// So in summary this line will execute the ExecuteWriteErrorCode if the CanExecute returns true, its state is updated only whem ErrroCodeData is run.

            WriteErrorCode = new DelegateCommand(ExecuteWriteErrorCode, CanExecuteWriteErrorCode).ObservesProperty(() => ErrorCodeData);

            ExitApplication = new DelegateCommand(ExecuteCloseWindow, CanExecuteCloseWindow).ObservesProperty(()=> ErrorCodeData);
        }


        #region WriteErrorCode Members
        private bool CanExecuteWriteErrorCode()
        {
            return true;
        }

        private void ExecuteWriteErrorCode()
        {
            ///This eventAggregator is used for communication between different ViewModels. 
            ///In this case it is getting the updated event and publishing it with a payload string. 
            ///Publishing is like sending a message.
            ///Subscribing is like recieving a message.
            _eventAggregator.GetEvent<UpdatedEvent>().Publish("Sending a Message from the ExecuteWriteErrorCode method!!");
        }


        public void Updated(string obj)
        {
            ErrorCodeData = obj;
        }

        #endregion

        #region CloseWindow Members

        /// <summary>
        /// This is the correct way to wire up methods to UI elements. 
        /// The CanExecute method is for validation.
        /// The Execute method is to do what you want to do.
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteCloseWindow()
        {
            return !string.IsNullOrWhiteSpace(ErrorCodeData);
        }
        
        private void ExecuteCloseWindow()
        {
            Application.Current.MainWindow.Close();
        }
        #endregion

        /// <summary>
        /// This bit at the bottom would be used to navigate from control to control. In WPF there is a concept of a shell UI.
        /// You would then populate a region in your shell UI with the controls that  you want.
        /// 
        /// In this example, there is no need for regions and so the region manager is commented out.
        /// </summary>

        //private readonly IRegionManager _regionManager;
        //public DelegateCommand<string> NavigateCommand { get; set; }

        //public ErrorCodeViewModel(IRegionManager regionManager)
        //{
        //    _regionManager = regionManager;

        //    NavigateCommand = new DelegateCommand<string>(Navigate);
        //}

        //private void Navigate(string uri)
        //{
        //    _regionManager.RequestNavigate("ContentRegion", uri);
        //}


    }
}
