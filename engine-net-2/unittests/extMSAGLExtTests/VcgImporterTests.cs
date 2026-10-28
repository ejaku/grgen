// by Claude Code with Edgar Jakumeit

using System;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using de.unika.ipd.grGen.extMSAGLExt;

namespace extMSAGLExtTests
{
    /// <summary>
    /// Tests of the vcg import: example files must yield the expected calls to the graph viewer client,
    /// and bogus files must cause the requested errors and warnings (reported to the console error stream).
    /// </summary>
    public class VcgImporterTests
    {
        RecordingGraphViewerClient client;
        TextWriter originalConsoleError;
        StringWriter consoleError;

        [SetUp]
        public void SetUp()
        {
            client = new RecordingGraphViewerClient();
            originalConsoleError = Console.Error;
            consoleError = new StringWriter();
            Console.SetError(consoleError);
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetError(originalConsoleError);
        }

        string Import(string vcg)
        {
            return VcgImporter.ImportFromText(vcg, client);
        }

        string Messages
        {
            get { return consoleError.ToString(); }
        }

        // ---- examples ----

        [Test]
        public void MinimumExample()
        {
            string error = Import("graph: { node: { title: \"foo\" } edge: { sourcename: \"foo\" targetname: \"foo\" } }");

            Assert.IsNull(error);
            Assert.IsEmpty(Messages);
            Assert.AreEqual(new string[]
            {
                "ClearGraph",
                "AddNodeRealizer nr0 Black Yellow Black Box",
                "AddNode foo nr0 ",
                "AddEdgeRealizer er0 Khaki Black 1 Continuous",
                "AddEdge e0 foo foo er0 ",
                "SetLayout SugiyamaScheme",
                "ForceLayout"
            }, client.Calls);
        }

        [Test]
        public void SmallCharacteristicExample()
        {
            string error = Import(@"graph: {
  node: { title: ""foo"" label: ""Foo"" }
  node: { title: ""bar"" color: yellow }
  edge: { sourcename: ""foo"" targetname: ""bar"" thickness: 3 }
  graph: { title: ""subgraph"" }
}");

            Assert.IsNull(error);
            Assert.IsEmpty(Messages);
            Assert.IsTrue(client.Calls.Contains("AddNode foo nr0 Foo"));
            Assert.IsTrue(client.Calls.Contains("AddNode bar nr0 "));
            Assert.IsTrue(client.Calls.Contains("AddSubgraphNode subgraph nr0 "));
            Assert.IsTrue(client.Calls.Contains("AddEdgeRealizer er0 Khaki Black 3 Continuous"));
            Assert.IsTrue(client.Calls.Contains("AddEdge e0 foo bar er0 "));
            Assert.AreEqual(1, client.CountCalls("AddNodeRealizer"));
        }

        [Test]
        public void NodeStylesAreMappedAndRealizersAreShared()
        {
            string error = Import(@"graph: {
  node: { title: ""a"" color: red shape: circle }
  node: { title: ""b"" color: red shape: circle }
  node: { title: ""c"" textcolor: blue bordercolor: green }
}");

            Assert.IsNull(error);
            Assert.AreEqual(2, client.CountCalls("AddNodeRealizer"));
            Assert.IsTrue(client.Calls.Contains("AddNodeRealizer nr0 Black Red Black Circle"));
            Assert.IsTrue(client.Calls.Contains("AddNodeRealizer nr1 Green Yellow Blue Box"));
            Assert.IsTrue(client.Calls.Contains("AddNode a nr0 "));
            Assert.IsTrue(client.Calls.Contains("AddNode b nr0 "));
            Assert.IsTrue(client.Calls.Contains("AddNode c nr1 "));
        }

        [Test]
        public void SubgraphNestingIsEstablishedByMoveNode()
        {
            string error = Import(@"graph: {
  graph: { title: ""outer""
    node: { title: ""inner1"" }
    graph: { title: ""middle"" node: { title: ""inner2"" } }
  }
  node: { title: ""top"" }
}");

            Assert.IsNull(error);
            Assert.AreEqual(new string[]
            {
                "ClearGraph",
                "AddNodeRealizer nr0 Black Yellow Black Box",
                "AddSubgraphNode outer nr0 ",
                "AddNode inner1 nr0 ",
                "AddSubgraphNode middle nr0 ",
                "AddNode inner2 nr0 ",
                "MoveNode inner2 middle",
                "MoveNode inner1 outer",
                "MoveNode middle outer",
                "AddNode top nr0 ",
                "SetLayout SugiyamaScheme",
                "ForceLayout"
            }, client.Calls);
        }

