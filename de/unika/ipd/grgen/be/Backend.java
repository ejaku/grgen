/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.be;

import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import java.io.File;

/**
 * Generic Backend interface.
 */
public interface Backend {
	
	/**
	 * Initialize the backend with the intermediate representation.
	 * @param unit The intermediate representation unit to
	 * generate code for.
	 * @param reporter An error reporter to which the backend send
	 * messages to.
	 * @param outputPath The output path, where
	 * all generated files should go.
	 */
	void init(Unit unit, ErrorReporter reporter, File outputPath);
	
	/**
	 * Initiates the generation of code.
	 * It is always called after {@link #init(IR)}.
	 */
	void generate();

	/**
	 * Clearup some things, perhaps.
	 * Called after {@link #generate}.
	 */
	void done();

}
