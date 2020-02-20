# Main Concepts in ApprovalTests

toc

## Verify
### What it does?
### How it does it?
### General usage
### Why would you customize it

The entry point to ApprovalTests is almost always some variation of a [Verify method](../Verify.md).

For example: 
snippet: simple_verify

This call brings together 3 things + default Approver to produce a `.received.` file which is compared to an `.approved.` file.

![](MainConceptsSimplified.svg)

**Note:** This is a simplified version of what ApprovalTests does. You can see a [full picture here](MainConceptsComplete.svg)

## Writers
### What it does?
[Writers](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Core/IApprovalWriter.cs) are responsible for writing the `.received.` file to the disk.
They also determine the extension for both `.received.` and `.approved.` files.

### How it does it?
Eventually, all Verify methods call:
snippet: complete_verify_call

Most of the time this is hidden in an underlying a Verify call.

### General usage
The vast majority of the time you will not interact directly with the Writers.

### Why would you customize it
If you want it to approve something that wrote to a new type of a binary file, you would create a custom Writer.
If you simply wanted to format text this is usually done as a step before calling:
snippet: verify_with_extension

## Namers
### What it does?
[Namers](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Core/IApprovalNamer.cs) are responsible for figuring out what the `.approved.` and `.received.` files should be called and where they are located.

### How it does it?
This is primarily done by inspecting a stack trace to detect your test framework's attributes.
It chooses the name as such `{ClassName}.{MethodName}.{AdditionalInformation(optional)}.approved.{Extension}`

### General usage
The vast majority of the time you will not interact directly with the Namers.

### Why would you customize it
The only reason you will want to create a Namer on your own is to support a new testing framework.

## Reporters
[Reporters](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Core/IApprovalReporter.cs) are called only on failure.
They are responsible for things such as opening Diff tools, copying commands to your clipboard or anything else that can help you determine what went wrong and so you can fix it.

It is very common to switch between Reporters for both personal preferences (a preferred Diff tool) and contextual preferences (at this moment I want to...).
There is also a chance you will create your own custom Reporter to support a tool you like or change the order in which Diff tools are selected.
Because using the right Reporter at the right time is so important, there are multiple places they can be configured, including which Reporter is the default Reporter.

## Approval Output Files
The core of Approvals is that your result and expectations are saved in output files.

* Actual: `ClassName.TestMethodName.received.txt`
* Expected: `ClassName.TestMethodName.approved.txt`

The actual files (`.received.`) are deleted on success and should never be checked on your source control.
The expected files (`.approved.`) need to be checked into your source control.

---

[Back to User Guide](../readme.md#top)