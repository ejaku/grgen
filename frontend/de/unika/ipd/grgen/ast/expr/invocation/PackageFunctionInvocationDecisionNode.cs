/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.invocation
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using AbsExprNode = de.unika.ipd.grgen.ast.expr.numeric.AbsExprNode;
	using ArcSinCosTanExprNode = de.unika.ipd.grgen.ast.expr.numeric.ArcSinCosTanExprNode;
	using ByteMaxExprNode = de.unika.ipd.grgen.ast.expr.numeric.ByteMaxExprNode;
	using ByteMinExprNode = de.unika.ipd.grgen.ast.expr.numeric.ByteMinExprNode;
	using CeilExprNode = de.unika.ipd.grgen.ast.expr.numeric.CeilExprNode;
	using DoubleMaxExprNode = de.unika.ipd.grgen.ast.expr.numeric.DoubleMaxExprNode;
	using DoubleMinExprNode = de.unika.ipd.grgen.ast.expr.numeric.DoubleMinExprNode;
	using EExprNode = de.unika.ipd.grgen.ast.expr.numeric.EExprNode;
	using FloatMaxExprNode = de.unika.ipd.grgen.ast.expr.numeric.FloatMaxExprNode;
	using FloatMinExprNode = de.unika.ipd.grgen.ast.expr.numeric.FloatMinExprNode;
	using FloorExprNode = de.unika.ipd.grgen.ast.expr.numeric.FloorExprNode;
	using IntMaxExprNode = de.unika.ipd.grgen.ast.expr.numeric.IntMaxExprNode;
	using IntMinExprNode = de.unika.ipd.grgen.ast.expr.numeric.IntMinExprNode;
	using LogExprNode = de.unika.ipd.grgen.ast.expr.numeric.LogExprNode;
	using LongMaxExprNode = de.unika.ipd.grgen.ast.expr.numeric.LongMaxExprNode;
	using LongMinExprNode = de.unika.ipd.grgen.ast.expr.numeric.LongMinExprNode;
	using MaxExprNode = de.unika.ipd.grgen.ast.expr.numeric.MaxExprNode;
	using MinExprNode = de.unika.ipd.grgen.ast.expr.numeric.MinExprNode;
	using PiExprNode = de.unika.ipd.grgen.ast.expr.numeric.PiExprNode;
	using PowExprNode = de.unika.ipd.grgen.ast.expr.numeric.PowExprNode;
	using RoundExprNode = de.unika.ipd.grgen.ast.expr.numeric.RoundExprNode;
	using SgnExprNode = de.unika.ipd.grgen.ast.expr.numeric.SgnExprNode;
	using ShortMaxExprNode = de.unika.ipd.grgen.ast.expr.numeric.ShortMaxExprNode;
	using ShortMinExprNode = de.unika.ipd.grgen.ast.expr.numeric.ShortMinExprNode;
	using SinCosTanExprNode = de.unika.ipd.grgen.ast.expr.numeric.SinCosTanExprNode;
	using SqrExprNode = de.unika.ipd.grgen.ast.expr.numeric.SqrExprNode;
	using SqrtExprNode = de.unika.ipd.grgen.ast.expr.numeric.SqrtExprNode;
	using TruncateExprNode = de.unika.ipd.grgen.ast.expr.numeric.TruncateExprNode;
	using ExistsFileExprNode = de.unika.ipd.grgen.ast.expr.procenv.ExistsFileExprNode;
	using ImportExprNode = de.unika.ipd.grgen.ast.expr.procenv.ImportExprNode;
	using NowExprNode = de.unika.ipd.grgen.ast.expr.procenv.NowExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using FunctionTypeNode = de.unika.ipd.grgen.ast.type.executable.FunctionTypeNode;
	using ResolvingEnvironment = de.unika.ipd.grgen.ast.util.ResolvingEnvironment;
	using IR = de.unika.ipd.grgen.ir.IR;
	using ParserEnvironment = de.unika.ipd.grgen.parser.ParserEnvironment;

	public class PackageFunctionInvocationDecisionNode : FunctionInvocationBaseNode
	{
		static PackageFunctionInvocationDecisionNode()
		{
			SetClassName(typeof(FunctionInvocationDecisionNode), "package function invocation decision expression");
		}

		internal static TypeNode functionTypeNode = new FunctionTypeNode();

		public string package_;
		public IdentNode functionIdent;
		private BuiltinFunctionInvocationBaseNode result;

		internal ParserEnvironment env;

		public PackageFunctionInvocationDecisionNode(string package_, IdentNode functionIdent,
				CollectNode<ExprNode> arguments, ParserEnvironment env)
			: base(functionIdent.Coords, arguments)
		{
			this.package_ = package_;
			this.functionIdent = BecomeParent(functionIdent);
			this.env = env;
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
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
				//childrenNames.add("methodIdent");
				childrenNames.Add("params");
				if(IsResolved())
					childrenNames.Add("result");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			ResolvingEnvironment resolvingEnvironment = new ResolvingEnvironment(null, error, Coords);
			result = Decide(package_ + "::" + functionIdent.ToString(), arguments, resolvingEnvironment);
			return result != null;
		}

		private static BuiltinFunctionInvocationBaseNode Decide(string functionName, CollectNode<ExprNode> arguments,
				ResolvingEnvironment env)
		{
			switch(functionName)
			{
			case "Math::min":
				if(arguments.Size() != 2)
				{
					env.ReportError("Math::min() expects 2 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new MinExprNode(env.Coords, arguments.Get(0), arguments.Get(1));
			case "Math::max":
				if(arguments.Size() != 2)
				{
					env.ReportError("Math::max() expects 2 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new MaxExprNode(env.Coords, arguments.Get(0), arguments.Get(1));
			case "Math::sin":
				if(arguments.Size() != 1)
				{
					env.ReportError("Math::sin() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
				{
					return new SinCosTanExprNode(env.Coords, SinCosTanExprNode.TrigonometryFunctionType.sin,
							arguments.Get(0));
				}
			case "Math::cos":
				if(arguments.Size() != 1)
				{
					env.ReportError("Math::cos() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
				{
					return new SinCosTanExprNode(env.Coords, SinCosTanExprNode.TrigonometryFunctionType.cos,
							arguments.Get(0));
				}
			case "Math::tan":
				if(arguments.Size() != 1)
				{
					env.ReportError("Math::tan() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
				{
					return new SinCosTanExprNode(env.Coords, SinCosTanExprNode.TrigonometryFunctionType.tan,
							arguments.Get(0));
				}
			case "Math::arcsin":
				if(arguments.Size() != 1)
				{
					env.ReportError("Math::arcsin() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
				{
					return new ArcSinCosTanExprNode(env.Coords, ArcSinCosTanExprNode.ArcusTrigonometryFunctionType.arcsin,
							arguments.Get(0));
				}
			case "Math::arccos":
				if(arguments.Size() != 1)
				{
					env.ReportError("Math::arccos() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
				{
					return new ArcSinCosTanExprNode(env.Coords, ArcSinCosTanExprNode.ArcusTrigonometryFunctionType.arccos,
							arguments.Get(0));
				}
			case "Math::arctan":
				if(arguments.Size() != 1)
				{
					env.ReportError("Math::arctan() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
				{
					return new ArcSinCosTanExprNode(env.Coords, ArcSinCosTanExprNode.ArcusTrigonometryFunctionType.arctan,
							arguments.Get(0));
				}
			case "Math::sqr":
				if(arguments.Size() == 1)
					return new SqrExprNode(env.Coords, arguments.Get(0));
				else
				{
					env.ReportError("Math::sqr() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "Math::sqrt":
				if(arguments.Size() == 1)
					return new SqrtExprNode(env.Coords, arguments.Get(0));
				else
				{
					env.ReportError("Math::sqrt() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "Math::pow":
				if(arguments.Size() == 2)
					return new PowExprNode(env.Coords, arguments.Get(0), arguments.Get(1));
				else if(arguments.Size() == 1)
					return new PowExprNode(env.Coords, arguments.Get(0));
				else
				{
					env.ReportError("Math::pow() expects 1 or 2 arguments (one means base e) (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "Math::log":
				if(arguments.Size() == 2)
					return new LogExprNode(env.Coords, arguments.Get(0), arguments.Get(1));
				else if(arguments.Size() == 1)
					return new LogExprNode(env.Coords, arguments.Get(0));
				else
				{
					env.ReportError("Math::log() expects 1 or 2 arguments (one means base e) (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "Math::abs":
				if(arguments.Size() != 1)
				{
					env.ReportError("Math::abs() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new AbsExprNode(env.Coords, arguments.Get(0));
			case "Math::pi":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::pi() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new PiExprNode(env.Coords);
			case "Math::e":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::e() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new EExprNode(env.Coords);
			case "Math::byteMin":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::byteMin() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new ByteMinExprNode(env.Coords);
			case "Math::byteMax":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::byteMax() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new ByteMaxExprNode(env.Coords);
			case "Math::shortMin":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::shortMin() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new ShortMinExprNode(env.Coords);
			case "Math::shortMax":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::shortMax() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new ShortMaxExprNode(env.Coords);
			case "Math::intMin":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::intMin() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new IntMinExprNode(env.Coords);
			case "Math::intMax":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::intMax() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new IntMaxExprNode(env.Coords);
			case "Math::longMin":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::longMin() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new LongMinExprNode(env.Coords);
			case "Math::longMax":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::longMax() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new LongMaxExprNode(env.Coords);
			case "Math::floatMin":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::floatMin() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new FloatMinExprNode(env.Coords);
			case "Math::floatMax":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::floatMax() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new FloatMaxExprNode(env.Coords);
			case "Math::doubleMin":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::doubleMin() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new DoubleMinExprNode(env.Coords);
			case "Math::doubleMax":
				if(arguments.Size() != 0)
				{
					env.ReportError("Math::doubleMax() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new DoubleMaxExprNode(env.Coords);
			case "Math::ceil":
				if(arguments.Size() != 1)
				{
					env.ReportError("Math::ceil() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new CeilExprNode(env.Coords, arguments.Get(0));
			case "Math::floor":
				if(arguments.Size() != 1)
				{
					env.ReportError("Math::floor() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new FloorExprNode(env.Coords, arguments.Get(0));
			case "Math::round":
				if(arguments.Size() != 1)
				{
					env.ReportError("Math::round() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new RoundExprNode(env.Coords, arguments.Get(0));
			case "Math::truncate":
				if(arguments.Size() != 1)
				{
					env.ReportError("Math::truncate() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new TruncateExprNode(env.Coords, arguments.Get(0));
			case "Math::sgn":
				if(arguments.Size() != 1)
				{
					env.ReportError("Math::sgn() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new SgnExprNode(env.Coords, arguments.Get(0));
			case "Time::now":
				if(arguments.Size() > 0)
				{
					env.ReportError("Time::now() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new NowExprNode(env.Coords);
			case "File::exists":
				if(arguments.Size() != 1)
				{
					env.ReportError("File::exists() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new ExistsFileExprNode(env.Coords, arguments.Get(0));
			case "File::import":
				if(arguments.Size() != 1)
				{
					env.ReportError("File::import() expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new ImportExprNode(env.Coords, arguments.Get(0));
			default:
				env.ReportError("A function of package::name " + functionName + " is not known."); // TODO: complain about wrong package, then wrong name
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

		public virtual ExprNode Result
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
