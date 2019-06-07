## Supported Diff Tools
The DiffReporter class goes through a chain of possible reporters to find the first option installed on your system. Currently the search goes in this order:


### Windows

snippet: windows_diff_reporters

### Mac

snippet: mac_diff_reporters

### Linux

snippet: linux_diff_reporters


## Making Custom Reporters

If your favorite diff tool isn't already in ApprovalTests. There are a couple ways you can fix that. First, try a custom reporter

snippet: custom_reporter

If you have more details you might want to use the DiffInfo Class.

snippet: custom_reporter_diff_info

*note:* Please consider contrubing these back via pull request.

## Joining Reporters

## Auto-Approving Reporters