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

namespace ss_level2
{
    public class login
    {
        static ConcurrentQueue<byte[]> buffer_list_index0 = new ConcurrentQueue<byte[]>();
        static ConcurrentQueue<byte[]> buffer_list_option0 = new ConcurrentQueue<byte[]>();
        static ConcurrentQueue<byte[]> buffer_list_market0 = new ConcurrentQueue<byte[]>();
        static ConcurrentQueue<byte[]> buffer_list_market1 = new ConcurrentQueue<byte[]>();
        static ConcurrentQueue<byte[]> buffer_list_queue0 = new ConcurrentQueue<byte[]>();
        static ConcurrentQueue<byte[]> buffer_list_queue1 = new ConcurrentQueue<byte[]>();
        static ConcurrentQueue<byte[]> buffer_list_tran0 = new ConcurrentQueue<byte[]>();
        static ConcurrentQueue<byte[]> buffer_list_tran1 = new ConcurrentQueue<byte[]>();
        static ConcurrentQueue<byte[]> buffer_list_order0 = new ConcurrentQueue<byte[]>();
        static ConcurrentQueue<byte[]> buffer_list_order1 = new ConcurrentQueue<byte[]>();


        static long buf_count_index0 = 0;
        static long buf_count_option0 = 0;
        static long buf_count_market0 = 0;
        static long buf_count_market1 = 0;
        static long buf_count_queue0 = 0;
        static long buf_count_queue1 = 0;
        static long buf_count_tran0 = 0;
        static long buf_count_tran1 = 0;
        static long buf_count_order0 = 0;
        static long buf_count_order1 = 0;


        //static DateTime hearttime_index = DateTime.Now;
        //static DateTime timeall = DateTime.Now;

        static byte[] buffer_index0 = new byte[64];

        static byte[] buffer_option0 = new byte[64];

        static byte[] buffer_market0 = new byte[64];
        static byte[] buffer_market1 = new byte[64];

        static byte[] buffer_queue0 = new byte[64];
        static byte[] buffer_queue1 = new byte[64];

        static byte[] buffer_tran0 = new byte[64];
        static byte[] buffer_tran1 = new byte[64];

        static byte[] buffer_order0 = new byte[64];
        static byte[] buffer_order1 = new byte[64];

        public static string ip = "";
        public static string user = "";
        public static string pass = "";
        public static bool heart_o_c = true;

        public static Socket index_socket;
        public static Socket option_socket;
        public static Socket market0_socket;
        public static Socket market1_socket;
        public static Socket tran0_socket;
        public static Socket tran1_socket;
        public static Socket queue0_socket;
        public static Socket queue1_socket;
        public static Socket order0_socket;
        public static Socket order1_socket;

        //断线重连更新 20191229
        public static DateTime hearttime_queue;
        public static DateTime hearttime_tran;
        public static DateTime hearttime_order;
        public static DateTime hearttime_market;
        public static DateTime hearttime_index;
        public static DateTime hearttime_option;

        public login()
        {
            //ss_Index_event += new ss_index(SS_data.Index_Data);
            //ss_Market_event0 += new ss_Market0(SS_data.Market_Data0);
            //ss_Market_event1 += new ss_Market1(SS_data.Market_Data1);
            //ss_Tran_event0 += new ss_Tran0(SS_data.Tran_Data0);
            //ss_Tran_event1 += new ss_Tran1(SS_data.Tran_Data1);
            //ss_Order_event0 += new ss_Order0(SS_data.Order_Data0);
            //ss_Order_event1 += new ss_Order1(SS_data.Order_Data1);
        }

        ///index
        public static void ConnectToServer_index0()
        {
            //断线重连更新 20191229
            
            abc_index:
            hearttime_index = DateTime.Now;
            //
            Socket c2 = Login_index0();
            Thread jie11 = new Thread(new ParameterizedThreadStart(Receive_message_index0));//执行具体内容的线程
            jie11.Start((object)c2);
            index_socket = c2;

            //断线重连更新 20191229
            while (ss_test.Program.xunhuan)//(true)
            {
                if ((DateTime.Now - hearttime_index).TotalSeconds > 20)
                {
                    try
                    {
                        c2.Close();
                        jie11.Abort();

                        Thread.Sleep(5000);
                        goto abc_index;
                    }
                    catch (Exception ex)
                    { Console.WriteLine("Connect:" + ex); }
                }
                Thread.Sleep(10000);
            }
            //断线重连更新 20191229
        }

        private static Socket Login_index0()
        {
            //初始化成员
            int port2 = 32920;
            string host2 = ip;
            IPAddress ipx = IPAddress.Parse(host2);
            IPEndPoint ipe = new IPEndPoint(ipx, port2);
            Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            c.Connect(ipe);
            string msg_send = "level2_api_33617" + "\n" + "login_user" + "\n" + user + "\n" + pass + "\n" + "SH_2;SZ_2" + "\n" + "Index" + "\n" + "0";
            byte[] sendstr = Encoding.ASCII.GetBytes(msg_send);
            c.Send(sendstr, sendstr.Length, 0);
            return c;
        }

        private static void Receive_message_index0(object temp1)
        {
            try
            {
                Socket c = (Socket)temp1;
                c.BeginReceive(buffer_index0, 0, buffer_index0.Length, SocketFlags.None, new AsyncCallback(Receive_callback_index0), c);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mess:" + ex);
            }
        }

        private static void Receive_callback_index0(IAsyncResult ar)
        {
            try
            {
                var socket = ar.AsyncState as Socket;
                var length = socket.EndReceive(ar);
                byte[] buffer_temp = new byte[length];
                Buffer.BlockCopy(buffer_index0, 0, buffer_temp, 0, length);
                buffer_list_index0.Enqueue(buffer_temp);
                buf_count_index0++;
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer_index0, 0, buffer_index0.Length, SocketFlags.None, new AsyncCallback(Receive_callback_index0), socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Receive:" + ex.Message);
            }
        }
        //index

        ///option
        public static void ConnectToServer_option0()
        {
            //断线重连更新 20191229
            
            abc_option:
            hearttime_option = DateTime.Now;
            //
            Socket c2 = Login_option0();
            Thread jie11 = new Thread(new ParameterizedThreadStart(Receive_message_option0));//执行具体内容的线程
            jie11.Start((object)c2);
            option_socket = c2;

            //断线重连更新 20191229
            while (ss_test.Program.xunhuan)//(true)
            {
                if ((DateTime.Now - hearttime_option).TotalSeconds > 20)
                {
                    try
                    {
                        c2.Close();
                        jie11.Abort();

                        Thread.Sleep(5000);
                        goto abc_option;
                    }
                    catch (Exception ex)
                    { Console.WriteLine("Connect:" + ex); }
                }
                Thread.Sleep(10000);
            }
            //断线重连更新 20191229
        }

