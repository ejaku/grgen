# Container Types

### Container Types
- `set<T>`, `map<K,V>`, `array<T>`, `deque<T>`
- When holding node/edge types, called "storages"

### Set
- Query methods: `.size()`, `.empty()`, `.peek(n)`, `.min()`, `.max()`, `.asArray()`
- Update methods: `.add(v)`, `.rem(v)`, `.clear()`, `.addAll(other)`
- Operators: `|` (union), `&` (intersection), `\` (difference); compound assignments `|=`, `&=`, `\=`
- Membership: `x in s` (O(1))
- Comparison: `==`, `!=`, `~~`, `<` (proper subset), `>`, `<=`, `>=`
- Constructor: `set<T>{ expr, ... }`; copy constructor: `set<T>(setExpr)` (filters incompatible elements when types differ)

### Map
- Query methods: `.size()`, `.empty()`, `.peek(n)`, `.domain()`, `.range()`, `.asArray()` (only `map<int,T>`)
- Update methods: `.add(k,v)`, `.rem(k)`, `.clear()`
- Lookup: `m[key]`, `x in m` (domain membership)
- Operators: `|`, `&`, `\` (right operand can be `set<K>`); compound assignments `|=`, `&=`, `\=`
- Membership: `x in m` (domain membership, O(1))
- Comparison: `==`, `!=`, `~~`, `<` (proper submap), `>`, `<=`, `>=`
- Constructor: `map<K,V>{ k1->v1, ... }`; copy constructor: `map<K,V>(mapExpr)` (filters incompatible elements when types differ)

### Array
- Query methods: `.size()`, `.empty()`, `.peek(n)`, `.peek()` (last), `.indexOf(v[,start])`, `.lastIndexOf(v[,start])`, `.indexOfOrdered(v)` (binary search), `.orderAscending()`, `.orderDescending()`, `.group()`, `.keepOneForEach()`, `.reverse()`, `.shuffle()`, `.subarray(start,len)`, `.asDeque()`, `.asSet()`, `.asMap()`, `.asString(sep)` (for `array<string>`)
- Accumulation, by-attribute, and lambda expression array methods: see `sequencegraphquerymanipulation.md`
- Update methods: `.add(v)` (append), `.add(v,i)` (insert at i), `.rem()` (last), `.rem(i)` (at i), `.clear()`, `.addAll(other)`
- Operator: `+` (concatenation); compound assignment `+=`
- Membership: `x in a` (O(n))
- Comparison: `==`, `!=`, `~~`, `<` (proper subarray/lexicographic), `>`, `<=`, `>=`
- Lookup: `a[index]`, indexed assignment `a[i] = v`
- Constructor: `array<T>[ expr, ... ]`; copy constructor: `array<T>(arrayExpr)` (filters incompatible elements when types differ)

### Deque
- Note: double-ended queue -- use as FIFO queue (BFS); contrast with array used as LIFO stack (DFS)
- Query methods: `.size()`, `.empty()`, `.peek(n)`, `.peek()` (first), `.indexOf(v[,start])`, `.lastIndexOf(v[,start])`, `.subdeque(start,len)`, `.asArray()`, `.asSet()`
- Update methods: `.add(v)` (end), `.add(v,i)` (at i), `.rem()` (begin -- FIFO), `.rem(i)`, `.clear()`
- Operator: `+` (concatenation); compound assignment `+=`
- Membership: `x in d` (O(n))
- Comparison: `==`, `!=`, `~~`, `<` (proper subdeque/lexicographic), `>`, `<=`, `>=`
- Constructor: `deque<T>[ expr, ... ]`; copy constructor: `deque<T>(dequeExpr)` (filters incompatible elements when types differ)

### Storage Access in Patterns
- `n:Type{storageVar}` -- bind to element from storage
- `n:Type{node.storageAttr}` -- bind from storage attribute
- `n:Type{map[keyElem]}` -- bind from map lookup by key

### Semantics Notes
- Container assigned to **attribute**: value semantics (deep copy)
- Container assigned between **variables**: reference semantics (shared)
- Container params to functions/procedures: by reference
- Do NOT modify a container during iteration; use `clone(container)` first -- returns a shallow clone of the same type (syntactically more convenient than a copy constructor when no type change is needed)
- For performance when modifying container attributes: prefer `.add()`, `.rem()`, indexed assignment over full assignment (which copies by value)
- Change tracking on set/map compound assignments: the change value is `true` if the container changed, `false` if not; `=> boolTarget` assigns the change value to boolTarget; `|> boolTarget` assigns `boolTarget | changeValue` to boolTarget; `&> boolTarget` assigns `boolTarget & changeValue` to boolTarget
