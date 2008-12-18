/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

// generates graphs containing dna-chains
// emits them in the input formats needed by
// a)GrGen.NET/GrShell
// b)Viatra

namespace GenerateGraphs
{
    class Element // Graph Elements == Nodes or Edges, node if src==null || tgt==null
    {
        public Element(string name, string type)
        {
            this.name = name;
            this.type = type;
        }

        public Element(string name, string type, Element src, Element tgt)
        {
            this.name = name;
            this.type = type;
            this.src = src;
            this.tgt = tgt;
        }

        public string name;
        public string type;
        public string comment;

        public Element src;
        public Element tgt;
    }

    class Program
    {
        const string NODE = "Node";
        const string NUCLEOTIDE = "N";
        const string ADENIN = "A";
        const string CYTOSIN = "C";
        const string GUANIN = "G";
        const string THYMIN = "T";
        const string URACIL = "U";
        const string HYDROXYGUANIN = "H";
        const string SUGAR = "S";
        const string DESOXYRIBOSE = "D";
        const string RIBOSE = "R";
        const string BINDING = "Edge";
        const string PHOSPHATE_GROUP = "PG";

        static void Main(string[] args)
        {
            List<Element> elements = new List<Element>(); 

            for (int i = 0; i < 2; ++i)
            {
                GenerateGraph_DNAChains(elements, i.ToString());
                GenerateGraph_DNAChainsCorrupt(elements);
            }
            
            StreamWriter grs = new StreamWriter("generateGraph.grs", false, Encoding.Default, 4096);
            EmitGraphForGrGen(elements, grs);
            grs.Close();

            StreamWriter vtml = new StreamWriter("generateGraph.vtml", false, Encoding.Default, 4096);
            EmitGraphForViatra(elements, vtml);
            vtml.Close();
        }

        private static void EmitGraphForViatra(List<Element> elements, StreamWriter vtml)
        {
            vtml.WriteLine("namespace beispiel.models;");
            vtml.WriteLine("import beispiel.metamodel;");
            vtml.WriteLine();

            foreach (Element element in elements)
            {
                if (element.src == null || element.tgt == null)
                {
                    vtml.WriteLine(element.type + "(" + element.name + "); //" + element.comment);
                }
                else
                {
                    if (element.type == BINDING)
                        vtml.WriteLine(NODE + "." + element.type + "(" + element.name + ", " + element.src.name + ", " + element.tgt.name + ");");
                    else //edge.type == PHOSPHATE_GROUP
                        vtml.WriteLine(SUGAR + "." + element.type + "(" + element.name + ", " + element.src.name + ", " + element.tgt.name + ");");
                }
            }
        }

        private static void EmitGraphForGrGen(List<Element> elements, StreamWriter grs)
        {
            grs.WriteLine("new graph \"TranskriptionAbstrakt\"");
            grs.WriteLine("debug set layout Organic");
            grs.WriteLine("dump set node A color green");
            grs.WriteLine("dump set node A shape lparallelogram");
            grs.WriteLine("dump set node G color blue");
            grs.WriteLine("dump set node G shape lparallelogram");
            grs.WriteLine("dump set node C color red");
            grs.WriteLine("dump set node C shape lparallelogram");
            grs.WriteLine("dump set node T color purple");
            grs.WriteLine("dump set node T shape lparallelogram");
            grs.WriteLine("dump set node U color orchid");
            grs.WriteLine("dump set node U shape lparallelogram");
            grs.WriteLine("dump set node H color cyan");
            grs.WriteLine("dump set node H shape lparallelogram");
            grs.WriteLine("dump set node D color yellow");
            grs.WriteLine("dump set node D shape rparallelogram");
            grs.WriteLine("dump set node R color orange");
            grs.WriteLine("dump set node R shape rparallelogram");
            grs.WriteLine();

            int i = 0;
            grs.WriteLine("silence on");
            grs.WriteLine("echo \"creating elements\"");
            foreach (Element element in elements)
            {
                if (element.src == null || element.tgt == null)
                {
                    grs.WriteLine("new " + element.name + ":" + element.type + " #" + element.comment);
                }
                else
                {
                    grs.WriteLine("new " + element.src.name + " -" + element.name + ":" + element.type + "-> " + element.tgt.name);
                }
                ++i;
                if (i % 1000 == 0) grs.WriteLine("echo \"" + i.ToString() + " elements created\"");
            }
            grs.WriteLine("echo \"done: " + i.ToString() + " elements created\"");
        }

