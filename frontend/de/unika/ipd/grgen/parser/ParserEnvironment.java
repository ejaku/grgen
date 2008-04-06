/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * ParserEnvironment.java
 *
 * @author Sebastian Hack
 * @version $Id$
 */

package de.unika.ipd.grgen.parser;

import java.io.File;
import java.util.Hashtable;

import antlr.TokenStreamException;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.ast.ArbitraryEdgeTypeNode;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.BasicTypeNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.ConnAssertNode;
import de.unika.ipd.grgen.ast.DirectedEdgeTypeNode;
import de.unika.ipd.grgen.ast.ExprNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.IntConstNode;
import de.unika.ipd.grgen.ast.ModelNode;
import de.unika.ipd.grgen.ast.NodeDeclNode;
import de.unika.ipd.grgen.ast.NodeTypeNode;
import de.unika.ipd.grgen.ast.TypeDeclNode;
import de.unika.ipd.grgen.ast.TypeNode;
import de.unika.ipd.grgen.ast.UndirectedEdgeTypeNode;
import de.unika.ipd.grgen.util.Annotations;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.EmptyAnnotations;

public abstract class ParserEnvironment extends Base {
	public static final String MODEL_SUFFIX = ".gm";

	public static final int TYPES = 0;
	public static final int ENTITIES = 1;
	public static final int ACTIONS = ENTITIES; // actions are also entities to get exec working
	public static final int ALTERNATIVES = 2;
	public static final int REPLACES = 3;

	private final SymbolTable[] symTabs = new SymbolTable[] {
		new SymbolTable("types"),
		new SymbolTable("entities"), // actions are also entities
		new SymbolTable("alternatives"),
		new SymbolTable("replaces")
	};

	private final IntConstNode one = new IntConstNode(Coords.getBuiltin(), 1);

	private final IntConstNode zero = new IntConstNode(Coords.getBuiltin(), 0);

	// TODO use or remove it
	// private final Scope rootScope;

	private Scope currScope;

	private final IdentNode nodeRoot;

	private final IdentNode arbitraryEdgeRoot;
	private final IdentNode directedEdgeRoot;
	private final IdentNode undirectedEdgeRoot;

	private final Sys system;

	private final ModelNode stdModel;

	/** Known literals of the lexer */
	protected Hashtable literals;

	/**
	 * Make a new parser environment.
	 */
	public ParserEnvironment(Sys system) {
		this.system = system;

		// Make the root scope
		// currScope = rootScope = new Scope(system.getErrorReporter());
		currScope = new Scope(system.getErrorReporter());
		BaseNode.setCurrScope(currScope);

		// Add keywords to the symbol table
		for(int i = 0; i < symTabs.length; i++) {
			symTabs[i].enterKeyword("int");
			symTabs[i].enterKeyword("string");
			symTabs[i].enterKeyword("boolean");
			symTabs[i].enterKeyword("float");
			symTabs[i].enterKeyword("double");
			symTabs[i].enterKeyword("object");
		}

		CollectNode<IdentNode> stdModelChilds = new CollectNode<IdentNode>();
		stdModel = new ModelNode(predefine(ENTITIES, "Std"), stdModelChilds);

		// The node type root
		nodeRoot = predefineType("Node",
				new NodeTypeNode(new CollectNode<IdentNode>(), new CollectNode<BaseNode>(), 0, null));

		// The edge type roots

		arbitraryEdgeRoot = predefineType("AEdge",
				new ArbitraryEdgeTypeNode(new CollectNode<IdentNode>(), new CollectNode<ConnAssertNode>(), new CollectNode<BaseNode>(), InheritanceTypeNode.MOD_ABSTRACT, null));
		CollectNode<IdentNode> superTypes = new CollectNode<IdentNode>();
		superTypes.addChild(arbitraryEdgeRoot);

		directedEdgeRoot = predefineType("Edge",
				new DirectedEdgeTypeNode(superTypes, new CollectNode<ConnAssertNode>(), new CollectNode<BaseNode>(), 0, null));
		undirectedEdgeRoot = predefineType("UEdge",
				new UndirectedEdgeTypeNode(superTypes, new CollectNode<ConnAssertNode>(), new CollectNode<BaseNode>(), 0, null));

		stdModelChilds.addChild(nodeRoot);
		stdModelChilds.addChild(arbitraryEdgeRoot);
		stdModelChilds.addChild(directedEdgeRoot);
		stdModelChilds.addChild(undirectedEdgeRoot);

		stdModelChilds.addChild(predefineType("int", BasicTypeNode.intType));
		stdModelChilds.addChild(predefineType("string", BasicTypeNode.stringType));
		stdModelChilds.addChild(predefineType("boolean", BasicTypeNode.booleanType));
		stdModelChilds.addChild(predefineType("float", BasicTypeNode.floatType));
		stdModelChilds.addChild(predefineType("double", BasicTypeNode.doubleType));
		stdModelChilds.addChild(predefineType("object", BasicTypeNode.objectType));
	}