        [Test]
        public void EdgesMayReferenceNodesDeclaredLater()
        {
            string error = Import(@"graph: {
  edge: { sourcename: ""a"" targetname: ""b"" }
  node: { title: ""a"" }
  node: { title: ""b"" }
}");

            Assert.IsNull(error);
            Assert.IsEmpty(Messages);
            Assert.IsTrue(client.Calls.Contains("AddEdge e0 a b er0 "));
        }

        [Test]
        public void EdgesNestedInSubgraphsAreImportedToo()
        {
            string error = Import(@"graph: {
  node: { title: ""a"" }
  graph: { title: ""sub""
    node: { title: ""b"" }
    edge: { sourcename: ""a"" targetname: ""b"" }
    graph: { title: ""subsub"" edge: { sourcename: ""b"" targetname: ""a"" } }
  }
}");

            Assert.IsNull(error);
            Assert.AreEqual(2, client.CountCalls("AddEdge"));
            Assert.IsTrue(client.Calls.Contains("AddEdge e0 a b er0 "));
            Assert.IsTrue(client.Calls.Contains("AddEdge e1 b a er0 "));
        }

        [Test]
        public void StringsMayContainVerbatimNewlinesAndEscapedQuotesAndBackslashes()
        {
            string error = Import("graph: { node: { title: \"a\" label: \"two\nlines \\\"quoted\\\" back\\\\slash\" } }");

            Assert.IsNull(error);
            Assert.IsEmpty(Messages);
            Assert.IsTrue(client.Calls.Contains("AddNode a nr0 two\nlines \"quoted\" back\\slash"));
        }

        [Test]
        public void CommentsAreSkipped()
        {
            string error = Import("graph: { // line comment\n node: { title: \"a\" /* block \" comment */ } }");

            Assert.IsNull(error);
            Assert.IsEmpty(Messages);
            Assert.AreEqual(1, client.CountCalls("AddNode"));
        }

        [Test]
        public void NodeAndEdgeAttributesAreRecoveredFromInfo1AndEdgeLabel()
        {
            string error = Import("graph: { node: { title: \"a\" label: \"A\" info1: \"Node::foo : int = 5\n  Node::bar : string = hello = world\" }"
                + " edge: { sourcename: \"a\" targetname: \"a\" label: \"E\nAttributes:\nEdge::w : double = 0.5\" } }");

            Assert.IsNull(error);
            Assert.IsEmpty(Messages);
            Assert.IsTrue(client.Calls.Contains("AddNode a nr0 A"));
            Assert.IsTrue(client.Calls.Contains("SetNodeAttribute a Node foo int 5"));
            Assert.IsTrue(client.Calls.Contains("SetNodeAttribute a Node bar string hello = world"));
            Assert.IsTrue(client.Calls.Contains("AddEdge e0 a a er0 E"));
            Assert.IsTrue(client.Calls.Contains("SetEdgeAttribute e0 Edge w double 0.5"));
        }

        [Test]
        public void DumpOfTheVcgBaseDumperIsImportedWithoutMessages()
        {
            // in the style written by the VCGBaseDumper
            string error = Import(@"graph:{
 layoutalgorithm: SugiyamaScheme //MSAGL
 node:{title:""n1"" label:""Node"" textcolor:black color:yellow shape:box info1: ""Node::a : int = 1""}
 graph:{title:""n2"" label:""Sub"" textcolor:black color:lightgrey info1: ""Node::a : int = 2""
  node:{title:""n3"" label:""Inner"" textcolor:black color:yellow shape:box info1: ""Node::a : int = 3""}
 }
 edge:{sourcename:""n1"" targetname:""n3"" label:""Edge"" textcolor:black color:khaki linestyle:continuous}
}");

            Assert.IsNull(error);
            Assert.IsEmpty(Messages);
            Assert.AreEqual(1, client.CountCalls("AddSubgraphNode"));
            Assert.AreEqual(2, client.CountCalls("AddNode"));
            Assert.AreEqual(1, client.CountCalls("AddEdge"));
            Assert.AreEqual(3, client.CountCalls("SetNodeAttribute"));
        }

