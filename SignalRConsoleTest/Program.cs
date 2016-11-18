using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace SignalRConsoleTest
{
    class Program
    {
        static DateTime sendtime;

        static void Main(string[] args)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            var hubConnection = new HubConnection("https://<yourwebserveraddress>.azurewebsites.net/");
            IHubProxy streamHub = hubConnection.CreateHubProxy("BlockTransferHub");
            streamHub.On<string,string>("broadcastMessage", (a,b) => {
                Console.WriteLine("human message {0} {1}", a,b);
                Console.WriteLine("delay {0} ms", (DateTime.Now - sendtime).TotalMilliseconds);
            });
            hubConnection.Start().Wait();
            Task t = new Task(async() =>
            {
                while (true)
                {
                    Console.WriteLine(".");
                    sendtime = DateTime.Now;
                    await streamHub.Invoke("send", "me", "this");

                    await Task.Delay(1000);
                }
            });
            t.Start();
            while (true) Task.Delay(1000).Wait();
        }
    }
}
