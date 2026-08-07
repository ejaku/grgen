/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit, Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.invocation
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using IdentExprNode = de.unika.ipd.grgen.ast.expr.IdentExprNode;
using ArrayAndNode = de.unika.ipd.grgen.ast.expr.array.ArrayAndNode;
using ArrayAsDequeNode = de.unika.ipd.grgen.ast.expr.array.ArrayAsDequeNode;
using ArrayAsMapNode = de.unika.ipd.grgen.ast.expr.array.ArrayAsMapNode;
using ArrayAsSetNode = de.unika.ipd.grgen.ast.expr.array.ArrayAsSetNode;
using ArrayAsStringNode = de.unika.ipd.grgen.ast.expr.array.ArrayAsStringNode;
using ArrayAvgNode = de.unika.ipd.grgen.ast.expr.array.ArrayAvgNode;
using ArrayDevNode = de.unika.ipd.grgen.ast.expr.array.ArrayDevNode;
using ArrayEmptyNode = de.unika.ipd.grgen.ast.expr.array.ArrayEmptyNode;
using ArrayExtractNode = de.unika.ipd.grgen.ast.expr.array.ArrayExtractNode;
using ArrayGroupByNode = de.unika.ipd.grgen.ast.expr.array.ArrayGroupByNode;
using ArrayGroupNode = de.unika.ipd.grgen.ast.expr.array.ArrayGroupNode;
using ArrayIndexOfByNode = de.unika.ipd.grgen.ast.expr.array.ArrayIndexOfByNode;
using ArrayIndexOfNode = de.unika.ipd.grgen.ast.expr.array.ArrayIndexOfNode;
using ArrayIndexOfOrderedByNode = de.unika.ipd.grgen.ast.expr.array.ArrayIndexOfOrderedByNode;
using ArrayIndexOfOrderedNode = de.unika.ipd.grgen.ast.expr.array.ArrayIndexOfOrderedNode;
using ArrayKeepOneForEachByNode = de.unika.ipd.grgen.ast.expr.array.ArrayKeepOneForEachByNode;
using ArrayKeepOneForEachNode = de.unika.ipd.grgen.ast.expr.array.ArrayKeepOneForEachNode;
using ArrayLastIndexOfByNode = de.unika.ipd.grgen.ast.expr.array.ArrayLastIndexOfByNode;
using ArrayLastIndexOfNode = de.unika.ipd.grgen.ast.expr.array.ArrayLastIndexOfNode;
using ArrayMaxNode = de.unika.ipd.grgen.ast.expr.array.ArrayMaxNode;
using ArrayMedNode = de.unika.ipd.grgen.ast.expr.array.ArrayMedNode;
using ArrayMedUnorderedNode = de.unika.ipd.grgen.ast.expr.array.ArrayMedUnorderedNode;
using ArrayMinNode = de.unika.ipd.grgen.ast.expr.array.ArrayMinNode;
using ArrayOrNode = de.unika.ipd.grgen.ast.expr.array.ArrayOrNode;
using ArrayOrderAscendingByNode = de.unika.ipd.grgen.ast.expr.array.ArrayOrderAscendingByNode;
using ArrayOrderAscendingNode = de.unika.ipd.grgen.ast.expr.array.ArrayOrderAscendingNode;
using ArrayOrderDescendingByNode = de.unika.ipd.grgen.ast.expr.array.ArrayOrderDescendingByNode;
using ArrayOrderDescendingNode = de.unika.ipd.grgen.ast.expr.array.ArrayOrderDescendingNode;
using ArrayPeekNode = de.unika.ipd.grgen.ast.expr.array.ArrayPeekNode;
using ArrayProdNode = de.unika.ipd.grgen.ast.expr.array.ArrayProdNode;
using ArrayReverseNode = de.unika.ipd.grgen.ast.expr.array.ArrayReverseNode;
using ArrayShuffleNode = de.unika.ipd.grgen.ast.expr.array.ArrayShuffleNode;
using ArraySizeNode = de.unika.ipd.grgen.ast.expr.array.ArraySizeNode;
using ArraySubarrayNode = de.unika.ipd.grgen.ast.expr.array.ArraySubarrayNode;
using ArraySumNode = de.unika.ipd.grgen.ast.expr.array.ArraySumNode;
using ArrayVarNode = de.unika.ipd.grgen.ast.expr.array.ArrayVarNode;
using DequeAsArrayNode = de.unika.ipd.grgen.ast.expr.deque.DequeAsArrayNode;
using DequeAsSetNode = de.unika.ipd.grgen.ast.expr.deque.DequeAsSetNode;
using DequeEmptyNode = de.unika.ipd.grgen.ast.expr.deque.DequeEmptyNode;
using DequeIndexOfNode = de.unika.ipd.grgen.ast.expr.deque.DequeIndexOfNode;
using DequeLastIndexOfNode = de.unika.ipd.grgen.ast.expr.deque.DequeLastIndexOfNode;
using DequePeekNode = de.unika.ipd.grgen.ast.expr.deque.DequePeekNode;
using DequeSizeNode = de.unika.ipd.grgen.ast.expr.deque.DequeSizeNode;
using DequeSubdequeNode = de.unika.ipd.grgen.ast.expr.deque.DequeSubdequeNode;
using MapAsArrayNode = de.unika.ipd.grgen.ast.expr.map.MapAsArrayNode;
using MapDomainNode = de.unika.ipd.grgen.ast.expr.map.MapDomainNode;
using MapEmptyNode = de.unika.ipd.grgen.ast.expr.map.MapEmptyNode;
using MapPeekNode = de.unika.ipd.grgen.ast.expr.map.MapPeekNode;
using MapRangeNode = de.unika.ipd.grgen.ast.expr.map.MapRangeNode;
using MapSizeNode = de.unika.ipd.grgen.ast.expr.map.MapSizeNode;
using SetAsArrayNode = de.unika.ipd.grgen.ast.expr.set.SetAsArrayNode;
using SetEmptyNode = de.unika.ipd.grgen.ast.expr.set.SetEmptyNode;
using SetMaxNode = de.unika.ipd.grgen.ast.expr.set.SetMaxNode;
using SetMinNode = de.unika.ipd.grgen.ast.expr.set.SetMinNode;
using SetPeekNode = de.unika.ipd.grgen.ast.expr.set.SetPeekNode;
using SetSizeNode = de.unika.ipd.grgen.ast.expr.set.SetSizeNode;
using StringAsArrayNode = de.unika.ipd.grgen.ast.expr.@string.StringAsArrayNode;
using StringEndsWithNode = de.unika.ipd.grgen.ast.expr.@string.StringEndsWithNode;
using StringIndexOfNode = de.unika.ipd.grgen.ast.expr.@string.StringIndexOfNode;
using StringLastIndexOfNode = de.unika.ipd.grgen.ast.expr.@string.StringLastIndexOfNode;
using StringLengthNode = de.unika.ipd.grgen.ast.expr.@string.StringLengthNode;
using StringReplaceNode = de.unika.ipd.grgen.ast.expr.@string.StringReplaceNode;
using StringStartsWithNode = de.unika.ipd.grgen.ast.expr.@string.StringStartsWithNode;
using StringSubstringNode = de.unika.ipd.grgen.ast.expr.@string.StringSubstringNode;
using StringToLowerNode = de.unika.ipd.grgen.ast.expr.@string.StringToLowerNode;
using StringToUpperNode = de.unika.ipd.grgen.ast.expr.@string.StringToUpperNode;
using ExternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using UntypedExecVarTypeNode = de.unika.ipd.grgen.ast.type.basic.UntypedExecVarTypeNode;
using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
using DequeTypeNode = de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
using ResolvingEnvironment = de.unika.ipd.grgen.ast.util.ResolvingEnvironment;
using IR = de.unika.ipd.grgen.ir.IR;