        [Test]
        public void LayoutAlgorithmIsApplied()
        {
            string error = Import("graph: { layoutalgorithm: MDS node: { title: \"a\" } }");

            Assert.IsNull(error);
            Assert.IsEmpty(Messages);
            Assert.IsTrue(client.Calls.Contains("SetLayout MDS"));
        }

        [Test]
        public void ReimportStartsWithAFreshRealizerNumbering()
        {
            Import("graph: { node: { title: \"a\" } }");
            client.Calls.Clear();
            Import("graph: { node: { title: \"a\" } }");

            Assert.IsTrue(client.Calls.Contains("AddNodeRealizer nr0 Black Yellow Black Box"));
        }

        // ---- errors and warnings ----

        [Test]
        public void SyntaxErrorAbortsTheImport()
        {
            string error = Import("graph: { node: { title: \"a\" ");

            Assert.IsNotNull(error);
            Assert.AreEqual(0, client.CountCalls("AddNode"));
        }

        [Test]
        public void NodeWithoutTitleIsSkippedWithAnError()
        {
            string error = Import("graph: { node: { label: \"x\" } node: { title: \"b\" } }");

            Assert.IsNull(error);
            StringAssert.Contains("error", Messages);
            StringAssert.Contains("title", Messages);
            Assert.AreEqual(1, client.CountCalls("AddNode"));
            Assert.IsTrue(client.Calls.Contains("AddNode b nr0 "));
        }

        [Test]
        public void SubgraphWithoutTitleIsSkippedWithAnError()
        {
            string error = Import("graph: { graph: { node: { title: \"a\" } } }");

            Assert.IsNull(error);
            StringAssert.Contains("title", Messages);
            Assert.AreEqual(0, client.CountCalls("AddSubgraphNode"));
        }

        [Test]
        public void DuplicateNodeNameIsReportedAndTheDuplicateIsSkipped()
        {
            string error = Import("graph: { node: { title: \"a\" label: \"first\" } node: { title: \"a\" label: \"second\" } }");

            Assert.IsNull(error);
            StringAssert.Contains("duplicate", Messages);
            Assert.AreEqual(1, client.CountCalls("AddNode"));
            Assert.IsTrue(client.Calls.Contains("AddNode a nr0 first"));
        }

        [Test]
        public void EdgeWithoutSourceOrTargetIsSkippedWithAnError()
        {
            string error = Import("graph: { node: { title: \"a\" } edge: { sourcename: \"a\" } edge: { targetname: \"a\" } }");

            Assert.IsNull(error);
            StringAssert.Contains("sourcename", Messages);
            StringAssert.Contains("targetname", Messages);
            Assert.AreEqual(0, client.CountCalls("AddEdge"));
        }

        [Test]
        public void EdgeToAnUnknownNodeIsSkippedWithAnError()
        {
            string error = Import("graph: { node: { title: \"a\" } edge: { sourcename: \"a\" targetname: \"nonexistent\" } }");

            Assert.IsNull(error);
            StringAssert.Contains("nonexistent", Messages);
            Assert.AreEqual(0, client.CountCalls("AddEdge"));
        }

        [Test]
        public void UnknownAttributesCauseAWarningAndAreSkipped()
        {
            string error = Import(@"graph: {
  classname 1: ""normal""
  node: { title: ""a"" fancy: 5 }
  edge: { sourcename: ""a"" targetname: ""a"" sparkle: yes }
}");

            Assert.IsNull(error);
            StringAssert.Contains("classname", Messages);
            StringAssert.Contains("fancy", Messages);
            StringAssert.Contains("sparkle", Messages);
            Assert.AreEqual(1, client.CountCalls("AddNode"));
            Assert.AreEqual(1, client.CountCalls("AddEdge"));
        }

