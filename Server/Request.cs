using System;
namespace Server
{

    public class Request
    {
        public string method { get; set; }
        public string path { get; set; }

        public string date { get; set; }

        public string body { get; set; }
    }

}