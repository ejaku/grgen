/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.executable
{

	using System.Collections.Generic;

	using Ident = de.unika.ipd.grgen.ir.Ident;
	using Identifiable = de.unika.ipd.grgen.ir.Identifiable;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	/// <summary>
	/// A procedure base.
	/// </summary>
	public abstract class ProcedureBase : Identifiable
	{
		/// <summary>
		/// A list of the return types </summary>
		protected internal List<Type> returnTypes = new List<Type>();

		/// <param name="name"> The name of the procedure. </param>
		/// <param name="ident"> The identifier that identifies this object. </param>
		public ProcedureBase(string name, Ident ident)
			: base(name, ident)
		{
		}

		/// <summary>
		/// Add a return type to the procedure. </summary>
		public virtual void AddReturnType(Type returnType)
		{
			returnTypes.Add(returnType);
		}

		/// <summary>
		/// Get all return types of this procedure. </summary>
		public virtual IList<Type> ReturnTypes
		{
			get
			{
				return returnTypes.AsReadOnly();
			}
		}

		/// <summary>
		/// Get all parameter types of this procedure. </summary>
		public abstract IList<Type> ParameterTypes {get;}
	}

}
