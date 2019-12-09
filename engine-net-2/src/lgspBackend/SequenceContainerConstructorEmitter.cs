/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// The C#-part responsible for emitting the container constructor expressions that appear in an exec statement, in a post-run after generating the sequence.
    /// </summary>
    public class SequenceContainerConstructorEmitter
    {
        public static void GenerateContainerConstructor(IGraphModel model, SequenceExpressionContainerConstructor cc, SourceBuilder source)
        {
            string containerType = TypesHelper.XgrsTypeToCSharpType(GetContainerType(cc), model);
            string valueType = TypesHelper.XgrsTypeToCSharpType(cc.ValueType, model);
            string keyType = null;
            if(cc is SequenceExpressionMapConstructor)
                keyType = TypesHelper.XgrsTypeToCSharpType(((SequenceExpressionMapConstructor)cc).KeyType, model);

            source.Append("\n");
            source.AppendFront("public static ");
            source.Append(containerType);
            source.Append(" fillFromSequence_" + cc.Id);
            source.Append("(");
            for(int i = 0; i < cc.ContainerItems.Length; ++i)
            {
                if(i > 0)
                    source.Append(", ");
                if(keyType != null)
                    source.AppendFormat("{0} paramkey{1}, ", keyType, i);
                source.AppendFormat("{0} param{1}", valueType, i);
            }
            source.Append(")\n");
            
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("{0} container = new {0}();\n", containerType);
            for(int i = 0; i < cc.ContainerItems.Length; ++i)
            {
                if(cc is SequenceExpressionSetConstructor)
                    source.AppendFrontFormat("container.Add(param{0}, null);\n", i);
                else if(cc is SequenceExpressionMapConstructor)
                    source.AppendFrontFormat("container.Add(paramkey{0}, param{0});\n", i);
                else if(cc is SequenceExpressionArrayConstructor)
                    source.AppendFrontFormat("container.Add(param{0});\n", i);
                else //if(cc is SequenceExpressionDequeConstructor)
                    source.AppendFrontFormat("container.Enqueue(param{0});\n", i);
            }
            source.AppendFront("return container;\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        private static string GetContainerType(SequenceExpressionContainerConstructor cc)
        {
            if(cc is SequenceExpressionSetConstructor)
                return "set<" + cc.ValueType + ">";
            else if(cc is SequenceExpressionMapConstructor)
                return "map<" + ((SequenceExpressionMapConstructor)cc).KeyType + "," + cc.ValueType + ">";
            else if(cc is SequenceExpressionArrayConstructor)
                return "array<" + cc.ValueType + ">";
            else //if(cc is SequenceExpressionDequeConstructor)
                return "deque<" + cc.ValueType + ">";
        }
    }
}
