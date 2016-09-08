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
        //Sqlite_Query_Handler SqliteQueries = new Sqlite_Query_Handler();
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

        //public DataTable getErrorCodeInfo()
        //{
        //    return SqliteQueries.Get_ErrorCodesTable();
        //}

        //public bool SaveErrorCodeInfo(DataTable ErrorCodeTable)
        //{
        //    DataTable errorCodeTable = ErrorCodeTable;
        //    return SqliteQueries.Update_ErrorCodesTable(errorCodeTable)>0;
        //}


    }
}
