/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ast.decl.executable;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.TestTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;

/**
 * AST node class representing tests
 */
public class TestDeclNode extends ActionDeclNode
{
	static {
		setName(TestDeclNode.class, "test declaration");
	}

	/** Type for this declaration. */
	private TestTypeNode type;
	private static final TypeNode testType = new TestTypeNode();


	public TestDeclNode(IdentNode id, PatternGraphLhsNode pattern,
			CollectNode<IdentNode> implementedMatchTypes, CollectNode<BaseNode> rets)
	{
		super(id, testType, pattern, implementedMatchTypes, rets);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(getValidVersion(returnFormalParametersUnresolved, returnFormalParameters));
		children.add(pattern);
		children.add(getValidVersion(implementedMatchTypesUnresolved, implementedMatchTypes));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("ret");
		childrenNames.add("pattern");
		childrenNames.add("implementedMatchTypes");
		return childrenNames;
	}

	private static final DeclarationTypeResolver<TestTypeNode> typeResolver =
			new DeclarationTypeResolver<TestTypeNode>(TestTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean matchAndReturnTypesAreOk = super.resolveLocal();

		type = typeResolver.resolve(typeUnresolved, this);
		
		return matchAndReturnTypesAreOk
				& type != null;
	}

	@Override
	protected boolean checkLocal()
	{
		boolean leftHandGraphsOk = super.checkLocal();

		boolean noRewriteParts = sameNumberOfRewriteParts(null, "test");

		return leftHandGraphsOk
				& noRewriteParts
				& checkReturns(pattern.returns);
	}

	public boolean checkControlFlow()
	{
		return true;
	}

	@Override
	public TypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}

	public static String getKindStr()
	{
		return "test";
	}

	@Override
	protected IR constructIR()
	{
		// return if the pattern graph already constructed the IR object
		// that may happen in recursive patterns (and other usages/references)
		if(isIRAlreadySet()) {
			return getIR();
		}

		Rule testRule = new Rule(getIdentNode().getIdent());

		// mark this node as already visited
		setIR(testRule);

		PatternGraphLhs left = pattern.getPatternGraph();
		for(DeclNode varCand : pattern.getParamDecls()) {
			if(!(varCand instanceof VarDeclNode))
				continue;
			VarDeclNode var = (VarDeclNode)varCand;
			left.addVariable(var.checkIR(Variable.class));
		}

		testRule.initialize(left, null);

		for(DefinedMatchTypeNode implementedMatchClassNode : implementedMatchTypes.getChildren()) {
			DefinedMatchType implementedMatchClass = implementedMatchClassNode.checkIR(DefinedMatchType.class);
			testRule.addImplementedMatchClass(implementedMatchClass);
		}

		constructImplicitNegs(left);
		constructIRaux(testRule, pattern.returns);

		return testRule;
	}
}
