/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.invocation;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CountEdgesFromIndexAccessFromToExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CountEdgesFromIndexAccessSameExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CountIncidenceFromIndexExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CountNodesFromIndexAccessFromToExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CountNodesFromIndexAccessSameExprNode;
import de.unika.ipd.grgen.ast.expr.graph.EdgesFromIndexAccessFromToAsArrayExprNode;
import de.unika.ipd.grgen.ast.expr.graph.EdgesFromIndexAccessFromToExprNode;
import de.unika.ipd.grgen.ast.expr.graph.EdgesFromIndexAccessMultipleFromToExprNode;
import de.unika.ipd.grgen.ast.expr.graph.EdgesFromIndexAccessSameAsArrayExprNode;
import de.unika.ipd.grgen.ast.expr.graph.EdgesFromIndexAccessSameExprNode;
import de.unika.ipd.grgen.ast.expr.graph.FromIndexAccessFromToPartExprNode;
import de.unika.ipd.grgen.ast.expr.graph.FromIndexAccessMultipleFromToExprNode;
import de.unika.ipd.grgen.ast.expr.graph.IsInEdgesFromIndexAccessFromToExprNode;
import de.unika.ipd.grgen.ast.expr.graph.IsInEdgesFromIndexAccessSameExprNode;
import de.unika.ipd.grgen.ast.expr.graph.IsInNodesFromIndexAccessFromToExprNode;
import de.unika.ipd.grgen.ast.expr.graph.IsInNodesFromIndexAccessSameExprNode;
import de.unika.ipd.grgen.ast.expr.graph.NodesFromIndexAccessFromToAsArrayExprNode;
import de.unika.ipd.grgen.ast.expr.graph.NodesFromIndexAccessFromToExprNode;
import de.unika.ipd.grgen.ast.expr.graph.NodesFromIndexAccessMultipleFromToExprNode;
import de.unika.ipd.grgen.ast.expr.graph.NodesFromIndexAccessSameAsArrayExprNode;
import de.unika.ipd.grgen.ast.expr.graph.NodesFromIndexAccessSameExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.FunctionTypeNode;
import de.unika.ipd.grgen.ast.util.ResolvingEnvironment;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.ParserEnvironment;

public class IndexFunctionInvocationDecisionNode extends FunctionOrBuiltinFunctionInvocationBaseNode
{
	static {
		setName(IndexFunctionInvocationDecisionNode.class, "index function invocation decision expression");
	}

	static TypeNode functionTypeNode = new FunctionTypeNode();

	public IdentNode functionIdent;
	private BuiltinFunctionInvocationBaseNode result;

	ParserEnvironment env;

	private CollectNode<BaseNode> arguments; // I prefer to keep the namespaces of indices and entities disjoint, so this special node is required with base node children instead of expression children (alternative would be to merge indices into entities, resolve the IdentExprNode also to an index, and remove this special handling as well as the special handling in the parser)

	
	public IndexFunctionInvocationDecisionNode(IdentNode functionIdent,
			CollectNode<BaseNode> arguments, ParserEnvironment env)
	{
		super(functionIdent.getCoords());
		this.functionIdent = becomeParent(functionIdent);
		this.env = env;
		this.arguments = becomeParent(arguments);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(arguments);
		if(isResolved())
			children.add(result);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("params");
		if(isResolved())
			childrenNames.add("result");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		ResolvingEnvironment resolvingEnvironment = new ResolvingEnvironment(env, error, getCoords());
		result = decide(functionIdent.toString(), arguments, resolvingEnvironment);
		return result != null;
	}
	
