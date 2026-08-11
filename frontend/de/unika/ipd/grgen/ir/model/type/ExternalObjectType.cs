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

using System.Collections.Generic;

using de.unika.ipd.grgen.ir;
using ExternalFunctionMethod = de.unika.ipd.grgen.ir.executable.ExternalFunctionMethod;
using ExternalProcedureMethod = de.unika.ipd.grgen.ir.executable.ExternalProcedureMethod;

/// <summary>
/// IR class that represents external object types.
/// </summary>
public class ExternalObjectType : InheritanceType
{
	private List<ExternalFunctionMethod> externalFunctionMethods = new List<ExternalFunctionMethod>();
	private List<ExternalProcedureMethod> externalProcedureMethods = new List<ExternalProcedureMethod>();

	private IDictionary<string, ExternalFunctionMethod> allExternalFunctionMethods = null;
	private IDictionary<string, ExternalProcedureMethod> allExternalProcedureMethods = null;

	/// <summary>
	/// Make a new external type. </summary>
	/// <param name="ident"> The identifier that declares this type. </param>
	public ExternalObjectType(Ident ident)
		: base("external object type", ident, 0, null)
	{
	}

	public virtual ICollection<ExternalFunctionMethod> ExternalFunctionMethods
	{
		get
		{
		return externalFunctionMethods.AsReadOnly();
		}
	}

	public virtual void AddExternalFunctionMethod(ExternalFunctionMethod method)
	{
		externalFunctionMethods.Add(method);
		method.Owner = this;
	}

	public virtual void AddExternalProcedureMethod(ExternalProcedureMethod method)
	{
		externalProcedureMethods.Add(method);
		method.Owner = this;
	}

	public virtual ICollection<ExternalProcedureMethod> ExternalProcedureMethods
	{
		get
		{
		return externalProcedureMethods.AsReadOnly();
		}
	}

	private void AddExternalFunctionMethods(ExternalObjectType type)
	{
		foreach(ExternalFunctionMethod fm in type.ExternalFunctionMethods)
		{
			string functionName = fm.Ident.ToString();
			allExternalFunctionMethods[functionName] = fm;
		}
	}

	private void AddExternalProcedureMethods(ExternalObjectType type)
	{
		foreach(ExternalProcedureMethod pm in type.ExternalProcedureMethods)
		{
			string procedureName = pm.Ident.ToString();
			allExternalProcedureMethods[procedureName] = pm;
		}
	}

	public virtual ICollection<ExternalFunctionMethod> AllExternalFunctionMethods
	{
		get
		{
		if(allExternalFunctionMethods == null)
		{
			allExternalFunctionMethods = new LinkedHashMap<string, ExternalFunctionMethod>();

			// add the members of the super types
			foreach(InheritanceType superType in AllSuperTypes)
				AddExternalFunctionMethods((ExternalObjectType)superType);

			// add members of the current type
			AddExternalFunctionMethods(this);
		}

		return allExternalFunctionMethods.Values;
		}
	}

	public virtual ICollection<ExternalProcedureMethod> AllExternalProcedureMethods
	{
		get
		{
		if(allExternalProcedureMethods == null)
		{
			allExternalProcedureMethods = new LinkedHashMap<string, ExternalProcedureMethod>();

			// add the members of the super types
			foreach(InheritanceType superType in AllSuperTypes)
				AddExternalProcedureMethods((ExternalObjectType)superType);

			// add members of the current type
			AddExternalProcedureMethods(this);
		}

		return allExternalProcedureMethods.Values;
		}
	}

	/// <summary>
	/// Return a classification of a type for the IR. </summary>
	public override TypeClass Classify()
	{
		return TypeClass.IS_EXTERNAL_CLASS_OBJECT;
	}
}

}
