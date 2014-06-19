using System;
using System.IO;
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

namespace tenlaruen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSetMNIST data;

        public MainWindow()
        {
            InitializeComponent();

            data = new DataSetMNIST();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            data.LoadTestData();
            MessageBox.Show("MNIST test data loaded");
        }

        private void btnTrain_Click(object sender, RoutedEventArgs e)
        {
            data.LoadTrainingData();
            MessageBox.Show("MNIST training data loaded");
        }
    }
}
