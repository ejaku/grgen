/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.Entity;

/**
 * A operator node for the identifier qualification.
 * This node treats expressions like:
 * a.b.c.d
 */
public class QualIdentNode extends BaseNode implements DeclaredCharacter
{
	
	static
	{
		setName(QualIdentNode.class, "Qual");
	}
	
	/** Index of the owner node. */
	private static final int OWNER = 0;
	
	/** Index of the member node. */
	private static final int MEMBER = 1;
	
	private static final String[] childrenNames =
	{
		"owner", "member"
	};
	
	private static final Resolver ownerResolver =
		new DeclResolver(DeclNode.class);
	
	private static final Resolver declResolver =
		new DeclResolver(DeclNode.class);
	
	/**
	 * Make a new identifier qualify node.
	 * @param coords The coordinates.
	 */
	public QualIdentNode(Coords coords, BaseNode owner, BaseNode member)
	{
		super(coords);
		setChildrenNames(childrenNames);
		addChild(owner);
		addChild(member);
		addResolver(OWNER, declResolver);
		// addResolver(MEMBER, identExprResolver);
	}
	
	/**
	 * This AST node implies an other way of name resolution.
	 * First of all, the left hand side (lhs) has to be resolved. It must be
	 * a declaration and its type must be an instance of {@link ScopeOwner},
	 * since qualification can only be done, if the lhs owns a scope.
	 *
	 * Then the right side (rhs) is tought to search the declarations
	 * of its identifiers in the scope owned by the lhs. This is done
	 * via {@link ExprNode#fixupDeclaration(ScopeOwner)}.
	 *
	 * Then, the rhs contains the rhs' ident nodes contains the
	 * right declarations and can be resolved either.
	 * @see de.unika.ipd.grgen.ast.BaseNode#resolve()
	 */
	protected boolean resolve()
	{
		boolean res = false;
		IdentNode member = (IdentNode) getChild(MEMBER);
		
		ownerResolver.resolve(this, OWNER);
		BaseNode owner = getChild(OWNER);
		res = owner.getResolve();
		
		if (owner instanceof NodeDeclNode || owner instanceof EdgeDeclNode)
		{
			TypeNode ownerType = (TypeNode) ((DeclNode) owner).getDeclType();
			
			if(ownerType instanceof ScopeOwner)
			{
				ScopeOwner o = (ScopeOwner) ownerType;
				o.fixupDefinition(member);
				declResolver.resolve(this, MEMBER);
				res = getChild(MEMBER).getResolve();
			}
			else
			{
				reportError("Left hand side of . does not own a scope");
				res = false;
			}
		}
		else
		{
			reportError("Left hand side of . is neither an Edge nor a Node.");
			res = false;
		}
		
		
		setResolved(res);
		return res;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check()
	{
		return checkChild(OWNER, DeclNode.class)
			&& checkChild(MEMBER, MemberDeclNode.class);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.DeclaredCharacter#getDecl()
	 */
	public DeclNode getDecl()
	{
		assertResolved();
		return (DeclNode) getChild(MEMBER);
	}
	
	protected IR constructIR()
	{
		Entity owner = (Entity)getChild(OWNER).checkIR(Entity.class);
		Entity member = (Entity)getChild(MEMBER).checkIR(Entity.class);
		
		return new Qualification(owner, member);
	}
}
