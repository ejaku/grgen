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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

import de.unika.ipd.grgen.ast.BaseNode;
import java.util.HashSet;
import java.util.Iterator;
import javax.swing.event.TreeModelEvent;
import javax.swing.event.TreeModelListener;
import javax.swing.tree.TreeModel;
import javax.swing.tree.TreePath;

/**
 * A walkable class, that is also a tree model.
 * This means, that each class extending this one can be
 * viewed in a swing tree view.
 */
public abstract class TreeModelWalkable implements TreeModel, Walkable {

	/** The root of the tree. */
	private Object root;
	
	/** All tree model listeners */
	private HashSet<TreeModelListener> listeners;
	
	/** Auxillary set for children getter. */
	private HashSet aux;
	
	protected TreeModelWalkable() {
		listeners = new HashSet<TreeModelListener>();
		root = null;
	}
	
	/**
	 * Set the root of the tree.
	 * @param object The root's tree.
	 */
	protected void setRoot(Object object) {
		root = object;
	}

  /**
   * @see javax.swing.tree.TreeModel#getRoot()
   */
  public Object getRoot() {
    return root;
  }

  /**
   * @see javax.swing.tree.TreeModel#getChild(java.lang.Object, int)
   */
  public Object getChild(Object arg0, int arg1) {
  	Object res = null;
  	Walkable w = (Walkable) arg0;
  	
  	Iterator<? extends BaseNode> it = w.getWalkableChildren().iterator();
  	
  	for(int i = 0; i < arg1; i++) {
  		assert it.hasNext() : "children iterator must have at least " + arg1
  		  + " childs";
  		res = it.next();
  	}
  	
  	return res;
  }

  /**
   * @see javax.swing.tree.TreeModel#getChildCount(java.lang.Object)
   */
  public int getChildCount(Object arg0) {
  	int i = 0;
  	Walkable w = (Walkable) arg0;
  	for(Iterator<? extends BaseNode> it = w.getWalkableChildren().iterator(); it.hasNext(); i++);
		return i;
  }

  /**
   * @see javax.swing.tree.TreeModel#isLeaf(java.lang.Object)
   */
  public boolean isLeaf(Object arg0) {
    Walkable w = (Walkable) arg0;
    return w.getWalkableChildren().isEmpty();
  }

  /**
   * @see javax.swing.tree.TreeModel#valueForPathChanged(javax.swing.tree.TreePath, java.lang.Object)
   */
  public void valueForPathChanged(TreePath arg0, Object arg1) {
  }

  /**
   * @see javax.swing.tree.TreeModel#getIndexOfChild(java.lang.Object, java.lang.Object)
   */
  public int getIndexOfChild(Object arg0, Object arg1) {
  	int i = 0;
  	Walkable w = (Walkable) arg0;

  	for(Iterator<? extends BaseNode> it = w.getWalkableChildren().iterator(); it.hasNext(); i++) {
  		if(it.next().equals(arg1))
  			break;
  	}
  	
  	return i;
  }

  /**
   * @see javax.swing.tree.TreeModel#addTreeModelListener(javax.swing.event.TreeModelListener)
   */
  public void addTreeModelListener(TreeModelListener arg0) {
    listeners.add(arg0);
  }

  /**
   * @see javax.swing.tree.TreeModel#removeTreeModelListener(javax.swing.event.TreeModelListener)
   */
  public void removeTreeModelListener(TreeModelListener arg0) {
  	listeners.remove(arg0);
  }

	/**
	 * Notify all listeners, that the tree has changed
	 */
	public void notifyListeners() {
		TreeModelEvent event = new TreeModelEvent(this, new Object[] { this });
		
		for(Iterator<TreeModelListener> it = listeners.iterator(); it.hasNext(); ) {
			TreeModelListener listener = it.next();
			listener.treeStructureChanged(event);
		}
				
	}

}
