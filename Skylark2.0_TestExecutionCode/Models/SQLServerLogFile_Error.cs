using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Skylark
{
    /// <summary>
    /// Class SQLServerLogFile_Error.
    /// Only For generating Log file of Errors that is produced by SQL Server class while inserting data or retriving from DB.
    /// Created by : Vijay Pareek
    /// Date: 10 November 2015
    /// </summary>
    public class SQLServerLogFile_Error
    {
        DateTime DatOfToday;
        DateTime TimOfToday;
        string FileNameOfLog;
        string folderpath;
        string fullpath;
        StreamWriter SQLError_LogFile;

        public SQLServerLogFile_Error()
        {
            DatOfToday = DateTime.Today;
            FileNameOfLog = DatOfToday.ToString("yyyyddMM");
            FileNameOfLog = FileNameOfLog + ".log";
            string Currentpath = Directory.GetCurrentDirectory();
            folderpath = Currentpath + " \\Log\\SQLERROR_Log\\";
            fullpath = Currentpath + " \\Log\\SQLERROR_Log\\" + FileNameOfLog;
        }

        public void GetERROR_SQL_Server(string Error_Text)
        {
            createNewFile();
            TimOfToday = DateTime.Now;
            SQLError_LogFile = File.AppendText(fullpath);
            SQLError_LogFile.WriteLine("");
            SQLError_LogFile.Write(TimOfToday.ToString());
            SQLError_LogFile.Write(" ERROR: <--- ");
            SQLError_LogFile.Write(Error_Text);
            SQLError_LogFile.Close();
        }
        public void ERROR_Function_Name(string function_Name)
        {
            createNewFile();
            TimOfToday = DateTime.Now;
            SQLError_LogFile = File.AppendText(fullpath);
            SQLError_LogFile.WriteLine("");
            SQLError_LogFile.Write(TimOfToday.ToString());
            SQLError_LogFile.Write("Method ---> ");
            SQLError_LogFile.Write(function_Name);
            SQLError_LogFile.Close();
        }
        public void createNewFile()
        {
            if (File.Exists(fullpath))
            {
                //Do Nothing..Already writing in Append Mode.
            }
            else
            {
                Directory.CreateDirectory(folderpath);
                SQLError_LogFile = new StreamWriter(fullpath);
                SQLError_LogFile.Close();
            }
        }
    }
}
