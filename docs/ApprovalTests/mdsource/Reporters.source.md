# Reporters

toc

## Using Reporters

For an introduction on how to use reporters check out [ getting started with reporters ](./ReportersGettingStarted.md)

### Supported Diff Tools

ApprovalTests Diff Reporters use [DiffEngine](https://github.com/SimonCropp/DiffEngine) which supports the following [diff tools](https://github.com/SimonCropp/DiffEngine/#supported-diff-tools)


#### Custom Diff Tool

Custom Diff Tools can be added via DiffEngine. See: https://github.com/SimonCropp/Verify/blob/master/docs/diff-tool.custom.md


## Making Custom Reporters

Extend `IApprovalFailureReporter`. For example a file can be launched on failure:

snippet: FileLauncherReporter.cs


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

---

[Back to User Guide](readme.md#top)