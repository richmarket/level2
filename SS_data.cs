using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ss_test
{
    class SS_data
    {
        public static  long num = 0;
        public static string str = System.Windows.Forms.Application.StartupPath;
        
        //指数行情推送
        public static void Index_Data(string Index)
        {
            ////buffer_tempArr是解析后的数据 数组 每条内容由于#分割 item内容由|分割
            string[] buffer_tempArr = Index.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
            {
                //buffer_tempArr为所获取的数据
                //可以拷贝到其他内存区域进行计算或者在此线程直接运算
                foreach (string temp in buffer_tempArr)
                {
                    string[] temp_x = temp.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    if (temp_x.Length > 1 && temp_x[1] == "931087")//为上证指数
                    {
                        Console.WriteLine("INDEX code:" + temp_x[0] + "  price_now:" + temp_x[9] + "  time:" + temp_x[3] + "  " + DateTime.Now.ToString("HH-mm-ss"));
                    }
                    //if (temp_x.Length > 1 )//为上证指数
                    //{
                    //    using (StreamWriter sw1 = new StreamWriter(string.Format(str + @"\{0}\index\", DateTime.Now.ToString("yyyyMMdd")) + "" + temp_x[1] + "_" + DateTime.Now.ToString("yyyyMMdd") + ".csv", true))
                    //    {
                    //        sw1.WriteLine(temp.Replace("|", ",") + "," + DateTime.Now.ToString("  HH-mm-ss-fff"));
                    //        sw1.Flush();
                    //        sw1.Close();
                    //    }
                    //}
                    
                    //Console.WriteLine(temp);
                    /*指数名称	指数代码
                      上证指数	000001
                      上证Ａ股	000002
                      上证Ｂ股	000003
                      工业指数	000004
                      商业指数	000005
                      地产指数	000006
                      公用指数	000007
                      综合事业	000008
                      上证180 	000010
                      基金指数	000011
                      国债指数	000012
                      企债指数	000013
                      上证５０	000016
                      红利指数	000015
                      沪深300 	000300*/


                    /*temp_x index行情快照结构
                    temp_x[0] 000002.SZ  标准代码
                    temp_x[1] 000002 代码
                    temp_x[2] 20170101 日期
                    temp_x[3] 93000000 时间
                    temp_x[4] 开盘指数
                    temp_x[5] 最高指数
                    temp_x[6] 最低指数
                    temp_x[7] 最新指数
                    temp_x[8] 交易量
                    temp_x[9] 成交金额
                    temp_x[10] 前盘指数                                                      
                    */
                }
            }

        }

        //期权行情推送
        public static void Option_Data(string Option)
        {
            ////buffer_tempArr是解析后的数据 数组 每条内容由于#分割 item内容由|分割
            string[] buffer_tempArr = Option.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
            {
                //buffer_tempArr为所获取的数据
                //可以拷贝到其他内存区域进行计算或者在此线程直接运算
                foreach (string temp in buffer_tempArr)
                {
                    string[] temp_x = temp.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    if (temp_x.Length > 1 && temp_x[1] == "10002222")//
                    {
                        Console.WriteLine("OPTION code:" + temp_x[0] + "  time:" + temp_x[3] + "  " + DateTime.Now.ToString("HH-mm-ss fff"));
                    }
                    //Console.WriteLine(temp);

                    /*temp_x option行情快照结构
                    temp_x[0] 000002.SZ  标准代码
                    temp_x[1] 000002 代码
                    temp_x[2] 20170101 日期
                    temp_x[3] 93000000 时间
                    temp_x[4] 阶段标志
                    temp_x[5] 涨停价
                    temp_x[6] 跌停价
                    temp_x[7] 成交总量
                    temp_x[8] 成交总金额
                    
                    temp_x[9] 最新价
                    temp_x[10] 开盘价
                    temp_x[11] 最高价
                    temp_x[12] 最低价
                    temp_x[13] 昨结算价
                    temp_x[14] 持仓总量
                    temp_x[15-19] 买1-5价
                    temp_x[20-24] 买1-5量
                    temp_x[25-29] 卖1-5价
                    temp_x[30-34] 卖1-5量
                     */
                }
            }

        }
        
        //十档行情推送 
        public static void Market_Data0(string Market0)
        {//buffer_tempArr是解析后的数据 数组 每条内容由于#分割 item内容由|分割
            string[] buffer_tempArr = Market0.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
            //Console.WriteLine("type:" + type_market + "  length:" + buffer_tempArr.Length + "   " + "  " + DateTime.Now.ToString("HH-mm-ss"));
            {//十档行情快照
                //buffer_tempArr为所获取的数据
                //可以拷贝到其他内存区域进行计算或者在此线程直接运算
                //(注:market0与market1为双线发送,合并才为完整数据)
                foreach (string temp in buffer_tempArr)
                {
                    string[] temp_x = temp.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    if (temp_x.Length > 10)// && (temp_x[1] == "600000" || temp_x[1] == "000002" || temp_x[1] == "000725" || Program.kezhuan_code.ContainsKey(temp_x[1])))
                    {
                        // Console.WriteLine("MARKET code:" + temp_x[0] + "  price_now:" + temp_x[9] + "  time:" + temp_x[3] + " " + num + DateTime.Now.ToString("  HH-mm-ss-fff"));
                        using (StreamWriter sw1 = new StreamWriter(string.Format(str + @"\{0}\market\", DateTime.Now.ToString("yyyyMMdd")) + "" + temp_x[1] + "_" + DateTime.Now.ToString("yyyyMMdd") + ".csv", true))
                        {
                            sw1.WriteLine(temp.Replace("|", ",") + "," + DateTime.Now.ToString("  HH-mm-ss-fff"));
                            //sw1.Flush();
                            //sw1.Close();
                        }//把下单的记录保存到order的日志里
                    }
                    if (temp_x.Length > 1 && temp_x[1] == "000002")
                    {
                        //Console.WriteLine(temp_x.Length);
                        Console.WriteLine("code:" + temp_x[0] + "  time:" + temp_x[3] + DateTime.Now.ToString("  HH-mm-ss-fff"));
                    }
                    //Console.WriteLine(temp);
                    /*temp_x market行情快照结构
                    temp_x[0] 000002.SZ  标准代码
                    temp_x[1] 000002 代码
                    temp_x[2] 20170101 日期
                    temp_x[3] 93000000 时间
                    temp_x[4] 状态
                    temp_x[5] 前收盘价
                    temp_x[6] 开盘价
                    temp_x[7] 最高价
                    temp_x[8] 最低价
                    temp_x[9] 最新价
                    temp_x[10]-temp_x[19] 申卖价1-10
                    temp_x[20]-temp_x[29] 申卖量1-10
                    temp_x[30]-temp_x[39] 申买价1-10
                    temp_x[40]-temp_x[49] 申买量1-10
                    temp_x[50] 成交笔数
                    temp_x[51] 成交总量
                    temp_x[52] 成交总金额
                    temp_x[53] 委托买入总量
                    temp_x[54] 委托卖出总量
                    temp_x[55] 加权平均委买价格
                    temp_x[56] 加权平均委卖价格
                    temp_x[57] 涨停价
                    temp_x[58] 跌停价                                                        
                    */
                }
            }
        }   

        //委托队列推送 
        public static void Queue_Data0(string Queue0)
        {
            //buffer_tempArr是解析后的数据 数组 每条内容由于#分割 item内容由|分割
            string[] buffer_tempArr = Queue0.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
            //Console.WriteLine("type:" + type_market + "  length:" + buffer_tempArr.Length + "   " + "  " + DateTime.Now.ToString("HH-mm-ss"));
            {//委托队列
                foreach (string temp in buffer_tempArr)
                {
                    string[] temp_x = temp.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    /*temp_x queue 委托队列结构
                    temp_x[0] 000002.SZ  标准代码
                    temp_x[1] 000002 代码
                    temp_x[2] 20170101 日期
                    temp_x[3] 93000000 时间
                    temp_x[4] 卖一price
                    temp_x[5] 卖一队列数
                    temp_x[6] 买一price
                    temp_x[7] 买一队列数
                    temp_x[8]-temp_x[107] 订单明细 sell_queue0-sell_queue50  buy_queue0-buy_queue50                                                     
                    */
                    if (temp_x.Length > 1 && temp_x[1] == "000002")
                    {
                        //Console.WriteLine(temp_x.Length);
                        Console.WriteLine("QUEUE code:" + temp_x[0] + temp);
                       //Console.WriteLine("code:" + temp_x[0] + "  time:" + temp_x[3]);
                    }
                    //Console.WriteLine(temp);
                }
            }
        }    

        //逐笔成交推送 
        public static void Tran_Data0(string Tran0)
        { //buffer_tempArr是解析后的数据 数组 每条内容由于#分割 item内容由|分割
            string[] buffer_tempArr = Tran0.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
            //Console.WriteLine("type:" + type_market + "  length:" + buffer_tempArr.Length + "   " + "  " + DateTime.Now.ToString("HH-mm-ss"));
            {//逐笔成交
                //buffer_tempArr为所获取的数据
                //可以拷贝到其他内存区域进行计算或者在此线程直接运算
                //(注:tran0与tran1为双线发送,合并才为完整数据)
                foreach (string temp in buffer_tempArr)
                {
                    string[] temp_x = temp.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    /*temp_x tran 逐笔成交结构
                    temp_x[0] 000002.SZ  标准代码
                    temp_x[1] 000002 代码
                    temp_x[2] 20170101 日期
                    temp_x[3] 93000000 时间
                    temp_x[4] 成交编号
                    temp_x[5] 成交价格
                    temp_x[6] 成交数量
                    temp_x[7] 成交金额
                    temp_x[8] 买卖方向(0成交方向不明，买方成交:'1',卖方成交'2')
                    temp_x[9] 成交类别
                    temp_x[10] 成交代码
                    temp_x[11]叫卖方委托序号
                    temp_x[12]叫买方委托序号                                                      
                    */
                    if (temp_x.Length > 1 && temp_x[1][0] == '1' && temp_x[1][1] == '2')
                    {
                        //Console.WriteLine("TRAN code:" + temp_x[0] + "  time:" + temp_x[3] + DateTime.Now.ToString("  HH-mm-ss-fff") + "  cj_price:" + temp_x[5] + "  cj_num:" + temp_x[6]);
                        Console.WriteLine(temp);
                    }
                    //if (temp_x.Length > 1)
                    //{
                    //    using (StreamWriter sw1 = new StreamWriter(string.Format(str + @"\{0}\tran\", DateTime.Now.ToString("yyyyMMdd")) + "" + temp_x[1] + "_" + DateTime.Now.ToString("yyyyMMdd") + ".csv", true))
                    //    {
                    //        sw1.WriteLine(temp.Replace("|", ",") + "," + DateTime.Now.ToString("  HH-mm-ss-fff"));
                    //        sw1.Flush();
                    //        sw1.Close();
                    //    }//把下单的记录保存到order的日志里
                    //}
                    //Console.WriteLine(temp);
                }
            }
        }     

        //逐笔委托推送 
        public static void Order_Data0(string Order0)
        { //buffer_tempArr是解析后的数据 数组 每条内容由于#分割 item内容由|分割
            string[] buffer_tempArr = Order0.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
            //Console.WriteLine("type:" + type_market + "  length:" + buffer_tempArr.Length + "   " + "  " + DateTime.Now.ToString("HH-mm-ss"));
            //逐笔委托
            //buffer_tempArr为所获取的数据
            //可以拷贝到其他内存区域进行计算或者在此线程直接运算
            //(注:order0与order1为双线发送,合并才为完整数据)
            foreach (string temp in buffer_tempArr)
            {
                string[] temp_x = temp.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                /*temp_x queue 委托队列结构
                temp_x[0] 000002.SZ  标准代码
                temp_x[1] 000002 代码
                temp_x[2] 20170101 日期
                temp_x[3] 93000000 时间
                temp_x[4] 委托号 
                temp_x[5] 委托价格
                temp_x[6] 委托数量
                temp_x[7] 委托类别
                temp_x[8] 委托代码                                           
                */
                if (temp_x.Length > 1 && temp_x[1] == "000002")
                {
                    //Console.WriteLine(temp_x.Length);
                    Console.WriteLine("ORDER: code:" + temp_x[0] + "  time:" + temp_x[3] + DateTime.Now.ToString("  HH-mm-ss-fff"));
                }
                //if (temp_x.Length > 1 )
                //{
                //    using (StreamWriter sw1 = new StreamWriter(string.Format(str + @"\{0}\order\", DateTime.Now.ToString("yyyyMMdd")) + "" + temp_x[1] + "_" + DateTime.Now.ToString("yyyyMMdd") + ".csv", true))
                //    {
                //        sw1.WriteLine(temp.Replace("|", ",") + "," + DateTime.Now.ToString("  HH-mm-ss-fff"));
                //        sw1.Flush();
                //        sw1.Close();
                //    }//把下单的记录保存到order的日志里
                //}
            }

        }

       



    }
}
