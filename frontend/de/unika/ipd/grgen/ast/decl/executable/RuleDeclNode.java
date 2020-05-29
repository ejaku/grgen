/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack, Daniel Grund
 */

package de.unika.ipd.grgen.ast.decl.executable;

import java.util.Collection;
import java.util.HashSet;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.EmitNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.PackageIdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.RhsDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.DeclExprNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.MemberAccessExprNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.pattern.GraphNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphNode;
import de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.RuleTypeNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.pattern.PatternGraph;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;

/**
 * AST node for a replacement rule.
 */
// TODO: a rule is not a test, factor the common stuff out into a base class
public class RuleDeclNode extends TestDeclNode
{
	static {
		setName(RuleDeclNode.class, "rule declaration");
	}

	public RhsDeclNode right;
	private RuleTypeNode type;

	/** Type for this declaration. */
	private static final TypeNode ruleType = new RuleTypeNode();

	/**
	 * Make a new rule.
	 * @param id The identifier of this rule.
	 * @param left The left hand side (The pattern to match).
	 * @param right The right hand side.
	 */
	public RuleDeclNode(IdentNode id, PatternGraphNode left, CollectNode<IdentNode> implementedMatchTypes,
			RhsDeclNode right, CollectNode<BaseNode> rets)
	{
		super(id, ruleType, left, implementedMatchTypes, rets);
		this.right = right;
		becomeParent(this.right);
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
		children.add(right);
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
		childrenNames.add("right");
		return childrenNames;
	}