	private static BuiltinFunctionInvocationBaseNode decide(String functionName, CollectNode<BaseNode> arguments,
			ResolvingEnvironment env)
	{
		switch(functionName) {
		case "nodesFromIndex":
			if(arguments.size() != 1) {
				env.reportError("nodesFromIndex() expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new NodesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), null, false, null, false);
			}
		case "nodesFromIndexSame":
			if(arguments.size() != 2) {
				env.reportError("nodesFromIndexSame() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new NodesFromIndexAccessSameExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1));
			}
		case "nodesFromIndexFrom":
		case "nodesFromIndexFromExclusive":
		case "nodesFromIndexTo":
		case "nodesFromIndexToExclusive":
			if(arguments.size() != 2) {
				env.reportError(functionName + "() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				if(functionName.startsWith("nodesFromIndexFrom")) {
					return new NodesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1), functionName.endsWith("Exclusive"), null, false);
				} else {
					return new NodesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), null, false, (ExprNode)arguments.get(1), functionName.endsWith("Exclusive"));
				}
			}
		case "nodesFromIndexFromTo":
		case "nodesFromIndexFromExclusiveTo":
		case "nodesFromIndexFromToExclusive":
		case "nodesFromIndexFromExclusiveToExclusive":
			if(arguments.size() != 3) {
				env.reportError(functionName + "() expects 3 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new NodesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1), functionName.contains("FromExclusive"), (ExprNode)arguments.get(2), functionName.contains("ToExclusive"));
			}
		case "edgesFromIndex":
			if(arguments.size() != 1) {
				env.reportError("edgesFromIndex() expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new EdgesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), null, false, null, false);
			}
		case "edgesFromIndexSame":
			if(arguments.size() != 2) {
				env.reportError("edgesFromIndexSame() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new EdgesFromIndexAccessSameExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1));
			}
		case "edgesFromIndexFrom":
		case "edgesFromIndexFromExclusive":
		case "edgesFromIndexTo":
		case "edgesFromIndexToExclusive":
			if(arguments.size() != 2) {
				env.reportError(functionName + "() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				if(functionName.startsWith("edgesFromIndexFrom")) {
					return new EdgesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1), functionName.endsWith("Exclusive"), null, false);
				} else {
					return new EdgesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), null, false, (ExprNode)arguments.get(1), functionName.endsWith("Exclusive"));
				}
			}
		case "edgesFromIndexFromTo":
		case "edgesFromIndexFromExclusiveTo":
		case "edgesFromIndexFromToExclusive":
		case "edgesFromIndexFromExclusiveToExclusive":
			if(arguments.size() != 3) {
				env.reportError(functionName + "() expects 3 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new EdgesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1), functionName.contains("FromExclusive"), (ExprNode)arguments.get(2), functionName.contains("ToExclusive"));
			}
		case "countNodesFromIndex":
			if(arguments.size() != 1) {
				env.reportError("countNodesFromIndex() expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new CountNodesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), null, false, null, false);
			}
		case "countNodesFromIndexSame":
			if(arguments.size() != 2) {
				env.reportError("countNodesFromIndexSame() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new CountNodesFromIndexAccessSameExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1));
			}
		case "countNodesFromIndexFrom":
		case "countNodesFromIndexFromExclusive":
		case "countNodesFromIndexTo":
		case "countNodesFromIndexToExclusive":
			if(arguments.size() != 2) {
				env.reportError(functionName + "() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				if(functionName.startsWith("countNodesFromIndexFrom")) {
					return new CountNodesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1), functionName.endsWith("Exclusive"), null, false);
				} else {
					return new CountNodesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), null, false, (ExprNode)arguments.get(1), functionName.endsWith("Exclusive"));
				}
			}
		case "countNodesFromIndexFromTo":
		case "countNodesFromIndexFromExclusiveTo":
		case "countNodesFromIndexFromToExclusive":
		case "countNodesFromIndexFromExclusiveToExclusive":
			if(arguments.size() != 3) {
				env.reportError(functionName + "() expects 3 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new CountNodesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1), functionName.contains("FromExclusive"), (ExprNode)arguments.get(2), functionName.contains("ToExclusive"));
			}
		case "countEdgesFromIndex":
			if(arguments.size() != 1) {
				env.reportError("countEdgesFromIndex() expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new CountEdgesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), null, false, null, false);
			}
		case "countEdgesFromIndexSame":
			if(arguments.size() != 2) {
				env.reportError("countEdgesFromIndexSame() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new CountEdgesFromIndexAccessSameExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1));
			}
		case "countEdgesFromIndexFrom":
		case "countEdgesFromIndexFromExclusive":
		case "countEdgesFromIndexTo":
		case "countEdgesFromIndexToExclusive":
			if(arguments.size() != 2) {
				env.reportError(functionName + "() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				if(functionName.startsWith("countEdgesFromIndexFrom")) {
					return new CountEdgesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1), functionName.endsWith("Exclusive"), null, false);
				} else {
					return new CountEdgesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), null, false, (ExprNode)arguments.get(1), functionName.endsWith("Exclusive"));
				}
			}
		case "countEdgesFromIndexFromTo":
		case "countEdgesFromIndexFromExclusiveTo":
		case "countEdgesFromIndexFromToExclusive":
		case "countEdgesFromIndexFromExclusiveToExclusive":
			if(arguments.size() != 3) {
				env.reportError(functionName + "() expects 3 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new CountEdgesFromIndexAccessFromToExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1), functionName.contains("FromExclusive"), (ExprNode)arguments.get(2), functionName.contains("ToExclusive"));
			}
		case "isInNodesFromIndex":
			if(arguments.size() != 2) {
				env.reportError("isInNodesFromIndex() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new IsInNodesFromIndexAccessFromToExprNode(env.getCoords(), (ExprNode)arguments.get(0), arguments.get(1), null, false, null, false);
			}
		case "isInNodesFromIndexSame":
			if(arguments.size() != 3) {
				env.reportError("isInNodesFromIndexSame() expects 3 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new IsInNodesFromIndexAccessSameExprNode(env.getCoords(), (ExprNode)arguments.get(0), arguments.get(1), (ExprNode)arguments.get(2));
			}
		case "isInNodesFromIndexFrom":
		case "isInNodesFromIndexFromExclusive":
		case "isInNodesFromIndexTo":
		case "isInNodesFromIndexToExclusive":
			if(arguments.size() != 3) {
				env.reportError(functionName + "() expects 3 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				if(functionName.startsWith("isInNodesFromIndexFrom")) {
					return new IsInNodesFromIndexAccessFromToExprNode(env.getCoords(), (ExprNode)arguments.get(0), arguments.get(1), (ExprNode)arguments.get(2), functionName.endsWith("Exclusive"), null, false);
				} else {
					return new IsInNodesFromIndexAccessFromToExprNode(env.getCoords(), (ExprNode)arguments.get(0), arguments.get(1), null, false, (ExprNode)arguments.get(2), functionName.endsWith("Exclusive"));
				}
			}
		case "isInNodesFromIndexFromTo":
		case "isInNodesFromIndexFromExclusiveTo":
		case "isInNodesFromIndexFromToExclusive":
		case "isInNodesFromIndexFromExclusiveToExclusive":
			if(arguments.size() != 4) {
				env.reportError(functionName + "() expects 4 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new IsInNodesFromIndexAccessFromToExprNode(env.getCoords(), (ExprNode)arguments.get(0), arguments.get(1), (ExprNode)arguments.get(2), functionName.contains("FromExclusive"), (ExprNode)arguments.get(3), functionName.contains("ToExclusive"));
			}
		case "isInEdgesFromIndex":
			if(arguments.size() != 2) {
				env.reportError("isInEdgesFromIndex() expects 2 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new IsInEdgesFromIndexAccessFromToExprNode(env.getCoords(), (ExprNode)arguments.get(0), arguments.get(1), null, false, null, false);
			}
		case "isInEdgesFromIndexSame":
			if(arguments.size() != 3) {
				env.reportError("isInEdgesFromIndexSame() expects 3 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new IsInEdgesFromIndexAccessSameExprNode(env.getCoords(), (ExprNode)arguments.get(0), arguments.get(1), (ExprNode)arguments.get(2));
			}
		case "isInEdgesFromIndexFrom":
		case "isInEdgesFromIndexFromExclusive":
		case "isInEdgesFromIndexTo":
		case "isInEdgesFromIndexToExclusive":
			if(arguments.size() != 3) {
				env.reportError(functionName + "() expects 3 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				if(functionName.startsWith("isInEdgesFromIndexFrom")) {
					return new IsInEdgesFromIndexAccessFromToExprNode(env.getCoords(), (ExprNode)arguments.get(0), arguments.get(1), (ExprNode)arguments.get(2), functionName.endsWith("Exclusive"), null, false);
				} else {
					return new IsInEdgesFromIndexAccessFromToExprNode(env.getCoords(), (ExprNode)arguments.get(0), arguments.get(1), null, false, (ExprNode)arguments.get(2), functionName.endsWith("Exclusive"));
				}
			}
		case "isInEdgesFromIndexFromTo":
		case "isInEdgesFromIndexFromExclusiveTo":
		case "isInEdgesFromIndexFromToExclusive":
		case "isInEdgesFromIndexFromExclusiveToExclusive":
			if(arguments.size() != 4) {
				env.reportError(functionName + "() expects 4 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new IsInEdgesFromIndexAccessFromToExprNode(env.getCoords(), (ExprNode)arguments.get(0), arguments.get(1), (ExprNode)arguments.get(2), functionName.contains("FromExclusive"), (ExprNode)arguments.get(3), functionName.contains("ToExclusive"));
			}
		case "nodesFromIndexAsArrayAscending":
		case "nodesFromIndexAsArrayDescending":
			if(arguments.size() != 1) {
				env.reportError(functionName + "() expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new NodesFromIndexAccessFromToAsArrayExprNode(env.getCoords(), arguments.get(0), functionName.contains("Ascending"), null, false, null, false);
			}
		case "nodesFromIndexSameAsArray":
			if(arguments.size() != 2) {
				env.reportError("nodesFromIndexSameAsArray() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new NodesFromIndexAccessSameAsArrayExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1));
			}
		case "nodesFromIndexFromAsArrayAscending":
		case "nodesFromIndexFromExclusiveAsArrayAscending":
		case "nodesFromIndexToAsArrayAscending":
		case "nodesFromIndexToExclusiveAsArrayAscending":
		case "nodesFromIndexFromAsArrayDescending":
		case "nodesFromIndexFromExclusiveAsArrayDescending":
		case "nodesFromIndexToAsArrayDescending":
		case "nodesFromIndexToExclusiveAsArrayDescending":
			if(arguments.size() != 2) {
				env.reportError(functionName + "() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				if(functionName.startsWith("nodesFromIndexFrom")) {
					return new NodesFromIndexAccessFromToAsArrayExprNode(env.getCoords(), arguments.get(0), functionName.contains("Ascending"), (ExprNode)arguments.get(1), functionName.contains("Exclusive"), null, false);
				} else {
					return new NodesFromIndexAccessFromToAsArrayExprNode(env.getCoords(), arguments.get(0), functionName.contains("Ascending"), null, false, (ExprNode)arguments.get(1), functionName.contains("Exclusive"));
				}
			}
		case "nodesFromIndexFromToAsArrayAscending":
		case "nodesFromIndexFromExclusiveToAsArrayAscending":
		case "nodesFromIndexFromToExclusiveAsArrayAscending":
		case "nodesFromIndexFromExclusiveToExclusiveAsArrayAscending":
		case "nodesFromIndexFromToAsArrayDescending":
		case "nodesFromIndexFromExclusiveToAsArrayDescending":
		case "nodesFromIndexFromToExclusiveAsArrayDescending":
		case "nodesFromIndexFromExclusiveToExclusiveAsArrayDescending":
			if(arguments.size() != 3) {
				env.reportError(functionName + "() expects 3 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new NodesFromIndexAccessFromToAsArrayExprNode(env.getCoords(), arguments.get(0), functionName.contains("Ascending"), (ExprNode)arguments.get(1), functionName.contains("FromExclusive"), (ExprNode)arguments.get(2), functionName.contains("ToExclusive"));
			}
		case "edgesFromIndexAsArrayAscending":
		case "edgesFromIndexAsArrayDescending":
			if(arguments.size() != 1) {
				env.reportError(functionName + "() expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new EdgesFromIndexAccessFromToAsArrayExprNode(env.getCoords(), arguments.get(0), functionName.contains("Ascending"), null, false, null, false);
			}
		case "edgesFromIndexSameAsArray":
			if(arguments.size() != 2) {
				env.reportError("edgesFromIndexSameAsArray() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new EdgesFromIndexAccessSameAsArrayExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1));
			}
		case "edgesFromIndexFromAsArrayAscending":
		case "edgesFromIndexFromExclusiveAsArrayAscending":
		case "edgesFromIndexToAsArrayAscending":
		case "edgesFromIndexToExclusiveAsArrayAscending":
		case "edgesFromIndexFromAsArrayDescending":
		case "edgesFromIndexFromExclusiveAsArrayDescending":
		case "edgesFromIndexToAsArrayDescending":
		case "edgesFromIndexToExclusiveAsArrayDescending":
			if(arguments.size() != 2) {
				env.reportError(functionName + "() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				if(functionName.startsWith("edgesFromIndexFrom")) {
					return new EdgesFromIndexAccessFromToAsArrayExprNode(env.getCoords(), arguments.get(0), functionName.contains("Ascending"), (ExprNode)arguments.get(1), functionName.contains("Exclusive"), null, false);
				} else {
					return new EdgesFromIndexAccessFromToAsArrayExprNode(env.getCoords(), arguments.get(0), functionName.contains("Ascending"), null, false, (ExprNode)arguments.get(1), functionName.contains("Exclusive"));
				}
			}
		case "edgesFromIndexFromToAsArrayAscending":
		case "edgesFromIndexFromExclusiveToAsArrayAscending":
		case "edgesFromIndexFromToExclusiveAsArrayAscending":
		case "edgesFromIndexFromExclusiveToExclusiveAsArrayAscending":
		case "edgesFromIndexFromToAsArrayDescending":
		case "edgesFromIndexFromExclusiveToAsArrayDescending":
		case "edgesFromIndexFromToExclusiveAsArrayDescending":
		case "edgesFromIndexFromExclusiveToExclusiveAsArrayDescending":
			if(arguments.size() != 3) {
				env.reportError(functionName + "() expects 3 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new EdgesFromIndexAccessFromToAsArrayExprNode(env.getCoords(), arguments.get(0), functionName.contains("Ascending"), (ExprNode)arguments.get(1), functionName.contains("FromExclusive"), (ExprNode)arguments.get(2), functionName.contains("ToExclusive"));
			}
		case "nodesFromIndexMultipleFromTo":
		case "edgesFromIndexMultipleFromTo":
			if(arguments.size() % 3 != 0) {
				env.reportError(functionName + "() expects a multiple of 3 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				FromIndexAccessMultipleFromToExprNode indexAccessMultiple = functionName.equals("nodesFromIndexMultipleFromTo") ? 
						new NodesFromIndexAccessMultipleFromToExprNode(env.getCoords()) : new EdgesFromIndexAccessMultipleFromToExprNode(env.getCoords());
 				for(int i = 0; i < arguments.size(); i += 3)
				{
					BaseNode index = arguments.get(i);
					ExprNode fromExpr = (ExprNode)arguments.get(i + 1);
					ExprNode toExpr = (ExprNode)arguments.get(i + 2);
					indexAccessMultiple.addIndexAccessExpr(new FromIndexAccessFromToPartExprNode(index.getCoords(), index, fromExpr, false, toExpr, false, i, indexAccessMultiple));
				}
				return indexAccessMultiple;
			}
		case "countFromIndex":
			if(arguments.size() != 2) {
				env.reportError("countFromIndex() expects 2 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new CountIncidenceFromIndexExprNode(env.getCoords(), arguments.get(0), (ExprNode)arguments.get(1));
			}
		default:
			env.reportError("An index function of name " + functionName + " is not known.");
			return null;
		}
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return result.getType();
	}

	public ExprNode getResult()
	{
		return result;
	}

	@Override
	protected IR constructIR()
	{
		return result.getIR();
	}
}
