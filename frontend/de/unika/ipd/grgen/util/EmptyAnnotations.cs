/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.util
{

	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Empty annotations.
	/// </summary>
	public class EmptyAnnotations : Annotations
	{
		private static readonly Annotations EMPTY = new EmptyAnnotations();

		public static Annotations Get()
		{
			return EMPTY;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.Annotations.containsKey(java.lang.String) "/>
		public virtual bool ContainsKey(string key)
		{
			return false;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.Annotations.get(java.lang.String) "/>
		public virtual object Get(string key)
		{
			return null;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.Annotations.put(java.lang.String, java.lang.Object) "/>
		public virtual void Put(string key, object value)
		{
			throw new Exception("Not implemented! Would be silently swallowed, which would be typically a bug.");
		}

		/// <seealso cref="de.unika.ipd.grgen.util.Annotations.isInteger(java.lang.String) "/>
		public virtual bool IsInteger(string key)
		{
			return false;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.Annotations.isBoolean(java.lang.String) "/>
		public virtual bool IsBoolean(string key)
		{
			return false;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.Annotations.isString(java.lang.String) "/>
		public virtual bool IsString(string key)
		{
			return false;
		}

		public virtual bool IsFlagSet(string key)
		{
			return false;
		}

		public virtual ICollection<string> KeySet()
		{
			return new HashSet<string>();
		}
	}

}
