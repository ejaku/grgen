package de.unika.ipd.grgen.ast.decl.executable;

import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.FunctionSignature;

public abstract class FunctionOrOperatorDeclBaseNode extends DeclNode implements FunctionSignature
{
	/** Result type of the function. */
	public TypeNode resultType;

	/** Parameter types. */
	protected Vector<TypeNode> parameterTypes;

	
	public FunctionOrOperatorDeclBaseNode(IdentNode ident, BaseNode type)
	{
		super(ident, type);
	}
	
	@Override
	public TypeNode getResultType()
	{
		assert isResolved();
		return resultType;
	}

	@Override
	public Vector<TypeNode> getParameterTypes()
	{
		assert isResolved();
		return parameterTypes;
	}
	
	@Override
	public int getDistance(Vector<TypeNode> argumentTypes)
	{
		if(argumentTypes.size() != parameterTypes.size())
			return Integer.MAX_VALUE;

		int distance = 0;
		for(int i = 0; i < parameterTypes.size(); i++) {
			debug.report(NOTE, "" + i + ": arg type: " + argumentTypes.get(i) + ", operand type: " + parameterTypes.get(i));

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
