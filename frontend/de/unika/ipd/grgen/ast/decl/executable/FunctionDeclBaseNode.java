package de.unika.ipd.grgen.ast.decl.executable;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;

public abstract class FunctionDeclBaseNode extends FunctionOrOperatorDeclBaseNode
{
	protected BaseNode resultUnresolved;

	
	public FunctionDeclBaseNode(IdentNode ident, BaseNode type)
	{
		super(ident, type);
	}

	private static final Resolver<TypeNode> resultTypeResolver =
			new DeclarationTypeResolver<TypeNode>(TypeNode.class);
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		resultType = resultTypeResolver.resolve(resultUnresolved, this);
		return resultType != null;
	}
}
