using DibujoTCP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DibujoTCP.ViewModels
{
    public class DrawViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Datos> Dibujos = new ObservableCollection<Datos>();
        public string Error { get; set; } = "";
        private Datos datos;
        public Datos Datos
        {
            get { return datos; }
            set { datos = value; PropertyChanged?.Invoke(this,new PropertyChangedEventArgs("Datos")); ; }
        }
        
        public void Rectangulo()
        {
            Rectangle rect = new Rectangle();
            rect.Width = datos.Ancho;
            rect.Height = datos.Alto;
            rect.X = datos.CoordenadaX;
            rect.Y = datos.CoordenadaY;

        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
