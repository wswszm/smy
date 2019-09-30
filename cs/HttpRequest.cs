using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace smy.cs
{
    class HttpRequest
    {
        private static string postUrl(string strURL, Dictionary<String, String> d)
        {
            try
            {
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                //Post请求方式
                request.Method = "POST";
                // 内容类型
                request.ContentType = "application/x-www-form-urlencoded";
                // 参数经过URL编码
                StringBuilder paraUrlCoded = new StringBuilder();
                foreach (var item in d)
                {
                    paraUrlCoded.Append(HttpUtility.UrlEncode(item.Key) + "=" + HttpUtility.UrlEncode(item.Value) + "&");
                }
                if (paraUrlCoded.Length > 0)
                {
                    paraUrlCoded.Remove(paraUrlCoded.Length - 1, 1);
                }
                byte[] payload;
                //将URL编码后的字符串转化为字节
                payload = Encoding.UTF8.GetBytes(paraUrlCoded.ToString());
                //设置请求的 ContentLength 
                request.ContentLength = payload.Length;
                //获得请 求流
                System.IO.Stream writer = request.GetRequestStream();
                //将请求参数写入流
                writer.Write(payload, 0, payload.Length);
                // 关闭请求流
                writer.Close();
                HttpWebResponse response;
                // 获得响应流
                response = (HttpWebResponse)request.GetResponse();
                System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string responseText = myreader.ReadToEnd();
                myreader.Close();
                Console.WriteLine(responseText);
                return responseText;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }

        }


        private static string postUrlJson(string strURL, string jsonParam)
        {
            try
            {
                //地址
                //json参数
                var request = (HttpWebRequest)WebRequest.Create(strURL);
                request.Method = "POST";
                request.ContentType = "application/json;charset=UTF-8";
                byte[] byteData = Encoding.UTF8.GetBytes(jsonParam);
                int length = byteData.Length;
                request.ContentLength = length;
                Stream writer = request.GetRequestStream();
                writer.Write(byteData, 0, length);
                writer.Close();
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
                return responseString.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }

        }
    }
}
