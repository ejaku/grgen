/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util.report;

import java.util.Enumeration;
import java.util.Vector;

import javax.swing.tree.DefaultTreeModel;
import javax.swing.tree.TreeNode;

/**
 *
 */
public class TreeHandler extends DefaultTreeModel implements Handler {

	/**
	 *
	 */
	private static final long serialVersionUID = 9154761780509127571L;

	class EnterNode implements TreeNode {
		private Vector<TreeNode> children = new Vector<TreeNode>();
		private String text;
		private TreeNode parent;

		public EnterNode(TreeNode parent, String text) {
			this.text = text;
			this.parent = parent;
		}

		public void add(TreeNode n) {
			children.add(n);
		}

		public TreeNode getChildAt(int i) {
			return children.get(i);
		}

		public int getChildCount() {
			return children.size();
		}

		public TreeNode getParent() {
			return parent;
		}

		public int getIndex(TreeNode arg0) {
			return children.indexOf(arg0);
		}

		public boolean getAllowsChildren() {
			return true;
		}

		public boolean isLeaf() {
			return children.size() == 0;
		}

		public Enumeration<TreeNode> children() {
			return children.elements();
		}

		public String toString() {
			return text;
		}

		public int[] getAddedChildIndices() {
			int[] res = new int[children.size()];
			for(int i = 0; i < res.length; i++)
				res[i] = i;
			return res;
		}
	}
  /**
   * A tree node representing a log message
   * These nodes are always leaves.
   */
  private class MsgNode implements TreeNode {

	  	// TODO use or remove it
	  	// private TreeNode parent;
		// private int level;
		// private Location loc;
		private String msg;

		public MsgNode(TreeNode parent, int level, Location loc, String msg) {
			// this.level = level;
			// this.loc = loc;
			this.msg = msg;
			// this.parent = parent;
		}

    public TreeNode getChildAt(int arg0) {
      return null;
    }

    public int getChildCount() {
      return 0;
    }

    public TreeNode getParent() {
      return null;
    }

    public int getIndex(TreeNode arg0) {
      return 0;
    }

    public boolean getAllowsChildren() {
      return false;
    }

    public boolean isLeaf() {
      return true;
    }

    public Enumeration<?> children() {
      return null;
    }

    public String toString() {
    	return msg;
    }
  }

  private EnterNode current;

  // TODO use or remove it
  // private EnterNode root;

  /**
   *
   */
  public TreeHandler() {
  	super(null);
  	EnterNode root = new EnterNode(null, "ROOT");
  	setRoot(root);
  	current = root;
  }

  /**
   * @see de.unika.ipd.grgen.util.report.Handler#report(int, de.unika.ipd.grgen.util.report.Location, java.lang.String)
   */
  public void report(int level, Location loc, String msg) {
    current.add(new MsgNode(current, level, loc, msg));
  }

  /**
   * @see de.unika.ipd.grgen.util.report.Handler#entering(java.lang.String)
   */
  public void entering(String s) {
  	EnterNode n = new EnterNode(current, s);
  	current.add(n);
  	current = n;
  }

  /**
   * @see de.unika.ipd.grgen.util.report.Handler#leaving()
   */
  public void leaving() {
  	EnterNode p = (EnterNode) current.getParent();
		if(p != null)
		  current = p;
  }

}
