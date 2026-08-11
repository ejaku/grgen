/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.invocation
{
	using de.unika.ipd.grgen.ast;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using ProcedureDeclBaseNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclBaseNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public abstract class ProcedureInvocationBaseNode : ProcedureOrBuiltinProcedureInvocationBaseNode
	{
		static ProcedureInvocationBaseNode()
		{
			SetClassName(typeof(ProcedureInvocationBaseNode), "procedure invocation base");
		}

		protected internal CollectNode<ExprNode> arguments;
		protected internal int context;

		protected internal ProcedureInvocationBaseNode(Coords coords, CollectNode<ExprNode> arguments, int context)
			: base(coords)
		{
			this.arguments = BecomeParent(arguments);
			this.context = context;
		}

		/// <summary>
		/// Check whether the usage adheres to the signature of the declaration </summary>
		protected internal virtual bool CheckSignatureAdhered(ProcedureDeclBaseNode pb, IdentNode unresolved, bool isMethod)
		{
			string procedureName = pb.ident.ToString();

			// check if the number of parameters are correct
			int expected = pb.ParameterTypes.Count;
			int actual = arguments.ChildrenExact.Count;
			if(expected != actual)
			{
				unresolved.ReportError("The procedure " + (isMethod ? "method " : "") + procedureName
						+ " expects " + expected + " arguments (given are " + actual + " arguments).");
				return false;
			}

			// check if the types of the parameters are correct
			bool res = true;
			for(int i = 0; i < arguments.Size(); ++i)
			{
				ExprNode actualParameter = arguments.Get(i);
				TypeNode actualParameterType = actualParameter.Type;
				TypeNode formalParameterType = pb.ParameterTypes[i];

				if(!actualParameterType.IsCompatibleTo(formalParameterType))
				{
					res = false;
					unresolved.ReportError("Cannot convert " + (i + 1) + ". argument"
							+ " from " + actualParameterType.TypeName
							+ " to the expected " + formalParameterType.TypeName
							+ " (when calling procedure " + (isMethod ? "method " : "") + pb.ToStringWithDeclarationCoords() + ")"
							+ actualParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
							+ formalParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
							+ ".");
				}
			}

			return res;
		}
	}

}
