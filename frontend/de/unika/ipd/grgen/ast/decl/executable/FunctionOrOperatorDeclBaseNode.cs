namespace de.unika.ipd.grgen.ast.decl.executable
{

using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using FunctionSignature = de.unika.ipd.grgen.ast.type.executable.FunctionSignature;

public abstract class FunctionOrOperatorDeclBaseNode : DeclNode, FunctionSignature
{
	/// <summary>
	/// Result type of the function. </summary>
	public TypeNode resultType;

	/// <summary>
	/// Parameter types. </summary>
	protected internal IList<TypeNode> parameterTypes;


	public FunctionOrOperatorDeclBaseNode(IdentNode ident, BaseNode type)
		: base(ident, type)
	{
	}

	public virtual TypeNode ResultType
	{
		get
		{
		Debug.Assert(IsResolved());
		return resultType;
		}
	}

	public virtual IList<TypeNode> ParameterTypes
	{
		get
		{
		Debug.Assert(IsResolved());
		return parameterTypes;
		}
	}

	public virtual int GetDistance(IList<TypeNode> argumentTypes)
	{
		if(argumentTypes.Count != parameterTypes.Count)
			return int.MaxValue;

		int distance = 0;
		for(int i = 0; i < parameterTypes.Count; i++)
		{
			debug.Report(NOTE, "" + i + ": arg type: " + argumentTypes[i] + ", operand type: " + parameterTypes[i]);

			bool equal = argumentTypes[i].IsEqual(parameterTypes[i]);
			bool compatible = argumentTypes[i].IsCompatibleTo(parameterTypes[i]);
			debug.Report(NOTE, "equal: " + equal + ", compatible: " + compatible);

			int compatibilityDistance = argumentTypes[i].CompatibilityDistance(parameterTypes[i]);

			if(compatibilityDistance == int.MaxValue)
				return int.MaxValue;

			distance += compatibilityDistance;
		}

		return distance;
	}
}

}
