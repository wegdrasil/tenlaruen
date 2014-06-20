using System;

namespace tenlaruen
{
    class Connection
    {
        public Neuron neuron;
        public double weight;

        public Connection(Neuron neuron, double weight)
        {
            this.neuron = neuron;
            this.weight = weight;
        }   
     }
}
