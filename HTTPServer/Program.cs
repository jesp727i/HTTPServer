using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;


namespace HTTPServer
{
    class Program
    {
        static string content;
        static string Time { get { return DateTime.Now.ToString("HH:mm:ss");}}
        static string Date { get { return DateTime.Now.ToString("dd/MM-yyyy"); } }
        static void Main(string[] args)
        {
            TcpListener listner = new TcpListener(IPAddress.Any ,10000);
            listner.Start();

            while (true)
            {
                TcpClient client = listner.AcceptTcpClient();
                StreamReader sr = new StreamReader(client.GetStream());
                StreamWriter sw = new StreamWriter(client.GetStream());
                try
                {
                    string request = sr.ReadLine();
                    Console.WriteLine(request);
                    string[] tokens = request.Split(' ');
                    string command = tokens[1];
                    switch (command)
                    {
                        case "/Time":
                            content = Time;
                            break;
                        case "/Date":
                            content = Date;
                            break;
                        default:
                            content = "Frontpage";
                            break;
                            
                    }

                    sw.WriteLine("HTTP/1.1 200 OK");
                    sw.WriteLine("Content-Type: text/plain");
                    sw.WriteLine("Content-Length: " + content.Length);
                    sw.WriteLine();
                    sw.WriteLine(content);
                    sw.Flush();
                    client.Close();
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
