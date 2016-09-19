using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skylark;
using System.Data;

namespace Skylark2_TestExecutionCode.Models
{
    class ErrorCodes
    {
        Sqlite_Query_Handler SqliteQueries = new Sqlite_Query_Handler();

        private string errorCode;
        public string ErrorCode
        {
            get
            {
                return errorCode;
            }
            set
            {
                errorCode = value;
            }
        }

        private string rootCause;

        public string RootCause
        {
            get
            {
                return rootCause;
            }
            set
            {
                rootCause = value;
            }
        }

        public string GetRootCause(string ErrorCodeData)
        {
            return rootCause = SqliteQueries.getRoot_Cause(ErrorCodeData);
        }

        public string InputRootCause()
        {
            return rootCause;
        }
    }
}
