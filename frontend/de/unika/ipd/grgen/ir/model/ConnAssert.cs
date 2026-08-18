/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.model
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Denotes the connections assertions of nodes and edges.
	/// </summary>

	using IR = de.unika.ipd.grgen.ir.IR;
	using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;

	public class ConnAssert : IR
	{
		private readonly long srcLower;
		private readonly long srcUpper;
		private readonly long tgtLower;
		private readonly long tgtUpper;
		private readonly NodeType srcType;
		private readonly NodeType tgtType;
		private readonly bool bothDirections;

		public ConnAssert(NodeType srcType, long srcLower, long srcUpper,
				NodeType tgtType, long tgtLower, long tgtUpper,
				bool bothDirections)
			: base("conn assert")
		{
			this.srcType = srcType;
			this.srcLower = srcLower;
			this.srcUpper = srcUpper;
			this.tgtType = tgtType;
			this.tgtLower = tgtLower;
			this.tgtUpper = tgtUpper;
			this.bothDirections = bothDirections;
		}

		public virtual NodeType SrcType
		{
			get
			{
				return srcType;
			}
		}

		public virtual NodeType TgtType
		{
			get
			{
				return tgtType;
			}
		}

		public virtual long SrcLower
		{
			get
			{
				return srcLower;
			}
		}

		public virtual long SrcUpper
		{
			get
			{
				return srcUpper;
			}
		}

		public virtual long TgtLower
		{
			get
			{
				return tgtLower;
			}
		}

		public virtual long TgtUpper
		{
			get
			{
				return tgtUpper;
			}
		}

		public virtual bool BothDirections
		{
			get
			{
				return bothDirections;
			}
		}

		public override void AddFields(IDictionary<string, object> fields)
		{
			base.AddFields(fields);
			fields["src_lower"] = Convert.ToString(srcLower);
			fields["src_upper"] = Convert.ToString(srcUpper);
			fields["tgt_lower"] = Convert.ToString(tgtLower);
			fields["tgt_upper"] = Convert.ToString(tgtUpper);
			fields["src_type"] = MyCollectionHelper.CreateSingletonSet(SrcType);
			fields["tgt_type"] = MyCollectionHelper.CreateSingletonSet(TgtType);
		}

		/// <summary>
		/// Compares a given connection assert with <code>this</code> one. </summary>
		/// <returns> a negative integer, zero, or a positive integer as the
		/// 	       argument is less than, equal to, or greater than
		///	       <code>this</code> connection assertion. </returns>
		public virtual int CompareTo(ConnAssert ca)
		{
			if(srcLower == ca.srcLower &&
				srcUpper == ca.srcUpper &&
				tgtLower == ca.tgtLower &&
				tgtUpper == ca.tgtUpper &&
				SrcType == ca.SrcType &&
				TgtType == ca.TgtType)
			{
				return 0;
			}

			if(this.srcLower < ca.srcLower)
			{
				if(this.srcUpper < ca.srcUpper)
				{
					if(this.tgtLower < ca.tgtLower)
					{
						if(this.tgtUpper < ca.tgtUpper)
							return -1;
					}
				}
			}

			return 1;
		}

		public override string ToString()
		{
			return Name +
					" {" +
					"(" + srcType + " [" + srcLower + ".." + srcUpper + "])," +
					"(" + tgtType + " [" + tgtLower + ".." + tgtUpper + "])" +
					"}";
		}
	}

}
