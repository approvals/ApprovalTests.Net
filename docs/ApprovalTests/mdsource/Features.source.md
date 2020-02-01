# ApprovalTests Features

toc

## Approvals.AssertText

If you prefer not to store your expectations in the `.approved.` files, you can store them directly inline with your code.
Sample:

(Before):

snippet: assert_text_before

When you do this, it will copy the c# for the `.recieved.` to your clipboard, so you can paste it inline.

(After)
snippet: assert_text


Currently, it put the text as an array of strings that gets concatenated as this tends to read better. Actual can be a string, or an array of strings.

It will also write the results to a temp files on failure and open a DiffTool, so you can easily view the results and differences.


## [MachineSpecificReporter](EnvironmentSpecificTests.md#machinespecificreporter)


## ApprovalsFilename

Sometimes you want to parse an approvals filename to get the parts.

snippet: approvals_filename

Will produce

snippet: ApprovalsFilenameTest.TestMachineSpecificName.approved.txt


## [Making Custom Reporters](Reporters.md##making-custom-reporters)


## [Environment SpecificTests](EnvironmentSpecificTests.md#environmentspecifictest)

---

[Back to User Guide](readme.md#top)