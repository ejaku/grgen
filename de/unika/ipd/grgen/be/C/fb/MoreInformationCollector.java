/**
 * Extracts all the information needed by the FrameBasedBackend
 * from the GrGen-internal IR
 *
 * @author Adam Szalkowski
 * @version $Id$
 */

package de.unika.ipd.grgen.be.C.fb;
import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.be.C.FrameBasedBackend;
import java.util.Iterator;
import java.util.Map;
import java.util.HashMap;
import java.util.TreeSet;
import java.util.Collection;
import de.unika.ipd.grgen.util.Attributes;
import de.unika.ipd.grgen.be.C.CBackend;
import java.io.PrintStream;
import java.util.Vector;
import java.util.Comparator;
import java.util.HashSet;
import de.unika.ipd.grgen.ast.AssignNode;
import de.unika.ipd.grgen.ir.Operator;

public class MoreInformationCollector extends InformationCollector
{

	/* maps an eval list to the action_id it belong to */
	protected Map evalListMap = new HashMap();

	/* replacement and pattern edges involved in an eval */
	protected Map evalInvolvedNodes = new HashMap();
	protected Map evalInvolvedEdges = new HashMap();
	
	/* maps action id to eval list */
	protected Map evalActions = new HashMap();

	/* edge and node attributes involved in that eval */
	protected Map[] involvedEvalNodeAttrIds;
	protected Map[] involvedEvalEdgeAttrIds;

	//returns id of corresponding pattern edge id if edge is kept
	//else -1 if edge is new one
	//usage: replacementEdgeIsPresevedNode[act_id][replacement_edge_num]
	protected int replacementEdgeIsPreservedEdge[][];
	
	//returns id of corresponding replacement edge id if edge is kept
	//else -1 if edge is to be deleted
	//usage: patternEdgeIsToBeKept[act_id][pattern_edge_num]
	protected int patternEdgeIsToBeKept[][];

	
	/*
	 * collect some information about evals
	 *
	 */
	protected void collectEvalInfo()
	{
		involvedEvalNodeAttrIds = new Map[ actionMap.size() ];
		involvedEvalEdgeAttrIds = new Map[ actionMap.size() ];
		
		for(Iterator it = actionMap.keySet().iterator(); it.hasNext(); ) {
			//get the current action
			Action act = (Action) it.next();

			if (act instanceof Rule) {
				Rule rule = (Rule) act;
				Integer act_id = (Integer)actionMap.get(act);
				
				Collection rule_evals = rule.getEvals();
				
				evalListMap.put( rule_evals, act_id );
				evalActions.put( rule_evals, act );
				
				Collection involvedNodes = new HashSet();
				Collection involvedEdges = new HashSet();
				involvedEvalNodeAttrIds[ act_id.intValue() ] = new HashMap();
				involvedEvalEdgeAttrIds[ act_id.intValue() ] = new HashMap();
				
				for(  Iterator it2 = rule_evals.iterator(); it2.hasNext(); ) {
					Assignment eval = (Assignment)it2.next();
					Qualification target = eval.getTarget();
					Expression expr = eval.getExpression();
					
					/* generate an expression that consists of both parts of the Assignment to use the already implemented methods for gathering InvolvedNodes/Edges etc. */
					Operator op = new Operator( (PrimitiveType)target.getType(), Operator.EQ );
					op.addOperand( target );
					op.addOperand( expr );

					//...extract the pattern nodes and edges involved in the evaluation
					involvedNodes.addAll( collectInvolvedNodes( op ) );
					involvedEdges.addAll( collectInvolvedEdges( op ) );
				
					/* for all evaluations the pairs (pattern_node_num, attr_id), which occur
					   in qualifications at the leaves of the eval, are needed.
					   To obtain that compute a map
						  act_id -> pattern_node_num_ -> { attr_ids }
					   implemented by an Array of Maps; usage is:
						  involvedPatternNodeAttrIds[act_id].get(pattern_node_num)
					   which yields a Collection of attr-ids.
					 */

					//collect the attr ids in dependency of evaluation and the pattern node

					//descent to the conditions leaves and look for qualifications
					__recursive_qual_collect( act_id.intValue(),
											 involvedEvalNodeAttrIds[ act_id.intValue() ],
											 involvedEvalEdgeAttrIds[ act_id.intValue() ],
											 op );
				}
				
				//add Collections of involved Nodes/Edges to prepared Maps
				evalInvolvedNodes.put( rule_evals, involvedNodes );
				evalInvolvedEdges.put( rule_evals, involvedEdges );
			}
		}
	}
	
