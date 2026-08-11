/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.type.executable
{

using System.Collections.Generic;

using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;

/// <summary>
/// Function abstraction.
/// </summary>
public interface FunctionSignature
{
	/// <summary>
	/// Get the result(/return) type of this function signature. </summary>
	/// <returns> The result(/return) type. </returns>
	TypeNode ResultType {get;}

	/// <summary>
	/// Get the parameter(/operand) types of this function signature. </summary>
	/// <returns> The parameter(/operand) types. </returns>
	IList<TypeNode> ParameterTypes {get;}

	/// <summary>
	/// Get the number of implicit type casts needed for calling this
	/// function signature with the given arguments(/operands). </summary>
	/// <param name="argumentTypes"> The types of the arguments(/operands) </param>
	/// <returns> The number of implicit type casts needed to apply the arguments(/operands)
	/// to this function signature. <code>Integer.MAX_VALUE</code> is returned,
	/// if the arguments(/operands) cannot be applied to this functions signature. </returns>
	int GetDistance(IList<TypeNode> argumentTypes);
}

}
