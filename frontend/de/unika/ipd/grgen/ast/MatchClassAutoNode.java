/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.Vector;
import java.util.function.Supplier;

import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.AlternativeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.DummyNodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.pattern.ConnectionCharacter;
import de.unika.ipd.grgen.ast.pattern.ConnectionNode;
import de.unika.ipd.grgen.ast.pattern.ExactNode;
import de.unika.ipd.grgen.ast.pattern.HomNode;
import de.unika.ipd.grgen.ast.pattern.InducedNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.pattern.SingleNodeConnNode;
import de.unika.ipd.grgen.ast.pattern.SubpatternReplNode;
import de.unika.ipd.grgen.ast.pattern.TotallyHomNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementsNode;
import de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;


/**
 * AST node class representing auto-generated match classes / match class bodies
 * (combining matches under name merging, to be used as results of "natural joins" of matches)
 */
public class MatchClassAutoNode extends BaseNode
{
	static {
		setName(MatchClassAutoNode.class, "match class auto");
	}

	protected String nameOfGraph;
	protected Coords coords;
	protected int modifiers;
	protected int context;
	
	protected CollectNode<IdentNode> matchTypesUnresolved;
	protected CollectNode<MatchTypeActionNode> matchTypes;

	CollectNode<BaseNode> connections;
	CollectNode<BaseNode> params;

