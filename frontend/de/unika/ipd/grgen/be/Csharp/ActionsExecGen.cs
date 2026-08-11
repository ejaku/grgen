/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Generates the exec representation for the SearchPlanBackend2 backend.
/// @author Edgar Jakumeit, Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.be.Csharp
{
	using System.Diagnostics;

	using de.unika.ipd.grgen.ir;
	using Procedure = de.unika.ipd.grgen.ir.executable.Procedure;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Alternative = de.unika.ipd.grgen.ir.pattern.Alternative;
	using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using CaseStatement = de.unika.ipd.grgen.ir.stmt.CaseStatement;
	using ConditionStatement = de.unika.ipd.grgen.ir.stmt.ConditionStatement;
	using ContainerAccumulationYield = de.unika.ipd.grgen.ir.stmt.ContainerAccumulationYield;
	using DoWhileStatement = de.unika.ipd.grgen.ir.stmt.DoWhileStatement;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
	using ExecStatement = de.unika.ipd.grgen.ir.stmt.ExecStatement;
	using ImperativeStmt = de.unika.ipd.grgen.ir.stmt.ImperativeStmt;
	using IntegerRangeIterationYield = de.unika.ipd.grgen.ir.stmt.IntegerRangeIterationYield;
	using MatchesAccumulationYield = de.unika.ipd.grgen.ir.stmt.MatchesAccumulationYield;
	using MultiStatement = de.unika.ipd.grgen.ir.stmt.MultiStatement;
	using SwitchStatement = de.unika.ipd.grgen.ir.stmt.SwitchStatement;
	using WhileStatement = de.unika.ipd.grgen.ir.stmt.WhileStatement;
	using ForFunction = de.unika.ipd.grgen.ir.stmt.graph.ForFunction;
	using SourceBuilder = de.unika.ipd.grgen.util.SourceBuilder;

	public class ActionsExecGen : CSharpBase
	{
		public ActionsExecGen(string nodeTypePrefix, string edgeTypePrefix, string objectTypePrefix, string transientObjectTypePrefix)
			: base(nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix)
		{
		}

		//////////////////////////////////////////
		// Imperative statement/exec generation //
		//////////////////////////////////////////

		public virtual void GenImperativeStatements(SourceBuilder sb, Rule rule, string pathPrefix, string packageName,
				bool isTopLevel, bool isSubpattern)
		{
			if(rule.Right == null)
				return;

			if(isTopLevel)
			{
				sb.Append("#if INITIAL_WARMUP\t\t// GrGen imperative statement section: "
						+ GetPackagePrefixDoubleColon(rule) + (isSubpattern ? "Pattern_" : "Rule_")
						+ FormatIdentifiable(rule) + "\n");
			}

			GenImperativeStatements(sb, rule, pathPrefix, packageName);

			PatternGraphLhs pattern = rule.Pattern;
			foreach(Alternative alt in pattern.Alts)
			{
				string altName = alt.NameOfGraph;
				foreach(Rule altCase in alt.AlternativeCases)
				{
					PatternGraphLhs altCasePattern = altCase.Left;
					GenImperativeStatements(sb, altCase,
							pathPrefix + altName + "_" + altCasePattern.NameOfGraph + "_", packageName,
							false, isSubpattern);
				}
			}

			foreach(Rule iter in pattern.Iters)
			{
				string iterName = iter.Left.NameOfGraph;
				GenImperativeStatements(sb, iter,
						pathPrefix + iterName + "_", packageName,
						false, isSubpattern);
			}

			if(isTopLevel)
				sb.Append("#endif\n");
		}

		private void GenImperativeStatements(SourceBuilder sb, Rule rule, string pathPrefix, string packageName)
		{
			int xgrsID = 0;
			foreach(EvalStatements evals in rule.Evals)
			{
				foreach(EvalStatement eval in evals.evalStatements)
					xgrsID = GenImperativeStatements(sb, rule, pathPrefix, packageName, eval, xgrsID);
			}
			foreach(ImperativeStmt istmt in rule.Right.ImperativeStmts)
			{
				if(istmt is Exec)
					xgrsID = GenExec(sb, pathPrefix, packageName, (Exec)istmt, xgrsID);
				else if(istmt is Emit)
				{
					// nothing to do
				}
				else
					Debug.Assert(false, "unknown ImperativeStmt: " + istmt + " in " + rule);
			}
		}

		private int GenImperativeStatements(SourceBuilder sb, Rule rule, string pathPrefix, string packageName,
				EvalStatement evalStmt, int xgrsID)
		{
			if(evalStmt is ConditionStatement)
			{
				ConditionStatement condStmt = (ConditionStatement)evalStmt;
				foreach(EvalStatement nestedEvalStmt in condStmt.Statements)
					xgrsID = GenImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
				if(condStmt.FalseCaseStatements != null)
				{
					foreach(EvalStatement nestedEvalStmt in condStmt.FalseCaseStatements)
						xgrsID = GenImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
				}
			}
			else if(evalStmt is SwitchStatement)
			{
				SwitchStatement switchStmt = (SwitchStatement)evalStmt;
				foreach(EvalStatement nestedEvalStmt in switchStmt.Statements)
					xgrsID = GenImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
			else if(evalStmt is CaseStatement)
			{
				CaseStatement caseStmt = (CaseStatement)evalStmt;
				foreach(EvalStatement nestedEvalStmt in caseStmt.Statements)
					xgrsID = GenImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
			else if(evalStmt is WhileStatement)
			{
				WhileStatement whileStmt = (WhileStatement)evalStmt;
				foreach(EvalStatement nestedEvalStmt in whileStmt.Statements)
					xgrsID = GenImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
			else if(evalStmt is DoWhileStatement)
			{
				DoWhileStatement doWhileStmt = (DoWhileStatement)evalStmt;
				foreach(EvalStatement nestedEvalStmt in doWhileStmt.Statements)
					xgrsID = GenImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
			else if(evalStmt is ContainerAccumulationYield)
			{
				ContainerAccumulationYield containerAccumulationYieldStmt = (ContainerAccumulationYield)evalStmt;
				foreach(EvalStatement nestedEvalStmt in containerAccumulationYieldStmt.Statements)
					xgrsID = GenImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
			else if(evalStmt is IntegerRangeIterationYield)
			{
				IntegerRangeIterationYield integerRangeIterationYieldStmt = (IntegerRangeIterationYield)evalStmt;
				foreach(EvalStatement nestedEvalStmt in integerRangeIterationYieldStmt.Statements)
					xgrsID = GenImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
			else if(evalStmt is MatchesAccumulationYield)
			{
				MatchesAccumulationYield matchesAccumulationYieldStmt = (MatchesAccumulationYield)evalStmt;
				foreach(EvalStatement nestedEvalStmt in matchesAccumulationYieldStmt.Statements)
					xgrsID = GenImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
			else if(evalStmt is ForFunction)
			{
				ForFunction forFunctionStmt = (ForFunction)evalStmt;
				foreach(EvalStatement nestedEvalStmt in forFunctionStmt.LoopedStatements)
					xgrsID = GenImperativeStatements(sb, rule, pathPrefix, packageName, nestedEvalStmt, xgrsID);
			}
			else if(evalStmt is ExecStatement)
			{
				ExecStatement execStmt = (ExecStatement)evalStmt;
				xgrsID = GenImperativeStatements(sb, rule, pathPrefix, packageName, execStmt, xgrsID);
			}
			return xgrsID;
		}

		private int GenExec(SourceBuilder sb, string pathPrefix, string packageName, Exec exec, int xgrsID)
		{
			sb.AppendFront("public static GRGEN_LIBGR.EmbeddedSequenceInfo XGRSInfo_" + pathPrefix + xgrsID
					+ " = new GRGEN_LIBGR.EmbeddedSequenceInfo(\n");
			sb.Indent();
			sb.AppendFront("new string[] {");
			foreach(Entity neededEntity in exec.GetNeededEntities(false))
			{
				if(!neededEntity.IsDefToBeYieldedTo())
					sb.Append("\"" + neededEntity.Ident + "\", ");
			}
			sb.Append("},\n");
			sb.AppendFront("new GRGEN_LIBGR.GrGenType[] { ");
			foreach(Entity neededEntity in exec.GetNeededEntities(false))
			{
				if(!neededEntity.IsDefToBeYieldedTo())
				{
					if(neededEntity is Variable)
						sb.Append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(neededEntity) + ")), ");
					else
					{
						GraphEntity gent = (GraphEntity)neededEntity;
						sb.Append(FormatTypeClassRef(gent.Type) + ".typeVar, ");
					}
				}
			}
			sb.Append("},\n");
			sb.AppendFront("new string[] {");
			foreach(Entity neededEntity in exec.GetNeededEntities(false))
			{
				if(neededEntity.IsDefToBeYieldedTo())
					sb.Append("\"" + neededEntity.Ident + "\", ");
			}
			sb.Append("},\n");
			sb.AppendFront("new GRGEN_LIBGR.GrGenType[] { ");
			foreach(Entity neededEntity in exec.GetNeededEntities(false))
			{
				if(neededEntity.IsDefToBeYieldedTo())
				{
					if(neededEntity is Variable)
						sb.Append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(neededEntity) + ")), ");
					else
					{
						GraphEntity gent = (GraphEntity)neededEntity;
						sb.Append(FormatTypeClassRef(gent.Type) + ".typeVar, ");
					}
				}
			}
			sb.Append("},\n");
			sb.AppendFront((!string.ReferenceEquals(packageName, null) ? "\"" + packageName + "\"" : "null") + ",\n");
			sb.AppendFront("\"" + EscapeBackslashAndDoubleQuotes(exec.XGRSString) + "\",\n");
			sb.AppendFront(exec.LineNr + "\n");
			sb.Unindent();
			sb.AppendFront(");\n");

			sb.AppendFront("private static bool ApplyXGRS_" + pathPrefix + xgrsID
					+ "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
			foreach(Entity neededEntity in exec.GetNeededEntities(false))
			{
				if(!neededEntity.IsDefToBeYieldedTo())
					sb.Append(", " + FormatType(neededEntity.Type) + " " + FormatEntity(neededEntity));
			}
			foreach(Entity neededEntity in exec.GetNeededEntities(false))
			{
				if(neededEntity.IsDefToBeYieldedTo())
					sb.Append(", ref " + FormatType(neededEntity.Type) + " " + FormatEntity(neededEntity));
			}
			sb.Append(") {\n");
			sb.Indent();
			foreach(Entity neededEntity in exec.GetNeededEntities(false))
			{
				if(neededEntity.IsDefToBeYieldedTo())
				{
					sb.AppendFront(FormatEntity(neededEntity) + " = ");
					sb.Append(GetInitializationValue(neededEntity.Type) + ";\n");
					sb.Append(";\n");
				}
			}
			sb.AppendFront("return true;\n");
			sb.Unindent();
			sb.AppendFront("}\n");

			++xgrsID;
			return xgrsID;
		}

		private int GenImperativeStatements(SourceBuilder sb, Rule rule, string pathPrefix, string packageName,
				ExecStatement execStmt, int xgrsID)
		{
			sb.AppendFront("public static GRGEN_LIBGR.EmbeddedSequenceInfo XGRSInfo_" + pathPrefix + xgrsID
					+ " = new GRGEN_LIBGR.EmbeddedSequenceInfo(\n");
			sb.Indent();
			sb.AppendFront("new string[] {");
			foreach(Entity neededEntity in execStmt.GetNeededEntities(true))
			{
				if(!neededEntity.IsDefToBeYieldedTo())
					sb.Append("\"" + neededEntity.Ident + "\", ");
			}
			sb.Append("},\n");
			sb.AppendFront("new GRGEN_LIBGR.GrGenType[] { ");
			foreach(Entity neededEntity in execStmt.GetNeededEntities(true))
			{
				if(!neededEntity.IsDefToBeYieldedTo())
				{
					if(neededEntity is Variable)
						sb.Append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(neededEntity) + ")), ");
					else
					{
						GraphEntity gent = (GraphEntity)neededEntity;
						sb.Append(FormatTypeClassRef(gent.Type) + ".typeVar, ");
					}
				}
			}
			sb.Append("},\n");
			sb.AppendFront("new string[] {");
			foreach(Entity neededEntity in execStmt.GetNeededEntities(true))
			{
				if(neededEntity.IsDefToBeYieldedTo())
					sb.Append("\"" + neededEntity.Ident + "\", ");
			}
			sb.Append("},\n");
			sb.AppendFront("new GRGEN_LIBGR.GrGenType[] { ");
			foreach(Entity neededEntity in execStmt.GetNeededEntities(true))
			{
				if(neededEntity.IsDefToBeYieldedTo())
				{
					if(neededEntity is Variable)
						sb.Append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(neededEntity) + ")), ");
					else
					{
						GraphEntity gent = (GraphEntity)neededEntity;
						sb.Append(FormatTypeClassRef(gent.Type) + ".typeVar, ");
					}
				}
			}
			sb.Append("},\n");
			sb.AppendFront((!string.ReferenceEquals(packageName, null) ? "\"" + packageName + "\"" : "null") + ",\n");
			sb.AppendFront("\"" + EscapeBackslashAndDoubleQuotes(execStmt.XGRSString) + "\",\n");
			sb.AppendFront(execStmt.LineNr + "\n");
			sb.Unindent();
			sb.AppendFront(");\n");

			sb.AppendFront("private static bool ApplyXGRS_" + pathPrefix + xgrsID
					+ "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
			foreach(Entity neededEntity in execStmt.GetNeededEntities(true))
			{
				if(!neededEntity.IsDefToBeYieldedTo())
					sb.Append(", " + FormatType(neededEntity.Type) + " " + FormatEntity(neededEntity));
			}
			foreach(Entity neededEntity in execStmt.GetNeededEntities(true))
			{
				if(neededEntity.IsDefToBeYieldedTo())
					sb.Append(", ref " + FormatType(neededEntity.Type) + " " + FormatEntity(neededEntity));
			}
			sb.Append(") {\n");
			sb.Indent();
			foreach(Entity neededEntity in execStmt.GetNeededEntities(true))
			{
				if(neededEntity.IsDefToBeYieldedTo())
				{
					sb.AppendFront(FormatEntity(neededEntity) + " = ");
					sb.Append(GetInitializationValue(neededEntity.Type) + ";\n");
					sb.Append(";\n");
				}
			}
			sb.AppendFront("return true;\n");
			sb.Unindent();
			sb.AppendFront("}\n");

			++xgrsID;
			return xgrsID;
		}

		public virtual void GenImperativeStatementClosures(SourceBuilder sb, Rule rule, string pathPrefix,
				bool isTopLevelRule)
		{
			if(rule.Right == null)
				return;

			if(!isTopLevelRule)
				GenImperativeStatementClosures(sb, rule, pathPrefix);

			PatternGraphLhs pattern = rule.Pattern;
			foreach(Alternative alt in pattern.Alts)
			{
				string altName = alt.NameOfGraph;
				foreach(Rule altCase in alt.AlternativeCases)
				{
					PatternGraphLhs altCasePattern = altCase.Left;
					GenImperativeStatementClosures(sb, altCase,
							pathPrefix + altName + "_" + altCasePattern.NameOfGraph + "_",
							false);
				}
			}

			foreach(Rule iter in pattern.Iters)
			{
				string iterName = iter.Left.NameOfGraph;
				GenImperativeStatementClosures(sb, iter,
						pathPrefix + iterName + "_",
						false);
			}
		}

		private void GenImperativeStatementClosures(SourceBuilder sb, Rule rule, string pathPrefix)
		{
			int xgrsID = 0;
			foreach(ImperativeStmt istmt in rule.Right.ImperativeStmts)
			{
				if(!(istmt is Exec))
					continue;

				Exec exec = (Exec)istmt;
				sb.Append("\n");
				sb.AppendFront("public class XGRSClosure_" + pathPrefix + xgrsID
						+ " : GRGEN_LGSP.LGSPEmbeddedSequenceClosure\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("public XGRSClosure_" + pathPrefix + xgrsID + "(");
				bool first = true;
				foreach(Entity neededEntity in exec.GetNeededEntities(false))
				{
					if(first)
						first = false;
					else
						sb.Append(", ");
					sb.Append(FormatType(neededEntity.Type) + " " + FormatEntity(neededEntity));
				}
				sb.Append(") {\n");
				sb.Indent();
				foreach(Entity neededEntity in exec.GetNeededEntities(false))
					sb.AppendFront("this." + FormatEntity(neededEntity) + " = " + FormatEntity(neededEntity) + ";\n");
				sb.Unindent();
				sb.AppendFront("}\n");

				sb.AppendFront("public override bool exec(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv) {\n");
				sb.AppendFrontIndented("return ApplyXGRS_" + pathPrefix + xgrsID + "(procEnv");
				foreach(Entity neededEntity in exec.GetNeededEntities(false))
					sb.Append(", " + FormatEntity(neededEntity));

				sb.Append(");\n");
				sb.AppendFront("}\n");

				foreach(Entity neededEntity in exec.GetNeededEntities(false))
					sb.AppendFront(FormatType(neededEntity.Type) + " " + FormatEntity(neededEntity) + ";\n");

				//sb.append("\n");
				//sb.append("\t\t\tpublic static int numFreeClosures = 0;\n");
				//sb.append("\t\t\tpublic static LGSPEmbeddedSequenceClosure rootOfFreeClosures = null;\n");

				sb.Unindent();
				sb.AppendFront("}\n");

				++xgrsID;
			}
		}

		public virtual void GenImperativeStatements(SourceBuilder sb, Procedure procedure)
		{
			int xgrsID = 0;
			foreach(EvalStatement evalStmt in procedure.Statements)
				xgrsID = GenImperativeStatements(sb, procedure, evalStmt, xgrsID);
		}

		private int GenImperativeStatements(SourceBuilder sb, Procedure procedure, EvalStatement evalStmt, int xgrsID)
		{
			if(evalStmt is ExecStatement)
			{
				GenImperativeStatement(sb, procedure, procedure.PackageContainedIn, (ExecStatement)evalStmt, xgrsID);
				++xgrsID;
			}
			else if(evalStmt is ConditionStatement)
			{
				ConditionStatement condStmt = (ConditionStatement)evalStmt;
				foreach(EvalStatement childEvalStmt in condStmt.Statements)
					xgrsID = GenImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
				if(condStmt.FalseCaseStatements != null)
				{
					foreach(EvalStatement childEvalStmt in condStmt.FalseCaseStatements)
						xgrsID = GenImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
				}
			}
			else if(evalStmt is SwitchStatement)
			{
				SwitchStatement switchStmt = (SwitchStatement)evalStmt;
				foreach(EvalStatement childEvalStmt in switchStmt.Statements)
					xgrsID = GenImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
			else if(evalStmt is CaseStatement)
			{
				CaseStatement caseStmt = (CaseStatement)evalStmt;
				foreach(EvalStatement childEvalStmt in caseStmt.Statements)
					xgrsID = GenImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
			else if(evalStmt is ContainerAccumulationYield)
			{
				foreach(EvalStatement childEvalStmt in ((ContainerAccumulationYield)evalStmt).Statements)
					xgrsID = GenImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
			else if(evalStmt is IntegerRangeIterationYield)
			{
				foreach(EvalStatement childEvalStmt in ((IntegerRangeIterationYield)evalStmt).Statements)
					xgrsID = GenImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
			else if(evalStmt is ForFunction)
			{
				foreach(EvalStatement childEvalStmt in ((ForFunction)evalStmt).LoopedStatements)
					xgrsID = GenImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
			else if(evalStmt is DoWhileStatement)
			{
				foreach(EvalStatement childEvalStmt in ((DoWhileStatement)evalStmt).Statements)
					xgrsID = GenImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
			else if(evalStmt is WhileStatement)
			{
				foreach(EvalStatement childEvalStmt in ((WhileStatement)evalStmt).Statements)
					xgrsID = GenImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
			else if(evalStmt is MultiStatement)
			{
				foreach(EvalStatement childEvalStmt in ((MultiStatement)evalStmt).Statements)
					xgrsID = GenImperativeStatements(sb, procedure, childEvalStmt, xgrsID);
			}
			return xgrsID;
		}

		private void GenImperativeStatement(SourceBuilder sb, Identifiable procedure, string packageName,
				ExecStatement execStmt, int xgrsID)
		{
			Exec exec = execStmt.Exec;

			sb.AppendFront("public static GRGEN_LIBGR.EmbeddedSequenceInfo XGRSInfo_" + FormatIdentifiable(procedure) + "_" + xgrsID
					+ " = new GRGEN_LIBGR.EmbeddedSequenceInfo(\n");
			sb.Indent();
			sb.AppendFront("new string[] {");
			foreach(Entity neededEntity in exec.GetNeededEntities(true))
			{
				if(!neededEntity.IsDefToBeYieldedTo())
					sb.Append("\"" + neededEntity.Ident + "\", ");
			}
			sb.Append("},\n");
			sb.AppendFront("new GRGEN_LIBGR.GrGenType[] { ");
			foreach(Entity neededEntity in exec.GetNeededEntities(true))
			{
				if(!neededEntity.IsDefToBeYieldedTo())
				{
					if(neededEntity is Variable)
						sb.Append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(neededEntity) + ")), ");
					else
					{
						GraphEntity gent = (GraphEntity)neededEntity;
						sb.Append(FormatTypeClassRef(gent.Type) + ".typeVar, ");
					}
				}
			}
			sb.Append("},\n");
			sb.AppendFront("new string[] {");
			foreach(Entity neededEntity in exec.GetNeededEntities(true))
			{
				if(neededEntity.IsDefToBeYieldedTo())
					sb.Append("\"" + neededEntity.Ident + "\", ");
			}
			sb.Append("},\n");
			sb.AppendFront("new GRGEN_LIBGR.GrGenType[] { ");
			foreach(Entity neededEntity in exec.GetNeededEntities(true))
			{
				if(neededEntity.IsDefToBeYieldedTo())
				{
					if(neededEntity is Variable)
						sb.Append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(neededEntity) + ")), ");
					else
					{
						GraphEntity gent = (GraphEntity)neededEntity;
						sb.Append(FormatTypeClassRef(gent.Type) + ".typeVar, ");
					}
				}
			}
			sb.Append("},\n");
			sb.AppendFront((!string.ReferenceEquals(packageName, null) ? "\"" + packageName + "\"" : "null") + ",\n");
			sb.AppendFront("\"" + EscapeBackslashAndDoubleQuotes(exec.XGRSString) + "\",\n");
			sb.AppendFront(exec.LineNr + "\n");
			sb.Unindent();
			sb.AppendFront(");\n");

			sb.AppendFront("private static bool ApplyXGRS_" + FormatIdentifiable(procedure) + "_" + xgrsID
					+ "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
			foreach(Entity neededEntity in exec.GetNeededEntities(true))
			{
				if(!neededEntity.IsDefToBeYieldedTo())
					sb.Append(", " + FormatType(neededEntity.Type) + " " + FormatEntity(neededEntity));
			}
			foreach(Entity neededEntity in exec.GetNeededEntities(true))
			{
				if(neededEntity.IsDefToBeYieldedTo())
					sb.Append(", ref " + FormatType(neededEntity.Type) + " " + FormatEntity(neededEntity));
			}
			sb.Append(") {\n");
			foreach(Entity neededEntity in exec.GetNeededEntities(true))
			{
				if(neededEntity.IsDefToBeYieldedTo())
				{
					sb.AppendFront(FormatEntity(neededEntity) + " = ");
					sb.Append(GetInitializationValue(neededEntity.Type) + ";\n");
					sb.Append(";\n");
				}
			}
			sb.AppendFront("return true;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		protected internal override void GenQualAccess(SourceBuilder sb, Qualification qual, object modifyGenerationState)
		{
			// needed because of inheritance, maybe todo: remove
		}

		protected internal override void GenMemberAccess(SourceBuilder sb, Entity member)
		{
			// needed because of inheritance, maybe todo: remove
		}
	}

}
