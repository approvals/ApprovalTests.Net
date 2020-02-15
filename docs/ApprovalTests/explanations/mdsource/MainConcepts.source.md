# Main Concepts in ApprovalTests

toc

## Verify
The entry point to ApprovalTests is almost always some variation of a [Verify method](../Verify.md).

For example: 
snippet: simple_verify

This call brings together 3 things + default Approver to produce a `.received.` file which is a compared to an `.approved.` file.
![](MainConceptsSimplified.svg)

**Note:** This is a simplified version of what ApprovalTests does. You can see a [full picture here](MainConceptsComplete.svg)

## Writers
Writers are responsible for writing the `.received.` file to the disc.
They also determine the extension for both `.received.` and `.approved.` files.
snippet: IApprovalWriter.cs

## Namers
Namers are responsible for figure out what the file should be called and where it is located.
They primarily do this by inspecting a stack trace to detect your test frameworks' attributes.

## Reporters
Reporters are called only on failure.
They are responsible for such as opening Diff tools, copying commands to your clipboard or anything else that can help you determine what went wrong and fix it. 

## Approval Output Files
The core of Approvals is that your result and expectations are saved in output files.
Actual: `ClassName.TestMethodName.received.txt`
Expected: `ClassName.TestMethodName.approved.txt`

The actual files (`.recieved.`) are deleted on success and should never be checked on your source control.
The expected files (`.approved.`) need to be checked into your source control.

---

[Back to User Guide](../readme.md#top)