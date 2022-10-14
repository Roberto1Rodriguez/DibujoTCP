using DibujoTCP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DibujoTCP.Services
{
    public class ClientService
    {
       TcpClient client=new TcpClient();
       public ClientService(string ip, Rectangulo rect)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), 20000);
            client.Connect(ipe);
            Thread.Sleep(1000);
            Enviar(rect);

        }

        public void Enviar(Rectangulo rect)
        {
            try
            {
                var json = JsonConvert.SerializeObject(rect);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                var stream=client.GetStream();
                stream.Write(buffer, 0, buffer.Length); 
            }
            catch (Exception)
            {

                client.Close();
            }
        }
        public void Cerrar()
        {
            client.Close(); 
        }
    }
}
