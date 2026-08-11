/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast
{

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using MatchClassFilterFunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.MatchClassFilterFunctionDeclNode;
using DefinedMatchTypeNode = de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
using Bad = de.unika.ipd.grgen.ir.Bad;
using IR = de.unika.ipd.grgen.ir.IR;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// Call of multiple actions.
/// </summary>
public class MultiCallActionNode : BaseNode
{
	static MultiCallActionNode()
	{
		SetClassName(typeof(MultiCallActionNode), "multiple call action");
	}

	private CollectNode<CallActionNode> actionCalls;

	private CollectNode<BaseNode> matchClassFilterFunctionsUnresolved;
	protected internal CollectNode<MatchTypeQualIdentNode> matchClassFilterFunctions;

	public MultiCallActionNode(Coords coords, CollectNode<CallActionNode> actionCalls,
			CollectNode<BaseNode> matchClassFilterFunctions)
		 : base(coords)
	{
		this.actionCalls = actionCalls;
		this.matchClassFilterFunctionsUnresolved = matchClassFilterFunctions;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(actionCalls);
			children.Add(GetValidVersionCollectNode(matchClassFilterFunctionsUnresolved, matchClassFilterFunctions));
			return children;
		}
	}

	/// <summary>
	/// returns names of the children, same order as in getChildren </summary>
	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("actionCalls");
			childrenNames.Add("matchClassFilter");
			return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		matchClassFilterFunctions = new CollectNode<MatchTypeQualIdentNode>();
		foreach(BaseNode matchClassFilterFunctionUnresolved in matchClassFilterFunctionsUnresolved.ChildrenExact)
		{
			matchClassFilterFunctions.AddChild((MatchTypeQualIdentNode)matchClassFilterFunctionUnresolved);
		}

		return true;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		return true; // only checking of children necessary
	}

	/// <summary>
	/// check after the IR is built </summary>
	protected internal virtual bool CheckPost()
	{
		bool res = true;

		// all actions must implement the match classes of the employed filters
		foreach(MatchTypeQualIdentNode matchClassFilterReference in matchClassFilterFunctions.ChildrenExact)
		{
			MatchClassFilterFunctionDeclNode matchClassFilter =
					(MatchClassFilterFunctionDeclNode)matchClassFilterReference.Member;
			string matchClassReferencedByFilterFunction = matchClassFilter.matchType.Ident.ToString();

			foreach(CallActionNode actionCall in actionCalls.ChildrenExact)
			{
				CheckWhetherCalledActionImplementsMatchClass(matchClassReferencedByFilterFunction, matchClassFilter,
						actionCall);
			}
		}

		return res;
	}

	public static void CheckWhetherCalledActionImplementsMatchClass(string matchClassReferencedByFilterFunction,
			MatchClassFilterFunctionDeclNode filterFunction, CallActionNode actionCall)
	{
		bool isMatchClassOfFilterImplementedByAction = false;
		foreach(DefinedMatchTypeNode matchType in actionCall.Action.ImplementedMatchClasses)
		{
			string matchClassImplementedByAction = matchType.Ident.ToString();
			if(matchClassReferencedByFilterFunction.Equals(matchClassImplementedByAction))
				isMatchClassOfFilterImplementedByAction = true;
		}

		if(!isMatchClassOfFilterImplementedByAction)
		{
			StringBuilder matchClassesImplementedByAction = new StringBuilder();
			if(actionCall.Action.ImplementedMatchClasses.Count == 0)
				matchClassesImplementedByAction.Append("no match classes");
			else
			{
				bool first = true;
				foreach(DefinedMatchTypeNode matchType in actionCall.Action.ImplementedMatchClasses)
				{
					string matchTypeNameImplementedByAction = matchType.TypeName;
					if(first)
						first = false;
					else
						matchClassesImplementedByAction.Append(",");
					matchClassesImplementedByAction.Append(matchTypeNameImplementedByAction);
				}
			}

			// TODO: print coordinates of match class, requires input of match class type instead of only string
			if(filterFunction != null)
			{
				actionCall.ReportError("The called filter function " + filterFunction.ToStringWithDeclarationCoords()
						+ " is defined for match class " + matchClassReferencedByFilterFunction + "."
						+ " The action " + actionCall.Action.ToStringWithDeclarationCoords()
						+ " it is applied on does not implement the match class"
						+ " (it implements " + matchClassesImplementedByAction + ").");
			}
			else
			{
				actionCall.ReportError("The multi rule query is defined to return match class " + matchClassReferencedByFilterFunction + "."
						+ " The action " + actionCall.Action.ToStringWithDeclarationCoords()
						+ " called in the multi rule query does not implement the match class"
						+ " (it implements " + matchClassesImplementedByAction + ").");
			}
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
	protected internal override IR ConstructIR()
	{
		Debug.Assert(false);
		return Bad.BadObject;
	}
}

}
