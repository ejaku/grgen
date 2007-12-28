/**
 * 
 */
package de.unika.ipd.grgen.util;

import de.unika.ipd.grgen.ir.Entity;

/**
 * @author adam
 * Something that is being retyped during the rewrite
 */
public interface Retyped {
	void setOldEntity(Entity old);
	
	Entity getOldEntity();
}
