package de.unika.ipd.grgen.ast.decl.executable;

import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.ProcedureSignature;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;

public abstract class ProcedureDeclBaseNode extends DeclNode implements ProcedureSignature
{
	protected CollectNode<BaseNode> resultsUnresolved;
	public CollectNode<TypeNode> resultTypesCollectNode;

	/** Result types. */
	public Vector<TypeNode> resultTypes;

	/** Parameter types. */
	protected Vector<TypeNode> parameterTypes;

	
	public ProcedureDeclBaseNode(IdentNode ident, BaseNode type)
	{
		super(ident, type);
	}

	private static final CollectResolver<TypeNode> resultTypeResolver =
			new CollectResolver<TypeNode>(new DeclarationTypeResolver<TypeNode>(TypeNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		resultTypesCollectNode = resultTypeResolver.resolve(resultsUnresolved, this);

		resultTypes = resultTypesCollectNode.getChildrenAsVector();
		
		return resultTypesCollectNode != null;
	}

	@Override
	public Vector<TypeNode> getParameterTypes()
	{
		assert isResolved();
		return parameterTypes;
	}

	@Override
	public Vector<TypeNode> getResultTypes()
	{
		assert isResolved();
		return resultTypes;
	}
	
	@Override
	public int getDistance(Vector<TypeNode> argumentTypes)
	{
		if(argumentTypes.size() == parameterTypes.size())
			return Integer.MAX_VALUE;

		int distance = 0;
		for(int i = 0; i < parameterTypes.size(); i++) {
			debug.report(NOTE, "" + i + ": arg type: " + argumentTypes.get(i) + ", op type: " + parameterTypes.get(i));

			boolean equal = argumentTypes.get(i).isEqual(parameterTypes.get(i));
			boolean compatible = argumentTypes.get(i).isCompatibleTo(parameterTypes.get(i));
			debug.report(NOTE, "equal: " + equal + ", compatible: " + compatible);

			int compatibilityDistance = argumentTypes.get(i).compatibilityDistance(parameterTypes.get(i));

			if(compatibilityDistance == Integer.MAX_VALUE)
				return Integer.MAX_VALUE;

			distance += compatibilityDistance;
		}

		return distance;
	}
}
