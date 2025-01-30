/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.util.report;

import java.util.Enumeration;
import java.util.Vector;

import javax.swing.tree.DefaultTreeModel;
import javax.swing.tree.TreeNode;

public class TreeHandler extends DefaultTreeModel implements Handler
{
	private static final long serialVersionUID = 9154761780509127571L;

	class EnterNode implements TreeNode
	{
		private Vector<TreeNode> children = new Vector<TreeNode>();
		private String text;
		private TreeNode parent;

		public EnterNode(TreeNode parent, String text)
		{
			this.text = text;
			this.parent = parent;
		}

		public void add(TreeNode n)
		{
			children.add(n);
		}

		@Override
		public TreeNode getChildAt(int i)
		{
			return children.get(i);
		}

		@Override
		public int getChildCount()
		{
			return children.size();
		}

		@Override
		public TreeNode getParent()
		{
			return parent;
		}

		@Override
		public int getIndex(TreeNode arg0)
		{
			return children.indexOf(arg0);
		}

		@Override
		public boolean getAllowsChildren()
		{
			return true;
		}

		@Override
		public boolean isLeaf()
		{
			return children.size() == 0;
		}

		@Override
		public Enumeration<TreeNode> children()
		{
			return children.elements();
		}

		@Override
		public String toString()
		{
			return text;
		}

		public int[] getAddedChildIndices()
		{
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
	private class MsgNode implements TreeNode
	{
		private String msg;

		public MsgNode(TreeNode parent, int level, Location loc, String msg)
		{
			this.msg = msg;
		}

		@Override
		public TreeNode getChildAt(int arg0)
		{
			return null;
		}

		@Override
		public int getChildCount()
		{
			return 0;
		}

		@Override
		public TreeNode getParent()
		{
			return null;
		}

		@Override
		public int getIndex(TreeNode arg0)
		{
			return 0;
		}

		@Override
		public boolean getAllowsChildren()
		{
			return false;
		}

		@Override
		public boolean isLeaf()
		{
			return true;
		}

		@Override
		public Enumeration<? extends TreeNode> children()
		{
			return null;
		}

		@Override
		public String toString()
		{
			return msg;
		}
	}

	private EnterNode current;

	public TreeHandler()
	{
		super(null);
		EnterNode root = new EnterNode(null, "ROOT");
		setRoot(root);
		current = root;
	}

	/**
	 * @see de.unika.ipd.grgen.util.report.Handler#report(int, de.unika.ipd.grgen.util.report.Location, java.lang.String)
	 */
	@Override
	public void report(int level, Location loc, String msg)
	{
		current.add(new MsgNode(current, level, loc, msg));
	}

	/**
	 * @see de.unika.ipd.grgen.util.report.Handler#entering(java.lang.String)
	 */
	public void entering(String s)
	{
		EnterNode n = new EnterNode(current, s);
		current.add(n);
		current = n;
	}

	/**
	 * @see de.unika.ipd.grgen.util.report.Handler#leaving()
	 */
	public void leaving()
	{
		EnterNode p = (EnterNode)current.getParent();
		if(p != null)
			current = p;
	}
}
