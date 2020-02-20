# Main Concepts in ApprovalTests

toc

## Verify
### What it does?
### How it does it?
### General usage
### Why would you customize it?

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

### Why would you customize it?
If you want it to approve something that writes to a new type of a binary file, you would create a custom Writer.

If you simply wanted to format text this is usually done in a separate step before calling:
snippet: verify_with_extension

## Namers
### What it does?
[Namers](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Core/IApprovalNamer.cs) are responsible for figuring out what the `.approved.` and `.received.` files should be called and where they are located.

### How it does it?
This is primarily done by inspecting a stack trace to detect your test framework's attributes. 

The naming pattern is: `{ClassName}.{MethodName}.{AdditionalInformation(optional)}.approved.{Extension}`

### General usage
The vast majority of the time you will not interact directly with the Namers.

### Why would you customize it?
To **support a new testing framerwork** is a main reason you would create your own Namer.

## Reporters
### What it does?
[Reporters](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Core/IApprovalReporter.cs) are called only on failure.
They are responsible for things such as opening Diff tools, copying commands to your clipboard or anything else that can help you determine what went wrong and so you can fix it.

### How it does it?
Reporters are very simple. They are called with a recieved and approved file names on failure. Usually, they make a call to a command line using these filenames as parameters.

For example: `YourDiffTool filename1 filename2`

### General usage
It is very common to switch between Reporters for both personal preferences (a preferred Diff tool) and contextual preferences (at this moment I want to...).

Because using the right Reporter at the right time is so important, there are multiple places they can be configured, including which Reporter is the default Reporter.

### Why would you customize it?
There are two reasons you want to write your custom Reporter:
1. To support a tool you like that is not currently supported 
1. To change the order in which Diff tools are selected

## Approval Output Files
### What it does?
The core of Approvals is that your result and expectations are saved in output files. These files allow us to verify expectations in future runs as well as use external tools.

### How it does it?
Approvals create two files:
* Actual: `ClassName.TestMethodName.received.txt`
* Expected: `ClassName.TestMethodName.approved.txt`
  
The actual files (`.received.`) are deleted on success and should never be checked on your source control.  
The expected files (`.approved.`) need to be checked into your source control.

### General usage
Every ApprovalTest will be generating these files.

### Why would you customize it
The two main ways of customizing the output files are:
1. To store all the output files in the subdirectory
1. Adding additional information for Data Driven Tests or [machine specific tests](../EnvironmentSpecificTests.md)

---

[Back to User Guide](../readme.md#top)