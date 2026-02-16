// by Claude Code with Edgar Jakumeit

import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

import de.unika.ipd.grgen.Main;
import de.unika.ipd.grgen.ast.UnitNode;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * Acceptance tests for the GrGen frontend compiler.
 * Uses JUnit as a safety net to verify that known-good inputs compile without errors.
 *
 * Extends Main via AcceptanceTestMain, overriding systemExit() to throw instead of terminating.
 */
public class AcceptanceTest
{
	/** Exception thrown by AcceptanceTestMain.systemExit() instead of terminating the process. */
	public static class SystemExitException extends RuntimeException
	{
		public final int exitCode;

		public SystemExitException(int exitCode)
		{
			super("systemExit(" + exitCode + ")");
			this.exitCode = exitCode;
		}
	}

	/** Main subclass that throws SystemExitException instead of calling System.exit(). */
	static class AcceptanceTestMain extends Main
	{
		protected AcceptanceTestMain(String[] args)
		{
			super(args);
		}

		@Override
		protected void systemExit(int status)
		{
			throw new SystemExitException(status);
		}

		/** Expose staticInit() for test setup. */
		public static void doStaticInit()
		{
			staticInit();
		}

		/** Public entry point for tests. */
		public void runCompiler()
		{
			run();
		}
	}

	@BeforeClass
	public static void staticSetup()
	{
		AcceptanceTestMain.doStaticInit();
	}

	@Before
	public void setup()
	{
		ErrorReporter.resetCounters();
		UnitNode.clearRoot();
	}

	/** Run the compiler on the given input files (without backend/code generation). */
	private void compile(String... inputFiles)
	{
		AcceptanceTestMain main = new AcceptanceTestMain(inputFiles);
		main.runCompiler();
	}

	@Test
	public void allTypesWithAllTypesModel()
	{
		compile("unittest/all_types.grg", "unittest/all_types_model.gm");
		assertEquals("No errors after compilation", 0, ErrorReporter.getErrorCount());
		assertEquals("No warnings after compilation", 0, ErrorReporter.getWarnCount());
	}

	@Test
	public void nestedAndSubpatterns()
	{
		compile("unittest/nested_and_subpatterns.grg", "unittest/nested_and_subpatterns_model.gm");
		assertEquals("No errors after compilation", 0, ErrorReporter.getErrorCount());
		assertEquals("No warnings after compilation", 0, ErrorReporter.getWarnCount());
	}

	@Test
	public void advancedConstructs()
	{
		compile("unittest/advanced_constructs.grg", "unittest/advanced_constructs_model.gm");
		assertEquals("No errors after compilation", 0, ErrorReporter.getErrorCount());
		assertEquals("No warnings after compilation", 0, ErrorReporter.getWarnCount());
	}
}
