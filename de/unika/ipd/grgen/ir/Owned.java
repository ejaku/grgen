/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * Something that is owned by another IR structure.
 */
public interface Owned {
	
	IR getOwner();

}
