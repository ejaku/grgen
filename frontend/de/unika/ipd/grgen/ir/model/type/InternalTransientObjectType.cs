/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.model.type
{
	using Ident = de.unika.ipd.grgen.ir.Ident;

	/// <summary>
	/// IR class that represents (internal non-node/edge) transient object types (i.e. classes).
	/// </summary>
	public class InternalTransientObjectType : BaseInternalObjectType
	{
		/// <summary>
		/// Make a new (internal) transient object type. </summary>
		/// <param name="ident"> The identifier that declares this type. </param>
		/// <param name="modifiers"> The modifiers for this type. </param>
		public InternalTransientObjectType(Ident ident, int modifiers)
			: base("internal transient object type", ident, modifiers)
		{
		}

		/// <seealso cref="de.unika.ipd.grgen.ir.type.Type.classify() "/>
		public override TypeClass Classify()
		{
			return TypeClass.IS_INTERNAL_TRANSIENT_CLASS_OBJECT;
		}
	}

}
