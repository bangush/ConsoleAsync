using System;
using System.Messaging;

namespace MSMQClient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (MessageQueue.Exists(@".\Private$\LearningHardMSMQ"))
            {
                // 创建消息队列对象
                using (MessageQueue mq = new MessageQueue(@".\Private$\LearningHardMSMQ"))
                {
                    // 设置消息队列的格式化器
                    mq.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
                    foreach (Message msg in mq.GetAllMessages())
                    {
                        Console.WriteLine("Received Private Message is: {0}", msg.Body);
                    }

                    Message firstmsg = mq.Receive(); // 获得消息队列中第一条消息
                    Console.WriteLine("Received The first Private Message is: {0}", firstmsg.Body);
                }
            }
            Console.Read();
        }
    }
}
