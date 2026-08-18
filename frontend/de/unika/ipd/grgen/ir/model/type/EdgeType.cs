/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ir.model.type
{

	using System.Collections.Generic;
	using System.Text;

	using ContainedInPackage = de.unika.ipd.grgen.ir.ContainedInPackage;
	using Ident = de.unika.ipd.grgen.ir.Ident;
	using ConnAssert = de.unika.ipd.grgen.ir.model.ConnAssert;

	/// <summary>
	/// IR class that represents edge types.
	/// </summary>
	public class EdgeType : InheritanceType, ContainedInPackage
	{
		private string packageContainedIn;

		/// <summary>
		/// The connection assertions. </summary>
		private readonly List<ConnAssert> connectionAsserts = new List<ConnAssert>();

		public enum DirectednessKind
		{
			Arbitrary,
			Directed,
			Undirected
		}

		protected internal DirectednessKind directedness;

		/// <summary>
		/// Make a new edge type. </summary>
		/// <param name="ident"> The identifier declaring this type. </param>
		/// <param name="modifiers"> The modifiers for this type. </param>
		/// <param name="externalName"> The name of the external implementation of this type or null. </param>
		public EdgeType(Ident ident, int modifiers, string externalName)
			: base("edge type", ident, modifiers, externalName)
		{
		}

		public virtual DirectednessKind Directedness
		{
			get
			{
				return directedness;
			}
			set
			{
				directedness = value;
			}
		}


		/// <summary>
		/// Sorts the Connection assertion of this edge type,
		/// so that the computed graph model digest is stable according to semantically equivalent connection assertions.
		/// The order of the sorting is given by the <code>compareTo</code> method.
		/// </summary>
		public virtual void CanonicalizeConnectionAsserts()
		{
			connectionAsserts.Sort(new ComparatorAnonymousInnerClass());
		}

		private class ComparatorAnonymousInnerClass : IComparer<ConnAssert>
		{
			public int Compare(ConnAssert ca1, ConnAssert ca2)
			{
				return ca1.CompareTo(ca2);
			}
		}

		/// <summary>
		/// Add the given connection assertion to this edge type. </summary>
		public virtual void AddConnAssert(ConnAssert ca)
		{
			connectionAsserts.Add(ca);
		}

		/// <summary>
		/// Get all connection assertions. </summary>
		public virtual ICollection<ConnAssert> ConnAsserts
		{
			get
			{
				return connectionAsserts.AsReadOnly();
			}
		}

		public override void AddFields(IDictionary<string, object> fields)
		{
			base.AddFields(fields);
			fields["conn_asserts"] = connectionAsserts.GetEnumerator();
		}

		public override void AddToDigest(StringBuilder sb)
		{
			base.AddToDigest(sb);

			sb.Append('[');
			int i = 0;
			foreach(ConnAssert ca in connectionAsserts)
			{
				if(i > 0)
					sb.Append(',');
				sb.Append(ca.ToString());
				++i;
			}
			sb.Append(']');
		}

		/// <seealso cref="de.unika.ipd.grgen.ir.type.Type.classify() "/>
		public override TypeClass Classify()
		{
			return TypeClass.IS_EDGE;
		}

		public virtual string PackageContainedIn
		{
			get
			{
				return packageContainedIn;
			}
			set
			{
				this.packageContainedIn = value;
			}
		}

	}

}
