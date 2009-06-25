/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * GraphDumperFactory.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.util;

public interface GraphDumperFactory {

	GraphDumper get(String fileNamePart);

}

