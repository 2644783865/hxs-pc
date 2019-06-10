using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace MaSoft.Code
{
    public static class SignHelper
    {
        #region 获取带有sign的请求参数

        /// <summary>
        /// 获取带有sign的请求参数
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetPostData(SignData inputObj,string key)
        {

            inputObj.SetValue("sign", inputObj.GetSign(key));
            return inputObj.ToUrlAll();
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSign(SignData inputObj, string key)
        {
            return inputObj.GetSign(key);
        }


        #endregion
    }

    public class SignData
    {
        #region 验证

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="key">加密KEY</param>
        /// <returns>签名, sign字段不参加签名</returns>
        public string GetSign(string key)
        {
            var str = ToUrl();
            str += "&key=" + key;
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString().ToUpper();//所有字符转为大写
        }

        #endregion

        #region 内部方法

        /// <summary>
        /// 采用排序的Dictionary的好处是方便对数据包进行签名，不用再签名之前再做一次排序
        /// </summary>
        private readonly SortedDictionary<string, object> _mValues = new SortedDictionary<string, object>();

        /// <summary>
        /// 设置某个字段的值
        /// </summary>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        public void SetValue(string key, object value)
        {
            _mValues[key] = value;
        }

        /// <summary>
        /// Dictionary格式转化成url参数格式
        /// </summary>
        /// <returns>url格式串, 该串不包含sign字段值</returns>
        public string ToUrl()
        {
            var buff = "";
            foreach (var pair in _mValues)
            {
                if (pair.Value == null)
                {
                    throw new Exception("内部含有值为null的字段!");
                }

                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }

        /// <summary>
        /// Dictionary全部转化成url参数格式
        /// </summary>
        /// <returns>url格式串,包含全部字段值</returns>
        public string ToUrlAll()
        {
            var buff = "";
            foreach (var pair in _mValues)
            {
                if (pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }

        /// <summary>
        /// Dictionary格式化成Json
        /// </summary>
        /// <returns>json串数据</returns>
        public string ToJson()
        {
            return _mValues.ToJson();
        }

        /// <summary>
        /// values格式化成能在Web页面上显示的结果
        /// </summary>
        /// <returns></returns>
        public string ToPrintString()
        {
            var str = "";
            foreach (var pair in _mValues)
            {
                if (pair.Value == null)
                {
                    throw new Exception("内部含有值为null的字段!");
                }
                str += pair.Key + " = " + pair.Value + "<br>";
            }
            return str;
        }

        /// <summary>
        /// 获取Dictionary
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<string, object> GetValues()
        {
            return _mValues;
        }

        #endregion

    }

}
