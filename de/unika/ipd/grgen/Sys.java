/**
 * Sys.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen;

import de.unika.ipd.grgen.util.report.ErrorReporter;
import java.io.File;

public interface Sys {
	
	File[] getModelPaths();
	
	ErrorReporter getErrorReporter();
	
	boolean backendEmitDebugFiles();
	
}

