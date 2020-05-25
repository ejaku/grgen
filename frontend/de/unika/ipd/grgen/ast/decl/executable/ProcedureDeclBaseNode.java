package de.unika.ipd.grgen.ast.decl.executable;

import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;

public abstract class ProcedureDeclBaseNode extends DeclNode
{
	protected CollectNode<BaseNode> retsUnresolved;
	public CollectNode<TypeNode> returnTypes;

	public ProcedureDeclBaseNode(IdentNode n, BaseNode t)
	{
		super(n, t);
	}

	private static final CollectResolver<TypeNode> retTypeResolver =
			new CollectResolver<TypeNode>(new DeclarationTypeResolver<TypeNode>(TypeNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		returnTypes = retTypeResolver.resolve(retsUnresolved, this);
		return returnTypes != null;
	}

	public abstract Vector<TypeNode> getParameterTypes();

	public Vector<TypeNode> getReturnTypes()
	{
		assert isResolved();

		Vector<TypeNode> types = new Vector<TypeNode>();
		for(TypeNode type : returnTypes.getChildren()) {
			types.add(type);
		}

		return types;
	}
}