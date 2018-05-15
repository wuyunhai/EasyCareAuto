 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.SocketService
{


    /// <summary>
    /// 异步线程发送信息
    /// </summary>
    public class ThreadSendMsg
    {
        public void DoWork()
        {
            while (true)
            {
                try
                {
                    List<MsgEntity> list = GlobalMsgList.GetList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        var session = list[i].session;
                        if (session != null)
                        {
                            var session_client = session.AppServer.GetAllSessions();
                            var client = session_client.Where(c => c.SN == list[i].sn);
                            if (client != null)
                            {
                                foreach (var item in client)
                                {
                                    item.Send(list[i].msg + " " + list[i].guid.ToString() + " " + list[i].open_id + "$");
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                     
                     //("线程循环出错：" + ex.Message + "------------" + DateTime.Now.ToString());
                }

                //线程睡眠1秒
                System.Threading.Thread.Sleep(2000);
            }
        }
    }
}
