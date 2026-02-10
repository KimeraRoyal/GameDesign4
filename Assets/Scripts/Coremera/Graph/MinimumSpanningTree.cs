/* Adapted from https://github.com/vazgriz/DungeonGenerator

Copyright (c) 2019 Ryan Vazquez

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using System.Collections.Generic;

namespace Graph.Graph
{
    public class MinimumSpanningTree
    {
        public List<Edge> Edges { get; private set; }
        public List<Edge> Excluded { get; private set; }

        private MinimumSpanningTree(List<Edge> edges)
        {
            Edges = new List<Edge>();
            Excluded = new List<Edge>(edges);
        }
        
        public static MinimumSpanningTree Find(List<Edge> edges, Vertex start)
        {
            MinimumSpanningTree mst = new(edges);
            mst.Find(start);
            return mst;
        }
        
        private void Find(Vertex start)
        {
            var unexplored = new HashSet<Vertex>();
            var explored = new HashSet<Vertex>();

            foreach (Edge edge in Excluded)
            {
                unexplored.Add(edge.U);
                unexplored.Add(edge.V);
            }

            explored.Add(start);

            while (unexplored.Count > 0)
            {
                Edge chosenEdge = null;
                
                float minWeight = float.PositiveInfinity;
                foreach (Edge edge in Excluded)
                {
                    if(!(explored.Contains(edge.U) ^ explored.Contains(edge.V))) { continue; }
                    
                    if(edge.Distance >= minWeight) { continue; }

                    chosenEdge = edge;
                    minWeight = edge.Distance;
                }
                
                if(chosenEdge == null) { break; }
                
                Edges.Add(chosenEdge);
                Excluded.Remove(chosenEdge);

                unexplored.Remove(chosenEdge.U);
                unexplored.Remove(chosenEdge.V);
                explored.Add(chosenEdge.U);
                explored.Add(chosenEdge.V);
            }
        }
    }
}