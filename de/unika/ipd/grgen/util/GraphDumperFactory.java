/**
 * GraphDumperFactory.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.util;

import java.io.File;

public interface GraphDumperFactory {
	
	GraphDumper get(File f);
	
}

