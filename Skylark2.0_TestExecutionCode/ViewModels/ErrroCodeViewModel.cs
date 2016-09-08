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

        //public ErrorCodes ErrorCodes
        //{
        //    get;
        //    set;
        //}

        public string ErrorCodeData
        {
            get { return _errorCode; }
            set { SetProperty(ref _errorCode, value); }
        }

        public DelegateCommand WriteErrorCode { get; set; }
        public DelegateCommand ExitApplication { get; set; }


        private readonly IEventAggregator _eventAggregator;

        public ErrorCodeViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<UpdatedEvent>().Subscribe(Updated);

            _eventAggregator = eventAggregator;
            WriteErrorCode = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => ErrorCodeData);


            //eventAggregator.GetEvent<CloseFormEvent>().Subscribe(CloseWindow);
            _eventAggregator = eventAggregator;
            ExitApplication = new DelegateCommand(CloseWindow, CanExecute);
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute()
        {
            _eventAggregator.GetEvent<UpdatedEvent>().Publish("Some New String");
        }

        public void Updated(string obj)
        {
            ErrorCodeData = "You just wrote a new error code!!!";
        }

        private void CloseWindow()
        {
            Application.Current.MainWindow.Close();
        }
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
