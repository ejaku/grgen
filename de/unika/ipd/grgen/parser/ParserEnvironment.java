/**
 * ParserEnvironment.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.parser;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.Collection;
import java.util.LinkedList;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.BasicTypeNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.ConstNode;
import de.unika.ipd.grgen.ast.EdgeTypeNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.IntConstNode;
import de.unika.ipd.grgen.ast.ModelNode;
import de.unika.ipd.grgen.ast.NodeTypeNode;
import de.unika.ipd.grgen.ast.TypeDeclNode;
import de.unika.ipd.grgen.ast.TypeNode;
import de.unika.ipd.grgen.util.Base;

public abstract class ParserEnvironment extends Base {

	public static final String MODEL_SUFFIX = ".gm";
	
	public static final Coords BUILTIN = new Coords(0, 0, "<builtin>");
	
	public static final int TYPES = 0;
	public static final int ENTITIES = 1;
	public static final int ACTIONS = 2;
	
	private final SymbolTable[] symTabs = new SymbolTable[] {
		new SymbolTable("types"),
		new SymbolTable("entities"),
		new SymbolTable("actions")
	};
	
	private final ConstNode one = new IntConstNode(Coords.getBuiltin(), 1);
	
	private final ConstNode zero = new IntConstNode(Coords.getBuiltin(), 0);

	private final Scope rootScope;
	
	private Scope currScope;
	
	private final IdentNode nodeRoot;
	
	private final IdentNode edgeRoot;
	
	private final Sys system;
	
	private final ModelNode stdModel;
	
	private final Collection builtins = new LinkedList();
	
	/**
	 * Make a new parser environment.
	 */
	public ParserEnvironment(Sys system) {
		this.system = system;
		
		// Make the root scope
		currScope = rootScope = new Scope(system.getErrorReporter());
		BaseNode.setCurrScope(currScope);

		// Add keywords to the symbol table
		for(int i = 0; i < symTabs.length; i++) {
			symTabs[i].enterKeyword("int");
			symTabs[i].enterKeyword("string");
			symTabs[i].enterKeyword("boolean");
		}

		stdModel = new ModelNode(predefine(ENTITIES, "Std"));
		CollectNode stdModelChilds = new CollectNode();
		stdModel.addChild(stdModelChilds);
		
		// The node type root
		nodeRoot = predefineType("Node", new NodeTypeNode(new CollectNode(),
																											new CollectNode(), 0));
		
		// The edge type root
		edgeRoot = predefineType("Edge", new EdgeTypeNode(new CollectNode(),
																											new CollectNode(),
																											new CollectNode(), 0));
		
		stdModelChilds.addChild(nodeRoot);
		stdModelChilds.addChild(edgeRoot);
		
		stdModelChilds.addChild(predefineType("int", BasicTypeNode.intType));
		stdModelChilds.addChild(predefineType("string", BasicTypeNode.stringType));
		stdModelChilds.addChild(predefineType("boolean", BasicTypeNode.booleanType));
	}
	
	public ModelNode getStdModel() {
		return stdModel;
	}
	
	public File findModel(String modelName) {
		File res = null;
		File[] modelPaths = system.getModelPaths();
		String modelFile = modelName + MODEL_SUFFIX;
		
		
		for(int i = 0; i < modelPaths.length; i++) {
			File curr = new File(modelPaths[i], modelFile);
			debug.report(NOTE, "trying: " + curr);
			if(curr.exists()) {
				res = curr;
				break;
			}
		}
		return res;
	}

	public FileInputStream openModel(String modelName) {
		File modelFile = findModel(modelName);
		FileInputStream res = null;
		
		try {
			res = new FileInputStream(modelFile);
		} catch(FileNotFoundException e) {
			system.getErrorReporter().error("Cannot load graph model: " + modelName);
			System.exit(1);
		}
		
		return res;
	}
	
	/**
	 * Predefine an identifier.
	 * @param symTab The symbol table to enter the identifier in.
	 * @param text The string of the identifier.
	 * @return An AST identifier node for this identifier.
	 */
	private IdentNode predefine(int symTab, String text) {
		return new IdentNode(define(symTab, text, BaseNode.BUILTIN));
	}

	/**
	 * Predefine a type.
	 * This method creates the type declaration of a given type.
	 * @param text The name of the type.
	 * @param type The AST type node.
	 * @return An AST identifier node for this type.
	 */
	private IdentNode predefineType(String text, TypeNode type) {
		IdentNode id = predefine(TYPES, text);
		id.setDecl(new TypeDeclNode(id, type));
		return id;
	}
	
	public Scope getCurrScope() {
		return currScope;
	}
	
	public void pushScope(IdentNode ident) {
		pushScope(ident.toString());
	}
	
	public void pushScope(String name) {
		currScope = currScope.newScope(name);
		BaseNode.setCurrScope(currScope);
	}
	
	public void popScope() {
		if(!currScope.isRoot())
			currScope = currScope.leaveScope();
		BaseNode.setCurrScope(currScope);
	}
	
	public Symbol.Definition define(int symTab, String text, Coords coords) {
		assert symTab >= 0 && symTab < symTabs.length : "Illegal symbol table index";
		Symbol sym = symTabs[symTab].get(text);
		return currScope.define(sym, coords);
	}
	
	public IdentNode defineAnonymousEntity(String text, Coords coords) {
		Symbol.Definition def = currScope.defineAnonymous(text, symTabs[ENTITIES], coords);
		return new IdentNode(def);
	}
	
	public Symbol.Occurrence occurs(int symTab, String text, Coords coords) {
		assert symTab >= 0 && symTab < symTabs.length : "Illegal symbol table index";
		Symbol sym = symTabs[symTab].get(text);
		return currScope.occurs(sym, coords);
	}
	
	/**
	 * Get the node root identifier.
	 * @return The node root type identifier.
	 */
	public IdentNode getNodeRoot() {
		return nodeRoot;
	}
	
	/**
	 * Get the edge root identifier.
	 * @return The edge root type identifier.
	 */
	public IdentNode getEdgeRoot() {
		return edgeRoot;
	}
	
	public BaseNode getOne() {
		return one;
	}
	
	public BaseNode getZero() {
		return zero;
	}
	
	public Sys getSystem() {
		return system;
	}
	
	/**
	 * Get an initializer for an AST node.
	 * This defaults to the error node.
	 * @return An initialization AST node.
	 */
	public BaseNode initNode() {
		return BaseNode.getErrorNode();
	}

	/**
	 * Get an initializer for an identifier AST node.
	 * This defaults to the invalid identifier.
	 * @return An initialization AST identifier node.
	 */
	public IdentNode getDummyIdent() {
		return IdentNode.getInvalid();
	}
	
	public abstract BaseNode parse(File inputFile);
	
	public abstract boolean hadError();
}

