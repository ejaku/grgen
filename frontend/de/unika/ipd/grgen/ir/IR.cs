/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ir
{

	using System.Collections.Generic;

	using Base = de.unika.ipd.grgen.util.Base;
	using GraphDumpable = de.unika.ipd.grgen.util.GraphDumpable;
	using GraphDumper = de.unika.ipd.grgen.util.GraphDumper;
	using XMLDumpable = de.unika.ipd.grgen.util.XMLDumpable;

	/// <summary>
	/// Base class for all IR classes.
	/// </summary>
	public abstract class IR : Base, GraphDumpable, XMLDumpable
	{
		/// <summary>
		/// The name of this IR object </summary>
		private string name;

		/// <summary>
		/// Names of the children of this node </summary>
		private string[] childrenNames;

		/// <summary>
		/// children name object for children without names </summary>
		private static readonly string[] noChildrenNames = new string[] { };

		private bool canonicalValid = false;

		/// <summary>
		/// Make a new IR object and name it. </summary>
		protected internal IR(string name)
		{
			this.name = name;
			childrenNames = noChildrenNames;
		}

		/// <returns> true, if this ir object is bad, false otherwise. </returns>
		public virtual bool IsBad()
		{
			return false;
		}

		/// <returns> The name of this IR object (that is group, node, edge, test, ...). </returns>
		public virtual string Name
		{
			get
			{
				return name;
			}
			set // Set the name of this IR object.
			{
			name = value;
			}
		}


		/// <summary>
		/// View an IR object as a string.
		/// The string of an IR object is its name. </summary>
		/// <seealso cref="java.lang.Object.toString()"/>
		public override string ToString()
		{
			return name;
		}

		/// <summary>
		/// Set the names of the children of this node. </summary>
		/// <param name="names"> A string array with the names. </param>
		protected internal virtual string[] ChildrenNames
		{
			set
			{
				this.childrenNames = value;
			}
		}

		/// <summary>
		/// Build the canonical form.
		/// Compound types must sort their members alphabetically.
		/// </summary>
		protected internal virtual void CanonicalizeLocal()
		{
			// default implementation for IR objects without named members
		}

		public void Canonicalize()
		{
			if(!canonicalValid)
			{
				CanonicalizeLocal();
				canonicalValid = true;
			}
		}

		protected internal void InvalidateCanonical()
		{
			canonicalValid = false;
		}

		//////////////////////////////////////////////////////////////////////////////////////////
		// XML dumping
		//////////////////////////////////////////////////////////////////////////////////////////

		/// <returns> Name of the tag as string. </returns>
		public virtual string TagName
		{
			get
			{
				return Name.Replace(' ', '_');
			}
		}

		/// <returns> Name of the tag that expresses a reference to this object. </returns>
		public virtual string RefTagName
		{
			get
			{
				return Name.Replace(' ', '_') + "_ref";
			}
		}

		/// <summary>
		/// Add the XML fields to a map. </summary>
		/// <param name="fields"> The map to add the fields to. </param>
		public virtual void AddFields(IDictionary<string, object> fields)
		{
			// empty
		}

		/// <returns> A unique ID for this object. </returns>
		public virtual string XMLId
		{
			get
			{
				return Id;
			}
		}

		//////////////////////////////////////////////////////////////////////////////////////////
		// graph dumping
		//////////////////////////////////////////////////////////////////////////////////////////

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeId() "/>
		public virtual string NodeId
		{
			get
			{
				return Id;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeColor() "/>
		public virtual Color NodeColor
		{
			get
			{
				return Color.WHITE;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeShape() "/>
		public virtual int NodeShape
		{
			get
			{
				return GraphDumper.DEFAULT;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeLabel() "/>
		public virtual string NodeLabel
		{
			get
			{
				return name;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeInfo() "/>
		public virtual string NodeInfo
		{
			get
			{
				return "ID: " + Id;
			}
		}

		/// <summary>
		/// By default this object has the number of the edge as edge label. </summary>
		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getEdgeLabel(int)"/>
		public virtual string GetEdgeLabel(int edge)
		{
			return edge < childrenNames.Length ? childrenNames[edge] : "" + edge;
		}
	}

}
