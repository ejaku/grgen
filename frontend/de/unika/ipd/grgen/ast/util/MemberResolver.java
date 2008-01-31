package de.unika.ipd.grgen.ast.util;

import java.util.Map;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.InvalidDeclNode;
import de.unika.ipd.grgen.util.Util;

/**
 * A resolver, that resolves a declaration node from an identifier (used in a member init).
 */
public class MemberResolver<T extends BaseNode> extends Resolver<T>
{
	private Class<T> cls;
	
	/**
 	 * Make a new member resolver.
 	 * 
	 * @param cls A class, the resolved node must be an instance of.
	 */
	public MemberResolver(Class<T> cls) {
		this.cls = cls;
	}

	/**
	 * Resolves n to node of type T, via member init if n is an identifier, via simple cast otherwise 
	 * returns null if n's declaration or n can't be cast to R.
	 */
	public T resolve(BaseNode n, BaseNode parent) {
		if(n instanceof IdentNode) {
			T resolved = resolve((IdentNode)n);
			parent.becomeParent(resolved);
			return resolved;
		} 
		if(cls.isInstance(n)) {
			return (T) n;
		}
		n.reportError("\"" + n + "\" is a " + n.getUseString() +
				" but a " + Util.getStr(cls, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}

	/** 
	 * Resolves n to node of type R, via member init
	 * returns null if n's declaration can't be cast to R.
	 */
	public T resolve(IdentNode n) {
		DeclNode res = n.getDecl();

		if(!(res instanceof InvalidDeclNode)) {
			if (cls.isInstance(res)) {
				return (T) res;
			}
			n.reportError("\"" + n + "\" is a " + res.getUseString() +
					" but a " + Util.getStr(cls, BaseNode.class, "getUseStr") + " is expected");
		}

		InheritanceTypeNode typeNode = (InheritanceTypeNode)n.getScope().getIdentNode().getDecl().getDeclType();

		Map<String, DeclNode> allMembers = typeNode.getAllMembers();

		res = allMembers.get(n.toString());

		if(res==null) {
			n.reportError("Undefined member " + n.toString() + " of "+ typeNode.getDecl().getIdentNode());
			return null;
		}
		
		if (cls.isInstance(res)) {
			return (T) res;
		}
		n.reportError("\"" + n + "\" is a " + res.getUseString() +
				" but a " + Util.getStr(cls, BaseNode.class, "getUseStr") + " is expected");

		return null;
	}
}
