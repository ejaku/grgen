/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.stmt
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using IR = de.unika.ipd.grgen.ir.IR;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class EvalStatementsNode : BaseNode
	{
		public string name;
		public CollectNode<EvalStatementNode> evalStatements;

		public EvalStatementsNode(Coords coords, string name)
			: base(coords)
		{
			this.name = name;
			evalStatements = new CollectNode<EvalStatementNode>();
		}

		public virtual void AddChild(EvalStatementNode evalStatement)
		{
			//assert(c!=null);
			evalStatements.AddChild(evalStatement);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				return evalStatements.Children;
			}
		}

		public virtual ICollection<EvalStatementNode> ChildrenExact
		{
			get
			{
				return evalStatements.ChildrenExact;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> res = new List<string>();
				for(int i = 0; i < Children.Count; ++i)
					res.Add("eval" + i);
				return res;
			}
		}

		protected internal override bool ResolveLocal()
		{
			return true;
		}

		protected internal override bool CheckLocal()
		{
			return true;
		}

		public virtual bool NoExecStatement()
		{
			bool res = true;
			foreach(EvalStatementNode evalStatement in evalStatements.ChildrenExact)
				res &= evalStatement.NoExecStatement(false);
			return res;
		}

		protected internal override IR ConstructIR()
		{
			if(IsIRAlreadySet())
				return (EvalStatements)IR;

			EvalStatements es = new EvalStatements(name);

			IR = es;

			foreach(EvalStatementNode evalStatement in evalStatements.ChildrenExact)
				es.evalStatements.Add(evalStatement.CheckIR<EvalStatement>(typeof(EvalStatement)));

			return es;
		}
	}

}
