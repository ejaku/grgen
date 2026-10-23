// by Claude Code with Edgar Jakumeit

using System.Collections.Generic;
using NUnit.Framework;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.extMSAGLExt;

namespace extMSAGLExtTests
{
    /// <summary>
    /// Tests of the live graph changes protocol command line handling: the CommandLineParser (tokenizer and argument conversions)
    /// and the CommandLineInterpreter (execution on the client; the commands writing responses to the network are not covered here).
    /// </summary>
    public class CommandLineTests
    {
        // ---- tokenizer ----

        [Test]
        public void KeywordWithoutArguments()
        {
            Assert.AreEqual(new string[] { "sync" }, CommandLineParser.Tokenize("sync"));
        }

        [Test]
        public void EmptyLineYieldsNoTokens()
        {
            Assert.IsEmpty(CommandLineParser.Tokenize(""));
        }

        [TestCase(" sync")]
        [TestCase("sync ")]
        [TestCase("   ")]
        [TestCase("addNode  \"n\"")]
        [TestCase("addNode \"n\"  \"nr0\"")]
        [TestCase("addNode \"n\" ")]
        public void SuperfluousSpacesAreASyntaxError(string line)
        {
            Assert.Throws<ProtocolSyntaxException>(() => CommandLineParser.Tokenize(line));
        }

        [TestCase("add-Node")]
        [TestCase("add\"Node")]
        [TestCase("\"sync\"")]
        [TestCase("add\\Node \"n\"")]
        public void KeywordWithNonWordCharactersIsASyntaxError(string line)
        {
            Assert.Throws<ProtocolSyntaxException>(() => CommandLineParser.Tokenize(line));
        }

        [TestCase("waitForElement tr-ue")]
        [TestCase("waitForElement tr\"ue")]
        [TestCase("waitForElement true\"x\"")]
        public void UnquotedArgumentWithNonWordCharactersIsASyntaxError(string line)
        {
            Assert.Throws<ProtocolSyntaxException>(() => CommandLineParser.Tokenize(line));
        }

        [Test]
        public void QuotedArgumentsMayContainSpaces()
        {
            Assert.AreEqual(new string[] { "addNode", "n", "nr0", "a label" },
                CommandLineParser.Tokenize("addNode \"n\" \"nr0\" \"a label\""));
        }

        [Test]
        public void EmptyQuotedArgumentIsKept()
        {
            Assert.AreEqual(new string[] { "setNodeLabel", "n1", "" },
                CommandLineParser.Tokenize("setNodeLabel \"n1\" \"\""));
        }

        [Test]
        public void UnquotedBooleanArgument()
        {
            Assert.AreEqual(new string[] { "waitForElement", "true" }, CommandLineParser.Tokenize("waitForElement true"));
        }

        [Test]
        public void EscapesAreUndone()
        {
            // in the line: setNodeLabel "n" "a\nb\"c\\d"   (with \n being the two characters backslash and n)
            string line = "setNodeLabel \"n\" \"a\\nb\\\"c\\\\d\"";
            Assert.AreEqual(new string[] { "setNodeLabel", "n", "a\nb\"c\\d" }, CommandLineParser.Tokenize(line));
        }

        [Test]
        public void EscapedBackslashBeforeNIsNoNewline()
        {
            // the escaped backslash followed by the letter n: a backslash and an n, no newline
            string line = "setNodeLabel \"n\" \"\\\\n\"";
            Assert.AreEqual(new string[] { "setNodeLabel", "n", "\\n" }, CommandLineParser.Tokenize(line));
        }

        [Test]
        public void UnterminatedStringIsASyntaxError()
        {
            Assert.Throws<ProtocolSyntaxException>(() => CommandLineParser.Tokenize("setNodeLabel \"n\" \"abc"));
        }

        [Test]
        public void EscapedQuoteDoesNotTerminateTheString()
        {
            Assert.Throws<ProtocolSyntaxException>(() => CommandLineParser.Tokenize("setNodeLabel \"n\" \"abc\\\""));
        }

        [Test]
        public void UnknownEscapeSequenceIsASyntaxError()
        {
            Assert.Throws<ProtocolSyntaxException>(() => CommandLineParser.Tokenize("setNodeLabel \"n\" \"a\\tb\""));
            // the same string with a tab character (without escaping) passes
            Assert.AreEqual(new string[] { "setNodeLabel", "n", "a\tb" }, CommandLineParser.Tokenize("setNodeLabel \"n\" \"a\tb\""));
        }

        [Test]
        public void DanglingBackslashIsASyntaxError()
        {
            Assert.Throws<ProtocolSyntaxException>(() => CommandLineParser.Tokenize("setNodeLabel \"n\" \"a\\"));
        }

        // ---- argument conversions ----

        [Test]
        public void AttributeKeyIsSplit()
        {
            string ownerType, attrName, attrType;
            CommandLineParser.SplitAttrKey("Node::foo : int", out ownerType, out attrName, out attrType);
            Assert.AreEqual("Node", ownerType);
            Assert.AreEqual("foo", attrName);
            Assert.AreEqual("int", attrType);
        }

