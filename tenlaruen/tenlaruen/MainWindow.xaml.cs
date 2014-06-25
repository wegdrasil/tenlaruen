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
    public partial class MainWindow : Window
    {
        DataSetMNIST data;
        NeuralNetwork net;
        int currentChar;

        public MainWindow()
        {
            InitializeComponent();

            data = new DataSetMNIST();
            Random rnd = new Random();


            //init network
            net = new NeuralNetwork(3);      //neural newtwor with 3 layers

            net.layers.Add(new Layer(784));    //input layer with 784 neurons
            net.layers.Add(new Layer(100));    //hidden layer with 100 neurons 
            net.layers.Add(new Layer(10));    //output layer with 10 neurons

            for (int i = 0; i < 784; i++)
            {
                net.layers[0].neurons.Add(new Neuron());
                net.layers[0].neurons[i].AddConnection(new Neuron(), (1.0 - (-1.0) * rnd.NextDouble() + (-1.0)));
                net.layers[0].neurons[i].connections[0].neuron.output = 1.0f;
            }
            for (int i = 0; i < 100; i++)
            {
                net.layers[1].neurons.Add(new Neuron());
                net.layers[1].neurons[i].AddConnection(new Neuron(), (1.0 - (-1.0) * rnd.NextDouble() + (-1.0)));
                net.layers[1].neurons[i].connections[0].neuron.output = 1.0f;
            }
            for (int i = 0; i < 10; i++)
            {
                net.layers[2].neurons.Add(new Neuron());
                net.layers[2].neurons[i].AddConnection(new Neuron(), (1.0 - (-1.0) * rnd.NextDouble() + (-1.0)));
                net.layers[2].neurons[i].connections[0].neuron.output = 1.0f;
            }

            //output -> hidden
            double r = 1.0;

            for (int j = 0; j < 10; j++ )
                for (int i = 0; i < 100; i++)
                    net.MakeConnection(2, j, 1, i, -r, r, rnd);
            
            //hidden -> input
            for (int j = 0; j < 100; j++)
                for (int i = 0; i < 784; i++)
                    net.MakeConnection(1, j, 0, i, -r, r, rnd);
            
        }

        private void btnLoadTrain_Click(object sender, RoutedEventArgs e)
        {
            data.LoadTrainingData();
            MessageBox.Show("Wczytano dane treningowe.");
        }

        private void btnLoadTest_Click(object sender, RoutedEventArgs e)
        {
            data.LoadTestData();
            MessageBox.Show("Wczytano dane testowe.");


        }

        private void btnTrain_Click(object sender, RoutedEventArgs e)
        {
            if (data.trainingData == null || data.testData == null)
            {
                MessageBox.Show("Zanim zaczniesz trenować sieć, wczytaj dane.");
                return;
            }
            else
            {
                Random rnd = new Random();
                MessageBox.Show("Trwa uczenie sieci.");

                for (int j = 0; j < 250; j++)
                {
                    int i = rnd.Next(60000);
                    int a = data.GetTrainingImage(i).label;
                    switch (a)
                    {
                        case 0:
                            net.Backpropagate(net.Calculate(data.GetTrainingImage(i).GetNetInput()), new double[] { +1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0 });
                            break;
                        case 1:
                            net.Backpropagate(net.Calculate(data.GetTrainingImage(i).GetNetInput()), new double[] { -1.0, +1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0 });
                            break;
                        case 2:
                            net.Backpropagate(net.Calculate(data.GetTrainingImage(i).GetNetInput()), new double[] { -1.0, -1.0, +1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0 });
                            break;
                        case 3:
                            net.Backpropagate(net.Calculate(data.GetTrainingImage(i).GetNetInput()), new double[] { -1.0, -1.0, -1.0, +1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0 });
                            break;
                        case 4:
                            net.Backpropagate(net.Calculate(data.GetTrainingImage(i).GetNetInput()), new double[] { -1.0, -1.0, -1.0, -1.0, +1.0, -1.0, -1.0, -1.0, -1.0, -1.0 });
                            break;
                        case 5:
                            net.Backpropagate(net.Calculate(data.GetTrainingImage(i).GetNetInput()), new double[] { -1.0, -1.0, -1.0, -1.0, -1.0, +1.0, -1.0, -1.0, -1.0, -1.0 });
                            break;
                        case 6:
                            net.Backpropagate(net.Calculate(data.GetTrainingImage(i).GetNetInput()), new double[] { -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, +1.0, -1.0, -1.0, -1.0 });
                            break;
                        case 7:
                            net.Backpropagate(net.Calculate(data.GetTrainingImage(i).GetNetInput()), new double[] { -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, +1.0, -1.0, -1.0 });
                            break;
                        case 8:
                            net.Backpropagate(net.Calculate(data.GetTrainingImage(i).GetNetInput()), new double[] { -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, +1.0, -1.0 });
                            break;
                        case 9:
                            net.Backpropagate(net.Calculate(data.GetTrainingImage(i).GetNetInput()), new double[] { -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, +1.0 });
                            break;

                        default:
                            break;
                    }
                }

                MessageBox.Show("Proces uczenia sieci neuronowej przebiegł pomyślnie.");

            }
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            if (data.trainingData == null || data.testData == null)
            {
                MessageBox.Show("Zanim zaczniesz testować sieć, wczytaj dane.");
                return;
            }
            else
            {
                double[] o = new double[10];
                o = net.Calculate(data.GetTestImage(currentChar).GetNetInput());

                double maxValue = o.Max();
                int maxIndex = o.ToList().IndexOf(maxValue);

                bestGuess0.Text = maxIndex.ToString();
                bestGuessValue.Text = o[maxIndex].ToString("0.000");
                image.Source = data.GetTestImage(currentChar).GetImage();
                currentChar++;
            }

        }
    }
}
