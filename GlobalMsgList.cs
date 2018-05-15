using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.SocketService
{
    /// <summary>
    /// 异步线程向客户端重发消息
    /// </summary>
    public class GlobalMsgList
    {
        private static List<MsgEntity> MsgList = new List<MsgEntity>();

        /// <summary>
        /// 添加一条发送失败的消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="session_id"></param>
        public static void AddMsg(Guid guid, string msg, string open_id, string sn, AppSession<MesSession> session)
        {
            MsgList.Add(new MsgEntity() { msg = msg, session = session, guid = guid, add_time = DateTime.Now, open_id = open_id, sn = sn });
        }

        public static void RemoveMsg(Guid guid)
        {
            var query = from c in MsgList where c.guid == guid select c;
            if (query != null)
            {
                MsgList.Remove(query.First());
                //LogHelper.WriteLog("关闭连接:" + query.First().session);
            }
        }

        /// <summary>
        /// 获取所有未确认成功的消息
        /// </summary>
        /// <returns></returns>
        public static List<MsgEntity> GetList()
        {
            return MsgList;
        }

    }

    /// <summary>
    /// 待重新发送的数据库对象
    /// </summary>
    public class MsgEntity
    {

        /// <summary>
        /// 用户标识
        /// </summary>
        public string open_id { get; set; }

        /// <summary>
        /// 消息唯一标识
        /// </summary>
        public Guid guid { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 机器码
        /// </summary>
        public string sn { get; set; }

        /// <summary>
        /// 会话实体
        /// </summary>
        public AppSession<MesSession> session { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time { get; set; }
    }
}
