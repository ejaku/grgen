// by Claude Code with Edgar Jakumeit

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.libGr;

namespace GraphAndModelTests
{
    public class NamedAndTypedGraphTests
    {
        private LGSPNamedGraph graph; // TODO: reduce to interface

        [SetUp]
        public void Setup()
        {
            LGSPGlobalVariables globalVars = new LGSPGlobalVariables();
            graph = LGSPBackend.Instance.CreateNamedFromSpec("SimpleGraphModel.gm", globalVars, 0);
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
            NodeType someNodeType = graph.GetNodeType("SomeNodeType");
            Assert.IsNotNull(someNodeType);

            INode n1 = graph.AddNode(someNodeType);
            Assert.AreEqual(1, graph.NumNodes);
            Assert.Contains(n1, graph.Nodes.ToList());

            INode n2 = graph.AddNode(someNodeType);
            Assert.AreEqual(2, graph.NumNodes);
            Assert.Contains(n2, graph.Nodes.ToList());
        }

        [Test]
        public void AddEdge()
        {
            NodeType someNodeType = graph.GetNodeType("SomeNodeType");
            EdgeType someEdgeType = graph.GetEdgeType("someEdgeType");
            Assert.IsNotNull(someEdgeType);

            INode n1 = graph.AddNode(someNodeType);
            INode n2 = graph.AddNode(someNodeType);
            IEdge e = graph.AddEdge(someEdgeType, n1, n2);

            Assert.AreEqual(1, graph.NumEdges);
            Assert.Contains(e, graph.Edges.ToList());
            Assert.AreSame(n1, e.Source);
            Assert.AreSame(n2, e.Target);
        }

        [Test]
        public void GetCompatibleNodesReturnsByType()
        {
            NodeType someNodeType = graph.GetNodeType("SomeNodeType");
            NodeType anotherNodeType = graph.GetNodeType("AnotherNodeType");
            Assert.IsNotNull(anotherNodeType);

            INode n1 = graph.AddNode(someNodeType);
            INode n2 = graph.AddNode(someNodeType);
            INode n3 = graph.AddNode(anotherNodeType);

            Assert.AreEqual(3, graph.NumNodes);
            Assert.AreEqual(2, graph.GetNumCompatibleNodes(someNodeType));
            Assert.AreEqual(1, graph.GetNumCompatibleNodes(anotherNodeType));

            List<INode> someNodes = graph.GetCompatibleNodes(someNodeType).ToList();
            Assert.AreEqual(2, someNodes.Count);
            Assert.Contains(n1, someNodes);
            Assert.Contains(n2, someNodes);

            List<INode> anotherNodes = graph.GetCompatibleNodes(anotherNodeType).ToList();
            Assert.AreEqual(1, anotherNodes.Count);
            Assert.Contains(n3, anotherNodes);
        }

        [Test]
        public void GetCompatibleEdgesReturnsByType()
        {
            NodeType someNodeType = graph.GetNodeType("SomeNodeType");
            NodeType anotherNodeType = graph.GetNodeType("AnotherNodeType");
            EdgeType someEdgeType = graph.GetEdgeType("someEdgeType");
            EdgeType anotherEdgeType = graph.GetEdgeType("anotherEdgeType");
            Assert.IsNotNull(anotherEdgeType);

            INode n1 = graph.AddNode(someNodeType);
            INode n2 = graph.AddNode(someNodeType);
            INode n3 = graph.AddNode(anotherNodeType);

            IEdge eSome = graph.AddEdge(someEdgeType, n1, n2);
            IEdge eAnother = graph.AddEdge(anotherEdgeType, n1, n3);

            Assert.AreEqual(2, graph.NumEdges);
            Assert.AreEqual(1, graph.GetNumCompatibleEdges(someEdgeType));
            Assert.AreEqual(1, graph.GetNumCompatibleEdges(anotherEdgeType));

            List<IEdge> someEdges = graph.GetCompatibleEdges(someEdgeType).ToList();
            Assert.AreEqual(1, someEdges.Count);
            Assert.Contains(eSome, someEdges);
        }

