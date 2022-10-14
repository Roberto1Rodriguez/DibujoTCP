using DibujoTCP.Models;
using DibujoTCP.Services;
using DibujoTCP.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DibujoTCP.ViewModels
{
    public class DrawViewModel : INotifyPropertyChanged
    {
     
        UserService server = new UserService();
        private ObservableCollection<Rectangulo> rec;
        public ObservableCollection<Rectangulo> Rec
        {
            get { return rec; }
            set { rec = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rec")); }

        }
        private ServidorView servidorview;
        public string Error { get; set; } = "";
        ClientService cliente;
        private UsuarioView usuarioview;
      
        public ICommand AgregarRectanguloCommand { get; set; }
        public ICommand IniciarCliente { get; set; }
        public ICommand IniciarServer { get; set; }
        public ICommand DetenerCommand { get; set; }

        private Rectangulo datos;
        public Rectangulo Datos
        {
            get { return datos; }
            set { datos = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Datos))); }
        }
        private Usuario usuario;
        public Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Usuario))); }
        }

        public DrawViewModel()
        {
            usuario = new Usuario();
            rec = new ObservableCollection<Rectangulo>();
            IniciarCliente = new RelayCommand(Dibujar);
            IniciarServer = new RelayCommand(IniciarServidor);
            AgregarRectanguloCommand = new RelayCommand(Rectangulo);
            DetenerCommand = new RelayCommand(Detener); 
            server.UsuarioConectado += Server_UsuarioConectado;
 
        }

        public void Server_UsuarioConectado(Rectangulo obj)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                rec.Add(obj);
            });

        }
  
        public void IniciarServidor()
        {
            server.Iniciar();
           
       
            if (servidorview == null)
            {
                servidorview = new ServidorView();
                servidorview.DataContext = this;
                servidorview.ShowDialog();
               
            }
            
        }
   //metodo para instanciar un nuevo cliente
        public void Dibujar()
        {
             
            if (!IPAddress.TryParse(usuario.ip, out IPAddress? direccion))
                {
                
                   Error = "Coloca una ip correcta";
                PropertyChange("Error");
                return;
                }
            if (string.IsNullOrEmpty(usuario.nombre))
            {
               
                Error = "Escribe el nombre de usuaro";
                PropertyChange("Error");
                return;
            }
            Error = "";
            cliente = new ClientService(Usuario.ip, datos);
            Datos = new Rectangulo();
            datos.Nombre = usuario.nombre;
            usuarioview = new UsuarioView();
                usuarioview.DataContext = this;
                usuarioview.ShowDialog();
            }
        //Manda el rectangulo al lienzo
        public void Rectangulo()
        {
            Error = "";
          
            if (datos.Alto==0)
                {
                    Error = "El alto de la figura no puede ser 0";
                PropertyChange("Error");
                return;
            }
            if (datos.Ancho==0)
            {
                Error = "El ancho de la figura no puede ser 0";
                PropertyChange("Error");
                return;
            }
            
            cliente.Enviar(datos);
          
        }

        //Detiene el servidor
        public void Detener()
        {
            server.Detener();
        }

        void PropertyChange(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propiedad));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
