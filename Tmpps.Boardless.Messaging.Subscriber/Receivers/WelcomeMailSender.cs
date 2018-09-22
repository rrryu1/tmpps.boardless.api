using System;
using System.Threading;
using System.Threading.Tasks;
using Tmpps.Boardless.Domain.Account.Models;
using Tmpps.Infrastructure.Common.Messaging.Interfaces;

namespace Tmpps.Boardless.Messaging.Subscriber.Receivers
{
    public class WelcomeMailSender : IMessageReceiver<WelcomeMailArgs>
    {
        private WelcomeMailArgs args;
        private CancellationTokenSource source;

        public WelcomeMailSender(
            CancellationTokenSource source,
            WelcomeMailArgs args)
        {
            this.source = source;
            this.args = args;
        }

        public async Task<int> ExecuteAsync()
        {
            Console.WriteLine($"{this.args.Email} {this.args.Name}");
            return await Task.FromResult(0);
        }
    }
}