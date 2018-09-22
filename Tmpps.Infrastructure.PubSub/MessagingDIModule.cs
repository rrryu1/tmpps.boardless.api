using Tmpps.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Tmpps.Infrastructure.Common.Messaging.Interfaces;

namespace Tmpps.Infrastructure.PubSub
{
    public class MessagingDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<MessageSender>(x => x.As<IMessageSender>().SingleInstance());
            builder.RegisterType<Subscriber>(x => x.As<IMessageSubscriber>().SingleInstance());
        }
    }
}