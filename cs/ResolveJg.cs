using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace YjScanning.cs
{
    class ResolveJg
    {
        public String ReadTxt(string path)
        {

            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            StringBuilder line = new StringBuilder() ;
            while (sr.ReadLine() != null)
            {
                line.Append(sr.ReadLine());
            }
            return line.ToString();
        }
        
        public void resolve() 
        {
            String jg = "C:\\Users\\crystal\\Desktop\\jg.txt";
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(jg);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(JArray));
            JArray ye = o as JArray;
            //每一页
            foreach (JObject jo in ye) 
            {
                //每一题型块
                if ("kgt".Equals(jo["type"])||"dx".Equals(jo["type"])|| "singleChoise".Equals(jo["type"])|| "multiChoise".Equals(jo["type"])) 
                {
                    StringReader xiaokuaiReader = new StringReader(jo["data"].ToString());
                    JArray xiaokuai = serializer.Deserialize(new JsonTextReader(xiaokuaiReader), typeof(JArray)) as JArray;
                    foreach (JObject l in xiaokuai) 
                    {
                        Console.WriteLine(l["choiseNum"]);
                    }
                }
            }
        }
    }
}
