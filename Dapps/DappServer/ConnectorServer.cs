using System;
using System.Text;
using System.Threading;
using LunarLabs.WebServer.Core;
using LunarLabs.WebServer.HTTP;
using LunarLabs.Parser;
using LunarLabs.Parser.JSON;
using LunarLabs.WebSockets;
using Phantasma.SDK;
using Phantasma.Domain;

namespace Phantasma.Dapps
{
    class ConnectorServer
    {
        static void RunConnector()
        {
            var settings = ServerSettings.DefaultSettings();
            settings.Port = WalletLink.WebSocketPort;

            var server = new HTTPServer(settings, ConsoleLogger.Write);

            Console.WriteLine("Starting Phantasma Sample connector at port " + settings.Port);

            /*
            server.Get("/authorize/{dapp}", (request) =>
            {
                return link.Execute(request.url);
            });

            server.Get("/getAccount/{dapp}/{token}", (request) =>
            {
                return link.Execute(request.url);
            });

            server.Get("/invokeScript/{script}/{dapp}/{token}", (request) =>
            {
                return link.Execute(request.url);
            });*/

            var api = new PhantasmaAPI("http://localhost:7078");
            var link = new SampleConnector(api);

            server.WebSocket("/phantasma", (socket) =>
            {
                while (socket.IsOpen)
                {
                    var msg = socket.Receive();

                    if (msg.CloseStatus == WebSocketCloseStatus.None)
                    {
                        var str = Encoding.UTF8.GetString(msg.Bytes);

                        link.Execute(str, (id, root, success) =>
                        {
                            root.AddField("id", id);
                            root.AddField("success", success);

                            var json = JSONWriter.WriteToString(root);
                            socket.Send(json);
                        });

                    }
                }
            });

            server.Run();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Initializing dapp server");

            // either parse the settings from the program args or initialize them manually
            var settings = ServerSettings.Parse(args);
            settings.Port = 4040;

            var server = new HTTPServer(settings, ConsoleLogger.Write);

            Console.WriteLine("Starting Phantasma Dapp samples at port " + settings.Port);

            server.Get("/", (request) =>
            {
                return HTTPResponse.FromString("Hello world!");
            });

            Console.CancelKeyPress += delegate {
                server.Stop();
            };

            // uncomment this line to enable sample connector, comment if you want to use Poltergeist or Phantom
            new Thread(() => RunConnector()).Start();

            server.Run();
        }
    }
}
