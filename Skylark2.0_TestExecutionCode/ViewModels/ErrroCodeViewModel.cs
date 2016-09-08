using Prism.Mvvm;
using Skylark2_TestExecutionCode.Models;
using Prism.Events;
using Skylark2_TestExecutionCode.Events;

namespace Skylark2_TestExecutionCode.ViewModels
{
    class ErroCodeViewModel : BindableBase
    {
        private string _errorCode;
        public ErroCodeViewModel()
        {
            ErrorCodes ErrorCodesObj = new ErrorCodes();
            ErrorCodesObj.ErrorCode = "Some Error Code";
            _errorCode = ErrorCodesObj.ErrorCode;
        }

        public ErrorCodes ErrorCodes
        {
            get;
            set;
        }

        public string ErrorCodeData
        {
            get { return _errorCode; }
            set { SetProperty(ref _errorCode, value); }
        }

        public ErroCodeViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<UpdatedEvent>().Subscribe(Updated);
        }

        private void Updated(string obj)
        {
            ErrorCodeData = "Some new data";
        }
    }
}