	public ModelNode getStdModel() {
		return stdModel;
	}

	public File findModel(String modelName) {
		File res = null;
		File[] modelPaths = system.getModelPaths();
		String modelFile = modelName + MODEL_SUFFIX;

		for(int i = 0; i < modelPaths.length; i++) {
			File curr;
			if(modelPaths[i].getPath().equals("."))
				curr = new File(modelFile);
			else
				curr = new File(modelPaths[i], modelFile);
			debug.report(NOTE, "trying: " + curr);
			if(curr.exists()) {
				res = curr;
				break;
			}
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
		currScope = currScope.newScope(ident);
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

	public boolean test(int symTab, String text) {
		assert symTab >= 0 && symTab < symTabs.length : "Illegal symbol table index";
		return symTabs[symTab].test(text);
	}

	/**
	 * Get the node root identifier.
	 * @return The node root type identifier.
	 */
	public IdentNode getNodeRoot() {
		return nodeRoot;
	}

	/**
	 * Get the directed edge root identifier.
	 * @return The directed edge root type identifier.
	 */
	public IdentNode getDirectedEdgeRoot() {
		return directedEdgeRoot;
	}

	/**
	 * Get the arbitrary edge root identifier.
	 * @return The arbitrary edge root type identifier.
	 */
	public IdentNode getArbitraryEdgeRoot() {
		return arbitraryEdgeRoot;
	}

	/**
	 * Get the undirected edge root identifier.
	 * @return The undirected edge root type identifier.
	 */
	public IdentNode getUndirectedEdgeRoot() {
		return undirectedEdgeRoot;
	}

	public IntConstNode getOne() {
		return one;
	}

	public IntConstNode getZero() {
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
	public ExprNode initExprNode() {
		return ExprNode.getInvalid();
	}

	public NodeDeclNode getDummyNodeDecl(int context)
	{
		return NodeDeclNode.getDummy(defineAnonymousEntity("dummy_node", new Coords()), this.getNodeRoot(), context);
	}

	/**
	 * Get an initializer for an identifier AST node.
	 * This defaults to the invalid identifier.
	 * @return An initialization AST identifier node.
	 */
	public IdentNode getDummyIdent() {
		return IdentNode.getInvalid();
	}

	public Annotations getEmptyAnnotations()
	{
		return EmptyAnnotations.get();
	}

	public Coords getInvalidCoords()
	{
		return Coords.getInvalid();
	}

	public abstract boolean isKeyword(String str);

	public abstract BaseNode parseActions(File inputFile);
	public abstract BaseNode parseModel(File inputFile);
	public abstract void pushFile(File inputFile) throws TokenStreamException;
	public abstract void popFile() throws TokenStreamException;
	public abstract String getFilename();

	public abstract boolean hadError();
}