        [Test]
        public void AttributeKeyWithoutOwnerIsASyntaxError()
        {
            string ownerType, attrName, attrType;
            Assert.Throws<ProtocolSyntaxException>(() => CommandLineParser.SplitAttrKey("foo : int", out ownerType, out attrName, out attrType));
        }

        [Test]
        public void KnownEnumNamesAreParsed()
        {
            Assert.AreEqual(GrColor.Red, CommandLineParser.ParseColor("red"));
            Assert.AreEqual(GrColor.Grey, CommandLineParser.ParseColor("darkgrey"));
            Assert.AreEqual(GrNodeShape.Hexagon, CommandLineParser.ParseShape("hexagon"));
            Assert.AreEqual(GrLineStyle.Dashed, CommandLineParser.ParseLineStyle("dashed"));
            Assert.AreEqual(42, CommandLineParser.ParseInt("42"));
        }

        [Test]
        public void UnknownEnumNamesAndBadIntegersAreSyntaxErrors()
        {
            Assert.Throws<ProtocolSyntaxException>(() => CommandLineParser.ParseColor("reddish"));
            Assert.Throws<ProtocolSyntaxException>(() => CommandLineParser.ParseShape("blob"));
            Assert.Throws<ProtocolSyntaxException>(() => CommandLineParser.ParseLineStyle("wavy"));
            Assert.Throws<ProtocolSyntaxException>(() => CommandLineParser.ParseInt("three"));
        }

        // ---- interpreter ----

        RecordingGraphViewerClient client;
        CommandLineInterpreter interpreter;

        [SetUp]
        public void SetUp()
        {
            client = new RecordingGraphViewerClient();
            interpreter = new CommandLineInterpreter(client);
        }

        bool Interpret(string line)
        {
            return interpreter.InterpretAsync(CommandLineParser.Tokenize(line), null).GetAwaiter().GetResult();
        }

        [Test]
        public void CommandsAreForwardedToTheClient()
        {
            Interpret("addNodeRealizer \"nr0\" \"black\" \"yellow\" \"black\" \"box\"");
            Interpret("addEdgeRealizer \"er0\" \"khaki\" \"black\" \"3\" \"dotted\"");
            Interpret("addNode \"n\" \"n1\" \"nr0\" \"first\\nnode\"");
            Interpret("addNode \"n\" \"n2\" \"nr0\" \"second\"");
            Interpret("addEdge \"e1\" \"n1\" \"n2\" \"er0\" \"edge \\\"label\\\"\"");
            Interpret("changeNodeAttr \"n1\" \"Node::foo : int\" \"5\"");
            Interpret("moveNode \"n2\" \"n1\"");
            Interpret("deleteEdge \"e1\"");
            Interpret("layout");

            Assert.AreEqual(new string[]
            {
                "AddNodeRealizer nr0 Black Yellow Black Box",
                "AddEdgeRealizer er0 Khaki Black 3 Dotted",
                "AddNode n1 nr0 first\nnode",
                "AddNode n2 nr0 second",
                "AddEdge e1 n1 n2 er0 edge \"label\"",
                "SetNodeAttribute n1 Node foo int 5",
                "MoveNode n2 n1",
                "DeleteEdge e1",
                "ForceLayout"
            }, client.Calls);
        }

        [Test]
        public void ExitClosesTheConnection()
        {
            Assert.IsTrue(Interpret("exit"));
            Assert.IsFalse(Interpret("layout"));
        }

        [Test]
        public void MissingArgumentsAreASyntaxError()
        {
            Assert.Throws<ProtocolSyntaxException>(() => Interpret("addEdge \"e1\" \"n1\" \"n2\" \"er0\""));
            Assert.IsEmpty(client.Calls);
        }

        [Test]
        public void SuperfluousArgumentsAreASyntaxError()
        {
            Assert.Throws<ProtocolSyntaxException>(() => Interpret("layout \"now\""));
            Assert.Throws<ProtocolSyntaxException>(() => Interpret("deleteNode \"n1\" \"n2\""));
            Assert.IsEmpty(client.Calls);
        }

        [Test]
        public void UnknownAndDeprecatedCommandsAreASyntaxError()
        {
            Assert.Throws<ProtocolSyntaxException>(() => Interpret("frobnicate \"n1\""));
            Assert.Throws<ProtocolSyntaxException>(() => Interpret("renameNode \"a\" \"b\""));
            Assert.Throws<ProtocolSyntaxException>(() => interpreter.InterpretAsync(new List<string>(), null).GetAwaiter().GetResult());
        }

        [Test]
        public void BadArgumentValuesAreASyntaxError()
        {
            Assert.Throws<ProtocolSyntaxException>(() => Interpret("addNodeRealizer \"nr0\" \"black\" \"yellowish\" \"black\" \"box\""));
            Assert.Throws<ProtocolSyntaxException>(() => Interpret("addEdgeRealizer \"er0\" \"khaki\" \"black\" \"wide\" \"dotted\""));
            Assert.Throws<ProtocolSyntaxException>(() => Interpret("changeNodeAttr \"n1\" \"nodoublecolon\" \"5\""));
            Assert.Throws<ProtocolSyntaxException>(() => Interpret("waitForElement maybe"));
            Assert.IsEmpty(client.Calls);
        }
    }
}
