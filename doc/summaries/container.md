# Container Types

### Container Types
- `set<T>`, `map<K,V>`, `array<T>`, `deque<T>`
- When holding node/edge types, called "storages"

### Set
- Methods: `.size()`, `.empty()`, `.peek(n)`, `.min()`, `.max()`, `.asArray()`
- Update: `.add(v)`, `.rem(v)`, `.clear()`, `.addAll(other)`
- Operators: `|` (union), `&` (intersection), `\` (difference)
- Comparison: `==`, `!=`, `~~`, `<` (proper subset), `>`, `<=`, `>=`
- Constructor: `set<T>{ expr, ... }`

### Map
- Methods: `.size()`, `.empty()`, `.peek(n)`, `.domain()`, `.range()`, `.asArray()` (only `map<int,T>`)
- Update: `.add(k,v)`, `.rem(k)`, `.clear()`
- Lookup: `m[key]`, `x in m` (domain membership)
- Operators: `|`, `&`, `\` (right operand can be `set<K>`)
- Constructor: `map<K,V>{ k1->v1, ... }`

### Array
- Methods: `.size()`, `.empty()`, `.peek(n)`, `.peek()` (last), `.indexOf(v[,start])`, `.lastIndexOf(v[,start])`, `.indexOfOrdered(v)` (binary search), `.orderAscending()`, `.orderDescending()`, `.group()`, `.keepOneForEach()`, `.reverse()`, `.shuffle()`, `.subarray(start,len)`, `.asDeque()`, `.asSet()`, `.asMap()`, `.asString(sep)` (for `array<string>`)
- Update: `.add(v)` (append), `.add(v,i)` (insert at i), `.rem()` (last), `.rem(i)` (at i), `.clear()`, `.addAll(other)`
- Operator: `+` (concatenation)
- Lookup: `a[index]`, indexed assignment `a[i] = v`
- Constructor: `array<T>[ expr, ... ]`

### Deque
- Methods: `.size()`, `.empty()`, `.peek(n)`, `.peek()` (first), `.indexOf(v)`, `.lastIndexOf(v)`, `.subdeque(start,len)`, `.asArray()`, `.asSet()`
- Update: `.add(v)` (end), `.add(v,i)` (at i), `.rem()` (begin -- FIFO), `.rem(i)`, `.clear()`
- Operator: `+` (concatenation)
- Constructor: `deque<T>] expr, ... [`

### Storage Access in Patterns
- `n:Type{storageVar}` -- bind to element from storage
- `n:Type{node.storageAttr}` -- bind from storage attribute
- `n:Type{map[keyElem]}` -- bind from map lookup by key

### Semantics Notes
- Container assigned to **attribute**: value semantics (deep copy)
- Container assigned between **variables**: reference semantics (shared)
- Container params to functions/procedures: by reference
- Do NOT modify a container during iteration; use `clone()` first
- Change assignment extension: `|= expr => boolTarget` tracks if container changed
