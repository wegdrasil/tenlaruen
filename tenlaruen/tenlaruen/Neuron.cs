using System;
using System.Collections.Generic;

namespace tenlaruen
{
    class Neuron
    {
        public List<Connection> connections;
        public double output;
        public double sigma;

        public Neuron()
        {
            connections = new List<Connection>();
            connections.Clear();
        }

        public void Init()
        {
            connections.Clear();
        }

        public void AddConnection(Neuron neuron, double weight)
        {
            connections.Add(new Connection(neuron, weight));
        }

    }
}
