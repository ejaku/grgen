/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using DefinedMatchTypeNode = de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
	using MatchTypeActionNode = de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
	using MatchTypeNode = de.unika.ipd.grgen.ast.type.MatchTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using MatchesAccumulationYield = de.unika.ipd.grgen.ir.stmt.MatchesAccumulationYield;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node representing an accumulation yielding of a matches variable.
	/// </summary>
	public class MatchesAccumulationYieldNode : NestingStatementNode
	{
		static MatchesAccumulationYieldNode()
		{
			SetClassName(typeof(MatchesAccumulationYieldNode), "MatchesAccumulationYield");
		}

		internal VarDeclNode iterationVariableUnresolved;
		internal IdentNode matchesContainerUnresolved;

		internal VarDeclNode iterationVariable;
		internal VarDeclNode matchesContainer;

		public MatchesAccumulationYieldNode(Coords coords, VarDeclNode iterationVariable, IdentNode matchesContainer,
				CollectNode<EvalStatementNode> accumulationStatements)
			: base(coords, accumulationStatements)
		{
			this.iterationVariableUnresolved = iterationVariable;
			BecomeParent(this.iterationVariableUnresolved);
			this.matchesContainerUnresolved = matchesContainer;
			BecomeParent(this.matchesContainerUnresolved);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(iterationVariableUnresolved, iterationVariable));
				children.Add(GetValidVersion(matchesContainerUnresolved, matchesContainer));
				children.Add(statements);
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
				childrenNames.Add("iterationVariable");
				childrenNames.Add("matchesContainer");
				childrenNames.Add("accumulationStatements");
				return childrenNames;
			}
		}

		private static readonly DeclarationResolver<VarDeclNode> matchesResolver =
				new DeclarationResolver<VarDeclNode>(typeof(VarDeclNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = true;

			/*if(!(matchesContainerUnresolved.toString().equals("this"))) {
				reportError("for matches loop expects to iterate the matches stored in the this object (of type array<match<rule-name>> or array<class match<class match-class-name>>)");
			}*/

			matchesContainer = matchesResolver.Resolve(matchesContainerUnresolved, this);
			if(matchesContainer == null)
				successfullyResolved = false;

			if(iterationVariableUnresolved is VarDeclNode)
				iterationVariable = (VarDeclNode)iterationVariableUnresolved;
			else
			{
				ReportError("Error in resolving the iteration variable of the for matches loop.");
				successfullyResolved = false;
			}

			if(!iterationVariable.Resolve())
				successfullyResolved = false;

			return successfullyResolved;
		}

		protected internal override bool CheckLocal()
		{
			TypeNode matchesContainerType = matchesContainer.DeclType;
			if(!(matchesContainerType is ArrayTypeNode))
			{
				ReportError("The for matches loop expects to iterate an array of matches (of type array<match<rule-name>> or array<match<class match-class-name>>), but is given: "
						+ matchesContainerType.ToStringWithDeclarationCoords());
				return false;
			}
			TypeNode matchesArrayValueType = ((ArrayTypeNode)matchesContainerType).valueType;

			MatchTypeActionNode matchesContainerActionMatchType = matchesArrayValueType is MatchTypeActionNode
					? (MatchTypeActionNode)matchesArrayValueType
					: null;
			DefinedMatchTypeNode matchesContainerDefinedMatchType = matchesArrayValueType is DefinedMatchTypeNode
					? (DefinedMatchTypeNode)matchesArrayValueType
					: null;
			MatchTypeNode matchesContainerMatchType = matchesContainerActionMatchType != null
					? (MatchTypeNode)matchesContainerActionMatchType : (MatchTypeNode)matchesContainerDefinedMatchType;
			if(matchesContainerActionMatchType == null && matchesContainerDefinedMatchType == null)
			{
				ReportError("The for matches loop expects to iterate an array of matches (of type array<match<rule-name>> or array<match<class match-class-name>>), but is given as array element type: "
						+ matchesArrayValueType.ToStringWithDeclarationCoords());
				return false;
			}

			TypeNode iterationVariableType = iterationVariable.DeclType;
			MatchTypeActionNode iterationVariableActionMatchType = iterationVariableType is MatchTypeActionNode
					? (MatchTypeActionNode)iterationVariableType
					: null;
			DefinedMatchTypeNode iterationVariableDefinedMatchType = iterationVariableType is DefinedMatchTypeNode
					? (DefinedMatchTypeNode)iterationVariableType
					: null;
			MatchTypeNode iterationVariableMatchType = iterationVariableActionMatchType != null
					? (MatchTypeNode)iterationVariableActionMatchType : (MatchTypeNode)iterationVariableDefinedMatchType;
			if(iterationVariableActionMatchType == null && iterationVariableDefinedMatchType == null)
			{
				ReportError("The for matches loop expects an iteration variable of match type (match<rule-name> or match<class match-class-name>), but is given: "
						+ iterationVariableType.ToStringWithDeclarationCoords());
				return false;
			}

			if(!iterationVariableMatchType.IsEqual(matchesContainerMatchType))
			{
				ReportError("The iteration variable of the for matches loop is of type " + iterationVariableMatchType.ToStringWithDeclarationCoords()
						+ " but the elements in the matches container are of type " + matchesContainerMatchType.ToStringWithDeclarationCoords() + ".");
				//"(defined by the rule referenced by the filter function)" "(defined by the match class referenced by the match class filter function)"
				return false;
			}

			return true;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			MatchesAccumulationYield may = new MatchesAccumulationYield(iterationVariable.CheckIR<Variable>(typeof(Variable)),
					matchesContainer.CheckIR<Variable>(typeof(Variable)));
			foreach(EvalStatementNode accumulationStatement in statements.ChildrenExact)
				may.AddStatement(accumulationStatement.CheckIR<EvalStatement>(typeof(EvalStatement)));
			return may;
		}
	}

}
