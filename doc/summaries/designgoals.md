# Design Goals

- **Expressiveness**: Rich pattern language (nested patterns, subpatterns, conditions, yielding)
- **General-purpose**: Not tied to a specific application domain
- **Performance**: Search plans adapt matching to host graph statistics; generated code, not interpreted
- **Understandability**: Declarative rules close to mathematical notation; visual debugging
- **Development convenience**: Rapid prototyping with GrShell; integration via C# API
- **Well-founded semantics**: Based on SPO graph rewriting theory
- **Platform independence**: Runs on .NET and Mono (Windows, Linux)
