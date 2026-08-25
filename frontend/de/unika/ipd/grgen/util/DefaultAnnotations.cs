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

	using System.Collections.Generic;

	/// <summary>
	/// Default annotations implementation.
	/// </summary>
	public class DefaultAnnotations : Annotations
	{
		private readonly IDictionary<string, object> annots = new Dictionary<string, object>();

		/// <seealso cref="de.unika.ipd.grgen.util.Annotations.containsKey(java.lang.String) "/>
		public virtual bool ContainsKey(string key)
		{
			return annots.ContainsKey(key);
		}

		/// <seealso cref="de.unika.ipd.grgen.util.Annotations.get(java.lang.String) "/>
		public virtual object Get(string key)
		{
			object res;
			annots.TryGetValue(key, out res);
			return res;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.Annotations.isBoolean(java.lang.String) "/>
		public virtual bool IsBoolean(string key)
		{
			return ContainsKey(key) && Get(key) is bool?;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.Annotations.isInteger(java.lang.String) "/>
		public virtual bool IsInteger(string key)
		{
			return ContainsKey(key) && Get(key) is int?;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.Annotations.isString(java.lang.String) "/>
		public virtual bool IsString(string key)
		{
			return ContainsKey(key) && Get(key) is string;
		}

		public virtual bool IsFlagSet(string key)
		{
			if(!ContainsKey(key))
				return false;
			object val = Get(key);
			return val is bool? && ((bool?)val).Value;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.Annotations.put(java.lang.String, java.lang.Object) "/>
		public virtual void Put(string key, object value)
		{
			annots[key] = value;
		}

		public virtual ICollection<string> KeySet()
		{
			return annots.Keys;
		}
	}

}