        [Test]
        public void NamedGraphLookupByName()
        {
            NodeType someNodeType = graph.GetNodeType("SomeNodeType");
            NodeType anotherNodeType = graph.GetNodeType("AnotherNodeType");
            EdgeType someEdgeType = graph.GetEdgeType("someEdgeType");

            INode n1 = graph.AddNode(someNodeType, "n1");
            INode n2 = graph.AddNode(someNodeType, "n2");
            INode n3 = graph.AddNode(anotherNodeType, "n3");
            IEdge e1 = graph.AddEdge(someEdgeType, n1, n2, "e1");

            Assert.AreSame(n1, graph.GetNode("n1"));
            Assert.AreSame(n2, graph.GetNode("n2"));
            Assert.AreSame(n3, graph.GetNode("n3"));
            Assert.AreSame(e1, graph.GetEdge("e1"));

            Assert.AreSame(n1, graph.GetGraphElement("n1"));
            Assert.AreSame(e1, graph.GetGraphElement("e1"));

            Assert.IsNull(graph.GetNode("nonexistent"));
            Assert.IsNull(graph.GetEdge("nonexistent"));
        }

        [Test]
        public void GetElementNameReturnsAssignedName()
        {
            NodeType someNodeType = graph.GetNodeType("SomeNodeType");
            INode n1 = graph.AddNode(someNodeType, "myNode");

            Assert.AreEqual("myNode", graph.GetElementName(n1));
        }

        [Test]
        public void IncidentEdgesFromNode()
        {
            NodeType someNodeType = graph.GetNodeType("SomeNodeType");
            NodeType anotherNodeType = graph.GetNodeType("AnotherNodeType");
            EdgeType someEdgeType = graph.GetEdgeType("someEdgeType");
            EdgeType anotherEdgeType = graph.GetEdgeType("anotherEdgeType");

            INode n1 = graph.AddNode(someNodeType);
            INode n2 = graph.AddNode(someNodeType);
            INode n3 = graph.AddNode(anotherNodeType);

            IEdge eSome = graph.AddEdge(someEdgeType, n1, n2);
            IEdge eAnother = graph.AddEdge(anotherEdgeType, n1, n3);

            // n1 has two outgoing edges
            List<IEdge> n1Out = n1.Outgoing.ToList();
            Assert.AreEqual(2, n1Out.Count);
            Assert.Contains(eSome, n1Out);
            Assert.Contains(eAnother, n1Out);

            // n1 has no incoming edges
            Assert.IsEmpty(n1.Incoming.ToList());

            // n2 has one incoming edge (eSome)
            List<IEdge> n2In = n2.Incoming.ToList();
            Assert.AreEqual(1, n2In.Count);
            Assert.Contains(eSome, n2In);

            // n3 has one incoming edge (eAnother)
            List<IEdge> n3In = n3.Incoming.ToList();
            Assert.AreEqual(1, n3In.Count);
            Assert.Contains(eAnother, n3In);

            // typed incident query: n1 outgoing of type someEdgeType
            List<IEdge> n1OutSome = n1.GetCompatibleOutgoing(someEdgeType).ToList();
            Assert.AreEqual(1, n1OutSome.Count);
            Assert.Contains(eSome, n1OutSome);
        }

        [Test]
        public void RemoveNodeAndEdge()
        {
            NodeType someNodeType = graph.GetNodeType("SomeNodeType");
            EdgeType someEdgeType = graph.GetEdgeType("someEdgeType");

            INode n1 = graph.AddNode(someNodeType);
            INode n2 = graph.AddNode(someNodeType);
            IEdge e1 = graph.AddEdge(someEdgeType, n1, n2);

            Assert.AreEqual(2, graph.NumNodes);
            Assert.AreEqual(1, graph.NumEdges);

            graph.Remove(e1);
            Assert.AreEqual(0, graph.NumEdges);

            graph.Remove(n1);
            Assert.AreEqual(1, graph.NumNodes);
        }

