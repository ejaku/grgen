/**
 * Created on Mar 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.graph;

import java.util.Iterator;


/**
 * A type with attributes.
 */
public interface AttributedType extends InheritanceType {

	Iterator getAttributeTypes();
	
}
