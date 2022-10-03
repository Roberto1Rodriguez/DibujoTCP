using DibujoTCP.Models;
using DibujoTCP.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DibujoTCP.ViewModels
{
    public class DrawViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Rectangulo> rec;
        public ObservableCollection<Rectangulo> Rec
        {
            get { return rec; }
            set { rec = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rec")); }

        }
        private UsuarioView usuario;
        public string Error { get; set; } = "";
        public ICommand AgregarRectanguloCommand { get; set; }
        public ICommand AgregarEllipseCommand { get; set; }
        public ICommand ver { get; set; }
        private Rectangulo datos;
        public Rectangulo Datos
        {
            get { return datos; }
            set { datos = value; PropertyChanged?.Invoke(this,new PropertyChangedEventArgs("Datos"));}
        }
        public DrawViewModel()
        {
            rec = new ObservableCollection<Rectangulo>();
            ver = new RelayCommand(verdibujo);
            AgregarRectanguloCommand = new RelayCommand(Rectangulo);
           
        }
        public void verdibujo()
        {
            Datos = new Rectangulo();
            usuario = new UsuarioView();
            usuario.DataContext = this;
            usuario.ShowDialog();
        }
        public void Rectangulo()
        {
            if (Datos != null)
            {
                rec.Add(Datos);
                PropertyChange();
                Datos = new Rectangulo();
            }
        }
        public void Enviar()
        {
            
        }
        private void PropertyChange(string v = null)
        {
            PropertyChanged?.Invoke(this,
               new PropertyChangedEventArgs(v));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
