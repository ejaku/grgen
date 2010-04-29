" Vim syntax file
" Language:     GrGen Rewrite Sequence 
" Maintainer:   Andreas Zwinkau <beza1e1@web.de>
" URL:          

syn clear

if !exists("main_syntax")
  let main_syntax = 'grgen xgrs'
endif

syn keyword gmKeyWords  new graph quit
syn keyword gmKeyWords  dump xgrs echo
syn keyword gmKeyWords  debug
syn keyword gmOptions  set layout option textcolor color
syn keyword gmOptions  group by incoming exclude 
syn keyword gmOptions  infotag
syn keyword gmValues    true false enable disable
syn keyword gmValues    white lightgreen yellow blue green
syn region gmComment    start="/\*" end="\*/"
syn region gmComment    start="//" end="$"
syn region gmComment    start="#" end="$"
syn region gmString    start="\"" end="\""

hi def link gmKeyWords       Statement
hi def link gmComment       Comment
hi def link gmString       String
hi def link gmOptions       Label
hi def link gmValues       Number

let b:current_syntax = "grg"

" vim:set ts=8 sts=2 sw=2 noet:
