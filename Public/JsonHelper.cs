using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MES.SocketService
{
    public static class JsonHelper
    {
        //Json序列化设置对象
        private static JsonSerializerSettings JsonSettings;

        private const string EmptyJson = "[]";

        /// <summary>
        /// 构造函数
        /// </summary>
        static JsonHelper()
        {
            var datetimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            JsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            JsonSettings.Converters.Add(datetimeConverter);
        }

        /// <summary>
        /// 序列化对象到JSON格式的字符串
        /// </summary>
        /// <param name="obj">任意对象</param>
        /// <param name="format">指定格式设置选项</param>
        /// <param name="jsonSettings">指定序列化设置</param>
        /// <returns>标准的JSON格式的字符串</returns>
        public static string Serialize(object obj, Formatting format, JsonSerializerSettings jsonSettings)
        {
            try
            {
                return obj == null ? string.Empty : JsonConvert.SerializeObject(obj, format, jsonSettings ?? JsonSettings);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 序列化对象到JSON格式的字符串（重载）
        /// </summary>
        public static string Serialize(object obj, Formatting format)
        {
            return Serialize(obj, format, JsonSettings);
        }

        /// <summary>
        /// 序列化对象到JSON格式的字符串（重载）
        /// </summary>
        public static string Serialize(object obj)
        {
            return Serialize(obj, Formatting.None, JsonSettings);
        }

        /// <summary>
        /// 序列化对象集合到JSON格式的字符串
        /// </summary>
        /// <param name="obj">对象集合</param>
        /// <param name="format">指定格式设置选项</param>
        /// <param name="jsonSettings">指定序列化设置</param>
        /// <returns>标准的JSON格式的字符串</returns>
        public static string SerializeList<T>(IEnumerable<T> list, Formatting format, JsonSerializerSettings jsonSettings) where T : class
        {
            try
            {
                return list == null ? EmptyJson : JsonConvert.SerializeObject(list, format, jsonSettings ?? JsonSettings);
            }
            catch (Exception)
            {
                return EmptyJson;
            }
        }

        /// <summary>
        /// 序列化对象集合到JSON格式的字符串（重载）
        /// </summary>
        public static string SerializeList<T>(IEnumerable<T> list, Formatting format) where T : class
        {
            return SerializeList<T>(list, format, JsonSettings);
        }

        /// <summary>
        /// 序列化对象集合到JSON格式的字符串（重载）
        /// </summary>
        public static string SerializeList<T>(IEnumerable<T> list) where T : class
        {
            return SerializeList<T>(list, Formatting.None, JsonSettings);
        }

        
        /// <summary>
        /// 反序列化JSON数据到对象
        /// </summary>
        /// <param name="json">需要反序列化的JSON字符串</param>
        /// <param name="format">格式设置选项</param>
        /// <param name="jsonSettings">指定序列化设置</param>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <returns>指定类型的对象</returns>
        public static T Deserialize<T>(string json, JsonSerializerSettings jsonSettings) where T : class
        {
            T result;
            if (jsonSettings == null)
            {
                jsonSettings = JsonSettings;
            }
            try
            {
                result = string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json, jsonSettings);
            }
            catch (JsonSerializationException)  //在发生异常后，再以集合的方式重试一次.
            {
                try
                {
                    var array = JsonConvert.DeserializeObject<IEnumerable<T>>(json, jsonSettings);
                    result = array.FirstOrDefault();
                }
                catch (Exception)
                {
                    result = default(T);
                }
            }
            catch (Exception)
            {
                result = default(T);
            }
            return result;
        }

        /// <summary>
        /// 反序列化JSON数据到对象（重载）
        /// </summary>
        public static T Deserialize<T>(string json) where T : class
        {
            return Deserialize<T>(json, JsonSettings);
        }


        /// <summary>
        /// 反序列化JSON数据到对象集合
        /// </summary>
        /// <param name="json">需要反序列化的JSON字符串</param>
        /// <param name="format">格式设置选项</param>
        /// <param name="jsonSettings">指定序列化设置</param>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <returns>对象集合</returns>
        public static IEnumerable<T> DeserializeToList<T>(string json, JsonSerializerSettings jsonSettings) where T : class
        {
            IEnumerable<T> result;
            if (jsonSettings == null)
            {
                jsonSettings = JsonSettings;
            }
            try
            {
                result = string.IsNullOrWhiteSpace(json) ? null : JsonConvert.DeserializeObject<IEnumerable<T>>(json, jsonSettings);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        /// <summary>
        /// 反序列化JSON数据到对象集合(重载)
        /// </summary>
        public static IEnumerable<T> DeserializeToList<T>(string json, Formatting format) where T : class
        {
            return DeserializeToList<T>(json, JsonSettings);
        }

        /// <summary>
        /// 反序列化JSON数据到对象集合(重载)
        /// </summary>
        public static IEnumerable<T> DeserializeToList<T>(string json) where T : class
        {
            return DeserializeToList<T>(json, JsonSettings);
        }
  
    #region 扩展方法

        /// <summary>
        /// 序列化对象到JSON格式的字符串（重载）
        /// </summary>
        public static string ToJson(this object obj)
        {
            return Serialize(obj, Formatting.None, JsonSettings);
        }

        /// <summary>
        /// 序列化对象集合到JSON格式的字符串（重载）
        /// </summary>
        public static string ToJson<T>(this IEnumerable<T> lst) where T : class
        {
            return SerializeList<T>(lst, Formatting.None, JsonSettings);
        }

        /// <summary>
        /// 反序列化JSON数据到对象（重载）
        /// </summary>
        public static T ToObj<T>(this string json) where T : class
        {
            return Deserialize<T>(json);
        }

        /// <summary>
        /// 反序列化JSON数据到对象集合
        /// </summary>
        public static IEnumerable<T> ToObjList<T>(this string json) where T : class
        {
            return DeserializeToList<T>(json);
        }

    #endregion
    }
}
