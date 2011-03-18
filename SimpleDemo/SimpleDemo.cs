using System;
using LinccerApi;

namespace SimpleDemo
{

    class Data
    {
        public Data(){
        }
        
        public string Message { get; set; }
        public string Timestamp {
            get {
                TimeSpan ts = DateTime.UtcNow - new DateTime (1970, 1, 1, 0, 0, 0);
                
                return ts.TotalSeconds.ToString();
            }
        }
        
    }

    class SimpleDemo
    {
        public static void Main (string[] args)
        {
            Linccer linccer = new Linccer ("Demo App");
            linccer.OnGpsChanged (23.44, 102.13, 100);
            linccer.Share ("one-to-one", new Data { Message = "Hello world" });
            Data receivedMessage = linccer.Receive<Data>();
            System.Console.WriteLine(receivedMessage.ToString());

        }
    }
}