        private static void GenerateGraph_DNAChains(List<Element> elements, string comment)
        {
            Element lastNode = null;
            lastNode = GenerateDNAChain(elements, 100, lastNode, "");
            lastNode = GenerateTATABox(elements, lastNode);
            lastNode = GenerateDNAChain(elements, 200, lastNode, comment + "/1");
            lastNode = GenerateTerminationSequence(elements, lastNode);
            lastNode = GenerateDNAChain(elements, 100, lastNode, "");
            lastNode = GenerateTATABox(elements, lastNode);
            lastNode = GenerateDNAChain(elements, 200, lastNode, comment + "/2");
            lastNode = GenerateTerminationSequence(elements, lastNode);
            GenerateDNAChain(elements, 100, lastNode, "");
        }

        private static void GenerateGraph_DNAChainsCorrupt(List<Element> elements)
        {
            Element lastNode = null;
            lastNode = GenerateCorruptDNAChain(elements, 100, lastNode);
            lastNode = GenerateTATABox(elements, lastNode);
            lastNode = GenerateCorruptDNAChain(elements, 100, lastNode);
            lastNode = GenerateTerminationSequence(elements, lastNode);
            lastNode = GenerateCorruptDNAChain(elements, 100, lastNode);
            lastNode = GenerateTATABox(elements, lastNode);
            lastNode = GenerateCorruptDNAChain(elements, 100, lastNode);
            lastNode = GenerateTerminationSequence(elements, lastNode);
            GenerateCorruptDNAChain(elements, 100, lastNode);
        }

