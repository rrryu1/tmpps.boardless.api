using Tmpps.Boardless.Cli.Configuration;

namespace Tmpps.Boardless.Messaging.Subscriber
{
    class Program
    {
        static int Main(string[] args)
        {
            return new Startup(args).Execute();
        }
    }
}