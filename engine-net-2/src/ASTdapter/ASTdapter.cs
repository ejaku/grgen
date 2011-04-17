/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using antlr;
using antlr.collections;
using de.unika.ipd.grGen.libGr;

namespace ASTdapter
{
    public class ASTdapter
    {
        private ParserPackage parserpackage;
        private Random myRandom = new Random();

        public ASTdapter(ParserPackage configuration)
        {
            parserpackage = configuration;
        }

        /// <summary>
        /// This method parses an input stream with the configured parser and loads the resulting AST into the given graph.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void Load(Stream from, IGraph to)
        {
            TokenStream lexer = parserpackage.GetLexer(from);
            LLkParser instance = parserpackage.GetParser(lexer);
            Load(instance, to);
        }

        /// <summary>
        /// This method parses the given parameter string with the configured parser and loads the resulting AST into the given graph.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void Load(string from, IGraph to)
        {
            TokenStream lexer = parserpackage.GetLexer(from);
            LLkParser instance = parserpackage.GetParser(lexer);
            Load(instance, to);
        }

        private void Load(LLkParser instance, IGraph to)
        {
            AST a;
            parserpackage.CallParser(instance);
            a = instance.getAST();
            if (a != null) Emit(a, to);
            else
            {
                throw new Exception("No AST!");
            }
        }

        private INode Emit(AST a, IGraph to)
        {
            String text = a.getText();
            int iType = a.Type;
            String type = parserpackage.GetTypeName(iType);
            NodeType currentNodeType = GetNodeType(type, to);
            INode currentNode = to.AddNode(currentNodeType);
            currentNode.SetAttribute("value", text);

            if (a.getNumberOfChildren() > 0)
            {
                List<AST> l = GetChildren(a);
                INode previousChild = null;
                foreach (AST current in l)
                {
                    INode childNode = Emit(current, to);
                    EdgeType childType = GetEdgeType("child", to);
                    to.AddEdge(childType, currentNode, childNode);

                    if (previousChild != null)
                    {
                        EdgeType nextType = GetEdgeType("next", to);
                        to.AddEdge(nextType, previousChild, childNode);
                    }
                    previousChild = childNode;
                }
            }
            return currentNode;
        }

        private NodeType GetNodeType(string name, IGraph graph)
        {
            return graph.Model.NodeModel.GetType(name);
        }

        private EdgeType GetEdgeType(string name, IGraph graph)
        {
            return graph.Model.EdgeModel.GetType(name);
        }

        private List<AST> GetChildren(AST a)
        {
            List<AST> result = new List<AST>();
            AST current = a.getFirstChild();
            while (current != null)
            {
                result.Add(current);
                current = current.getNextSibling();
            }
            return result;
        }


    }
}
