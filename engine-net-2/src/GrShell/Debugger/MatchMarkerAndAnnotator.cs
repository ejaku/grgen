/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.grShell
{
    class MatchMarkerAndAnnotator
    {
        readonly ElementRealizers realizers;
        readonly GraphAnnotationAndChangesRecorder renderRecorder = null;
        readonly YCompClient ycompClient = null;


        public MatchMarkerAndAnnotator(ElementRealizers realizers, GraphAnnotationAndChangesRecorder renderRecorder, YCompClient ycompClient)
        {
            this.realizers = realizers;
            this.renderRecorder = renderRecorder;
            this.ycompClient = ycompClient;
        }

        public void Mark(int rule, int match, SequenceSomeFromSet seq)
        {
            if(seq.IsNonRandomRuleAllCall(rule))
            {
                MarkMatches(seq.Matches[rule], realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
                AnnotateMatches(seq.Matches[rule], true);
            }
            else
            {
                MarkMatch(seq.Matches[rule].GetMatch(match), realizers.MatchedNodeRealizer, realizers.MatchedEdgeRealizer);
                AnnotateMatch(seq.Matches[rule].GetMatch(match), true);
            }
        }

        public void Unmark(int rule, int match, SequenceSomeFromSet seq)
        {
            if(seq.IsNonRandomRuleAllCall(rule))
            {
                MarkMatches(seq.Matches[rule], null, null);
                AnnotateMatches(seq.Matches[rule], false);
            }
            else
            {
                MarkMatch(seq.Matches[rule].GetMatch(match), null, null);
                AnnotateMatch(seq.Matches[rule].GetMatch(match), false);
            }
        }

        public void MarkMatch(IMatch match, String nodeRealizerName, String edgeRealizerName)
        {
            foreach(INode node in match.Nodes)
            {
                ycompClient.ChangeNode(node, nodeRealizerName);
            }
            foreach(IEdge edge in match.Edges)
            {
                ycompClient.ChangeEdge(edge, edgeRealizerName);
            }
            MarkMatches(match.EmbeddedGraphs, nodeRealizerName, edgeRealizerName);
            foreach(IMatches iteratedsMatches in match.Iterateds)
                MarkMatches(iteratedsMatches, nodeRealizerName, edgeRealizerName);
            MarkMatches(match.Alternatives, nodeRealizerName, edgeRealizerName);
            MarkMatches(match.Independents, nodeRealizerName, edgeRealizerName);
        }

        public void MarkMatches(IEnumerable<IMatch> matches, String nodeRealizerName, String edgeRealizerName)
        {
            foreach(IMatch match in matches)
            {
                MarkMatch(match, nodeRealizerName, edgeRealizerName);
            }
        }

        public void AnnotateMatch(IMatch match, bool addAnnotation)
        {
            AnnotateMatch(match, addAnnotation, "", 0, true);
            if(addAnnotation)
            {
                renderRecorder.AnnotateGraphElements(ycompClient);
            }
        }

        public void AnnotateMatches(IEnumerable<IMatch> matches, bool addAnnotation)
        {
            AnnotateMatches(matches, addAnnotation, "", 0, true);
            if(addAnnotation)
            {
                renderRecorder.AnnotateGraphElements(ycompClient);
            }
        }

        private void AnnotateMatches(IEnumerable<IMatch> matches, bool addAnnotation, string prefix, int nestingLevel, bool topLevel)
        {
            foreach(IMatch match in matches)
            {
                AnnotateMatch(match, addAnnotation, prefix, nestingLevel, topLevel);
            }
        }

        private void AnnotateMatch(IMatch match, bool addAnnotation, string prefix, int nestingLevel, bool topLevel)
        {
            const int PATTERN_NESTING_DEPTH_FROM_WHICH_ON_TO_CLIP_PREFIX = 7;

            for(int i = 0; i < match.NumberOfNodes; ++i)
            {
                INode node = match.getNodeAt(i);
                IPatternNode patternNode = match.Pattern.Nodes[i];
                if(addAnnotation)
                {
                    if(patternNode.PointOfDefinition == match.Pattern
                        || patternNode.PointOfDefinition == null && topLevel)
                    {
                        String name = match.Pattern.Nodes[i].UnprefixedName;
                        if(nestingLevel > 0)
                        {
                            if(nestingLevel < PATTERN_NESTING_DEPTH_FROM_WHICH_ON_TO_CLIP_PREFIX)
                                name = prefix + "/" + name;
                            else
                                name = "/|...|=" + nestingLevel + "/" + name;
                        }
                        renderRecorder.AddNodeAnnotation(node, name);
                    }
                }
                else
                {
                    ycompClient.AnnotateElement(node, null);
                    renderRecorder.RemoveNodeAnnotation(node);
                }
            }
            for(int i = 0; i < match.NumberOfEdges; ++i)
            {
                IEdge edge = match.getEdgeAt(i);
                IPatternEdge patternEdge = match.Pattern.Edges[i];
                if(addAnnotation)
                {
                    if(patternEdge.PointOfDefinition == match.Pattern
                        || patternEdge.PointOfDefinition == null && topLevel)
                    {
                        String name = match.Pattern.Edges[i].UnprefixedName;
                        if(nestingLevel > 0)
                        {
                            if(nestingLevel < PATTERN_NESTING_DEPTH_FROM_WHICH_ON_TO_CLIP_PREFIX)
                                name = prefix + "/" + name;
                            else
                                name = "/|...|=" + nestingLevel + "/" + name;
                        }
                        renderRecorder.AddEdgeAnnotation(edge, name);
                    }
                }
                else
                {
                    ycompClient.AnnotateElement(edge, null);
                    renderRecorder.RemoveEdgeAnnotation(edge);
                }
            }
            AnnotateSubpatternMatches(match, addAnnotation, prefix, nestingLevel);
            AnnotateIteratedsMatches(match, addAnnotation, prefix, nestingLevel);
            AnnotateAlternativesMatches(match, addAnnotation, prefix, nestingLevel);
            AnnotateIndependentsMatches(match, addAnnotation, prefix, nestingLevel);
        }

        private void AnnotateSubpatternMatches(IMatch parentMatch, bool addAnnotation, string prefix, int nestingLevel)
        {
            IPatternGraph pattern = parentMatch.Pattern;
            IEnumerable<IMatch> matches = parentMatch.EmbeddedGraphs;
            int i = 0;
            foreach(IMatch match in matches)
            {
                AnnotateMatch(match, addAnnotation, prefix + "/" + pattern.EmbeddedGraphs[i].Name, nestingLevel + 1, true);
                ++i;
            }
        }

        private void AnnotateIteratedsMatches(IMatch parentMatch, bool addAnnotation, string prefix, int nestingLevel)
        {
            IPatternGraph pattern = parentMatch.Pattern;
            IEnumerable<IMatches> iteratedsMatches = parentMatch.Iterateds;
            int numIterated, numOptional, numMultiple, numOther;
            ClassifyIterateds(pattern, out numIterated, out numOptional, out numMultiple, out numOther);

            int i = 0;
            foreach(IMatches matches in iteratedsMatches)
            {
                String name;
                if(pattern.Iterateds[i].MinMatches == 0 && pattern.Iterateds[i].MaxMatches == 0) {
                    name = "(.)*";
                    if(numIterated > 1)
                        name += "'" + i;
                } else if(pattern.Iterateds[i].MinMatches == 0 && pattern.Iterateds[i].MaxMatches == 1) {
                    name = "(.)?";
                    if(numOptional > 1)
                        name += "'" + i;
                } else if(pattern.Iterateds[i].MinMatches == 1 && pattern.Iterateds[i].MaxMatches == 0) {
                    name = "(.)+";
                    if(numMultiple > 1)
                        name += "'" + i;
                } else {
                    name = "(.)[" + pattern.Iterateds[i].MinMatches + ":" + pattern.Iterateds[i].MaxMatches + "]";
                    if(numOther > 1)
                        name += "'" + i;
                }

                int j = 0;
                foreach(IMatch match in matches)
                {
                    AnnotateMatch(match, addAnnotation, prefix + "/" + name + "/" + j, nestingLevel + 1, false);
                    ++j;
                }

                ++i;
            }
        }

        private void AnnotateAlternativesMatches(IMatch parentMatch, bool addAnnotation, string prefix, int nestingLevel)
        {
            IPatternGraph pattern = parentMatch.Pattern;
            IEnumerable<IMatch> matches = parentMatch.Alternatives;
            int i = 0;
            foreach(IMatch match in matches)
            {
                String name = "(.|.)";
                if(pattern.Alternatives.Length>1)
                    name += "'" + i;
                String caseName = match.Pattern.Name;
                AnnotateMatch(match, addAnnotation, prefix + "/" + name + "/" + caseName, nestingLevel + 1, false);
                ++i;
            }
        }

        private void AnnotateIndependentsMatches(IMatch parentMatch, bool addAnnotation, string prefix, int nestingLevel)
        {
            IPatternGraph pattern = parentMatch.Pattern;
            IEnumerable<IMatch> matches = parentMatch.Independents;
            int i = 0;
            foreach(IMatch match in matches)
            {
                String name = "&(.)";
                if(pattern.IndependentPatternGraphs.Length>1)
                    name += "'" + i;
                AnnotateMatch(match, addAnnotation, prefix + "/" + name, nestingLevel + 1, false);
                ++i;
            }
        }

        private static void ClassifyIterateds(IPatternGraph pattern, out int numIterated, out int numOptional, out int numMultiple, out int numOther)
        {
            numIterated = numOptional = numMultiple = numOther = 0;
            for(int i = 0; i < pattern.Iterateds.Length; ++i)
            {
                if(pattern.Iterateds[i].MinMatches == 0 && pattern.Iterateds[i].MaxMatches == 0)
                    ++numIterated;
                else if(pattern.Iterateds[i].MinMatches == 0 && pattern.Iterateds[i].MaxMatches == 1)
                    ++numOptional;
                else if(pattern.Iterateds[i].MinMatches == 1 && pattern.Iterateds[i].MaxMatches == 0)
                    ++numMultiple;
                else
                    ++numOther;
            }
        }
    }
}