	public MatchClassAutoNode(String nameOfGraph, Coords coords, int modifiers, int context, 
			CollectNode<IdentNode> matchTypes)
	{
		super(coords);
		this.nameOfGraph = nameOfGraph;
		this.modifiers = modifiers;
		this.context = context;
		this.matchTypesUnresolved = becomeParent(matchTypes);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(matchTypesUnresolved, matchTypes));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("matchTypes");
		return childrenNames;
	}

	private static final CollectResolver<MatchTypeActionNode> matchTypesResolver =
			new CollectResolver<MatchTypeActionNode>(new DeclarationTypeResolver<MatchTypeActionNode>(MatchTypeActionNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		for(IdentNode mtid : matchTypesUnresolved.getChildren()) {
			if(!(mtid instanceof PackageIdentNode)) {
				fixupDefinition(mtid, mtid.getScope());
			}
		}
		matchTypes = matchTypesResolver.resolve(matchTypesUnresolved, this);

		return matchTypes != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(matchTypes.getChildren().size() != 2) {
			reportError("The auto(match<T> | match<S>) construct is only supported on two types (given are " + matchTypes.getChildren().size() + ").");
			return false;
		}
		
		return true;
	}

	public PatternGraphLhsNode getPatternGraph()
	{
		connections = new CollectNode<BaseNode>();
		params = new CollectNode<BaseNode>();
		
		CollectNode<SubpatternUsageDeclNode> subpatterns = new CollectNode<SubpatternUsageDeclNode>();
		CollectNode<SubpatternReplNode> subpatternRepls = new CollectNode<SubpatternReplNode>();
		CollectNode<AlternativeDeclNode> alts = new CollectNode<AlternativeDeclNode>();
		CollectNode<IteratedDeclNode> iters = new CollectNode<IteratedDeclNode>();
		CollectNode<PatternGraphLhsNode> negs = new CollectNode<PatternGraphLhsNode>();
		CollectNode<PatternGraphLhsNode> idpts = new CollectNode<PatternGraphLhsNode>();
		CollectNode<ExprNode> conds = new CollectNode<ExprNode>();
		CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
		CollectNode<HomNode> homs = new CollectNode<HomNode>();
		CollectNode<TotallyHomNode> totallyhoms = new CollectNode<TotallyHomNode>();
		CollectNode<ExactNode> exact = new CollectNode<ExactNode>();
		CollectNode<InducedNode> induced = new CollectNode<InducedNode>();
		PatternGraphLhsNode res = new PatternGraphLhsNode(nameOfGraph, coords, 
				connections, params, subpatterns, subpatternRepls,
				alts, iters, negs, idpts, conds,
				returnz, homs, totallyhoms, exact, induced, modifiers, context);
		
		return res;
	}

	public boolean fillPatternGraph(PatternGraphLhsNode patternGraph)
	{
		boolean result = true;
		
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();

		Map<String, TypeNode> entitiesToTypes = new HashMap<String, TypeNode>();
		
		for(MatchTypeActionNode matchType : matchTypes.getChildren()) {
			PatternGraphLhsNode lhsPattern = matchType.getAction().pattern;
			for(ConnectionCharacter cc : lhsPattern.getConnections()) {
				if(cc instanceof ConnectionNode) {
					ConnectionNode connection = (ConnectionNode)cc;
					EdgeDeclNode edge = connection.getEdge();
					String edgeName = edge.getIdentNode().getSymbol().getText();
					if(edgeName.startsWith("$")) {
						result &= addSourceAndTargetIfNotYetAddedOrTypeCheckIfDuplicate(connection,
								entitiesToTypes, connections, patternGraph);
					} else {
						if(!entitiesToTypes.containsKey(edgeName)) {
							ConnectionNode connectionClone = connection.cloneForAuto(patternGraph);
							connections.addChild(connectionClone);
							entitiesToTypes.put(edgeName, edge.getDeclType());
							result &= replaceSourceAndTargetIfAlreadyAdded(connection,
									connectionClone, entitiesToTypes, patternGraph);
						} else {
							result &= addSourceAndTargetIfNotYetAddedOrTypeCheckIfDuplicate(connection,
									entitiesToTypes, connections, patternGraph);
							result &= isTypeMatching(edge, entitiesToTypes);
						}
					}
				} else {
					SingleNodeConnNode singleNode = (SingleNodeConnNode)cc;
					NodeDeclNode node = singleNode.getNode();
					result &= addIfNotYetAddedOrTypeCheckIfDuplicate(node, entitiesToTypes,
							connections, () -> singleNode.cloneForAuto(patternGraph));
				}
			}
			
			for(VarDeclNode defVar : lhsPattern.getDefVariablesToBeYieldedTo().getChildren()) {
				result &= addIfNotYetAddedOrTypeCheckIfDuplicate(defVar, entitiesToTypes,
						params, () -> defVar.cloneForAuto(patternGraph));
			}
			
			for(BaseNode param : lhsPattern.params.getChildren()) {
				if(param instanceof VarDeclNode) {
					VarDeclNode var = (VarDeclNode)param;
					result &= addIfNotYetAddedOrTypeCheckIfDuplicate(var, entitiesToTypes,
							params, () -> var.cloneForAuto(patternGraph));
				} 
			}
		}

		patternGraph.addDefVariablesToBeYieldedTo(defVariablesToBeYieldedTo);
		patternGraph.addYieldings(evals);
		
		return result;
	}

	private boolean addSourceAndTargetIfNotYetAddedOrTypeCheckIfDuplicate(ConnectionNode connection,
			Map<String, TypeNode> entitiesToTypes, CollectNode<BaseNode> connections,
			PatternGraphLhsNode patternGraph)
	{
		boolean result = true;
		NodeDeclNode source = connection.getSrc();
		if(!(source instanceof DummyNodeDeclNode)) {
			result &= addIfNotYetAddedOrTypeCheckIfDuplicate(source, entitiesToTypes,
					connections, () -> new SingleNodeConnNode(source.cloneForAuto(patternGraph)));
		}
		NodeDeclNode target = connection.getTgt();
		if(!(target instanceof DummyNodeDeclNode)) {
			result &= addIfNotYetAddedOrTypeCheckIfDuplicate(target, entitiesToTypes,
					connections, () -> new SingleNodeConnNode(target.cloneForAuto(patternGraph)));
		}
		return result;
	}
	
	private boolean replaceSourceAndTargetIfAlreadyAdded(ConnectionNode connection,
			ConnectionNode connectionClone, Map<String, TypeNode> entitiesToTypes,
			PatternGraphLhsNode patternGraph)
	{
		NodeDeclNode source = connection.getSrc();
		String sourceName = source.getIdentNode().getSymbol().getText();
		if(entitiesToTypes.containsKey(sourceName)) {
			NodeDeclNode newSource = new DummyNodeDeclNode(source.getIdentNode(),
					source.getDeclType(), source.context, patternGraph);
			connectionClone.setSrc(newSource);
		}
		NodeDeclNode target = connection.getTgt();
		String targetName = target.getIdentNode().getSymbol().getText();
		if(entitiesToTypes.containsKey(targetName)) {
			NodeDeclNode newTarget = new DummyNodeDeclNode(target.getIdentNode(),
					target.getDeclType(), target.context, patternGraph);
			connectionClone.setTgt(newTarget);
		}
		return isTypeMatching(source, entitiesToTypes) & isTypeMatching(target, entitiesToTypes);
	}
	
	private boolean addIfNotYetAddedOrTypeCheckIfDuplicate(DeclNode entity, Map<String, TypeNode> entitiesToTypes,
			CollectNode<BaseNode> connections, Supplier<BaseNode> entityForAuto)
	{
		String nodeName = entity.getIdentNode().getSymbol().getText();
		if(nodeName.startsWith("$"))
			return true;
		if(!entitiesToTypes.containsKey(nodeName)) {
			connections.addChild(entityForAuto.get());
			entitiesToTypes.put(nodeName, entity.getDeclType());
			return true;
		} else
			return isTypeMatching(entity, entitiesToTypes);
	}
	
	private boolean isTypeMatching(DeclNode decl, Map<String, TypeNode> entitiesToTypes)
	{
		String entity = decl.getIdentNode().getSymbol().getText();
		TypeNode type = entitiesToTypes.get(entity);
		if(!decl.getDeclType().isEqual(type)) {
			reportError("Ambiguous resulting type: the entity " + entity
					+ " is declared with type " + type.toStringWithDeclarationCoords()
					+ " and with type + " + decl.getDeclType().toStringWithDeclarationCoords() + ".");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		throw new RuntimeException("Not implemented");
	}
}
