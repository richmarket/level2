using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net;
using System.Collections.Concurrent;
using System.IO.Compression;

namespace ss_test
{
    class Program
    {
        public static string str = System.Windows.Forms.Application.StartupPath;

        //断线重连更新 20191229
        public static bool xunhuan = true;
        //
        static void Main(string[] args)
        {

            ss_level2.login.ip = "";//ip
            //ss_level2.login.ip = "";//ip
            ss_level2.login.user = "";//用户名
            ss_level2.login.pass = "";//;密码
            ss_level2.login.heart_o_c = false;


            ss_level2.login.ss_Index_event += new ss_level2.login.ss_index(SS_data.Index_Data);
            ss_level2.login.ss_Option_event += new ss_level2.login.ss_option(SS_data.Option_Data);
            ss_level2.login.ss_Market_event0 += new ss_level2.login.ss_Market0(SS_data.Market_Data0);
            ss_level2.login.ss_Queue_event0 += new ss_level2.login.ss_Queue0(SS_data.Queue_Data0);
            ss_level2.login.ss_Tran_event0 += new ss_level2.login.ss_Tran0(SS_data.Tran_Data0);
            ss_level2.login.ss_Order_event0 += new ss_level2.login.ss_Order0(SS_data.Order_Data0);


            create_directory();
            //开启订阅
            //ss_level2.login.option_Get();
            //ss_level2.login.index_Get();
            //ss_level2.login.market_Get();
            //ss_level2.login.tran_Get();
            //ss_level2.login.order_Get();
            //ss_level2.login.queue_Get();
            //推送的数据在SS_data.cs中的回调函数中获取
            //getallmarket();
            //Console.ReadLine();
            //开启订阅
            //ss_level2.login.option_Close();
            //ss_level2.login.index_Close();
            //ss_level2.login.market_Close();
            //ss_level2.login.tran_Close();
            //ss_level2.login.order_Close();
            //ss_level2.login.queue_Close();
            Console.ReadLine();
        }
        //获取 当日市场股票代码表
        public static void getallmarket()
        {
            try
            {
                IPAddress ip = IPAddress.Parse(ss_level2.login.ip);
                IPEndPoint ipe = new IPEndPoint(ip, 30920);
                Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                c.Connect(ipe);
                string securites_account_password = "level2_api_33617get_allmarket" + "\n" + ss_level2.login.user + "\n" + "\n" + "\n" + "\n";

                byte[] sendstr = Encoding.ASCII.GetBytes(securites_account_password);
                c.Send(sendstr, sendstr.Length, 0);
                c.ReceiveTimeout = 500000;
                byte[] rece2 = new byte[1024 * 1024];
                int bytes2 = c.Receive(rece2, rece2.Length, 0);
                string recv2 = "";

                recv2 += UTF8Encoding.UTF8.GetString(rece2, 0, bytes2);

                string[] recvall2 = recv2.Split(new string[] { "#" }, StringSplitOptions.None);
                int num = 0;
                if (recvall2.Length <= 2)
                {//说明丢包，或者服务器数据错误 
                }
                else if (recvall2[0] == "begin" && recvall2[2] == "end")
                {//
                    //Console.WriteLine(recvall2[1]);
                    Console.WriteLine("获取服务器上所有代码成功");
                    //com_cg = true;
                    string[] temp1 = recvall2[1].Split(new string[] { "\n" }, StringSplitOptions.None);
                    for (int i = 0; i < temp1.Length; i++)
                    {
                        string[] temp2 = temp1[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                        {
                            if (temp2.Length >= 8)
                            {
                                {
                                    Console.WriteLine(num++ +" "+temp2[0] + " " + temp2[1] + " " + temp2[2] + " " + temp2[3] + " " + temp2[4] + " " + temp2[5] + " " + temp2[6] + " " + temp2[7]);
                                    //code name date time state preclose max min
                                    //state 0开市前 1开盘集合竞价 2连续竞价阶段 3盘中临时停牌 4收盘集合竞价 5集中竞价闭市 6协议转让结束 7闭市

                                }
                            }
                        }
                    }
                    Console.WriteLine("获取服务器上所有代码成功");
                }

                c.Close();
            }
            catch (Exception ex)
            { Console.WriteLine(ex); }
        }

        private static void create_directory()
        {
            if (Directory.Exists(string.Format(str + @"\{0}\market", DateTime.Now.ToString("yyyyMMdd"))))
            {//存在
            }
            else
            {//不存在就创建该文件夹
                Directory.CreateDirectory(string.Format(str + @"\{0}\market", DateTime.Now.ToString("yyyyMMdd")));
            }

            if (Directory.Exists(string.Format(str + @"\{0}\tran", DateTime.Now.ToString("yyyyMMdd"))))
            {//存在
            }
            else
            {//不存在就创建该文件夹
                Directory.CreateDirectory(string.Format(str + @"\{0}\tran", DateTime.Now.ToString("yyyyMMdd")));
            }

            if (Directory.Exists(string.Format(str + @"\{0}\index", DateTime.Now.ToString("yyyyMMdd"))))
            {//存在
            }
            else
            {//不存在就创建该文件夹
                Directory.CreateDirectory(string.Format(str + @"\{0}\index", DateTime.Now.ToString("yyyyMMdd")));
            }

            if (Directory.Exists(string.Format(str + @"\{0}\queue", DateTime.Now.ToString("yyyyMMdd"))))
            {//存在
            }
            else
            {//不存在就创建该文件夹
                Directory.CreateDirectory(string.Format(str + @"\{0}\queue", DateTime.Now.ToString("yyyyMMdd")));
            }

            if (Directory.Exists(string.Format(str + @"\{0}\order", DateTime.Now.ToString("yyyyMMdd"))))
            {//存在
            }
            else
            {//不存在就创建该文件夹
                Directory.CreateDirectory(string.Format(str + @"\{0}\order", DateTime.Now.ToString("yyyyMMdd")));
            }
        }
    }
}
