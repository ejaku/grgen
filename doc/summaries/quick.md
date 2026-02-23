# Quick Start Tutorial

### Example: Finite State Machine Transformation
The tutorial walks through a complete example: modeling a state machine graph then transforming it.

### Model Definition (`.gm`)
- Define node classes (`State`, `StartState`) and edge classes (`Transition`) with attributes
- Inheritance: `StartState extends State`

### Rule Definition (`.grg`)
- Pattern (LHS): specify graph elements to match with types and conditions
- Replacement (RHS): specify what to create/keep/delete
- `modify` block: keep matched elements, add new ones
- `replace` block: delete unmentioned matched elements

### Shell Script (`.grs`)
- `new graph "model.grg" "graphname"` -- create graph and compile
- `new :Type` -- create nodes; `new src -:EdgeType-> tgt` -- create edges
- `exec rule()` -- apply rule
- `show num nodes` / `show num edges` -- inspect graph

### Debugging
- `debug exec sequence` -- step through rule application visually
- Graph visualization via yComp or MSAGL
