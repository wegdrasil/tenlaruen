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
            Random rnd = new Random();


            //init network
            net = new NeuralNetwork(3);      //neural newtwor with 3 layers

            net.layers.Add(new Layer(2));    //input layer with 2 neurons
            net.layers.Add(new Layer(3));    //hidden layer with 3 neurons 
            net.layers.Add(new Layer(4));    //output layer with 4 neurons

            for (int i = 0; i < 2; i++)
            {
                net.layers[0].neurons.Add(new Neuron());
                net.layers[0].neurons[i].AddConnection(new Neuron(), (1.0 - (-1.0) * rnd.NextDouble() + (-1.0)));
                net.layers[0].neurons[i].connections[0].neuron.output = 1.0f;
            }
            for (int i = 0; i < 3; i++)
            {
                net.layers[1].neurons.Add(new Neuron());
                net.layers[1].neurons[i].AddConnection(new Neuron(), (1.0 - (-1.0) * rnd.NextDouble() + (-1.0)));
                net.layers[1].neurons[i].connections[0].neuron.output = 1.0f;
            }
            for (int i = 0; i < 4; i++)
            {
                net.layers[2].neurons.Add(new Neuron());
                net.layers[2].neurons[i].AddConnection(new Neuron(), (1.0 - (-1.0) * rnd.NextDouble() + (-1.0)));
                net.layers[2].neurons[i].connections[0].neuron.output = 1.0f;
            }

            //output -> hidden
            double r = 1.0;
            net.MakeConnection(2, 0, 1, 0, -r, r, rnd);
            net.MakeConnection(2, 0, 1, 1, -r, r, rnd);
            net.MakeConnection(2, 0, 1, 2, -r, r, rnd);

            net.MakeConnection(2, 1, 1, 0, -r, r, rnd);
            net.MakeConnection(2, 1, 1, 1, -r, r, rnd);
            net.MakeConnection(2, 1, 1, 2, -r, r, rnd);

            net.MakeConnection(2, 2, 1, 0, -r, r, rnd);
            net.MakeConnection(2, 2, 1, 1, -r, r, rnd);
            net.MakeConnection(2, 2, 1, 2, -r, r, rnd);

            net.MakeConnection(2, 3, 1, 0, -r, r, rnd);
            net.MakeConnection(2, 3, 1, 1, -r, r, rnd);
            net.MakeConnection(2, 3, 1, 2, -r, r, rnd);

            //hidden -> input
            net.MakeConnection(1, 0, 0, 0, -r, r, rnd);
            net.MakeConnection(1, 0, 0, 1, -r, r, rnd);

            net.MakeConnection(1, 1, 0, 0, -r, r, rnd);
            net.MakeConnection(1, 1, 0, 1, -r, r, rnd);

            net.MakeConnection(1, 2, 0, 0, -r, r, rnd);
            net.MakeConnection(1, 2, 0, 1, -r, r, rnd);

            double[] input0 = new double[] { 0.0, 0.0 };
            double[] input1 = new double[] { 0.0, 1.0 };
            double[] input2 = new double[] { 1.0, 0.0 };
            double[] input3 = new double[] { 1.0, 1.0 };

            for (int i = 0; i < 100000; i++)
            {
                int a = rnd.Next(4);
                switch (a)
                {
                    case 0:
                        net.Backpropagate(net.Calculate(input0), new double[] { +1.0, -1.0, -1.0, -1.0 });
                        break;
                    case 1:
                        net.Backpropagate(net.Calculate(input1), new double[] { -1.0, +1.0, -1.0, -1.0 });
                        break;
                    case 2:
                        net.Backpropagate(net.Calculate(input2), new double[] { -1.0, -1.0, +1.0, -1.0 });
                        break;
                    case 3:
                        net.Backpropagate(net.Calculate(input3), new double[] { -1.0, -1.0, -1.0, +1.0 });
                        break;
                    default:
                        break;
                }
            }

            double[] o = new double[4];
            o = net.Calculate(input0);

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

            image.Source = data.GetTrainingImage(9530).GetImage();
        }
    }
}
