# Main Concepts in ApprovalTests

toc

## Verify
The entry point to ApprovalTests is almost always some variation of a [Verify method](Verify.md).

For example: 
snippet: simple_verify

This call brings together three things to produce a .received. file which is a compared to .approved. file.
![](MainConcepts.svg)

## Writers write to a file.
## Namers figure out what the file should be called and where it is located.
## Reporters** are called on failure to help you determine what went wrong. 


## Approval Output Files


---

[Back to User Guide](readme.md#top)