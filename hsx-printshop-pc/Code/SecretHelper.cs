﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace MaSoft.Code
{
    public static class SecretHelper
    {
        #region MD5编码

        /// <summary>
        /// 将字符串使用MD5算法解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncodeMd5String(this string input)
        {
            if (input.IsNullOrEmpty())
                return input;

            var md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.Default.GetBytes(input));
            return BitConverter.ToString(result).Replace("-", "");
        }

        /// <summary>
        /// 获得32位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_32(this string input)
        {
            var md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));
            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获得16位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_16(this string input)
        {
            return GetMD5_32(input).Substring(8, 16);
        }

        /// <summary>
        /// 获得8位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_8(this string input)
        {
            return GetMD5_32(input).Substring(8, 8);
        }

        /// <summary>
        /// 获得4位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_4(this string input)
        {
            return GetMD5_32(input).Substring(8, 4);
        }

        /// <summary>
        /// 添加MD5的前缀，便于检查有无篡改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string AddMd5Profix(this string input)
        {
            return GetMD5_4(input) + input;
        }

        /// <summary>
        /// 移除MD5的前缀
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveMd5Profix(this string input)
        {
            return input.Substring(4);
        }

        /// <summary>
        /// 验证MD5前缀处理的字符串有无被篡改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ValidateValue(this string input)
        {
            var res = false;
            if (input.Length < 4) return false;
            var tmp = input.Substring(4);
            if (input.Substring(0, 4) == GetMD5_4(tmp))
            {
                res = true;
            }
            return res;
        }

        #endregion

        #region SHA256编码

        /// <summary>
        /// SHA256编码
        /// </summary>
        /// <param name="sourceString">字符串</param>
        /// <returns></returns>
        public static string Sha256Encrypt(this string sourceString)
        {
            var bytes = Encoding.UTF8.GetBytes(sourceString);
            SHA256 sha256 = new SHA256Managed();
            var tmpByte = sha256.ComputeHash(bytes);
            sha256.Clear();
            return Convert.ToBase64String(tmpByte);
        }

        #endregion

        #region base64算法编码

        /// <summary> 
        /// 将字符串使用base64算法编码 
        /// </summary> 
        /// <param name="encodingName">编码类型（编码名称） 
        /// * 代码页 名称 
        /// * 1200 "UTF-16LE"、"utf-16"、"ucs-2"、"unicode"或"ISO-10646-UCS-2" 
        /// * 1201 "UTF-16BE"或"unicodeFFFE" 
        /// * 1252 "windows-1252"
        /// * 65000 "utf-7"、"csUnicode11UTF7"、"unicode-1-1-utf-7"、"unicode-2-0-utf-7"、"x-unicode-1-1-utf-7"或"x-unicode-2-0-utf-7" 
        /// * 65001 "utf-8"、"unicode-1-1-utf-8"、"unicode-2-0-utf-8"、"x-unicode-1-1-utf-8"或"x-unicode-2-0-utf-8" 
        /// * 20127 "us-ascii"、"us"、"ascii"、"ANSI_X3.4-1968"、"ANSI_X3.4-1986"、"cp367"、"csASCII"、"IBM367"、"iso-ir-6"、"ISO646-US"或"ISO_646.irv:1991" 
        /// * 54936 "GB18030"
        /// </param>
        /// <param name="source">待加密的字符串</param>
        /// <returns>加密后的字符串</returns> 
        public static string EncodeBase64String(this string source, string encodingName = "UTF-8")
        {
            byte[] bytes = Encoding.GetEncoding(encodingName).GetBytes(source);
            return Convert.ToBase64String(bytes);
        }

        #endregion

        #region base64算法解码

        /// <summary> 
        /// 将字符串使用base64算法解码
        /// </summary> 
        /// <param name="encodingName">编码类型</param> 
        /// <param name="base64String">已用base64算法加密的字符串</param> 
        /// <returns>解密后的字符串</returns> 
        public static string DecodeBase64String(this string base64String, string encodingName = "UTF-8")
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64String);
                return Encoding.GetEncoding(encodingName).GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region 使用 指定密钥字符串 加密/解密string

        /// <summary>
        /// 使用指定密钥字符串加密string
        /// </summary>
        /// <param name="original">原始文字</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static string Encrypt(this string original, string key)
        {
            var buff = Encoding.Default.GetBytes(original);
            var kb = Encoding.Default.GetBytes(key);
            return Convert.ToBase64String(Encrypt(buff, kb));
        }

        /// <summary>
        /// 使用指定密钥字符串解密string
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static string Decrypt(this string encrypted, string key)
        {
            return Decrypt(encrypted, key, Encoding.Default);
        }

        /// <summary>
        /// 使用指定密钥字符串解密string,返回指定编码方式明文
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码方案</param>
        /// <returns>明文</returns>
        public static string Decrypt(this string encrypted, string key, Encoding encoding)
        {
            var buff = Convert.FromBase64String(encrypted);
            var kb = Encoding.Default.GetBytes(key);
            return encoding.GetString(Decrypt(buff, kb));
        }

        #endregion

        #region 使用 缺省密钥字符串 加密/解密string

        /// <summary>
        /// 使用缺省密钥字符串加密string
        /// 缺省密钥：masoft.cn
        /// </summary>
        /// <param name="original">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(this string original)
        {
            return Encrypt(original, "masoft.cn");
        }

        /// <summary>
        /// 使用缺省密钥字符串解密string
        /// 缺省密钥：masoft.cn
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string encrypted)
        {
            return Decrypt(encrypted, "masoft.cn", Encoding.Default);

        }

        #endregion

        #region  使用 给定密钥 加密/解密/byte[]

        /// <summary>
        /// byte[]生成MD5摘要
        /// </summary>
        /// <param name="original">数据源</param>
        /// <returns>摘要</returns>
        public static byte[] MakeMd5(this byte[] original)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            var keyhash = hashmd5.ComputeHash(original);
            return keyhash;
        }

        /// <summary>
        /// 使用给定密钥加密byte[]
        /// </summary>
        /// <param name="original">明文byte[]</param>
        /// <param name="key">密钥byte[]</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(this byte[] original, byte[] key)
        {
            var des = new TripleDESCryptoServiceProvider
            {
                Key = MakeMd5(key),
                Mode = CipherMode.ECB
            };
            return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
        }

        /// <summary>
        /// 使用给定密钥解密数据byte[]
        /// </summary>
        /// <param name="encrypted">密文byte[]</param>
        /// <param name="key">密钥byte[]</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(this byte[] encrypted, byte[] key)
        {
            var des = new TripleDESCryptoServiceProvider
            {
                Key = MakeMd5(key),
                Mode = CipherMode.ECB
            };
            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        #endregion

        #region 使用 缺省密钥字符串 加密/解密/byte[]

        /// <summary>
        /// 使用缺省密钥字符串加密
        /// 缺省密钥字符串：masoft.cn
        /// </summary>
        /// <param name="original">原始数据</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(this byte[] original)
        {
            var key = Encoding.Default.GetBytes("masoft.cn");
            return Encrypt(original, key);
        }

        /// <summary>
        /// 使用缺省密钥字符串解密byte[]
        /// 缺省密钥字符串：masoft.cn
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(this byte[] encrypted)
        {
            var key = Encoding.Default.GetBytes("masoft.cn");
            return Decrypt(encrypted, key);
        }

        #endregion
    }
}
