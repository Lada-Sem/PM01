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
    /// Логика взаимодействия для CalculateView.xaml
    /// </summary>
    public partial class CalculateView : UserControl
    {
        public CalculateView()
        {
            InitializeComponent();
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var viewModel = (CalculateViewModel)DataContext;
            var treeView = (TreeViewItem)sender;
            viewModel.Execute((string)treeView.Header);
        }

        private void CartesianChart_ChartPointPointerDown(LiveChartsCore.Kernel.Sketches.IChartView chart, LiveChartsCore.Kernel.ChartPoint point)
        {
            var viewModel = (CalculateViewModel)DataContext;
            if (viewModel.A == 0)
            {
                viewModel.A = point.Coordinate.SecondaryValue;
            }
            else if (viewModel.B == 0)
            {
                viewModel.B = point.Coordinate.SecondaryValue;
            }
            else
            {
                viewModel.A = 0;
                viewModel.B = 0;
            }
        }
    }
}
