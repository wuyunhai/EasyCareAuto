using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MES.SocketService
{
    public static class DBHelper
    {
        public static string SQL = string.Empty;

        public static string ConnStr = _GetDBConn();
        public static string _GetDBConn()
        {
            string path = Directory.GetCurrentDirectory() + "/SystemSettings.ini";

            //string path = Path.GetFullPath("SystemSettings.ini");

            if (!File.Exists(path))
            {
                return string.Empty;
            }

            IniFile ini = new IniFile(path);
            string server = ini.ReadString("Settings", "SalcompDBConn", "");
            string DB_Server = ini.ReadString("Settings", "DB_Server", "");
            string DB_Name = ini.ReadString("Settings", "DB_Name", "");
            string DB_User = ini.ReadString("Settings", "DB_User", "");
            string DB_Pwd = ini.ReadString("Settings", "DB_Password", "");

            server = "Data Source=" + DB_Server + ";Initial Catalog=" + DB_Name + ";Persist Security Info=True;User ID=" + DB_User + ";Password=" + DB_Pwd + ";Max Pool Size=500; Min Pool Size=20;Pooling=true;Connect Timeout=2";
            return server;
        }

        public static string Language_sign = string.Empty;

        public static string _GetID(string SQL)
        {
            DataBase DB_Global = new DataBase(DBHelper._GetDBConn());
            DataSet ds_PerHour = new DataSet();
            ds_PerHour = DB_Global.RunProc(SQL, ds_PerHour);
            if (ds_PerHour.Tables[0].Rows.Count > 0)
            {
                string str = ds_PerHour.Tables[0].Rows[0][0].ToString();
                return ds_PerHour.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "-1";
            }
        }

        public static DataTable _GetGrdInfo(string SQL)
        {
            DataBase DB_Global = new DataBase(DBHelper._GetDBConn());
            DataSet ds_PerHour = new DataSet();
            ds_PerHour = DB_Global.RunProc(SQL, ds_PerHour);
            return ds_PerHour.Tables[0];
        }

        public static DataTable _GetDTInfo(string SQL)
        {
            DataBase DB_Global = new DataBase(DBHelper._GetDBConn());
            DataSet ds_PerHour = new DataSet();
            ds_PerHour = DB_Global.RunProc(SQL, ds_PerHour);
            return ds_PerHour.Tables[0];
        }

        public static int EXECRecord_RowsAffected(string SQL)
        {
            SqlConnection thisConnection = new SqlConnection(DBHelper._GetDBConn());
            thisConnection.Open();
            SqlCommand thisCommand = thisConnection.CreateCommand();
            thisCommand.CommandText = SQL;
            int rowsAffected = thisCommand.ExecuteNonQuery();
            thisConnection.Close();
            return rowsAffected;
        }


        #region 业务集

        /// <summary>
        /// 获取当前工艺生产产品类别
        /// </summary>
        /// <returns></returns>
        public static DataTable GettechnologyType()
        {
            SqlParameter[] parameter = new SqlParameter[] {
            };
            DataSet ds = new DataSet();
            ds = DBHelper.ExecuteDataSet(DBHelper._GetDBConn(), CommandType.StoredProcedure, "SFC_DM_WorkOrderRemain", parameter);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询工单余量，返回工单编码，工单做完则返回【-1】
        /// </summary>
        /// <returns></returns>
        public static DataTable GetWorkOrderRemain()
        {
            SqlParameter[] parameter = new SqlParameter[] {
            };
            DataSet ds = new DataSet();
            ds = DBHelper.ExecuteDataSet(DBHelper._GetDBConn(), CommandType.StoredProcedure, "SFC_DM_WorkOrderRemain", parameter);
            return ds.Tables[0];
        }

        #endregion

        #region 存储过程执行方法

        /// <summary>
        /// 为执行命令准备参数
        /// </summary>
        /// <param name="cmd">SqlCommand 命令</param>
        /// <param name="conn">已经存在的数据库连接</param>
        /// <param name="trans">数据库事务处理</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">Command text，T-SQL语句 例如 Select * from Products</param>
        /// <param name="cmdParms">返回带参数的命令</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            //判断数据库连接状态
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            //判断是否需要事务处理
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }


        /// <summary>
        /// return a dataset
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(DBHelper._GetDBConn(), cmdType, cmdText, commandParameters);
        }

        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="cmdText">存储过程的名字</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSetProducts(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(DBHelper._GetDBConn(), CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        /// 返回一个DataSet
        /// </summary>

        /// <param name="cmdText">T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSetText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(DBHelper._GetDBConn(), CommandType.Text, cmdText, commandParameters);
        }

        public static DataView ExecuteDataSet(string connectionString, string sortExpression, string direction, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = sortExpression + " " + direction;
                return dv;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }


        #endregion

    }

    public class DataBase
    {
        string ConnStr = string.Empty;

        public DataBase(string Str)
        {
            this.ConnStr = Str;
            try
            {
                if (Str == "")
                {

                    this.ConnStr = Str;

                }
                else
                {
                    this.ConnStr = Str;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回connection对象
        /// @auther lyq
        /// @date   2010.6.18
        /// </summary>
        /// <returns></returns>
        public SqlConnection ReturnConn()
        {
            SqlConnection Conn = new SqlConnection(ConnStr);
            Conn.Open();
            return Conn;
        }
        public void Dispose(SqlConnection Conn)
        {
            if (Conn != null)
            {
                Conn.Close();
                Conn.Dispose();
            }
            GC.Collect();
        }
        public void Dispose()
        {
            GC.Collect();
        }
        /// <summary>
        /// 运行SQL语句,返回SqlCommand对象
        /// @auther lyq
        /// @date   2010.6.18
        /// </summary>
        /// <param name="SQL"></param>
        public void RunProc(string SQL)
        {
            SqlConnection Conn;
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            SqlCommand Cmd;
            Cmd = CreateCmd(SQL, Conn);
            try
            {
                Cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception(SQL);
            }
            Dispose(Conn);
            return;
        }

        /// <summary>
        /// 运行SQL语句返回DataReader
        /// @auther lyq
        /// @date   2010.6.18
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns>SqlDataReader对象.</returns>
        public SqlDataReader RunProcGetReader(string SQL)
        {
            SqlConnection Conn;
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            SqlCommand Cmd;
            Cmd = CreateCmd(SQL, Conn);
            SqlDataReader Dr;
            try
            {
                Dr = Cmd.ExecuteReader(CommandBehavior.Default);
            }
            catch
            {
                throw new Exception(SQL);
            }
            //Dispose(Conn);
            return Dr;
        }

        /// <summary>
        /// 生成Command对象
        /// @auther lyq
        /// @date   2010.6.18
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="Conn"></param>
        /// <returns></returns>
        public SqlCommand CreateCmd(string SQL, SqlConnection Conn)
        {
            SqlCommand Cmd;
            Cmd = new SqlCommand(SQL, Conn);
            return Cmd;
        }

        /// <summary>
        /// 生成Command对象
        /// @auther lyq
        /// @date   2010.6.18
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public SqlCommand CreateCmd(string SQL)
        {
            SqlConnection Conn;
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            SqlCommand Cmd;
            Cmd = new SqlCommand(SQL, Conn);
            return Cmd;
        }

        /// <summary>
        /// 运行SQL语句，返回adapter对象
        /// @auther lyq
        /// @date   2010.6.18
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="Conn"></param>
        /// <returns></returns>
        public SqlDataAdapter CreateDa(string SQL)
        {
            SqlConnection Conn;
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            SqlDataAdapter Da;
            Da = new SqlDataAdapter(SQL, Conn);
            return Da;
        }

        /// <summary>
        /// 运行SQL语句,返回DataSet对象
        /// @auther lyq
        /// @date   2010.6.18
        /// </summary>
        /// <param name="procName">SQL语句</param>
        /// <param name="prams">DataSet对象</param>
        public DataSet RunProc(string SQL, DataSet Ds)
        {
            SqlConnection Conn;
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            SqlDataAdapter Da;
            //Da = CreateDa(SQL, Conn);
            Da = new SqlDataAdapter(SQL, Conn);
            try
            {
                Da.Fill(Ds);
            }
            catch (Exception Err)
            {
                throw Err;
            }

            Dispose(Conn);
            return Ds;
        }

        /// <summary>
        /// 运行SQL语句,返回DataSet对象
        /// @auther lyq
        /// @date   2010.6.18
        /// </summary>
        /// <param name="procName">SQL语句</param>
        /// <param name="prams">DataSet对象</param>
        /// <param name="dataReader">表名</param>
        public DataSet RunProc(string SQL, DataSet Ds, string tablename)
        {
            SqlConnection Conn;
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            SqlDataAdapter Da;
            Da = CreateDa(SQL);
            try
            {
                Da.Fill(Ds, tablename);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            Dispose(Conn);
            return Ds;
        }

        /// <summary>
        /// 运行SQL语句,返回DataSet对象
        /// @auther lyq
        /// @date   2010.6.18
        /// </summary>
        /// <param name="procName">SQL语句</param>
        /// <param name="prams">DataSet对象</param>
        /// <param name="dataReader">表名</param>
        public DataSet RunProc(string SQL, DataSet Ds, int StartIndex, int PageSize, string tablename)
        {
            SqlConnection Conn;
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            SqlDataAdapter Da;
            Da = CreateDa(SQL);
            try
            {
                Da.Fill(Ds, StartIndex, PageSize, tablename);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            Dispose(Conn);
            return Ds;
        }

        /// <summary>
        /// 检验是否存在数据
        /// @auther lyq
        /// @date   2010.6.18
        /// </summary>
        /// <returns></returns>
        public bool ExistDate(string SQL)
        {
            SqlConnection Conn;
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            SqlDataReader Dr;
            Dr = CreateCmd(SQL, Conn).ExecuteReader();
            if (Dr.Read())
            {
                Dispose(Conn);
                return true;
            }
            else
            {
                Dispose(Conn);
                return false;
            }
        }

        /// <summary>
        /// 返回SQL语句执行结果的第一行第一列
        /// @auther lyq
        /// @date   2010.6.18
        /// </summary>
        /// <returns>字符串</returns>
        public string ReturnValue(string SQL)
        {
            SqlConnection Conn;
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            string result;
            SqlDataReader Dr;
            try
            {
                Dr = CreateCmd(SQL, Conn).ExecuteReader();
                if (Dr.Read())
                {
                    result = Dr[0].ToString();
                    Dr.Close();
                }
                else
                {
                    result = "";
                    Dr.Close();
                }
            }
            catch
            {
                throw new Exception(SQL);
            }
            Dispose(Conn);
            return result;
        }

        /// <summary>
        /// 返回SQL语句第一列,第ColumnI列,
        /// @auther lyq
        /// @date   2010.6.18
        /// </summary>
        /// <returns>字符串</returns>
        public string ReturnValue(string SQL, int ColumnI)
        {
            SqlConnection Conn;
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            string result;
            SqlDataReader Dr;
            try
            {
                Dr = CreateCmd(SQL, Conn).ExecuteReader();
            }
            catch
            {
                throw new Exception(SQL);
            }
            if (Dr.Read())
            {
                result = Dr[ColumnI].ToString();
            }
            else
            {
                result = "";
            }
            Dr.Close();
            Dispose(Conn);
            return result;
        }

        /// <summary>
        /// 生成一个存储过程使用的sqlcommand.
        /// @auther lyq
        /// @date   2010.6.18
        /// </summary>
        /// <param name="procName">存储过程名.</param>
        /// <param name="prams">存储过程入参数组.</param>
        /// <returns>sqlcommand对象.</returns>
        public SqlCommand CreateCmd(string procName, SqlParameter[] prams)
        {
            SqlConnection Conn;
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            SqlCommand Cmd = new SqlCommand(procName, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    if (parameter != null)
                    {
                        Cmd.Parameters.Add(parameter);
                    }
                }
            }
            return Cmd;
        }

        /// <summary>
        /// 为存储过程生成一个SqlCommand对象
        /// @auther lyq
        /// @date   2010.6.22
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">存储过程参数</param>
        /// <returns>SqlCommand对象</returns>
        private SqlCommand CreateCmd(string procName, SqlParameter[] prams, SqlDataReader Dr)
        {
            SqlConnection Conn;
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            SqlCommand Cmd = new SqlCommand(procName, Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    Cmd.Parameters.Add(parameter);
            }
            Cmd.Parameters.Add(
             new SqlParameter("ReturnValue", SqlDbType.Int, 4,
             ParameterDirection.ReturnValue, false, 0, 0,
             string.Empty, DataRowVersion.Default, null));

            return Cmd;
        }

        /// <summary>
        /// 运行存储过程,返回.
        /// @auther lyq
        /// @date   2010.6.23
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">存储过程参数</param>
        /// <param name="dataReader">SqlDataReader对象</param>
        public void RunProc(string procName, SqlParameter[] prams, SqlDataReader Dr)
        {

            SqlCommand Cmd = CreateCmd(procName, prams, Dr);
            Dr = Cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return;
        }

        /// <summary>
        /// 运行存储过程,返回
        /// @auther lyq
        /// @date   2010.6.24
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">存储过程参数</param>
        public string RunProc(string procName, SqlParameter[] prams)
        {
            SqlDataReader Dr;
            SqlCommand Cmd = CreateCmd(procName, prams);
            Dr = Cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            if (Dr.Read())
            {
                return Dr.GetValue(0).ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 运行存储过程,返回dataset.
        /// @auther lyq
        /// @date   2010.6.27
        /// </summary>
        /// <param name="procName">存储过程名.</param>
        /// <param name="prams">存储过程入参数组.</param>
        /// <returns>dataset对象.</returns>
        public DataSet RunProc(string procName, SqlParameter[] prams, DataSet Ds)
        {
            SqlCommand Cmd = CreateCmd(procName, prams);
            SqlDataAdapter Da = new SqlDataAdapter(Cmd);
            try
            {
                Da.Fill(Ds);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Ds;
        }


        /// <summary>
        /// 运行存储过程,返回dataset.
        /// @auther lyq
        /// @date   2010.6.27
        /// </summary>
        /// <param name="procName">procName.</param>
        /// <param name="prams">prams.</param>
        /// <returns>dataset.</returns>
        public DataSet RunProcess(string procName, SqlParameter[] prams)
        {
            SqlCommand Cmd = CreateCmd(procName, prams);
            SqlDataAdapter Da = new SqlDataAdapter(Cmd);
            DataSet Ds = new DataSet();
            try
            {
                Da.Fill(Ds);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Ds;
        }

    }
    //-------------------------------------------------------------------------------
    /// 
    /// IniFile 的摘要说明。
    /// 
    public class IniFile
    {
        private string FFileName;
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileInt(
        string lpAppName,
        string lpKeyName,
        int nDefault,
        string lpFileName
        );
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
        string lpAppName,
        string lpKeyName,
        string lpDefault,
        StringBuilder lpReturnedString,
        int nSize,
        string lpFileName
        );
        [DllImport("kernel32.dll")]
        private static extern bool WritePrivateProfileString(
        string lpAppName,
        string lpKeyName,
        string lpString,
        string lpFileName
        );
        public IniFile(string filename)
        {
            FFileName = filename;
        }
        public int ReadInt(string section, string key, int def)
        {
            return GetPrivateProfileInt(section, key, def, FFileName);
        }
        public string ReadString(string section, string key, string def)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, temp, 1024, FFileName);
            return temp.ToString();
        }
        public void WriteInt(string section, string key, int iVal)
        {
            WritePrivateProfileString(section, key, iVal.ToString(), FFileName);
        }
        public void WriteString(string section, string key, string strVal)
        {
            WritePrivateProfileString(section, key, strVal, FFileName);
        }
        public void DelKey(string section, string key)
        {
            WritePrivateProfileString(section, key, null, FFileName);
        }
        public void DelSection(string section)
        {
            WritePrivateProfileString(section, null, null, FFileName);
        }
    }
}