	protected static final DeclarationTypeResolver<RuleTypeNode> typeResolver =
			new DeclarationTypeResolver<RuleTypeNode>(RuleTypeNode.class);
	private static final CollectResolver<DefinedMatchTypeNode> matchTypeResolver =
			new CollectResolver<DefinedMatchTypeNode>(
					new DeclarationTypeResolver<DefinedMatchTypeNode>(DefinedMatchTypeNode.class));
	private static final CollectResolver<TypeNode> retTypeResolver =
			new CollectResolver<TypeNode>(new DeclarationTypeResolver<TypeNode>(TypeNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		type = typeResolver.resolve(typeUnresolved, this);
		for(IdentNode mtid : implementedMatchTypesUnresolved.getChildren()) {
			if(!(mtid instanceof PackageIdentNode))
				fixupDefinition(mtid, mtid.getScope());
		}
		implementedMatchTypes = matchTypeResolver.resolve(implementedMatchTypesUnresolved, this);
		returnFormalParameters = retTypeResolver.resolve(returnFormalParametersUnresolved, this);

		boolean filtersOk = true;
		for(FilterAutoDeclNode filter : filters) {
			if(filter instanceof FilterAutoSuppliedDeclNode)
				filtersOk &= ((FilterAutoSuppliedDeclNode)filter).resolve();
			else //if(filter instanceof FilterAutoGeneratedNode)
				filtersOk &= ((FilterAutoGeneratedDeclNode)filter).resolve();
		}

		return type != null
				& returnFormalParameters != null
				& implementedMatchTypes != null
				& filtersOk;
	}

	public Set<DeclNode> getDeleted()
	{
		return right.getDeleted(pattern);
	}

	/**
	 * Check that only graph elements are returned, that are not deleted.
	 *
	 * The check also consider the case that a node is returned and homomorphic
	 * matching is allowed with a deleted node.
	 */
	private boolean checkReturnedElemsNotDeleted()
	{
		assert isResolved();

		boolean valid = true;
		Set<DeclNode> deleted = right.getDeleted(pattern);
		Collection<DeclNode> maybeDeleted = right.getMaybeDeleted(pattern);

		for(ExprNode expr : right.graph.returns.getChildren()) {
			valid &= checkReturnedElemNotDeleted(expr, deleted, maybeDeleted);
		}

		return valid;
	}

	private boolean checkReturnedElemNotDeleted(ExprNode expr, Set<DeclNode> deleted, Collection<DeclNode> maybeDeleted)
	{
		boolean valid = true;

		if(!(expr instanceof DeclExprNode))
			return valid;

		ConstraintDeclNode retElem = ((DeclExprNode)expr).getConstraintDeclNode();
		if(retElem == null)
			return valid;

		if(deleted.contains(retElem)) {
			valid = false;

			expr.reportError("The deleted " + retElem.getUseString()
					+ " \"" + retElem.ident + "\" must not be returned");
		} else if(maybeDeleted.contains(retElem)) {
			retElem.maybeDeleted = true;

			if(!retElem.getIdentNode().getAnnotations().isFlagSet("maybeDeleted")) {
				valid = false;

				String errorMessage = "Returning \"" + retElem.ident + "\" that may be deleted"
						+ ", possibly it's homomorphic with a deleted " + retElem.getUseString();
				errorMessage += " (use a [maybeDeleted] annotation if you think that this does not cause problems)";

				if(retElem instanceof EdgeDeclNode) {
					errorMessage += " or \"" + retElem.ident + "\" is a dangling " + retElem.getUseString()
							+ " and a deleted node exists";
				}
				expr.reportError(errorMessage);
			}
		}

		return valid;
	}

	/**
	 * Check that only graph elements are returned, that are not retyped.
	 *
	 * The check also consider the case that a node is returned and homomorphic
	 * matching is allowed with a retyped node.
	 */
	private boolean checkReturnedElemsNotRetyped()
	{
		assert isResolved();

		boolean valid = true;

		for(ExprNode expr : right.graph.returns.getChildren()) {
			if(!(expr instanceof DeclExprNode))
				continue;

			ConstraintDeclNode retElem = ((DeclExprNode)expr).getConstraintDeclNode();
			if(retElem == null)
				continue;

			if(retElem.getRetypedElement() != null) {
				valid = false;

				expr.reportError("The retyped " + retElem.getUseString()
						+ " \"" + retElem.ident + "\" must not be returned");
			}
		}

		return valid;
	}

	/**
	 * Check that every graph element is retyped to at most one type.
	 */
	private boolean checkElemsNotRetypedToDifferentTypes()
	{
		assert isResolved();

		boolean valid = true;

		for(Set<ConstraintDeclNode> homSet : pattern.getHoms()) {
			valid &= checkElemsInHomSetNotRetypedToDifferentTypes(homSet);
		}

		return valid;
	}

	private boolean checkElemsInHomSetNotRetypedToDifferentTypes(Set<ConstraintDeclNode> homSet)
	{
		boolean multipleRetypes = false;

		InheritanceTypeNode type = null;
		for(ConstraintDeclNode elem : homSet) {
			ConstraintDeclNode retypedElem = elem.getRetypedElement();

			if(retypedElem != null) {
				InheritanceTypeNode currentType = retypedElem.getDeclType();

				if(type != null && currentType != type) {
					multipleRetypes = true;
					break;
				}

				type = currentType;
			}
		}

		if(multipleRetypes) {
			for(ConstraintDeclNode elem : homSet) {
				ConstraintDeclNode retypedElem = elem.getRetypedElement();

				if(retypedElem != null) {
					retypedElem.reportError("The " + elem.getUseString() + " "
							+ elem + " must not retyped to different types");
				}
			}
		}

		return !multipleRetypes;
	}

	/**
	 * Check that only graph elements are retyped, that are not deleted.
	 */
	private boolean checkRetypedElemsNotDeleted()
	{
		assert isResolved();

		boolean valid = true;

		for(DeclNode decl : getDeleted()) {
			if(!(decl instanceof ConstraintDeclNode))
				continue;

			ConstraintDeclNode retElem = ((ConstraintDeclNode)decl);

			if(retElem.getRetypedElement() != null) {
				valid = false;

				retElem.reportError("The retyped " + retElem.getUseString()
						+ " \"" + retElem.ident + "\" must not be deleted");
			}
		}

		return valid;
	}

	private HashSet<ConstraintDeclNode> collectNeededElements(ExprNode expr)
	{
		HashSet<ConstraintDeclNode> neededElements = new HashSet<ConstraintDeclNode>();
		if(expr instanceof MemberAccessExprNode) // attribute access is decoupled via temporary variable, so deletion of element is ok
			return neededElements;

		for(BaseNode child : expr.getChildren()) {
			if(child instanceof ExprNode)
				neededElements.addAll(collectNeededElements((ExprNode)child));

			if(child instanceof DeclExprNode)
				neededElements.add(((DeclExprNode)child).getConstraintDeclNode());
			else if(child instanceof ConstraintDeclNode)
				neededElements.add((ConstraintDeclNode)child);
		}

		return neededElements;
	}

	/**
	 * Check that emit elems are not deleted.
	 * The check considers the case that parameters are deleted due to homomorphic matching.
	 */
	private boolean checkEmitElemsNotDeleted()
	{
		assert isResolved();

		boolean valid = true;
		Set<DeclNode> delete = right.getDeleted(pattern);
		Collection<DeclNode> maybeDeleted = right.getMaybeDeleted(pattern);

		for(BaseNode imperativeStmt : right.graph.imperativeStmts.getChildren()) {
			if(!(imperativeStmt instanceof EmitNode))
				continue;

			EmitNode emit = (EmitNode)imperativeStmt;
			for(BaseNode child : emit.getChildren()) {
				ExprNode expr = (ExprNode)child;
				for(ConstraintDeclNode declNode : collectNeededElements(expr)) {
					valid &= checkEmitElemNotDeleted(declNode, expr, delete, maybeDeleted);
				}
			}
		}

		return valid;
	}

	private boolean checkEmitElemNotDeleted(ConstraintDeclNode declNode, ExprNode expr,
			Set<DeclNode> delete, Collection<DeclNode> maybeDeleted)
	{
		if(delete.contains(declNode)) {
			expr.reportError("The deleted " + declNode.getUseString() + " \"" + declNode.ident
					+ "\" must not be used in an emit(/emitdebug) statement (you may use an emithere(/emitheredebug) instead)");
			return false;
		}
		if(maybeDeleted.contains(declNode)) {
			declNode.maybeDeleted = true;

			if(!declNode.getIdentNode().getAnnotations().isFlagSet("maybeDeleted")) {
				String errorMessage = "Element \"" + declNode.ident + "\" used in emit statement may be deleted"
						+ ", possibly it's homomorphic with a deleted " + declNode.getUseString();
				errorMessage += " (use a [maybeDeleted] annotation if you think that this does not cause problems)";

				if(declNode instanceof EdgeDeclNode) {
					errorMessage += " or \"" + declNode.ident + "\" is a dangling " + declNode.getUseString()
							+ " and a deleted node exists";
				}

				errorMessage += " (you may use an emithere instead)";

				expr.reportError(errorMessage);

				return false;
			}
		}

		return true;
	}

	private void calcMaybeRetyped()
	{
		for(Set<ConstraintDeclNode> homSet : pattern.getHoms()) {
			boolean containsRetypedElem = false;
			for(ConstraintDeclNode elem : homSet) {
				if(elem.getRetypedElement() != null) {
					containsRetypedElem = true;
					break;
				}
			}

			// If there was one homomorphic element, which is retyped,
			// all non-retyped elements in the same hom group are marked
			// as maybeRetyped.
			if(containsRetypedElem) {
				for(ConstraintDeclNode elem : homSet) {
					if(elem.getRetypedElement() == null)
						elem.maybeRetyped = true;
				}
			}
		}
	}

	/**
	 * Check, if the rule type node is right.
	 * The children of a rule type are
	 * 1) a pattern for the left side.
	 * 2) a pattern for the right side.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal()
	{
		right.warnElemAppearsInsideAndOutsideDelete(pattern);

		boolean leftHandGraphsOk = super.checkLocal();

		GraphNode right = this.right.graph;

		// check if the pattern name equals the rule name
		// named replace/modify parts are only allowed in subpatterns
		String ruleName = ident.toString();
		if(!right.nameOfGraph.equals(ruleName))
			this.right.reportError("Named replace/modify parts in rules are not allowed");

		// check if parameters only exists for subpatterns
		if(right.params.getChildren().size() > 0)
			this.right.reportError("Parameters for the replace/modify part are only allowed in subpatterns");

		boolean noReturnInPatternOk = true;
		if(pattern.returns.size() > 0) {
			reportError("No return statements in pattern parts of rules allowed");
			noReturnInPatternOk = false;
		}

		calcMaybeRetyped();

		return leftHandGraphsOk
				& checkRhsReuse(this.right)
				& sameNumberOfRewriteParts(this.right, "rule")
				& noReturnInPatternOk
				& noAbstractElementInstantiated(this.right)
				& checkRetypedElemsNotDeleted()
				& checkReturnedElemsNotDeleted()
				& checkElemsNotRetypedToDifferentTypes()
				& checkReturnedElemsNotRetyped()
				& checkExecParamsNotDeleted(this.right)
				& checkEmitElemsNotDeleted()
				& checkReturns(right.returns);
	}

	public NodeDeclNode tryGetNode(IdentNode ident)
	{
		for(NodeDeclNode node : pattern.getNodes()) {
			if(node.ident.toString().equals(ident.toString()))
				return node;
		}
		for(NodeDeclNode node : right.graph.getNodes()) {
			if(node.ident.toString().equals(ident.toString()))
				return node;
		}
		return null;
	}

	public EdgeDeclNode tryGetEdge(IdentNode ident)
	{
		for(EdgeDeclNode edge : pattern.getEdges()) {
			if(edge.ident.toString().equals(ident.toString()))
				return edge;
		}
		for(EdgeDeclNode edge : right.graph.getEdges()) {
			if(edge.ident.toString().equals(ident.toString()))
				return edge;
		}
		return null;
	}

	public VarDeclNode tryGetVar(IdentNode ident)
	{
		for(VarDeclNode var : pattern.defVariablesToBeYieldedTo.getChildren()) {
			if(var.ident.toString().equals(ident.toString()))
				return var;
		}
		for(DeclNode varCand : pattern.getParamDecls()) {
			if(!(varCand instanceof VarDeclNode))
				continue;
			VarDeclNode var = (VarDeclNode)varCand;
			if(var.ident.toString().equals(ident.toString()))
				return var;
		}
		for(VarDeclNode var : right.graph.defVariablesToBeYieldedTo.getChildren()) {
			if(var.ident.toString().equals(ident.toString()))
				return var;
		}
		for(DeclNode varCand : right.graph.getParamDecls()) {
			if(!(varCand instanceof VarDeclNode))
				continue;
			VarDeclNode var = (VarDeclNode)varCand;
			if(var.ident.toString().equals(ident.toString()))
				return var;
		}
		return null;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR()
	{
		// return if the pattern graph already constructed the IR object
		// that may happen in recursive patterns (and other usages/references)
		if(isIRAlreadySet()) {
			return getIR();
		}

		Rule rule = new Rule(getIdentNode().getIdent());

		// mark this node as already visited
		setIR(rule);

		PatternGraph left = pattern.getPatternGraph();
		for(DeclNode varCand : pattern.getParamDecls()) {
			if(!(varCand instanceof VarDeclNode))
				continue;
			VarDeclNode var = (VarDeclNode)varCand;
			left.addVariable(var.checkIR(Variable.class));
		}

		PatternGraph right = this.right.getPatternGraph(left);

		rule.initialize(left, right);

		for(DefinedMatchTypeNode implementedMatchClassNode : implementedMatchTypes.getChildren()) {
			DefinedMatchType implementedMatchClass = implementedMatchClassNode.checkIR(DefinedMatchType.class);
			rule.addImplementedMatchClass(implementedMatchClass);
		}

		constructImplicitNegs(left);
		constructIRaux(rule, this.right.graph.returns);

		// add eval statements to the IR
		for(EvalStatements evalStatement : this.right.getRHSGraph().getYieldEvalStatements()) {
			rule.addEval(evalStatement);
		}

		return rule;
	}

	@Override
	public RuleTypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}
}
