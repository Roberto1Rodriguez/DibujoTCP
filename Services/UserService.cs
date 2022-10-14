using DibujoTCP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace DibujoTCP.Services
{
    public class UserService
    {
        public TcpListener? server;
        List<TcpClient> clients = new List<TcpClient>();
        public void Iniciar()
        {
            if (server == null)
            {
                IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 20000);
                server = new TcpListener(ipe);
                Thread hilo1 = new Thread(new ThreadStart(Escuchar));
                hilo1.Start();
               
            }
           
        }

        void Escuchar()
        {
            if (server != null)
            {
                try
                {
                    server.Start();
                    while (server.Server.IsBound && server != null)
                    {
                        var clienteNuevo = server.AcceptTcpClient();
                        clients.Add(clienteNuevo);
                        Thread hiloRecibir = new Thread(new ParameterizedThreadStart(Recibir));
                        hiloRecibir.Start(clienteNuevo);

                    }
                }
                catch (Exception)
                {

                } 
            }
        }
    
        public event Action<Rectangulo>? UsuarioConectado;
        
       public void Recibir(object? tcpClient)
        {
            try
            {



                if (tcpClient != null)
                {
                    TcpClient cliente = (TcpClient)tcpClient;
                    var stream = cliente.GetStream();
                    while (cliente.Connected)
                    {
                        if (cliente.Available > 0)//si hay algun byte disponible
                        {
                            byte[] buffer = new byte[cliente.Available];
                            stream.Read(buffer, 0, buffer.Length);

                            var rect = JsonConvert.DeserializeObject<Rectangulo>(System.Text.Encoding.UTF8.GetString(buffer));
                            if (rect != null)
                            {
                                UsuarioConectado?.Invoke(rect);
                            }
                        }
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception)
            {

            }

        }
        public void Detener()
        {
            if (server != null)
            {
                server.Stop();
                server = null;
            }
        }
    }
}
