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

        double BiSigmoid(double x, double beta)
        {
            return (1 - Math.Pow(Math.E, (beta * x))) / (1 + Math.Pow(Math.E, (-beta * x)));
        }

        public void Calculate()
        {
            double sum;

            foreach(Neuron n in neurons)
            {
                //sum += bias
                sum = 0; //?

                foreach(Connection c in n.connections)
                {
                    sum += c.neuron.output * c.weight;
                }
                n.output = BiSigmoid(sum, 1.0f);
            }
        }

	    //void Backpropagate( )
    }
}
