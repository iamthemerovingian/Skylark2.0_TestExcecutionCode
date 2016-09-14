using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows;
///added by nitish for regular expression

namespace Skylark
{
    /// <summary>
    /// Project: Skylark
    /// Class Sqlite_Query_Handler.
    /// All SQLite DB Queries in respective functions/Mathods used in whole project.
    /// Created by: Vijay Pareek
    /// 01 December 2015
    /// </summary>
    /// Please refer "Product" as "Technology" Here.. Do not change anywaher including Database.....(Vijay Pareek)
    public class Sqlite_Query_Handler
    {
        public static string ERR1 = "ERR_RecordNotFound";
        public static string ERR2 = "ERR_CanNotReadTable";
        public static string ERR3 = "ERR_DataTableLocked";
        public static string ERR4 = "ERR_DatabaseLocked";
        public static string ERR5 = "ERR_Connection";
        public static string ERR6 = "ERR_Code-404";
        public static string ERR7 = "ERR_RecordAlreadyExists.";
        public static string ERR8 = "ERR_RecordNotInserted.";
        SQLServerLogFile_Error ServerErrorLog = new SQLServerLogFile_Error();
        SQLiteConnection sqliteconn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLitePath"].ConnectionString);//Global Connection String SQLite Server.
        public Sqlite_Query_Handler()
        {
            //if (SqlServer_Check.SQLFlag == false)
            //{
            //    sqliteconn.Open();
            //}
            //else
            //{
            //    //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            //    //Do not Open the Connection
            //}
        }
        //Getting User Name And Password and role here...
        public int Get_User_Details(string user, string pwd)
        {
            int Role = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand chkRole = new SQLiteCommand("SELECT role FROM userData WHERE userName = '" + user + "'", sqliteconn);
                SQLiteDataReader RoleReader = chkRole.ExecuteReader();
                if (RoleReader.Read())
                {
                    Role = Convert.ToInt32(RoleReader[0]);
                    MessageBox.Show("Role:" + Role);
                }
                else
                {
                    MessageBox.Show("Error In Getting Role For User: " + user);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_User_Details");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_User_Details");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_User_Details");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_User_Details");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Role;
        }
        //LOGIN Authentication Method....
        public bool chklogin(string user, string pwd)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand chkLogin = new SQLiteCommand("SELECT userName, password FROM userData WHERE userName = '" + user + "' AND password = '" + pwd + "'", sqliteconn);
                SQLiteDataReader ldr = chkLogin.ExecuteReader();
                if (ldr.Read())
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    //MessageBox.Show("User Name or Password Is wrong. Please Try Again.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("chklogin");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("chklogin");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("chklogin");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("chklogin");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Getting Role String For User..
        public string get_User_Role(string user)
        {
            string role = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand chkRole = new SQLiteCommand("SELECT sRole FROM userData WHERE userName = '" + user + "'", sqliteconn);
                SQLiteDataReader RoleReader = chkRole.ExecuteReader();
                if (RoleReader.Read())
                {
                    role = RoleReader[0].ToString();
                    MessageBox.Show("Role:" + role);
                }
                else
                {
                    // MessageBox.Show("Error In Getting Role For User: " + user);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_User_Role");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_User_Role");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_User_Role");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_User_Role");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return role;
        }
        //All Queries used on Start Test Form.......Implemented here.....
        //Station Name based on Station Id Here...
        public string Get_Station_Name(int id)
        {
            string stnName = null;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdStatn = new SQLiteCommand("SELECT Station_Name FROM Station WHERE Station_Id='" + id + "'", sqliteconn);
                SQLiteDataReader sdr = cmdStatn.ExecuteReader();
                if (sdr.Read())
                {
                    stnName = sdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Station_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR6);
                    //return ERR6;
                }
                return stnName;
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Station_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Station_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Station_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Station_Name");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return stnName;
        }
        //Getting Technology Name Here.
        public string Get_Tech_Name(int Tech_Id)
        {
            string Techno = null;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdProduct = new SQLiteCommand("SELECT Product_Type FROM Product WHERE Product_Id='" + Tech_Id + "'", sqliteconn);
                SQLiteDataReader pdr = cmdProduct.ExecuteReader();
                if (pdr.Read())
                {
                    Techno = pdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Tech_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR6);
                }
                return Techno;
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Tech_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Tech_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Tech_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Tech_Name");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
                return Techno;
            }
        }
        //added by prasad
        public DataTable Get_Test_Items_For_Dii(int model_id, int Station_Id)
        {
            int count_Row = 0;
            DataTable dt = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmd1 = new SQLiteCommand("SELECT  Test_Item_Name  FROM Test_Items  WHERE Model_Id = '" + model_id + "' AND Station_Id ='" + Station_Id +"'" , sqliteconn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd1);
                da.Fill(dt);
                count_Row = dt.Rows.Count;
                if (count_Row != 0)
                {
                    Console.WriteLine("Table Returned...");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                return dt;
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return dt;
        }
        //Getting Test Items By Model And Station Id...
        public DataTable Get_Test_Items(int model_id, int Station_Id)
        {
            int count_Row = 0;
            DataTable dt = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmd1 = new SQLiteCommand("SELECT  Test_Item_Name, T.Test_Item_Id, S.Test_Item_Order,S.Model_Id, Command_Id  FROM Test_Items AS T  JOIN Station_TestItem AS S ON S.Test_Item_Id = T.Test_Item_Id WHERE S.Model_Id = '" + model_id + "' AND S.Station_Id ='" + Station_Id + "' ORDER BY  S.Test_Item_Order", sqliteconn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd1);
                da.Fill(dt);
                count_Row = dt.Rows.Count;
                if (count_Row != 0)
                {
                    Console.WriteLine("Table Returned...");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                return dt;
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return dt;
        }
        public DataTable Get_Test_ItemsFor_StartTest(int model_id, int Station_Id)
        {
            int count_Row = 0;
            DataTable dt = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmd1 = new SQLiteCommand("SELECT  Test_Item_Name, T.Test_Item_Id, S.Test_Item_Order, Command_Id  FROM Test_Items AS T  JOIN Station_TestItem AS S ON S.Test_Item_Id = T.Test_Item_Id WHERE S.Model_Id = '" + model_id + "' AND S.Station_Id ='" + Station_Id + "' AND S.Test_Item_Order >= '" + 1 + "' ORDER BY  S.Test_Item_Order", sqliteconn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd1);
                da.Fill(dt);
                count_Row = dt.Rows.Count;
                if (count_Row != 0)
                {
                    Console.WriteLine("Table Returned...");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_ItemsFor_StartTest");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                return dt;
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_ItemsFor_StartTest");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_ItemsFor_StartTest");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_ItemsFor_StartTest");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_ItemsFor_StartTest");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return dt;
        }
        //Getting Model Details Here By Model_Id
        public string[] Get_Model_Detais(int model_Id)
        {
            string[] model = new string[6];
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdPrintr = new SQLiteCommand("SELECT Model_Name, Model_Code, Model_FW_Version, Product_Id , HardwareID,Driver_Name FROM Model WHERE Model_Id='" + model_Id + "'", sqliteconn);
                SQLiteDataReader mdr = cmdPrintr.ExecuteReader();
                if (mdr.Read())
                {
                    model[0] = (mdr[0].ToString());
                    model[1] = (mdr[1].ToString());
                    model[2] = (mdr[2].ToString());
                    model[3] = (mdr[3].ToString());
                    model[4] = (mdr[4].ToString());
                    model[5] = (mdr[5].ToString());

                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Model_Detais");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Model_Detais");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Model_Detais");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Model_Detais");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Model_Detais");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return model;
        }
        //Update Model Details...
        public bool Update_Model(string Model_Name, string Model_Code, string FW_Version, int Model_Id, string HardwareID ,string driver_name)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand UpdateModelCMD = new SQLiteCommand("UPDATE Model set Model_Name ='" + Model_Name + "', Model_Code = '" + Model_Code + "', Model_FW_Version = '" + FW_Version + "', HardwareID ='" + HardwareID+ "', Driver_Name ='" + driver_name + "' WHERE Model_Id ='" + Model_Id + "'", sqliteconn);
                int y = UpdateModelCMD.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    //MessageBox.Show("Details Updated Succesfully.");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Update_Model");
                    ServerErrorLog.GetERROR_SQL_Server("Record is Not Updated Succesfully.Model: " + Model_Name);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Model");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Getting Test Item Id By Test Name..
        //modified by Nitish addeded parameters station id and model id 
        public int Get_test_Id (string test_Name, int Station_id, int Model_id)
        {
            int testId = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
               // SQLiteCommand testitemCmd = new SQLiteCommand("SELECT Test_Item_Id FROM Test_Items WHERE Model_Id = '" + Model_id + "' AND Station_Id = '" + Station_id + "' AND Test_Item_Name='" + test_Name + "'", sqliteconn);

                SQLiteCommand testitemCmd = new SQLiteCommand("SELECT T.Test_Item_Id FROM Test_Items AS T JOIN Station_TestItem AS S ON S.Test_Item_Id = T.Test_Item_Id WHERE S.Model_Id = '" + Model_id + "' AND S.Station_Id ='" + Station_id + "' AND  S.Test_Item_Order >= '" + 0 + "'", sqliteconn);

                SQLiteDataReader tdr = testitemCmd.ExecuteReader();
                if (tdr.Read())
                {
                    testId = Convert.ToInt32(tdr[0]);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Id");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return testId;
        }
        //Getting Image Path For Test Items By Test Item Id...
        public string Get_Img_Path(int test_ID)
        {
            string img_Name = null;
            string img_Path = null;
            string fullPath = null;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand imgCmd = new SQLiteCommand("SELECT  Image_Name,  Image_Path FROM Test_Items_Image WHERE Test_Item_Id = '" + test_ID + "'", sqliteconn);
                SQLiteDataReader imgrdr = imgCmd.ExecuteReader();
                if (imgrdr.Read())
                {
                    img_Name = imgrdr[0].ToString();
                    img_Path = imgrdr[1].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Img_Path");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
                fullPath = img_Path + img_Name;
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Img_Path");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Img_Path");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Img_Path");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Img_Path");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR. " + xc.Message);
                }
            }
            return fullPath;
        }
        //Getting Retry Flag for Test Item By Test Item Id...
        public int get_Retry_Flag(int testId)
        {
            int retry = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getRetryCmd = new SQLiteCommand("SELECT Retry FROM Test_Item_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader retryReader = getRetryCmd.ExecuteReader();
                if (retryReader.Read())
                {
                    retry = Convert.ToInt32(retryReader[0]);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Retry_Flag");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Retry_Flag");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Retry_Flag");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Retry_Flag");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Retry_Flag");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return retry;
        }
        //Getting Command Id For Test Item By Test Item Id...
        public int get_Command_Id(int test_Id)
        {
            int cmd_Id = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getCommandCmd = new SQLiteCommand("SELECT Command_Id FROM Test_Items WHERE Test_Item_Id='" + test_Id + "'", sqliteconn);
                SQLiteDataReader cdr = getCommandCmd.ExecuteReader();
                if (cdr.Read())
                {
                    cmd_Id = Convert.ToInt32(cdr[0]);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Command_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Command_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Command_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Command_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Command_Id");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR. " + xc.Message);
                }
            }
            return cmd_Id;
        }
        //Getting Command Name by Command Id.
        public string get_Command_Name(int cmd_Id)
        {
            string cmd_Name = null;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getCommandNameCmd = new SQLiteCommand("SELECT Command_Name FROM Commands WHERE Command_Id='" + cmd_Id + "'", sqliteconn);
                SQLiteDataReader cndr = getCommandNameCmd.ExecuteReader();
                if (cndr.Read())
                {
                    cmd_Name = cndr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Command_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Command_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Command_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Command_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Command_Name");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR. " + xc.Message);
                }
            }
            return cmd_Name;
        }
        //Getting Sample File By Test Item Id...
        public DataTable get_Sample_File(int Test_Id)
        {
            DataTable sampleFileTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand sampleFileCmd = new SQLiteCommand("SELECT Sample_File_Name,Sample_File_Path,Sample_File_Size FROM Sample_Files  Where Test_Item_Id= '" + Test_Id + "' ", sqliteconn);
                SQLiteDataAdapter Sdt = new SQLiteDataAdapter(sampleFileCmd);
                Sdt.Fill(sampleFileTable);
                int row = sampleFileTable.Rows.Count;
                if (row != 0)
                {
                    Console.WriteLine("SUccess");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR. " + xc.Message);
                }
            }
            return sampleFileTable;
        }
        //Getting Pass Fail Option for TestItem by Test Item Id..
        public bool GetPass_Fail_Opt(int testId)
        {
            bool passFail = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getPassFail = new SQLiteCommand("SELECT Result_Pass_Fail FROM Test_Item_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader pdr = getPassFail.ExecuteReader();
                if (pdr.Read())
                {
                    passFail = Convert.ToBoolean(pdr[0]);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetPass_Fail_Opt");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetPass_Fail_Opt");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetPass_Fail_Opt");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetPass_Fail_Opt");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetPass_Fail_Opt");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return passFail;
        }
        //Getting External Tool for Test Item Id...
        public DataTable Get_External_ToolDetails(int test_ID)
        {
            DataTable toolTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getToolCMD = new SQLiteCommand("SELECT External_Tool_Path,Test_Result_Path,External_Tool FROM Test_Item_Options WHERE Test_Item_Id = '" + test_ID + "'", sqliteconn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(getToolCMD);
                da.Fill(toolTable);
                int count = toolTable.Rows.Count;
                if (count != 0)
                {
                    Console.WriteLine("Records filled in Data Table.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_External_ToolDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_External_ToolDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_External_ToolDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_External_ToolDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_External_ToolDetails");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return toolTable;
        }
        //Inserting Test Results For test Items....
        public bool InsertTest_Results(string JOB_Num, int Test_Count, string Serial_NUM, string Test_Result, string Run_Mode, string App_Version, string FW_Version, string Model_Code, string Test_Station, string PC_Name, string Start_Date, string End_Date, string Start_Time, string End_Time, string Total_Time, int Test_ID, string Command_Name, string[] FW_Recieve_Vals, string Tech_Name, string Model_Name, string Factory, string Country, string FW_Response, string Test_Name, string user_name, string ErrorCode = "", string Root_Cause = "")
        {
            bool saved = false;
            int y = 0;

            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();

                SQLiteCommand cmdserverResultSave = new SQLiteCommand("INSERT INTO Test_Results (Job_Number, Test_Run_Count, Serial_Number, Test_Result, Test_Run_Mode, Application_Version, FW_Version, Model_Code, Test_Station, PC_Name, Test_Start_Date, Test_End_Date, Test_Start_Time, Test_End_Time, Total_Elapsed_Time, Test_Item_Id, Command_Name, FW_Recieve_Param1, FW_Recieve_Param2, FW_Recieve_Param3, FW_Recieve_Param4, FW_Recieve_Param5, FW_Recieve_Param6, FW_Recieve_Param7, FW_Recieve_Param8, FW_Recieve_Param9, Technology_Name, Model_Name, Factory_Location, Country, FW_Response, TestItemNameValue, User_Name, ErrorCode, Root_Cause ) values(@JOB_Num, @Test_Count, @Serial_NUM, @Test_Result, @Run_Mode, @App_Version, @FW_Version, @Model_Code, @Test_Station, @PC_Name, @Start_Date, @End_Date, @Start_Time, @End_Time, @Total_Time, @Test_ID, @Command_Name, @FW_Recieve_Vals0, @FW_Recieve_Vals1, @FW_Recieve_Vals2, @FW_Recieve_Vals3, @FW_Recieve_Vals4, @FW_Recieve_Vals5, @FW_Recieve_Vals6, @FW_Recieve_Vals7, @FW_Recieve_Vals8, @Tech_Name, @Model_Name, @Factory, @Country, @FW_Response, @Test_Name, @userName, @ErrorCode, @Root_Cause)", sqliteconn);

                cmdserverResultSave.Parameters.AddWithValue("@JOB_Num", JOB_Num);
                cmdserverResultSave.Parameters.AddWithValue("@Test_Count", Test_Count);
                cmdserverResultSave.Parameters.AddWithValue("@Serial_NUM", Serial_NUM);
                cmdserverResultSave.Parameters.AddWithValue("@Test_Result", Test_Result);
                cmdserverResultSave.Parameters.AddWithValue("@Run_Mode", Run_Mode);
                cmdserverResultSave.Parameters.AddWithValue("@App_Version", App_Version);
                cmdserverResultSave.Parameters.AddWithValue("@FW_Version", FW_Version);
                cmdserverResultSave.Parameters.AddWithValue("@Model_Code", Model_Code);
                cmdserverResultSave.Parameters.AddWithValue("@Test_Station", Test_Station);
                cmdserverResultSave.Parameters.AddWithValue("@PC_Name", PC_Name);
                cmdserverResultSave.Parameters.AddWithValue("@Start_Date", Start_Date);
                cmdserverResultSave.Parameters.AddWithValue("@End_Date", End_Date);
                cmdserverResultSave.Parameters.AddWithValue("@Start_Time", Start_Time);
                cmdserverResultSave.Parameters.AddWithValue("@End_Time", End_Time);
                cmdserverResultSave.Parameters.AddWithValue("@Total_Time", Total_Time);
                cmdserverResultSave.Parameters.AddWithValue("@Test_ID", Test_ID);
                cmdserverResultSave.Parameters.AddWithValue("@Command_Name", Command_Name);
                cmdserverResultSave.Parameters.AddWithValue("@FW_Recieve_Vals0", FW_Recieve_Vals[0]);
                cmdserverResultSave.Parameters.AddWithValue("@FW_Recieve_Vals1", FW_Recieve_Vals[1]);
                cmdserverResultSave.Parameters.AddWithValue("@FW_Recieve_Vals2", FW_Recieve_Vals[2]);
                cmdserverResultSave.Parameters.AddWithValue("@FW_Recieve_Vals3", FW_Recieve_Vals[3]);
                cmdserverResultSave.Parameters.AddWithValue("@FW_Recieve_Vals4", FW_Recieve_Vals[4]);
                cmdserverResultSave.Parameters.AddWithValue("@FW_Recieve_Vals5", FW_Recieve_Vals[5]);
                cmdserverResultSave.Parameters.AddWithValue("@FW_Recieve_Vals6", FW_Recieve_Vals[6]);
                cmdserverResultSave.Parameters.AddWithValue("@FW_Recieve_Vals7", FW_Recieve_Vals[7]);
                cmdserverResultSave.Parameters.AddWithValue("@FW_Recieve_Vals8", FW_Recieve_Vals[8]);
                cmdserverResultSave.Parameters.AddWithValue("@Tech_Name", Tech_Name);
                cmdserverResultSave.Parameters.AddWithValue("@Model_Name", Model_Name);
                cmdserverResultSave.Parameters.AddWithValue("@Factory", Factory);
                cmdserverResultSave.Parameters.AddWithValue("@Country", Country);
                cmdserverResultSave.Parameters.AddWithValue("@FW_Response", FW_Response);
                cmdserverResultSave.Parameters.AddWithValue("@Test_Name", Test_Name);
                cmdserverResultSave.Parameters.AddWithValue("@userName", StoreAndRetrieveBasicInfo.userName);
                cmdserverResultSave.Parameters.AddWithValue("@ErrorCode", ErrorCode);
                cmdserverResultSave.Parameters.AddWithValue("@Root_Cause", Root_Cause);


                y = cmdserverResultSave.ExecuteNonQuery();
                if (y != 0)
                {
                    saved = true;
                    Console.WriteLine("Test Results Successfully saved on SQL Server");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("InsertTest_Results");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("InsertTest_Results");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("InsertTest_Results");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("InsertTest_Results");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("InsertTest_Results");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return saved;
        }
        //ADD CMD Parameters Form's Queries Here....
        public bool Insert_Test_Item(string Test_Name, int station_Id, int CMD_Id, int model_Id)
        {
            bool ok = false;
            int c = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand insertTest = new SQLiteCommand("INSERT INTO Test_Items (Test_Item_Name, Station_Id, Command_Id, Model_Id) VALUES('" + Test_Name + "','" + station_Id + "','" + CMD_Id + "','" + model_Id + "')", sqliteconn);
                c = insertTest.ExecuteNonQuery();
                if (c != 0)
                {
                    ok = true;
                    Console.WriteLine("Record Inserted Successfully.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR8);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Inserting Station Test Item Mapping....
        public bool Insert_Station_Test_Item(int stn_Id, int test_Id, int Model_Id, int order)
        {
            bool ok = false;
            int c = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand insert_Stn_Test = new SQLiteCommand("INSERT INTO Station_TestItem (Station_Id, Test_Item_Id, Model_Id, Test_Item_Order) VALUES ('" + stn_Id + "', '" + test_Id + "', '" + Model_Id + "', '" + order + "')", sqliteconn);
                c = insert_Stn_Test.ExecuteNonQuery();
                if (c != 0)
                {
                    ok = true;
                    Console.WriteLine("Record Inserted Successfully.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Station_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR8);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Station_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Station_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Station_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Station_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Inserting Model Station Mapping here...
        public bool Inser_Model_Station(int station_Id, int model_Id)
        {
            bool ok = false;
            int c = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand insertModelStn = new SQLiteCommand("INSERT INTO Model_Station (Station_Id, Model_Id) VALUES('" + station_Id + "','" + model_Id + "')", sqliteconn);
                c = insertModelStn.ExecuteNonQuery();
                if (c != 0)
                {
                    ok = true;
                    Console.WriteLine("Record inserted.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Inser_Model_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR8);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Inser_Model_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Inser_Model_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Inser_Model_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Inser_Model_Station");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Getting Total Test Items COunt
        public int Get_Test_Items_Count(int ModelId, int StnId)
        {
            int y = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                DataTable testTable = new DataTable();
                SQLiteCommand getCount = new SQLiteCommand("SELECT T.Test_Item_Id, Test_Item_Name, S.Test_Item_Order  FROM Test_Items AS T  JOIN Station_TestItem AS S ON S.Test_Item_Id = T.Test_Item_Id WHERE S.Model_Id = '" + ModelId + "' AND S.Station_Id ='" + StnId + "' AND S.Test_Item_Order >='" + 1 + "'", sqliteconn);
                SQLiteDataAdapter cdAdptr = new SQLiteDataAdapter(getCount);
                cdAdptr.Fill(testTable);
                y = testTable.Rows.Count;
                if (y != 0)
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items_Count");
                    ServerErrorLog.GetERROR_SQL_Server("Number of Records:" + y);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items_Count");
                    ServerErrorLog.GetERROR_SQL_Server("No Records Found");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items_Count");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items_Count");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items_Count");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items_Count");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return y;
        }
        //Send Parameters Query Using Array for N number of Parameters...
        public bool insert_Send_Params(int test_Id, string[] sendParams, int Model_Id)
        {
            bool ok = false;
            int c = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                for (int i = 0; i < sendParams.Length; i++)
                {
                    SQLiteCommand insertModelStn = new SQLiteCommand("INSERT INTO Test_Item_Send_Parameters (Test_Item_Id, Send_Parameter, Model_Id) VALUES('" + test_Id + "','" + sendParams[i] + "','" + Model_Id + "')", sqliteconn);
                    c = insertModelStn.ExecuteNonQuery();
                    if (c != 0)
                    {
                        ok = true;
                        Console.WriteLine("Record inserted.");
                    }
                    else
                    {
                        ServerErrorLog.ERROR_Function_Name("insert_Send_Params");
                        ServerErrorLog.GetERROR_SQL_Server(ERR8);
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_Send_Params");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_Send_Params");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_Send_Params");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("insert_Send_Params");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Added by Amit to update expected parameters
        public bool Update_Expected_Paarams(int test_Id, string[] ExpectedParams, int Model_Id)
        {
            bool ok = false, updateResult = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();

                //Delete Records First

                SQLiteCommand delRanges = new SQLiteCommand("DELETE FROM Test_Item_Expected_Parameters WHERE Test_Item_Id = '" + test_Id + "'", sqliteconn);
                int y = delRanges.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    Console.WriteLine("Record Deleted form Test Items Image Table");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("DeleteImageTestItemID");
                    ServerErrorLog.GetERROR_SQL_Server(ERR6);
                }

                //Now Insert Records First
                if (ok == true)
                {
                    for (int i = 0; i < ExpectedParams.Length; i++)
                    {
                        SQLiteCommand UpdateExpectedTestItem = new SQLiteCommand("INSERT INTO Test_Item_Expected_Parameters (Test_Item_Id, Expected_Parameter, Model_Id) VALUES('" + test_Id + "','" + ExpectedParams[i] + "','" + Model_Id + "')", sqliteconn);

                        int co = UpdateExpectedTestItem.ExecuteNonQuery();
                        if (co != 0)
                        {
                            updateResult = true;
                            Console.WriteLine("Record Updated.");
                        }
                        else
                        {
                            ServerErrorLog.ERROR_Function_Name("Update_Expected_Paarams");
                            ServerErrorLog.GetERROR_SQL_Server(ERR8);
                        }
                    }
                }

            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Expected_Paarams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Expected_Paarams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Expected_Paarams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Expected_Paarams");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return updateResult;
        }
        //Expected Parameters Query Using Array for N number of Parameters...
        public bool Insert_Expected_Paarams(int test_Id, string[] ExpectedParams, int Model_Id)
        {
            bool ok = false;
            int c = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                for (int i = 0; i < ExpectedParams.Length; i++)
                {
                    SQLiteCommand insertModelStn = new SQLiteCommand("INSERT INTO Test_Item_Expected_Parameters (Test_Item_Id, Expected_Parameter, Model_Id) VALUES('" + test_Id + "','" + ExpectedParams[i] + "','" + Model_Id + "')", sqliteconn);
                    c = insertModelStn.ExecuteNonQuery();
                    if (c != 0)
                    {
                        ok = true;
                        Console.WriteLine("Record inserted.");
                    }
                    else
                    {
                        ServerErrorLog.ERROR_Function_Name("Insert_Expected_Paarams");
                        ServerErrorLog.GetERROR_SQL_Server(ERR8);
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Expected_Paarams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Expected_Paarams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Expected_Paarams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Expected_Paarams");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Inserting Test Options..
        public bool TestOptionsInsert(int test_Id,
                                      string PassFail,
                                      string wait,
                                      string contin,
                                      int retry,
                                      int retry_time,
                                      string ExternalTool,
                                      string ExternalToolPath,
                                      string TestResultPath,
                                      string SendTextOption,
                                      string CheckTextFromFw,
                                      string RangeofValues,
                                      string CheckRegLable,
                                      string CheckStopWatch,
                                      string writeRegulationNumber,
                                      string CheckComputation,
                                      string Multicommand,
                                      string checkValueFrom_FW,
                                      string SaveUserInput,
                                      string CheckTestItemRetry,
                                      string TestItemNameRetry,
                                      string retritestId,
                                      string loopcheck,
                                      int looptime,
                                      string looptestitemName,
                                      int LooptestitemId,
                                      string CheckPrinterStatus,
                                      string CheckSampleFile,
                                      string SampleFileName,
                                      string Copy_EB)
        {
            bool ok = false;
            //again set the sqliteconn obj in order to avoid conn.dispose from using statement - Sachini 8_11_2016
            SQLiteConnection sqliteconn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLitePath"].ConnectionString);//Global Connection String SQLite Server.

            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                //SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Test_Item_Options(Result_Pass_Fail, Wait, Continue, Retry, Test_Item_Id, Retry_Time, External_Tool, External_Tool_Path, Test_Result_Path, SendTextToFW, CheckTextFromFW, RangeOfValues, CheckRegLabel, StopWatch) VALUES('" + PassFail + "','" + wait + "','" + contin + "', '" + retry + "', '" + test_Id + "', '" + retry_time + "','" + ExternalTool + "','" + ExternalToolPath + "','" + TestResultPath + "','" + SendTextOption + "', '" + CheckTextFromFw + "', '" + RangeofValues + "', '" + CheckRegLable + "', '" + CheckStopWatch + "')", sqliteconn);
                //int y = cmd.ExecuteNonQuery();

                using (sqliteconn)
                {
                    sqliteconn.Open();
                    using (SQLiteTransaction mytransaction = sqliteconn.BeginTransaction())
                    {
                        using (SQLiteCommand mycommand = new SQLiteCommand(sqliteconn))
                        {
                            mycommand.CommandText = "INSERT INTO Test_Item_Options(Result_Pass_Fail, Wait, Continue, Retry, Test_Item_Id, Retry_Time, External_Tool, External_Tool_Path, Test_Result_Path, SendTextToFW, CheckTextFromFW, RangeOfValues, CheckRegLabel, StopWatch,Send_Scan_regLable, Computation, MultiCommandOption, Check_Params_From_FW, Save_User_Input,CheckTestItemName,TestItemNameForRetry,RetryTestItemId,CheckLoop,NumberOfLoop,LoopTestItemName,LoopTestItemId,CheckPrinterStatus,CheckSampleFile,SampleFileName,Copy_EB_SN) VALUES('"
                                                    + PassFail + "','" 
                                                    + wait + "','" 
                                                    + contin + "', '" 
                                                    + retry + "', '" 
                                                    + test_Id + "', '" 
                                                    + retry_time + "','" 
                                                    + ExternalTool + "','" 
                                                    + ExternalToolPath + "','" 
                                                    + TestResultPath + "','" 
                                                    + SendTextOption + "', '" 
                                                    + CheckTextFromFw + "', '" 
                                                    + RangeofValues + "', '" 
                                                    + CheckRegLable + "', '" 
                                                    + CheckStopWatch + "', '" 
                                                    + writeRegulationNumber + "', '" 
                                                    + CheckComputation + "', '" 
                                                    + Multicommand + "', '" 
                                                    + checkValueFrom_FW + "','" 
                                                    + SaveUserInput + "','" 
                                                    + CheckTestItemRetry + "', '" 
                                                    + TestItemNameRetry + "','" 
                                                    + retritestId + "','" 
                                                    + loopcheck +"','"
                                                    + looptime +"','" 
                                                    + looptestitemName + "','"
                                                    + LooptestitemId + "','"
                                                    + CheckPrinterStatus + "','"
                                                    + CheckSampleFile + "','"
                                                    + SampleFileName + "','"
                                                    + Copy_EB +"')";
                            int y = mycommand.ExecuteNonQuery();
                            if (y != 0)
                            {
                                ok = true;
                                Console.WriteLine("Details Saved Successfully, in Test_Item_Options.");
                            }
                            else
                            {
                                ok = false;
                                ServerErrorLog.ERROR_Function_Name("TestOptionsInsert");
                                ServerErrorLog.GetERROR_SQL_Server(ERR8);
                            }
                            mytransaction.Commit();
                        }
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("TestOptionsInsert");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("TestOptionsInsert");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("TestOptionsInsert");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    MessageBox.Show(xc.ToString());
                    MessageBox.Show(xc.Message);
                    ServerErrorLog.ERROR_Function_Name("TestOptionsInsert");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Delete Image if already exist, added by Amit on 12 Feb 2016
        public bool DeleteImageTestItemID(int testId)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand delUserCmd = new SQLiteCommand("DELETE FROM Test_Items_Image WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                int y = delUserCmd.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    Console.WriteLine("Record Deleted form Test Items Image Table");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("DeleteImageTestItemID");
                    ServerErrorLog.GetERROR_SQL_Server(ERR6);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("DeleteImageTestItemID");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("DeleteImageTestItemID");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("DeleteImageTestItemID");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("DeleteImageTestItemID");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Deleting Expected and Send Parameters from Sqlite DB for given Test Item to Update new values....
        public bool Delete_TestExpectedVal(int testId)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdchk = new SQLiteCommand("SELECT Expected_Parameter FROM Test_Item_Expected_Parameters WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader dr = cmdchk.ExecuteReader();
                if (dr.Read())
                {
                    SQLiteCommand cmdchkPrm = new SQLiteCommand("DELETE FROM Test_Item_Expected_Parameters WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                    int y = cmdchkPrm.ExecuteNonQuery();
                    if (y != 0)
                    {
                        ok = true;
                        Console.WriteLine("Record Deleted form Expected Params.");
                    }
                    else
                    {
                        ok = false;
                        Console.WriteLine("Expected Params are not Deleted For Test Item.");
                    }
                }
                else
                {
                    ok = true;
                    ServerErrorLog.ERROR_Function_Name("Delete_TestExpectedVal");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_TestExpectedVal");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_TestExpectedVal");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_TestExpectedVal");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_TestExpectedVal");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Delete Send Parameters....
        public bool DeleteTestSendVal(int testId)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdchk = new SQLiteCommand("SELECT Send_Parameter FROM Test_Item_Send_Parameters WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader sdr = cmdchk.ExecuteReader();
                if (sdr.Read())
                {
                    SQLiteCommand cmdchkPrm = new SQLiteCommand("DELETE FROM Test_Item_Send_Parameters WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                    int y = cmdchkPrm.ExecuteNonQuery();
                    if (y != 0)
                    {
                        ok = true;
                        Console.WriteLine("Record Deleted. Send Parameters");
                    }
                    else
                    {
                        ok = false;
                        Console.WriteLine("Record is not deleted. Send parameters.");
                    }
                }
                else
                {
                    ok = true;
                    ServerErrorLog.ERROR_Function_Name("DeleteTestSendVal");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("DeleteTestSendVal");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("DeleteTestSendVal");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("DeleteTestSendVal");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("DeleteTestSendVal");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Deleting Test Item Options by Test Item Id..
        public bool testOption_Delete(int testId)
        {
            bool ret = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                using (sqliteconn)
                {
                    sqliteconn.Open();
                    using (SQLiteTransaction newTrans = sqliteconn.BeginTransaction())
                    {
                        using (SQLiteCommand sqliteCmd = new SQLiteCommand(sqliteconn))
                        {
                            SQLiteCommand cmdchk = new SQLiteCommand("SELECT Result_Pass_Fail FROM Test_Item_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                            SQLiteDataReader sdr = cmdchk.ExecuteReader();
                            if (sdr.Read())
                            {
                                SQLiteCommand cmdTestOption = new SQLiteCommand("DELETE FROM Test_Item_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                                int y = cmdTestOption.ExecuteNonQuery();
                                if (y != 0)
                                {
                                    ret = true;
                                    Console.WriteLine("Record is deleted form Test Options");
                                }
                                else
                                {
                                    Console.WriteLine("Record is not deleted form Test Options");
                                    ret = false;
                                }
                            }
                            else
                            {
                                ret = true;
                                ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                                ServerErrorLog.GetERROR_SQL_Server(ERR1);
                            }
                        }
                        newTrans.Commit();
                    }
                    sqliteconn.Close();
                }

            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ret;
        }
        //below function added by Amit on 12 Feb 2016 to check test item ID presence in Test Items Image table
        public bool CheckTestItemIDFromImageTable(int Test_Id)
        {
            bool IsTestItemImagePresent = true;

            try
            {

                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();

                SQLiteCommand cmdTestItemIDImage = new SQLiteCommand("SELECT Test_Item_Id FROM Test_Items_Image WHERE Test_Item_Id ='" + Test_Id + "' ", sqliteconn);
                SQLiteDataReader ImageTestID = cmdTestItemIDImage.ExecuteReader();

                if (!ImageTestID.Read())
                {
                    IsTestItemImagePresent = false;

                }

            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }

            return IsTestItemImagePresent;

        }
        //below function added by Amit on 10 Feb 2016 to check test item ID presence in TestItemOptions table
        public bool CheckTestItemIDFromExpected(int Test_Id)
        {
            bool IsTestItemPresent = true;

            try
            {
                sqliteconn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLitePath"].ConnectionString);//Global Connection String SQLite Server. Sachini 8/16/2016
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();

                SQLiteCommand cmdTestItemIDOpt = new SQLiteCommand("SELECT Test_Item_Id FROM Test_Item_Expected_Parameters WHERE Test_Item_Id ='" + Test_Id + "' ", sqliteconn);
                SQLiteDataReader opdrTestID = cmdTestItemIDOpt.ExecuteReader();

                //int RetrivedTestItemID = opdrTestID.GetInt32(0);

                //if (RetrivedTestItemID == Test_Id)
                //{
                //    IsTestItemPresent = false;

                //}

                if (!opdrTestID.Read())
                {
                    IsTestItemPresent = false;

                }



            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }

            return IsTestItemPresent;

        }
        //below function added by Amit on 8 Feb 2016 to check test item ID presence in TestItemOptions table
        public bool CheckTestItemIDFromOPtions(int Test_Id)
        {
            bool IsTestItemPresent = true;

            try
            {

                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();

                SQLiteCommand cmdTestItemIDOpt = new SQLiteCommand("SELECT Test_Item_Id FROM Test_Item_Options WHERE Test_Item_Id ='" + Test_Id + "' ", sqliteconn);
                SQLiteDataReader opdrTestID = cmdTestItemIDOpt.ExecuteReader();

                //int RetrivedTestItemID = opdrTestID.GetInt32(0);

                //if (RetrivedTestItemID == Test_Id)
                //{
                //    IsTestItemPresent = false;

                //}

                if (!opdrTestID.Read())
                {
                    IsTestItemPresent = false;

                }



            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("testOption_Delete");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }

            return IsTestItemPresent;

        }
        //Getting all options for test item.
        public string[] Get_testOptions(int Test_Id)
        {
            string[] testOpt = new string[32];
            try
            {
                //SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                //sqliteconn.Close();
                //GC.Collect();
                //sqliteconn.Open();
                //SQLiteCommand cmdTestOpt = new SQLiteCommand("SELECT Result_Pass_Fail, Wait, Continue, Retry, Retry_Time, External_Tool, External_Tool_Path, Test_Result_Path, SendTextToFW, CheckTextFromFW, RangeOfValues, CheckRegLabel, StopWatch, Send_Scan_regLable, Computation, MultiCommandOption, Check_Params_From_FW ,CheckTestItemName,TestItemNameForRetry,RetryTestItemId,CheckLoop,NumberOfLoop,LoopTestItemName,LoopTestItemId,CheckSampleFile,SampleFileName,Copy_EB_SN FROM Test_Item_Options WHERE Test_Item_Id ='" + Test_Id + "' ", sqliteconn);
                //SQLiteDataReader opdr = cmdTestOpt.ExecuteReader();
                //if (opdr == null)
                //{
                //    testOpt = null;

                //}
                //else
                //{
                //    while (opdr.Read())
                //    {
                //        testOpt[0] = opdr[0].ToString();
                //        testOpt[1] = opdr[1].ToString();
                //        testOpt[2] = opdr[2].ToString();
                //        testOpt[3] = opdr[3].ToString();
                //        testOpt[4] = opdr[4].ToString();
                //        testOpt[5] = opdr[5].ToString();
                //        testOpt[6] = opdr[6].ToString();
                //        testOpt[7] = opdr[7].ToString();
                //        testOpt[8] = opdr[8].ToString();
                //        testOpt[9] = opdr[9].ToString();
                //        testOpt[10] = opdr[10].ToString();
                //        testOpt[11] = opdr[11].ToString();
                //        testOpt[12] = opdr[17].ToString();
                //        testOpt[13] = opdr[18].ToString();
                //        testOpt[14] = opdr[19].ToString();

                //        testOpt[15] = opdr[20].ToString();
                //        testOpt[16] = opdr[21].ToString();
                //        testOpt[17] = opdr[22].ToString();
                //        testOpt[18] = opdr[23].ToString();

                //        testOpt[19] = opdr[24].ToString();
                //        testOpt[20] = opdr[25].ToString();
                //        testOpt[21] = opdr[26].ToString();
                //    }
                //}

                //modified by nitish to retrieve all values from test_item_option 

                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdTestOpt = new SQLiteCommand("SELECT * FROM Test_Item_Options WHERE Test_Item_Id ='" + Test_Id + "' ", sqliteconn);
                SQLiteDataReader opdr = cmdTestOpt.ExecuteReader();
                if (opdr == null)
                {
                    testOpt = null;

                }
                else
                {
                    //so all 32 test option are in test option now  by nitish
                    int count = opdr.FieldCount;
                    while (opdr.Read())
                    {
                        for (int i = 0; i < count; i++)
                        {
                            Console.WriteLine(opdr.GetValue(i));
                            testOpt[i] = opdr[i].ToString();
                        }
                            
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_testOptios");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_testOptios");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_testOptios");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_testOptios");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return testOpt;

        }
        //Add CMD Parameters Queries ends here...
        //Insert New Command IN DB.
        public bool Insert_CMD(string cmdName)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand CheckCMD = new SQLiteCommand("SELECT Command_Name FROM Commands WHERE Command_Name = '" + cmdName + "'", sqliteconn);
                SQLiteDataReader cdr = CheckCMD.ExecuteReader();//Checking For Same Command if Exists.
                if (cdr.HasRows)
                {
                    cdr.Dispose();
                    MessageBox.Show(ERR7);
                }
                else
                {
                    SQLiteCommand insertCmd = new SQLiteCommand("INSERT INTO Commands(Command_Name) VALUES('" + cmdName + "')", sqliteconn);
                    int y = insertCmd.ExecuteNonQuery();
                    if (y != 0)
                    {
                        ok = true;
                        Console.WriteLine("Command Added Successfully, Commands Table.");
                        ServerErrorLog.ERROR_Function_Name("Insert_CMD");
                        ServerErrorLog.GetERROR_SQL_Server("Success Fully Written in SQL Server...Command");
                    }
                    else
                    {
                        ok = false;
                        ServerErrorLog.ERROR_Function_Name("Insert_CMD");
                        ServerErrorLog.GetERROR_SQL_Server(ERR8);
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_CMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_CMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_CMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_CMD");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Insert New Station.
        public bool Insert_Station(string Station_Name)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand CheckStn = new SQLiteCommand("SELECT Station_Name FROM Station WHERE Station_Name = '" + Station_Name + "'", sqliteconn);
                SQLiteDataReader cdr = CheckStn.ExecuteReader();//Checking For Same Command if Exists.
                if (cdr.HasRows)
                {
                    cdr.Dispose();
                    MessageBox.Show(ERR7);
                }
                else
                {
                    SQLiteCommand insertStn = new SQLiteCommand("INSERT INTO Station(Station_Name) VALUES('" + Station_Name + "')", sqliteconn);
                    int y = insertStn.ExecuteNonQuery();
                    if (y != 0)
                    {
                        ok = true;
                        Console.WriteLine("Station Added Successfully, Commands Table.");
                    }
                    else
                    {
                        ok = false;
                        ServerErrorLog.ERROR_Function_Name("Insert_Station");
                        ServerErrorLog.GetERROR_SQL_Server(ERR8);
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Station");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Get User Details To Edit User..
        public string[] Get_User(string userName)
        {
            string[] userDetails = new string[2];
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getUserCmd = new SQLiteCommand("SELECT password, role  FROM userData WHERE userName = '" + userName + "'", sqliteconn);
                SQLiteDataReader udr = getUserCmd.ExecuteReader();
                if (udr.Read())
                {
                    userDetails[0] = udr[0].ToString();
                    userDetails[1] = udr[1].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_User");
                    ServerErrorLog.GetERROR_SQL_Server("Record Not Found.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_User");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_User");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_User");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_User");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return userDetails;
        }
        //ADD user and Edit User Form... And Checking for Duplicate UserName.
        public bool Add_User(string UserName, string PWD, int role, string sRole)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand chkUser = new SQLiteCommand("SELECT userName FROM userData WHERE userName = '" + UserName + "'", sqliteconn);
                SQLiteDataReader udr = chkUser.ExecuteReader();
                if (udr.HasRows)
                {
                    udr.Dispose();
                    MessageBox.Show(ERR7);
                }
                else
                {
                    SQLiteCommand insert_User = new SQLiteCommand("INSERT INTO userData (userName, password, role, sRole) values('" + UserName + "', '" + PWD + "', '" + role + "', '" + sRole + "')", sqliteconn);
                    int y = insert_User.ExecuteNonQuery();
                    if (y != 0)
                    {
                        ok = true;
                        Console.WriteLine("User Added Succesfully.");
                    }
                    else
                    {
                        ok = false;
                        ServerErrorLog.ERROR_Function_Name("Add_User");
                        ServerErrorLog.GetERROR_SQL_Server(ERR8);
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Add_User");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Add_User");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Add_User");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Add_User");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Edit User Details.//Cannot change UserName...
        public bool EditUser(string userName, string password, int role, string SRole)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand EdituserCMD = new SQLiteCommand("UPDATE userData SET userName='" + userName + "', password='" + password + "',role='" + role + "',sRole='" + SRole + "' WHERE userName='" + userName + "'", sqliteconn);
                int y = EdituserCMD.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    Console.WriteLine("Record Updated Succesfully.,UserData");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("EditUser");
                    ServerErrorLog.GetERROR_SQL_Server(ERR8);
                }

            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("EditUser");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("EditUser");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("EditUser");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("EditUser");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Delete User...
        public bool DeleteUser(string UserName)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand delUserCmd = new SQLiteCommand("DELETE FROM userData WHERE userName = '" + UserName + "'", sqliteconn);
                int y = delUserCmd.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    ServerErrorLog.ERROR_Function_Name("DeleteUser");
                    ServerErrorLog.GetERROR_SQL_Server("Record Deleted Succesfully. userData.");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("DeleteUser");
                    ServerErrorLog.GetERROR_SQL_Server(ERR6);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("DeleteUser");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("DeleteUser");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("DeleteUser");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("DeleteUser");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Insert Sample File
        public bool Upload_Sample_File(string File_Name, string Path, string Size, int test_Id)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand insertSample = new SQLiteCommand("INSERT INTO Sample_Files(Sample_File_Name, Sample_File_Path,Sample_File_Size,Test_Item_Id) VALUES('" + File_Name + "','" + Path + "','" + Size + "','" + test_Id + "')", sqliteconn);
                int y = insertSample.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    Console.WriteLine("Record Inserted Succesfully. Sample File Table.");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Upload_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Upload_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Upload_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Upload_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Upload_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Image Upload for Test Items..
        //Mod
        public bool Upload_Image_Test_Item(String FileName, String ImageLoation, String Tech, String model, int test_Id)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand insertImage = new SQLiteCommand("INSERT INTO Test_Items_Image(Image_Name, Image_Path,Test_Item_Id) VALUES('" + FileName + "','" + ImageLoation + "','" + test_Id + "')", sqliteconn);
                //SQLiteCommand insertImage = new SQLiteCommand("INSERT INTO Test_Items_Image(Image_Name, Image_Path,Test_Item_Id) VALUES('" + FileName + "','" + Path + "\\" + Tech + "\\" + model + "\\Icon\\" + "','" + test_Id + "')", sqliteconn);

                int y = insertImage.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    Console.WriteLine("Record Inserted seccesfully, Test Item Image.");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Upload_Image_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Upload_Image_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Upload_Image_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Upload_Image_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Upload_Image_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Get Model Name & Technology Name Mathod.. This will return an array...
        public string[] GetModel_Tech(int Model_Id)
        {
            string[] model_Tech = new string[2];
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getModelTech = new SQLiteCommand("SELECT Model_name,P.Product_Type FROM Model AS M LEFT OUTER JOIN Product AS P ON  M.Model_Id = '" + Model_Id + "'  Where P.Product_Id= M.Product_Id", sqliteconn);
                SQLiteDataReader mdr = getModelTech.ExecuteReader();
                if (mdr.Read())
                {
                    model_Tech[0] = mdr[0].ToString();
                    model_Tech[1] = mdr[1].ToString();
                    Console.WriteLine("Received Records Succesfully.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetModel_Tech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetModel_Tech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetModel_Tech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetModel_Tech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetModel_Tech");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return model_Tech;
        }
        //Insert into Moddel Command mapping table...//Mapping Command with Technology Here.. Inserting data into technology-command mapping table here..
        public bool Insert_Model_Command_Tech(int Model_Id, int CMD_Id, int Tech_Id)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand Mapp_CMD_model_Tech = new SQLiteCommand("INSERT INTO Product__Model_Command (Product_Id, Command_Id, Model_Id) VALUES('" + Tech_Id + "', '" + CMD_Id + "', '" + Model_Id + "')", sqliteconn);
                int y = Mapp_CMD_model_Tech.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    Console.WriteLine("Succesfully Inserted Record In Model_Command_Tech Table.");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Insert_Model_Command_Tech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR8);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Model_Command_Tech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Model_Command_Tech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Model_Command_Tech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Model_Command_Tech");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Loading Model For Listbox Of Whole Table here...
        public DataTable Load_Model_Table()
        {
            DataTable ModelTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand LoadModelCMD = new SQLiteCommand("SELECT Model_Name, Model_Id, Model_Code FROM Model ", sqliteconn);
                SQLiteDataAdapter MdlAdptre = new SQLiteDataAdapter(LoadModelCMD);
                MdlAdptre.Fill(ModelTable);
                int c = ModelTable.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("table Loaded Succesfully. Product");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Model_Table");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Model_Table");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Model_Table");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Model_Table");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Model_Table");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ModelTable;
        }
        //Loading Model Table Based on Technology Id..... (For Combobox)
        public DataTable Load_Model(int Tech_Id)
        {
            DataTable ModelTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand LoadModelCMD = new SQLiteCommand("SELECT Model_Name, Model_Id, Model_Code FROM Model WHERE Product_Id = '" + Tech_Id + "'", sqliteconn);
                SQLiteDataAdapter mdlAdptre = new SQLiteDataAdapter(LoadModelCMD);
                mdlAdptre.Fill(ModelTable);
                int c = ModelTable.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("table Loaded Succesfully. Model");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Model");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ModelTable;
        }
        //Loading Technology Table Here... (For Comboboxs...)
        public DataTable LoadTech()
        {
            DataTable TechTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand LoadTechCMD = new SQLiteCommand("SELECT Product_Type, Product_Id FROM Product ", sqliteconn);
                SQLiteDataAdapter PrdctAdptre = new SQLiteDataAdapter(LoadTechCMD);
                PrdctAdptre.Fill(TechTable);
                int c = TechTable.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("table Loaded Succesfully. Product");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("LoadTech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("LoadTech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("LoadTech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("LoadTech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("LoadTech");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return TechTable;
        }
        //Loading Stations Here..
        public DataTable Load_Station()
        {
            DataTable StnTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand LoadStnCMD = new SQLiteCommand("SELECT Station_Name, Station_Id FROM Station ", sqliteconn);
                SQLiteDataAdapter StnAdptre = new SQLiteDataAdapter(LoadStnCMD);
                StnAdptre.Fill(StnTable);
                int c = StnTable.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("table Loaded Succesfully. Product");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Station");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return StnTable;
        }
        //Loading Commands Here..
        public DataTable Load_Cmd()
        {
            DataTable CMDTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand LoadCommandCMD = new SQLiteCommand("SELECT Command_Name, Command_Id FROM Commands ", sqliteconn);
                SQLiteDataAdapter CMDAdptre = new SQLiteDataAdapter(LoadCommandCMD);
                CMDAdptre.Fill(CMDTable);
                int c = CMDTable.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("table Loaded Succesfully. Product");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Cmd");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Cmd");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Cmd");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Cmd");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Cmd");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return CMDTable;
        }
        //Getting Station based On Model Id Here...
        public DataTable get_Station_table(int Model_Id)
        {
            DataTable stnTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getStnCMD = new SQLiteCommand("SELECT Station_Name, S.Station_Id FROM Station AS S LEFT OUTER JOIN Model_Station AS M ON  M.Model_Id ='" + Model_Id + "' Where M.Station_Id= S.Station_Id", sqliteconn);
                SQLiteDataAdapter stnAdptr = new SQLiteDataAdapter(getStnCMD);
                stnAdptr.Fill(stnTable);
                int y = stnTable.Rows.Count;
                if (y != 0)
                {
                    //DO NOTHING...
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Station_table");
                    ServerErrorLog.GetERROR_SQL_Server("Check dataBase For More Information..");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Station_table");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Station_table");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Station_table");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Station_table");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return stnTable;
        }
        //Update Station Here...
        public bool Update_Stn(string Statn_Name, int Stn_id)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand UpdateStnCmd = new SQLiteCommand("UPDATE Station SET Station_Name='" + Statn_Name + "' WHERE Station_Id='" + Stn_id + "'", sqliteconn);
                int y = UpdateStnCmd.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    //MessageBox.Show("Station Updated Succesfully.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Stn");
                    ServerErrorLog.GetERROR_SQL_Server("Record Is Not Updated...");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Stn");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Stn");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Stn");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Stn");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Getting Command Id by Command Name...
        public int get_CMD_Id(string cmdName)
        {
            int cmdId = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getCMD = new SQLiteCommand("SELECT Command_Id FROM Commands WHERE Command_Name = '" + cmdName + "'", sqliteconn);
                SQLiteDataReader cdr = getCMD.ExecuteReader();
                if (cdr.Read())
                {
                    cmdId = cdr.GetInt32(0);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_CMD_Id");
                    ServerErrorLog.GetERROR_SQL_Server("NO Such Record. or Check Databse.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_CMD_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_CMD_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_CMD_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_CMD_Id");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return cmdId;
        }
        //Getting Mapped Commands
        public DataTable Get_MappedCMD(int Tech_Id, int Model_Id)
        {
            DataTable mappedCMDTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand GetMappedCMD = new SQLiteCommand("SELECT Command_Name FROM Commands AS S LEFT OUTER JOIN Product__Model_Command AS C ON S.Command_Id = C.Command_Id WHERE C.Product_Id ='" + Tech_Id + "' AND C.Model_Id = '" + Model_Id + "'", sqliteconn);
                SQLiteDataAdapter mappCMDAdptr = new SQLiteDataAdapter(GetMappedCMD);
                mappCMDAdptr.Fill(mappedCMDTable);
                int c = mappedCMDTable.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("table loaded succesfully. Mapped CMds,");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MappedCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MappedCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MappedCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MappedCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MappedCMD");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return mappedCMDTable;
        }
        //Delete form Mapped commands(Un Mapp Commands Here....)Also Checking first if command exists..in Mapping Table.
        public bool Un_Map_CMD(int Tech_Id, int CMD_Id, int Model_Id)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                //Checking If Command is Existing In DB(If command is Mapped..)
                SQLiteCommand ChkMappCMD = new SQLiteCommand("SELECT Product_Id, Command_Id, Model_Id FROM Product__Model_Command WHERE Product_Id = '" + Tech_Id + "' AND Command_Id = '" + CMD_Id + "' AND Model_Id = '" + Model_Id + "'", sqliteconn);
                SQLiteDataReader CMDRdr = ChkMappCMD.ExecuteReader();
                if (CMDRdr.HasRows)
                {
                    SQLiteCommand UnMapCMD = new SQLiteCommand("DELETE  FROM Product__Model_Command WHERE Product_Id = '" + Tech_Id + "' AND Command_Id = '" + CMD_Id + "' AND Model_Id = '" + Model_Id + "'", sqliteconn);
                    int y = UnMapCMD.ExecuteNonQuery();
                    if (y != 0)
                    {
                        ok = true;
                        Console.WriteLine("Deleted Record Succesfully. CMD Mapping Table.");
                    }
                    else
                    {
                        ok = false;
                        Console.WriteLine("Record is not deleted succesfully., CMD Mapping Table.");
                    }
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Map_CMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Map_CMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Map_CMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Map_CMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Map_CMD");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        ////Mapp Commands.....(Also Checking for existing mapped command if any..)
        public bool Mapp_Commands(int Tech, int cmdId, int model_Id)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                //Checking If Command is Existing In DB(If command is Mapped..)
                SQLiteCommand ChkMappCMD = new SQLiteCommand("SELECT Product_Id, Command_Id, Model_Id FROM Product__Model_Command WHERE Product_Id = '" + Tech + "' AND Command_Id = '" + cmdId + "' AND Model_Id = '" + model_Id + "'", sqliteconn);
                SQLiteDataReader CMDRdr = ChkMappCMD.ExecuteReader();
                if (CMDRdr.HasRows)
                {
                    MessageBox.Show("Selected Command Is already Mapped.");
                }
                else
                {
                    SQLiteCommand insertCMDMapp = new SQLiteCommand("INSERT INTO Product__Model_Command(Product_Id, Command_Id, Model_Id) VALUES ('" + Tech + "','" + cmdId + "', '" + model_Id + "')", sqliteconn);
                    int y = insertCMDMapp.ExecuteNonQuery();
                    if (y != 0)
                    {
                        ok = true;
                        ServerErrorLog.ERROR_Function_Name("Mapp_Commands");
                        ServerErrorLog.GetERROR_SQL_Server("Command Mapped Succesfully.");
                    }
                    else
                    {
                        ok = false;
                        ServerErrorLog.ERROR_Function_Name("Mapp_Commands");
                        ServerErrorLog.GetERROR_SQL_Server("Command is not Mapped Succesfully.");
                    }
                }
            }
            catch (Exception xc)
            {

                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Mapp_Commands");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Mapp_Commands");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Mapp_Commands");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Mapp_Commands");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        ////Mapp Models Here....
        public bool Mapp_Station(int model_id, int Stn_Id)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                //Checking If Model is Existing In DB(If Model is already Mapped..)
                SQLiteCommand ChkMappModel = new SQLiteCommand("SELECT Model_Id, Station_Id FROM Model_Station WHERE Model_Id = '" + model_id + "' AND Station_Id = '" + Stn_Id + "'", sqliteconn);
                SQLiteDataReader CMDRdr = ChkMappModel.ExecuteReader();
                if (CMDRdr.HasRows)
                {
                    MessageBox.Show("Selected Command Is already Mapped.");
                }
                else
                {
                    SQLiteCommand insertModelMapp = new SQLiteCommand("INSERT INTO Model_Station (Station_Id, Model_Id) VALUES('" + Stn_Id + "','" + model_id + "')", sqliteconn);
                    int y = insertModelMapp.ExecuteNonQuery();
                    if (y != 0)
                    {
                        ok = true;
                        ServerErrorLog.ERROR_Function_Name("Mapp_Station");
                        ServerErrorLog.GetERROR_SQL_Server("Command Mapped Succesfully.");
                    }
                    else
                    {
                        ok = false;
                        ServerErrorLog.ERROR_Function_Name("Mapp_Station");
                        ServerErrorLog.GetERROR_SQL_Server("Command is not Mapped Succesfully.");
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Mapp_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Mapp_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Mapp_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Mapp_Station");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        ////GEt Mapped Stations here.....
        public DataTable Get_Mapped_Stns(int Model_Id)
        {
            DataTable mappeStnTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand GetMappedCMD = new SQLiteCommand("SELECT Station_Name, S.Station_Id FROM Model_Station AS S LEFT OUTER JOIN Station AS M ON S.Station_Id = M.Station_Id WHERE S.Model_Id = '" + Model_Id + "'", sqliteconn);
                SQLiteDataAdapter mappCMDAdptr = new SQLiteDataAdapter(GetMappedCMD);
                mappCMDAdptr.Fill(mappeStnTable);
                int c = mappeStnTable.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("table loaded succesfully. Mapped CMds,");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Stns");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Stns");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Stns");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Stns");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Stns");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return mappeStnTable;
        }
        ////Getting Station Id..
        public int Get_Station_Id(string StnName)
        {
            int StnId = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getStanId = new SQLiteCommand("SELECT Station_Id FROM Station WHERE Station_Name = '" + StnName + "'", sqliteconn);
                SQLiteDataReader cdr = getStanId.ExecuteReader();
                if (cdr.Read())
                {
                    StnId = cdr.GetInt32(0);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Station_Id");
                    ServerErrorLog.GetERROR_SQL_Server("NO Such Record. or Check Databse.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Station_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Station_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Station_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Station_Id");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return StnId;
        }
        ////Un Mapping Models From Technology...(Deleting Records from Table to Un Mapp existing Stations.)
        public bool Un_Mapp_Stns(int model, int stn)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                //Checking If Stations is Existing In DB(If Stations is Mapped..)
                SQLiteCommand ChkMapStn = new SQLiteCommand("SELECT Model_Id, Station_Id FROM Model_Station WHERE Model_Id = '" + model + "' AND Station_Id = '" + stn + "'", sqliteconn);
                SQLiteDataReader CMDRdr = ChkMapStn.ExecuteReader();
                if (CMDRdr.HasRows)
                {
                    SQLiteCommand UnMapStn = new SQLiteCommand("DELETE  FROM Model_Station WHERE Model_Id = '" + model + "' AND Station_Id = '" + stn + "'", sqliteconn);
                    int y = UnMapStn.ExecuteNonQuery();
                    if (y != 0)
                    {
                        ok = true;
                        Console.WriteLine("Deleted Record Succesfully. CMD Mapping Table.");
                    }
                    else
                    {
                        ok = false;
                        Console.WriteLine("Record is not deleted succesfully., CMD Mapping Table.");
                    }
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Mapp_Stns");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Mapp_Stns");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Mapp_Stns");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Mapp_Stns");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Mapp_Stns");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        ////Add new Model Here...
        public bool AddModel(string MdlName, string MdlCode, string FW_Version, int Tech_Id, string HardwareID, string driver_name)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand chkModel = new SQLiteCommand("SELECT Model_Name, Model_Code, Model_FW_Version, Product_Id FROM Model WHERE Model_Name = '" + MdlName + "'", sqliteconn);
                SQLiteDataReader mdr = chkModel.ExecuteReader();
                if (mdr.Read())
                {
                    Console.WriteLine(ERR7);
                    MessageBox.Show("Record Already Exists.");
                }
                else
                {
                    SQLiteCommand addModelCMD = new SQLiteCommand("INSERT INTO Model (Model_Name, Model_Code, Model_FW_Version, Product_Id, HardwareID,Driver_Name) values('" + MdlName + "','" + MdlCode + "','" + FW_Version + "', '" + Tech_Id + "','" + HardwareID + "','" + driver_name + "')", sqliteconn);
                    int y = addModelCMD.ExecuteNonQuery();
                    if (y != 0)
                    {
                        ok = true;
                        Console.WriteLine("Record Added Succesfully., Model Table");
                    }
                    else
                    {
                        ok = false;
                        ServerErrorLog.ERROR_Function_Name("AddModel");
                        ServerErrorLog.GetERROR_SQL_Server(ERR8);
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("AddModel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("AddModel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("AddModel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("AddModel");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        ////Getting Stations based on Perticuler Model...
        public DataTable GetStation(int Model_Id)
        {
            DataTable stnTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getStnCMD = new SQLiteCommand("SELECT Station_Name, S.Station_Id FROM Station AS S LEFT OUTER JOIN Model_Station AS M ON  M.Model_Id ='" + Model_Id + "' Where M.Station_Id= S.Station_Id", sqliteconn);
                SQLiteDataAdapter stnAdptr = new SQLiteDataAdapter(getStnCMD);
                stnAdptr.Fill(stnTable);
                int c = stnTable.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("Rcords Loaded Successfully, Station table.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetStation");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetStation");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetStation");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetStation");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetStation");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return stnTable;
        }
        ////Creating New Technology Here and Checking for Duplicate Records also...
        public bool AddTechnology(string Tech_Name)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand chkTech = new SQLiteCommand("SELECT Product_Type FROM Product WHERE Product_Type = '" + Tech_Name + "'", sqliteconn);
                SQLiteDataReader trdr = chkTech.ExecuteReader();
                if (trdr.HasRows)
                {
                    Console.WriteLine("Record Already Exists .... Product Table...");
                }
                else
                {
                    SQLiteCommand addTechCMD = new SQLiteCommand("INSERT INTO Product (Product_Type) VALUES('" + Tech_Name + "')", sqliteconn);
                    int y = addTechCMD.ExecuteNonQuery();
                    if (y != 0)
                    {
                        ok = true;
                        Console.WriteLine("Record Added Succesfully. Technology(Product Table)");
                    }
                    else
                    {
                        ok = false;
                        ServerErrorLog.ERROR_Function_Name("AddTechnology");
                        ServerErrorLog.GetERROR_SQL_Server(ERR8);
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("AddTechnology");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("AddTechnology");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("AddTechnology");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("AddTechnology");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        ////Remaining For TMFC Form..??What is it?
        ////Getting Model Details based on Model_Id....Returns Array of String Type.
        public string[] GetModelDetails(int Model_id)
        {
            string[] ModelDetails = new string[3];
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand ModelDetailCMD = new SQLiteCommand("SELECT Model_Name, Model_Code, FW_Version WHERE Model_Id ='" + Model_id + "'", sqliteconn);
                SQLiteDataReader Mdr = ModelDetailCMD.ExecuteReader();
                if (Mdr.Read())
                {
                    ModelDetails[0] = Mdr[0].ToString();
                    ModelDetails[1] = Mdr[1].ToString();
                    ModelDetails[2] = Mdr[2].ToString();
                    Console.WriteLine("Succesfully got data., Model");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetModelDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetModelDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetModelDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetModelDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetModelDetails");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ModelDetails;
        }
        ////Test Results Report Queries Here...//based In Start Date.
        public DataTable Get_Test_Result_OnStartDate(string _StartDate)
        {
            DataTable Test_Results_StartDate = new DataTable();
            try
            {
                string StartDate = _StartDate + " 00:00:00.00";
                string EndDate = _StartDate + " 23:59:59.999";

                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand Get_Results_on_StartDateCMD = new SQLiteCommand(" SELECT * FROM Test_Results  Where Test_Start_Date= '" + _StartDate + "'", sqliteconn);
                SQLiteDataAdapter TestAdptr = new SQLiteDataAdapter(Get_Results_on_StartDateCMD);
                TestAdptr.Fill(Test_Results_StartDate);
                int c = Test_Results_StartDate.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("Table Loaded Succesfully., Test_Results on Start date.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_OnStartDate");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }

            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_OnStartDate");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_OnStartDate");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_OnStartDate");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_OnStartDate");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Test_Results_StartDate;
        }
        ////Test Results based On Technology..
        public DataTable Get_Test_Results_OnTechnology(string Tech)
        {
            DataTable Test_Results_Tech = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand Get_Results_on_TechCMD = new SQLiteCommand(" SELECT * FROM Test_Results  Where Technology_Name = '" + Tech + "'", sqliteconn);
                SQLiteDataAdapter TestAdptr = new SQLiteDataAdapter(Get_Results_on_TechCMD);
                TestAdptr.Fill(Test_Results_Tech);
                int c = Test_Results_Tech.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("Table Loaded Succesfully., Test_Results on Tech.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnTechnology");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }

            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnTechnology");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnTechnology");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnTechnology");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnTechnology");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Test_Results_Tech;
        }
        ////Test_Results Based On Models...
        public DataTable Get_Test_Results_OnModel(string Model)
        {
            DataTable Test_Results_Model = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand Get_Results_on_ModelCMD = new SQLiteCommand(" SELECT * FROM Test_Results  Where Model_Name = '" + Model + "'", sqliteconn);
                SQLiteDataAdapter TestAdptr = new SQLiteDataAdapter(Get_Results_on_ModelCMD);
                TestAdptr.Fill(Test_Results_Model);
                int c = Test_Results_Model.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("Table Loaded Succesfully., Test_Results on Model.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnModel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }

            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnModel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnModel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnModel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnModel");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Test_Results_Model;
        }
        ////Get Test Results based on Location(factory and Country)
        public DataTable Get_Test_Results_OnLocation(string Factory, string Country)
        {
            DataTable Test_Results_Location = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand Get_Results_on_LocCMD = new SQLiteCommand(" SELECT * FROM Test_Results  Where Factory_Location= '" + Factory + "' AND Country= '" + Country + "'", sqliteconn);
                SQLiteDataAdapter TestAdptr = new SQLiteDataAdapter(Get_Results_on_LocCMD);
                TestAdptr.Fill(Test_Results_Location);
                int c = Test_Results_Location.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("Table Loaded Succesfully., Test_Results on Model.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnLocation");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }

            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnLocation");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnLocation");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnLocation");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnLocation");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Test_Results_Location;
        }
        ////Get Results Based on Start And End Date....(Start Date to End Date)
        public DataTable Get_Test_Results_OnStart_To_EndDate(string _Start_Date, string _End_Date)
        {
            DataTable Test_Results_Start_EndDate = new DataTable();
            try
            {
                string StartDate = _Start_Date + " 00:00:00.00" ;
                string EndDate = _End_Date + " 23:59:59.999";
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand Get_Results_on_DateCMD = new SQLiteCommand(" SELECT * FROM Test_Results  Where Test_Start_Date >= '" + StartDate + "' AND Test_Start_Date <= '" + EndDate + "'", sqliteconn);
                SQLiteDataAdapter TestAdptr = new SQLiteDataAdapter(Get_Results_on_DateCMD);
                TestAdptr.Fill(Test_Results_Start_EndDate);
                int c = Test_Results_Start_EndDate.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("Table Loaded Succesfully., Test_Results on Start & End Date.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnStart_To_EndDate");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }

            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnStart_To_EndDate");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnStart_To_EndDate");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnStart_To_EndDate");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_OnStart_To_EndDate");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Test_Results_Start_EndDate;
        }
        ////Get Test_Results On Month Based
        public DataTable Get_Test_Results_On_Month(string Tech, string Model, string Month, string year)
        {
            DataTable Test_Results_On_Month = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand Get_Results_on_MonthCMD = new SQLiteCommand(" SELECT * FROM Test_Results  Where Model_Name='" + Model + "' AND Technology_Name='" + Tech + "' AND SUBSTR(Test_Start_Date, 1, 2)= '" + Month + "' AND SUBSTR(Test_Start_Date, -4, 4)='" + year + "'", sqliteconn);
                SQLiteDataAdapter TestAdptr = new SQLiteDataAdapter(Get_Results_on_MonthCMD);
                TestAdptr.Fill(Test_Results_On_Month);
                int c = Test_Results_On_Month.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("Table Loaded Succesfully., Test_Results on Start & End Date.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Month");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }

            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Month");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Month");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Month");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Month");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Test_Results_On_Month;
        }
        ////Get Test Results on Year Based..
        public DataTable Get_Test_Results_On_Year(string year)
        {
            DataTable Test_Results_On_Year = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand Get_Results_on_YearCMD = new SQLiteCommand(" SELECT * FROM Test_Results  Where SUBSTR(Test_Start_Date, -4, 4)= '" + year + "'", sqliteconn);
                SQLiteDataAdapter TestAdptr = new SQLiteDataAdapter(Get_Results_on_YearCMD);
                TestAdptr.Fill(Test_Results_On_Year);
                int c = Test_Results_On_Year.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("Table Loaded Succesfully., Test_Results on Start & End Date.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Year");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Year");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Year");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Year");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Year");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Test_Results_On_Year;
        }
        ////Get Test Results Based on Quarters(For Every 3 Months..) Give here Quarter number(int) And Selecter Year As String.
        public DataTable Get_Test_Results_On_Quarters(int Quarter, string year)
        {
            DataTable Test_Result_Quarter = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                if (Quarter == 1)
                {
                    SQLiteCommand get_result_Quarter1 = new SQLiteCommand("SELECT * FROM Test_Results  Where SUBSTR(Test_Start_Date, -4, 4)= '" + year + "' AND (SUBSTR(Test_Start_Date, 1, 2)='1-' OR SUBSTR(Test_Start_Date, 1, 2)='2-' OR SUBSTR(Test_Start_Date, 1, 2)='3-') ", sqliteconn);
                    SQLiteDataAdapter quarterAdptr = new SQLiteDataAdapter(get_result_Quarter1);
                    quarterAdptr.Fill(Test_Result_Quarter);
                    int c = Test_Result_Quarter.Rows.Count;
                    if (c != 0)
                    {
                        Console.WriteLine("Records Loaded Succesfully., Test Results, Quarter 1.");
                    }
                    else
                    {
                        ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Quarters");
                        ServerErrorLog.GetERROR_SQL_Server(ERR1 + "For Quarter 1");
                    }
                }
                else if (Quarter == 2)
                {
                    SQLiteCommand get_result_Quarter2 = new SQLiteCommand("SELECT * FROM Test_Results  Where SUBSTR(Test_Start_Date, -4, 4)= '" + year + "' AND (SUBSTR(Test_Start_Date, 1, 2)='4-' OR SUBSTR(Test_Start_Date, 1, 2)='5-' OR SUBSTR(Test_Start_Date, 1, 2)='6-')", sqliteconn);
                    SQLiteDataAdapter quarterAdptr = new SQLiteDataAdapter(get_result_Quarter2);
                    quarterAdptr.Fill(Test_Result_Quarter);
                    int c = Test_Result_Quarter.Rows.Count;
                    if (c != 0)
                    {
                        Console.WriteLine("Records Loaded Succesfully., Test Results, Quarter 2.");
                    }
                    else
                    {
                        ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Quarters");
                        ServerErrorLog.GetERROR_SQL_Server(ERR1 + "For Quarter 2");
                    }
                }
                else if (Quarter == 3)
                {
                    SQLiteCommand get_result_Quarter3 = new SQLiteCommand("SELECT * FROM Test_Results  Where SUBSTR(Test_Start_Date, -4, 4)= '" + year + "' AND (SUBSTR(Test_Start_Date, 1, 2)='7-' OR SUBSTR(Test_Start_Date, 1, 2)='8-' OR SUBSTR(Test_Start_Date, 1, 2)='9-')", sqliteconn);
                    SQLiteDataAdapter quarterAdptr = new SQLiteDataAdapter(get_result_Quarter3);
                    quarterAdptr.Fill(Test_Result_Quarter);
                    int c = Test_Result_Quarter.Rows.Count;
                    if (c != 0)
                    {
                        Console.WriteLine("Records Loaded Succesfully., Test Results, Quarter 3.");
                    }
                    else
                    {
                        ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Quarters");
                        ServerErrorLog.GetERROR_SQL_Server(ERR1 + "For Quarter 3"); ;
                    }
                }
                else if (Quarter == 4)
                {
                    SQLiteCommand get_result_Quarter4 = new SQLiteCommand("SELECT * FROM Test_Results  Where SUBSTR(Test_Start_Date, -4, 4)= '" + year + "' AND (SUBSTR(Test_Start_Date, 1, 2)='10' OR SUBSTR(Test_Start_Date, 1, 2)='11' OR SUBSTR(Test_Start_Date, 1, 2)='12')", sqliteconn);
                    SQLiteDataAdapter quarterAdptr = new SQLiteDataAdapter(get_result_Quarter4);
                    quarterAdptr.Fill(Test_Result_Quarter);
                    int c = Test_Result_Quarter.Rows.Count;
                    if (c != 0)
                    {
                        Console.WriteLine("Records Loaded Succesfully., Test Results, Quarter 4.");
                    }
                    else
                    {
                        ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Quarters");
                        ServerErrorLog.GetERROR_SQL_Server(ERR1 + "For Quarter 4");
                    }
                }
                else
                {
                    Console.WriteLine("Please choose correct Quarter...");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Quarters");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Quarters");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Quarters");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Results_On_Quarters");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Test_Result_Quarter;
        }
        ////View Login Page Queries here...1.User Wise (have to pass the user name and start date..)
        public DataTable Get_Login_Session_Report_On_UserWise(string userName, string startDate)
        {
            DataTable Test_Results_On_UserLogin_Session = new DataTable();
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            sqliteconn.Open();
            try
            {
                SQLiteCommand Get_Login_SessionCMD = new SQLiteCommand("SELECT loginID,userName,role,cast(login_start as TEXT),cast(login_end as TEXT),cast(login_interval as TEXT) FROM Login_Details  Where userName= '" + userName + "' AND SUBSTR(login_start,1,10)='" + startDate + "'", sqliteconn);
                SQLiteDataAdapter TestAdptr = new SQLiteDataAdapter(Get_Login_SessionCMD);
                TestAdptr.Fill(Test_Results_On_UserLogin_Session);
                int c = Test_Results_On_UserLogin_Session.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("Table Loaded Succesfully., Test_Results on Start & End Date.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Login_Session_Report_On_UserWise");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Login_Session_Report_On_UserWise");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Login_Session_Report_On_UserWise");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Login_Session_Report_On_UserWise");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Login_Session_Report_On_UserWise");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Test_Results_On_UserLogin_Session;
        }
        ////Start Date and End Date Wise.(Have to pass the start date and End Date...)
        public DataTable Get_Login_Session_Report_On_DateWise(string StartDate, string EndDate)
        {
            DataTable Test_Results_On_UserLogin_Session = new DataTable();
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            sqliteconn.Open();
            try
            {
                SQLiteCommand Get_Login_SessionCMD = new SQLiteCommand("SELECT loginID,userName,role,cast(login_start as TEXT),cast(login_end as TEXT),cast(login_interval as TEXT) FROM Login_Details  Where SUBSTR(login_start,1,10) >= '" + StartDate + "' AND SUBSTR(login_start,1,10) <= '" + EndDate + "'", sqliteconn);
                SQLiteDataAdapter TestAdptr = new SQLiteDataAdapter(Get_Login_SessionCMD);
                TestAdptr.Fill(Test_Results_On_UserLogin_Session);
                int c = Test_Results_On_UserLogin_Session.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("Table Loaded Succesfully., Test_Results on Start & End Date.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Login_Session_Report_On_DateWise");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Login_Session_Report_On_DateWise");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Login_Session_Report_On_DateWise");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Login_Session_Report_On_DateWise");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Login_Session_Report_On_DateWise");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Test_Results_On_UserLogin_Session;
        }
        //Get Send Parameters Form Database According to Respective TestId
        public DataTable Get_Send_Params(int Test_Id)
        {
            DataTable Send_Params = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand Get_Send_ParamsCMD = new SQLiteCommand("SELECT Send_Parameter FROM Test_Item_Send_Parameters WHERE Test_Item_id = '" + Test_Id + "'", sqliteconn);
                SQLiteDataAdapter expectedAdepter = new SQLiteDataAdapter(Get_Send_ParamsCMD);
                expectedAdepter.Fill(Send_Params);
                int c = Send_Params.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("Table Loaded Succesfully., Expected Parameters.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_Params");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_Params");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_Params");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_Params");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_Params");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Send_Params;
        }
        //Get Expected parameters Form Database According to Respective TestId
        public DataTable Get_Expected_Params(int Test_Id)
        {
            DataTable Expected_Params = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand Get_Expected_ParamsCMD = new SQLiteCommand("SELECT Expected_Parameter FROM Test_Item_Expected_Parameters WHERE Test_Item_Id = '" + Test_Id + "'", sqliteconn);
                SQLiteDataAdapter expectedAdepter = new SQLiteDataAdapter(Get_Expected_ParamsCMD);
                expectedAdepter.Fill(Expected_Params);
                int c = Expected_Params.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("Table Loaded Succesfully., Expected Parameters.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Expected_Params");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Expected_Params");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Expected_Params");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Expected_Params");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Expected_Params");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Expected_Params;
        }
        //Getting Work Location Table
        public DataTable Get_Work_Location()
        {
            DataTable Loc_table = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getLocation = new SQLiteCommand("SELECT FactoryName, CountryName, WorkLoc_ID FROM Work_Location", sqliteconn);
                SQLiteDataAdapter locAdptr = new SQLiteDataAdapter(getLocation);
                locAdptr.Fill(Loc_table);
                int y = Loc_table.Rows.Count;
                if (y != 0)
                {
                    //Do Nothing
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Work_Location");
                    ServerErrorLog.GetERROR_SQL_Server("Can Not Load table. Check Database for records.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Work_Location");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Work_Location");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Work_Location");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Work_Location");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Loc_table;
        }
        //Getting Test Item Table Here...
        public DataTable Get_Test_Items_Table()
        {
            DataTable test_Item = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getTestCMD = new SQLiteCommand("SELECT Test_Item_Id, Test_Item_Name FROM Test_Items", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(getTestCMD);
                testAdptr.Fill(test_Item);
                int y = test_Item.Rows.Count;
                if (y != 0)
                {
                    //DO Nothing...
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server("Check DataBase");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return test_Item;
        }
        //Mapp Station Test Items..HERE..
        public bool Mapp_Station_TestItems(int Station_id, int Test_Id, int Model_id)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand chkmapped = new SQLiteCommand("SELECT Test_Item_Id, Station_Id, Model_Id FROM Station_TestItem WHERE Station_Id = '" + Station_id + "' AND Test_Item_Id = '" + Test_Id + "' AND Model_Id = '" + Model_id + "'", sqliteconn);
                SQLiteDataReader tdr = chkmapped.ExecuteReader();
                if (tdr.Read())
                {
                    MessageBox.Show("Selected Test Item is Already Mapped. Can not mapp again with same Station.");
                }
                else
                {
                    SQLiteCommand mappTest = new SQLiteCommand("INSERT INTO Station_TestItem (Station_Id, Test_Item_Id, Model_Id, Test_Item_Order) VALUES('" + Station_id + "', '" + Test_Id + "', '" + Model_id + "', '" + 0 + "')", sqliteconn);
                    int y = mappTest.ExecuteNonQuery();
                    if (y != 0)
                    {
                        ok = true;
                        //MessageBox.Show("Test Items Mapped Succesfully.");
                    }
                    else
                    {
                        ServerErrorLog.ERROR_Function_Name("Mapp_Station_TestItems");
                        ServerErrorLog.GetERROR_SQL_Server("Test Items Not mapped Succesfully.");
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Mapp_Station_TestItems");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Mapp_Station_TestItems");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Mapp_Station_TestItems");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Mapp_Station_TestItems");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Un Mapping Test Items Here....
        public bool Un_Mapp_Station_TestItem(int Station_id, int Test_Id, int Model_id)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand UnMappTest = new SQLiteCommand("DELETE  FROM Station_TestItem WHERE Model_Id = '" + Model_id + "' AND Station_Id = '" + Station_id + "' AND Test_Item_Id = '" + Test_Id + "'", sqliteconn);
                int y = UnMappTest.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    //DO Nothing..
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Mapp_Station_TestItem");
                    ServerErrorLog.GetERROR_SQL_Server("No Record Found or Check Database.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Mapp_Station_TestItem");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Mapp_Station_TestItem");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Mapp_Station_TestItem");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Un_Mapp_Station_TestItem");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Get Mapped TestItems Here...
        public DataTable Get_Mapped_Test_Items(int Station_id, int Model_id)
        {
            DataTable MappedTestItems = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand chkmapped = new SQLiteCommand("SELECT T.Test_Item_Id, Test_Item_Name FROM Test_Items AS T JOIN Station_TestItem AS S ON S.Test_Item_Id = T.Test_Item_Id WHERE S.Model_Id = '" + Model_id + "' AND S.Station_Id ='" + Station_id + "' AND  S.Test_Item_Order >= '" + 0 + "'", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(chkmapped);
                testAdptr.Fill(MappedTestItems);
                int y = MappedTestItems.Rows.Count;
                if (y != 0)
                {
                    //do NOthing...
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server("No Records.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return MappedTestItems;
        }
        //Get New Max Test Item Id Here
        public int Get_Test_Max_Id()
        {
            int Test_Id = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getTestId = new SQLiteCommand("SELECT MAX (Test_Item_Id) FROM Test_Items ", sqliteconn);
                SQLiteDataReader tdr = getTestId.ExecuteReader();
                if (tdr.Read())
                {
                    Test_Id = tdr.GetInt32(0);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Max_Id");
                    ServerErrorLog.GetERROR_SQL_Server("Could Not Found record. or Check Table.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Max_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Max_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Max_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Max_Id");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Test_Id;
        }
        //Get model and Technology Name Only to generate the Image and Sample File Folders..
        public string[] Get_Tech_Model_Name(int Model_Id)
        {
            string[] Model_Tech = new string[2];
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getModelTech = new SQLiteCommand("SELECT Model_name,P.Product_Type FROM Model AS M LEFT OUTER JOIN Product AS P ON  M.Model_Id = '" + Model_Id + "'  Where P.Product_Id= M.Product_Id", sqliteconn);
                SQLiteDataReader Mrdr = getModelTech.ExecuteReader();
                if (Mrdr.Read())
                {
                    Model_Tech[0] = Mrdr[0].ToString();
                    Model_Tech[1] = Mrdr[1].ToString();
                    Mrdr.Close(); // added by Sachini 8_11_2016 , without this sqlite is going to locked state
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Tech_Model_Name");
                    ServerErrorLog.GetERROR_SQL_Server("Could Not read record.. Check Database For error Details.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Tech_Model_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Tech_Model_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Tech_Model_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Tech_Model_Name");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Model_Tech;
        }
        //Get SendText Option...(Test Id)
        public string Get_WriteSNtoFW_Opt(int test_Id)
        {
            string SendText = null;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getSendText = new SQLiteCommand("SELECT SendTextToFW FROM Test_Item_Options WHERE Test_Item_Id = '" + test_Id + "'", sqliteconn);
                SQLiteDataReader pdr = getSendText.ExecuteReader();
                if (pdr.Read())
                {
                    SendText = pdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SendTextOpt");
                    ServerErrorLog.GetERROR_SQL_Server("Check DB");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SendTextOpt");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SendTextOpt");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SendTextOpt");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SendTextOpt");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return SendText;
        }
        //Get External Tool Option Here..
        public string Get_External_ToolOption(int testId)
        {
            string ExternalTool = "False";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getExternal = new SQLiteCommand("SELECT External_Tool FROM Test_Item_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader pdr = getExternal.ExecuteReader();
                if (pdr.Read())
                {
                    ExternalTool = pdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_External_ToolOption");
                    ServerErrorLog.GetERROR_SQL_Server("Check DB");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_External_ToolOption");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_External_ToolOption");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_External_ToolOption");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_External_ToolOption");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ExternalTool;
        }
        //Getting Test Results Based on Test Items..
        public DataTable Get_Test_Result_OnTest_Item(string TechName, string Model, string station, string Test_Item)
        {
            DataTable TestItemResult = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand GetTestItemWise = new SQLiteCommand("SELECT * FROM Test_Results  Where  Technology_Name='" + TechName + "'AND Model_Name='" + Model + "' AND Test_Station='" + station + "'  AND TestItemNameValue='" + Test_Item + "'", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(GetTestItemWise);
                testAdptr.Fill(TestItemResult);
                int y = TestItemResult.Rows.Count;
                if (y != 0)
                {
                    //DO NOTHING HERE> OK.      
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_OnTest_Item");
                    ServerErrorLog.GetERROR_SQL_Server("Could not load table. Check for Records, or Data Table.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_OnTest_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_OnTest_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_OnTest_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_OnTest_Item");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return TestItemResult;
        }
        //Updating Test Items (Test Items Table)
        public bool Update_Test_Item(int Order, int Test_Id, int Stn_Id,int ModelID1)
        //public bool Update_Test_Item(int Test_Id, int Order, String Test_Name, int Stn_Id, int cmdId)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand UpdateTestCMD = new SQLiteCommand("UPDATE Station_TestItem SET Test_Item_Order='" + Order + "' WHERE Test_Item_Id='" + Test_Id + "' AND Station_Id='" + Stn_Id + "' AND Model_Id='" + ModelID1 + "'", sqliteconn);
                int y = UpdateTestCMD.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    //MessageBox.Show("Record Updated Succesfully.");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Update_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server("Record Not Updated, Check Database.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Test_Item");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Get Send Params For Test Item..
        public List<string> Get_Send_Paramters(int TestItemID)
        {
            List<string> ParamsList = new List<string>();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT Send_Parameter FROM Test_Item_Send_Parameters WHERE Test_Item_id = '" + TestItemID + "' ", sqliteconn);
                SQLiteDataAdapter adptr = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                adptr.Fill(dt);
                SQLiteDataReader dr = cmd.ExecuteReader();
                int cnt = dt.Rows.Count;
                for (int i = 0; i <= dt.Rows.Count; i++)
                {
                    if (dr.Read())
                    {
                        ParamsList.Add((string)dr["Send_Parameter"]);
                    }
                }
                dr.Dispose();
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_Paramters");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_Paramters");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_Paramters");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_Paramters");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ParamsList;
        }
        //get Expected params For Test Items..
        public List<string> Get_ExpectedParams(int TestId)
        {
            List<string> ParamsListEx = new List<string>();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT Expected_Parameter FROM Test_Item_Expected_Parameters WHERE Test_Item_Id = '" + TestId + "' ", sqliteconn);
                SQLiteDataAdapter adptr = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                adptr.Fill(dt);
                SQLiteDataReader dr = cmd.ExecuteReader();
                int cnt = dt.Rows.Count;
                for (int i = 0; i <= dt.Rows.Count; i++)
                {
                    if (dr.Read())
                    {
                        ParamsListEx.Add((string)dr["Expected_Parameter"]);
                    }
                }
                dr.Dispose();
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ExpectedParams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ExpectedParams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ExpectedParams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ExpectedParams");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ParamsListEx;
        }
        //Upadating Commands..
        public bool Update_Cmd(string cmdName, int cmdId)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand updatecmd = new SQLiteCommand("UPDATE Commands SET Command_Name = '" + cmdName + "' WHERE Command_Id = '" + cmdId + "'", sqliteconn);
                int y = updatecmd.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Update_Cmd");
                    ServerErrorLog.GetERROR_SQL_Server("Unkown Error Chaeck Databese or Record Not found.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Cmd");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Cmd");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Cmd");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Cmd");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Checking Reg. Number Here..
        public string Check_RangeValue(int TestId)
        {
            string Range = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand ckRangeText = new SQLiteCommand("SELECT RangeOfValues FROM Test_Item_Options WHERE Test_Item_Id = '" + TestId + "'", sqliteconn);
                SQLiteDataReader RangeTextReader = ckRangeText.ExecuteReader();
                if (RangeTextReader.Read())
                {
                    Range = RangeTextReader["RangeOfValues"].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Check_RangeValue");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_RangeValue");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_RangeValue");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_RangeValue");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Check_RangeValue");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return Range;
        }
        //Checking Regulation Lable Here..
        public string Chk_RegLabel(int TestId)
        {
            string RegLable = "False";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand ckRangeText = new SQLiteCommand("SELECT CheckRegLabel FROM Test_Item_Options WHERE Test_Item_Id = '" + TestId + "'", sqliteconn);
                SQLiteDataReader RangeTextReader = ckRangeText.ExecuteReader();
                if (RangeTextReader.Read())
                {
                    RegLable = RangeTextReader["CheckRegLabel"].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_RegLabel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_RegLabel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_RegLabel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_RegLabel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_RegLabel");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return RegLable;
        }
        //Checking SN send from FW option is True or not.
        public string Get_CheckSerialNumber_Opt(int TestId)
        {
            string textopt = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand ckRangeText = new SQLiteCommand("SELECT CheckTextFromFW FROM Test_Item_Options WHERE Test_Item_Id = '" + TestId + "'", sqliteconn);
                SQLiteDataReader RangeTextReader = ckRangeText.ExecuteReader();
                if (RangeTextReader.Read())
                {
                    textopt = RangeTextReader["CheckTextFromFW"].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_TextFromFW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_TextFromFW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_TextFromFW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_TextFromFW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_TextFromFW");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return textopt;
        }
        //Getting Distinct Technology Name from test Results Table.
        public DataTable Get_TechFrom_Result()
        {
            DataTable techTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getTestTechCMD = new SQLiteCommand("SELECT distinct Technology_Name FROM Test_Results", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(getTestTechCMD);
                testAdptr.Fill(techTable);
                int y = techTable.Rows.Count;
                if (y != 0)
                {
                    //DO Nothing...
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_TechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Check DataBase");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_TechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_TechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_TechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_TechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return techTable;
        }
        //Get Distinct Model Name from test Results Table.
        public DataTable Get_ModelFrom_Result()
        {
            DataTable ModelTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getTestModelCMD = new SQLiteCommand("SELECT distinct Model_Name FROM Test_Results", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(getTestModelCMD);
                testAdptr.Fill(ModelTable);
                int y = ModelTable.Rows.Count;
                if (y != 0)
                {
                    //DO Nothing...
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ModelFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Check DataBase");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ModelFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ModelFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ModelFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ModelFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ModelTable;
        }
        //Get Distinct Factory Name from test Results Table.
        public DataTable Get_FactoryFrom_Result(string country)
        {
            DataTable FactoryTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getTestFacCMD = new SQLiteCommand("SELECT distinct Factory_Location FROM Test_Results WHERE  Country='"  + country + "'", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(getTestFacCMD);
                testAdptr.Fill(FactoryTable);
                int y = FactoryTable.Rows.Count;
                if (y != 0)
                {
                    //DO Nothing...
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_FactoryFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Check DataBase");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_FactoryFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_FactoryFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_FactoryFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_FactoryFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return FactoryTable;
        }
        //Get Distinct Country Name from test Results Table.
        public DataTable Get_CountryFrom_Result()
        {
            DataTable CountryTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getTestCntryCMD = new SQLiteCommand("SELECT distinct Country FROM Test_Results", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(getTestCntryCMD);
                testAdptr.Fill(CountryTable);
                int y = CountryTable.Rows.Count;
                if (y != 0)
                {
                    //DO Nothing...
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_CountryFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Check DataBase");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_CountryFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_CountryFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_CountryFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_CountryFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return CountryTable;
        }
        //Get Distinct Year wise from test Results Table.
        public DataTable Get_YearFrom_Result()
        {
            DataTable yearTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getTestCntryCMD = new SQLiteCommand("SELECT distinct SUBSTR(Test_Start_Date,-4 , 4)  FROM Test_Results", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(getTestCntryCMD);
                testAdptr.Fill(yearTable);
                int y = yearTable.Rows.Count;
                if (y != 0)
                {
                    //DO Nothing...
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_YearFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Check DataBase");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_YearFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_YearFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_YearFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_YearFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return yearTable;
        }
        //Get Distinct TestItem Name wise from test Results Table.
        public DataTable Get_TestNameFrom_Result(string model, string station)
        {
            DataTable TestNameTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getTestCntryCMD = new SQLiteCommand("SELECT  distinct TestItemNameValue FROM Test_Results  Where  Model_Name='" + model + "' AND Test_Station ='" + station + "'", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(getTestCntryCMD);
                testAdptr.Fill(TestNameTable);
                int y = TestNameTable.Rows.Count;
                if (y != 0)
                {
                    //DO Nothing...
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_TestNameFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Check DataBase");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_TestNameFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_TestNameFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_TestNameFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_TestNameFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return TestNameTable;
        }




        //Get Distinct Stations name on  Model Name and  Tech from test Results Table.
        public DataTable Get_station_on_ModelTechFrom_Result(string tech, string model)
        {
            DataTable TestStationTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                //"SELECT  distinct Test_Station FROM Test_Results  Where Technology_Name ='" + tech + "' AND Model_Name ='" + model + "'"
                SQLiteCommand getTestCntryCMD = new SQLiteCommand("SELECT  distinct Test_Station FROM Test_Results  Where Technology_Name ='" + tech + "' AND Model_Name ='" + model + "'", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(getTestCntryCMD);
                testAdptr.Fill(TestStationTable);
                int y = TestStationTable.Rows.Count;
                if (y != 0)
                {
                    //DO Nothing...
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_station_on_ModelTechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Check DataBase");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_station_on_ModelTechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_station_on_ModelTechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_station_on_ModelTechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_station_on_ModelTechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return TestStationTable;
        }

        //Get Distinct Model Name base on Tech from test Results Table.
        public DataTable Get_Model_on_TechFrom_Result(string tech)
        {
            DataTable modelTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getTestCntryCMD = new SQLiteCommand("SELECT  distinct Model_Name FROM Test_Results  Where Technology_Name ='" + tech + "'", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(getTestCntryCMD);
                testAdptr.Fill(modelTable);
                int y = modelTable.Rows.Count;
                if (y != 0)
                {
                    //DO Nothing...
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Model_on_TechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Check DataBase");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Model_on_TechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Model_on_TechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Model_on_TechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Model_on_TechFrom_Result");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return modelTable;
        }
        //Getting test Item id and associated command id.
        public string[] testName_CmdId(int test_id)
        {
            string[] test_Na_cmd = new string[2];
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdTestName = new SQLiteCommand("SELECT Test_Item_Name, Command_Id FROM Test_Items WHERE Test_Item_Id = '" + test_id + "'", sqliteconn);
                SQLiteDataReader tdr = cmdTestName.ExecuteReader();
                while (tdr.Read())
                {
                    test_Na_cmd[0] = tdr[0].ToString();
                    test_Na_cmd[1] = tdr[1].ToString();
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("testName_CmdId");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("testName_CmdId");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("testName_CmdId");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("testName_CmdId");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return test_Na_cmd;
        }
        //Getting all commands here for modify test items.
        public DataTable get_Cmdtable(int tech_id, int model_id)
        {
            DataTable cmdTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdchkcmd = new SQLiteCommand("SELECT S.Command_Name, S.Command_Id AS V FROM Commands AS S LEFT OUTER JOIN Product__Model_Command AS C ON S.Command_Id = C.Command_Id WHERE C.Product_Id='" + tech_id + "' AND C.Model_Id = '" + model_id + "'", sqliteconn);
                SQLiteDataAdapter adptr = new SQLiteDataAdapter(cmdchkcmd);
                adptr.Fill(cmdTable);
                int c = cmdTable.Rows.Count;
                if (c != 0)
                {
                    //OK No need to action.
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Cmdtable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Cmdtable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Cmdtable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Cmdtable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Cmdtable");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return cmdTable;
        }
        //getting Send Params for modify Test Items.
        public DataTable Get_Send_ParamtersList(int TestItemID)
        {
            DataTable dt = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT Send_Parameter FROM Test_Item_Send_Parameters WHERE Test_Item_id = '" + TestItemID + "' ", sqliteconn);
                SQLiteDataAdapter adptr = new SQLiteDataAdapter(cmd);
                adptr.Fill(dt);
                int cnt = dt.Rows.Count;
                if (cnt != 0)
                {
                    //Okay.
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_ParamtersList");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_ParamtersList");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_ParamtersList");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_ParamtersList");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Send_ParamtersList");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return dt;
        }
        //Getting expected paramslist for modify test item.
        public DataTable Get_ExpectedParamsList(int TestId)
        {
            DataTable expec_table = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT Expected_Parameter FROM Test_Item_Expected_Parameters WHERE Test_Item_Id = '" + TestId + "' ", sqliteconn);
                SQLiteDataAdapter adptr = new SQLiteDataAdapter(cmd);
                adptr.Fill(expec_table);
                int cnt = expec_table.Rows.Count;
                if (cnt != 0)
                {
                    //Okay
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ExpectedParamsList");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {

                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ExpectedParamsList");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ExpectedParamsList");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ExpectedParamsList");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ExpectedParamsList");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return expec_table;
        }
        //uPDATE tEST item Details
        public bool Test_details_Update(string test_name, int cmd_Id, int Test_Id)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdtestupdate = new SQLiteCommand("UPDATE Test_Items SET Test_Item_Name = '" + test_name + "', Command_Id= '" + cmd_Id + "' WHERE Test_Item_Id ='" + Test_Id + "'", sqliteconn);
                int z = cmdtestupdate.ExecuteNonQuery();
                if (z != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Test_details_Update");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Test_details_Update");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Test_details_Update");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Test_details_Update");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Test_details_Update");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Insert into Send parameters for modify Test Item.
        public bool insert_Test_Sendparams(int testId, string param, int Model)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdSend = new SQLiteCommand("INSERT INTO Test_Item_Send_Parameters (Test_Item_Id, Send_Parameter, Model_Id) VALUES ('" + testId + "', '" + param + "', '" + Model + "')", sqliteconn);
                int yz = cmdSend.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("insert_Test_Sendparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_Test_Sendparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_Test_Sendparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_Test_Sendparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("insert_Test_Sendparams");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Insert into expected params for modify test item.
        public bool insert_Test_Expectedparams(int testId, string param, int Model)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdexpect = new SQLiteCommand("INSERT INTO Test_Item_Expected_Parameters (Test_Item_Id, Expected_Parameter, Model_Id) VALUES ('" + testId + "', '" + param + "', '" + Model + "')", sqliteconn);
                int yz = cmdexpect.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("insert_Test_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_Test_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_Test_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_Test_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("insert_Test_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Getting Stopwatch Option hereh for 3D Pen...
        public string Get_StopWatch_Option(int testId)
        {
            string StopWatch = "False";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getExternal = new SQLiteCommand("SELECT StopWatch FROM Test_Item_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader pdr = getExternal.ExecuteReader();
                if (pdr.Read())
                {
                    StopWatch = pdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_StopWatch_Option");
                    ServerErrorLog.GetERROR_SQL_Server("Check DB");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_StopWatch_Option");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_StopWatch_Option");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_StopWatch_Option");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_StopWatch_Option");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return StopWatch;
        }
        //Update Image For Modify Test Item Here...
        public bool UpdateImage(int Test_Id, string Image_Name)
        {
            bool ok = false;
            try
            {

                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getImage = new SQLiteCommand("SELECT Image_Name FROM Test_Items_Image WHERE Test_Item_Id = '" + Test_Id + "'", sqliteconn);
                SQLiteDataReader pdr = getImage.ExecuteReader();
                if (pdr.Read())
                {
                    SQLiteCommand updateImage = new SQLiteCommand("UPDATE Test_Items_Image SET Image_Name = '" + Image_Name + "'  WHERE Test_Item_Id = '" + Test_Id + "'", sqliteconn);
                    int za = updateImage.ExecuteNonQuery();
                    if (za != 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        ok = false;
                        ServerErrorLog.ERROR_Function_Name("UpdateImage");
                        ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    }
                }
                else
                {
                    ok = true;
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("UpdateImage");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("UpdateImage");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("UpdateImage");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("UpdateImage");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Get Image Path.
        public string Get_Image_Path(int Test_Id)
        {
            string path = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getImagepath = new SQLiteCommand("SELECT Image_Path FROM Test_Items_Image WHERE Test_Item_Id = '" + Test_Id + "'", sqliteconn);
                SQLiteDataReader pdr = getImagepath.ExecuteReader();
                if (pdr.Read())
                {
                    path = pdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Image_Path");
                    ServerErrorLog.GetERROR_SQL_Server("No Record found.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Image_Path");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Image_Path");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Image_Path");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Image_Path");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return path;
        }
        //Getting buttons for Start Test Form.
        public DataTable Get_ButtonsList(int ModelId, int statn)
        {
            DataTable expec_table = new DataTable();

            try
            {
                //SQLiteDataReader DataRead = null;
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                string comm = "SELECT T.Test_Item_Name, S.Test_Item_Order  FROM Test_Items AS T  JOIN Station_TestItem AS S ON S.Test_Item_Id = T.Test_Item_Id WHERE S.Model_Id = '" + ModelId + "' AND S.Station_Id ='" + statn + "' AND S.Test_Item_Order >='" + 1 + "' ORDER BY  S.Test_Item_Order";
                SQLiteCommand commnd = new SQLiteCommand(comm, sqliteconn);
                //DataRead = commnd.ExecuteReader();
                SQLiteDataAdapter adptr = new SQLiteDataAdapter(commnd);
                adptr.Fill(expec_table);

                //bool success = DataRead.HasRows;
                if (expec_table.Rows.Count > 0)
                {
                    //Okay
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ButtonsList, no data to get");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {

                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ButtonsList");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ButtonsList");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ButtonsList");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ButtonsList");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return expec_table;
        }
        //Getting Commands only according to Technology..
        public DataTable Load_Cmd_Tech(int Tech_id)
        {
            DataTable CmmndTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand LoadCommandCMD = new SQLiteCommand("SELECT Command_Name FROM Commands AS S LEFT OUTER JOIN Product__Model_Command AS C ON S.Command_Id = C.Command_Id WHERE C.Product_Id ='" + Tech_id + "'", sqliteconn);
                SQLiteDataAdapter CMDAdptre = new SQLiteDataAdapter(LoadCommandCMD);
                CMDAdptre.Fill(CmmndTable);
                int c = CmmndTable.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("table Loaded Succesfully. Product");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Cmd");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Cmd_Tech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Cmd_Tech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Cmd_Tech");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Load_Cmd_Tech");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return CmmndTable;
        }
        //Milinda get Continue
        public bool get_Continue_Flag(int testId)
        {
            bool Continue = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getContinue = new SQLiteCommand("SELECT Continue FROM Test_Item_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader ContinueReader = getContinue.ExecuteReader();
                if (ContinueReader.Read())
                {
                    Continue = Convert.ToBoolean(ContinueReader[0]);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Continue_Flag");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Continue_Flag");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Continue_Flag");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Continue_Flag");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Continue_Flag");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Continue;
        }
        //Getting the Option Used to wrire the REG label to FW from Test Item Option Table
        public string Get_SendRegLabeltoFW_Opt(int TestId)
        {
            string RegLable = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand ckRangeText = new SQLiteCommand("SELECT Send_Scan_regLable FROM Test_Item_Options WHERE Test_Item_Id = '" + TestId + "'", sqliteconn);
                SQLiteDataReader RangeTextReader = ckRangeText.ExecuteReader();
                if (RangeTextReader.Read())
                {
                    RegLable = RangeTextReader["Send_Scan_regLable"].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_ScanRegLabel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_ScanRegLabel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_ScanRegLabel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_ScanRegLabel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_ScanRegLabel");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return RegLable;
        }

        public int Get_test_Item_Id(string test_Name, int model, int statn)
        {
            int testId = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand testitemCmd = new SQLiteCommand("SELECT T.Test_Item_Id FROM Test_Items AS T  JOIN Station_TestItem AS S ON S.Test_Item_Id = T.Test_Item_Id WHERE S.Model_Id = '" + model + "' AND S.Station_Id ='" + statn + "' AND T.Test_Item_Name='" + test_Name + "'AND  S.Test_Item_Order > '" + 0 + "'", sqliteconn);
                SQLiteDataReader tdr = testitemCmd.ExecuteReader();
                if (tdr.Read())
                {
                    testId = Convert.ToInt32(tdr[0]);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return testId;
        }
        //Storing Login Details here....
        public bool Store_login_Session(string userName, string sRole, string login_start)
        {
            bool ok = false;
            try
            {
                //DateTime start_time = Convert.ToDateTime(login_start);
                //String Login = start_time.ToString("MM/dd/yyyy HH:mm:ss");
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand insert_loginDetails = new SQLiteCommand("INSERT INTO Login_Details (userName, role,login_start) VALUES ('" + userName + "','" + sRole + "','" + login_start + "')", sqliteconn);
                int yz = insert_loginDetails.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Store_login_Session");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Store_login_Session");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Store_login_Session");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Store_login_Session");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Store_login_Session");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Updating logout Time and Interval Here...
        public bool Update_login_Session(string login_end)
        {
            bool ok = false;
            string login = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand get_loginTime = new SQLiteCommand("SELECT login_start FROM Login_Details WHERE loginID = (SELECT MAX(loginID) FROM Login_Details) ", sqliteconn);
                SQLiteDataReader sdr = get_loginTime.ExecuteReader();
                if (sdr.Read())
                {
                    login = sdr[0].ToString();
                    SQLiteCommand Update_loginDetails = new SQLiteCommand("UPDATE Login_Details SET login_end ='" + login_end + "' WHERE loginID= (SELECT MAX(loginID) FROM Login_Details) ", sqliteconn);
                    int y = Update_loginDetails.ExecuteNonQuery();
                    if (y != 0)
                    {
                        TimeSpan ts = Convert.ToDateTime(login_end).Subtract(Convert.ToDateTime(login));
                        SQLiteCommand Update_loginInterval = new SQLiteCommand("UPDATE Login_Details SET login_interval ='" + ts + "' WHERE loginID= (SELECT MAX(loginID) FROM Login_Details) ", sqliteconn);
                        int yzz = Update_loginInterval.ExecuteNonQuery();
                        if (yzz != 0)
                        {
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                            ServerErrorLog.ERROR_Function_Name("Update_login_Session");
                            ServerErrorLog.GetERROR_SQL_Server("Can not Update Table(login_Details)-Column: Login Interval");
                        }
                    }
                    else
                    {
                        ServerErrorLog.ERROR_Function_Name("Update_login_Session");
                        ServerErrorLog.GetERROR_SQL_Server("Can not read from Table(login_Details)");
                    }
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Update_login_Session");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_login_Session");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_login_Session");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_login_Session");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Update_login_Session");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Get Sample File Path Only
        public string get_Sample_File_Path(int Test_Id)
        {
            string sampleFileTable = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand sampleFileCmd = new SQLiteCommand("SELECT Sample_File_Path FROM Sample_Files  Where Test_Item_Id= '" + Test_Id + "' ", sqliteconn);
                SQLiteDataReader tdr = sampleFileCmd.ExecuteReader();
                if (tdr.Read())
                {
                    sampleFileTable = tdr[0].ToString();
                    Console.WriteLine("SUccess");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR. " + xc.Message);
                }
            }
            return sampleFileTable;
        }
        //Getting Image Name Here...
        public string Get_Image_Name(int Test_Id)
        {
            string name = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getImagepath = new SQLiteCommand("SELECT Image_Name FROM Test_Items_Image WHERE Test_Item_Id = '" + Test_Id + "'", sqliteconn);
                SQLiteDataReader pdr = getImagepath.ExecuteReader();
                if (pdr.Read())
                {
                    name = pdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Image_Name");
                    ServerErrorLog.GetERROR_SQL_Server("No Record found.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Image_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Image_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Image_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Image_Name");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return name;
        }

        public bool Insert_LogIn(string userName, string sRole, string Start_time)
        {
            bool saved = false;
            int y = 0;

            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdserverResultSave = new SQLiteCommand("INSERT INTO Login_Details (userName, role,login_start) VALUES ('" + userName + "','" + sRole + "','" + Start_time + "')", sqliteconn);
                y = cmdserverResultSave.ExecuteNonQuery();
                if (y != 0)
                {
                    saved = true;
                    Console.WriteLine("LogIn Successfully saved on SQL Server");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_LogIn");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
                //    mytransaction.Commit();
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_LogIn");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_LogIn");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_LogIn");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_LogIn");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return saved;
        }

        public bool Insert_LogOut(DateTime login_End, TimeSpan total, long countRow)
        {
            bool saved = false;
            int y = 0;

            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdserverResultSave = new SQLiteCommand("UPDATE Login_Details SET login_end = '" + login_End + "', login_interval = '" + total + "' WHERE loginID='" + countRow + "'", sqliteconn);
                y = cmdserverResultSave.ExecuteNonQuery();
                if (y != 0)
                {
                    saved = true;
                    Console.WriteLine("LogOut Successfully saved on SQL Server");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_LogOut");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
                //    mytransaction.Commit();
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_LogOut");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_LogOut");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_LogOut");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_LogOut");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return saved;
        }

        public long get_LogInRows()
        {
            long Rows = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getRows = new SQLiteCommand("SELECT COUNT(*) FROM Login_Details;", sqliteconn);
                SQLiteDataReader RowReader = getRows.ExecuteReader();
                if (RowReader.Read())
                {
                    Rows = (long)RowReader[0];
                    RowReader.Close(); //22 Aug 2016
                   
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_LogInRows");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_LogInRows");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_LogInRows");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_LogInRows");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_LogInRows");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Rows;
        }

        public bool ComputationOptionsInsert(
                                            int test_Id,

                                            string ConditionalComputationOpt,
                                            string UnconditionalComputationOpt,

                                            string checkedAVG,
                                            string AVG_Params,
                                            string AVG_Operator,
                                            string AVG_Condition,

                                            string checkedSUM,
                                            string SUM_Params,
                                            string SUM_Operator,
                                            string SUM_Condition,

                                            string checkedPercentage,
                                            string PER_Params,
                                            string PER_Operator,
                                            string PER_Condition,

                                            string checkedSTDEV,
                                            string STDEV_Params,
                                            string STDEV_Operator,
                                            string STDEV_Condition,

                                            string checkedMAX,
                                            string MAX_Params,
                                            string MAX_Operator,
                                            string MAX_Condition,

                                            string checkedMIN,
                                            string MIN_Params,
                                            string MIN_Operator,
                                            string MIN_Condition,


                                            string checkedMAXsMIN,
                                            string MAXsMIN_Params,
                                            string MAXsMIN_Operator,
                                            string MAXsMIN_Condition                                       
                                            )
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                using (sqliteconn)
                {
                    sqliteconn.Open();
                    using (SQLiteTransaction mytransaction = sqliteconn.BeginTransaction())
                    {
                        using (SQLiteCommand mycommand = new SQLiteCommand(sqliteconn))
                        {
                            mycommand.CommandText = "INSERT INTO Computation_Options VALUES('"
                                                      + test_Id + "', '"
                                                      + ConditionalComputationOpt + "', '"
                                                      + UnconditionalComputationOpt + "', '"
                                                      + checkedAVG + "','"
                                                      + AVG_Params + "','"
                                                      + AVG_Operator + "','"
                                                      + AVG_Condition + "','"
                                                      + checkedSUM + "','"
                                                      + SUM_Params + "','"
                                                      + SUM_Operator + "','"
                                                      + SUM_Condition + "','"
                                                      + checkedPercentage + "', '"
                                                      + PER_Params + "', '"
                                                      + PER_Operator + "', '"
                                                      + PER_Condition + "', '"
                                                      + checkedSTDEV + "', '"
                                                      + STDEV_Params + "', '"
                                                      + STDEV_Operator + "', '"
                                                      + STDEV_Condition + "', '"
                                                      + checkedMAX + "','"
                                                      + MAX_Params + "','"
                                                      + MAX_Operator + "','"
                                                      + MAX_Condition + "','"
                                                      + checkedMIN + "','"
                                                      + MIN_Params + "','"
                                                      + MIN_Operator + "','"
                                                      + MIN_Condition + "','"
                                                      + checkedMAXsMIN + "','"
                                                      + MAXsMIN_Params + "','"
                                                      + MAXsMIN_Operator + "','"
                                                      + MAXsMIN_Condition + "')";
                            int y = mycommand.ExecuteNonQuery();
                            if (y != 0)
                            {
                                ok = true;
                                Console.WriteLine("Details Saved Successfully, in ComputationOptionsInsert.");
                            }
                            else
                            {
                                ok = false;
                                ServerErrorLog.ERROR_Function_Name("ComputationOptionsInsert");
                                ServerErrorLog.GetERROR_SQL_Server(ERR8);
                            }
                            mytransaction.Commit();
                        }
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("ComputationOptionsInsert");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("ComputationOptionsInsert");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("ComputationOptionsInsert");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("ComputationOptionsInsert");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }

        public DataTable Get_Computation_Options(int test_ID)
        {
            DataTable expec_table = new DataTable();

            try
            {
                //SQLiteDataReader DataRead = null;
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                string comm = "SELECT * FROM Computation_Options WHERE Test_Item_ID = '" + test_ID + "'";
                SQLiteCommand commnd = new SQLiteCommand(comm, sqliteconn);
                //DataRead = commnd.ExecuteReader();
                SQLiteDataAdapter adptr = new SQLiteDataAdapter(commnd);
                adptr.Fill(expec_table);

                //bool success = DataRead.HasRows;
                if (expec_table.Rows.Count > 0)
                {
                    //Okay
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Computation_Options, no data to get");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {

                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Computation_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Computation_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Computation_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Computation_Options");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return expec_table;
        }

        public bool Get_Perform_Computation(int testId)
        {
            string Computation = "False";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getExternal = new SQLiteCommand("SELECT Computation FROM Test_Item_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader pdr = getExternal.ExecuteReader();
                if (pdr.Read())
                {
                    Computation = pdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Perform_Computation");
                    ServerErrorLog.GetERROR_SQL_Server("Check DB");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Perform_Computation");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Perform_Computation");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Perform_Computation");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Perform_Computation");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }

            bool returnBool = Convert.ToBoolean(Computation);
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return returnBool;
        }

        //*******************************************all functions related Multi Commands are stated here....**************************************// 
        //Inserting Multi-Commands parameters...
        public bool insert_MultiCommand_Expectedparams(int testId, int command_id, string param, int order)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdexpect = new SQLiteCommand("INSERT INTO MultiCommands_Expected_Params (Test_Item_Id, Command_Id, Expected_Params, Multiple_Command_Order) VALUES ('" + testId + "', '" + command_id + "', '" + param + "', '" + order + "')", sqliteconn);
                int yz = cmdexpect.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Inserting Multiple commands here...
        public bool insert_MultiCommands(int testId, int command_id, int order)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdexpect = new SQLiteCommand("INSERT INTO MultiCommands_Test_Items (Test_Item_Id, Command_Id, Command_Order) VALUES ('" + testId + "', '" + command_id + "', '" + order + "')", sqliteconn);
                int yz = cmdexpect.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Expectedparams");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Getting Test Item Id For Multi Command Functionality..
        public int Get_test_Item_IdForMultiCmd(string test_Name, int model, int statn)
        {
            int testId = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand testitemCmd = new SQLiteCommand("SELECT Test_Item_Id FROM Test_Items WHERE Model_Id = '" + model + "' AND Station_Id ='" + statn + "' AND Test_Item_Name='" + test_Name + "'", sqliteconn);
                SQLiteDataReader tdr = testitemCmd.ExecuteReader();
                if (tdr.Read())
                {
                    testId = Convert.ToInt32(tdr[0]);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return testId;
        }
        //Getting Multi Command Data Here...Not Completed yet.
        public int Get_test_Item_DetailsForMultiCmd(string test_Name, int model, int statn)
        {
            int testId = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand testitemCmd = new SQLiteCommand("SELECT Test_Item_Id FROM Test_Items WHERE Model_Id = '" + model + "' AND Station_Id ='" + statn + "' AND Test_Item_Name='" + test_Name + "'", sqliteconn);
                SQLiteDataReader tdr = testitemCmd.ExecuteReader();
                if (tdr.Read())
                {
                    testId = Convert.ToInt32(tdr[0]);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_test_Item_Id");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return testId;
        }
        //Get MultiCommand Option here..
        public string Get_MultiCmd_Opt(int TestId)
        {
            string opt = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand multiopt = new SQLiteCommand("SELECT MultiCommandOption FROM Test_Item_Options WHERE Test_Item_Id = '" + TestId + "'", sqliteconn);
                SQLiteDataReader tdr = multiopt.ExecuteReader();
                if (tdr.Read())
                {
                    opt = tdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultiCmd_Opt");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultiCmd_Opt");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultiCmd_Opt");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultiCmd_Opt");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultiCmd_Opt");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return opt;
        }
        //Getting all related commands to test item id
        public DataTable Get_MultipleCommands(int Test_id)
        {
            DataTable CmdTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand LoadCommandCMD = new SQLiteCommand("SELECT Command_Id, Command_Order FROM MultiCommands_Test_Items WHERE Test_Item_Id ='" + Test_id + "'", sqliteconn);
                SQLiteDataAdapter CMDAdptre = new SQLiteDataAdapter(LoadCommandCMD);
                CMDAdptre.Fill(CmdTable);
                int c = CmdTable.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("table Loaded Succesfully. Product");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultipleCommands");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultipleCommands");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultipleCommands");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultipleCommands");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultipleCommands");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return CmdTable;
        }
        //Getting Command Name(as String to send to FW)
        public string Get_MultiCmd_CMDName(int CMDId)
        {
            string cmdName = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand multiopt = new SQLiteCommand("SELECT Command_Name FROM Commands WHERE Command_Id = '" + CMDId + "'", sqliteconn);
                SQLiteDataReader tdr = multiopt.ExecuteReader();
                if (tdr.Read())
                {
                    cmdName = tdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultiCmd_CMDName");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultiCmd_CMDName");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultiCmd_CMDName");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultiCmd_CMDName");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MultiCmd_CMDName");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return cmdName;
        }
        //Getting mapped Command Table for multiple Commands.. Here...
        public DataTable Get_MappedCMD_ForMultiCmd(int Tech_Id, int Model_Id)
        {
            DataTable mappedCMDTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand GetMappedCMD = new SQLiteCommand("SELECT S.Command_Name, S.Command_Id FROM Commands AS S LEFT OUTER JOIN Product__Model_Command AS C ON S.Command_Id = C.Command_Id WHERE C.Product_Id ='" + Tech_Id + "' AND C.Model_Id = '" + Model_Id + "'", sqliteconn);
                SQLiteDataAdapter mappCMDAdptr = new SQLiteDataAdapter(GetMappedCMD);
                mappCMDAdptr.Fill(mappedCMDTable);
                int c = mappedCMDTable.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("table loaded succesfully. Mapped CMds,");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MappedCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MappedCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MappedCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MappedCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_MappedCMD");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return mappedCMDTable;
        }
        //Inserting Multi-Commands Send parameters...
        public bool insert_MultiCommand_Sendparams(int testId, int command_id, string param, int order)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdSend = new SQLiteCommand("INSERT INTO MultiCommands_Send_Params (Test_Item_Id, Command_Id, Send_Params, Multiple_Command_Order) VALUES ('" + testId + "', '" + command_id + "', '" + param + "', '" + order + "')", sqliteconn);
                int yz = cmdSend.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Sendparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Sendparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Sendparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Sendparams");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("insert_MultiCommand_Sendparams");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Getting MultiCommands Expected params here...
        public List<string> Get_ExpectedParams_MultiCMD(int TestId, int cmd_Id, int cmdOrder)
        {
            List<string> ParamsListEx = new List<string>();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT Expected_Params FROM MultiCommands_Expected_Params WHERE Test_Item_Id = '" + TestId + "' AND Command_Id = '" + cmd_Id + "' AND Multiple_Command_Order ='" + cmdOrder + "' ", sqliteconn);
                SQLiteDataAdapter adptr = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                adptr.Fill(dt);
                SQLiteDataReader dr = cmd.ExecuteReader();
                int cnt = dt.Rows.Count;
                for (int i = 0; i <= dt.Rows.Count; i++)
                {
                    if (dr.Read())
                    {
                        ParamsListEx.Add((string)dr["Expected_Params"]);
                    }
                }
                dr.Dispose();
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ExpectedParams_MultiCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ExpectedParams_MultiCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ExpectedParams_MultiCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ExpectedParams_MultiCMD");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ParamsListEx;
        }
        //Getting Multicommands Send Parameters Here...
        public List<string> Get_SendParams_MultiCMD(int TestId, int cmd_Id, int cmdOrder)
        {
            List<string> ParamsListEx = new List<string>();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT Send_Params FROM MultiCommands_Send_Params WHERE Test_Item_Id = '" + TestId + "' AND Command_Id = '" + cmd_Id + "' AND Multiple_Command_Order = '" + cmdOrder + "' ", sqliteconn);
                SQLiteDataAdapter adptr = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                adptr.Fill(dt);
                SQLiteDataReader dr = cmd.ExecuteReader();
                int cnt = dt.Rows.Count;
                for (int i = 0; i <= dt.Rows.Count; i++)
                {
                    if (dr.Read())
                    {
                        ParamsListEx.Add((string)dr["Send_Params"]);
                    }
                }
                dr.Dispose();
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SendParams_MultiCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SendParams_MultiCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SendParams_MultiCMD");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SendParams_MultiCMD");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ParamsListEx;
        }
        //Getting Test Items, Model Name, Station Name and Command Name(For Mapping to More then one Station.)...VJP-07 March 2016
        public DataTable Get_Test_Items_Details_FUll()
        {
            int count_Row = 0;
            DataTable Test_Item = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmd1 = new SQLiteCommand("SELECT T.Test_Item_Id, Test_Item_Name, C.Command_Name,  T.Command_Id , M.Model_Name, D.Station_Name FROM Test_Items AS T  JOIN Station_TestItem AS S ON S.Test_Item_Id = T.Test_Item_Id JOIN Commands AS C ON T.Command_Id = C.Command_Id JOIN Model AS M ON S.Model_Id=M.Model_Id JOIN Station AS D ON S.Station_Id=D.Station_Id  ORDER BY  S.Test_Item_Order", sqliteconn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd1);
                da.Fill(Test_Item);
                count_Row = Test_Item.Rows.Count;
                if (count_Row != 0)
                {
                    Console.WriteLine("Table Returned...");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                return Test_Item;
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Test_Item;
        }

        //Inserting values in Check Values from FW Table to Check(Compare Vales) Returned form FW ImPlemented in Configure Test Item Form And Class File function.... VJP-08 March 2016
        public bool Insert_CheckValues_From_FW(string _IndexToCheck,int NumberofParams, decimal MinValue, decimal maxValue, decimal TestID)
        {
            bool ok = false;
            int c = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand insertValuesCheck = new SQLiteCommand("INSERT INTO Check_Params_From_FW (RangeIndexToCheck, Number_Of_Params, Minimum_Value, Maximum_Value, Test_Item_Id) VALUES('"
                                                                        + _IndexToCheck + "','"
                                                                        + NumberofParams + "','"
                                                                        + MinValue + "','"
                                                                        + maxValue + "', '"
                                                                        + TestID + "')"
                                                                        , sqliteconn);
                c = insertValuesCheck.ExecuteNonQuery();
                if (c != 0)
                {
                    ok = true;
                    Console.WriteLine("Record inserted.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_CheckValues_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR8);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_CheckValues_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_CheckValues_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_CheckValues_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_CheckValues_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Checking For the option (Check Values Returned from FW.)
        public string Chk_Check_Params_Range_FW(int TestId)
        {
            string Chkvalue = "False";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand ckValueFW = new SQLiteCommand("SELECT Check_Params_From_FW FROM Test_Item_Options WHERE Test_Item_Id = '" + TestId + "'", sqliteconn);
                SQLiteDataReader RangeTextReader = ckValueFW.ExecuteReader();
                if (RangeTextReader.Read())
                {
                    Chkvalue = RangeTextReader["Check_Params_From_FW"].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_Check_Params_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_Check_Params_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_Check_Params_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_Check_Params_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_Check_Params_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return Chkvalue;
        }

        //Getting minimum and Maximum values form Check Values From FW Table For given Test Item Id
        public string[] Get_Values_to_check_From_FW(int TestId)
        {
            string[] ValuesListEx = new string[4];
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdTestVal = new SQLiteCommand("SELECT RangeIndexToCheck, Number_Of_Params, Minimum_Value, Maximum_Value FROM Check_Params_From_FW WHERE Test_Item_Id ='" + TestId + "' ", sqliteconn);
                SQLiteDataReader opdr = cmdTestVal.ExecuteReader();
                if (opdr == null)
                {
                    ValuesListEx = null;
                    ServerErrorLog.ERROR_Function_Name("Get_Values_to_check_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server("No Values Found in DB");
                }
                else
                {
                    while (opdr.Read())
                    {
                        ValuesListEx[0] = opdr[0].ToString();
                        ValuesListEx[1] = opdr[1].ToString();
                        ValuesListEx[2] = opdr[2].ToString();
                        ValuesListEx[3] = opdr[3].ToString();
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Values_to_check_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Values_to_check_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Values_to_check_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Values_to_check_From_FW");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ValuesListEx;
        }

        ////Get Mapped TestItems for modify Test Items Form(Only enable Test Items) Here...
        public DataTable Get_Mapped_Test_Items_For_ModifyForm(int Station_id, int Model_id)
        {
            DataTable MappedTestItems = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand chkmapped = new SQLiteCommand("SELECT T.Test_Item_Id, Test_Item_Name FROM Test_Items AS T JOIN Station_TestItem AS S ON S.Test_Item_Id = T.Test_Item_Id WHERE S.Model_Id = '" + Model_id + "' AND S.Station_Id ='" + Station_id + "' AND  S.Test_Item_Order > '" + 0 + "'", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(chkmapped);
                testAdptr.Fill(MappedTestItems);
                int y = MappedTestItems.Rows.Count;
                if (y != 0)
                {
                    //do NOthing...
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server("No Records.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Mapped_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return MappedTestItems;
        }
        //Deleting Samplpe File to Update new one for modify test Items....
        public bool Delete_Sample_File(int test_Id)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand deleteSample = new SQLiteCommand("DELETE  FROM Sample_Files WHERE Test_Item_Id = '" + test_Id + "'", sqliteconn);
                int y = deleteSample.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    Console.WriteLine("Record Inserted Succesfully. Sample File Table.");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Delete_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_Sample_File");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }

        //Get Sample File Image Name Only for deleting the existing file for Modify test Item...
        public string get_Sample_File_Name(int Test_Id)
        {
            string sampleFileName = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand sampleFileCmd = new SQLiteCommand("SELECT Sample_File_Name FROM Sample_Files  Where Test_Item_Id= '" + Test_Id + "' ", sqliteconn);
                SQLiteDataReader tdr = sampleFileCmd.ExecuteReader();
                if (tdr.Read())
                {
                    sampleFileName = tdr[0].ToString();
                    Console.WriteLine("SUccess");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File_Name");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR. " + xc.Message);
                }
            }
            return sampleFileName;
        }

        //get Hardware ID of SLA printer;
        public string get_HardwareID_SLA(int Model_Id)
        {
            string HardwareID_from_DB = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand sampleFileCmd = new SQLiteCommand("SELECT HardwareID FROM model  Where Model_Id = '" + Model_Id + "' ", sqliteconn);
                SQLiteDataReader tdr = sampleFileCmd.ExecuteReader();
                if (tdr.Read())
                {
                    HardwareID_from_DB = tdr[0].ToString();
                    Console.WriteLine("SUccess");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_Sample_File_Name");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR. " + xc.Message);
                }
            }
            return HardwareID_from_DB;
        }
        ///// added by prasad for Control Flow and Loop Control///////////////////////////////////////////
        //to get testitem name checked or not for retry control flow
        public string get_CheckTestItemNameretry_Flag(int testId)
        {
            string opt = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand multiopt = new SQLiteCommand("SELECT CheckTestItemName FROM Test_Item_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader tdr = multiopt.ExecuteReader();
                if (tdr.Read())
                {
                    opt = tdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_CheckTestItemNameretry_Flag");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_CheckTestItemNameretry_Flag");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_CheckTestItemNameretry_Flag");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_CheckTestItemNameretry_Flag");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_CheckTestItemNameretry_Flag");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return opt;
        }


        //to get testitem name  for retry control flow
        public string get_RetriTestItemNAme(int Test_Id)
        {
            string sampleretryItemName = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand sampleFileCmd = new SQLiteCommand("SELECT TestItemNameForRetry FROM Test_Item_Options  Where Test_Item_Id= '" + Test_Id + "' ", sqliteconn);
                SQLiteDataReader tdr = sampleFileCmd.ExecuteReader();
                if (tdr.Read())
                {
                    sampleretryItemName = tdr[0].ToString();
                    Console.WriteLine("SUccess");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_RetriTestItemNAme");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_RetriTestItemNAme");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_RetriTestItemNAme");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_RetriTestItemNAme");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_RetriTestItemNAme");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR. " + xc.Message);
                }
            }
            return sampleretryItemName;
        }


        //to get whether loop check condtion enable or disable
        public string getCheckLoop(int testId)
        {
            string opt = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand multiopt = new SQLiteCommand("SELECT CheckLoop FROM Test_Item_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader tdr = multiopt.ExecuteReader();
                if (tdr.Read())
                {
                    opt = tdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("getCheckLoop");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("getCheckLoop");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("getCheckLoop");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("getCheckLoop");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("getCheckLoop");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return opt;
        }

        //to get the value how many time to loop
        public int getLoopTime(int testId)
        {
            int opt=0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand multiopt = new SQLiteCommand("SELECT NumberOfLoop FROM Test_Item_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader tdr = multiopt.ExecuteReader();
                if (tdr.Read())
                {
                    opt = Convert.ToInt32(tdr[0]);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("getLoopTime");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("getLoopTime");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("getLoopTime");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("getLoopTime");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("getLoopTime");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return opt;
        }

        //to get test item name for loop
        public string get_loopTEstItemNAme(int Test_Id)
        {
            string sampleretryItemName = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand sampleFileCmd = new SQLiteCommand("SELECT LoopTestItemName FROM Test_Item_Options  Where Test_Item_Id= '" + Test_Id + "' ", sqliteconn);
                SQLiteDataReader tdr = sampleFileCmd.ExecuteReader();
                if (tdr.Read())
                {
                    sampleretryItemName = tdr[0].ToString();
                    Console.WriteLine("SUccess");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_loopTEstItemNAme");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_loopTEstItemNAme");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_loopTEstItemNAme");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("get_loopTEstItemNAme");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("get_loopTEstItemNAme");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR. " + xc.Message);
                }
            }
            return sampleretryItemName;
        }


        //to get test data of test item name for retry and test item id of that test item name
        public string[] GetDataFromOptionTable(int test_id)
        {
            string[] TestIDOptins = new string[2];
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdTestName = new SQLiteCommand("SELECT TestItemNameForRetry, RetryTestItemId FROM Test_Item_Options WHERE Test_Item_Id = '" + test_id + "'", sqliteconn);
                SQLiteDataReader tdr = cmdTestName.ExecuteReader();
                while (tdr.Read())
                {
                    TestIDOptins[0] = tdr[0].ToString();
                    TestIDOptins[1] = tdr[1].ToString();
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetDataFromOptionTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetDataFromOptionTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetDataFromOptionTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetDataFromOptionTable");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return TestIDOptins;
        }



        //to get test data of test item name for loop and test item id of that test item name
        public string[] GetLoopData(int test_id)
        {
            string[] TestIDOptins = new string[2];
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdTestName = new SQLiteCommand("SELECT LoopTestItemName, LoopTestItemId FROM Test_Item_Options WHERE Test_Item_Id = '" + test_id + "'", sqliteconn);
                SQLiteDataReader tdr = cmdTestName.ExecuteReader();
                while (tdr.Read())
                {
                    TestIDOptins[0] = tdr[0].ToString();
                    TestIDOptins[1] = tdr[1].ToString();
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetLoopData");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetLoopData");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetLoopData");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetLoopData");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return TestIDOptins;
        }
        //////////////////////////////////////////////////////////////////////////////////////



        //get User Iput Option Here
        public bool Get_Save_User_Input_Option(int TestId)
        {
            bool Option = false;
            string opt = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand Save_User_Input_Option = new SQLiteCommand("SELECT Save_User_Input FROM Test_Item_Options WHERE Test_Item_Id = '" + TestId + "'", sqliteconn);
                SQLiteDataReader tdr = Save_User_Input_Option.ExecuteReader();
                if (tdr.Read())
                {
                    opt = tdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Save_User_Input_Option");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Save_User_Input_Option");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Save_User_Input_Option");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Save_User_Input_Option");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Save_User_Input_Option");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            try
            {
                Option = Convert.ToBoolean(opt);
            }
            catch
            {
                return false;
            }
            return Option;
        }

        //Getting wait Option Here.
        public bool Get_Wait_Option(int testId)
        {
            string Wait_Option = "False";
            bool returnBool = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getExternal = new SQLiteCommand("SELECT Wait FROM Test_Item_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader pdr = getExternal.ExecuteReader();
                if (pdr.Read())
                {
                    Wait_Option = pdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Wait_Option");
                    ServerErrorLog.GetERROR_SQL_Server("Check DB");
                }
                returnBool = Convert.ToBoolean(Wait_Option);
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Wait_Option");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Wait_Option");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Wait_Option");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Wait_Option");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return returnBool;
        }

        //Getting Wait Duration Here.
        public int Get_Wait_Duration(int testId)
        {
            int Wait_Duration = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getExternal = new SQLiteCommand("SELECT Wait_Duration FROM Wait_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader pdr = getExternal.ExecuteReader();
                if (pdr.Read())
                {
                    Wait_Duration = Convert.ToInt32(pdr[0]);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server("Check DB");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return Wait_Duration;
        }

        //Inserting Wait Duration
        public bool Insert_Wait_Duration(int Duration, int testId)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                if (Duration > 0)
                {
                    SQLiteCommand insertValuesCheck = new SQLiteCommand("INSERT INTO Wait_Options (Wait_Duration ,Test_Item_Id) VALUES('" + Duration + "','" + testId + "')", sqliteconn);
                    int c = insertValuesCheck.ExecuteNonQuery();
                    if (c != 0)
                    {
                        ok = true;
                        Console.WriteLine("Record inserted.");
                    }
                    else
                    {
                        ServerErrorLog.ERROR_Function_Name("Insert_Wait_Duration");
                        ServerErrorLog.GetERROR_SQL_Server("Check DB");
                    }
                }

                else
                {

                    MessageBox.Show("Invalid wait time");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }



        //Inserting Wait Duration
        public bool Update_Wait_Duration(int Duration, int testId)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand insertValuesCheck = new SQLiteCommand("UPDATE Wait_Options  set Wait_Duration ='" + Duration + "' WHERE Test_item_id ='" + testId + "'", sqliteconn);
                int c = insertValuesCheck.ExecuteNonQuery();
                if (c != 0)
                {
                    ok = true;
                    Console.WriteLine("Record inserted.");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server("Check DB");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return ok;
        }


        //Inserting SN write Options.
        public bool Insert_Write_SN_Options(int _testId, string _CharacterNumber, string _Condition, string _AppendValue, string _UnconditionalAppendValue, string _AppendOption, string _TotalNumberofChars, string _TotalNumberofCharsOpt)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdexpect = new SQLiteCommand("INSERT INTO Write_SN_Options (Test_Item_Id, CharacterNumber, Condition, AppendValue, UnconditionalAppendValue, AppendOption,TotalNumberofChars,TotalNumberofCharsOpt) VALUES ('"
                                                            + _testId + "', '"
                                                            + _CharacterNumber + "', '"
                                                            + _Condition + "','"
                                                            + _AppendValue + "','"
                                                            + _UnconditionalAppendValue + "','"
                                                            + _AppendOption + "','"
                                                            + _TotalNumberofChars + "','"
                                                            + _TotalNumberofCharsOpt + "')"
                                                            , sqliteconn);
                int yz = cmdexpect.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }

        //Getting SN write Options.
        public string[] Get_SN_WriteOptions(int testId)
        {
            string[] WriteOptions = new string[100];
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand multiopt = new SQLiteCommand("SELECT * FROM Write_SN_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader tdr = multiopt.ExecuteReader();
                if (tdr.Read())
                {
                    WriteOptions[0] = tdr[0].ToString();
                    WriteOptions[1] = tdr[1].ToString();
                    WriteOptions[2] = tdr[2].ToString();
                    WriteOptions[3] = tdr[3].ToString();
                    WriteOptions[4] = tdr[4].ToString();
                    WriteOptions[5] = tdr[5].ToString();
                    WriteOptions[6] = tdr[6].ToString();
                    WriteOptions[7] = tdr[7].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_WriteOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_WriteOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_WriteOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_WriteOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_WriteOptions");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return WriteOptions;
        }

        //Delete SN Write Options.
        public bool Delete_SN_Write_Options(int testId)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand delUserCmd = new SQLiteCommand("DELETE FROM Write_SN_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                int y = delUserCmd.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    Console.WriteLine("Record Deleted form Write_SN_Options Table");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Write_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR6);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Write_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Write_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Write_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Write_Options");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }

        //Inserting SN Check Options
        public bool Insert_Check_SN_Options(int _testId, string _CharacterNumber, string _SequentialChars, string _ConditionalChars, string _Conditions, string _EqualCharsOpt, string _SequentialCharsOpt, string _ConditionalCharOpt, string _TotalNumberofChars, string _TotalNumberofCharsOpt)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdexpect = new SQLiteCommand("INSERT INTO Check_SN_Options (Test_Item_Id, CharacterNumber, SequentialChars, ConditionalChars, Conditions, EqualCharsOpt, SequentialCharsOpt, ConditionalCharOpt, TotalNumberofChars, TotalNumberofCharsOpt )VALUES ('" 
                                                                                            + _testId + "', '"
                                                                                            + _CharacterNumber + "', '"
                                                                                            + _SequentialChars + "','"
                                                                                            + _ConditionalChars + "','"
                                                                                            + _Conditions + "','"
                                                                                            + _EqualCharsOpt + "','"
                                                                                            + _SequentialCharsOpt + "','"
                                                                                            + _ConditionalCharOpt + "','"
                                                                                            + _TotalNumberofChars + "','"
                                                                                            +_TotalNumberofCharsOpt + "')"
                                                                                            , sqliteconn);
                int yz = cmdexpect.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }

        //Getting SN Check Options.
        public string[] Get_SN_CheckOptions(int testId)
        {
            string[] CheckOptions = new string[100];
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand multiopt = new SQLiteCommand("SELECT * FROM Check_SN_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader tdr = multiopt.ExecuteReader();
                if (tdr.Read())
                {
                    CheckOptions[0] = tdr[0].ToString();
                    CheckOptions[1] = tdr[1].ToString();
                    CheckOptions[2] = tdr[2].ToString();
                    CheckOptions[3] = tdr[3].ToString();
                    CheckOptions[4] = tdr[4].ToString();
                    CheckOptions[5] = tdr[5].ToString();
                    CheckOptions[6] = tdr[6].ToString();
                    CheckOptions[7] = tdr[7].ToString();
                    CheckOptions[8] = tdr[8].ToString();
                    CheckOptions[9] = tdr[9].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_CheckOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_CheckOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_CheckOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_CheckOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_CheckOptions");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return CheckOptions;
        }

        //Delete SN Check Options.
        public bool Delete_SN_Check_Options(int testId)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand delUserCmd = new SQLiteCommand("DELETE FROM Check_SN_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                int y = delUserCmd.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    Console.WriteLine("Record Deleted form Check_SN_Options Table");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Check_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR6);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Check_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Check_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Check_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Check_Options");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }

        //Delete Computation Options.
        public bool Delete_Computation_Options(int testId)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand delUserCmd = new SQLiteCommand("DELETE FROM Computation_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                int y = delUserCmd.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    Console.WriteLine("Record Deleted form Delete_Computation_Options");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Delete_Computation_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR6);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_Computation_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_Computation_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_Computation_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_Computation_Options");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }

        //Getting Sqlite Version here.
        public string Get_SQLITE_Version()
        {
            string[] SQLITE_Version = new string[6];
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM DB_Version WHERE   ID = (SELECT MAX(ID)  FROM DB_Version)", sqliteconn);
                //SQLiteCommand cmd = new SQLiteCommand("SELECT Send_Params FROM MultiCommands_Send_Params WHERE Test_Item_Id = '" + TestId + "' AND Command_Id = '" + cmd_Id + "' AND Multiple_Command_Order = '" + cmdOrder + "' ", sqliteconn);
                //SQLiteDataAdapter adptr = new SQLiteDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //adptr.Fill(dt);
                SQLiteDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    SQLITE_Version[0] = dr[0].ToString();
                    SQLITE_Version[1] = dr[1].ToString();
                    SQLITE_Version[2] = dr[2].ToString();
                    SQLITE_Version[3] = dr[3].ToString();
                    SQLITE_Version[4] = dr[4].ToString();
                    SQLITE_Version[5] = dr[5].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("SQLITE_Version");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("SQLITE_Version");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("SQLITE_Version");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("SQLITE_Version");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("SQLITE_Version");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            string ReturnString = SQLITE_Version[1] + "." + SQLITE_Version[2] + "." +SQLITE_Version[3];
            return ReturnString;
        }

        public bool Insert_SQLITE_Version(string _Major, string _Minor, string _Release, string _ReleaseNote, string _Owner)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdexpect = new SQLiteCommand("INSERT INTO DB_Version (MajorReleaseNumber, MinorReleaseNumber, RevisionReleaseNumber, Log, Owner) VALUES ('"
                                                            + _Major       + "', '"
                                                            + _Minor       + "', '"
                                                            + _Release     + "','"
                                                            + _ReleaseNote + "','"
                                                            + _Owner       +  "')"
                                                            , sqliteconn);
                int yz = cmdexpect.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Insert_SQLITE_Version");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_SQLITE_Version");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_SQLITE_Version");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_SQLITE_Version");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_SQLITE_Version");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }

        //-------------------------------added by  prasad for external tool data insert---------------------------------


        public bool Insert_ExternalTool_ParaMeters(int test_Id, string[] ToolParameters, int Model_Id)
        {
            bool ok = false;
            int c = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                for (int i = 0; i < ToolParameters.Length; i++)
                {
                    SQLiteCommand insertModelStn = new SQLiteCommand("INSERT INTO ExternalToolReceivedParameters (Test_Item_Id, ReceivedParameters, Model_Id) VALUES('" + test_Id + "','" + ToolParameters[i] + "','" + Model_Id + "')", sqliteconn);
                    c = insertModelStn.ExecuteNonQuery();
                    if (c != 0)
                    {
                        ok = true;
                        Console.WriteLine("Record inserted.");
                    }
                    else
                    {
                        ServerErrorLog.ERROR_Function_Name("Insert_ExternalTool_ParaMeters");
                        ServerErrorLog.GetERROR_SQL_Server(ERR8);
                    }
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_ExternalTool_ParaMeters");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_ExternalTool_ParaMeters");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_ExternalTool_ParaMeters");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_ExternalTool_ParaMeters");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }

        //Inserting SN Check Options
        public bool Insert_Check_SLA_Options(int _testId, string SLACheckIndex, string SLAIndexValue)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdexpect = new SQLiteCommand("INSERT INTO Check_SN_Options (Test_Item_Id, SLAIndexCheck, SLAIndexValue)VALUES ('"
                                                                                            + _testId + "', '"
                                                                                            + SLACheckIndex + "', '"
                                                                                            + SLAIndexValue + "')"
                                                                                            , sqliteconn);
                int yz = cmdexpect.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Insert_Check_SLA_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Write_SN_Options");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }
        //Checking SN send from FW option is True or not.
        public int Check_SLAOIndexValue(int TestId)
        {
            int textopt=0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand ckRangeText = new SQLiteCommand("SELECT SLAIndexValue FROM Check_SN_Options WHERE Test_Item_Id = '" + TestId + "'", sqliteconn);
                SQLiteDataReader RangeTextReader = ckRangeText.ExecuteReader();
                if (RangeTextReader.Read())
                {
                    textopt =Convert.ToInt32(RangeTextReader[0]);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAOIndexValue");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAOIndexValue");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAOIndexValue");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAOIndexValue");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAOIndexValue");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return textopt;
        }

        public string Check_SLAContainsOption(int TestId)
        {
            string textopt = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand ckRangeText = new SQLiteCommand("SELECT SLAContainsCheck1 FROM Check_SN_Options WHERE Test_Item_Id = '" + TestId + "'", sqliteconn);
                SQLiteDataReader RangeTextReader = ckRangeText.ExecuteReader();
                if (RangeTextReader.Read())
                {
                    textopt = RangeTextReader["SLAContainsCheck1"].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAContainsOption");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAContainsOption");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAContainsOption");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAContainsOption");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAContainsOption");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return textopt;
        }

        public string Check_SLAOIndexOption(int TestId)
        {
            string textopt = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand ckRangeText = new SQLiteCommand("SELECT SLAIndexCheck FROM Check_SN_Options WHERE Test_Item_Id = '" + TestId + "'", sqliteconn);
                SQLiteDataReader RangeTextReader = ckRangeText.ExecuteReader();
                if (RangeTextReader.Read())
                {
                    textopt = RangeTextReader[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAOIndexOption");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAOIndexOption");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAOIndexOption");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAOIndexOption");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SLAContainsOption");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return textopt;
        }

        //Getting SLA SN Check Options.
        public string[] Get_SN_CheckOptions_SLA(int testId)
        {
            string[] CheckOptions = new string[100];
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand multiopt = new SQLiteCommand("SELECT * FROM Check_SN_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader tdr = multiopt.ExecuteReader();
                if (tdr.Read())
                {
                    CheckOptions[0] = tdr[0].ToString();
                    CheckOptions[1] = tdr[1].ToString();
                    CheckOptions[2] = tdr[2].ToString();
                    CheckOptions[3] = tdr[3].ToString();
                    CheckOptions[4] = tdr[4].ToString();
                    CheckOptions[5] = tdr[5].ToString();
                    CheckOptions[6] = tdr[6].ToString();
                    CheckOptions[7] = tdr[7].ToString();
                    CheckOptions[8] = tdr[8].ToString();
                    CheckOptions[9] = tdr[9].ToString();
                    CheckOptions[10] = tdr[10].ToString();
                    CheckOptions[11] = tdr[11].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_CheckOptions_SLA");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_CheckOptions_SLA");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_CheckOptions_SLA");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_CheckOptions_SLA");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_SN_CheckOptions_SLA");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return CheckOptions;
        }

        //Delete SN Check Options.
        public bool Delete_SN_Check_Options_SLA(int testId)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand delUserCmd = new SQLiteCommand("DELETE FROM Check_SN_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                int y = delUserCmd.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    Console.WriteLine("Record Deleted form Check_SN_Options Table");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Check_Options_SLA");
                    ServerErrorLog.GetERROR_SQL_Server(ERR6);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Check_Options_SLA");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Check_Options_SLA");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Check_Options_SLA");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Check_Options_SLA");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }

        //Added By Milinda, Loading specific commands for a specific Model here
        public DataTable LoadCommandForModel(int _ProductID, int _ModelID)
        {
            DataTable CMDTable = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdchkcmd = new SQLiteCommand("SELECT Command_Name, S.Command_Id AS V FROM Commands AS S LEFT OUTER JOIN Product__Model_Command AS C ON S.Command_Id = C.Command_Id WHERE C.Product_Id='" + _ProductID + "' AND C.Model_Id = '" + _ModelID + "'", sqliteconn);
                SQLiteDataAdapter adptr = new SQLiteDataAdapter(cmdchkcmd);
                adptr.Fill(CMDTable);
                int c = CMDTable.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("table Loaded Succesfully. Product");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("LoadCommandForModel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("LoadCommandForModel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("LoadCommandForModel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("LoadCommandForModel");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("LoadCommandForModel");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return CMDTable;
        }

        //Added By Milinda to Insert Check Printer Status Options.
        public bool Insert_Check_Printer_Status_Options(int _testId, int _CommandID_1, int _CommandID_2, string _PassCondition_1, string _PassCondition_2)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdexpect = new SQLiteCommand("INSERT INTO Check_Printer_Status_Options (Test_Item_Id, CommandID_1, CommandID_2, PassCondition_1, PassCondition_2) VALUES ('"
                                                            + _testId + "', '"
                                                            + _CommandID_1 + "', '"
                                                            + _CommandID_2 + "','"
                                                            + _PassCondition_1 + "','"
                                                            + _PassCondition_2 + "')"
                                                            , sqliteconn);
                int yz = cmdexpect.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Insert_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }

        //Getting Check Printer Status Options.
        public string[] Get_Check_Printer_Status_Options(int testId)
        {
            string[] CheckOptions = new string[100];
            try
            {
                // 0 is Test Item ID
                // 1 is CommanID_1
                // 2 is CommandID_2
                // 3 is Passcondition_1
                // 4 is Passcondition_2
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand multiopt = new SQLiteCommand("SELECT * FROM Check_Printer_Status_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader tdr = multiopt.ExecuteReader();
                if (tdr.Read())
                {
                    CheckOptions[0] = tdr[0].ToString();
                    CheckOptions[1] = tdr[1].ToString();
                    CheckOptions[2] = tdr[2].ToString();
                    CheckOptions[3] = tdr[3].ToString();
                    CheckOptions[4] = tdr[4].ToString();
                    //CheckOptions[5] = tdr[5].ToString();
                    //CheckOptions[6] = tdr[6].ToString();
                    //CheckOptions[7] = tdr[7].ToString();
                    //CheckOptions[8] = tdr[8].ToString();
                    //CheckOptions[9] = tdr[9].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse is Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return CheckOptions;
        }

        //Added By Milinda to Delete Check Printer Status Options.
        public bool Delete_Check_Printer_Status_Options(int testId)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand delUserCmd = new SQLiteCommand("DELETE FROM Check_Printer_Status_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                int y = delUserCmd.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    Console.WriteLine("Record Deleted form Delete_Check_Printer_Status_Options");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Delete_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR6);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_Check_Printer_Status_Options");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_SN_Check_Options");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }

        //Added By Milinda to get Check Printer Status Options
        public bool Get_Check_Printer_Status_Option(int testId)
        {
            string Option = "False";
            bool returnBool = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getExternal = new SQLiteCommand("SELECT CheckPrinterStatus FROM Test_Item_Options WHERE Test_Item_Id = '" + testId + "'", sqliteconn);
                SQLiteDataReader pdr = getExternal.ExecuteReader();
                if (pdr.Read())
                {
                    Option = pdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Check_Printer_Status_Option");
                    ServerErrorLog.GetERROR_SQL_Server("Check DB");
                }
                returnBool = Convert.ToBoolean(Option);
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Check_Printer_Status_Option");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Check_Printer_Status_Option");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Check_Printer_Status_Option");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Check_Printer_Status_Option");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return returnBool;
        }

        //--------------------------added by Nitish update Fw version---------

        //Update Model Details...
        public bool Update_Model_fw_version(string FW_Version, int Model_Id)
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand UpdateModel_fw_CMD = new SQLiteCommand("UPDATE Model set Model_FW_Version = '" + FW_Version + "' WHERE Model_Id ='" + Model_Id + "'", sqliteconn);
                int y = UpdateModel_fw_CMD.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    MessageBox.Show("Details Updated Succesfully.");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Update_Model");
                    ServerErrorLog.GetERROR_SQL_Server("Record is Not Updated Succesfully.Model: " + Model_Id);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Update_Model");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return ok;
        }


        //Added by Nitish--adding firmware version in testItem expected paramerters table

        //  we need Test-item-id  for  Test_Item_Expected_Parameters  table to change Expected _parameter

        // for that we need to check test_item table

        //in this "Model_id"  and Test_item _name  will give us Test_item id

        public bool Update_testItem_expceted_parm_fw(string FW_Version, int Model_Id)
        {
            bool ok = false;

            string pattern_Test_chkfw = @"(chk|check)\s*_*\s*(fw|firmware)\s*_*\s*(ver|version)";

            int count_Row = 0;
            DataTable dt = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmd1 = new SQLiteCommand("SELECT  Test_Item_Id,Test_Item_Name  FROM Test_Items  WHERE Model_Id = '" + Model_Id + "'", sqliteconn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd1);
                da.Fill(dt);
                count_Row = dt.Rows.Count;

                int test_item_id=0;
                if (count_Row != 0)
                {
                    Console.WriteLine("Table Returned...");
                    foreach(DataRow dr in dt.Rows)
                    {
                        string item_name = dr["Test_Item_Name"].ToString().ToLower();
                        Console.WriteLine(item_name);
                        if (Regex.IsMatch(item_name,pattern_Test_chkfw))
                            {
                                //MessageBox.Show("Test Item :- ' "+item_name+" '  is being updated..");
                                test_item_id = Convert.ToInt32(dr["Test_Item_Id"]);
                                break;
                            }
                    }
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }

                //MessageBox.Show("test item fw found" + test_item_id);

                //now with this test item id we update test item parameter table

                SQLiteCommand Update_Expected_fw_CMD = new SQLiteCommand("update Test_Item_Expected_Parameters set Expected_Parameter ='" + FW_Version + "' WHERE Test_item_id ='" + test_item_id + "'", sqliteconn);
                int y = Update_Expected_fw_CMD.ExecuteNonQuery();
                if (y != 0)
                {
                    ok = true;
                    MessageBox.Show("New Fw version Updated Succesfully.");
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Update_Model");
                    ServerErrorLog.GetERROR_SQL_Server("Record is Not Updated Succesfully.Model: " + Model_Id);
                }

            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Items");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            

            return ok;
        }
        //''''''''''''''''''''''''''''''''''''''''added by prasad''''''''''''''''''''''''''''''''
        //to check sample file name checked or not
        public string Check_SampleFileNameNobel(int TestId)
        {
            string name = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand ckRangeText = new SQLiteCommand("SELECT CheckSampleFile FROM Test_Item_Options WHERE Test_Item_Id = '" + TestId + "'", sqliteconn);
                SQLiteDataReader RangeTextReader = ckRangeText.ExecuteReader();
                if (RangeTextReader.Read())
                {
                    name = RangeTextReader[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SampleFileName");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SampleFileName");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SampleFileName");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SampleFileName");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SampleFileName");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return name;
        }

        //to get sample file name 
        public string Get_SampleFileNameNobel(int TestId)
        {
            string name = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand ckRangeText = new SQLiteCommand("SELECT SampleFileName FROM Test_Item_Options WHERE Test_Item_Id = '" + TestId + "'", sqliteconn);
                SQLiteDataReader RangeTextReader = ckRangeText.ExecuteReader();
                if (RangeTextReader.Read())
                {
                    name = RangeTextReader[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SampleFileName");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SampleFileName");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SampleFileName");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SampleFileName");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Check_SampleFileName");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return name;
        }
        //Added By Milinda, Loading Model Details.
        public DataTable GetModelDetails(string ModelCode)
        {
            DataTable ModelDetails = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                //SQLiteCommand cmdchkcmd = new SQLiteCommand("SELECT Command_Name, S.Command_Id AS V FROM Commands AS S LEFT OUTER JOIN Product__Model_Command AS C ON S.Command_Id = C.Command_Id WHERE C.Product_Id='" + _ProductID + "' AND C.Model_Id = '" + _ModelID + "'", sqliteconn);
                string command = "SELECT Model.Model_Name, Model.Model_Code, Model.Model_Id, Product.Product_Type, Product.Product_Id FROM Model INNER JOIN Product ON Model.Product_Id = Product.Product_Id WHERE Model.Model_Code =" + "'" + ModelCode + "'";
                SQLiteCommand cmdchkcmd = new SQLiteCommand(command,sqliteconn);
                SQLiteDataAdapter adptr = new SQLiteDataAdapter(cmdchkcmd);
                adptr.Fill(ModelDetails);
                int c = ModelDetails.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("table Loaded Succesfully. Product");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetModelDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetModelDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetModelDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetModelDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetModelDetails");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return ModelDetails;
        }
        //Getting Wait Duration Here.
        public int Get_Factory_ID(string FactoryName)
        {
            int FactoryID = 0;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getExternal = new SQLiteCommand("SELECT WorkLoc_ID FROM Work_Location WHERE FactoryName = '" + FactoryName + "'", sqliteconn);
                SQLiteDataReader pdr = getExternal.ExecuteReader();
                if (pdr.Read())
                {
                    FactoryID = Convert.ToInt32(pdr[0]);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Wait_Duration");
                    ServerErrorLog.GetERROR_SQL_Server("Get_Factory_ID");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Factory_ID");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Factory_ID");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Factory_ID");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Factory_ID");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return FactoryID;
        }

        //Getting Wait Duration Here.
        public string Get_Country_Name(int CountryID)
        {
            string CountryName = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand getExternal = new SQLiteCommand("SELECT CountryName FROM Work_Location WHERE WorkLoc_ID = '" + CountryID + "'", sqliteconn);
                SQLiteDataReader pdr = getExternal.ExecuteReader();
                if (pdr.Read())
                {
                    CountryName = pdr[0].ToString();
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Country_Name");
                    ServerErrorLog.GetERROR_SQL_Server("Get_Country_Name");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Country_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Country_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Country_Name");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Country_Name");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return CountryName;
        }

        //Getting scan and auto detect table for datagridview
        public DataTable Get_ScanAutoDetectTable()
        {
            int count_Row = 0;
            DataTable dt = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                string command = "SELECT S.Peripheral_Model_Name, S.Peripheral_Model_Code, S.Peripheral_Model_ID, M.Model_Name, M.Model_Code, M.Model_Id, P.Product_Type, P.Product_Id FROM ScanAutoDetect_Options S INNER JOIN Product P ON P.Product_Id = S.Tech_ID INNER JOIN Model M ON M.Model_Id = S.Printer_Model_ID";
                //SQLiteCommand cmd1 = new SQLiteCommand("SELECT * FROM ScanAutoDetect_Options", sqliteconn);
                SQLiteCommand cmd1 = new SQLiteCommand(command, sqliteconn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd1);
                da.Fill(dt);
                count_Row = dt.Rows.Count;
                if (count_Row != 0)
                {
                    Console.WriteLine("Table Returned...");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ScanAutoDetectTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ScanAutoDetectTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ScanAutoDetectTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ScanAutoDetectTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ScanAutoDetectTable");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return dt;
        }

        //Getting scan and auto detect table for datagridview
        public DataTable Get_PeriphDetails(string ModelCode)
        {
            int count_Row = 0;
            DataTable dt = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmd1 = new SQLiteCommand("SELECT S.Peripheral_Model_Name, S.Peripheral_Model_Code, S.Peripheral_Model_ID, M.Model_Name, M.Model_Code, M.Model_Id, P.Product_Type, P.Product_Id FROM ScanAutoDetect_Options S INNER JOIN Product P ON P.Product_Id = S.Tech_ID INNER JOIN Model M ON M.Model_Id = S.Printer_Model_ID WHERE Peripheral_Model_Code =" + "'" + ModelCode + "'", sqliteconn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd1);
                da.Fill(dt);
                count_Row = dt.Rows.Count;
                if (count_Row != 0)
                {
                    Console.WriteLine("Table Returned...");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_PeriphDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_PeriphDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_PeriphDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_PeriphDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_PeriphDetails");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return dt;
        }

        //Added By Milinda, Loading Model Details.
        public DataTable GetWorkLocDetails(string Domain)
        {
            DataTable WorkLocDetails = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                //SQLiteCommand cmdchkcmd = new SQLiteCommand("SELECT Command_Name, S.Command_Id AS V FROM Commands AS S LEFT OUTER JOIN Product__Model_Command AS C ON S.Command_Id = C.Command_Id WHERE C.Product_Id='" + _ProductID + "' AND C.Model_Id = '" + _ModelID + "'", sqliteconn);
                string command = "SELECT * FROM Work_Location WHERE DomainName =" + "'" + Domain + "'";
                SQLiteCommand cmdchkcmd = new SQLiteCommand(command, sqliteconn);
                SQLiteDataAdapter adptr = new SQLiteDataAdapter(cmdchkcmd);
                adptr.Fill(WorkLocDetails);
                int c = WorkLocDetails.Rows.Count;
                if (c != 0)
                {
                    Console.WriteLine("table Loaded Succesfully. Product");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetWorkLocDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetWorkLocDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetWorkLocDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("GetWorkLocDetails");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("GetWorkLocDetails");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return WorkLocDetails;
        }

        //Added By Milinda to Insert ScanAutoDetect Options.
        public bool Insert_ScanAutoDetectOptions(
                                                    string PeriphModelName,
                                                    string PeriphModelCode,
                                                    int TechID,
                                                    int PrinterModelID
                                                )
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdexpect = new SQLiteCommand("INSERT INTO ScanAutoDetect_Options (Peripheral_Model_Name, Peripheral_Model_Code, Tech_ID, Printer_Model_ID) VALUES ('"
                                                            + PeriphModelName + "', '"
                                                            + PeriphModelCode + "', '"
                                                            + TechID + "','"
                                                            + PrinterModelID + "')"
                                                            , sqliteconn);
                int yz = cmdexpect.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Insert_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Insert_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return ok;
        }
        
        //Added By Milinda to Update ScanAutoDetect Options.
        public bool Update_ScanAutoDetectOptions(
                                                    string PeriphModelName,
                                                    string PeriphModelCode,
                                                    int TechID,
                                                    int PrinterModelID,
                                                    int PrefiphModelID
                                                )
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdexpect = new SQLiteCommand("UPDATE ScanAutoDetect_Options SET Peripheral_Model_Name = @PeriphModelName, Peripheral_Model_Code = @PeriphModelCode, Tech_ID = @TechID, Printer_Model_ID = @PrinterModelID WHERE Peripheral_Model_ID = @PrefiphModelID"
                                                            , sqliteconn);

                cmdexpect.Parameters.AddWithValue("@PeriphModelName", PeriphModelName);
                cmdexpect.Parameters.AddWithValue("@PeriphModelCode", PeriphModelCode);
                cmdexpect.Parameters.AddWithValue("@TechID", TechID);
                cmdexpect.Parameters.AddWithValue("@PrinterModelID", PrinterModelID);
                cmdexpect.Parameters.AddWithValue("@PrefiphModelID", PrefiphModelID);

                int yz = cmdexpect.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Update_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Update_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return ok;
        }

        //Added By Milinda to Delete ScanAutoDetect Options.
        public bool Delete_ScanAutoDetectOptions(
                                                    int PrefiphModelID
                                                )
        {
            bool ok = false;
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand cmdexpect = new SQLiteCommand("DELETE FROM ScanAutoDetect_Options WHERE Peripheral_Model_ID = @PrefiphModelID"
                                                            , sqliteconn);

                cmdexpect.Parameters.AddWithValue("@PrefiphModelID", PrefiphModelID);

                int yz = cmdexpect.ExecuteNonQuery();
                if (yz != 0)
                {
                    ok = true;
                }
                else
                {
                    ok = false;
                    ServerErrorLog.ERROR_Function_Name("Delete_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Delete_ScanAutoDetectOptions");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return ok;
        }


        //--------------added by nitish -------------------------//
        //Getting Test Results Based on Stations..
         
        public DataTable Get_Test_Result_On_Station(string TechName, string Model, string station)
        {
            DataTable TestItemResult = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand GetTestItemWise = new SQLiteCommand("SELECT * FROM Test_Results  Where  Technology_Name='" + TechName + "'AND Model_Name='" + Model + "' AND Test_Station='" + station +"'", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(GetTestItemWise);
                testAdptr.Fill(TestItemResult);
                int y = TestItemResult.Rows.Count;
                if (y != 0)
                {
                    //DO NOTHING HERE> OK.      
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server("Could not load table. Check for Records, or Data Table.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return TestItemResult;
        }


        //getting user names for  pass test items


        public DataTable Get_Username_for_Pass_Test()
        {
            DataTable TestItemResult = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand GetUserWise = new SQLiteCommand("SELECT distinct User_Name FROM Test_Results  Where  Test_Result= 'Pass' ", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(GetUserWise);
                testAdptr.Fill(TestItemResult);
                int y = TestItemResult.Rows.Count;
                if (y != 0)
                {
                    //DO NOTHING HERE> OK.      
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server("Could not load table. Check for Records, or Data Table.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return TestItemResult;
        }



        public DataTable Get_TestResults_for_User(string user_name)
        {
            DataTable TestItemResult = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand GetUserWise = new SQLiteCommand("SELECT * FROM Test_Results  Where  Test_Result= 'Pass' AND  User_Name ='" + user_name + "'", sqliteconn);
                SQLiteDataAdapter testAdptr = new SQLiteDataAdapter(GetUserWise);
                testAdptr.Fill(TestItemResult);
                int y = TestItemResult.Rows.Count;
                if (y != 0)
                {
                    //DO NOTHING HERE> OK.      
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server("Could not load table. Check for Records, or Data Table.");
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Test_Result_On_Station");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return TestItemResult;
        }


        //Added by milinda to get the Root_Cause from the Error code.
        public string getRoot_Cause(string ErrorCode)
        {
            string Root_Cause = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand sampleFileCmd = new SQLiteCommand("SELECT Root_Cause FROM ErrorCodes  Where ErrorCode = '" + ErrorCode + "' ", sqliteconn);
                SQLiteDataReader tdr = sampleFileCmd.ExecuteReader();
                if (tdr.Read())
                {
                    Root_Cause = tdr[0].ToString();
                    Console.WriteLine("Success");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("getRoot_Cause");
                    ServerErrorLog.GetERROR_SQL_Server(ERR1);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("getRoot_Cause");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("getRoot_Cause");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("getRoot_Cause");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("getRoot_Cause");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR. " + xc.Message);
                }
            }
            sqliteconn.Close();
            GC.Collect();
            return Root_Cause;
        }

        //Getting scan and auto detect table for datagridview
        public DataTable Get_ErrorCodesTable()
        {
            int count_Row = 0;
            DataTable dt = new DataTable();
            try
            {
                SQLiteConnection.ClearAllPools();
                string command = "SELECT * FROM ErrorCodes";
                SQLiteCommand cmd1 = new SQLiteCommand(command, sqliteconn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd1);
                da.Fill(dt);
                count_Row = dt.Rows.Count;
                if (count_Row != 0)
                {
                    Console.WriteLine("Table Returned...");
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ErrorCodeTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ErrorCodeTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ErrorCodeTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ErrorCodeTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_ErrorCodeTable");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            SQLiteConnection.ClearAllPools();
            //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
            sqliteconn.Close();
            GC.Collect();
            return dt;
        }

        //Updating ErrorCode Table
        public int Update_ErrorCodesTable(DataTable dtbl)
        {
            int rowsAffected = 0;

            try
            {
                string sqliteCommand = "SELECT * FROM ErrorCodes";
                SQLiteDataAdapter sqliteAdapter = new SQLiteDataAdapter(sqliteCommand, sqliteconn);

                SQLiteCommandBuilder sqliteCmdBldr = new SQLiteCommandBuilder(sqliteAdapter);
                rowsAffected = sqliteAdapter.Update(dtbl);
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_ErrorCodesTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                    //return ERR4;
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_ErrorCodesTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                    //return ERR5;
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Update_ErrorCodesTable");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Update_ErrorCodesTable");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }

                throw;
            }
            
            return rowsAffected;
        }

        //Checking Copy_EB_SN is True or not. by Sachini 17-Aug-2016
        public string Get_Copy_EB_SN(int TestId)
        {
            string textopt = "False";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand ckText = new SQLiteCommand("SELECT Copy_EB_SN FROM Test_Item_Options WHERE Test_Item_Id = '" + TestId + "'", sqliteconn);
                SQLiteDataReader TextReader = ckText.ExecuteReader();
                if (TextReader.Read())
                {
                    textopt = TextReader["Copy_EB_SN"].ToString();
                    TextReader.Close();

                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Chk_TextFromFW");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Copy_EB_SN");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Copy_EB_SN");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Copy_EB_SN");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_Copy_EB_SN");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return textopt;
        }


        //Get the driver name for model. by Sachini 22-Aug-2016
        public string Get_drivername_model(int ModelId)
        {
            string textopt = "";
            try
            {
                SQLiteConnection.ClearAllPools();
                //New SQLITE dll sqliteconn.Dispose(); Commented by Milinda on 1st of August 2016
                sqliteconn.Close();
                GC.Collect();
                sqliteconn.Open();
                SQLiteCommand ckText = new SQLiteCommand("SELECT Driver_Name FROM Model WHERE Model_Id= '" + ModelId + "'", sqliteconn);
                SQLiteDataReader TextReader = ckText.ExecuteReader();
                if (TextReader.Read())
                {
                    textopt = TextReader["Driver_Name"].ToString();
                    TextReader.Close();

                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_drivername_model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
            }
            catch (Exception xc)
            {
                if (xc.Message.Contains("Databse Locked"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_drivername_model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR4);
                }
                else if (xc.Message.Contains("Databse is Not Open"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_drivername_model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR5);
                }
                else if (xc.Message.Contains("Can Not Read Table"))
                {
                    ServerErrorLog.ERROR_Function_Name("Get_drivername_model");
                    ServerErrorLog.GetERROR_SQL_Server(ERR2);
                }
                else
                {
                    ServerErrorLog.ERROR_Function_Name("Get_drivername_model");
                    ServerErrorLog.GetERROR_SQL_Server("Un-Known ERROR." + xc.Message);
                }
            }
            return textopt;
        }


    }
}