        private static Socket Login_option0()
        {
            //初始化成员
            int port2 = 31920;
            string host2 = ip;
            IPAddress ipx = IPAddress.Parse(host2);
            IPEndPoint ipe = new IPEndPoint(ipx, port2);
            Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            c.Connect(ipe);
            string msg_send = "level2_api_33617" + "\n" + "login_user" + "\n" + user + "\n" + pass + "\n" + "SH_2;SZ_2" + "\n" + "Option" + "\n" + "0";
            byte[] sendstr = Encoding.ASCII.GetBytes(msg_send);
            c.Send(sendstr, sendstr.Length, 0);
            return c;
        }

        private static void Receive_message_option0(object temp1)
        {
            try
            {
                Socket c = (Socket)temp1;
                c.BeginReceive(buffer_option0, 0, buffer_option0.Length, SocketFlags.None, new AsyncCallback(Receive_callback_option0), c);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mess:" + ex);
            }
        }

        private static void Receive_callback_option0(IAsyncResult ar)
        {
            try
            {
                var socket = ar.AsyncState as Socket;
                var length = socket.EndReceive(ar);
                byte[] buffer_temp = new byte[length];
                Buffer.BlockCopy(buffer_option0, 0, buffer_temp, 0, length);
                buffer_list_option0.Enqueue(buffer_temp);
                buf_count_option0++;
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer_option0, 0, buffer_option0.Length, SocketFlags.None, new AsyncCallback(Receive_callback_option0), socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Receive:" + ex.Message);
            }
        }
        //option

        /// market
        public static void ConnectToServer_market0()
        {
            //断线重连更新 20191229
            
            abc_market:
            hearttime_market = DateTime.Now;
            //
            Socket c2 = Login_market0();
            Thread jie11 = new Thread(new ParameterizedThreadStart(Receive_message_market0));//执行具体内容的线程
            jie11.Start((object)c2);
            market0_socket = c2;
            //断线重连更新 20191229
            while (ss_test.Program.xunhuan)//(true)
            {
                if ((DateTime.Now - hearttime_market).TotalSeconds > 20)
                {
                    try
                    {
                        c2.Close();
                        jie11.Abort();

                        Thread.Sleep(5000);
                        goto abc_market;
                    }
                    catch (Exception ex)
                    { Console.WriteLine("Connect:" + ex); }
                }
                Thread.Sleep(10000);
            }
            //断线重连更新 20191229
        }

        private static Socket Login_market0()
        {
            //初始化成员
            int port2 = 33920;
            string host2 = ip;
            IPAddress ipx = IPAddress.Parse(host2);
            IPEndPoint ipe = new IPEndPoint(ipx, port2);
            Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            c.Connect(ipe);
            string msg_send = "level2_api_33617" + "\n" + "login_user" + "\n" + user + "\n" + pass + "\n" + "SH_2;SZ_2" + "\n" + "Market0" + "\n" + "0";
            byte[] sendstr = Encoding.ASCII.GetBytes(msg_send);
            c.Send(sendstr, sendstr.Length, 0);
            return c;
        }

        private static void Receive_message_market0(object temp1)
        {
            try
            {
                Socket c = (Socket)temp1;
                c.BeginReceive(buffer_market0, 0, buffer_market0.Length, SocketFlags.None, new AsyncCallback(Receive_callback_market0), c);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mess:" + ex);
            }
        }

        private static void Receive_callback_market0(IAsyncResult ar)
        {
            try
            {
                var socket = ar.AsyncState as Socket;
                var length = socket.EndReceive(ar);
                byte[] buffer_temp = new byte[length];
                Buffer.BlockCopy(buffer_market0, 0, buffer_temp, 0, length);
                buffer_list_market0.Enqueue(buffer_temp);
                buf_count_market0++;
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer_market0, 0, buffer_market0.Length, SocketFlags.None, new AsyncCallback(Receive_callback_market0), socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Receive:" + ex.Message);
            }
        }
        //market

        /// queue
        public static void ConnectToServer_queue0()
        {
            //断线重连更新 20191229
            
            abc_queue:
            hearttime_queue = DateTime.Now;
            //
            Socket c2 = Login_queue0();
            //timeall = DateTime.Now;
            Thread jie11 = new Thread(new ParameterizedThreadStart(Receive_message_queue0));//执行具体内容的线程
            jie11.Start((object)c2);
            queue0_socket = c2;
            //断线重连更新 20191229
            while (ss_test.Program.xunhuan)//(true)
            {
                if ((DateTime.Now - hearttime_queue).TotalSeconds > 20)
                {
                    try
                    {
                        c2.Close();
                        jie11.Abort();

                        Thread.Sleep(5000);
                        goto abc_queue;
                    }
                    catch (Exception ex)
                    { Console.WriteLine("Connect:" + ex); }
                }
                Thread.Sleep(10000);
            }
            //断线重连更新 20191229
        }

        private static Socket Login_queue0()
        {
            //初始化成员
            int port2 = 36920;
            string host2 = ip;
            IPAddress ipx = IPAddress.Parse(host2);
            IPEndPoint ipe = new IPEndPoint(ipx, port2);
            Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            c.Connect(ipe);
            string msg_send = "level2_api_33617" + "\n" + "login_user" + "\n" + user + "\n" + pass + "\n" + "SH_2;SZ_2" + "\n" + "Queue0" + "\n" + "0";
            byte[] sendstr = Encoding.ASCII.GetBytes(msg_send);
            c.Send(sendstr, sendstr.Length, 0);
            return c;
        }

        private static void Receive_message_queue0(object temp1)
        {
            try
            {
                Socket c = (Socket)temp1;
                c.BeginReceive(buffer_queue0, 0, buffer_queue0.Length, SocketFlags.None, new AsyncCallback(Receive_callback_queue0), c);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mess:" + ex);
            }
        }

        private static void Receive_callback_queue0(IAsyncResult ar)
        {
            try
            {
                var socket = ar.AsyncState as Socket;
                var length = socket.EndReceive(ar);
                byte[] buffer_temp = new byte[length];
                Buffer.BlockCopy(buffer_queue0, 0, buffer_temp, 0, length);
                buffer_list_queue0.Enqueue(buffer_temp);
                buf_count_queue0++;
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer_queue0, 0, buffer_queue0.Length, SocketFlags.None, new AsyncCallback(Receive_callback_queue0), socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Receive:" + ex.Message);
            }
        }
        //queue


        
        /// tran
        public static void ConnectToServer_tran0()
        {
            //断线重连更新 20191229
            //hearttime_tran = DateTime.Now;//(1、修改前)
            abc_tran:
            hearttime_tran = DateTime.Now;//(1、修改后)
            //

            Socket c2 = Login_tran0();
            Thread jie11 = new Thread(new ParameterizedThreadStart(Receive_message_tran0));//执行具体内容的线程
            jie11.Start((object)c2);
            tran0_socket = c2;

            //断线重连更新 20191229
            while (ss_test.Program.xunhuan)//(true)
            {
                //if ((DateTime.Now - hearttime_tran).TotalSeconds > 10000)//(2、修改后)
                if ((DateTime.Now - hearttime_tran).TotalSeconds > 20)//(2、修改后)
                {
                    try
                    {
                        c2.Close();
                        jie11.Abort();
                        //Thread.Sleep(1000);//(3、修改后)
                        Thread.Sleep(5000);//(3、修改后)
                        goto abc_tran;
                    }
                    catch (Exception ex)
                    { Console.WriteLine("Connect:" + ex); }
                }
                Thread.Sleep(10*1000);
            }
            //断线重连更新 20191229
        }

