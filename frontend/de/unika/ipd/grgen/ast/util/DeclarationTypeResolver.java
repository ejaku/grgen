package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.util.Util;

/**
 * A resolver, that resolves an identifier into it's AST type node.
 */
public class DeclarationTypeResolver<T extends BaseNode>
{
	private Class<T> cls;
	
	/**
 	 * Make a new type declaration resolver.
 	 * 
	 * @param cls A class, the resolved node must be an instance of.
	 */
	public DeclarationTypeResolver(Class<T> cls) {
		this.cls = cls;
	}

	/**
	 * Resolves n to node of type R, via declaration type if n is an identifier, via simple cast otherwise 
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
	 * Resolves n to node of type R, via declaration type
	 * returns null if n's declaration can't be cast to R.
	 */
	public T resolve(IdentNode n) {
		BaseNode resolved = n.getDecl().getDeclType();
		if(cls.isInstance(resolved)) {
			return (T) resolved;
		}
		n.reportError("\"" + n + "\" is a " + resolved.getUseString() +
				" but a " + Util.getStr(cls, BaseNode.class, "getUseStr") + " is expected");
		return null;
	}
}
