<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
**Contents**

- [EnvironmentSpecificTest](#environmentspecifictest)
- [MachineSpecificReporter](#machinespecificreporter)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## EnvironmentSpecificTest

Sometimes you need Additonal Information in the Approval Output File Name.  
ApprovalTests allows for `ClassName.MethodName.AdditionalInforamtion.approved.extension`

```
using(var cleanup = NamerFactory.AsEnvironmentSpecificTest( ()=> "Any.Additional.Data"))
{
}
```

As this will clean up the additional information regardless of the test execution.

There are a few convience functions in `ApprovalResults` setup for the common situations:

* UniqueForDotNetVersion  
* UniqueForMachineName  
* UniqueForOs
* UniqueForRuntime
* GetUserName

## MachineSpecificReporter
Some things will always be different if you run them on Windows 7 versus Windows 10 (for example: WinForms will always render differently). Approval Test has a feature to handle this situation by including the OS in approvals file name so you have a different approval file for each OS.

If you are using a machine specific name in your approval tests
for example:
snippet: unique_for_os

This can produce files such as:
snippet: ApprovalsFilenameTest.TestSimilarFiles.approved.txt

If this is run on a new machine, it could produce a new approval file.
This can be confusing as you might not remember what the old system used to produce.
If you use a MachineSpecificReporter and the existing approval file does not exist (or is empty),
it will search or the last approved version from a different machine and copy it over as a starting point.
This will always start with a line like 
```
Copied from: EmailTest.Testname.Microsoft_Windows_10_Pro.approved.eml
```

This makes it easier to understand how this system differs from the last system.


---

[Back to User Guide](/doc/README.md#top)
