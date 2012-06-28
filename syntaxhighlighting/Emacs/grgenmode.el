;; very simple grgen mode
;; by Dominik Dietrich, feel free to extend and modify

(add-to-list 'auto-mode-alist '("\\.gri\\'" . grgen-mode))
(add-to-list 'auto-mode-alist '("\\.grg\\'" . grgen-mode))
(add-to-list 'auto-mode-alist '("\\.gm\\'" . grgen-mode))
(add-to-list 'auto-mode-alist '("\\.grs\\'" . grgen-mode))

;; we setup the keywords in a list and compute efficient regexp representation later
(defvar grgen-keywords
  '("rule" "pattern" "emit" "eval" "delete" "emithere" "exec" "yield" "def" "class" "new" "node" "edge" "var")
  "grgen keywords.")

;; same for the block keywords
(defvar grgen-block-keywords
  '("negative" "if" "modify" "iterated" "alternative" "independent")
  "grgen block keywords.")


(defvar grgen-keywords-regexp (regexp-opt grgen-keywords 'words))
(defvar grgen-block-keywords-regexp (regexp-opt grgen-block-keywords 'words))


(setq grgen-font-lock-keywords
  `(
	(,grgen-keywords-regexp . font-lock-keyword-face)
	(,grgen-block-keywords-regexp . font-lock-constant-face)
	("rule[[:space:]]+\\([_A-Za-z0-9]+\\)" 1 font-lock-function-name-face)
	("pattern[[:space:]]+\\([_A-Za-z0-9]+\\)" 1 font-lock-function-name-face)
	(":[[:space:]]*\\([_A-Za-z0-9]+\\)[[:space:]]*([^)]*)" 1 font-lock-function-name-face)
	("[_A-Za-z0-9]+::[[:space:]]*\\([_A-Za-z0-9]+\\)" . font-lock-variable-name-face)
	(":[[:space:]]*\\([_A-Za-z0-9]+\\)" . font-lock-type-face)
	("[A-Za-z0-9]+[[:space:]]*{" . font-lock-keyword-face)
	))

;;(string-match "[^ {}]*{" "modify {")

(defvar grgen-mode-syntax-table (make-syntax-table))

(modify-syntax-entry ?\/ ". 12b" grgen-mode-syntax-table)
(modify-syntax-entry ?\n "> b" grgen-mode-syntax-table)

;; you can cheat the function easily by putting several {/} in one line ;)
;; ignore empty lines as well
(defun grgen-indent-line ()
  "Indent current line as grgen code"
  (interactive)
  (beginning-of-line)
  ;; rule 1: beginning of buffer: indent=0
  (if (bobp) (indent-line-to 0)
    (let (previndent)
			;; get the indentation of the previous line
			(save-excursion
				(forward-line -1)
				(setq previndent (current-indentation))
				(if (looking-at "[^{}\n]*{[^\n}]*\n") 
						(progn (setq previndent (+ previndent 2))
									 (message "found {"))
					nil)
				)
			(if (looking-at "[^{}\n]*}")
					(progn (setq previndent (- previndent 2))
									 (message "found }"))
				nil)
			(indent-line-to previndent)
			)
		)
	)
	  
(define-derived-mode grgen-mode fundamental-mode
  (setq font-lock-defaults '((grgen-font-lock-keywords)))
  (setq mode-name "grgen")
  (set-syntax-table grgen-mode-syntax-table)
  (setq-default tab-width 2) 
  (setq indent-line-function 'grgen-indent-line)
  (define-key global-map (kbd "RET") 'newline-and-indent)
)