        [Test]
        public void ClearRemovesEverything()
        {
            NodeType someNodeType = graph.GetNodeType("SomeNodeType");
            EdgeType someEdgeType = graph.GetEdgeType("someEdgeType");

            INode n1 = graph.AddNode(someNodeType);
            INode n2 = graph.AddNode(someNodeType);
            graph.AddEdge(someEdgeType, n1, n2);

            graph.Clear();
            Assert.AreEqual(0, graph.NumNodes);
            Assert.AreEqual(0, graph.NumEdges);
        }

        [Test]
        public void NodeTypeIsCorrect()
        {
            NodeType someNodeType = graph.GetNodeType("SomeNodeType");
            NodeType anotherNodeType = graph.GetNodeType("AnotherNodeType");

            INode n1 = graph.AddNode(someNodeType);
            INode n2 = graph.AddNode(anotherNodeType);

            Assert.AreEqual(someNodeType, n1.Type);
            Assert.AreEqual(anotherNodeType, n2.Type);
            Assert.AreEqual("SomeNodeType", n1.Type.Name);
            Assert.AreEqual("AnotherNodeType", n2.Type.Name);
        }

        [Test]
        public void EdgeTypeIsCorrect()
        {
            NodeType someNodeType = graph.GetNodeType("SomeNodeType");
            EdgeType someEdgeType = graph.GetEdgeType("someEdgeType");

            INode n1 = graph.AddNode(someNodeType);
            INode n2 = graph.AddNode(someNodeType);
            IEdge e = graph.AddEdge(someEdgeType, n1, n2);

            Assert.AreEqual(someEdgeType, e.Type);
            Assert.AreEqual("someEdgeType", e.Type.Name);
        }

        [Test]
        public void RingTopology()
        {
            NodeType someNodeType = graph.GetNodeType("SomeNodeType");
            NodeType anotherNodeType = graph.GetNodeType("AnotherNodeType");
            EdgeType someEdgeType = graph.GetEdgeType("someEdgeType");
            EdgeType anotherEdgeType = graph.GetEdgeType("anotherEdgeType");

            INode n1 = graph.AddNode(someNodeType, "n1");
            INode n2 = graph.AddNode(someNodeType, "n2");
            INode n3 = graph.AddNode(someNodeType, "n3");
            INode n4 = graph.AddNode(anotherNodeType, "n4");

            // Ring: n1 -> n2 -> n3 -> n1
            IEdge e12 = graph.AddEdge(someEdgeType, n1, n2, "e12");
            IEdge e23 = graph.AddEdge(someEdgeType, n2, n3, "e23");
            IEdge e31 = graph.AddEdge(someEdgeType, n3, n1, "e31");
            // n1 -> n4
            IEdge e14 = graph.AddEdge(anotherEdgeType, n1, n4, "e14");

            Assert.AreEqual(4, graph.NumNodes);
            Assert.AreEqual(4, graph.NumEdges);
            Assert.AreEqual(3, graph.GetNumCompatibleNodes(someNodeType));
            Assert.AreEqual(1, graph.GetNumCompatibleNodes(anotherNodeType));
            Assert.AreEqual(3, graph.GetNumCompatibleEdges(someEdgeType));
            Assert.AreEqual(1, graph.GetNumCompatibleEdges(anotherEdgeType));

            // Verify ring connectivity
            Assert.AreSame(n2, e12.Target);
            Assert.AreSame(n3, e23.Target);
            Assert.AreSame(n1, e31.Target);

            // Verify cross-type edge
            Assert.AreSame(n1, e14.Source);
            Assert.AreSame(n4, e14.Target);

            // Verify named lookup
            Assert.AreSame(n2, graph.GetNode("n2"));
            Assert.AreSame(e23, graph.GetEdge("e23"));
            Assert.AreEqual("e14", graph.GetElementName(e14));
        }
    }
}
