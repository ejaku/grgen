// by Claude Code with Edgar Jakumeit

using System;
using NUnit.Framework;
using de.unika.ipd.grgen;
using de.unika.ipd.grgen.ast;
using de.unika.ipd.grgen.util.report;

namespace unittest
{
    /// <summary>
    /// Acceptance tests for the GrGen frontend compiler.
    /// Uses NUnit as a safety net to verify that known-good inputs compile without errors.
    ///
    /// Extends Frontend via AcceptanceTestFrontend, overriding SystemExit() to throw instead of terminating.
    /// The grg/gm test input files are copied to the build output directory by the project; tests reference them by bare filename.
    /// </summary>
    [TestFixture]
    public class AcceptanceTest
    {
        public class SystemExitException : Exception
        {
            public readonly int exitCode;

            public SystemExitException(int exitCode) : base("SystemExit(" + exitCode + ")")
            {
                this.exitCode = exitCode;
            }
        }

        internal class AcceptanceTestFrontend : Frontend
        {
            internal AcceptanceTestFrontend(string[] args) : base(args)
            {
            }

            protected override void SystemExit(int status)
            {
                throw new SystemExitException(status);
            }

            public static void DoStaticInit()
            {
                StaticInit();
            }

            public void RunCompiler()
            {
                Run();
            }
        }

        [OneTimeSetUp]
        public static void StaticSetup()
        {
            AcceptanceTestFrontend.DoStaticInit();
        }

        [SetUp]
        public void Setup()
        {
            ErrorReporter.ResetCounters();
            UnitNode.ClearRoot();
        }

        private void Compile(params string[] inputFiles)
        {
            AcceptanceTestFrontend frontend = new AcceptanceTestFrontend(inputFiles);
            frontend.RunCompiler();
        }

        [Test]
        public void AllTypesWithAllTypesModel()
        {
            Compile("all_types.grg");
            Assert.AreEqual(0, ErrorReporter.ErrorCount, "No errors after compilation");
            Assert.AreEqual(0, ErrorReporter.WarnCount, "No warnings after compilation");
        }

        [Test]
        public void NestedAndSubpatterns()
        {
            Compile("nested_and_subpatterns.grg");
            Assert.AreEqual(0, ErrorReporter.ErrorCount, "No errors after compilation");
            Assert.AreEqual(0, ErrorReporter.WarnCount, "No warnings after compilation");
        }

        [Test]
        public void AdvancedConstructs()
        {
            Compile("advanced_constructs.grg");
            Assert.AreEqual(0, ErrorReporter.ErrorCount, "No errors after compilation");
            Assert.AreEqual(0, ErrorReporter.WarnCount, "No warnings after compilation");
        }
    }
}
