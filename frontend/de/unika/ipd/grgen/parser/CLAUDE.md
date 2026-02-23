# Parser

The `parser/` package implements lexical and syntactic analysis for the GrGen frontend, transforming `.grg` (rules) and `.gm` (model) source files into an Abstract Syntax Tree.

## Directory Structure

```
parser/
├── antlr/                          # ANTLR-based parser implementation
│   ├── GrGen.g                     # Main grammar (~4,960 lines, hand-written)
│   ├── EmbeddedExec.g              # Embedded sequences grammar (~1,486 lines, hand-written)
│   ├── GrGenParser.java            # GENERATED from GrGen.g (~52K lines)
│   ├── GrGenLexer.java             # GENERATED from GrGen.g (~6K lines)
│   ├── GrGen_EmbeddedExec.java     # GENERATED from EmbeddedExec.g (~20K lines)
│   ├── GrGen.tokens                # GENERATED token definitions
│   ├── EmbeddedExec.tokens         # GENERATED token definitions
│   ├── GRParserEnvironment.java    # ANTLR-specific parser context
│   ├── Coords.java                 # ANTLR token coordinate extraction
│   ├── SubunitInclude.java         # File/model include stack management
│   ├── keywords.txt                # Reserved keyword list (67 keywords)
│   └── gen-keywords-code.sh        # Script to generate keyword registration code
│
├── ParserEnvironment.java          # Base parser context (abstract, ~37K)
├── Coords.java                     # Source location tracking (line, column, filename)
├── Symbol.java                     # Lexical symbol with Definition and Occurrence inner classes
├── SymbolTable.java                # Namespace mapping (String → Symbol)
├── Scope.java                      # Nested scope/namespace management
├── SymbolTableException.java       # Symbol table error exception
└── AnonymousScopeNamer.java        # Auto-naming for unnamed constructs (alt_0, iter_0, neg_0, ...)
```

## Parsing Pipeline

```
Input (.grg/.gm files)
    → GRParserEnvironment (manages file includes, scoping)
    → GrGenLexer (tokenization, 158 token types)
    → GrGenParser (parse rules from GrGen.g)
        ├── delegates to GrGen_EmbeddedExec for embedded sequences
        ├── uses ParserEnvironment for scoping/symbol tracking
        ├── uses AnonymousScopeNamer for unnamed constructs
        └── constructs AST nodes via embedded Java code
    → UnitNode (root of AST)
```

## Key Classes

### ParserEnvironment (abstract base)
Central context managing 13 symbol tables:
- TYPES (0), ENTITIES (1), ACTIONS (2), ALTERNATIVES (3), ITERATEDS (4), NEGATIVES (5), INDEPENDENTS (6), REPLACES (7), MODELS (8), FUNCTIONS_AND_EXTERNAL_FUNCTIONS (9), COMPUTATION_BLOCKS (10), INDICES (11), PACKAGES (12)

Manages scope push/pop, symbol definition/occurrence tracking, pre-defined root types (node, directed/undirected/arbitrary edge, internal object).

### GRParserEnvironment (ANTLR-specific)
Extends `ParserEnvironment`. Manages file inclusion stack with circular-include detection. Handles `pushFile()` / `popFile()` for switching lexer input streams (implementations of abstract methods).

### Symbol Resolution
- `Symbol.Definition` - Where a symbol is defined (location, scope, AST node).
- `Symbol.Occurrence` - Where a symbol is used (linked back to its definition).
- `Scope` - Nested scopes with parent-child relationships, tracks definitions and unresolved occurrences.

## Grammar Files

**`GrGen.g`** - Main grammar defining:
- Model declarations (node/edge types, object types, attributes, inheritance, connection assertions, indices)
- Rule declarations (patterns and rewrites (modify/replace), alternatives, iterateds, negatives, independents), subpattern declarations and function/procedure/filter function declarations, match class declarations
- Pattern entities: node and edge declarations, variable declarations, entity uses, subpattern usages, continuations of graphlet prefixes (grammar pattern left factoring)
- Computations (statements) and expressions
- Package declarations, use via package prefix in identifiers
- Imports `EmbeddedExec` grammar for embedded sequences

**`EmbeddedExec.g`** - Embedded sequences grammar covering:
- Sequences as such (rule calls, sequence operators, sequence control flow)
- Sequence computations (assignments, control flow, function/procedure calls)
- Sequence expressions (operators with proper precedence, container operations, graph queries)

## Regenerating Parsers

After grammar changes:
```bash
cd frontend && make .grammar
```
