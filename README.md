# 沪深level2 行情数据
level2
mail 994143128@qq.com

                    //十档行情推送
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
                    *///逐笔成交推送
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
                    *///逐笔委托推送
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
                    //期权行情推送
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
