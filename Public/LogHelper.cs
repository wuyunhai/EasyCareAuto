using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace MES.SocketService
{
    //显示日志委托
    public delegate void LogDisplayDelegate(string msg);

    //日志记录等级
    public enum LogLevel
    {
        NONE = 0,
        FATAL,
        ERROR,
        WARN,
        INFO,
        DEBUG,
    }

    //用于传递日志信息的辅助类
    public class LogMsg
    {
        //日志记录等级
        public LogLevel Level
        {
            get;
            set;
        }
        //日志记录时间
        public DateTime LogTime
        {
            get;
            set;
        }
        //日志文本
        public string MsgText
        {
            get;
            set;
        }
        //位置信息
        public string Location
        {
            get;
            set;
        }

        //构造函数
        public LogMsg() : this(LogLevel.INFO, "")
        {

        }

        public LogMsg(LogLevel level, string msg)
        {
            Level = level;
            MsgText = msg;
            LogTime = DateTime.Now;
            Location = string.Empty;
        }
    }

    //日志管理类
    public class LogHelper : IDisposable
    {
        //保存日志的文件夹
        private static string logPath = string.Empty;

        //显示日志事件
        public event LogDisplayDelegate OnDisplayLog;
        //日志记录线程状态
        private bool IsStartThread = false;
        //日志路径
        private static string LogPath
        {
            get { return logPath; }
            set { logPath = value; }
        }
        //当前日志等级
        public LogLevel CurLevel
        {
            get;
            set;
        }
        //日志是否带有调用位置信息（通过反射）
        public bool HasLocation
        {
            get;
            set;
        }

        //日志信息队列
        private Queue<LogMsg> logMsgs = null;

        //日志文件的写入流对象
        private StreamWriter sw = null;

        //标识日志记录活动是否启动
        public bool IsStarted
        {
            get;
            private set;
        }

        //单例对象
        private static LogHelper log = null;

        //私有构造函数
        private LogHelper(string path)
        {
            CurLevel = LogLevel.NONE;
            HasLocation = true;
            if (string.IsNullOrEmpty(path))
            {
                logPath = AppDomain.CurrentDomain.BaseDirectory + @"Log\";
            }
            else
            {
                if (path.EndsWith("\\"))
                    logPath = path;
                else
                    logPath = path + "\\";
            }
            //若不存在日志目录，则新建此目录
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
        }

        //获得类的单例实例
        public static LogHelper GetInstence(string path = "")
        {
            try
            {
                if (log == null)
                {
                    log = new LogHelper(path);
                }
                return log;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        //启动日志记录
        public bool StartLog(LogLevel level)
        {
            try
            {
                if (!IsStartThread)
                {
                    logMsgs = new Queue<LogMsg>();
                    CurLevel = level;
                    Thread thread = new Thread(LogRecThread);
                    thread.IsBackground = true;
                    thread.Start();
                }
                if (!IsStarted)
                {
                    string fileName = LogPath + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                    sw = new StreamWriter(fileName, true, Encoding.UTF8);
                    IsStarted = true;
                }
            }
            catch (System.Exception)
            {
                IsStarted = false;
                IsStartThread = false;
            }
            return IsStarted;
        }

        //析构函数
        ~LogHelper()
        {
            Dispose();
        }

        //日志写入线程
        private void LogRecThread()
        {
            IsStartThread = true;
            LogMsg logMsg = new LogMsg();
            while (true)
            {
                //判断队列中是否存在待写入的日志                
                if (logMsgs.Count > 0)
                {
                    lock (logMsgs)
                    {
                        logMsg = logMsgs.Dequeue();
                    }
                    if (logMsg != null)
                    {
                        WriteLogFile(logMsg);
                    }
                }
                else
                {
                    //判断是否已经发出终止日志并关闭的消息
                    if (IsStarted)
                    {
                        Thread.Sleep(10);
                    }
                    else
                    {
                        IsStartThread = false;
                        break;
                    }
                }
            }
        }

        //将日志写入文件
        private void WriteLogFile(LogMsg logMsg)
        {
            string msg = string.Empty;
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(logMsg.LogTime.ToString("[HH:mm:ss,fff] "));
                sb.Append(Enum.GetName(logMsg.Level.GetType(), logMsg.Level) + " >> ");
                sb.Append(logMsg.MsgText + "  ");
                sb.Append(logMsg.Location);
                msg = sb.ToString();
                sw.WriteLine(msg);
                sw.Flush();
                msg = msg + "\r\n";
            }
            catch (System.Exception ex)
            {
                msg = "日志记录出现异常:\r\n" + ex.Message;
            }
            if (OnDisplayLog != null)
                OnDisplayLog(msg);
        }

        //将日志写入文件（每次打开关闭文件流）
        private void WriteLogFile2(LogMsg logMsg)
        {
            if (!IsStarted)
                return;
            //关闭默认的持续流
            if (sw != null)
            {
                sw.Close();
                sw = null;
            }
            string fileName = LogPath + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            try
            {
                using (StreamWriter sw2 = new StreamWriter(fileName, true, Encoding.UTF8))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(logMsg.LogTime.ToString("[HH:mm:ss] "));
                    sb.Append(Enum.GetName(logMsg.Level.GetType(), logMsg.Level) + " >> ");
                    sb.Append(logMsg.MsgText);
                    sw2.WriteLine(sb.ToString());
                    sw2.Flush();
                }
            }
            catch (System.Exception)
            {
            }
        }

        //写日志
        private void LogWrite(LogMsg logMsg)
        {
            if (StartLog(CurLevel) && logMsg != null && CurLevel >= logMsg.Level)
            {
                if (HasLocation)
                {
                    StackTrace st = new StackTrace();
                    MethodBase mb = st.GetFrame(2).GetMethod();
                    logMsg.Location = " (Class: " + mb.ReflectedType.Name + ", Method: " + mb.Name + ")";
                }
                lock (logMsgs)
                {
                    logMsgs.Enqueue(logMsg);
                }
            }
        }

        //写入日志队列（指定文本，采用DEBUG等级）
        public void Debug(string msg, params object[] args)
        {
            if (null != args)
            {
                msg = string.Format(msg, args);
            }
            LogWrite(new LogMsg(LogLevel.DEBUG, msg));
        }

        //写入日志队列（指定文本，采用INFO等级）
        public void Info(string msg, params object[] args)
        {
            if (null != args && args.Length > 0)
            {
                msg = string.Format(msg, args);
            }
            LogWrite(new LogMsg(LogLevel.INFO, msg));
        }

        //写入日志队列（指定文本，采用WARN等级）
        public void Warn(string msg, params object[] args)
        {
            if (null != args)
            {
                msg = string.Format(msg, args);
            }
            LogWrite(new LogMsg(LogLevel.WARN, msg));
        }

        //写入日志队列（指定文本，采用ERROR等级）
        public void Error(string msg, params object[] args)
        {
            if (null != args)
            {
                msg = string.Format(msg, args);
            }
            LogWrite(new LogMsg(LogLevel.ERROR, msg));
        }

        //写入日志队列（指定文本，采用FATAL等级）
        public void Fatal(string msg, params object[] args)
        {
            if (null != args)
            {
                msg = string.Format(msg, args);
            }
            LogWrite(new LogMsg(LogLevel.FATAL, msg));
        }

        //列出目录下所有文件
        public List<string> ListAllLogFiles(string path)
        {
            List<string> fileList = new List<string>();
            try
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    fileList.Clear();
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        if (file.Name.ToLower().Contains(".log"))
                        {
                            fileList.Add(file.Name);
                        }
                    }
                }
                return fileList;
            }
            catch (System.Exception)
            {
                return fileList;
            }
        }

        //将指定天数前的日志文件删除
        public void DelLogFiles(TimeSpan days)
        {
            try
            {
                if (Directory.Exists(logPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(logPath);
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        DateTime dtFile = file.LastWriteTime;
                        if (file.Name.ToLower().Contains(".log"))
                        {
                            if (DateTime.Now - dtFile > days)
                            {
                                File.Delete(file.FullName);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        //将指定的日志文件删除（通过文件全路径）
        public void DelLogFiles(string[] fileList)
        {
            try
            {
                foreach (string file in fileList)
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        //输出指定日志文件内容
        public string OutputLogText(string file)
        {
            string output = "";
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    output = sr.ReadToEnd();
                }
                return output;
            }
            catch (System.Exception)
            {
                return "";
            }
        }

        //销毁日志类对象
        public void Dispose()
        {
            IsStarted = false;
            if (sw != null)
            {
                sw = null;
            }
        }

        //设置当前日志等级
        public void SetLogLevel(LogLevel level)
        {
            CurLevel = level;
        }
    }
}
