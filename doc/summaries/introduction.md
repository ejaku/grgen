# Introduction

- GrGen.NET is a tool for graph-based pattern matching and transformation
- Use when: data is naturally graph-structured, transformations are pattern-based (compiler construction, model transformation, computer linguistics, network analysis)
- Not ideal for: purely numerical computation, simple string processing, or problems without graph structure
- Core concept: rules specify a left-hand side (LHS) pattern to match and a right-hand side (RHS) replacement
- Matching semantics: injective by default (each graph element matched at most once), based on graph morphisms
- Rewriting semantics: SPO (Single Pushout) by default -- dangling edges are deleted automatically
- Visual debugging: work directly on a visualization of your graph and rule applications; graph viewers (yComp, MSAGL) and the step-wise debugger are central to the development workflow
