/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.be;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.ir.Unit;
import java.io.File;

/**
 * Generic Backend interface.
 */
public interface Backend {
	
	/**
	 * Initialize the backend with the intermediate representation.
	 * @param unit The intermediate representation unit to
	 * generate code for.
	 * @param sys The system.
	 * @param outputPath The output path, where
	 * all generated files should go.
	 */
	void init(Unit unit, Sys system, File outputPath);
	
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

