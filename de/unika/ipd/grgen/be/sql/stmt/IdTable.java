/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.sql.meta.*;

import java.io.PrintStream;


/**
 * A table with an ID.
 */
public interface IdTable extends Table {

	Column colId();
	
	ManipulationStatement genUpdateStmt(StatementFactory factory, MarkerSource ms, Column col);
	Query genGetStmt(StatementFactory factory, MarkerSource ms, Column col);

}
