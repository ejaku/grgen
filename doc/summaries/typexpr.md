# Basic Types and Expressions

### Primitive Types and Literals
- `boolean` (`true`/`false`), `byte` (suffix `y`), `short` (suffix `s`), `int`, `long` (suffix `L`), `float` (suffix `f`), `double` (suffix `d` or decimal point), `string`, `object`
- `null` -- the one literal of object type
- Literal examples: `42y`, `42s`, `42`, `42L`, `0xDEAD`, `0xBEEFL`, `3.14f`, `3.14`, `"hello"`, `true`, `MyEnum::val`
- Implicit upcasts: `byte < short < int < long`, `float < double`, `int -> double`, `enum -> int`, all -> `string`
- `byte`/`short` are upcast to `int` in all arithmetic (even if all operands are byte/short)
- Explicit casts: `(int)double`, `(float)double`, `(object)T` from any type; `object` → `string` calls `toString()` or returns `"null"`
- No cast between different enum types, and no `int` → `enum` cast

### Operators by Type
- **Boolean**: `!` (not), `&&`/`||` (lazy and/or), `&`/`|`/`^` (strict and/or/xor), `? :` (ternary), `==`, `!=`
- **Numbers**: arithmetic `+`, `-`, `*`, `/`, `%`; comparison `<`, `<=`, `==`, `!=`, `>=`, `>`; integers also: bitwise `&`, `|`, `^`, `~`, `<<`, `>>`, `>>>`
- **Strings**: `+` (concatenation); `==`, `!=`, `<`, `<=`, `>`, `>=` (lexicographic; prefix is smaller); `s in t` (substring test)
- **Types**: `==`, `!=`, `<`, `<=`, `>`, `>=` on type expressions (subtypes greater)

### Operator Precedence (ascending)
1. `? :` (ternary)
2. `||` (lazy or)
3. `&&` (lazy and)
4. `|` (strict or / bitwise or)
5. `^` (xor / bitwise xor)
6. `&` (strict and / bitwise and)
7. `\` (set difference)
8. `==`, `!=`, `~~`
9. `<`, `<=`, `>`, `>=`, `in`
10. `<<`, `>>`, `>>>` (shifts)
11. `+`, `-`
12. `*`, `/`, `%`
13. `~`, `!`, unary `-`, unary `+`

### Key Expressions
- `typeof(elem)` -- runtime type; type comparisons: subtypes are **greater** than supertypes; `Node`/`Edge` are the bottom (smallest/most general) elements
- `nameof(elem)` -- persistent name as string; `nameof()` without argument = current graph's name
- `uniqueof(elem)` -- unique ID as `int` for nodes/edges (requires `unique` in model), as `long` for class objects (no declaration needed)
- `random()` -> double [0,1); `random(n)` -> int [0,n)
- `Math::` package: `min`, `max`, `abs`, `ceil`, `floor`, `round`, `truncate`, `sqr`, `sqrt`, `pow`, `log`, `sgn`, `sin`, `cos`, `tan`, `arcsin`, `arccos`, `arctan`, `pi()`, `e()`, `byteMin()`..`doubleMax()`
- `Time::now()` -> long (UTC, 100ns ticks since 1601)
- `(Type)expr` -- explicit cast
- `~~` -- deep value equality for graphs, class objects, containers, and graph elements; crashes on cycles
- `in` -- container membership or string substring test
- `[?it]` -- returns `array<match<r.it>>` of all iterated matches (named iterated `it` of rule `r`)
- `count(it)` -- number of iterated matches

### String Methods
- `.length()`, `.startsWith(s)`, `.endsWith(s)`, `.indexOf(s[,start])`, `.lastIndexOf(s[,start])`, `.substring(start[,len])`, `.replace(start,len,repl)`, `.toLower()`, `.toUpper()`, `.asArray(separator)`