        [Test]
        public void UnsupportedLayoutAlgorithmCausesAWarningAndTheDefaultIsUsed()
        {
            // as written by the VCGDumper for yComp
            string error = Import("graph: { layoutalgorithm: normal //$ \"Compilergraph\"\n node: { title: \"a\" } }");

            Assert.IsNull(error);
            StringAssert.Contains("warning", Messages);
            StringAssert.Contains("normal", Messages);
            Assert.IsTrue(client.Calls.Contains("SetLayout SugiyamaScheme"));
        }

        [Test]
        public void UnknownColorShapeAndLineStyleCauseAnErrorAndTheDefaultIsUsed()
        {
            string error = Import(@"graph: {
  node: { title: ""a"" color: reddish shape: blob }
  edge: { sourcename: ""a"" targetname: ""a"" linestyle: wavy color: ""red"" }
}");

            Assert.IsNull(error);
            StringAssert.Contains("reddish", Messages);
            StringAssert.Contains("blob", Messages);
            StringAssert.Contains("wavy", Messages);
            Assert.IsTrue(client.Calls.Contains("AddNodeRealizer nr0 Black Yellow Black Box"));
            Assert.IsTrue(client.Calls.Contains("AddEdgeRealizer er0 Khaki Black 1 Continuous"));
        }

        [Test]
        public void ThicknessMustBeAnIntegerInTheValidRange()
        {
            string error = Import(@"graph: {
  node: { title: ""a"" }
  edge: { sourcename: ""a"" targetname: ""a"" thickness: 9 }
  edge: { sourcename: ""a"" targetname: ""a"" thickness: 2.5 }
  edge: { sourcename: ""a"" targetname: ""a"" thickness: 5 }
}");

            Assert.IsNull(error);
            StringAssert.Contains("thickness", Messages);
            Assert.AreEqual(3, client.CountCalls("AddEdge"));
            Assert.IsTrue(client.Calls.Contains("AddEdgeRealizer er0 Khaki Black 1 Continuous"));
            Assert.IsTrue(client.Calls.Contains("AddEdgeRealizer er1 Khaki Black 5 Continuous"));
        }

        [Test]
        public void ValueOfTheWrongKindCausesAnError()
        {
            string error = Import(@"graph: {
  node: { title: ""a"" label: 42 }
  node: { title: 7 }
}");

            Assert.IsNull(error);
            StringAssert.Contains("label", Messages);
            StringAssert.Contains("title", Messages);
            Assert.AreEqual(1, client.CountCalls("AddNode"));
            Assert.IsTrue(client.Calls.Contains("AddNode a nr0 "));
        }

        // ---- non-trivial examples written by the vcg dumpers of GrGen.NET ----

        // written by the VCGDumper (for yComp), must not fail (but may cause warnings)
        [Test]
        public void MoveMethodsExampleGraphIsImportedCompletely()
        {
            string text = File.ReadAllText("MoveMethodsExampleGraph.vcg");

            string error = Import(text);

            Assert.IsNull(error);
            StringAssert.DoesNotContain("error", Messages);
            AssertAllElementsWereAdded(text);
        }

        // written by the VCGBaseDumper (for extMSAGLExt), must be imported without warnings
        [Test]
        public void MoveMethodsExampleGraphMSAGLIsImportedCompletelyWithoutMessages()
        {
            string text = File.ReadAllText("MoveMethodsExampleGraphMSAGL.vcg");

            string error = Import(text);

            Assert.IsNull(error);
            Assert.IsEmpty(Messages);
            AssertAllElementsWereAdded(text);
        }

        void AssertAllElementsWereAdded(string text)
        {
            int declaredNodes = Regex.Matches(text, @"\bnode\s*:\s*\{").Count;
            int declaredSubgraphs = Regex.Matches(text, @"\bgraph\s*:\s*\{").Count - 1; // the first one is the top-level graph
            int declaredEdges = Regex.Matches(text, @"\bedge\s*:\s*\{").Count;
            Assert.Greater(declaredNodes, 0);
            Assert.Greater(declaredEdges, 0);
            Assert.AreEqual(declaredNodes, client.CountCalls("AddNode"));
            Assert.AreEqual(declaredSubgraphs, client.CountCalls("AddSubgraphNode"));
            Assert.AreEqual(declaredEdges, client.CountCalls("AddEdge"));
        }
    }
}
