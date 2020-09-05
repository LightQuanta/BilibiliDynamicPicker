using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace DynPicker
{
    class BilibiliAPI
    {
        /// <summary>
        /// 获取转发动态的用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetRepostedUsers(string uid)
        {
            Dictionary<string, string> users = new Dictionary<string, string>();

            //获取转发的API
            string link = "https://api.live.bilibili.com/dynamic_repost/v1/dynamic_repost/view_repost?dynamic_id=" + uid + "&offset=";

            int offset = 0;
            string json = GetWebpage(link + offset.ToString());
            JObject j = JObject.Parse(json);
            if (j["code"].ToString() == "0")
            {
                int count = int.Parse(j["data"]["total_count"].ToString());
                int page = count <= 20 ? 1 : count / 20 + 1;

                //如果没人转发直接返回null
                if (count==0)
                {
                    return null;
                } else
                {
                    //如果有人转发先把第一页给添加进去
                    foreach (JObject rep in j["data"]["comments"].Children())
                    {
                        if (!users.ContainsKey(rep["uname"].ToString()))
                        {
                            users.Add(rep["uname"].ToString(), rep["comment"].ToString());
                        }
                    }
                    //如果只有一页直接返回
                    if (page==1)
                    {
                        return users;    
                    } else
                    {
                        //如果不止一页就尝试获取所有转发
                        while (j["data"]["has_more"].ToString()=="True")
                        {
                            //每次往后获取20个转发
                            offset += 20;
                            //为防止rate limit睡眠0.05s
                            Thread.Sleep(50);
                            string ss = GetWebpage(link + offset.ToString());
                            j = JObject.Parse(ss);
                            foreach (JObject rep in j["data"]["comments"].Children())
                            {
                                if (!users.ContainsKey(rep["uname"].ToString()))
                                {
                                    users.Add(rep["uname"].ToString(), rep["uid"].ToString());
                                }
                            }
                        }
                        return users;
                    }
                }
            } else
            {
                return null;
            }
        }

        /// <summary>
        /// 把JSON格式的回复数据转换成包含用户名和用户id的Dictionary
        /// </summary>
        /// <param name="replies">回复数据</param>
        /// <returns></returns>
        private static Dictionary<string, string> GetReplyDic(JArray replies)
        {
            Dictionary<string, string> users = new Dictionary<string, string>();

            foreach (JObject item in replies.Children())
            {
                //获取一条评论的发送者名称和uid
                string name = item["member"]["uname"].ToString();
                string uid = item["member"]["mid"].ToString();

                //如果id没有重复就添加进辞典
                if (!users.ContainsKey(name))
                {
                    users.Add(name, uid);
                }

                //如果评论被回复再处理一遍回复评论（仅处理前20个，别问为什么，问就是懒）
                if (item["replies"] is JArray)
                {
                    //递归获得回复
                    Dictionary<string, string> rep = GetReplyDic((JArray)item["replies"]);
                    foreach (KeyValuePair<string, string> k in rep)
                    {
                        if (!users.ContainsKey(k.Key))
                        {
                            users.Add(k.Key, k.Value);
                        }
                    }
                }
            }
            return users;
        }

        /// <summary>
        /// 获取评论区所有用户
        /// </summary>
        /// <param name="id">请求的API网址</param>
        /// <returns>包含用户名和用户uid的Dictionary</returns>
        public static Dictionary<string, string> GetCommentedUsers(string id)
        {
            id = GetCommentApiUrl(id);

            Dictionary<string, string> users = new Dictionary<string, string>();

            //获取动态第一页信息
            string json = GetReplyJson(id, "1");
            JObject j = JObject.Parse(json);

            //如果成功获取评论区信息
            if (j["code"].ToString() == "0")
            {
                //获取评论页数
                int pages = int.Parse(j["data"]["page"]["num"].ToString());
                int count = int.Parse(j["data"]["page"]["count"].ToString());

                pages = count <= 20 ? 1 : count / 20 + 1;

                //如果没有评论直接返回null
                if (count == 0)
                {
                    return null;
                }

                if (pages == 1)
                {
                    //如果只有1页直接返回第一页内容
                    return GetReplyDic((JArray)j["data"]["replies"]);
                }
                //否则循环获取所有页数的评论
                else
                {
                    //先读取第一页评论的内容
                    foreach (KeyValuePair<string, string> k in GetReplyDic((JArray)j["data"]["replies"]))
                    {
                        if (!users.ContainsKey(k.Key))
                        {
                            users.Add(k.Key, k.Value);
                        }
                    }
                    //再循环获取接下来几页评论的内容
                    for (int i = 2; i <= pages; i++)
                    {
                        j = JObject.Parse(GetReplyJson(id, i.ToString()));
                        foreach (KeyValuePair<string, string> k in GetReplyDic((JArray)j["data"]["replies"]))
                        {
                            if (!users.ContainsKey(k.Key))
                            {
                                users.Add(k.Key, k.Value);
                            }
                        }
                        //防止rate limit
                        Thread.Sleep(50);
                    }
                }
                return users;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取网页内容
        /// </summary>
        /// <param name="url">网页链接</param>
        /// <returns></returns>
        private static string GetWebpage(string url)
        {
            try
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;
                Byte[] pageData = MyWebClient.DownloadData(url);            //从指定网站下载数据
                string pageHtml = Encoding.Default.GetString(pageData);     //如果获取网站页面采用的是GB2312，则使用这句    
                                                                            //string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
                return pageHtml;
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// 获取动态类型
        /// </summary>
        /// <param name="json">需要处理的json</param>
        /// <returns></returns>
        private static string GetDynType(string json)
        {
            JObject j = JObject.Parse(json);
            if (j["data"]["card"]["desc"]["type"].ToString() == "1" || j["data"]["card"]["desc"]["type"].ToString() == "4")
            {
                return "17";
            }
            else
            {
                return "11";
            }
        }
        /// <summary>
        /// 获取bilibili API所需的动态ID（根据动态类型判断）
        /// </summary>
        /// <param name="json">需要处理的json</param>
        /// <returns></returns>
        private static string GetDynId(string json)
        {
            JObject j = JObject.Parse(json);
            if (GetDynType(json) == "17")
            {
                return j["data"]["card"]["desc"]["dynamic_id"].ToString();
            }
            else
            {
                return j["data"]["card"]["desc"]["rid"].ToString();
            }
        }
        /// <summary>
        /// 获取bilibili读取动态内容所需的链接
        /// </summary>
        /// <param name="oid">动态id</param>
        /// <returns></returns>
        private static string GetCommentApiUrl(string oid)
        {
            string s = GetWebpage("https://api.vc.bilibili.com/dynamic_svr/v1/dynamic_svr/get_dynamic_detail?dynamic_id=" + oid);
            return "https://api.bilibili.com/x/v2/reply?type=" + GetDynType(s) + "&oid=" + GetDynId(s);
        }

        /// <summary>
        /// 获取动态回复相关的JSON
        /// </summary>
        /// <param name="url">BIlibili评论区数据获取URL</param>
        /// <param name="page">评论区页数</param>
        /// <returns></returns>
        private static string GetReplyJson(string url, string page)
        {
            string s = GetWebpage(url + "&pn=" + page);     //我会说我之前把pn写成page导致获取了8次第一页评论的内容吗（
            return s;
        }
    }
}