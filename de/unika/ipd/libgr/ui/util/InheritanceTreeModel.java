/**
 * Created on Mar 15, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.ui.util;

import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import javax.swing.event.TreeModelListener;
import javax.swing.tree.TreeModel;
import javax.swing.tree.TreePath;

import de.unika.ipd.libgr.graph.AttributedType;


/**
 * 
 */
public class InheritanceTreeModel implements TreeModel {

	private Collection<TreeModelListener> listeners = new HashSet<TreeModelListener>();
	
	private AttributedType root;
	
	private Map<AttributedType, List> cache = new HashMap<AttributedType, List>();
	
	public InheritanceTreeModel(AttributedType root) {
		this.root = root;
	}
	
	private List getChilds(Object obj) {
		List res;
		
		if(cache.containsKey(obj)) {
			res = cache.get(obj);
		} else {
			res = new LinkedList();
			AttributedType ty = (AttributedType) obj;
			for(Iterator<Object> it = ty.getSubTypes(); it.hasNext();) 
				res.add(it.next());
			cache.put(ty, res);
		}
		
		return res;
	}
	
	/**
	 * @see javax.swing.tree.TreeModel#addTreeModelListener(javax.swing.event.TreeModelListener)
	 */
	public void addTreeModelListener(TreeModelListener l) {
		listeners.add(l);
	}
	
	/**
	 * @see javax.swing.tree.TreeModel#getChild(java.lang.Object, int)
	 */
	public Object getChild(Object parent, int index) {
		return getChilds(parent).get(index);
	}

	/**
	 * @see javax.swing.tree.TreeModel#getChildCount(java.lang.Object)
	 */
	public int getChildCount(Object parent) {
		return getChilds(parent).size();
	}

	/**
	 * @see javax.swing.tree.TreeModel#getIndexOfChild(java.lang.Object, java.lang.Object)
	 */
	public int getIndexOfChild(Object parent, Object child) {
		return getChilds(parent).indexOf(child);
	}

	/**
	 * @see javax.swing.tree.TreeModel#getRoot()
	 */
	public Object getRoot() {
		return root;
	}
	
	/**
	 * @see javax.swing.tree.TreeModel#isLeaf(java.lang.Object)
	 */
	public boolean isLeaf(Object node) {
		return getChilds(node).size() == 0;
	}
	
	/**
	 * @see javax.swing.tree.TreeModel#removeTreeModelListener(javax.swing.event.TreeModelListener)
	 */
	public void removeTreeModelListener(TreeModelListener l) {
		listeners.remove(l);
	}
	
	/**
	 * @see javax.swing.tree.TreeModel#valueForPathChanged(javax.swing.tree.TreePath, java.lang.Object)
	 */
	public void valueForPathChanged(TreePath path, Object newValue) {
	}
}
