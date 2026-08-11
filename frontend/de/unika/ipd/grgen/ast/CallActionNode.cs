/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>

namespace de.unika.ipd.grgen.ast
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ExecVarDeclNode = de.unika.ipd.grgen.ast.decl.ExecVarDeclNode;
	using ActionDeclNode = de.unika.ipd.grgen.ast.decl.executable.ActionDeclNode;
	using FilterFunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FilterFunctionDeclNode;
	using SequenceDeclNode = de.unika.ipd.grgen.ast.decl.executable.SequenceDeclNode;
	using EdgeInterfaceTypeChangeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeInterfaceTypeChangeDeclNode;
	using NodeInterfaceTypeChangeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeInterfaceTypeChangeDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using TypeTypeNode = de.unika.ipd.grgen.ast.type.basic.TypeTypeNode;
	using UntypedExecVarTypeNode = de.unika.ipd.grgen.ast.type.basic.UntypedExecVarTypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using Bad = de.unika.ipd.grgen.ir.Bad;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Coords = de.unika.ipd.grgen.parser.Coords;
	using Symbol = de.unika.ipd.grgen.parser.Symbol;

	// todo: the entire exec handling in the frontend is nothing but a dirty hack, clean this

	/// <summary>
	/// Call of an action with parameters and returns.
	/// </summary>
	public class CallActionNode : BaseNode
	{
		static CallActionNode()
		{
			SetClassName(typeof(CallActionNode), "call action");
		}

		private IdentNode actionUnresolved;

		private CollectNode<BaseNode> paramsUnresolved;
		private CollectNode<BaseNode> returnsUnresolved;
		private CollectNode<BaseNode> filterFunctionsUnresolved; // only IdentNode in CallActionNode

		private bool isAllBracketed;

		private ActionDeclNode action;
		private SequenceDeclNode sequence;
		private ExecVarDeclNode boolVar;

		public CollectNode<ExprNode> @params;
		protected internal CollectNode<ExecVarDeclNode> returns;
		protected internal CollectNode<FilterFunctionDeclNode> filterFunctions;

		/// <param name="ruleUnresolved">      an IdentNode: thr rule/test name </param>
		/// <param name="paramsUnresolved">    a  CollectNode<BaseNode> </param>
		/// <param name="returnsUnresolved">   a  CollectNode<BaseNode> </param>
		public CallActionNode(Coords coords, IdentNode ruleUnresolved, CollectNode<BaseNode> paramsUnresolved,
				CollectNode<BaseNode> returnsUnresolved, CollectNode<BaseNode> filterFunctionsUnresolved,
				bool isAllBracketed)
			: base(coords)
		{
			this.actionUnresolved = ruleUnresolved;
			this.paramsUnresolved = paramsUnresolved;
			this.returnsUnresolved = returnsUnresolved;
			this.filterFunctionsUnresolved = filterFunctionsUnresolved;
			this.isAllBracketed = isAllBracketed;
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(actionUnresolved, action, sequence, boolVar));
				children.Add(GetValidVersionCollectNode(paramsUnresolved, @params));
				children.Add(GetValidVersionCollectNode(returnsUnresolved, returns));
				children.Add(GetValidVersionCollectNode(filterFunctionsUnresolved, filterFunctions));
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
				childrenNames.Add("action");
				childrenNames.Add("params");
				childrenNames.Add("returns");
				childrenNames.Add("filter");
				return childrenNames;
			}
		}

		/// <summary>
		/// Returns Params
		/// </summary>
		/// <returns>    a  CollectNode<IdentNode> </returns>
		protected internal virtual CollectNode<ExprNode> Params
		{
			get
			{
				Debug.Assert(IsResolved());
				return @params;
			}
		}

		public virtual ActionDeclNode Action
		{
			get
			{
				return action;
			}
		}

		/*
		 * This introduces an ExecVar definition if an identifier is not defined
		 * to support the usage-is-definition policy of the graph global variables in the sequences.
		 * Note: an (x)=r() & (x:A)=r() error will not be found due to the grgen symbol table and the fixupDefinition
		 * not taking care of the position of the definition compared to the uses
		 * (which makes sense for every other construct of the grgen language);
		 * this error will be caught later on when the xgrs is processed by the libgr sequence parser and symbol table.
		 */
		public virtual void AddImplicitDefinitions()
		{
			for(int i = 0; i < returnsUnresolved.Size(); ++i)
			{
				if(!(returnsUnresolved.Get(i) is IdentNode))
					continue;
				IdentNode id = (IdentNode)returnsUnresolved.Get(i);

				debug.Report(NOTE, "Implicit definition for " + id + " in scope " + Scope);

				// Get the definition of the ident's symbol local to the owned scope.
				Symbol.Definition def = Scope.GetCurrDef(id.Symbol);
				debug.Report(NOTE, "definition is: " + def);

				// If this definition is valid, i.e. it exists, it will be used
				// else, an ExecVarDeclNode of this name is added to the scope
				if(def.IsValid())
					id.SymDef = def;
				else
				{
					Symbol.Definition vdef = Scope.Define(id.Symbol, id.Coords);
					id.SymDef = vdef;
					vdef.Node = id;
					Scope.LeaveScope();
					ExecVarDeclNode evd = new ExecVarDeclNode(id, BasicTypeNode.untypedType);
					id.Decl = evd;
					returnsUnresolved.Set(i, evd);
				}
			}
		}

		private static readonly DeclarationTripleResolver<ActionDeclNode, SequenceDeclNode, ExecVarDeclNode> actionResolver =
			new DeclarationTripleResolver<ActionDeclNode, SequenceDeclNode, ExecVarDeclNode>(
					typeof(ActionDeclNode), typeof(SequenceDeclNode), typeof(ExecVarDeclNode));

		private static readonly CollectResolver<ExprNode> paramNodeResolver =
			new CollectResolver<ExprNode>(new DeclarationResolver<ExprNode>(typeof(ExprNode)));

		private static readonly CollectResolver<ExecVarDeclNode> varDeclNodeResolver =
			new CollectResolver<ExecVarDeclNode>(new DeclarationResolver<ExecVarDeclNode>(typeof(ExecVarDeclNode)));

		private static readonly CollectResolver<FilterFunctionDeclNode> filterResolver =
			new CollectResolver<FilterFunctionDeclNode>(new DeclarationResolver<FilterFunctionDeclNode>(
					typeof(FilterFunctionDeclNode)));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = true;
			AddImplicitDefinitions();
			if(!(actionUnresolved is PackageIdentNode))
				FixupDefinition(actionUnresolved, actionUnresolved.Scope);

			Triple<ActionDeclNode, SequenceDeclNode, ExecVarDeclNode> resolved = actionResolver.Resolve(actionUnresolved, this);
			if(resolved != null)
			{
				if(resolved.first != null)
					action = resolved.first;
				else if(resolved.second != null)
					sequence = resolved.second;
				else
					boolVar = resolved.third;
			}

			successfullyResolved &= resolved != null && (action != null || sequence != null || boolVar != null);

			if(action != null)
			{
				foreach(BaseNode filterFunctionUnresolved in filterFunctionsUnresolved.ChildrenExact)
				{
					if(!(filterFunctionUnresolved is PackageIdentNode))
					{
						if(!TryFixupDefinition(filterFunctionUnresolved, action.Scope.Parent))
							FixupDefinition(filterFunctionUnresolved, filterFunctionUnresolved.Scope);
					}
				}
			}

			@params = paramNodeResolver.Resolve(paramsUnresolved, this);
			successfullyResolved &= @params != null;

			returns = varDeclNodeResolver.Resolve(returnsUnresolved, this);
			successfullyResolved &= returns != null;

			filterFunctions = filterResolver.Resolve(filterFunctionsUnresolved, this);
			successfullyResolved &= filterFunctions != null;

			return successfullyResolved;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			bool res = true;

			/* cannot be checked here, because type info is not yet computed
			 res &= checkParams(action.getParamDecls(), params.getChildrenExact());
			 res &= checkReturns(action.returnFormalParameters, returns);
			 */

			return res;
		}

		/// <summary>
		/// check after the IR is built </summary>
		protected internal virtual bool CheckPost()
		{
			bool res = true;

			if(action != null)
			{
				res &= CheckParams(action.pattern.ParamDecls, @params.ChildrenExact);
				res &= CheckReturns(action.returnFormalParameters.ChildrenExact, returns);
			}
			else if(sequence != null)
			{
				IList<TypeNode> outTypes = new List<TypeNode>();
				foreach(ExecVarDeclNode varDecl in sequence.outParams.ChildrenExact)
					outTypes.Add(varDecl.DeclType);
				res &= CheckParams(sequence.ParamDecls, @params.ChildrenExact);
				res &= CheckReturns(outTypes, returns);
			}

			if(action != null)
			{
				foreach(FilterFunctionDeclNode filter in filterFunctions.ChildrenExact)
				{
					if(filter.action != action)
					{
						ReportError("The filter " + filter.ToStringWithDeclarationCoords()
								+ " is defined for the action " + filter.action.ToStringWithDeclarationCoords() + "."
								+ " It cannot be applied to the action " + action.ToStringWithDeclarationCoords() + ".");
					}
				}
			}
			else
			{
				if(filterFunctionsUnresolved.Size() > 0)
					ReportError("Match filters can only be applied to tests or rules (but not to " + actionUnresolved + ").");
			}

			return res;
		}

		/// <summary>
		/// Method checkParams </summary>
		/// <param name="formalParams">        a  Collection<? extends DeclNode> </param>
		/// <param name="actualParams">        a  Collection<? extends DeclNode> </param>
		/// <returns>   a  boolean </returns>
		private bool CheckParams(ICollection<DeclNode> formalParams,
				ICollection<ExprNode> actualParams)
		{
			if(formalParams.Count != actualParams.Count)
			{
				ReportError("The " + (action != null ? action.Kind + " " + action.ToStringWithDeclarationCoords() : sequence.Kind + " " + sequence.ToStringWithDeclarationCoords())
						+ " expects " + formalParams.Count + " arguments,"
						+ " but is given " + actualParams.Count + " arguments.");
				return false;
			}

			bool res = true;
			if(actualParams.Count > 0)
			{
				IEnumerator<ExprNode> iterAP = actualParams.GetEnumerator();
				int paramCounter = 1;
				foreach(DeclNode formalParam in formalParams)
				{
	// JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
					ExprNode actualParam = iterAP.Next();

					res &= CheckParam(paramCounter, formalParam, actualParam);

					++paramCounter;
				}
			}
			return res;
		}

		private bool CheckParam(int paramPos, DeclNode formalParam, ExprNode actualParam)
		{
			TypeNode formalParameterType;
			if(formalParam is EdgeInterfaceTypeChangeDeclNode)
			{
				EdgeInterfaceTypeChangeDeclNode typeChangeFormalParam = (EdgeInterfaceTypeChangeDeclNode)formalParam;
				formalParameterType = typeChangeFormalParam.interfaceType.DeclType;
			}
			else if(formalParam is NodeInterfaceTypeChangeDeclNode)
			{
				NodeInterfaceTypeChangeDeclNode typeChangeFormalParam = (NodeInterfaceTypeChangeDeclNode)formalParam;
				formalParameterType = typeChangeFormalParam.interfaceType.DeclType;
			}
			else
				formalParameterType = formalParam.Decl.GetDeclType();

			TypeNode actualParameterType = actualParam.Type;

			if(actualParameterType is UntypedExecVarTypeNode)
				return true;

			if(actualParameterType is TypeTypeNode
					&& (formalParameterType is NodeTypeNode || formalParameterType is EdgeTypeNode))
				return true;

			if(!actualParameterType.IsCompatibleTo(formalParameterType))
			{
				string actionOrSequence;
				if(action != null)
					actionOrSequence = action.Kind + " " + action.ToStringWithDeclarationCoords();
				else
					actionOrSequence = sequence.Kind + " " + sequence.ToStringWithDeclarationCoords();
				ReportError("Cannot convert " + paramPos + ". argument"
						+ " from " + actualParameterType.TypeName
						+ " to the expected " + formalParameterType.TypeName
						+ " (when calling " + actionOrSequence + ")"
						+ actualParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
						+ formalParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
						+ ".");
				return false;
			}

			return true;
		}

		/// <summary>
		/// Method checkReturns </summary>
		/// <param name="formalReturns"> a  Collection<? extends TypeNode> </param>
		/// <param name="actualReturns"> a  CollectNode<ExecVarDeclNode> </param>
		/// <returns>   a  boolean </returns>
		private bool CheckReturns<T1>(ICollection<T1> formalReturns,
				CollectNode<ExecVarDeclNode> actualReturns) where T1 : de.unika.ipd.grgen.ast.type.TypeNode
		{
			// It is ok to have no actual returns, but if there are some, then they have to fit.
			if(actualReturns.Size() > 0 && formalReturns.Count != actualReturns.Size())
			{
				ReportError("The " + (action != null ? action.Kind + " " + action.ToStringWithDeclarationCoords() : sequence.Kind + " " + sequence.ToStringWithDeclarationCoords())
						+ " expects " + formalReturns.Count + " return arguments,"
						+ " but is given " + actualReturns.Size() + " return arguments.");
				return false;
			}

			bool res = true;
			if(actualReturns.Size() > 0)
			{
				IEnumerator<ExecVarDeclNode> iterAR = actualReturns.ChildrenExact.GetEnumerator();
				int returnPos = 0;
				foreach(TypeNode formalReturn in formalReturns)
				{
	// JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
					ExecVarDeclNode actualReturn = iterAR.Next();
					res &= CheckReturn(formalReturn, actualReturn, returnPos);
					++returnPos;
				}
			}
			return res;
		}

		private bool CheckReturn(TypeNode formalReturn, ExecVarDeclNode actualReturn, int returnPos)
		{
			TypeNode formalReturnType = formalReturn;
			TypeNode actualReturnType = actualReturn.Decl.GetDeclType();

			if(actualReturnType is UntypedExecVarTypeNode)
				return true;

			bool incommensurable = false;

			if(isAllBracketed)
			{
				if(!(actualReturnType is ArrayTypeNode))
					incommensurable = true;
				else
				{
					ArrayTypeNode arrayType = (ArrayTypeNode)actualReturnType;
					if(!formalReturnType.IsCompatibleTo(arrayType.valueType))
						incommensurable = true;
				}
			}
			else if(!formalReturnType.IsCompatibleTo(actualReturnType))
				incommensurable = true;

			if(incommensurable)
			{
				string actionOrSequence;
				if(action != null)
					actionOrSequence = action.Kind + " " + action.ToStringWithDeclarationCoords();
				else
					actionOrSequence = sequence.Kind + " " + sequence.ToStringWithDeclarationCoords();
				ReportError("Cannot assign " + (returnPos + 1) + ". return argument of type " + formalReturnType.TypeName
						+ (isAllBracketed ? " (array<" + formalReturnType.TypeName + ">)" : "")
						+ " to a variable " + actualReturn + " of type " + actualReturnType.TypeName
						+ " (when calling " + actionOrSequence + ")"
						+ formalReturnType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
						+ actualReturnType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
						+ ".");
				return false;
			}

			return true;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
		protected internal override IR ConstructIR()
		{
			Debug.Assert(false);
			return Bad.BadObject; // TODO fix this
		}
	}

}
