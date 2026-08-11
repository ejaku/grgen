/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.invocation
{
using de.unika.ipd.grgen.ast;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using FunctionOrOperatorDeclBaseNode = de.unika.ipd.grgen.ast.decl.executable.FunctionOrOperatorDeclBaseNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using Coords = de.unika.ipd.grgen.parser.Coords;

public abstract class FunctionInvocationBaseNode : FunctionOrBuiltinFunctionInvocationBaseNode
{
	static FunctionInvocationBaseNode()
	{
		SetClassName(typeof(FunctionInvocationBaseNode), "function invocation base");
	}

	protected internal CollectNode<ExprNode> arguments;

	public FunctionInvocationBaseNode(Coords coords, CollectNode<ExprNode> arguments)
		: base(coords)
	{
		this.arguments = BecomeParent(arguments);
	}

	/// <summary>
	/// Check whether the usage adheres to the signature of the declaration </summary>
	protected internal virtual bool CheckSignatureAdhered(FunctionOrOperatorDeclBaseNode fb, IdentNode unresolved, bool isMethod)
	{
		// check if the number of parameters are correct
		int expected = fb.ParameterTypes.Count;
		int actual = arguments.ChildrenExact.Count;
		if(expected != actual)
		{
			unresolved.ReportError("The function " + (isMethod ? "method " : "") + fb.ToStringWithDeclarationCoords()
					+ " expects " + expected + " arguments (given are " + actual + " arguments).");
			return false;
		}

		// check if the types of the parameters are correct
		bool res = true;
		for(int i = 0; i < arguments.Size(); ++i)
		{
			ExprNode actualParameter = arguments.Get(i);
			TypeNode actualParameterType = actualParameter.Type;
			TypeNode formalParameterType = fb.ParameterTypes[i];

			if(!actualParameterType.IsCompatibleTo(formalParameterType))
			{
				res = false;
				string exprTypeName = actualParameterType.TypeName;
				string paramTypeName = formalParameterType.TypeName;
				unresolved.ReportError("Cannot convert " + (i + 1) + ". argument from " + exprTypeName
						+ " to the expected " + paramTypeName + " (when calling function " + (isMethod ? "method " : "") + fb.ToStringWithDeclarationCoords() + ")"
						+ actualParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
						+ formalParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
						+ ".");
			}
		}

		return res;
	}
}

}
