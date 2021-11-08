# How to customize the order of DiffTools in your Reporter

toc


### Choosing a DiffTool preference

If you do not like the default order that DiffTools are chosen, you can easily create a custom Reporter with your own preferences.  
Once you create the class, you select it with the `[UseReporter(typeof(CustomDiffReporter))]`

snippet: CustomDiffReporter.cs

You can also override the default order while using `DiffReporter` by defining `DiffEngine_ToolOrder` environment variable. The value of that variable should contain a delimiter (`,`, `|`, ` `) separated list of the diff tool names in the desired order. More details about that in [Diff Tool Order](https://github.com/VerifyTests/DiffEngine/blob/main/docs/diff-tool.order.md)

---

[Back to User Guide](../readme.md#top)