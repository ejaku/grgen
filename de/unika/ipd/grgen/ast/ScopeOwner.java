/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

/**
 * Something, that owns a scope.
 */
public interface ScopeOwner {

	boolean fixupDefinition(IdentNode id);

}
