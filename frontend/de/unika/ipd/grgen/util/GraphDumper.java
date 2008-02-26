/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
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