	/**
	 * Method collectReplacementEdgeIsPreservedEdgeInfo
	 *
	 */
	private void collectReplacementEdgeIsPreservedEdgeInfo()
	{
		replacementEdgeIsPreservedEdge =
			new int[n_graph_actions][max_n_replacement_edges];
		
		//init the array with -1
		for (int i = 0; i < n_graph_actions; i++)
			for (int j = 0; j < max_n_replacement_edges; j++)
				replacementEdgeIsPreservedEdge[i][j] = -1;
		
		//for all edges preserved set the corresponding array entry to the
		//appropriate pattern edge number
		Iterator act_it = actionMap.keySet().iterator();
		for ( ; act_it.hasNext() ; ) {
			MatchingAction action = (MatchingAction) act_it.next();
			int act_id = ((Integer) actionMap.get(action)).intValue();
			
			if (action instanceof Rule) {
				//compute the set of replacement edges preserved by this action
				Collection replacement_edges_preserved = new HashSet();
				replacement_edges_preserved.addAll(
					((Rule) action).getRight().getEdges() );
				replacement_edges_preserved.retainAll(action.getPattern().getEdges());
				//for all those preserved replacement edges store the
				//corresponding pattern edge
				Iterator preserved_edge_it =
					replacement_edges_preserved.iterator();
				for ( ; preserved_edge_it.hasNext() ; ) {
					Edge edge = (Edge) preserved_edge_it.next();
					int edge_num =
						((Integer) replacement_edge_num[act_id].get(edge)).intValue();
					replacementEdgeIsPreservedEdge[act_id][edge_num] =
						((Integer) pattern_edge_num[act_id].get(edge)).intValue();
				}
			}
		}
		
	}
	
	/**
	 * Method collectPatternEdgesToBeKeptInfo
	 *
	 */
	private void collectPatternEdgesToBeKeptInfo()
	{
		patternEdgeIsToBeKept = new int[n_graph_actions][max_n_pattern_edges];
		
		//init the arrays with -1
		for (int i = 0; i < n_graph_actions; i++)
			for (int j = 0; j < max_n_pattern_edges; j++)
				patternEdgeIsToBeKept[i][j] = -1;

		//for all edges to be kept set the corresponding array entry to the
		//appropriate replacement edge number
		Iterator act_it = actionMap.keySet().iterator();
		for ( ; act_it.hasNext() ; ) {
			MatchingAction action = (MatchingAction) act_it.next();
			int act_id = ((Integer) actionMap.get(action)).intValue();
			
			//compute the set of pattern edges to be kept for this action
			Collection pattern_edges_to_keep = new HashSet();
			pattern_edges_to_keep.addAll(action.getPattern().getEdges());
			if (action instanceof Rule) {
				Graph replacement = ((Rule)action).getRight();
				pattern_edges_to_keep.retainAll(replacement.getEdges());
				//iterate over the pattern edges to be kept and store their
				//corresponding replacement edge number
				Iterator kept_edge_it = pattern_edges_to_keep.iterator();
				for ( ; kept_edge_it.hasNext() ; ) {
					Edge edge = (Edge) kept_edge_it.next();
					int edge_num =
						((Integer) pattern_edge_num[act_id].get(edge)).intValue();
					patternEdgeIsToBeKept[act_id][edge_num] =
						((Integer) replacement_edge_num[act_id].get(edge)).intValue();
				}
			}
		}
	}

    protected void collectActionInfo()
    {
		super.collectActionInfo();
	    collectPatternEdgesToBeKeptInfo();
	    collectReplacementEdgeIsPreservedEdgeInfo();
    }
}

