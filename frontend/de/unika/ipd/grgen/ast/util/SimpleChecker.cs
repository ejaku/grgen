/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ast.util
{
using System;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using ErrorReporter = de.unika.ipd.grgen.util.report.ErrorReporter;

/// <summary>
/// A checker that checks if the AST node is an instance of one of the specified types.
/// </summary>
public class SimpleChecker : Checker
{
	/// <summary>
	/// The types the node is to be checked against. </summary>
	private Type[] validTypes;

	/// <summary>
	/// Create checker with one type to check the AST node against </summary>
	public SimpleChecker(Type validType)
	{
		this.validTypes = new Type[] { validType };
	}

	/// <summary>
	/// Create checker with the types to check the AST node against </summary>
	public SimpleChecker(Type[] validTypes)
	{
		this.validTypes = validTypes;
	}

	/// <summary>
	/// Just check whether the node is an instance of one of the valid types </summary>
	/// <seealso cref="de.unika.ipd.grgen.ast.check.Checker.check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter)"/>
	public virtual bool Check(BaseNode bn, ErrorReporter reporter)
	{
		bool res = false;

		// If the declaration's type is an instance of the desired class
		// everything's fine, else report errors

		for(int i = 0; i < validTypes.Length; i++)
		{
			if(validTypes[i].IsInstanceOfType(bn))
			{
				res = true;
				break;
			}
		}

		if(!res)
		{
			if(validTypes.Length == 1)
				bn.ReportError("AST node " + bn.Name + " must be an instance of type " + ShortClassName(validTypes[0]));
			else
				bn.ReportError("AST node " + bn.Name + " - Unknown type");
		}

		return res;
	}

	/// <summary>
	/// Strip the package name from the class name. </summary>
	/// <param name="cls"> The class. </param>
	/// <returns> stripped class name. </returns>
	protected internal static string ShortClassName(Type cls)
	{
// JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
		string s = cls.FullName;
		return s.Substring(s.LastIndexOf('.') + 1);
	}
}

}
