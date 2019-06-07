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

