" Vim syntax file
" Language:     GrGen Model
" Maintainer:   Sebastian Buchwald <Sebastian.Buchwald@kit.edu>
" Last Change:  2011 May 7

if version < 600
  syntax clear
elseif !exists("b:current_syntax")
  finish
endif

syn spell notoplevel

syn keyword gmKeyWords  abstract arbitrary class connect const copy directed
syn keyword gmKeyWords  edge enum extends node
syn keyword gmValues    string int boolean
syn region  gmComment   start="/\*" end="\*/" contains=@Spell
syn region  gmComment   start="//" end="$" contains=@Spell

hi def link gmKeyWords  Statement
hi def link gmComment   Comment
hi def link gmValues    Number

let b:current_syntax = "gm"
