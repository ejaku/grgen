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


/**
 * Created on Mar 15, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.ui.util;

import javax.swing.JPanel;
import javax.swing.JTabbedPane;
import javax.swing.JTree;

import de.unika.ipd.libgr.graph.EdgeType;
import de.unika.ipd.libgr.graph.NodeType;
import de.unika.ipd.libgr.graph.TypeModel;


/**
 * A graph info panel.
 */
public class TypeModelInfoPanel extends JPanel {

	private JTabbedPane tabbedPane = new JTabbedPane();
	private JPanel nodePanel = new JPanel();
	private JPanel edgePanel = new JPanel();
	
	public TypeModelInfoPanel(TypeModel typeModel) {

		NodeType nodeRoot = typeModel.getNodeRootType();
		EdgeType edgeRoot = typeModel.getEdgeRootType();
		
		JTree nodeTree = new JTree(new InheritanceTreeModel(nodeRoot));
		JTree edgeTree = new JTree(new InheritanceTreeModel(edgeRoot));
		
		nodePanel.add(nodeTree);
		edgePanel.add(edgeTree);
		
		tabbedPane.addTab("Node Types", nodePanel);
		tabbedPane.addTab("Edge Types", edgePanel);		
		
		add(tabbedPane);
	}

	
	
	
}
