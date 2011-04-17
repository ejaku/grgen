/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using System.IO;

namespace grIO
{
    public class FileIO
    {
        private IGraph graph;
        private NodeType outputnodetype;
        private NodeType fileType;
        private EdgeType createOrOverwriteType;
        private EdgeType createOrAppendType;
        private EdgeType containsLineType;
        private EdgeType nextLineType;
        private NodeType lineType;
        private AttributeType fileNameAttrType;
        private AttributeType lineContentAttrType;

        public FileIO(IGraph g)
        {
            try
            {
                graph = g;
                outputnodetype = g.Model.NodeModel.GetType("grIO_OUTPUT");
                if (outputnodetype == null) throw new Exception();
                createOrOverwriteType = g.Model.EdgeModel.GetType("grIO_CreateOrOverwrite");
                if (createOrOverwriteType == null) throw new Exception();
                createOrAppendType = g.Model.EdgeModel.GetType("grIO_CreateOrAppend");
                if (createOrAppendType == null) throw new Exception();
                fileType = g.Model.NodeModel.GetType("grIO_File");
                if (fileType == null) throw new Exception();
                fileNameAttrType = fileType.GetAttributeType("path");
                if (fileNameAttrType == null) throw new Exception();
                lineType = g.Model.NodeModel.GetType("grIO_File_Line");
                if (lineType == null) throw new Exception();
                containsLineType = g.Model.EdgeModel.GetType("grIO_File_ContainsLine");
                if (containsLineType == null) throw new Exception();
                nextLineType = g.Model.EdgeModel.GetType("grIO_File_NextLine");
                if (nextLineType == null) throw new Exception();
                lineContentAttrType = lineType.GetAttributeType("content");
                if (lineContentAttrType == null) throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception("Could not find the required node/edge types. Did you include the GrIO-model?");
            }
        }

        public void FlushFiles()
        {
            foreach (INode n in graph.GetCompatibleNodes(outputnodetype))
            {
                FlushFile(n);
            }
        }

        private void FlushFile(INode output)
        {
            int count = 0;
            foreach (IEdge e in output.GetCompatibleOutgoing(createOrOverwriteType))
            {
                CreateOrOverwrite(e.Target);
                graph.Remove(e);
                count++;
            }
            foreach (IEdge e in output.GetCompatibleOutgoing(createOrAppendType))
            {
                CreateOrAppend(e.Target);
                graph.Remove(e);
                count++;
            }
            Infrastructure.MsgToConsole("Wrote " + count + " file(s).");
        }

        private void CreateOrAppend(INode file)
        {
            string filename = (string)file.GetAttribute(fileNameAttrType.Name);
            using(FileStream fs = new FileStream(filename, FileMode.Append))
                Write(file, fs);
        }

        private void CreateOrOverwrite(INode file)
        {
            string filename = (string)file.GetAttribute(fileNameAttrType.Name);
            using(FileStream fs = new FileStream(filename, FileMode.Create))
                Write(file, fs);
        }

        private void Write(INode file, Stream s)
        {
            using(StreamWriter sw = new StreamWriter(s))
            {
                foreach(INode line in GetLines(file))
                {
                    string content = (string)line.GetAttribute(lineContentAttrType.Name);
                    sw.WriteLine(content);
                }
            }
        }

        private IEnumerable<INode> GetLines(INode file)
        {
            INode first = null;
            // find first line
            foreach (IEdge e in file.GetCompatibleOutgoing(containsLineType))
            {
                bool isFirst = true;
                foreach (IEdge incoming in e.Target.GetCompatibleIncoming(nextLineType))
                {
                    isFirst = false;
                    break;
                }
                if (isFirst)
                {
                    first = e.Target;
                    break;
                }
            }
            if (first == null) yield break;

            yield return first;

            INode currentNode = GetNextLineNode(first);
            while (currentNode != null)
            {
                yield return currentNode;
                currentNode = GetNextLineNode(currentNode);
            }
        }

        private INode GetNextLineNode(INode current)
        {
            foreach (IEdge nextEdge in current.GetCompatibleOutgoing(nextLineType))
            {
                return nextEdge.Target;
            }
            return null;
        }
    }
}