        private static Socket Login_tran0()
        {
            //初始化成员
            int port2 = 34920;
            string host2 = ip;
            IPAddress ipx = IPAddress.Parse(host2);
            IPEndPoint ipe = new IPEndPoint(ipx, port2);
            Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            c.Connect(ipe);
            string msg_send = "level2_api_33617" + "\n" + "login_user" + "\n" + user + "\n" + pass + "\n" + "SH_2;SZ_2" + "\n" + "Tran0" + "\n" + "0";
            byte[] sendstr = Encoding.ASCII.GetBytes(msg_send);
            c.Send(sendstr, sendstr.Length, 0);
            return c;
        }

        private static void Receive_message_tran0(object temp1)
        {
            try
            {
                Socket c = (Socket)temp1;
                c.BeginReceive(buffer_tran0, 0, buffer_tran0.Length, SocketFlags.None, new AsyncCallback(Receive_callback_tran0), c);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mess:" + ex);
            }
        }

        private static void Receive_callback_tran0(IAsyncResult ar)
        {
            try
            {
                var socket = ar.AsyncState as Socket;
                var length = socket.EndReceive(ar);
                byte[] buffer_temp = new byte[length];
                Buffer.BlockCopy(buffer_tran0, 0, buffer_temp, 0, length);
                buffer_list_tran0.Enqueue(buffer_temp);
                buf_count_tran0++;
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer_tran0, 0, buffer_tran0.Length, SocketFlags.None, new AsyncCallback(Receive_callback_tran0), socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Receive:" + ex.Message);
            }
        }

        
        ///order
        public static void ConnectToServer_order0()
        {
            //断线重连更新 20191229
            
            abc_order:
            hearttime_order = DateTime.Now;
            //断线重连更新 20191229

            Socket c2 = Login_order0();
            Thread jie11 = new Thread(new ParameterizedThreadStart(Receive_message_order0));//执行具体内容的线程
            jie11.Start((object)c2);
            order0_socket = c2;

            //断线重连更新 20191229
            while (ss_test.Program.xunhuan)//(true)
            {
                if ((DateTime.Now - hearttime_order).TotalSeconds > 20)
                {
                    try
                    {
                        c2.Close();
                        jie11.Abort();

                        Thread.Sleep(5000);
                        goto abc_order;
                    }
                    catch (Exception ex)
                    { Console.WriteLine("Connect:" + ex); }
                }
                Thread.Sleep(10000);
            }
            //断线重连更新 20191229
        }

        private static Socket Login_order0()
        {
            //初始化成员
            int port2 = 35920;
            string host2 = ip;
            IPAddress ipx = IPAddress.Parse(host2);
            IPEndPoint ipe = new IPEndPoint(ipx, port2);
            Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            c.Connect(ipe);
            string msg_send = "level2_api_33617" + "\n" + "login_user" + "\n" + user + "\n" + pass + "\n" + "SH_2;SZ_2" + "\n" + "Order0" + "\n" + "0";
            byte[] sendstr = Encoding.ASCII.GetBytes(msg_send);
            c.Send(sendstr, sendstr.Length, 0);
            return c;
        }

        private static void Receive_message_order0(object temp1)
        {
            try
            {
                Socket c = (Socket)temp1;
                c.BeginReceive(buffer_order0, 0, buffer_order0.Length, SocketFlags.None, new AsyncCallback(Receive_callback_order0), c);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mess:" + ex);
            }
        }

        private static void Receive_callback_order0(IAsyncResult ar)
        {
            try
            {
                var socket = ar.AsyncState as Socket;
                var length = socket.EndReceive(ar);
                byte[] buffer_temp = new byte[length];
                Buffer.BlockCopy(buffer_order0, 0, buffer_temp, 0, length);
                buffer_list_order0.Enqueue(buffer_temp);
                buf_count_order0++;
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer_order0, 0, buffer_order0.Length, SocketFlags.None, new AsyncCallback(Receive_callback_order0), socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Receive:" + ex.Message);
            }
        }
        //order



        public static login lg = new login();
        public static Thread index_con_thread;
        public static Thread index_dic_thread;

        public static Thread option_con_thread;
        public static Thread option_dic_thread;

        public static Thread market0_con_thread;
        public static Thread market0_dic_thread;

        public static Thread tran0_con_thread;
        public static Thread tran0_dic_thread;

        public static Thread queue0_con_thread;
        public static Thread queue0_dic_thread;

        public static Thread order0_con_thread;
        public static Thread order0_dic_thread;

        public static void index_Get()
        {
            //********index******
            index_con_thread = new Thread(login.ConnectToServer_index0);
            index_con_thread.Start();

            index_dic_thread = new Thread(lg.Byte_decompress_index0);
            index_dic_thread.Start();
            //**************
        }

        public static void option_Get()
        {
            //********index******
            option_con_thread = new Thread(login.ConnectToServer_option0);
            option_con_thread.Start();

            option_dic_thread = new Thread(lg.Byte_decompress_option0);
            option_dic_thread.Start();
            //**************
        }

        public static void market_Get()
        {
            //********market******
            market0_con_thread = new Thread(login.ConnectToServer_market0);
            market0_con_thread.Start();
            //market1_con_thread = new Thread(login.ConnectToServer_market1);
            //market1_con_thread.Start();

            market0_dic_thread = new Thread(lg.Byte_decompress_market0);
            market0_dic_thread.Start();
            //market1_dic_thread = new Thread(lg.Byte_decompress_market1);
            //market1_dic_thread.Start();

            //**************
        }

        public static void tran_Get()
        {
            ////********tran******
            tran0_con_thread = new Thread(login.ConnectToServer_tran0);
            tran0_con_thread.Start();


            tran0_dic_thread = new Thread(lg.Byte_decompress_tran0);
            tran0_dic_thread.Start();

            ////**************
        }

        public static void order_Get()
        {
            ////********order******
            order0_con_thread = new Thread(login.ConnectToServer_order0);
            order0_con_thread.Start();
            //order1_con_thread = new Thread(login.ConnectToServer_order1);
            //order1_con_thread.Start();

            order0_dic_thread = new Thread(lg.Byte_decompress_order0);
            order0_dic_thread.Start();
            //order1_dic_thread = new Thread(lg.Byte_decompress_order1);
            //order1_dic_thread.Start();
            ////**************
        }

