using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DibujoTCP.Services;
using DibujoTCP.ViewModels;

namespace DibujoTCP.Views
{
    /// <summary>
    /// Lógica de interacción para ServidorView.xaml
    /// </summary>
    public partial class ServidorView : Window
    {
        public ServidorView()
        {
            InitializeComponent();
        }

     

        private void Window_Closed(object sender, EventArgs e)
        {
            ((DrawViewModel)this.DataContext).DetenerServidorCommand.Execute(null);
        }
    }
}