        private static Element GenerateTATABox(List<Element> elements, Element prevNode)
        {
            Element[] n = new Element[6*2];

            // TATAAA
            n[0] = new Element(getNodeName(), DESOXYRIBOSE);
            n[0 + 1] = new Element(getNodeName(), THYMIN);
            n[2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[2 + 1] = new Element(getNodeName(), ADENIN);
            n[4] = new Element(getNodeName(), DESOXYRIBOSE);
            n[4 + 1] = new Element(getNodeName(), THYMIN);
            n[6] = new Element(getNodeName(), DESOXYRIBOSE);
            n[6 + 1] = new Element(getNodeName(), ADENIN);
            n[8] = new Element(getNodeName(), DESOXYRIBOSE);
            n[8 + 1] = new Element(getNodeName(), ADENIN);
            n[10] = new Element(getNodeName(), DESOXYRIBOSE);
            n[10 + 1] = new Element(getNodeName(), ADENIN);

            for (int i = 0; i < 6*2; ++i)
            {
                elements.Add(n[i]);
            }

            Element currNode = null;
            Element nucleotideNode = null;
            for (int i = 0; i < 6*2; i+=2)
            {
                currNode = n[i];
                nucleotideNode = n[i+1];
                elements.Add(new Element(getEdgeName(), BINDING, currNode, nucleotideNode));
                if (prevNode != null) elements.Add(new Element(getEdgeName(), PHOSPHATE_GROUP, prevNode, currNode));
                prevNode = currNode;
            }

            return currNode;
        }

        private static Element GenerateDNAChain(List<Element> elements,
            int length, Element prevNode, string comment)
        {
            Element currNode = null;
            Element currNucleotideNode = null;
            Element prevNucleotideNode = null;
            for (int i = 1; i <= length; ++i)
            {
                currNode = new Element(getNodeName(), DESOXYRIBOSE);
                currNucleotideNode = new Element(getNodeName(), drawAminoAcid());
                while(currNucleotideNode.type==ADENIN && prevNucleotideNode!=null && prevNucleotideNode.type==THYMIN) 
                    currNucleotideNode.type = drawAminoAcid();
                if(i==1) currNode.comment = currNucleotideNode.comment = comment + "/first of " + length.ToString();
                if(i==length) currNode.comment = currNucleotideNode.comment = comment + "/last of " + length.ToString();
                elements.Add(currNode);
                elements.Add(currNucleotideNode);
                elements.Add(new Element(getEdgeName(), BINDING, currNode, currNucleotideNode));
                if (prevNode != null) elements.Add(new Element(getEdgeName(), PHOSPHATE_GROUP, prevNode, currNode));
                prevNode = currNode;
                prevNucleotideNode = currNucleotideNode;
            }

            return currNode; 
        }

        private static Element GenerateCorruptDNAChain(List<Element> elements,
            int length, Element prevNode)
        {
            Element currNode = null;
            Element currNucleotideNode = null;
            Element prevNucleotideNode = null;
            bool isCorrupt = false;
            for(int i = 1; i <= length; ++i)
            {
                currNode = new Element(getNodeName(), DESOXYRIBOSE);
                currNucleotideNode = new Element(getNodeName(), drawMaybeCorruptAminoAcid());
                while(currNucleotideNode.type==ADENIN && prevNucleotideNode!=null && prevNucleotideNode.type==THYMIN)
                    currNucleotideNode.type = drawAminoAcid();
                if (currNucleotideNode.type == URACIL || currNucleotideNode.type == HYDROXYGUANIN) isCorrupt = true;
                if(i==length && !isCorrupt) currNucleotideNode.type = URACIL;
                elements.Add(currNode);
                elements.Add(currNucleotideNode);
                elements.Add(new Element(getEdgeName(), BINDING, currNode, currNucleotideNode));
                if (prevNode != null) elements.Add(new Element(getEdgeName(), PHOSPHATE_GROUP, prevNode, currNode));
                prevNode = currNode;
                prevNucleotideNode = currNucleotideNode;
            }

            return currNode;
        }

        private static Element GenerateTerminationSequence(List<Element> elements, Element prevNode)
        {
            Element[] n = new Element[24 * 2];

            // CCCACT
            n[0*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[0*2+1] = new Element(getNodeName(), CYTOSIN);
            n[1*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[1*2+1] = new Element(getNodeName(), CYTOSIN);
            n[2*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[2*2+1] = new Element(getNodeName(), CYTOSIN);
            n[3*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[3*2+1] = new Element(getNodeName(), ADENIN);
            n[4*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[4*2+1] = new Element(getNodeName(), CYTOSIN);
            n[5*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[5*2+1] = new Element(getNodeName(), THYMIN);

            // ......
            n[6*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[6*2+1] = new Element(getNodeName(), drawAminoAcid());
            n[7*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[7*2+1] = new Element(getNodeName(), drawAminoAcid());
            n[8*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[8*2+1] = new Element(getNodeName(), drawAminoAcid());
            n[9*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[9*2+1] = new Element(getNodeName(), drawAminoAcid());
            n[10*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[10*2+1] = new Element(getNodeName(), drawAminoAcid());
            n[11*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[11*2+1] = new Element(getNodeName(), drawAminoAcid());
            
            // AGTGGG = invers und gespiegelt zu CCCACT
            n[12*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[12*2+1] = new Element(getNodeName(), ADENIN);
            n[13*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[13*2+1] = new Element(getNodeName(), GUANIN);
            n[14*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[14*2+1] = new Element(getNodeName(), THYMIN);
            n[15*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[15*2+1] = new Element(getNodeName(), GUANIN);
            n[16*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[16*2+1] = new Element(getNodeName(), GUANIN);
            n[17*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[17*2+1] = new Element(getNodeName(), GUANIN);
            
            // AAAAAA
            n[18*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[18*2+1] = new Element(getNodeName(), ADENIN);
            n[19*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[19*2+1] = new Element(getNodeName(), ADENIN);
            n[20*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[20*2+1] = new Element(getNodeName(), ADENIN);
            n[21*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[21*2+1] = new Element(getNodeName(), ADENIN);
            n[22*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[22*2+1] = new Element(getNodeName(), ADENIN);
            n[23*2] = new Element(getNodeName(), DESOXYRIBOSE);
            n[23*2+1] = new Element(getNodeName(), ADENIN);

            for (int i = 0; i < 24*2; ++i)
            {
                elements.Add(n[i]);
            }

            Element currNode = null;
            Element nucleotideNode = null;
            for (int i = 0; i < 24*2; i+=2)
            {
                currNode = n[i];
                nucleotideNode = n[i+1];
                elements.Add(new Element(getEdgeName(), BINDING, currNode, nucleotideNode));
                if (prevNode != null) elements.Add(new Element(getEdgeName(), PHOSPHATE_GROUP, prevNode, currNode));
                prevNode = currNode;
            }

            return currNode;
        }

        private static string getNodeName()
        {
            return "N" + getId();
        }
        
        private static string getEdgeName()
        {
            return "E" + getId();
        }
        
        private static string getId()
        {
            ++id;
            return id.ToString();
        }

        private static string drawAminoAcid()
        {
            int index = random.Next(4);
            if (index == 0) return ADENIN;
            if (index == 1) return CYTOSIN;
            if (index == 2) return GUANIN;
            return THYMIN;
        }

        private static string drawMaybeCorruptAminoAcid()
        {
            int index = random.Next(34);
            if (index >= 0 && index <= 7) return ADENIN;
            if (index >= 8 && index <= 15) return CYTOSIN;
            if (index >= 16 && index <= 23) return GUANIN;
            if (index >= 24 && index <= 31) return THYMIN;
            if (index == 32) return URACIL;
            return HYDROXYGUANIN;
        }

        private static int id = -1;
        private static Random random = new Random(0);
    }
}
