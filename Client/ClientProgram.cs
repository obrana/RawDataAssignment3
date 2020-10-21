using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Globalization;

namespace Client
{
    public class ClientProgram
    {
        public static double DateToUnixTimestamp(string date)
        {
            const string format = "dd/MM/yyyy";
            var provider = CultureInfo.InvariantCulture;
            var value = DateTime.ParseExact(date, format, provider);
            return (value - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public static void Main(string[] args)
        {

            using var client = new TcpClient();
            client.Connect(IPAddress.Loopback, 5000);

            var stream = client.GetStream();

            Console.WriteLine("Method ?");
            var method = Console.ReadLine();
            Console.WriteLine("Path ?");
            var path = Console.ReadLine();
            Console.WriteLine("Date (dd/mm/yyyy) ?");
            var date = Console.ReadLine();
            Console.WriteLine("Body ?");
            var body = Console.ReadLine();


            var data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new
            {
                Method = method,
                Path = path,
                Date = DateToUnixTimestamp(date),
                Body = body
            }));

            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", data);

            data = new byte[client.ReceiveBufferSize];

            var cnt = stream.Read(data, 0, data.Length);
            var msg = Encoding.UTF8.GetString(data, 0, cnt);

            Console.WriteLine("\n press Enter to continue..");
            Console.Read();
        }
    }
}
