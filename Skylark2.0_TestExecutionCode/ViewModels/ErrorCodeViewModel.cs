using Prism.Mvvm;
using Skylark2_TestExecutionCode.Models;
using Prism.Events;
using Skylark2_TestExecutionCode.Events;
using Prism.Regions;
using Prism.Commands;
using System.Windows;
using Skylark2_TestExecutionCode.Notifications;
using Prism.Interactivity.InteractionRequest;

namespace Skylark2_TestExecutionCode.ViewModels
{
    class ErrorCodeViewModel : BindableBase
    {
        private string _errorCode;
        private string _rootCause;

        ErrorCodes ErrorCodesObj = new ErrorCodes();
        public InteractionRequest<InputTextNotification> InputTextRequest { get; set; }
        public InteractionRequest<INotification> NotificationRequest { get; private set; }

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
        public string RootCause
        {
            get { return _rootCause; }
            set { SetProperty(ref _rootCause, value); }
        }
        /// <summary>
        /// Delegate Commands are the things that the UI binds to when they want to call an action.
        /// So if you want a button press to do something, you have to make a Delegate Command and follow the style for ExitApplication command.
        /// In the UI, you must Bind the Delegate Command Name and use the ViewModelLocator.AutoWireViewModel="True" line in the XAML.
        /// </summary>
        public DelegateCommand FinishedCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand CustomCommand { get; set; }
        public DelegateCommand RaiseNotificationCommand { get; set; }


        /// <summary>
        /// This is the method that holds the execution and validation scenarios of certain methods.
        /// the IEventAggregator is for communicating between different view models.
        /// 
        /// </summary>
        /// <param name="eventAggregator"></param>

        private readonly IEventAggregator _eventAggregator;

        //Will only work if you are using prism.unity package and have registered this form with Unity in the bootstrapper class.
        public ErrorCodeViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            ///The eventAggregator can cath events and publish them so other people can subscribe to them.
            ///This is used to communicate between different viewmodels.
            ///
            _eventAggregator = eventAggregator;

            ///This is getting the updatedevent and waiting for any messages that are sent to it.
            ///So when ExceuteWriteErrorCode is called it sends a massage to us and we will recieve it here.
            ///After Recieving it we will call the Updated method.
            eventAggregator.GetEvent<ErrorCodeUpdated>().Subscribe(ErrorCodeSaved);
            eventAggregator.GetEvent<RootCauseUpdated>().Subscribe(RootCauseSaved);

            ///This is a notification request, it is used to bind to the current viewmodel and pass control to a notification through InteractionRequests.
            NotificationRequest = new InteractionRequest<INotification>();
            InputTextRequest = new InteractionRequest<InputTextNotification>();

            /// WriteErrorCode object is loaded with the object of type delegate command.
            /// This delegate command will execute the ExecuteWriteErrorCode if the CanExecuteWriteErrorCode method returns a true.
            /// The ObservesProperty method keeps executing the CanExecuteWriteErrorCode metod every time ErrorCodeData method is run.
            /// 
            /// So in summary this line will execute the ExecuteWriteErrorCode if the CanExecute returns true, its state is updated only whem ErrroCodeData is run.

            FinishedCommand = new DelegateCommand(FinishLogic, CanExecuteFinishLogic).ObservesProperty(() => ErrorCodeData);

            CancelCommand = new DelegateCommand(ExecuteCloseWindow, CanExecuteCloseWindow).ObservesProperty(()=> RootCause);

            CustomCommand = new DelegateCommand(RaiseInputTextDialog, CanExcecuteRaiseInputDialog).ObservesProperty(() => ErrorCodeData).ObservesProperty(() => RootCause);

            RaiseNotificationCommand = new DelegateCommand(this.RaiseNotification);

            ///This is for navigating regions.
             _regionManager = regionManager;
             NavigateCommand = new DelegateCommand<string>(Navigate);

            ShowInteractionWindowAsync();
        }

        private async void ShowInteractionWindowAsync()
        {
            await CustomCommand.Execute();
        }

        private void RaiseNotification()
        {
            this.NotificationRequest.Raise(
               new Notification { Content = "Notification Message", Title = "Notification" },
                    n =>
                    {
                        //MessageBox.Show("The user was notified.");
                    });
        }

        private bool CanExcecuteRaiseInputDialog()
        {
            return true;
        }

        private void RaiseInputTextDialog()
        {
            InputTextNotification notification = new InputTextNotification();
            notification.Confirmed = false;
            notification.Content = "Empty String";
            notification.ErrorCodeText = "No Error Code";
            notification.RootCauseText = " No Root Cause";
            notification.Title = "No Title";

            this.InputTextRequest.Raise(notification,
                returned =>
                {
                    if (returned != null && returned.Confirmed && !string.IsNullOrWhiteSpace(returned.ErrorCodeText) && !string.IsNullOrWhiteSpace(returned.RootCauseText))
                    {
                        this.ErrorCodeData = returned.ErrorCodeText;
                        this.RootCause = returned.RootCauseText;
                        MessageBox.Show("Error Code Data Recieved is : " + returned.ErrorCodeText + "\n Root Cause is : " + returned.RootCauseText);
                    }
                    else if (returned.Confirmed == false)
                    {
                        MessageBox.Show($"Confirmed = {returned.Confirmed}");
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong...\n Error Code: " + returned.ErrorCodeText + "\n Root Cause: " + returned.RootCauseText + "\n Confirmed: " + returned.Confirmed);
                    }
                });
        }

        #region WriteErrorCode Members
        private bool CanExecuteFinishLogic()
        {
            if (ErrorCodeData == "8888")
            {
                //RootCause = ErrorCodesObj.InputRootCause();
            }
            else
            {
                RootCause = ErrorCodesObj.GetRootCause(ErrorCodeData);
            }

            if (string.IsNullOrWhiteSpace(RootCause))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void FinishLogic()
        {
            _eventAggregator.GetEvent<ErrorCodeUpdated>().Publish(ErrorCodeData);
            _eventAggregator.GetEvent<RootCauseUpdated>().Publish(RootCause);
        }

        public void ErrorCodeSaved(string obj)
        {
            this.ErrorCodeData = obj;
            //MessageBox.Show("Error Code: " + obj);
        }

        public void RootCauseSaved(string obj)
        {
            this.RootCause = obj;
            //MessageBox.Show("Root Cause: " + obj);
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
            return true;
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

        private readonly IRegionManager _regionManager;
        public DelegateCommand<string> NavigateCommand { get; set; }

        private void Navigate(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion2", uri);
        }


    }
}
