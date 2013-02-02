/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir;

/**
 * Class for the different kinds of indexes available for accessing a storage for binding a pattern element
 */
public class StorageAccessIndex {
//	public Variable indexVariable = null;
//	public Qualification indexAttribute = null;
	public GraphEntity indexGraphEntity = null;
//	public GraphEntity indexGlobalVariable = null;

//	public StorageAccessIndex(Variable indexVariable) {
//		this.indexVariable = indexVariable;
//	}

//	public StorageAccessIndex(Qualification indexAttribute) {
//		this.indexAttribute = indexAttribute;
//	}

	public StorageAccessIndex(GraphEntity indexGraphEntityOrGlobalVariable) {
		PatternGraph directlyNestingLHSGraph;
		if(indexGraphEntityOrGlobalVariable instanceof Node) {
			directlyNestingLHSGraph = ((Node)indexGraphEntityOrGlobalVariable).directlyNestingLHSGraph;
		} else {
			directlyNestingLHSGraph = ((Edge)indexGraphEntityOrGlobalVariable).directlyNestingLHSGraph;
		}
		if(directlyNestingLHSGraph != null) {
			this.indexGraphEntity = indexGraphEntityOrGlobalVariable;
		} else {
//			this.indexGlobalVariable = indexGraphEntityOrGlobalVariable;
		}
	}
}
