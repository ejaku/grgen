/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

/**
 * A checker, that checks for the type of a declaration.
 * It basically uses {@link de.unika.ipd.grgen.ast.util.MultChecker}, 
 * sice this is just a special case, causing nearly no overhead.
 */
public class DeclTypeChecker extends MultChecker {

	/**
	 * Make a new decl type checker using one class specifying the
	 * type the declarations type child must be of.
	 * @param type
	 */
	public DeclTypeChecker(Class type) {
		super(new Class[] { type });
	}
}
