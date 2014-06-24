using System;
using System.Collections.Generic;

namespace tenlaruen
{
    class NeuralNetwork
    {
        public List<Layer> layers;

        public NeuralNetwork(int numLayers)
        {
            layers = new List<Layer>(numLayers);
        }

        public void MakeConnection(int layerA, int neuronA, int layerB, int neuronB, double lo, double hi, Random rnd)
        {
            double weight = (hi - lo) * rnd.NextDouble() + lo;
            layers[layerA].neurons[neuronA].AddConnection(layers[layerB].neurons[neuronB], weight);
        }

        public double[] Calculate(double[] input)
        {
            for (int i = 0; i < layers[0].neurons.Count; i++)
                layers[0].neurons[i].output = input[i];

            for (int i = 1; i < layers.Count; i++)
                layers[i].Calculate();

            double[] output = new double[layers[layers.Count - 1].neurons.Count];

            for (int i = 0; i < layers[layers.Count - 1].neurons.Count; i++)
                output[i] = layers[layers.Count - 1].neurons[i].output;

            return output;
        }
        double BiSigmoid(double x, double beta)
        {
            //return (1 - Math.Pow(Math.E, (beta * x))) / (1 + Math.Pow(Math.E, (-beta * x)));
            return 1.7159 * Math.Tanh(0.66666667 * x);
        }

        double DiffBiSigmoid(double x, double beta)
        {
            //return beta*(1 - Math.Pow(BiSigmoid(x,beta),2));
            //return Math.Pow((1.0 / Math.Cosh(x)), 2);
            return (0.66666667 / 1.7159 * (1.7159 + (x)) * (1.7159 - (x)));
        }

        public double[] Backpropagate(double[] actualOutput, double[] desiredOutput)
        {
            double[] LastLayerError = new double[actualOutput.Length];

            for (int i = 0; i < actualOutput.Length; i++)
                LastLayerError[i] = desiredOutput[i] - actualOutput[i];

            for (int i = 0; i < layers[layers.Count - 1].neurons.Count; i++)
            {
                Neuron n = layers[layers.Count - 1].neurons[i];

                double delta = LastLayerError[i] * DiffBiSigmoid(n.output, 1.0f);

                foreach (Connection c in n.connections)
                {

                    c.weight = c.weight + (0.0005) * delta * c.neuron.output;
                    c.neuron.sigma += delta * c.weight;

                }

            }

            for (int i = layers.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < layers[i].neurons.Count; j++)
                {
                    Neuron n = layers[i].neurons[j];
                    double delta = DiffBiSigmoid(n.output, 1.0f) * n.sigma;

                    foreach (Connection c in n.connections)
                    {
                        c.weight = c.weight + (0.0005) * delta * c.neuron.output;
                        c.neuron.sigma += delta * c.weight;

                    }
                }


            }
            return LastLayerError;
        }


    }
}