        public static void queue_Get()
        {
            //********queue******
            queue0_con_thread = new Thread(login.ConnectToServer_queue0);
            queue0_con_thread.Start();
            //queue1_con_thread = new Thread(login.ConnectToServer_queue1);
            //queue1_con_thread.Start();

            queue0_dic_thread = new Thread(lg.Byte_decompress_queue0);
            queue0_dic_thread.Start();
            //queue1_dic_thread = new Thread(lg.Byte_decompress_queue1);
            //queue1_dic_thread.Start();
            //**************
        }

        public static void index_Close()
        {
            ss_test.Program.xunhuan = false;
            index_socket.Close();
            index_con_thread.Abort();
            index_dic_thread.Abort();
        }

        public static void option_Close()
        {
            ss_test.Program.xunhuan = false;
            option_socket.Close();
            option_con_thread.Abort();
            option_dic_thread.Abort();
        }

        public static void market_Close()
        {
            ss_test.Program.xunhuan = false;
            market0_socket.Close();
            //market1_socket.Close();

            market0_con_thread.Abort();
            //market1_con_thread.Abort();
            market0_dic_thread.Abort();
            //market1_dic_thread.Abort();
        }

        public static void tran_Close()
        {
            ss_test.Program.xunhuan = false;
            tran0_socket.Close();
            //tran1_socket.Close();
            tran0_con_thread.Abort();
            //tran1_con_thread.Abort();
            tran0_dic_thread.Abort();
            //tran1_dic_thread.Abort();
        }

        public static void queue_Close()
        {
            ss_test.Program.xunhuan = false;
            queue0_socket.Close();
            //queue1_socket.Close();
            queue0_con_thread.Abort();
            //queue1_con_thread.Abort();
            queue0_dic_thread.Abort();
            //queue1_dic_thread.Abort();
        }

        public static void order_Close()
        {
            ss_test.Program.xunhuan = false;
            order0_socket.Close();
            //order1_socket.Close();
            order0_con_thread.Abort();
            //order1_con_thread.Abort();
            order0_dic_thread.Abort();
            //order1_dic_thread.Abort();
        }

        //Index_data
        public delegate void ss_index(string s);
        public static event ss_index ss_Index_event;

        //Potion_data
        public delegate void ss_option(string s);
        public static event ss_option ss_Option_event;

        //Market_data0
        public delegate void ss_Market0(string s);
        public static event ss_Market0 ss_Market_event0;

        //Queue_data0
        public delegate void ss_Queue0(string s);
        public static event ss_Queue0 ss_Queue_event0;

        //Tran_data0
        public delegate void ss_Tran0(string s);
        public static event ss_Tran0 ss_Tran_event0;

        //Order_data0
        public delegate void ss_Order0(string s);
        public static event ss_Order0 ss_Order_event0;



