About Silverlight Unit Testing
==============================
Silverlight unit test applications run inside of the web browser, just like any
other Silverlight application.

Silverlight test applications use the same metadata as the Visual Studio 
framework for regular .NET applications, and should be familiar.

Do note that test projects are limited to the same sandboxed security model as 
all other Silverlight applications, you will find that some of the features and 
development experience found in the full Visual Studio unit test framework are
not available.

This includes
- Private reflection support
- Deployment items
- Visual Studio test integration

Quality Band
============
The test, build, and infrastructure components are in the Experimental quality
band.

Silverlight Code Coverage
=========================
The Silverlight Toolkit tools include the ability to instrument class libraries
that will still load within the restrictions of the Silverlight security model.

To get started:

1. Open the project file contents in the editor
   a. Right-click on the test application project, and Unload it.
   b. Right-click on the test application project, and Edit the project file.

2. Add information about the class library you would like to instrument

   a. Locate the item group, InstrumentSilverlightAssemblies, and make sure that
      the assembly that you would like to instrument is listed. Here is an 
      example for a class library in the project called ClassLibrary1:
      
      <ItemGroup>
        <InstrumentSilverlightAssemblies Include="ClassLibrary1">
          <Visible>false</Visible>
        </InstrumentSilverlightAssemblies>
      </ItemGroup>
      
3. Re-load the project file. This will enable coverage support from the
   command line and other tools.


Silverlight Toolkit
http://silverlight.codeplex.com/