<!-- by Edgar Jakumeit -->
The patterns (which are pattern graphs) of the actions (rules/tests containing a rule pattern) and the used subpatterns should be rendered in graph form.
Pattern nodes are to be rendered as nodes, and pattern edges are to be rendered as special nodes with an incoming tentacle edge from the source node and an outgoing tentacle edge to the target node, also pattern variables are to be rendered as nodes.
The pattern elements (nodes, edges, variables) are to be rendered inside top-level group nodes of the containing actions or subpatterns; these should be rendered inside package nodes if packages are used.
The pattern graphs contain also nested patterns, in the form of iterated and alternative patterns, as well as negative and independent patterns, they are to be visualized by graph nesting (inside group nodes), too.
Nested patterns are to be rendered as nodes with a contained pattern graph; in case of an alternative, the group node for the alternative contains multiple group nodes for the different alternative cases, each in turn contains its specific pattern graph.
The pattern graphs contain also embedded graphs of the used subpatterns.
Subpattern usages (embedded graphs) are to be rendered as nodes pointing to their used subpattern (by special edges).
A pattern element is only to be rendered in the top-down first pattern it is defined.

Pattern nodes and edges should be labeled with name ":" type.
Variable nodes should receive a label of the form "var" name ":" type in case of a basic type, or of the form "ref" name ":" type otherwise.
Actions, subpatterns, and packages should be labeled with their name and a rule/subpattern/package stereotype.
Negatives, independents, and alternatives should be labeled with a negative/independent/alternative stereotype.
Alternative cases should be rendered with their name only.
An iterated with min 0 and max 1 is to be stereotyped with optional, an iterated with min 1 and max 0 is to be stereotyped with multiple, an iterated with min 0 and max 0 is to be stereotyped with iterated, otherwise min..max should be used.
A subpattern usage node is to be labeled with a label usage-name ":" subpattern-name.
A pattern element that is def-to-be-yielded to is to be rendered with a "def" prefix before the label.

In case of dangling edges, omit the tentacle edges of the missing nodes (an alternative would be to render edges as edges, then the rendering of patterns would be similar to the rendering of the pattern matches, which would be good, but this is not wanted, as it may occur that nested patterns define only edges (to upper-level nodes), and edges alone cannot be added to a group node).

This rendering is to be displayed upon invocation of a gshow patterns command in the GrShell.
With the configured graph viewer, utilizing the basic graph rendering interface.
