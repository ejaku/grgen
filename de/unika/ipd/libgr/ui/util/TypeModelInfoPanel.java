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
