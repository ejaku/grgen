/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

/**
 * Something that has been declared.
 */
public interface DeclaredCharacter {

	/**
	 * Get the declaration of this declaration character.
	 * @return 
	 */
	DeclNode getDecl();

}
