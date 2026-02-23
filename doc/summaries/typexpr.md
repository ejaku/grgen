# Basic Types and Expressions

### Primitive Types
- `boolean` (`true`/`false`), `byte` (suffix `y`), `short` (suffix `s`), `int`, `long` (suffix `L`), `float` (suffix `f`), `double` (suffix `d` or decimal point), `string`, `object`
- Hex literals: `0xDEAD`, `0xBEEFL`
- Implicit upcasts: `byte < short < int < long`, `float < double`, `int -> double`, all -> `string`

### Operators (ascending precedence)
1. `? :` (ternary)
2. `||` (lazy or)
3. `&&` (lazy and)
4. `|` (strict or / bitwise or)
5. `^` (xor / bitwise xor)
6. `&` (strict and / bitwise and)
7. `\` (set difference)
8. `==`, `!=`, `~~` (deep equality)
9. `<`, `<=`, `>`, `>=`, `in`
10. `<<`, `>>`, `>>>` (shifts)
11. `+`, `-`
12. `*`, `/`, `%`
13. `~`, `!`, unary `-`, unary `+`

### Key Expressions
- `typeof(elem)` -- runtime type; type comparisons: subtypes are "greater than" supertypes
- `nameof(elem)` -- persistent name as string
- `uniqueof(elem)` -- unique ID (requires `node edge unique;` in model)
- `random()` -> double [0,1); `random(n)` -> int [0,n)
- `Math::` package: `min`, `max`, `abs`, `ceil`, `floor`, `round`, `truncate`, `sqr`, `sqrt`, `pow`, `log`, `sgn`, `sin`, `cos`, `tan`, `arcsin`, `arccos`, `arctan`, `pi()`, `e()`, `byteMin()`..`doubleMax()`
- `Time::now()` -> long (UTC, 100ns ticks since 1601)
- `(Type)expr` -- explicit cast
- `~~` -- deep attribute equality (crashes on cycles)
- `in` -- container membership or string substring test

### String Methods
- `.length()`, `.startsWith(s)`, `.endsWith(s)`, `.indexOf(s[,start])`, `.lastIndexOf(s[,start])`, `.substring(start[,len])`, `.replace(start,len,repl)`, `.toLower()`, `.toUpper()`, `.asArray(separator)`
