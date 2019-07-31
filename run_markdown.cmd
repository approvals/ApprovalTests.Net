:: ---------------------------------------------------
:: Update code examples
:: ---------------------------------------------------
:: For info on mdsnippets, see https://github.com/SimonCropp/MarkdownSnippets

:: install dotnet SDK from http://go.microsoft.com/fwlink/?LinkID=798306&clcid=0x409
:: Then install MarkdownSnippets.Tool with
::   dotnet tool install -g MarkdownSnippets.Tool
:: To update:
::   dotnet tool update  -g MarkdownSnippets.Tool


call dotnet tool update -g MarkdownSnippets.Tool

call mdsnippets

:: Custom Markdown linting
:: todo: fix
:: ./fix_markdown.sh