using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Skylark2_TestExecutionCode.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skylark2_TestExecutionCode.ViewModels
{
    class InputTextViewModel : BindableBase, IInteractionRequestAware
    {
        private InputTextNotification notification;
        private readonly IEventAggregator _eventAggregator;
        private string _rootcauseText;
        private string _errorCodeText;

        public InputTextViewModel()
        {

        }

        public Action FinishInteraction { get; set; }

        public INotification Notification
        {
            get
            {
                return this.Notification;
            }

            set
            {
                if (value is InputTextNotification)
                {
                    SetProperty(ref notification, value as InputTextNotification);
                } 
            }
        }

        public string ErrorCodeText
        {
            get { return _errorCodeText; }
            set { SetProperty(ref _errorCodeText, value); }
        }

        public string RootCauseText
        {
            get { return _rootcauseText; }
            set { SetProperty(ref _rootcauseText, value); }
        }

        public DelegateCommand SaveCommand { get; set; }

        public DelegateCommand CancelCommand { get; set; }

        public InputTextViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            SaveCommand = new DelegateCommand(SaveLogic, CanExcecuteSaveLogic).ObservesProperty(() => ErrorCodeText).ObservesProperty(() => RootCauseText);
            CancelCommand = new DelegateCommand(CancelLogic, CanExcecuteCancelLogic).ObservesProperty(() => ErrorCodeText).ObservesProperty(() => RootCauseText);
        }

        private bool CanExcecuteCancelLogic()
        {
            return true;
        }

        private void CancelLogic()
        {
            notification.ErrorCodeText = null;
            notification.RootCauseText = null;
            notification.Confirmed = false;
            FinishInteraction();
        }

        private bool CanExcecuteSaveLogic()
        {
            return !string.IsNullOrWhiteSpace(ErrorCodeText) && !string.IsNullOrWhiteSpace(RootCauseText);
        }

        private void SaveLogic()
        {
            notification.ErrorCodeText = this.ErrorCodeText;
            notification.RootCauseText = this.RootCauseText;
            notification.Confirmed = true;
            FinishInteraction();
        }
    }
}
