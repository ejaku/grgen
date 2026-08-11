/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.util.report
{
	/// <summary>
	/// An empty location.
	/// </summary>
	public class EmptyLocation : Location
	{
		private static readonly EmptyLocation EMPTY;

		static EmptyLocation()
		{
			EMPTY = new EmptyLocation();
		}

		/// <summary>
		/// Return the empty string always. </summary>
		/// <seealso cref="de.unika.ipd.grgen.util.report.Location.getLocation()"/>
		public virtual string Location
		{
			get
			{
				return "<nowhere>";
			}
		}

		/// <summary>
		/// This location is never valid. </summary>
		/// <seealso cref="de.unika.ipd.grgen.util.report.Location.hasLocation()"/>
		public virtual bool HasLocation()
		{
			return false;
		}

		/// <summary>
		/// Get a new empty location </summary>
		/// <returns> an empty location </returns>
		public static EmptyLocation EmptyLoc
		{
			get
			{
				return EMPTY;
			}
		}
	}

}
