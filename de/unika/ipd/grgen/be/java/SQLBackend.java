/**
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import de.unika.ipd.grgen.be.sql.SQLGenerator;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.libgr.actions.Action;
import de.unika.ipd.libgr.actions.Actions;


/**
 * A Java/SQL backend.
 */
public class SQLBackend extends JavaIdBackend implements Actions {

	
	/** Map action names to SQLActions. */
	Map actions = new HashMap();
	
	/** The SQL code generator. */
	SQLGenerator sqlGen;
	
	/** The database context. */
	DatabaseContext db;
	
	public void makeActions() {
		for(Iterator it = actionMap.keySet().iterator(); it.hasNext();) {
			MatchingAction a = (MatchingAction) it.next();
			int id = ((Integer) actionMap.get(a)).intValue();

			SQLAction act = new SQLAction(a, sqlGen, db);
			actions.put(a.getIdent().toString(), act);
		}
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.Backend#done()
	 */
	public void done() {
	}

	/**
	 * @see de.unika.ipd.grgen.be.Backend#generate()
	 */
	public void generate() {
		
	}
	
	/**
	 * @see de.unika.ipd.libgr.actions.Actions#getAction(java.lang.String)
	 */
	public Action getAction(String name) {
		return (Action) actions.get(name);
	}
	
	/**
	 * @see de.unika.ipd.libgr.actions.Actions#getActions()
	 */
	public Iterator getActions() {
		return actions.values().iterator();
	}
}
