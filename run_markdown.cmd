:: ---------------------------------------------------
:: Update tables of contents
:: ---------------------------------------------------
:: For info on doctoc, see https://github.com/thlorenz/doctoc

:: To install:
::   npm install -g doctoc

doctoc --title **Contents** .

:: ---------------------------------------------------
:: Update code examples
:: ---------------------------------------------------
:: For info on mdsnippets, see https://github.com/SimonCropp/MarkdownSnippets

:: install dotnet SDK from http://go.microsoft.com/fwlink/?LinkID=798306&clcid=0x409
:: Then install MarkdownSnippets.Tool with
::   dotnet tool install -g MarkdownSnippets.Tool
:: To update:
::   dotnet tool update  -g MarkdownSnippets.Tool

dotnet tool update  -g MarkdownSnippets.Tool

mdsnippets

:: Custom Markdown linting
:: todo: fix
:: ./fix_markdown.sh