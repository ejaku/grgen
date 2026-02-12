// by Claude Code with Edgar Jakumeit

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;

namespace GraphAndModelTests
{
    public class GraphTests
    {
        private LGSPGraph graph; // TODO: reduce to interface

        [SetUp]
        public void Setup()
        {
            LGSPGlobalVariables globalVars = new LGSPGlobalVariables();
            graph = LGSPBackend.Instance.CreateFromSpec("DefaultGraphModel.gm", globalVars);
        }

        [Test]
        public void EmptyGraphHasNoNodesOrEdges()
        {
            Assert.AreEqual(0, graph.NumNodes);
            Assert.AreEqual(0, graph.NumEdges);
            Assert.IsEmpty(graph.Nodes);
            Assert.IsEmpty(graph.Edges);
        }

        [Test]
        public void AddNode()
        {
            NodeType nodeType = graph.Model.NodeModel.RootType;
            Assert.IsNotNull(nodeType);

            INode n1 = graph.AddNode(nodeType);
            Assert.AreEqual(1, graph.NumNodes);
            Assert.Contains(n1, graph.Nodes.ToList());

            INode n2 = graph.AddNode(nodeType);
            Assert.AreEqual(2, graph.NumNodes);
            Assert.Contains(n2, graph.Nodes.ToList());
        }

        [Test]
        public void AddEdge()
        {
            NodeType nodeType = graph.Model.NodeModel.RootType;
            EdgeType edgeType = graph.Model.EdgeModel.GetType("Edge"); // .RootType returns AEdge which is abstract; we need the concrete directed Edge type (TODO)
            Assert.IsNotNull(edgeType);

            INode n1 = graph.AddNode(nodeType);
            INode n2 = graph.AddNode(nodeType);
            IEdge e = graph.AddEdge(edgeType, n1, n2);

            Assert.AreEqual(1, graph.NumEdges);
            Assert.Contains(e, graph.Edges.ToList());
            Assert.AreSame(n1, e.Source);
            Assert.AreSame(n2, e.Target);
        }

        [Test]
        public void IncidentEdgesFromNode()
        {
            NodeType nodeType = graph.Model.NodeModel.RootType;
            EdgeType edgeType = graph.Model.EdgeModel.GetType("Edge");

            INode n1 = graph.AddNode(nodeType);
            INode n2 = graph.AddNode(nodeType);
            IEdge e = graph.AddEdge(edgeType, n1, n2);

            List<IEdge> n1Out = n1.Outgoing.ToList();
            Assert.AreEqual(1, n1Out.Count);
            Assert.Contains(e, n1Out);
            Assert.IsEmpty(n1.Incoming.ToList());

            List<IEdge> n2In = n2.Incoming.ToList();
            Assert.AreEqual(1, n2In.Count);
            Assert.Contains(e, n2In);
            Assert.IsEmpty(n2.Outgoing.ToList());
        }

        [Test]
        public void RemoveNodeAndEdge()
        {
            NodeType nodeType = graph.Model.NodeModel.RootType;
            EdgeType edgeType = graph.Model.EdgeModel.GetType("Edge");

            INode n1 = graph.AddNode(nodeType);
            INode n2 = graph.AddNode(nodeType);
            IEdge e = graph.AddEdge(edgeType, n1, n2);

            Assert.AreEqual(2, graph.NumNodes);
            Assert.AreEqual(1, graph.NumEdges);

            graph.Remove(e);
            Assert.AreEqual(0, graph.NumEdges);

            graph.Remove(n1);
            Assert.AreEqual(1, graph.NumNodes);
        }

        [Test]
        public void ClearRemovesEverything()
        {
            NodeType nodeType = graph.Model.NodeModel.RootType;
            EdgeType edgeType = graph.Model.EdgeModel.GetType("Edge");

            INode n1 = graph.AddNode(nodeType);
            INode n2 = graph.AddNode(nodeType);
            graph.AddEdge(edgeType, n1, n2);

            graph.Clear();
            Assert.AreEqual(0, graph.NumNodes);
            Assert.AreEqual(0, graph.NumEdges);
        }
    }
}
