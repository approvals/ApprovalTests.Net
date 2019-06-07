<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
**Contents**

- [ApprovalTests](#approvaltests)
  - [Features](#features)
  - [Main concepts for ApprovalTests](#main-concepts-for-approvaltests)
  - [Approval Output Files](#approval-output-files)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

# ApprovalTests

## Features 

As new features are added, they will be [documented here](Features.md)

## Main concepts for ApprovalTests  

[Reporters](Reporters.md#top) Namers & Writers are the 3 pieces that allow ApprovalTests to work. 
 
**Writers** write to a file  
**Namers** figure out what the file should be called and where it is located  
**Reporters** are called on failure to help you determine what went wrong.  


## Approval Output Files

* [Enviroment SpecificTests](EnvironmentSpecificTests.md)