public class FunctionMethodInvocationDecisionNode : FunctionInvocationBaseNode
{
	static FunctionMethodInvocationDecisionNode()
	{
		SetClassName(typeof(FunctionMethodInvocationDecisionNode), "function method invocation decision expression");
	}

	internal static TypeNode methodTypeNode = new TypeNodeAnonymousInnerClass();

	private class TypeNodeAnonymousInnerClass : TypeNode
	{
		private readonly FunctionMethodInvocationDecisionNode outerInstance;

		public override ICollection<BaseNode> Children
		{
			get
			{
			IList<BaseNode> children = new List<BaseNode>();
			// no children
			return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
			IList<string> childrenNames = new List<string>();
			// no children
			return childrenNames;
			}
		}
	}

	private ExprNode targetExpr;
	private IdentNode methodIdent;
	private IdentNode attributeIdent; // in most cases null, not used
	private FunctionOrBuiltinFunctionInvocationBaseNode result;

	public FunctionMethodInvocationDecisionNode(ExprNode targetExpr, IdentNode methodIdent, CollectNode<ExprNode> arguments,
			IdentNode attributeIdent)
		: base(methodIdent.Coords, arguments)
	{
		this.targetExpr = BecomeParent(targetExpr);
		this.methodIdent = BecomeParent(methodIdent);
		this.attributeIdent = BecomeParent(attributeIdent);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(targetExpr);
		//children.add(methodIdent);	// HACK: We don't have a declaration, so avoid failure during check phase
		children.Add(arguments);
		if(IsResolved())
			children.Add(result);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("targetExpr");
		//childrenNames.add("methodIdent");
		childrenNames.Add("params");
		if(IsResolved())
			childrenNames.Add("result");
		return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		if(!targetExpr.Resolve())
			return false;

		string methodName = methodIdent.ToString();
		TypeNode targetType = targetExpr.Type;

		ResolvingEnvironment resolvingEnvironment = new ResolvingEnvironment(null, error, Coords);
		if(targetType == BasicTypeNode.stringType)
			result = DecideString(targetExpr, attributeIdent, methodName, arguments, resolvingEnvironment);
		else if(targetType is MapTypeNode)
			result = DecideMap(targetExpr, attributeIdent, methodName, arguments, resolvingEnvironment);
		else if(targetType is SetTypeNode)
			result = DecideSet(targetExpr, attributeIdent, methodName, arguments, resolvingEnvironment);
		else if(targetType is ArrayTypeNode)
			result = DecideArray(targetExpr, attributeIdent, methodName, arguments, resolvingEnvironment);
		else if(targetType is DequeTypeNode)
			result = DecideDeque(targetExpr, attributeIdent, methodName, arguments, resolvingEnvironment);
		else if(targetType is InheritanceTypeNode && !(targetType is ExternalObjectTypeNode))
		{
			if(targetExpr is FunctionMethodInvocationDecisionNode)
			{
				ReportError("Method call chains are not supported, assign to a temporary def variable and invoke the method on it.");
				return false;
			}
			result = new FunctionMethodInvocationExprNode(((IdentExprNode)targetExpr).Ident, methodIdent, arguments);
		}
		else if(targetType is ExternalObjectTypeNode)
			result = new ExternalFunctionMethodInvocationExprNode(targetExpr, methodIdent, arguments);
		else if(targetType is UntypedExecVarTypeNode)
			result = new UntypedFunctionMethodInvocationExprNode(methodIdent.Coords, arguments);
		else
			ReportError(targetType.TypeName + " does not have any function methods.");

		return result != null;
	}

