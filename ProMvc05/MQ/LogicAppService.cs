using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMvc05.MQ
{
    public interface ILogicAppService
    {
        Task<int> AddOrder(string whse, string id, string nextstep);
        
    }
    public class LogicAppService : ILogicAppService
    {
        //这个主要是学习下topicClient，
        //要去StartUp里注册下服务，，，，，这是一定的

        private ITopicClient _topicClient = null;
        public static string ServiceBusConnectingString { set; get; }
        public static string TopicName { set; get; }

        public ITopicClient topicClient
        {
            get
            {
                if (_topicClient == null)
                {
                    _topicClient = new TopicClient(ServiceBusConnectingString, TopicName);
                }
                return _topicClient;
            }
            set
            {
                _topicClient = value;
            }
        }
        

        public  async Task<int> AddOrder(string whse, string id, string nextstep)
        {
            int i = 0;
            try
            {
                string messageBody = $"{id},{whse},{nextstep}";
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                message.Label = "下一步";
                await topicClient.SendAsync(message);////这里就是带着信息，去servicebus触发下一个阶段的label了
                i = 1;
            }
            catch (Exception ex)
            {
                i = 0;
            }
            finally
            {
                if (topicClient != null)
                    await topicClient.CloseAsync();
            }
            return i;
        }
    }
}
