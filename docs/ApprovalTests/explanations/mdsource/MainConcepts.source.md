# Main Concepts in ApprovalTests

toc

## Verify
The entry point to ApprovalTests is almost always some variation of a [Verify method](Verify.md).

For example: 
snippet: simple_verify

This call brings together three things to produce a `.received.` file which is a compared to an `.approved.` file.
![](MainConceptsSimplified.svg)

**Note:** This is a simplified version of what ApprovalTests does. You can see a [full picture here](MainConceptsComplete.svg)

## Writers
Writers are responsible for writing the `.received.` file to the disc.
They also determine the extension for both `.received.` and `.approved.` files.
snippet: IApprovalWriter.cs

## Namers figure out what the file should be called and where it is located.
## Reporters** are called on failure to help you determine what went wrong. 


## Approval Output Files


---

[Back to User Guide](readme.md#top)