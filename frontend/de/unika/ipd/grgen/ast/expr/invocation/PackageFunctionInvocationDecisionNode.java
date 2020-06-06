/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.expr.numeric.AbsExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.ArcSinCosTanExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.ByteMaxExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.ByteMinExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.CeilExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.DoubleMaxExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.DoubleMinExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.EExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.FloatMaxExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.FloatMinExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.FloorExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.IntMaxExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.IntMinExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.LogExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.LongMaxExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.LongMinExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.MaxExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.MinExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.PiExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.PowExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.RoundExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.SgnExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.ShortMaxExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.ShortMinExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.SinCosTanExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.SqrExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.SqrtExprNode;
import de.unika.ipd.grgen.ast.expr.numeric.TruncateExprNode;
import de.unika.ipd.grgen.ast.expr.procenv.ExistsFileExprNode;
import de.unika.ipd.grgen.ast.expr.procenv.ImportExprNode;
import de.unika.ipd.grgen.ast.expr.procenv.NowExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.FunctionTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.ParserEnvironment;

public class PackageFunctionInvocationDecisionNode extends FunctionInvocationBaseNode
{
	static {
		setName(FunctionInvocationDecisionNode.class, "package function invocation decision expression");
	}

	static TypeNode functionTypeNode = new FunctionTypeNode();

	public String package_;
	public IdentNode functionIdent;
	private BuiltinFunctionInvocationBaseNode result;

	ParserEnvironment env;

	public PackageFunctionInvocationDecisionNode(String package_, IdentNode functionIdent,
			CollectNode<ExprNode> arguments, ParserEnvironment env)
	{
		super(functionIdent.getCoords(), arguments);
		this.package_ = package_;
		this.functionIdent = becomeParent(functionIdent);
		this.env = env;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		//children.add(methodIdent);	// HACK: We don't have a declaration, so avoid failure during check phase
		children.add(arguments);
		if(isResolved())
			children.add(result);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		//childrenNames.add("methodIdent");
		childrenNames.add("params");
		if(isResolved())
			childrenNames.add("result");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		result = decide(package_, functionIdent.toString());
		return result != null;
	}
	
