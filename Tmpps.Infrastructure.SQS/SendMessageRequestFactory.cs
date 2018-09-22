using System;
using Amazon.SQS.Model;
using Tmpps.Infrastructure.Common.Foundation.Exceptions;
using Tmpps.Infrastructure.Common.Messaging;
using Tmpps.Infrastructure.Common.Messaging.Interfaces;
using Tmpps.Infrastructure.SQS.Interfaces;
using Newtonsoft.Json;

namespace Tmpps.Infrastructure.SQS
{
    public class SendMessageRequestFactory : ISendMessageRequestFactory
    {
        private ISQSConfig config;
        private ISQSHelper sqsHelper;

        public SendMessageRequestFactory(ISQSConfig config, ISQSHelper sqsHelper)
        {
            this.config = config;
            this.sqsHelper = sqsHelper;
        }
        public SendMessageRequest CreateSendMessage(object message, Type type, int receiveCount = 0)
        {
            var json = JsonConvert.SerializeObject(message);
            return this.CreateSendMessage(json, type, receiveCount);
        }
        public SendMessageRequest CreateSendMessage(string message, Type type, int receiveCount = 0)
        {
            if (!config.SQSQueueSettingDictionary.TryGetValue(type.Name, out var queueSetting))
            {
                queueSetting = config.DefaultSQSQueueSetting;
            }
            var queueUrl = this.CreateUrl(queueSetting, receiveCount);
            var sendMessageRequest = new SendMessageRequest(queueUrl, message)
            {
                DelaySeconds = this.sqsHelper.ComputeDelaySeconds(queueSetting.DelayType, queueSetting.FirstDelaySeconds, receiveCount),
                MessageAttributes = this.sqsHelper.CreateMessageAttributes(type, receiveCount),
            };
            return sendMessageRequest;
        }

        private string CreateUrl(SQSQueueSetting queueSetting, int receiveCount)
        {
            return queueSetting.DelayType != SQSDelayType.FirstTimeOnly &&
                !string.IsNullOrEmpty(queueSetting.DeadLetterQueueUrl) &&
                queueSetting.MaxReceiveCount > 0 &&
                queueSetting.MaxReceiveCount <= receiveCount ? queueSetting.DeadLetterQueueUrl : queueSetting.QueueUrl;
        }

        public SendMessageRequest CreateSendMessage<T>(T message, int receiveCount = 0) where T : class
        {
            return this.CreateSendMessage(message, typeof(T), receiveCount);
        }
    }
}