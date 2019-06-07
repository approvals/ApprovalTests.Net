<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
**Contents**

- [EnvironmentSpecificTest](#environmentspecifictest)

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

---

[Back to User Guide](/doc/README.md#top)
