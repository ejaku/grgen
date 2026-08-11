/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ir
{

	public class Constructor : IR
	{
		private LinkedHashSet<ConstructorParam> parameters;

		public Constructor(LinkedHashSet<ConstructorParam> parameters)
			: base("constructor")
		{
			this.parameters = parameters;
		}

		public virtual LinkedHashSet<ConstructorParam> Parameters
		{
			get
			{
				return parameters;
			}
		}
	}

}