	private BuiltinFunctionInvocationBaseNode decide(String package_, String functionName)
	{
		switch(package_ + "::" + functionName) {
		case "Math::min":
			if(arguments.size() != 2) {
				reportError("Math::min(.,.) takes two parameters.");
				return null;
			} else
				return new MinExprNode(getCoords(), arguments.get(0), arguments.get(1));
		case "Math::max":
			if(arguments.size() != 2) {
				reportError("Math::max(.,.) takes two parameters.");
				return null;
			} else
				return new MaxExprNode(getCoords(), arguments.get(0), arguments.get(1));
		case "Math::sin":
			if(arguments.size() != 1) {
				reportError("Math::sin(.) takes one parameter.");
				return null;
			} else {
				return new SinCosTanExprNode(getCoords(), SinCosTanExprNode.TrigonometryFunctionType.sin,
						arguments.get(0));
			}
		case "Math::cos":
			if(arguments.size() != 1) {
				reportError("Math::cos(.) takes one parameter.");
				return null;
			} else {
				return new SinCosTanExprNode(getCoords(), SinCosTanExprNode.TrigonometryFunctionType.cos,
						arguments.get(0));
			}
		case "Math::tan":
			if(arguments.size() != 1) {
				reportError("Math::tan(.) takes one parameter.");
				return null;
			} else {
				return new SinCosTanExprNode(getCoords(), SinCosTanExprNode.TrigonometryFunctionType.tan,
						arguments.get(0));
			}
		case "Math::arcsin":
			if(arguments.size() != 1) {
				reportError("Math::arcsin(.) takes one parameter.");
				return null;
			} else {
				return new ArcSinCosTanExprNode(getCoords(), ArcSinCosTanExprNode.ArcusTrigonometryFunctionType.arcsin,
						arguments.get(0));
			}
		case "Math::arccos":
			if(arguments.size() != 1) {
				reportError("Math::arccos(.) takes one parameter.");
				return null;
			} else {
				return new ArcSinCosTanExprNode(getCoords(), ArcSinCosTanExprNode.ArcusTrigonometryFunctionType.arccos,
						arguments.get(0));
			}
		case "Math::arctan":
			if(arguments.size() != 1) {
				reportError("Math::arctan(.) takes one parameter.");
				return null;
			} else {
				return new ArcSinCosTanExprNode(getCoords(), ArcSinCosTanExprNode.ArcusTrigonometryFunctionType.arctan,
						arguments.get(0));
			}
		case "Math::sqr":
			if(arguments.size() == 1)
				return new SqrExprNode(getCoords(), arguments.get(0));
			else {
				reportError("Math::sqr(.) takes one parameter.");
				return null;
			}
		case "Math::sqrt":
			if(arguments.size() == 1)
				return new SqrtExprNode(getCoords(), arguments.get(0));
			else {
				reportError("Math::sqrt(.) takes one parameter.");
				return null;
			}
		case "Math::pow":
			if(arguments.size() == 2)
				return new PowExprNode(getCoords(), arguments.get(0), arguments.get(1));
			else if(arguments.size() == 1)
				return new PowExprNode(getCoords(), arguments.get(0));
			else {
				reportError("Math::pow(.,.)/Math::pow(.) takes one or two parameters (one means base e).");
				return null;
			}
		case "Math::log":
			if(arguments.size() == 2)
				return new LogExprNode(getCoords(), arguments.get(0), arguments.get(1));
			else if(arguments.size() == 1)
				return new LogExprNode(getCoords(), arguments.get(0));
			else {
				reportError("Math::log(.,.)/Math::log(.) takes one or two parameters (one means base e).");
				return null;
			}
		case "Math::abs":
			if(arguments.size() != 1) {
				reportError("Math::abs(.) takes one parameter.");
				return null;
			} else
				return new AbsExprNode(getCoords(), arguments.get(0));
		case "Math::pi":
			if(arguments.size() != 0) {
				reportError("Math::pi() takes no parameters.");
				return null;
			} else
				return new PiExprNode(getCoords());
		case "Math::e":
			if(arguments.size() != 0) {
				reportError("Math::e() takes no parameters.");
				return null;
			} else
				return new EExprNode(getCoords());
		case "Math::byteMin":
			if(arguments.size() != 0) {
				reportError("Math::byteMin() takes no parameters.");
				return null;
			} else
				return new ByteMinExprNode(getCoords());
		case "Math::byteMax":
			if(arguments.size() != 0) {
				reportError("Math::byteMax() takes no parameters.");
				return null;
			} else
				return new ByteMaxExprNode(getCoords());
		case "Math::shortMin":
			if(arguments.size() != 0) {
				reportError("Math::shortMin() takes no parameters.");
				return null;
			} else
				return new ShortMinExprNode(getCoords());
		case "Math::shortMax":
			if(arguments.size() != 0) {
				reportError("Math::shortMax() takes no parameters.");
				return null;
			} else
				return new ShortMaxExprNode(getCoords());
		case "Math::intMin":
			if(arguments.size() != 0) {
				reportError("Math::intMin() takes no parameters.");
				return null;
			} else
				return new IntMinExprNode(getCoords());
		case "Math::intMax":
			if(arguments.size() != 0) {
				reportError("Math::intMax() takes no parameters.");
				return null;
			} else
				return new IntMaxExprNode(getCoords());
		case "Math::longMin":
			if(arguments.size() != 0) {
				reportError("Math::longMin() takes no parameters.");
				return null;
			} else
				return new LongMinExprNode(getCoords());
		case "Math::longMax":
			if(arguments.size() != 0) {
				reportError("Math::longMax() takes no parameters.");
				return null;
			} else
				return new LongMaxExprNode(getCoords());
		case "Math::floatMin":
			if(arguments.size() != 0) {
				reportError("Math::floatMin() takes no parameters.");
				return null;
			} else
				return new FloatMinExprNode(getCoords());
		case "Math::floatMax":
			if(arguments.size() != 0) {
				reportError("Math::floatMax() takes no parameters.");
				return null;
			} else
				return new FloatMaxExprNode(getCoords());
		case "Math::doubleMin":
			if(arguments.size() != 0) {
				reportError("Math::doubleMin() takes no parameters.");
				return null;
			} else
				return new DoubleMinExprNode(getCoords());
		case "Math::doubleMax":
			if(arguments.size() != 0) {
				reportError("Math::doubleMax() takes no parameters.");
				return null;
			} else
				return new DoubleMaxExprNode(getCoords());
		case "Math::ceil":
			if(arguments.size() != 1) {
				reportError("Math::ceil(.) takes one parameter.");
				return null;
			} else
				return new CeilExprNode(getCoords(), arguments.get(0));
		case "Math::floor":
			if(arguments.size() != 1) {
				reportError("Math::floor(.) takes one parameter.");
				return null;
			} else
				return new FloorExprNode(getCoords(), arguments.get(0));
		case "Math::round":
			if(arguments.size() != 1) {
				reportError("Math::round(.) takes one parameter.");
				return null;
			} else
				return new RoundExprNode(getCoords(), arguments.get(0));
		case "Math::truncate":
			if(arguments.size() != 1) {
				reportError("Math::truncate(.) takes one parameter.");
				return null;
			} else
				return new TruncateExprNode(getCoords(), arguments.get(0));
		case "Math::sgn":
			if(arguments.size() != 1) {
				reportError("Math::sgn(.) takes one parameter.");
				return null;
			} else
				return new SgnExprNode(getCoords(), arguments.get(0));
		case "Time::now":
			if(arguments.size() > 0) {
				reportError("Time::now() takes no parameters.");
				return null;
			} else
				return new NowExprNode(getCoords());
		case "File::exists":
			if(arguments.size() != 1) {
				reportError("File::exists(.) takes one parameter.");
				return null;
			} else
				return new ExistsFileExprNode(getCoords(), arguments.get(0));
		case "File::import":
			if(arguments.size() != 1) {
				reportError("File::import(.) takes one parameter.");
				return null;
			} else
				return new ImportExprNode(getCoords(), arguments.get(0));
		default:
			reportError("no function " + functionName + " known");
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
