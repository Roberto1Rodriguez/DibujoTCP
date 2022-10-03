using DibujoTCP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DibujoTCP.Services
{
    public class UserService
    {
        public TcpListener? server;
        List<TcpClient> clients = new List<TcpClient>();
        public void Iniciar()
        {
            if (server==null)
            {
                IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 20012);
                server = new TcpListener(ipe);
                Thread hilo1 = new Thread(new ThreadStart(Escuchar));
                hilo1.Start();
            }
        }

        void Escuchar()
        {
            if (server != null)
            {
                server.Start();
                while (server.Server.IsBound)
                {
                    var clienteNuevo = server.AcceptTcpClient();
                    clients.Add(clienteNuevo);
                    Thread hiloRecibir = new Thread(new ParameterizedThreadStart(Recibir));
                    hiloRecibir.Start(clienteNuevo);
                }
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
        void Enviar(TcpClient Cliente, byte[] buffer)
        {
            if (Cliente.Connected)
            {
                var stream = Cliente.GetStream();
                stream.Write(buffer,0,buffer.Length);
            }
        }
        public event Action<Rectangulo>? UsuarioConectado;
        
        void Recibir(object? tcpClient)
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
                        clients.ForEach(x =>
                        {
                            if (x != cliente)
                            {
                                Enviar(x, buffer);
                            }
                        });
                        var usuario = JsonConvert.DeserializeObject<Rectangulo>(System.Text.Encoding.UTF8.GetString(buffer));
                        if (usuario != null)
                        {
                            UsuarioConectado?.Invoke(usuario);
                        }
                    }
                    Thread.Sleep(1000);
                }
            }

        }
    }
}
