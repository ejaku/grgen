/**
 * Aggregate.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.be.sql.meta;

/**
 * An aggregate.
 */
public interface Aggregate extends Column {

	int MIN = 1;
	int MAX = 2;
	int SUM = 3;
	int COUNT = 4;
	
}

