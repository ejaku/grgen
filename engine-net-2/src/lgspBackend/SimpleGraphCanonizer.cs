/*
 * Based on:
 *  1. WEININGER D,                                                                                                                                                                                                                         
 *    "SMILES, A chemical language and information-system. 1.                                                                                                                                                                               
 *     Introductions to methodology and encoding rules"                                                                                                                                                                                     
 *     JOURNAL OF CHEMICAL INFORMATION AND COMPUTER SCIENCES,28(#1),                                                                                                                                                                        
 *     1988,31-36                                                                                                                                                                                                                           
 *  2. WEININGER D, WEININGER A, WEININGER JL,                                                                                                                                                                                              
 *    "SMILES 2. Algorithm for generation of unique SMILES notation"                                                                                                                                                                        
 *    JOURNAL OF CHEMICAL INFORMATION AND COMPUTER SCIENCES,29(#2),                                                                                                                                                                         
 *    1989,97-101
 *
 * Implementation based on the python implementation by Brian Kelly:
 * (Dr Dobbs article) http://www.drdobbs.com/graph-canonicalization/184405341
 * 
 * Brian says: THIS ROUTINE WON'T NECESSARILY DIFFERENTIATE NONISOMORPHIC COSPECTRAL GRAPHS...I need to understand cospectrality better,
 * the algorithm obviously works on some cospectral graphs (e.g. C_4 U K_1 and S_5).  Presumably this is an issue with highly regular graphs.
 * 
 * From http://mathworld.wolfram.com/CospectralGraphs.html:
 * "Determining which graphs are uniquely determined by their spectra is in general a very hard problem. Only a small fraction of graphs are known
 * to be so determined, but it is conceivable that almost all graphs have this property (van Dam and Haemers 2003)."
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

using INodeList = System.Collections.Generic.List<de.unika.ipd.grGen.libGr.INode>;
using IEdgeList = System.Collections.Generic.List<de.unika.ipd.grGen.libGr.IEdge>;

namespace de.unika.ipd.grGen.lgsp
{
	enum CoEdgeTypeOffsets{Undirected,Tail,Head}

	class CoNode
	{
		public INode Parent { get; set; }
		public List<int> Neighbors { get; set; }

		public CoNode ()
		{
			Parent = null;
			Neighbors = new List<int> ();
		}

		public CoNode (INode parent) : this()
		{
			Parent = parent;
		}
	}


	sealed public class SimpleGraphCanonizer:GraphCanonizer
	{
		Dictionary<IGraphElement, string> elementTypeNameMap;

		List<CoNode> coNodes;
		List<long> coNodeTypeIDs;
		List<long> edgeCoNodeTypeIDs;

		Dictionary<INode, int> nodeToCoNodeIndexMap;
		Dictionary<INode, Dictionary<INode,IEdgeList>> nodeNeighborMap;

		List<string> typeNames;
		List<string> edgeTypeNames;

		Dictionary<string, long> typeNameIDMap;
		Dictionary<string, long> edgeTypeNameIDMap;

		private List<long> nodeSymmIDs;
		private List<int> nodeTraversalOrder;

		private Dictionary<INode, int> closureNodes;
		private int curClosure = 0;

		private List<List<int>> connectedComponents;

		private Dictionary<int,int> curCoIndexer;
		private List<int> curConnectedComponenet;

		Dictionary<INode, List<Pair<INode,List<IEdge>>>> sortedNodeNeighborMap;

		INode lowestOrderNode = null;

		public SimpleGraphCanonizer ()
		{

		}

		//Stolen from GXLExport.cs
		private string CanonizeElement (IGraphElement elem)
		{
				List<String> attributes = new List<String> ();
			foreach (AttributeType attrType in elem.Type.AttributeTypes) {
				object value = elem.GetAttribute (attrType.Name);
				
				String valType;
				String valuestr = (value == null) ? "" : value.ToString ();
				switch (attrType.Kind) {
				case AttributeKind.BooleanAttr:
					valType = "bool";
					valuestr = ((bool)value) ? "true" : "false";
					break;
					
				case AttributeKind.DoubleAttr:
				case AttributeKind.FloatAttr:
					valType = "double";
					break;
					
				case AttributeKind.ByteAttr:
				case AttributeKind.ShortAttr:
				case AttributeKind.IntegerAttr:
				case AttributeKind.LongAttr:
					valType = "int";
					break;
					
				// TODO: This does not allow differentiating between empty and null strings
				case AttributeKind.StringAttr:
					valType = "string";
					valuestr= "\"" + valuestr + "\"";
					break;
					
				case AttributeKind.EnumAttr:
					valType = "enum";
					break;
					
				default:
					throw new Exception ("Unsupported attribute value type: \"" + attrType.Kind + "\"");
				}

				attributes.Add (attrType.Name + ":" + valType + ":" + valuestr);
			}

			attributes.Sort ((x,y) => x.CompareTo (y));
	
			string elemTypeName = null;
			string elemClassName = elem.GetType ().Name;

			if (elem is INode) {
				elemTypeName = "Node";
			} else if (elem is IEdge) {
				elemTypeName = "Edge";
			} else {
				elemTypeName = "Unsupported Graph Element Type";
			}

			StringBuilder sb = new StringBuilder (elemClassName + ":" + elemTypeName);

			if (attributes.Count > 0) {
				sb.Append (",");
				for (int i=0; i< (attributes.Count-1); i++) {
					sb.Append (attributes [i] + ",");
				}
			}

			if(attributes.Count > 0)
				sb.Append(attributes[attributes.Count-1]);

			return sb.ToString();
		}


		private string CanonizeEdgeList (INode fromNode, IEdgeList edges)
		{
			//Don't need arbitary, correct?
			StringBuilder sb = new StringBuilder();

			List<string> outgoing = new List<string> ();
			List<string> undirected = new List<string> ();
			List<string> incoming = new List<string> ();

			foreach (var e in edges) {
				if( e.Type.Directedness==Directedness.Directed) {
					if(e.Source==fromNode) {
						outgoing.Add(elementTypeNameMap[e]);
					} else {
						incoming.Add(elementTypeNameMap[e]);
					}
				} else if( e.Type.Directedness==Directedness.Undirected) {
					undirected.Add(elementTypeNameMap[e]);
				}
			}

			outgoing.Sort ();
			undirected.Sort ();
			incoming.Sort ();

			foreach(var es in outgoing)
				sb.Append("|-(" + es + ")->|");

			foreach(var es in undirected) 
				sb.Append("|-(" + es + ")-|");

			foreach(var es in incoming)
				sb.Append("|<-(" + es + ")-|");


			return sb.ToString();
		}
		/// <summary>
		/// Canonize a graph
		/// </summary>
		/// <param name="graph">The graph to canonize.</param>
		public string Canonize (LGSPGraph graph)
		{
			elementTypeNameMap = new Dictionary<IGraphElement, string> ();
			//List of virtual nodes
			coNodes = new List<CoNode> ();
			nodeToCoNodeIndexMap = new Dictionary<INode, int> ();

			typeNames = new List<string> ();
			edgeTypeNames = new List<string> ();

			//Build virtual node(s) for each node and edge
			//Build the vNode -> typeID Map (2 entries for every directed edge class)

			foreach (var n in graph.Nodes) {
				string nodeTypeString = CanonizeElement (n);
				elementTypeNameMap [n] = nodeTypeString;
				if (!typeNames.Contains (nodeTypeString)) {
					typeNames.Add (nodeTypeString);
				}

				CoNode con = new CoNode (n);
				nodeToCoNodeIndexMap [n] = coNodes.Count;

				coNodes.Add (con);
			}

			foreach (var e in graph.Edges) {
				string edgeTypeString = CanonizeElement (e);
				elementTypeNameMap [e] = edgeTypeString;
				if (!edgeTypeNames.Contains (edgeTypeString)) {
					edgeTypeNames.Add (edgeTypeString);
				}
			}

			typeNames.Sort ();
			edgeTypeNames.Sort ();

			typeNameIDMap = new Dictionary<string, long> ();
			edgeTypeNameIDMap = new Dictionary<string, long> ();

			for (int i=0; i<typeNames.Count; i++) {
				string typeName = typeNames [i];
				typeNameIDMap [typeName] = (long)i;
			}
		
			{
				//increment by 3, types reserved for undirected, tail, head
				int index = 0;

				for (int i=0; i<edgeTypeNames.Count; i++) {
					string edgeTypeName = edgeTypeNames [i];
					edgeTypeNameIDMap [edgeTypeName] = index;
					index += 3;
				}
			}

			coNodeTypeIDs = coNodes.ConvertAll (x => 0L);

			foreach (var n in graph.Nodes) {
				int index = nodeToCoNodeIndexMap [n];
				string typeName = elementTypeNameMap [n];
				coNodeTypeIDs [index] = (typeNameIDMap [typeName]);
			}

			nodeNeighborMap = new Dictionary<INode, Dictionary<INode, IEdgeList>> ();

			//Fill in neighbormap
			foreach (var curNode in graph.Nodes) {
				Dictionary<INode, IEdgeList> neighborMap = new Dictionary<INode, IEdgeList> ();
				foreach (var e in curNode.Incident) {
					INode toNode = e.GetOther (curNode);
					if (!neighborMap.ContainsKey (toNode)) {
						neighborMap [toNode] = new IEdgeList ();
					}
					if (!neighborMap [toNode].Contains (e)) {
						neighborMap [toNode].Add (e);
					}
				}
				nodeNeighborMap [curNode] = neighborMap;
			}

			//Now every coNode corresponing to an INode has a correct typeID associated with it ...
			//There is a disjoint set of IDs (starting at 0) reserved in the edgeTypeNameIDMap
			edgeCoNodeTypeIDs = new List<long> ();
			Dictionary<INode, bool> visitedNodeSet = new Dictionary<INode, bool> ();
			connectedComponents = new List<List<int>> ();

			//Partition into connected components
			foreach (var n in graph.Nodes) {
				if (!visitedNodeSet.ContainsKey (n)) {
					visitedNodeSet [n] = true;
					List<int> connectedComponent = new List<int> ();
					BuildConnectedComponent (n, visitedNodeSet, connectedComponent);
					BuildCoNodes(connectedComponent);
					connectedComponents.Add (connectedComponent);
				}
			}


			if (edgeCoNodeTypeIDs.Count != 0) {
				PartialRankOrderList (edgeCoNodeTypeIDs);
				for (int i=0; i<edgeCoNodeTypeIDs.Count; i++) {
					edgeCoNodeTypeIDs [i] += (long)typeNames.Count;
				}
			}
			//Concat the edgeTypeIDs (built in RecurisveCoNodeBuild) to coNodeTypeIDs
			coNodeTypeIDs.AddRange(edgeCoNodeTypeIDs);
			List<String> connectedComponentStrings = new List<String> (connectedComponents.Count);

			foreach (var connectedComponent in connectedComponents) {
				connectedComponentStrings.Add (CanonizeConnectedComponent (connectedComponent));
			}

			connectedComponentStrings.Sort ();

			StringBuilder sb = new StringBuilder ();

			for (int i=0; i<(connectedComponentStrings.Count-1); i++) {
				sb.Append (connectedComponentStrings[i] + ".");
			}

			sb.Append(connectedComponentStrings[connectedComponentStrings.Count-1]);
			return sb.ToString();
		}


		private string CanonizeConnectedComponent (List<int> connectedComponent)
		{
			curConnectedComponenet = connectedComponent;

			nodeSymmIDs = curConnectedComponenet.ConvertAll (x => coNodeTypeIDs [x]);
			curCoIndexer = new Dictionary<int, int> ();
	
			for (int i=0; i< curConnectedComponenet.Count; i++) {	
				curCoIndexer [curConnectedComponenet [i]] = i;
			}
	
			PartialRankOrderList (nodeSymmIDs);  
			nodeSymmIDs = StabilizeSymmIDs (nodeSymmIDs);

			nodeTraversalOrder = nodeSymmIDs.ConvertAll (x => (int)x);
		
			SetNodeTraversalOrder ();
		
			int lowestOrderIndex = nodeTraversalOrder.IndexOf(0);
			lowestOrderNode = coNodes [curConnectedComponenet [lowestOrderIndex]].Parent;

			FindClosures ();
			
			String canonicalString = BuildCanonicalString();
			
			return canonicalString;
		}

		public void BuildConnectedComponent (INode curNode, Dictionary<INode, bool>  visitedNodeSet, List<int> connectedComponent)
		{
			int curIndex = nodeToCoNodeIndexMap [curNode];
			connectedComponent.Add (curIndex);

			Dictionary<INode, IEdgeList> neighborMap = nodeNeighborMap [curNode];

			foreach (var pair in neighborMap) {
				INode toNode = pair.Key;
				
				if(visitedNodeSet.ContainsKey(toNode)) {
					continue;
				} else {
					visitedNodeSet[toNode]=true;
					BuildConnectedComponent(toNode,visitedNodeSet, connectedComponent);
				}
			}
		}

		private void BuildCoNodes (List<int> connectedComponent)
		{
			int nNodes = connectedComponent.Count;
			
			for(int i=0; i<nNodes; i++) {
				int curIndex=connectedComponent[i];
				CoNode cur = coNodes[curIndex];
				INode curNode = cur.Parent;

				for(int j=i; j<nNodes; j++) {
					int toIndex = connectedComponent[j];
				
					CoNode to = coNodes[toIndex];
					INode toNode = to.Parent;
					
					if(!nodeNeighborMap[curNode].ContainsKey(toNode)) {
						continue;
					}

					IEdgeList edges = nodeNeighborMap[curNode][toNode];
					bool compositeEdgeIsDirected = false;
					
					foreach(var e in edges) {
						if(e.Type.Directedness == Directedness.Directed) {
							compositeEdgeIsDirected=true;
						}
					}
					
					if(compositeEdgeIsDirected) {
						//create two coNodes
						CoNode nearCur = new CoNode();
						CoNode nearTo = new CoNode();
						
						int nearCurIndex = coNodes.Count;
						int nearToIndex = coNodes.Count+1;
						
						coNodes.Add(nearCur);
						coNodes.Add(nearTo);

						connectedComponent.Add(nearCurIndex);
						connectedComponent.Add(nearToIndex);
						
						long nearCurTypeID = 1L;
						long nearToTypeID = 1L;
						
						foreach(var e in edges) {
							int baseEdgeTypeID = (int) edgeTypeNameIDMap[elementTypeNameMap[e]];
							
							if(e.Type.Directedness==Directedness.Directed) {
								if(e.Source==curNode) {
									nearCurTypeID *= GetPrime(baseEdgeTypeID +(int)CoEdgeTypeOffsets.Head);
									nearToTypeID *= GetPrime(baseEdgeTypeID + (int)CoEdgeTypeOffsets.Tail);
								} else if(e.Target==curNode) {
									nearCurTypeID *= GetPrime(baseEdgeTypeID + (int)CoEdgeTypeOffsets.Tail);
									nearToTypeID *= GetPrime(baseEdgeTypeID + (int)CoEdgeTypeOffsets.Head);
								}
							} else if (e.Type.Directedness==Directedness.Undirected) {
								long edgePrime = GetPrime(baseEdgeTypeID);
								nearCurTypeID *= edgePrime;
								nearToTypeID *= edgePrime;
							}
							else {
								throw new Exception ("Unsupported directedness type");
							}
						}
						//fill in typeids
						edgeCoNodeTypeIDs.Add(nearCurTypeID);
						edgeCoNodeTypeIDs.Add(nearToTypeID);
						
						//fill in neighbor lists
						cur.Neighbors.Add (nearCurIndex);
						nearCur.Neighbors.Add (curIndex);
						
						nearCur.Neighbors.Add (nearToIndex);
						nearTo.Neighbors.Add (nearCurIndex);
						
						nearTo.Neighbors.Add (toIndex);
						to.Neighbors.Add (nearToIndex);
					
					} else {
						//create just one coNode representing the undirected edge
						CoNode middle = new CoNode();
						
						int middleIndex = coNodes.Count;
						coNodes.Add(middle);
						
						connectedComponent.Add(middleIndex);
						
						long ueTypeID = 1L;
						
						foreach(var e in edges) {
							long baseEdgeTypeID = GetPrime((int) edgeTypeNameIDMap[elementTypeNameMap[e]]);
							ueTypeID *= baseEdgeTypeID;
						}
						
						edgeCoNodeTypeIDs.Add(ueTypeID);
						
						//fill in the neighbors
						coNodes[curIndex].Neighbors.Add(middleIndex);
						coNodes[toIndex].Neighbors.Add(middleIndex);
						
						coNodes[middleIndex].Neighbors.Add(curIndex);
						coNodes[middleIndex].Neighbors.Add(toIndex);
					}
				}
			}
		}

		
		private void PartialRankOrderList (List<long> list)
		{
			List<Pair<long,int>> rankList = new List<Pair<long, int>> ();
				
			{
				int index = 0;
				foreach (var i in list) {		
					rankList.Add (new Pair<long,int>(i, index));
					++index;
				}
			}

			rankList.Sort((x,y) => x.fst.CompareTo(y.fst));

			{
				int index=0;
				long old=rankList[0].fst;

				for(int i=0; i<rankList.Count; i++)
				{
					Pair<long,int> pair = rankList[i];

					if(old!=pair.fst) {
						index++;
					}

					old = pair.fst;
					list [pair.snd] = index;
				}
			}
		}


		private List<long> StabilizeSymmIDs (List<long> inputNodeSymmIDs)
		{
			while (true) {
				List<long> newNodeSymmIDs = inputNodeSymmIDs.ConvertAll (x => GetPrime ((int) x));
				newNodeSymmIDs = NeighborProduct (newNodeSymmIDs);
				newNodeSymmIDs=BreakRankTies(inputNodeSymmIDs,newNodeSymmIDs);
				bool stabilized=true;
				for(int i=0; i<inputNodeSymmIDs.Count; i++) if(inputNodeSymmIDs[i] != newNodeSymmIDs[i]) stabilized = false;
				if(stabilized) {
					return newNodeSymmIDs;
				}
				inputNodeSymmIDs=newNodeSymmIDs;
			}
		}


		private List<long> NeighborProduct (List<long> inputNodeSymmIDs)
		{
			List<long> newNodeSymmIDs = new List<long> (inputNodeSymmIDs);

			for(int i=0; i<inputNodeSymmIDs.Count; i++) {
				long product = 1L;
				List<int> neighborIndexes = coNodes[curConnectedComponenet[i]].Neighbors;
				
				for(int j=0; j<neighborIndexes.Count; j++) {
					long nodeToSymmID = inputNodeSymmIDs[curCoIndexer[neighborIndexes[j]]];
					product *= nodeToSymmID;
				}
				newNodeSymmIDs[i]=product;
			}

			return newNodeSymmIDs;
		}

	
		private List<long> BreakRankTies (List<long> oldNodeSymmIDs, List<long> newNodeSymmIDs)
		{
			List<long> tieBrokenSymmIDs = new List<long> (oldNodeSymmIDs);

			//would be much prettier with tuples
			List<Pair<long,int>> sortedMap = new List<Pair<long, int>> ();

			for (int i=0; i<oldNodeSymmIDs.Count; i++) {
				sortedMap.Add (new Pair<long, int> (oldNodeSymmIDs [i], i));
			}

			sortedMap.Sort ((x,y) =>
			                {
								int result = x.fst.CompareTo (y.fst);
								return (result!=0) ? result : newNodeSymmIDs[x.snd].CompareTo(newNodeSymmIDs[y.snd]);
							});

			long lastOldID = -1, lastNewID = -1, recountID = -1;

			for (int i=0; i<sortedMap.Count; i++) {
				int ti = sortedMap [i].snd;

				long oldID = oldNodeSymmIDs [ti];
				long newID = newNodeSymmIDs [ti];

				if (oldID != lastOldID) {
					lastOldID = oldID;
					lastNewID = newID;
					++recountID;
				} else if (newID != lastNewID) {
					lastNewID = newID;
					++recountID;
				}
				tieBrokenSymmIDs [ti] = recountID;
			}

			return tieBrokenSymmIDs;	
		}


		private long GetPrime (int i)
		{
			if (i < primes.Count) {
				return primes [i];
			}
			else {
				FindPrimesTo (i);
				return primes [i];
			}
		}


		static private void FindPrimesTo (int i)
		{
			long nextPrime = primes [primes.Count - 1] + 1;
			while (primes.Count < i) {
				long primeLimit = nextPrime * nextPrime;
				bool isPrime = true;
				foreach (var prime in primes) {
					if (prime > primeLimit)
						break;
					if (nextPrime % prime == 0) {	
						isPrime = false;
						break;
					}
				}

				if (isPrime)
					primes.Add (nextPrime);

				++nextPrime;
			}
		}

		//nodeTraversalOrder should be set to nodeSymmIDs to start with
		private void SetNodeTraversalOrder ()
		{
			while (true) {

				int pos = FindIndexOfLowestTraversalOrderTie(nodeTraversalOrder);
			
				if(pos == -1) {
					return;
				}
				
				for(int i=0; i<nodeTraversalOrder.Count; i++)
					nodeTraversalOrder[i]=nodeTraversalOrder[i]*2+1;	//why do we 2x here instead of +1?
				
				nodeTraversalOrder[pos]=nodeTraversalOrder[pos]-1;
				
				nodeTraversalOrder=StabilizeSymmIDs(nodeTraversalOrder.ConvertAll(x => (long) x)).ConvertAll (x => (int) x);
			}
		}
		
		
		private int FindIndexOfLowestTraversalOrderTie (List<int> traversalOrder)
		{
			List<Pair<int,int>> stableOrder = new List<Pair<int, int>> (traversalOrder.Count);
			for (int i=0; i<traversalOrder.Count; i++) {
				stableOrder.Add (new Pair<int, int> (traversalOrder [i], i));
			}

			stableOrder.Sort ((x,y) => x.fst.CompareTo (y.fst));

			int lowestTieValue = stableOrder[0].fst;
			for (int i=1; i<stableOrder.Count; i++) {
				if (stableOrder [i].fst == lowestTieValue) {
					return stableOrder [i - 1].snd;
				}
				lowestTieValue = stableOrder [i].fst;
			}

			return -1;
		}


		private void FindClosures ()
		{	
			closureNodes = new Dictionary<INode, int> ();

			INode prevNode = null;
			INode startNode = lowestOrderNode;
			Dictionary<INode,bool> visitedNodeSet = new Dictionary<INode, bool> ();

			sortedNodeNeighborMap = new Dictionary<INode, List<Pair<INode,List<IEdge>>>> ();

			FindClosures(prevNode, startNode, visitedNodeSet);
		}


		private void FindClosures (INode prevNode, INode curNode, Dictionary<INode, bool> visitedNodeSet)
		{
			visitedNodeSet [curNode] = true;

			Dictionary<INode, IEdgeList> neighborMap = nodeNeighborMap [curNode];
			List<INode> neighborNodes = new List<INode> (neighborMap.Keys);

			List<int> neighborCoNodes = new List<int> (coNodes [nodeToCoNodeIndexMap [curNode]].Neighbors);
			neighborCoNodes.Sort ();

			neighborNodes.Sort ((x,y) => (nodeTraversalOrder [curCoIndexer [nodeToCoNodeIndexMap [x]]].CompareTo (nodeTraversalOrder [curCoIndexer [nodeToCoNodeIndexMap [y]]])));
			List<Pair<INode, IEdgeList>> neighborList = neighborNodes.ConvertAll (x => new Pair<INode, IEdgeList> (x, neighborMap [x]));

			if (prevNode != null) {
				int prevIndex = 0;

				for (int i=0; i< neighborList.Count; i++) {
					if (neighborList [i].fst == prevNode) {
						prevIndex = i;
						break;
					}
				}

				neighborList.RemoveAt (prevIndex);
			}

			Dictionary<int,bool> pruneIndexSet = new Dictionary<int, bool> ();

			for (int i=0; i<neighborList.Count; i++) {
				INode toNode = neighborList [i].fst;

				if (closureNodes.ContainsKey (curNode)) {
					pruneIndexSet[i]=true;
					continue;
				} else if (visitedNodeSet.ContainsKey (toNode) && (!closureNodes.ContainsKey (toNode))) {
					closureNodes [toNode] = 0;
					continue;
				} else if (!visitedNodeSet.ContainsKey (toNode)) {
					FindClosures (curNode, toNode, visitedNodeSet);
					continue;
				} else {
					continue;
				}
			}

			List<Pair<INode, IEdgeList>> newNeighborList = new List<Pair<INode, IEdgeList>> ();

			for (int i=0; i< neighborList.Count; i++) {
				if(!pruneIndexSet.ContainsKey(i)) {
					newNeighborList.Add (neighborList[i]);
				}
			}
			 
			sortedNodeNeighborMap [curNode] = newNeighborList;

		}

		private string BuildCanonicalString ()
		{
			INode startNode = lowestOrderNode;
			Dictionary<INode,bool> visitedNodeSet = new Dictionary<INode, bool> ();
		
			StringBuilder sb = new StringBuilder();
			BuildCanonicalString(startNode, sb, visitedNodeSet);

			return sb.ToString();
		}
		
		
		private void BuildCanonicalString (INode curNode, StringBuilder sb, Dictionary<INode,bool> visitedNodeSet)
		{
			visitedNodeSet [curNode] = true;

			sb.Append ("(");
		
			if (closureNodes.ContainsKey (curNode)) {
				bool isDeclaration = false;
				if (closureNodes [curNode] == 0) {
					curClosure++;
					closureNodes [curNode] = curClosure;
					isDeclaration = true;
				}

				if(isDeclaration) {
				  sb.Append (elementTypeNameMap [curNode] + "," + closureNodes [curNode]);
				}
				else {
					sb.Append (closureNodes [curNode] + ")");
					return;
				}

			} else {
				sb.Append (elementTypeNameMap [curNode]);
			}
			sb.Append (")");

			List<Pair<INode,List<IEdge>>> sortedNeighborList = sortedNodeNeighborMap [curNode];

			if (sortedNeighborList.Count > 1) {
				for (int i=0; i<sortedNeighborList.Count-1; i++) {
					INode toNode = sortedNeighborList [i].fst;

					if (visitedNodeSet.ContainsKey(toNode) && !closureNodes.ContainsKey(toNode)) {
						continue;
					}

					IEdgeList edgeList = sortedNeighborList [i].snd;

					sb.Append ("{" + CanonizeEdgeList (curNode, edgeList));

					BuildCanonicalString (toNode, sb, visitedNodeSet);

					sb.Append ("}");
				}
			}

			if (sortedNeighborList.Count > 0) {
				INode lastToNode = sortedNeighborList [sortedNeighborList.Count - 1].fst;

				IEdgeList lastEdgeList = sortedNeighborList [sortedNeighborList.Count - 1].snd;
				sb.Append (CanonizeEdgeList (curNode, lastEdgeList));
				BuildCanonicalString (lastToNode, sb, visitedNodeSet);
			}
		}

		static private List<long> primes=new List<long>() {
			2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41,
			43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97,
			101, 103, 107, 109, 113, 127, 131, 137, 139, 149,
			151, 157, 163, 167, 173, 179, 181, 191, 193, 197,
			199, 211, 223, 227, 229, 233, 239, 241, 251, 257,
			263, 269, 271, 277, 281, 283, 293, 307, 311, 313,
			317, 331, 337, 347, 349, 353, 359, 367, 373, 379,
			383, 389, 397, 401, 409, 419, 421, 431, 433, 439,
			443, 449, 457, 461, 463, 467, 479, 487, 491, 499,
			503, 509, 521, 523, 541, 547, 557, 563, 569, 571,
			577, 587, 593, 599, 601, 607, 613, 617, 619, 631,
			641, 643, 647, 653, 659, 661, 673, 677, 683, 691,
			701, 709, 719, 727, 733, 739, 743, 751, 757, 761,
			769, 773, 787, 797, 809, 811, 821, 823, 827, 829,
			839, 853, 857, 859, 863, 877, 881, 883, 887, 907,
			911, 919, 929, 937, 941, 947, 953, 967, 971, 977,
			983, 991, 997, 1009, 1013, 1019, 1021, 1031, 1033,
			1039, 1049, 1051, 1061, 1063, 1069, 1087, 1091,
			1093, 1097, 1103, 1109, 1117, 1123, 1129, 1151,
			1153, 1163, 1171, 1181, 1187, 1193, 1201, 1213,
			1217, 1223, 1229, 1231, 1237, 1249, 1259, 1277,
			1279, 1283, 1289, 1291, 1297, 1301, 1303, 1307,
			1319, 1321, 1327, 1361, 1367, 1373, 1381, 1399,
			1409, 1423, 1427, 1429, 1433, 1439, 1447, 1451,
			1453, 1459, 1471, 1481, 1483, 1487, 1489, 1493,
			1499, 1511, 1523, 1531, 1543, 1549, 1553, 1559,
			1567, 1571, 1579, 1583, 1597, 1601, 1607, 1609,
			1613, 1619, 1621, 1627, 1637, 1657, 1663, 1667,
			1669, 1693, 1697, 1699, 1709, 1721, 1723, 1733,
			1741, 1747, 1753, 1759, 1777, 1783, 1787, 1789,
			1801, 1811, 1823, 1831, 1847, 1861, 1867, 1871,
			1873, 1877, 1879, 1889, 1901, 1907, 1913, 1931,
			1933, 1949, 1951, 1973, 1979, 1987, 1993, 1997,
			1999, 2003, 2011, 2017, 2027, 2029, 2039, 2053,
			2063, 2069, 2081, 2083, 2087, 2089, 2099, 2111,
			2113, 2129, 2131, 2137, 2141, 2143, 2153, 2161,
			2179, 2203, 2207, 2213, 2221, 2237, 2239, 2243,
			2251, 2267, 2269, 2273, 2281, 2287, 2293, 2297,
			2309, 2311, 2333, 2339, 2341, 2347, 2351, 2357,
			2371, 2377, 2381, 2383, 2389, 2393, 2399, 2411,
			2417, 2423, 2437, 2441, 2447, 2459, 2467, 2473,
			2477, 2503, 2521, 2531, 2539, 2543, 2549, 2551,
			2557, 2579, 2591, 2593, 2609, 2617, 2621, 2633,
			2647, 2657, 2659, 2663, 2671, 2677, 2683, 2687,
			2689, 2693, 2699, 2707, 2711, 2713};

	}
}

