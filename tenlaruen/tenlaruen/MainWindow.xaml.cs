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
        NeuralNetwork net;
       
        public MainWindow()
        {
            InitializeComponent();

            data = new DataSetMNIST();
            Random rnd = new Random(0);

            //init network
            net = new NeuralNetwork(3);      //neural newtwor with 3 layers

            net.layers.Add(new Layer(2));    //input layer with 2 neurons
            net.layers.Add(new Layer(3));    //hidden layer with 1 neurons 
            net.layers.Add(new Layer(4));    //output layer with 4 neurons

            for (int i = 0; i < 2; i++)
                net.layers[0].neurons.Add(new Neuron());
            for (int i = 0; i < 3; i++)
                net.layers[1].neurons.Add(new Neuron());
            for (int i = 0; i < 4; i++)
                net.layers[2].neurons.Add(new Neuron());

            //output -> hidden
            net.MakeConnection(2, 0, 1, 0, -0.1, 0.1, rnd);
            net.MakeConnection(2, 0, 1, 1, -0.1, 0.1, rnd);
            net.MakeConnection(2, 0, 1, 2, -0.1, 0.1, rnd);

            net.MakeConnection(2, 1, 1, 0, -0.1, 0.1, rnd);
            net.MakeConnection(2, 1, 1, 1, -0.1, 0.1, rnd);
            net.MakeConnection(2, 1, 1, 2, -0.1, 0.1, rnd);

            net.MakeConnection(2, 2, 1, 0, -0.1, 0.1, rnd);
            net.MakeConnection(2, 2, 1, 1, -0.1, 0.1, rnd);
            net.MakeConnection(2, 2, 1, 2, -0.1, 0.1, rnd);

            net.MakeConnection(2, 3, 1, 0, -0.1, 0.1, rnd);
            net.MakeConnection(2, 3, 1, 1, -0.1, 0.1, rnd);
            net.MakeConnection(2, 3, 1, 2, -0.1, 0.1, rnd);

            //hidden -> input
            net.MakeConnection(1, 0, 0, 0, -0.1, 0.1, rnd);
            net.MakeConnection(1, 0, 0, 1, -0.1, 0.1, rnd);

            net.MakeConnection(1, 1, 0, 0, -0.1, 0.1, rnd);
            net.MakeConnection(1, 1, 0, 1, -0.1, 0.1, rnd);

            net.MakeConnection(1, 2, 0, 0, -0.1, 0.1, rnd);
            net.MakeConnection(1, 2, 0, 1, -0.1, 0.1, rnd);

            double[] input = new double[] { 1.0, 1.0 };
            double[] output = net.Calculate(input);
                    
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            data.LoadTestData();
            MessageBox.Show("MNIST test data loaded");
            image.Source = data.GetTestImage(666).GetImage();


        }

        private void btnTrain_Click(object sender, RoutedEventArgs e)
        {
            data.LoadTrainingData();
            MessageBox.Show("MNIST training data loaded");

            image.Source = data.GetTrainingImage(666).GetImage();
        }
    }
}
