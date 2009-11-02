/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.util;

import java.awt.Color;

/**
 * A Dumper for Graphs
 */
public interface GraphDumper {

	int DEFAULT = -1;

	int BOX = 0;
	int RHOMB = 1;
	int ELLIPSE = 2;
	int TRIANGLE = 3;

	int SOLID = 0;
	int DASHED = 1;
	int DOTTED = 2;

	void begin();
	void finish();

	void beginSubgraph(GraphDumpable d);
	void beginSubgraph(String name);
	void endSubgraph();

	void node(GraphDumpable d);

	void edge(GraphDumpable from, GraphDumpable to, String label, int style,
						Color color);

	void edge(GraphDumpable from, GraphDumpable to, String label, int style);
	void edge(GraphDumpable from, GraphDumpable to, String label);
	void edge(GraphDumpable from, GraphDumpable to);
}