        /*/ <index 数据解析>*/
        public void Byte_decompress_index0()
        {
            long dic_temp = 1;
            //用于存储剩余未解析的字节数
            List<byte> _LBuff = new List<byte>(0);
            while (true)
            {
                if (dic_temp <= buf_count_index0)
                {
                    try
                    {
                        byte[] by_temp;
                        buffer_list_index0.TryDequeue(out by_temp);
                        {
                            //拷贝剩余留下的字节数
                            _LBuff.AddRange(by_temp);
                            dic_temp++;
                            //循环取包头 
                            //备注： 完整包组成 SS_begin + (@index_@) + 13位数据长 + 数据  （8 + 8 + 13 +数据）
                            string type_market = "";
                            int len_market = 0;
                            DateTime time1 = DateTime.Now;
                            DateTime time2 = DateTime.Now;
                            for (int i = 0; i < _LBuff.Count; i++)
                            {
                                //Console.WriteLine("i:"+i);
                                if (_LBuff.Count >= i + 11)
                                {
                                    byte[] heart_beart = new byte[11] { _LBuff[i], _LBuff[i + 1], _LBuff[i + 2], _LBuff[i + 3], _LBuff[i + 4], _LBuff[i + 5], _LBuff[i + 6], _LBuff[i + 7], _LBuff[i + 8], _LBuff[i + 9], _LBuff[i + 10] };
                                    string heart = Encoding.UTF8.GetString(heart_beart);
                                    if (heart == "Heart_beart")
                                    {
                                        //断线重连更新 20191229
                                        hearttime_index = DateTime.Now;
                                        //
                                        if (heart_o_c == true)
                                        {
                                            Console.WriteLine("index0 " + heart + DateTime.Now.ToString("   HH-mm-ss-fff"));
                                        }
                                        _LBuff.RemoveRange(0, i + 11);
                                        //hearttime = DateTime.Now;
                                        break;
                                    }
                                }
                                if (_LBuff.Count >= i + 8 + 8 + 13)
                                { //取出前8位
                                    try
                                    {
                                        byte[] top_8 = new byte[8] { _LBuff[i], _LBuff[i + 1], _LBuff[i + 2], _LBuff[i + 3], _LBuff[i + 4], _LBuff[i + 5], _LBuff[i + 6], _LBuff[i + 7] };
                                        string top_8_name = Encoding.UTF8.GetString(top_8);
                                        if (top_8_name == "SS_begin")
                                        { //说明包头无误，开始获取包长度及包内容解析
                                            //market 类型8位
                                            type_market = Encoding.UTF8.GetString(new byte[8] { _LBuff[i + 8], _LBuff[i + 9], _LBuff[i + 10], _LBuff[i + 11], _LBuff[i + 12], _LBuff[i + 13], _LBuff[i + 14], _LBuff[i + 15] });
                                            //market 长度
                                            //Console.WriteLine(Encoding.UTF8.GetString(new byte[13] { _LBuff[i + 16], _LBuff[i + 17], _LBuff[i + 18], _LBuff[i + 19], _LBuff[i + 20], _LBuff[i + 21], _LBuff[i + 22], _LBuff[i + 23], _LBuff[i + 24], _LBuff[i + 25], _LBuff[i + 26], _LBuff[i + 27], _LBuff[i + 28] }));
                                            len_market = int.Parse(Encoding.UTF8.GetString(new byte[13] { _LBuff[i + 16], _LBuff[i + 17], _LBuff[i + 18], _LBuff[i + 19], _LBuff[i + 20], _LBuff[i + 21], _LBuff[i + 22], _LBuff[i + 23], _LBuff[i + 24], _LBuff[i + 25], _LBuff[i + 26], _LBuff[i + 27], _LBuff[i + 28] }));
                                            //截取 _LBuff指定market 长度进行解析，剩余拷贝下次循环
                                            if (_LBuff.Count >= len_market + i + 8 + 8 + 13)
                                            { //说明剩余字节可以截断解析
                                                time1 = DateTime.Now;
                                                byte[] buffer_ma = new byte[len_market];
                                                _LBuff.CopyTo(i + 8 + 8 + 13, buffer_ma, 0, len_market);
                                                _LBuff.RemoveRange(0, i + 8 + 8 + 13 + len_market);
                                                string buffer_temp = "";
                                                if (type_market == "@index_@")
                                                {
                                                    byte[] buffer_ma_x = Decompress(buffer_ma);//解压数据
                                                    buffer_temp = Encoding.UTF8.GetString(buffer_ma_x);
                                                }
                                                else if (type_market == "@Index_@")
                                                {
                                                    buffer_temp = Encoding.UTF8.GetString(buffer_ma);//非解压
                                                }
                                                // 将数据复制到其它数据缓存区，由其它线程做业务逻辑处理 例如:建立一个queue 将数据保存至queue中继续操作.这里演示数据打印屏幕
                                                ss_Index_event(buffer_temp);

                                                break;
                                            }
                                            else
                                            { //不够长度无法解析，等待后续数据到达 再进行解析
                                                break;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex); }
                }
                else { Thread.Sleep(1); }
            }
        }
        /* <index 数据解析>*/

        /*/ <option数据解析>*/
        public void Byte_decompress_option0()
        {
            long dic_temp = 1;
            //用于存储剩余未解析的字节数
            List<byte> _LBuff = new List<byte>(0);
            while (true)
            {
                if (dic_temp <= buf_count_option0)
                {
                    try
                    {
                        byte[] by_temp;
                        buffer_list_option0.TryDequeue(out by_temp);
                        {
                            //拷贝剩余留下的字节数
                            _LBuff.AddRange(by_temp);
                            dic_temp++;
                            //循环取包头 
                            //备注： 完整包组成 SS_begin + (@option_@) + 13位数据长 + 数据  （8 + 8 + 13 +数据）
                            string type_market = "";
                            int len_market = 0;
                            DateTime time1 = DateTime.Now;
                            DateTime time2 = DateTime.Now;
                            for (int i = 0; i < _LBuff.Count; i++)
                            {
                                //Console.WriteLine("i:"+i);
                                if (_LBuff.Count >= i + 11)
                                {
                                    byte[] heart_beart = new byte[11] { _LBuff[i], _LBuff[i + 1], _LBuff[i + 2], _LBuff[i + 3], _LBuff[i + 4], _LBuff[i + 5], _LBuff[i + 6], _LBuff[i + 7], _LBuff[i + 8], _LBuff[i + 9], _LBuff[i + 10] };
                                    string heart = Encoding.UTF8.GetString(heart_beart);
                                    if (heart == "Heart_beart")
                                    {
                                        //断线重连更新 20191229
                                        hearttime_option = DateTime.Now;
                                        //
                                        if (heart_o_c == true)
                                        {
                                            Console.WriteLine("option0 " + heart + DateTime.Now.ToString("   HH-mm-ss-fff"));
                                        }
                                        _LBuff.RemoveRange(0, i + 11);
                                        //hearttime = DateTime.Now;
                                        break;
                                    }
                                }
                                if (_LBuff.Count >= i + 8 + 8 + 13)
                                { //取出前8位
                                    try
                                    {
                                        byte[] top_8 = new byte[8] { _LBuff[i], _LBuff[i + 1], _LBuff[i + 2], _LBuff[i + 3], _LBuff[i + 4], _LBuff[i + 5], _LBuff[i + 6], _LBuff[i + 7] };
                                        string top_8_name = Encoding.UTF8.GetString(top_8);
                                        if (top_8_name == "SS_begin")
                                        { //说明包头无误，开始获取包长度及包内容解析
                                            //market 类型8位
                                            type_market = Encoding.UTF8.GetString(new byte[8] { _LBuff[i + 8], _LBuff[i + 9], _LBuff[i + 10], _LBuff[i + 11], _LBuff[i + 12], _LBuff[i + 13], _LBuff[i + 14], _LBuff[i + 15] });
                                            //market 长度
                                            //Console.WriteLine(Encoding.UTF8.GetString(new byte[13] { _LBuff[i + 16], _LBuff[i + 17], _LBuff[i + 18], _LBuff[i + 19], _LBuff[i + 20], _LBuff[i + 21], _LBuff[i + 22], _LBuff[i + 23], _LBuff[i + 24], _LBuff[i + 25], _LBuff[i + 26], _LBuff[i + 27], _LBuff[i + 28] }));
                                            len_market = int.Parse(Encoding.UTF8.GetString(new byte[13] { _LBuff[i + 16], _LBuff[i + 17], _LBuff[i + 18], _LBuff[i + 19], _LBuff[i + 20], _LBuff[i + 21], _LBuff[i + 22], _LBuff[i + 23], _LBuff[i + 24], _LBuff[i + 25], _LBuff[i + 26], _LBuff[i + 27], _LBuff[i + 28] }));
                                            //截取 _LBuff指定market 长度进行解析，剩余拷贝下次循环
                                            if (_LBuff.Count >= len_market + i + 8 + 8 + 13)
                                            { //说明剩余字节可以截断解析
                                                time1 = DateTime.Now;
                                                byte[] buffer_ma = new byte[len_market];
                                                _LBuff.CopyTo(i + 8 + 8 + 13, buffer_ma, 0, len_market);
                                                _LBuff.RemoveRange(0, i + 8 + 8 + 13 + len_market);
                                                string buffer_temp = "";
                                                if (type_market == "@option@")
                                                {
                                                    byte[] buffer_ma_x = Decompress(buffer_ma);//解压数据
                                                    buffer_temp = Encoding.UTF8.GetString(buffer_ma_x);
                                                }
                                                else if (type_market == "@Option@")
                                                {
                                                    buffer_temp = Encoding.UTF8.GetString(buffer_ma);//非解压
                                                }
                                                // 将数据复制到其它数据缓存区，由其它线程做业务逻辑处理 例如:建立一个queue 将数据保存至queue中继续操作.这里演示数据打印屏幕
                                                ss_Option_event(buffer_temp);

                                                break;
                                            }
                                            else
                                            { //不够长度无法解析，等待后续数据到达 再进行解析
                                                break;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex); }
                }
                else { Thread.Sleep(1); }
            }
        }
        /* <option 数据解析>*/

        /* </market 数据解析>*/
        public void Byte_decompress_market0()
        {
            long dic_temp = 1;
            //用于存储剩余未解析的字节数
            List<byte> _LBuff = new List<byte>(0);
            while (true)
            {
                if (dic_temp <= buf_count_market0)
                {
                    try
                    {
                        byte[] by_temp;
                        buffer_list_market0.TryDequeue(out by_temp);
                        {
                            //拷贝剩余留下的字节数
                            _LBuff.AddRange(by_temp);
                            dic_temp++;
                            //循环取包头 
                            //备注： 完整包组成 SS_begin + (@market@) + 13位数据长 + 数据  （8 + 8 + 13 +数据）
                            string type_market = "";
                            int len_market = 0;
                            DateTime time1 = DateTime.Now;
                            DateTime time2 = DateTime.Now;
                            for (int i = 0; i < _LBuff.Count; i++)
                            {
                                //Console.WriteLine("i:"+i);
                                if (_LBuff.Count >= i + 11)
                                {
                                    byte[] heart_beart = new byte[11] { _LBuff[i], _LBuff[i + 1], _LBuff[i + 2], _LBuff[i + 3], _LBuff[i + 4], _LBuff[i + 5], _LBuff[i + 6], _LBuff[i + 7], _LBuff[i + 8], _LBuff[i + 9], _LBuff[i + 10] };
                                    string heart = Encoding.UTF8.GetString(heart_beart);
                                    if (heart == "Heart_beart")
                                    {
                                        //断线重连更新 20191229
                                        hearttime_market = DateTime.Now;
                                        //
                                        if (heart_o_c == true)
                                        {
                                            Console.WriteLine("market0 " + heart + DateTime.Now.ToString("   HH-mm-ss-fff"));
                                        }
                                        _LBuff.RemoveRange(0, i + 11);
                                        //hearttime = DateTime.Now;
                                        break;
                                    }
                                }
                                if (_LBuff.Count >= i + 8 + 8 + 13)
                                { //取出前8位
                                    try
                                    {
                                        byte[] top_8 = new byte[8] { _LBuff[i], _LBuff[i + 1], _LBuff[i + 2], _LBuff[i + 3], _LBuff[i + 4], _LBuff[i + 5], _LBuff[i + 6], _LBuff[i + 7] };
                                        string top_8_name = Encoding.UTF8.GetString(top_8);
                                        if (top_8_name == "SS_begin")
                                        { //说明包头无误，开始获取包长度及包内容解析
                                            //market 类型8位
                                            type_market = Encoding.UTF8.GetString(new byte[8] { _LBuff[i + 8], _LBuff[i + 9], _LBuff[i + 10], _LBuff[i + 11], _LBuff[i + 12], _LBuff[i + 13], _LBuff[i + 14], _LBuff[i + 15] });
                                            //market 长度
                                            //Console.WriteLine(Encoding.UTF8.GetString(new byte[13] { _LBuff[i + 16], _LBuff[i + 17], _LBuff[i + 18], _LBuff[i + 19], _LBuff[i + 20], _LBuff[i + 21], _LBuff[i + 22], _LBuff[i + 23], _LBuff[i + 24], _LBuff[i + 25], _LBuff[i + 26], _LBuff[i + 27], _LBuff[i + 28] }));
                                            len_market = int.Parse(Encoding.UTF8.GetString(new byte[13] { _LBuff[i + 16], _LBuff[i + 17], _LBuff[i + 18], _LBuff[i + 19], _LBuff[i + 20], _LBuff[i + 21], _LBuff[i + 22], _LBuff[i + 23], _LBuff[i + 24], _LBuff[i + 25], _LBuff[i + 26], _LBuff[i + 27], _LBuff[i + 28] }));
                                            //截取 _LBuff指定market 长度进行解析，剩余拷贝下次循环
                                            if (_LBuff.Count >= len_market + i + 8 + 8 + 13)
                                            { //说明剩余字节可以截断解析
                                                time1 = DateTime.Now;
                                                byte[] buffer_ma = new byte[len_market];
                                                _LBuff.CopyTo(i + 8 + 8 + 13, buffer_ma, 0, len_market);
                                                _LBuff.RemoveRange(0, i + 8 + 8 + 13 + len_market);
                                                string buffer_temp = "";
                                                if (type_market == "@market@")
                                                {
                                                    byte[] buffer_ma_x = Decompress(buffer_ma);//解压数据
                                                    buffer_temp = Encoding.UTF8.GetString(buffer_ma_x);
                                                }
                                                else if (type_market == "@Market@")
                                                {
                                                    buffer_temp = Encoding.UTF8.GetString(buffer_ma);//非解压
                                                }
                                                // 将数据复制到其它数据缓存区，由其它线程做业务逻辑处理 例如:建立一个queue 将数据保存至queue中继续操作.这里演示数据打印屏幕

                                                ss_Market_event0(buffer_temp);

                                                break;
                                            }
                                            else
                                            { //不够长度无法解析，等待后续数据到达 再进行解析
                                                break;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex); }
                }
                else { Thread.Sleep(1); }
            }
        }
        /* </market 数据解析>*/

        /* <tran 数据解析>*/
        public void Byte_decompress_tran0()
        {
            long dic_temp = 1;
            //用于存储剩余未解析的字节数
            List<byte> _LBuff = new List<byte>(0);
            while (true)
            {
                if (dic_temp <= buf_count_tran0)
                {
                    try
                    {
                        byte[] by_temp;
                        buffer_list_tran0.TryDequeue(out by_temp);
                        {
                            //拷贝剩余留下的字节数
                            _LBuff.AddRange(by_temp);
                            dic_temp++;
                            //循环取包头 
                            //备注： 完整包组成 SS_begin + (@tran__@) + 13位数据长 + 数据  （8 + 8 + 13 +数据）
                            string type_market = "";
                            int len_market = 0;
                            DateTime time1 = DateTime.Now;
                            DateTime time2 = DateTime.Now;
                            for (int i = 0; i < _LBuff.Count; i++)
                            {
                                //Console.WriteLine("i:"+i);
                                if (_LBuff.Count >= i + 11)
                                {
                                    byte[] heart_beart = new byte[11] { _LBuff[i], _LBuff[i + 1], _LBuff[i + 2], _LBuff[i + 3], _LBuff[i + 4], _LBuff[i + 5], _LBuff[i + 6], _LBuff[i + 7], _LBuff[i + 8], _LBuff[i + 9], _LBuff[i + 10] };
                                    string heart = Encoding.UTF8.GetString(heart_beart);
                                    if (heart == "Heart_beart")
                                    {
                                        //断线重连更新 20191229
                                        hearttime_tran = DateTime.Now;
                                        //
                                        if (heart_o_c == true)
                                        {
                                            Console.WriteLine("tran0 " + heart + DateTime.Now.ToString("   HH-mm-ss-fff"));
                                        }
                                        _LBuff.RemoveRange(0, i + 11);
                                        //hearttime = DateTime.Now;
                                        break;
                                    }
                                }
                                if (_LBuff.Count >= i + 8 + 8 + 13)
                                { //取出前8位
                                    try
                                    {
                                        byte[] top_8 = new byte[8] { _LBuff[i], _LBuff[i + 1], _LBuff[i + 2], _LBuff[i + 3], _LBuff[i + 4], _LBuff[i + 5], _LBuff[i + 6], _LBuff[i + 7] };
                                        string top_8_name = Encoding.UTF8.GetString(top_8);
                                        if (top_8_name == "SS_begin")
                                        { //说明包头无误，开始获取包长度及包内容解析
                                            //market 类型8位
                                            type_market = Encoding.UTF8.GetString(new byte[8] { _LBuff[i + 8], _LBuff[i + 9], _LBuff[i + 10], _LBuff[i + 11], _LBuff[i + 12], _LBuff[i + 13], _LBuff[i + 14], _LBuff[i + 15] });
                                            //market 长度
                                            //Console.WriteLine(Encoding.UTF8.GetString(new byte[13] { _LBuff[i + 16], _LBuff[i + 17], _LBuff[i + 18], _LBuff[i + 19], _LBuff[i + 20], _LBuff[i + 21], _LBuff[i + 22], _LBuff[i + 23], _LBuff[i + 24], _LBuff[i + 25], _LBuff[i + 26], _LBuff[i + 27], _LBuff[i + 28] }));
                                            //Thread.Sleep(500000);
                                            len_market = int.Parse(Encoding.UTF8.GetString(new byte[13] { _LBuff[i + 16], _LBuff[i + 17], _LBuff[i + 18], _LBuff[i + 19], _LBuff[i + 20], _LBuff[i + 21], _LBuff[i + 22], _LBuff[i + 23], _LBuff[i + 24], _LBuff[i + 25], _LBuff[i + 26], _LBuff[i + 27], _LBuff[i + 28] }));
                                            //截取 _LBuff指定market 长度进行解析，剩余拷贝下次循环
                                            if (_LBuff.Count >= len_market + i + 8 + 8 + 13)
                                            { //说明剩余字节可以截断解析
                                                time1 = DateTime.Now;
                                                byte[] buffer_ma = new byte[len_market];
                                                _LBuff.CopyTo(i + 8 + 8 + 13, buffer_ma, 0, len_market);
                                                _LBuff.RemoveRange(0, i + 8 + 8 + 13 + len_market);
                                                string buffer_temp = "";

                                                if (type_market == "@tran__@")
                                                {
                                                    byte[] buffer_ma_x = Decompress(buffer_ma);//解压数据
                                                    buffer_temp = Encoding.UTF8.GetString(buffer_ma_x);

                                                }
                                                else if (type_market == "@Tran__@")
                                                {
                                                    buffer_temp = Encoding.UTF8.GetString(buffer_ma);//非解压

                                                }
                                                // 将数据复制到其它数据缓存区，由其它线程做业务逻辑处理 例如:建立一个queue 将数据保存至queue中继续操作.这里演示数据打印屏幕
                                                ss_Tran_event0(buffer_temp);


                                                break;
                                            }
                                            else
                                            { //不够长度无法解析，等待后续数据到达 再进行解析
                                                break;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex); }
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }
        /* <tran 数据解析>*/

        /* <order 数据解析>*/
        public void Byte_decompress_order0()
        {
            long dic_temp = 1;
            //用于存储剩余未解析的字节数
            List<byte> _LBuff = new List<byte>(0);
            while (true)
            {
                if (dic_temp <= buf_count_order0)
                {
                    try
                    {
                        byte[] by_temp;
                        buffer_list_order0.TryDequeue(out by_temp);
                        {
                            //拷贝剩余留下的字节数
                            _LBuff.AddRange(by_temp);
                            dic_temp++;
                            //循环取包头 
                            //备注： 完整包组成 SS_begin + (@order_@) + 13位数据长 + 数据  （8 + 8 + 13 +数据）
                            string type_market = "";
                            int len_market = 0;
                            DateTime time1 = DateTime.Now;
                            DateTime time2 = DateTime.Now;
                            for (int i = 0; i < _LBuff.Count; i++)
                            {
                                //Console.WriteLine("i:"+i);
                                if (_LBuff.Count >= i + 11)
                                {
                                    byte[] heart_beart = new byte[11] { _LBuff[i], _LBuff[i + 1], _LBuff[i + 2], _LBuff[i + 3], _LBuff[i + 4], _LBuff[i + 5], _LBuff[i + 6], _LBuff[i + 7], _LBuff[i + 8], _LBuff[i + 9], _LBuff[i + 10] };
                                    string heart = Encoding.UTF8.GetString(heart_beart);
                                    if (heart == "Heart_beart")
                                    {
                                        //断线重连更新 20191229
                                        hearttime_order = DateTime.Now;
                                        //
                                        if (heart_o_c)
                                        {
                                            Console.WriteLine("order0 " + heart + DateTime.Now.ToString("   HH-mm-ss-fff"));
                                        }
                                        _LBuff.RemoveRange(0, i + 11);
                                        //hearttime = DateTime.Now;
                                        break;
                                    }
                                }
                                if (_LBuff.Count >= i + 8 + 8 + 13)
                                { //取出前8位
                                    try
                                    {
                                        byte[] top_8 = new byte[8] { _LBuff[i], _LBuff[i + 1], _LBuff[i + 2], _LBuff[i + 3], _LBuff[i + 4], _LBuff[i + 5], _LBuff[i + 6], _LBuff[i + 7] };
                                        string top_8_name = Encoding.UTF8.GetString(top_8);
                                        if (top_8_name == "SS_begin")
                                        { //说明包头无误，开始获取包长度及包内容解析
                                            //market 类型8位
                                            type_market = Encoding.UTF8.GetString(new byte[8] { _LBuff[i + 8], _LBuff[i + 9], _LBuff[i + 10], _LBuff[i + 11], _LBuff[i + 12], _LBuff[i + 13], _LBuff[i + 14], _LBuff[i + 15] });
                                            //market 长度
                                            //Console.WriteLine(Encoding.UTF8.GetString(new byte[13] { _LBuff[i + 16], _LBuff[i + 17], _LBuff[i + 18], _LBuff[i + 19], _LBuff[i + 20], _LBuff[i + 21], _LBuff[i + 22], _LBuff[i + 23], _LBuff[i + 24], _LBuff[i + 25], _LBuff[i + 26], _LBuff[i + 27], _LBuff[i + 28] }));
                                            len_market = int.Parse(Encoding.UTF8.GetString(new byte[13] { _LBuff[i + 16], _LBuff[i + 17], _LBuff[i + 18], _LBuff[i + 19], _LBuff[i + 20], _LBuff[i + 21], _LBuff[i + 22], _LBuff[i + 23], _LBuff[i + 24], _LBuff[i + 25], _LBuff[i + 26], _LBuff[i + 27], _LBuff[i + 28] }));
                                            //截取 _LBuff指定market 长度进行解析，剩余拷贝下次循环
                                            if (_LBuff.Count >= len_market + i + 8 + 8 + 13)
                                            { //说明剩余字节可以截断解析
                                                time1 = DateTime.Now;
                                                byte[] buffer_ma = new byte[len_market];
                                                _LBuff.CopyTo(i + 8 + 8 + 13, buffer_ma, 0, len_market);
                                                _LBuff.RemoveRange(0, i + 8 + 8 + 13 + len_market);
                                                string buffer_temp = "";
                                                if (type_market == "@order_@")
                                                {
                                                    byte[] buffer_ma_x = Decompress(buffer_ma);//解压数据
                                                    buffer_temp = Encoding.UTF8.GetString(buffer_ma_x);
                                                }
                                                else if (type_market == "@Order_@")
                                                {
                                                    buffer_temp = Encoding.UTF8.GetString(buffer_ma);//非解压
                                                }
                                                // 将数据复制到其它数据缓存区，由其它线程做业务逻辑处理 例如:建立一个queue 将数据保存至queue中继续操作.这里演示数据打印屏幕
                                                ss_Order_event0(buffer_temp);

                                                break;
                                            }
                                            else
                                            { //不够长度无法解析，等待后续数据到达 再进行解析
                                                break;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex); }
                }
                else { Thread.Sleep(1); }
            }
        }
        /* <order 数据解析>*/

        /* <queue 数据解析>*/
        public void Byte_decompress_queue0()
        {
            long dic_temp = 1;
            //用于存储剩余未解析的字节数
            List<byte> _LBuff = new List<byte>(0);
            while (true)
            {
                if (dic_temp <= buf_count_queue0)
                {
                    try
                    {
                        byte[] by_temp;
                        buffer_list_queue0.TryDequeue(out by_temp);
                        {
                            //拷贝剩余留下的字节数
                            _LBuff.AddRange(by_temp);
                            dic_temp++;
                            //循环取包头 
                            //备注： 完整包组成 SS_begin + (@queue_@) + 13位数据长 + 数据  （8 + 8 + 13 +数据）
                            string type_market = "";
                            int len_market = 0;
                            DateTime time1 = DateTime.Now;
                            DateTime time2 = DateTime.Now;
                            for (int i = 0; i < _LBuff.Count; i++)
                            {
                                //Console.WriteLine("i:"+i);
                                if (_LBuff.Count >= i + 11)
                                {
                                    byte[] heart_beart = new byte[11] { _LBuff[i], _LBuff[i + 1], _LBuff[i + 2], _LBuff[i + 3], _LBuff[i + 4], _LBuff[i + 5], _LBuff[i + 6], _LBuff[i + 7], _LBuff[i + 8], _LBuff[i + 9], _LBuff[i + 10] };
                                    string heart = Encoding.UTF8.GetString(heart_beart);
                                    if (heart == "Heart_beart")
                                    {
                                        //断线重连更新 20191229
                                        hearttime_queue = DateTime.Now;
                                        //
                                        if (heart_o_c)
                                        {
                                            Console.WriteLine("queue0 " + heart + DateTime.Now.ToString("   HH-mm-ss-fff"));
                                        }
                                        //Console.WriteLine("queue0 " + heart + DateTime.Now.ToString("   HH-mm-ss-fff"));
                                        _LBuff.RemoveRange(0, i + 11);
                                        //hearttime = DateTime.Now;
                                        break;
                                    }
                                }
                                if (_LBuff.Count >= i + 8 + 8 + 13)
                                { //取出前8位
                                    try
                                    {
                                        byte[] top_8 = new byte[8] { _LBuff[i], _LBuff[i + 1], _LBuff[i + 2], _LBuff[i + 3], _LBuff[i + 4], _LBuff[i + 5], _LBuff[i + 6], _LBuff[i + 7] };
                                        string top_8_name = Encoding.UTF8.GetString(top_8);
                                        if (top_8_name == "SS_begin")
                                        { //说明包头无误，开始获取包长度及包内容解析
                                            //market 类型8位
                                            type_market = Encoding.UTF8.GetString(new byte[8] { _LBuff[i + 8], _LBuff[i + 9], _LBuff[i + 10], _LBuff[i + 11], _LBuff[i + 12], _LBuff[i + 13], _LBuff[i + 14], _LBuff[i + 15] });
                                            //market 长度
                                            //Console.WriteLine(Encoding.UTF8.GetString(new byte[13] { _LBuff[i + 16], _LBuff[i + 17], _LBuff[i + 18], _LBuff[i + 19], _LBuff[i + 20], _LBuff[i + 21], _LBuff[i + 22], _LBuff[i + 23], _LBuff[i + 24], _LBuff[i + 25], _LBuff[i + 26], _LBuff[i + 27], _LBuff[i + 28] }));
                                            len_market = int.Parse(Encoding.UTF8.GetString(new byte[13] { _LBuff[i + 16], _LBuff[i + 17], _LBuff[i + 18], _LBuff[i + 19], _LBuff[i + 20], _LBuff[i + 21], _LBuff[i + 22], _LBuff[i + 23], _LBuff[i + 24], _LBuff[i + 25], _LBuff[i + 26], _LBuff[i + 27], _LBuff[i + 28] }));
                                            //截取 _LBuff指定market 长度进行解析，剩余拷贝下次循环
                                            if (_LBuff.Count >= len_market + i + 8 + 8 + 13)
                                            { //说明剩余字节可以截断解析
                                                time1 = DateTime.Now;
                                                byte[] buffer_ma = new byte[len_market];
                                                _LBuff.CopyTo(i + 8 + 8 + 13, buffer_ma, 0, len_market);
                                                _LBuff.RemoveRange(0, i + 8 + 8 + 13 + len_market);
                                                string buffer_temp = "";
                                                if (type_market == "@queue_@")
                                                {
                                                    byte[] buffer_ma_x = Decompress(buffer_ma);//解压数据
                                                    buffer_temp = Encoding.UTF8.GetString(buffer_ma_x);
                                                }
                                                else if (type_market == "@Queue_@")
                                                {
                                                    buffer_temp = Encoding.UTF8.GetString(buffer_ma);//非解压
                                                }
                                                // 将数据复制到其它数据缓存区，由其它线程做业务逻辑处理 例如:建立一个queue 将数据保存至queue中继续操作.这里演示数据打印屏幕
                                                ss_Queue_event0(buffer_temp);
                                                
                                                break;
                                            }
                                            else
                                            { //不够长度无法解析，等待后续数据到达 再进行解析
                                                break;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex); }
                }
                else { Thread.Sleep(1); }
            }
        }
        /* <queue 数据解析>*/

        /* <解压数组>*/
        public static byte[] Decompress(byte[] data)
        {
            MemoryStream stream = new MemoryStream();

            GZipStream gZipStream = new GZipStream(new MemoryStream(data), CompressionMode.Decompress);

            byte[] bytes = new byte[1024 * 102];
            int n;
            while ((n = gZipStream.Read(bytes, 0, bytes.Length)) != 0)
            {
                stream.Write(bytes, 0, n);
            }
            gZipStream.Close();
            return stream.ToArray();
        }
    }
}
