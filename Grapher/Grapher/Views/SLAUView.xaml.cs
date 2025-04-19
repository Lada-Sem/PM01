using Grapher.ViewModels.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Grapher.Views
{
    /// <summary>
    /// Логика взаимодействия для SLAUView.xaml
    /// </summary>
    public partial class SLAUView : UserControl
    {
        public SLAUView()
        {
            InitializeComponent();
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var treeView = (TreeViewItem)sender;
            var viewModel = (SLAUViewModel)DataContext;
            viewModel.SelectMethod((string)treeView.Header);
        }
    }
}
