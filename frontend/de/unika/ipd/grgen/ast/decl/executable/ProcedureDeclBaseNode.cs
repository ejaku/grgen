namespace de.unika.ipd.grgen.ast.decl.executable
{

using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using ProcedureSignature = de.unika.ipd.grgen.ast.type.executable.ProcedureSignature;
using de.unika.ipd.grgen.ast.util;

public abstract class ProcedureDeclBaseNode : DeclNode, ProcedureSignature
{
	protected internal CollectNode<BaseNode> resultsUnresolved;
	public CollectNode<TypeNode> resultTypesCollectNode;

	/// <summary>
	/// Result types. </summary>
	public IList<TypeNode> resultTypes;

	/// <summary>
	/// Parameter types. </summary>
	protected internal IList<TypeNode> parameterTypes;


	public ProcedureDeclBaseNode(IdentNode ident, BaseNode type) : base(ident, type)
	{
	}

	private static readonly CollectResolver<TypeNode> resultTypeResolver =
			new CollectResolver<TypeNode>(new DeclarationTypeResolver<TypeNode>(typeof(TypeNode)));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		resultTypesCollectNode = resultTypeResolver.Resolve(resultsUnresolved, this);

		if(resultTypesCollectNode != null)
			resultTypes = resultTypesCollectNode.ChildrenAsList;

		return resultTypes != null;
	}

	public virtual IList<TypeNode> ParameterTypes
	{
		get
		{
		Debug.Assert(IsResolved());
		return parameterTypes;
		}
	}

	public virtual IList<TypeNode> ResultTypes
	{
		get
		{
		Debug.Assert(IsResolved());
		return resultTypes;
		}
	}

	public virtual int GetDistance(IList<TypeNode> argumentTypes)
	{
		if(argumentTypes.Count == parameterTypes.Count)
			return int.MaxValue;

		int distance = 0;
		for(int i = 0; i < parameterTypes.Count; i++)
		{
			debug.Report(NOTE, "" + i + ": arg type: " + argumentTypes[i] + ", op type: " + parameterTypes[i]);

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
