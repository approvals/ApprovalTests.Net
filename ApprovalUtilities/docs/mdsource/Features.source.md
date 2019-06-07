## Making Custom Reporters

If your favorite diff tool isn't already in ApprovalTests. There are a couple ways you can fix that. First, try a custom reporter

snippet: custom_reporter

If you have more details you might want to use the DiffInfo Class.

snippet: custom_reporter_diff_info

*note:* Please consider contrubing these back via pull request.

## Disposables.Create

`using` statements nicely cleanup on exit if you have a `IDisposable` or you can create a simple disposable object by passing in a lambda.  

snippet: disposables