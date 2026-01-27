<!-- by Edgar Jakumeit -->
The graph model should be rendered in graph form similar to an UML class diagram.
This means: an UML-like-graph-node per node class, or edge class, or object class or transient object class, or enum.
And inherits-from UML-like-graph-edges from the sub to the super-classes.
If they are contained in packages, they are to be rendered nested inside a package node (with their non-package-prefixed name then).
UML stereotypes are to be shown for abstract classes as well as for enums and packages; methods are not to be shown.
This rendering is to be displayed upon invocation of a gshow model command in the GrShell. 
With the configured graph viewer, utilizing the basic graph rendering interface.
