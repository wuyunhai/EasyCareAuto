using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.ServiceProcess;
using SuperSocket.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketEngine;
using System.Windows.Forms;
using YUN.Framework.Commons;
using System.Diagnostics; 

namespace MES.SocketService
{
    static partial class Program
    {
        /// <summary>
        /// 命令集合
        /// </summary>
        private static Dictionary<string, ControlCommand> m_CommandHandlers = new Dictionary<string, ControlCommand>(StringComparer.OrdinalIgnoreCase);

        private static bool setConsoleColor;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        { 
            //获取运行的软件进程  
            Process instance = StartupHelper.RunningInstance();
            if (instance == null)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                GlobalData.RUN_TYPE = "w";
                Splasher.Show(typeof(frmSplash));
                Application.Run(new Frm_Main());

            }
            else
            {
                //如果已经存在进程，那么提示信息，并切换到存在的界面    
                StartupHelper.HandleRunningInstance(instance, "系统已经在运行！");
            }
        }

        /// <summary>
        /// 主入口
        /// </summary>
        /// <param name="args"></param>
        //[STAThread]
        //static void Main(string[] args)
        //{
        //    RunMain(args);

        //}

        private static void RunMain(string[] args)
        {
            //如果是运行在Linux上，则改变脚本的执行
            if (Platform.IsMono && Path.DirectorySeparatorChar == '/')
                ChangeScriptExecutable();

            if ((!Platform.IsMono && !Environment.UserInteractive) // windows service
                 || (Platform.IsMono && !AppDomain.CurrentDomain.FriendlyName.Equals(Path.GetFileName(Assembly.GetEntryAssembly().CodeBase))))//MonoService
            {
                RunAsService();
                return;
            }


            string exeArg = string.Empty;
            if (args == null || args.Length < 1)
            {
                //Console.WriteLine(EnumHelper.GetMemberValue<CommunicationCode>(CommunicationCode.Ask.ToString()));
                //Console.WriteLine(EnumHelper.GetMemberName<StationCode>("0101"));

                Console.WriteLine("Welcome to MES SocketService!");
                Console.WriteLine("请输入以继续...");
                Console.WriteLine("-[r]: 以控制台程序运行;");
                Console.WriteLine("-[w]: 以windows 窗体程序运行;");
                Console.WriteLine("-[i]: 以windows 服务运行;");
                Console.WriteLine("-[u]: 从windows 服务中卸载该程序");

                while (true)
                {
                    exeArg = Console.ReadKey().KeyChar.ToString();
                    GlobalData.RUN_TYPE = exeArg;
                    Console.WriteLine();

                    if (Run(exeArg, null))
                        break;
                }
            }
            else
            {
                exeArg = args[0];

                if (!string.IsNullOrEmpty(exeArg))
                    exeArg = exeArg.TrimStart('-');

                Run(exeArg, args);
            }
        }

        /// <summary>
        /// 初始化命令逻辑判断
        /// </summary>
        /// <param name="exeArg"></param>
        /// <param name="startArgs"></param>
        /// <returns></returns>
        private static bool Run(string exeArg, string[] startArgs)
        {
            switch (exeArg.ToLower())
            {
                case ("i"):
                    SelfInstaller.InstallMe();
                    Console.ReadKey();
                    return true;

                case ("u"):
                    SelfInstaller.UninstallMe();
                    Console.ReadKey();
                    return true;

                case ("r"):
                    RunAsConsole();
                    return true;

                case ("c"):
                    RunAsController(startArgs);
                    return true;

                case ("w"):
                    RunAsWindowsForm(startArgs);
                    return true;

                default:
                    Console.WriteLine("无效的参数!");
                    return false;
            }

        }

