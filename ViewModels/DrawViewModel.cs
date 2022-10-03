using DibujoTCP.Models;
using DibujoTCP.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace DibujoTCP.ViewModels
{
    public class DrawViewModel : INotifyPropertyChanged
    {

        private UsuarioView usuario;
        public string Error { get; set; } = "";
        public ICommand AgregarRectanguloCommand { get; set; }
        public ICommand ver { get; set; }
        private Datos datos;
        public Datos Datos
        {
            get { return datos; }
            set { datos = value; PropertyChanged?.Invoke(this,new PropertyChangedEventArgs("Datos"));}
        }
        public DrawViewModel()
        {
            ver = new RelayCommand(verdibujo);
            AgregarRectanguloCommand = new RelayCommand(Rectangulo);
        }
        public void verdibujo()
        {
            usuario = new UsuarioView();
            usuario.DataContext = this;
            usuario.ShowDialog();
        }
        public void Rectangulo()
        {
           Canvas myCanvas1 = new Canvas();
            myCanvas1.Background = datos.color;
            myCanvas1.Height = 100;
            myCanvas1.Width = 100;
            Canvas.SetTop(myCanvas1, 0);
            Canvas.SetLeft(myCanvas1, 0);
            usuario.MyCanvas.Children.Add(myCanvas1);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