	private static BuiltinFunctionInvocationBaseNode DecideString(ExprNode targetExpr, IdentNode attributeIdent,
			string methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName)
		{
		case "length":
			if(arguments.Size() != 0)
			{
				env.ReportError("string.length() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new StringLengthNode(env.Coords, targetExpr);
		case "toUpper":
			if(arguments.Size() != 0)
			{
				env.ReportError("string.toUpper() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new StringToUpperNode(env.Coords, targetExpr);
		case "toLower":
			if(arguments.Size() != 0)
			{
				env.ReportError("string.toLower() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new StringToLowerNode(env.Coords, targetExpr);
		case "substring":
			if(arguments.Size() != 1 && arguments.Size() != 2)
			{
				env.ReportError("string.substring(startIndex, length) expects 2 arguments, or 1 if the length is omitted (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else if(arguments.Size() == 2)
				return new StringSubstringNode(env.Coords, targetExpr, arguments.Get(0), arguments.Get(1));
			else
				return new StringSubstringNode(env.Coords, targetExpr, arguments.Get(0));
		case "indexOf":
			if(arguments.Size() != 1 && arguments.Size() != 2)
			{
				env.ReportError("string.indexOf(strToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else if(arguments.Size() == 1)
				return new StringIndexOfNode(env.Coords, targetExpr, arguments.Get(0));
			else
				return new StringIndexOfNode(env.Coords, targetExpr, arguments.Get(0), arguments.Get(1));
		case "lastIndexOf":
			if(arguments.Size() != 1 && arguments.Size() != 2)
			{
				env.ReportError("string.lastIndexOf(strToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else if(arguments.Size() == 1)
				return new StringLastIndexOfNode(env.Coords, targetExpr, arguments.Get(0));
			else
				return new StringLastIndexOfNode(env.Coords, targetExpr, arguments.Get(0), arguments.Get(1));
		case "startsWith":
			if(arguments.Size() != 1)
			{
				env.ReportError("string.startsWith(strToSearchFor) expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new StringStartsWithNode(env.Coords, targetExpr, arguments.Get(0));
		case "endsWith":
			if(arguments.Size() != 1)
			{
				env.ReportError("string.endsWith(strToSearchFor) expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new StringEndsWithNode(env.Coords, targetExpr, arguments.Get(0));
		case "replace":
			if(arguments.Size() != 3)
			{
				env.ReportError("string.replace(startIndex, length, replaceStr) expects 3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new StringReplaceNode(env.Coords, targetExpr, arguments.Get(0), arguments.Get(1), arguments.Get(2));
		case "asArray":
			if(arguments.Size() != 1)
			{
				env.ReportError("string.asArray(separator) expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new StringAsArrayNode(env.Coords, targetExpr, arguments.Get(0));
		default:
			env.ReportError("string does not have a function method named " + methodName + ".");
			return null;
		}
	}

	private static BuiltinFunctionInvocationBaseNode DecideMap(ExprNode targetExpr, IdentNode attributeIdent,
			string methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName)
		{
		case "size":
			if(arguments.Size() != 0)
			{
				env.ReportError("map<S,T>.size() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new MapSizeNode(env.Coords, targetExpr);
		case "empty":
			if(arguments.Size() != 0)
			{
				env.ReportError("map<S,T>.empty() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new MapEmptyNode(env.Coords, targetExpr);
		case "domain":
			if(arguments.Size() != 0)
			{
				env.ReportError("map<S,T>.domain() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new MapDomainNode(env.Coords, targetExpr);
		case "range":
			if(arguments.Size() != 0)
			{
				env.ReportError("map<S,T>.range() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new MapRangeNode(env.Coords, targetExpr);
		case "asArray":
			if(arguments.Size() != 0)
			{
				env.ReportError("map<int,T>.asArray() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new MapAsArrayNode(env.Coords, targetExpr);
		case "peek":
			if(arguments.Size() != 1)
			{
				env.ReportError("map<S,T>.peek(number in iteration sequence) expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new MapPeekNode(env.Coords, targetExpr, arguments.Get(0));
		default:
			env.ReportError("map<S,T> does not have a function method named " + methodName + ".");
			return null;
		}
	}

	private static BuiltinFunctionInvocationBaseNode DecideSet(ExprNode targetExpr, IdentNode attributeIdent,
			string methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName)
		{
		case "size":
			if(arguments.Size() != 0)
			{
				env.ReportError("set<T>.size() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new SetSizeNode(env.Coords, targetExpr);
		case "empty":
			if(arguments.Size() != 0)
			{
				env.ReportError("set<T>.empty() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new SetEmptyNode(env.Coords, targetExpr);
		case "peek":
			if(arguments.Size() != 1)
			{
				env.ReportError("set<T>.peek(number in iteration sequence) expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new SetPeekNode(env.Coords, targetExpr, arguments.Get(0));
		case "min":
			if(arguments.Size() != 0)
			{
				env.ReportError("set<T>.min() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new SetMinNode(env.Coords, targetExpr);
		case "max":
			if(arguments.Size() != 0)
			{
				env.ReportError("set<T>.max() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new SetMaxNode(env.Coords, targetExpr);
		case "asArray":
			if(arguments.Size() != 0)
			{
				env.ReportError("set<T>.asArray() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new SetAsArrayNode(env.Coords, targetExpr);
		default:
			env.ReportError("set<T> does not have a function method named " + methodName + ".");
			return null;
		}
	}

	private static BuiltinFunctionInvocationBaseNode DecideArray(ExprNode targetExpr, IdentNode attributeIdent,
			string methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName)
		{
		case "size":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.size() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArraySizeNode(env.Coords, targetExpr);
		case "empty":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.empty() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayEmptyNode(env.Coords, targetExpr);
		case "peek":
			if(arguments.Size() != 0 && arguments.Size() != 1)
			{
				env.ReportError("array<T>.peek(index) expects 1 argument; or 0 arguments, then returning the value from the end (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(arguments.Size() == 0)
					return new ArrayPeekNode(env.Coords, targetExpr);
				else
					return new ArrayPeekNode(env.Coords, targetExpr, arguments.Get(0));
			}
			goto case "indexOf";
		case "indexOf":
			if(arguments.Size() != 1 && arguments.Size() != 2)
			{
				env.ReportError("array<T>.indexOf(valueToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else if(arguments.Size() == 1)
				return new ArrayIndexOfNode(env.Coords, targetExpr, arguments.Get(0));
			else
				return new ArrayIndexOfNode(env.Coords, targetExpr, arguments.Get(0), arguments.Get(1));
		case "indexOfBy":
			if(arguments.Size() != 1 && arguments.Size() != 2)
			{
				env.ReportError("array<T>.indexOfBy<attribute>(valueToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else if(arguments.Size() == 1)
				return new ArrayIndexOfByNode(env.Coords, targetExpr, attributeIdent, arguments.Get(0));
			else
				return new ArrayIndexOfByNode(env.Coords, targetExpr, attributeIdent, arguments.Get(0), arguments.Get(1));
		case "indexOfOrdered":
			if(arguments.Size() != 1)
			{
				env.ReportError("array<T>.indexOfOrdered(valueToSearchFor) expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayIndexOfOrderedNode(env.Coords, targetExpr, arguments.Get(0));
		case "indexOfOrderedBy":
			if(arguments.Size() != 1)
			{
				env.ReportError("array<T>.indexOfOrderedBy<attribute>(valueToSearchFor) expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayIndexOfOrderedByNode(env.Coords, targetExpr, attributeIdent, arguments.Get(0));
		case "lastIndexOf":
			if(arguments.Size() != 1 && arguments.Size() != 2)
			{
				env.ReportError("array<T>.lastIndexOf(valueToSearchFor) expects 1 argument, or a second startIndex argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else if(arguments.Size() == 1)
				return new ArrayLastIndexOfNode(env.Coords, targetExpr, arguments.Get(0));
			else
				return new ArrayLastIndexOfNode(env.Coords, targetExpr, arguments.Get(0), arguments.Get(1));
		case "lastIndexOfBy":
			if(arguments.Size() != 1 && arguments.Size() != 2)
			{
				env.ReportError("array<T>.lastIndexOfBy<attribute>(valueToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else if(arguments.Size() == 1)
				return new ArrayLastIndexOfByNode(env.Coords, targetExpr, attributeIdent, arguments.Get(0));
			else
				return new ArrayLastIndexOfByNode(env.Coords, targetExpr, attributeIdent, arguments.Get(0), arguments.Get(1));
		case "subarray":
			if(arguments.Size() != 2)
			{
				env.ReportError("array<T>.subarray(startIndex, length) expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArraySubarrayNode(env.Coords, targetExpr, arguments.Get(0), arguments.Get(1));
		case "orderAscending":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.orderAscending() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayOrderAscendingNode(env.Coords, targetExpr);
		case "orderDescending":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.orderDescending() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayOrderDescendingNode(env.Coords, targetExpr);
		case "group":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.group() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayGroupNode(env.Coords, targetExpr);
		case "keepOneForEach":
			if(attributeIdent == null)
			{
				if(arguments.Size() != 0)
				{
					env.ReportError("array<T>.keepOneForEach() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new ArrayKeepOneForEachNode(env.Coords, targetExpr);
			}
			else
			{
				if(arguments.Size() != 0)
				{
					env.ReportError("array<T>.keepOneForEach<attribute>() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new ArrayKeepOneForEachByNode(env.Coords, targetExpr, attributeIdent);
			}
			goto case "orderAscendingBy";
		case "orderAscendingBy":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.orderAscendingBy<attribute>() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayOrderAscendingByNode(env.Coords, targetExpr, attributeIdent);
		case "orderDescendingBy":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.orderDescendingBy<attribute>() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayOrderDescendingByNode(env.Coords, targetExpr, attributeIdent);
		case "groupBy":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.groupBy<attribute>() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayGroupByNode(env.Coords, targetExpr, attributeIdent);
		case "reverse":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.reverse() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayReverseNode(env.Coords, targetExpr);
		case "shuffle":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.shuffle() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayShuffleNode(env.Coords, targetExpr);
		case "extract":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.extract<attribute>() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayExtractNode(env.Coords, targetExpr, attributeIdent);
		case "asSet":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.asSet() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayAsSetNode(env.Coords, targetExpr);
		case "asDeque":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.asDeque() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayAsDequeNode(env.Coords, targetExpr);
		case "asMap":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.asMap() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayAsMapNode(env.Coords, targetExpr);
		case "asString":
			if(arguments.Size() != 1)
			{
				env.ReportError("array<string>.asString(separator) expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayAsStringNode(env.Coords, targetExpr, arguments.Get(0));
		case "sum":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.sum() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArraySumNode(env.Coords, targetExpr);
		case "prod":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.prod() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayProdNode(env.Coords, targetExpr);
		case "min":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.min() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayMinNode(env.Coords, targetExpr);
		case "max":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.max() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayMaxNode(env.Coords, targetExpr);
		case "avg":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.avg() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayAvgNode(env.Coords, targetExpr);
		case "med":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.med() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayMedNode(env.Coords, targetExpr);
		case "medUnordered":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.medUnordered() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayMedUnorderedNode(env.Coords, targetExpr);
		case "var":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.var() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayVarNode(env.Coords, targetExpr);
		case "dev":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.dev() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayDevNode(env.Coords, targetExpr);
		case "and":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.and() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayAndNode(env.Coords, targetExpr);
		case "or":
			if(arguments.Size() != 0)
			{
				env.ReportError("array<T>.or() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new ArrayOrNode(env.Coords, targetExpr);
		default:
			env.ReportError("array<T> does not have a function method named " + methodName + ".");
			return null;
		}
	}

	private static BuiltinFunctionInvocationBaseNode DecideDeque(ExprNode targetExpr, IdentNode attributeIdent,
			string methodName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(methodName)
		{
		case "size":
			if(arguments.Size() != 0)
			{
				env.ReportError("deque<T>.size() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new DequeSizeNode(env.Coords, targetExpr);
		case "empty":
			if(arguments.Size() != 0)
			{
				env.ReportError("deque<T>.empty() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new DequeEmptyNode(env.Coords, targetExpr);
		case "peek":
			if(arguments.Size() != 0 && arguments.Size() != 1)
			{
				env.ReportError("deque<T>.peek(index) expects 1 argument; or 0 arguments, then returning the value from the begin (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(arguments.Size() == 0)
					return new DequePeekNode(env.Coords, targetExpr);
				else
					return new DequePeekNode(env.Coords, targetExpr, arguments.Get(0));
			}
			goto case "indexOf";
		case "indexOf":
			if(arguments.Size() != 1 && arguments.Size() != 2)
			{
				env.ReportError("deque<T>.indexOf(valueToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else if(arguments.Size() == 1)
				return new DequeIndexOfNode(env.Coords, targetExpr, arguments.Get(0));
			else
				return new DequeIndexOfNode(env.Coords, targetExpr, arguments.Get(0), arguments.Get(1));
		case "lastIndexOf":
			if(arguments.Size() != 1 && arguments.Size() != 2)
			{
				env.ReportError("deque<T>.lastIndexOf(valueToSearchFor) expects one argument, or a second startIndex argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else if(arguments.Size() == 1)
				return new DequeLastIndexOfNode(env.Coords, targetExpr, arguments.Get(0));
			else
				return new DequeLastIndexOfNode(env.Coords, targetExpr, arguments.Get(0), arguments.Get(1));
		case "subdeque":
			if(arguments.Size() != 2)
			{
				env.ReportError("deque<T>.subdeque(startIndex, length) expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new DequeSubdequeNode(env.Coords, targetExpr, arguments.Get(0), arguments.Get(1));
		case "asSet":
			if(arguments.Size() != 0)
			{
				env.ReportError("deque<T>.asSet() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new DequeAsSetNode(env.Coords, targetExpr);
		case "asArray":
			if(arguments.Size() != 0)
			{
				env.ReportError("deque<T>.asArray() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new DequeAsArrayNode(env.Coords, targetExpr);
		default:
			env.ReportError("deque<T> does not have a function method named " + methodName + ".");
			return null;
		}
	}

	protected internal override bool CheckLocal()
	{
		return true;
	}

	public override TypeNode Type
	{
		get
		{
		return result.Type;
		}
	}

	protected internal virtual ExprNode Result
	{
		get
		{
		return result;
		}
	}

	protected internal override IR ConstructIR()
	{
		return result.IR;
	}
}

}
