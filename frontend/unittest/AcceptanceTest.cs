// by Claude Code with Edgar Jakumeit

namespace unittest
{
using System;

using Before = org.junit.Before;
using BeforeClass = org.junit.BeforeClass;
using Test = org.junit.Test;
// JAVA TO C# CONVERTER TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.junit.Assert.*;

using Main = de.unika.ipd.grgen.Main;
using UnitNode = de.unika.ipd.grgen.ast.UnitNode;
using ErrorReporter = de.unika.ipd.grgen.util.report.ErrorReporter;

/// <summary>
/// Acceptance tests for the GrGen frontend compiler.
/// Uses JUnit as a safety net to verify that known-good inputs compile without errors.
/// 
/// Extends Main via AcceptanceTestMain, overriding systemExit() to throw instead of terminating.
/// </summary>
public class AcceptanceTest
{
	/// <summary>
	/// Exception thrown by AcceptanceTestMain.systemExit() instead of terminating the process. </summary>
	public class SystemExitException : Exception
	{
		public readonly int exitCode;

		public SystemExitException(int exitCode) : base("systemExit(" + exitCode + ")")
		{
			this.exitCode = exitCode;
		}
	}

	/// <summary>
	/// Main subclass that throws SystemExitException instead of calling System.exit(). </summary>
	internal class AcceptanceTestMain : Main
	{
		protected internal AcceptanceTestMain(string[] args) : base(args)
		{
		}

		protected internal override void SystemExit(int status)
		{
			throw new SystemExitException(status);
		}

		/// <summary>
		/// Expose staticInit() for test setup. </summary>
		public static void DoStaticInit()
		{
			StaticInit();
		}

		/// <summary>
		/// Public entry point for tests. </summary>
		public virtual void RunCompiler()
		{
			Run();
		}
	}

// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
// ORIGINAL LINE: @BeforeClass public static void staticSetup()
	public static void StaticSetup()
	{
		AcceptanceTestMain.DoStaticInit();
	}

// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
// ORIGINAL LINE: @Before public void setup()
	public virtual void Setup()
	{
		ErrorReporter.ResetCounters();
		UnitNode.ClearRoot();
	}

	/// <summary>
	/// Run the compiler on the given input files (without backend/code generation). </summary>
	private void Compile(params string[] inputFiles)
	{
		AcceptanceTestMain main = new AcceptanceTestMain(inputFiles);
		main.RunCompiler();
	}

// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
// ORIGINAL LINE: @Test public void allTypesWithAllTypesModel()
	public virtual void AllTypesWithAllTypesModel()
	{
		Compile("unittest/all_types.grg", "unittest/all_types_model.gm");
		assertEquals("No errors after compilation", 0, ErrorReporter.ErrorCount);
		assertEquals("No warnings after compilation", 0, ErrorReporter.WarnCount);
	}

// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
// ORIGINAL LINE: @Test public void nestedAndSubpatterns()
	public virtual void NestedAndSubpatterns()
	{
		Compile("unittest/nested_and_subpatterns.grg", "unittest/nested_and_subpatterns_model.gm");
		assertEquals("No errors after compilation", 0, ErrorReporter.ErrorCount);
		assertEquals("No warnings after compilation", 0, ErrorReporter.WarnCount);
	}

// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
// ORIGINAL LINE: @Test public void advancedConstructs()
	public virtual void AdvancedConstructs()
	{
		Compile("unittest/advanced_constructs.grg", "unittest/advanced_constructs_model.gm");
		assertEquals("No errors after compilation", 0, ErrorReporter.ErrorCount);
		assertEquals("No warnings after compilation", 0, ErrorReporter.WarnCount);
	}
}

}
