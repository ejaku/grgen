/**
 * ModelNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.*;

import de.unika.ipd.grgen.ast.TypeDeclNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Model;
import java.util.Iterator;

public class ModelNode extends DeclNode {

	protected static final TypeNode modelType = new TypeNode() { };
	
	static {
		setName(ModelNode.class, "model declaration");
		setName(modelType.getClass(), "model type");
	}
	
	/** Index of the decl collect node in the children. */
	private static final int DECLS = 2;
	
	/** Names of the children */
	private static String[] childrenNames = {
		"ident", "type", "decls"
	};
	
	private static final Checker checker =
		new CollectChecker(new SimpleChecker(TypeDeclNode.class));
	
	private static final Resolver declResolver =
		new CollectResolver(new DeclResolver(TypeDeclNode.class));
	
	public ModelNode(IdentNode id) {
		super(id, modelType);
		setChildrenNames(childrenNames);
		addResolver(DECLS, declResolver);
	}
	
	/**
	 * The main node has an ident node and a collect node with
	 * - group declarations
	 * - edge class decls
	 * - node class decls
	 * as child.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(DECLS, checker);
	}
	
	/**
	 * Get the IR model node for this ast node.
	 * @return The model for this ast node.
	 */
	public Model getModel() {
		return (Model) checkIR(Model.class);
	}
	
	/**
	 * Construct the ir object for this ast node.
	 * For a main node, this is a unit.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		Ident id = (Ident) getChild(IDENT).checkIR(Ident.class);
		Model res = new Model(id);
		Iterator children = getChild(DECLS).getChildren();
		while(children.hasNext()) {
			TypeDeclNode typeDecl = (TypeDeclNode) children.next();
			res.addType(((TypeNode) typeDecl.getDeclType()).getType());
		}
		return res;
	}

}

