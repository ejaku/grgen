/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.util
{

	/// <summary>
	/// A Graph Dumpable proxy class.
	/// </summary>
	public class GraphDumpableProxy : GraphDumpable
	{
		/// <summary>
		/// The GraphDumpable to be proxy for. </summary>
		private GraphDumpable gd;

		public GraphDumpableProxy(GraphDumpable gd)
		{
			this.gd = gd;
		}

		/// <summary>
		/// Get the proxied object. </summary>
		/// <returns> The proxied GraphDumpable object. </returns>
		protected internal virtual GraphDumpable GraphDumpable
		{
			get
			{
				return gd;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeId()"/>
		public virtual string NodeId
		{
			get
			{
				return gd.NodeId;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeColor()"/>
		public virtual Color NodeColor
		{
			get
			{
				return gd.NodeColor;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeShape()"/>
		public virtual int NodeShape
		{
			get
			{
				return gd.NodeShape;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeLabel()"/>
		public virtual string NodeLabel
		{
			get
			{
				return gd.NodeLabel;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeInfo()"/>
		public virtual string NodeInfo
		{
			get
			{
				return gd.NodeInfo;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getEdgeLabel(int)"/>
		public virtual string GetEdgeLabel(int edge)
		{
			return gd.GetEdgeLabel(edge);
		}
	}

}