        private static void RunAsWindowsForm(string[] startArgs)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_Main());
        }


        /// <summary>
        /// 以控制台形式运行Socket服务
        /// </summary>
        static void RunAsConsole()
        {
            Console.WriteLine("Welcome to SuperSocket SocketService!");

            CheckCanSetConsoleColor();

            Console.WriteLine("初始化...");

            var bootstrap = BootstrapFactory.CreateBootstrap();
            if (!bootstrap.Initialize())
            {
                SetConsoleColor(ConsoleColor.Red);
                Console.WriteLine("初始化失败");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("启动中...");

            var result = bootstrap.Start();
            Console.WriteLine("-------------------------------------------------------------------");
            foreach (var server in bootstrap.AppServers)
            {
                if (server.State == ServerState.Running)
                {
                    SetConsoleColor(ConsoleColor.Green);
                    Console.WriteLine("- {0} 运行中", server.Name);
                }
                else
                {
                    SetConsoleColor(ConsoleColor.Red);
                    Console.WriteLine("- {0} 启动失败", server.Name);
                }
            }

            Console.ResetColor();
            Console.WriteLine("-------------------------------------------------------------------");

            switch (result)
            {
                case StartResult.Failed:
                    SetConsoleColor(ConsoleColor.Red);
                    Console.WriteLine("无法启动服务，更多错误信息请查看日志");
                    Console.ReadKey();
                    return;
                case StartResult.None:
                    SetConsoleColor(ConsoleColor.Red);
                    Console.WriteLine("没有服务器配置，请检查你的配置！");
                    Console.ReadKey();
                    return;
                case StartResult.PartialSuccess:
                    SetConsoleColor(ConsoleColor.Red);
                    Console.WriteLine("一些服务启动成功，但是还有一些启动失败，更多错误信息请查看日志");
                    break;
                case StartResult.Success:
                    Console.WriteLine("服务已经开始！");
                    break;
            }

            Console.ResetColor();
            Console.WriteLine("输入'quit'以停止服务");

            RegisterCommands();

            ReadConsoleCommand(bootstrap);

            bootstrap.Stop();

            Console.WriteLine("The SuperSocket ServiceEngine has been stopped!");

        }

        private static void RunAsController(string[] arguments)
        {
            if (arguments == null || arguments.Length < 2)
            {
                Console.WriteLine("无效的参数！");
                return;
            }

            var config = ConfigurationManager.GetSection("superSocket") as IConfigurationSource;
            if (config == null)
            {
                Console.WriteLine("superSocket不存在于Section节点");
                return;
            }

            var clientChannel = new IpcClientChannel();
            ChannelServices.RegisterChannel(clientChannel, false);

            IBootstrap bootstrap = null;
            try
            {
                var remoteBootstrapUri = $"ipc://SuperSocket.Bootstrap[{Math.Abs(AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar).GetHashCode())}]/Bootstrap.rem";
                bootstrap = (IBootstrap)Activator.GetObject(typeof(IBootstrap), remoteBootstrapUri);


            }
            catch (RemotingException)
            {
                if (config.Isolation != IsolationMode.Process)
                {
                    Console.WriteLine("错误：该SuperSocket引擎尚未启动！");
                    return;
                }
            }

            RegisterCommands();

            var cmdName = arguments[1];
            ControlCommand cmd;
            if (!m_CommandHandlers.TryGetValue(cmdName, out cmd))
            {
                Console.WriteLine("未知命令");
                return;
            }

            try
            {
                if (cmd.Handler(bootstrap, arguments.Skip(1).ToArray()))
                    Console.WriteLine("Ok");
            }
            catch (Exception e)
            {
                Console.WriteLine("错误. " + e.Message);
            }
        }


        /// <summary>
        /// 递归以接收命令
        /// </summary>
        /// <param name="bootstrap"></param>
        static void ReadConsoleCommand(IBootstrap bootstrap)
        {
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                ReadConsoleCommand(bootstrap);
                return;
            }

            if ("quit".Equals(line, StringComparison.OrdinalIgnoreCase))
                return;

            var cmdArray = line.Split(' ');

            ControlCommand cmd;
            if (!m_CommandHandlers.TryGetValue(cmdArray[0], out cmd))
            {
                Console.WriteLine("未知命令");
                ReadConsoleCommand(bootstrap);
                return;
            }

            try
            {
                if (cmd.Handler(bootstrap, cmdArray))
                    Console.WriteLine("Ok");
            }
            catch (Exception e)
            {
                Console.WriteLine("错误. " + e.Message + Environment.NewLine + e.StackTrace);
            }

            ReadConsoleCommand(bootstrap);
        }


        /// <summary>
        /// 改变控制台显示的颜色
        /// </summary>
        static void CheckCanSetConsoleColor()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;//前景色
                Console.ResetColor();
                setConsoleColor = true;
            }
            catch
            {
                setConsoleColor = false;
            }
        }

        /// <summary>
        /// 改变控制台前景色
        /// </summary>
        /// <param name="color"></param>
        private static void SetConsoleColor(ConsoleColor color)
        {
            if (setConsoleColor)
                Console.ForegroundColor = color;
        }

        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="handler"></param>
        private static void AddCommand(string name, string description, Func<IBootstrap, string[], bool> handler)
        {
            var command = new ControlCommand
            {
                Name = name,
                Description = description,
                Handler = handler
            };

            m_CommandHandlers.Add(command.Name, command);
        }

        /// <summary>
        /// 注册命令
        /// </summary>
        private static void RegisterCommands()
        {
            AddCommand("List", "List all server instances", ListCommand);
            AddCommand("Start", "Start a server instance: Start {ServerName}", StartCommand);
            AddCommand("Stop", "Stop a server instance: Stop {ServerName}", StopCommand);
        }


        /// <summary>
        /// 启动服务命令
        /// </summary>
        /// <param name="bootstrap"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        static bool StartCommand(IBootstrap bootstrap, string[] arguments)
        {
            var name = arguments[1];
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("服务名称为空");
                return false;
            }

            var server = bootstrap.AppServers.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (server == null)
            {
                Console.WriteLine("服务不存在");
                return false;
            }

            server.Start();

            return true;
        }

        /// <summary>
        /// 停止服务命令
        /// </summary>
        /// <param name="bootstrap"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        static bool StopCommand(IBootstrap bootstrap, string[] arguments)
        {
            var name = arguments[1];
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("服务名称为空");
                return false;
            }

            var server = bootstrap.AppServers.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (server == null)
            {
                Console.WriteLine("服务不存在");
                return false;
            }

            server.Stop();

            return true;
        }

        /// <summary>
        /// 服务列表
        /// </summary>
        /// <param name="bootstrap"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        static bool ListCommand(IBootstrap bootstrap, string[] arguments)
        {
            foreach (var s in bootstrap.AppServers)
            {
                var processInfo = s as IProcessServer;

                if (processInfo != null && processInfo.ProcessId > 0)
                    Console.WriteLine("{0}[PID:{1}] - {2}", s.Name, processInfo.ProcessId, s.State);
                else
                    Console.WriteLine("{0} - {1} - {2}", s.Name, s.State, s.SessionCount);
            }

            return false;
        }

        /// <summary>
        /// 如果是运行在Linux上，则改变脚本执行
        /// </summary>
        static void ChangeScriptExecutable()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "supersocket.sh");

            try
            {
                if (!File.Exists(filePath))
                    return;

                File.SetAttributes(filePath, (FileAttributes)((uint)File.GetAttributes(filePath) | 0x80000000));
            }
            catch
            {

            }
        }

        /// <summary>
        /// 如果本身就在windows service中或Linux中运行，则重新注册
        /// </summary>
        static void RunAsService()
        {
            ServiceBase[] servicesToRun;

            servicesToRun = new ServiceBase[] { new MainService() };

            ServiceBase.Run(servicesToRun);
        }
    }


}
