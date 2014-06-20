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
            
            double[] output = new double[layers[layers.Count-1].neurons.Count];

            for (int i = 0; i < layers[layers.Count-1].neurons.Count; i++)
                output[i] = layers[layers.Count-1].neurons[i].output;
            
            return output;
        }

        void Backpropagate()
        {

        }

        
    }
}
