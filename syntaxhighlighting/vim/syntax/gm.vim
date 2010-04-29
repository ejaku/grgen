" Vim syntax file
" Language:     GrGen Model file
" Maintainer:   Andreas Zwinkau <beza1e1@web.de>
" URL:          

syn clear

if !exists("main_syntax")
  let main_syntax = 'grgen model'
endif

syn keyword gmKeyWords  abstract arbitrary class connect const copy directed
syn keyword gmKeyWords  edge enum extends node
syn keyword gmValues  string int boolean
syn region gmComment    start="/\*" end="\*/"
syn region gmComment    start="//" end="$"

hi def link gmKeyWords       Statement
hi def link gmComment       Comment
hi def link gmValues       Number

let b:current_syntax = "gm"

" vim:set ts=8 sts=2 sw=2 noet:
