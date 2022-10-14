using DibujoTCP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DibujoTCP.Views
{
    /// <summary>
    /// Lógica de interacción para UsuarioView.xaml
    /// </summary>
    public partial class UsuarioView : Window
    {
        public UsuarioView()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((DrawViewModel)this.DataContext).DetenerServidorCommand.Execute(null);
        }
    }
}
