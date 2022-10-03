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
using System.Windows.Shapes;

namespace DibujoTCP.ViewModels
{
    public class DrawViewModel : INotifyPropertyChanged
    {

        private UsuarioView usuario;
        public string Error { get; set; } = "";
        public ICommand AgregarRectanguloCommand { get; set; }
        public ICommand AgregarEllipseCommand { get; set; }
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
            AgregarEllipseCommand = new RelayCommand(Ellipse);
        }
        public void verdibujo()
        {
            Datos = new Datos();
            usuario = new UsuarioView();
            usuario.DataContext = this;
            usuario.ShowDialog();
        }
        public void Rectangulo()
        {
            SolidColorBrush colores = (SolidColorBrush)new BrushConverter().ConvertFromString(datos.color);
           Canvas myCanvas1 = new Canvas();
            myCanvas1.Background = colores;
            myCanvas1.Height = datos.Alto;
            myCanvas1.Width = datos.Ancho;
            Canvas.SetTop(myCanvas1, datos.CoordenadaY);
            Canvas.SetLeft(myCanvas1, datos.CoordenadaX);
            usuario.MyCanvas.Children.Add(myCanvas1);
        }
        public void Ellipse()
        {
            SolidColorBrush colores = (SolidColorBrush)new BrushConverter().ConvertFromString(datos.color);
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = colores;
            ellipse.Height = datos.Alto;
            ellipse.Width = datos.Ancho;
            Canvas.SetTop(ellipse, datos.CoordenadaY);
            Canvas.SetLeft(ellipse, datos.CoordenadaX);
            usuario.MyCanvas.Children.Add(ellipse);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
