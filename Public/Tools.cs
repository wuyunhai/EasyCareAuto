using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.SocketService
{
    /// <summary>
    /// 工具帮助类
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// 计算时间差
        /// </summary>
        /// <param name="starTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="action">取值类别,d=天，h=小时，m=分钟，s=秒，ad=总天数，ah=总小时，am=总分钟，as=总秒</param>
        /// <returns></returns>
        public static double DateTimeDiff(DateTime starTime, DateTime endTime, string action)
        {
            if (endTime < starTime)
                return 0;
            TimeSpan ts = endTime - starTime;
            switch (action)
            {
                case "d"://天
                    return ts.Days;
                case "h"://小时
                    return ts.Hours;
                case "m": //分钟
                    return ts.Minutes;
                case "s"://秒
                    return ts.Seconds;
                case "ad"://总天数
                    return ts.TotalDays;
                case "ah"://总小时
                    return ts.TotalHours;
                case "am"://总分钟
                    return ts.TotalMinutes;
                case "as": //总秒
                    return ts.TotalSeconds;
                default:
                    return 0;
            }
        }
    }
}
