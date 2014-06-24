using System;
using System.Collections.Generic;

namespace tenlaruen
{
    class Layer
    {
        public List<Neuron> neurons;
        public Layer prevLayer;
        public int numNeurons;

        public Layer(int numNeurons)
        {
            neurons = new List<Neuron>(numNeurons);
            this.numNeurons = numNeurons;
        }
        //SIGMOID(x) (1.7159*tanh(0.66666667*x))
        //DSIGMOID(S) (0.66666667/1.7159*(1.7159+(S))*(1.7159-(S)))  // derivative of the sigmoid as a function of the sigmoid's output

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

        public void Calculate()
        {
            double sum;

            foreach (Neuron n in neurons)
            {
                sum = 0.0;

                foreach (Connection c in n.connections)
                {
                    sum += c.neuron.output * c.weight;
                }
                n.output = BiSigmoid(sum, 1.0f);
            }
        }

        public void Backpropagate(int layerId)
        {

        }
    }
}
