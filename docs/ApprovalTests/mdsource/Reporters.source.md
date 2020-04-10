# Reporters

toc

## Using Reporters

For an introduction on how to use reporters check out [ getting started with reporters ](./ReportersGettingStarted.md)

## Supported Diff Tools

The DiffReporter class goes through a chain of possible reporters to find the first option installed on your system. Currently the search goes in this order:


### Windows

snippet: windows_diff_reporters


### Mac

snippet: mac_diff_reporters


### Linux

snippet: linux_diff_reporters


## Making Custom Reporters

See https://github.com/SimonCropp/Verify/blob/master/docs/diff-tool.custom.md


## Joining Reporters

These classes help you combine reporters to make more powerful combinations

* FirstWorkingReporter - launch the first report for this system, only 1
* MultiReporter - launch ALL reporters


### Choosing a DiffTool preference

See [How to customize the order of DiffTools in your Reporter](howtos/CustomizingDiffToolSelectionOrder.md)

## Auto-Approving Reporters

These reporters create a commandline move file to approve the results and place it on your clipboard when a test fails.

* ClipboardReporter - This test only
* AllFailingTestsClipboardReporter - All tests (this might make a long command line)


## Continous Integration

ApprovalTests will not launch anything if you are running on a CI machine.

Currently, we support:

snippet: continuous_integration

You can add to this by configuring the FrontLoadedReporter Annotation.

## File Types

ApprovalTests will do different things depending on if it thinks a file is an image or not. It does this by the file extension.

### Text File extensions

snippet: text_file_types


### Image File extensions

snippet: image_file_types

---

[Back to User Guide](readme.md#top)