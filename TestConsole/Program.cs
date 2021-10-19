using Openfin.Desktop;
using System;

namespace DOTNETChannelAPI
{
    class Program
    {
        public static Runtime _runtime;

        static void Main(string[] args)
        {
            MakeProvider();
            Console.ReadLine();
        }

        public static void MakeProvider()
        {
            Action connected = new Action(ConnectChannel);
            _runtime = Runtime.GetRuntimeInstance(new RuntimeOptions
            {
                Version = "stable"
            });
            _runtime.Connect(connected);
        }

        public static void ConnectChannel()
        {
            var providerBus = _runtime.InterApplicationBus.Channel.CreateProvider("channelName");

            providerBus.Opened += (identity, payload) =>
            {
                Console.WriteLine("Client connection request identity: ", identity?.ToString());
                Console.WriteLine("Client connection request payload: ", payload?.ToString());
            };

            providerBus.RegisterTopic<Object>("topic", (payload, client) =>
            {
                Console.WriteLine("Action dispatched by client: ", client.RemoteEndpoint);
                Console.WriteLine("Payload sent in dispatch: ", payload?.ToString());
            });

        }
    }
}
