/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.sql.meta.Column;
import de.unika.ipd.grgen.be.sql.meta.Table;
import java.io.PrintStream;


/**
 * A table with an ID.
 */
public interface IdTable extends Table {

	Column colId();
	
	PrintStream genUpdateStmt(PrintStream sb, Column col);
	PrintStream genGetStmt(PrintStream sb, Column col);
	
	
}
