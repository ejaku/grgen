/**
 * Sys.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen;

import de.unika.ipd.grgen.util.report.ErrorReporter;
import java.io.File;
import java.io.OutputStream;

public interface Sys {
	
	File[] getModelPaths();
	
	ErrorReporter getErrorReporter();
	
	OutputStream createDebugFile(File file);
	
	boolean backendEmitDebugFiles();
	
}

