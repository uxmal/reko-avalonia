## Reko Avalonia prototype

This project is a prototype implementation of a user interface for the [Reko decompiler](https://github.com/uxmal/reko). It's made available so that interested parties can look at the progress of the work of moving Reko to a different user interfaces. The plan is to work in this project to get a stable GUI up, then move it to the master project once deemed fit for use. 

PR's are welcome, but please make them *small* and *limited in scope*. Ideally, consult with @uxmal over on [Gitter](https://gitter.im/uxmal/reko) or Discord.

### How to build in Visual Studio

You will want to install the Avalonia VS extension before working on this project. The project will pull in the Reko runtime components from [https://nuget.org]. You will also need to provide your own `Reko.Gui.dll`, since this is not part of the Reko runtime nuget. If you're already building Reko on your machine this should not be an issue. Open the `reko-avalonia.csproj` file and find the following XML fragment:
```XML
    <Reference Include="Reko.Gui">
      <HintPath>..\..\..\master\src\Gui\bin\Debug\netstandard2.1\Reko.Gui.dll</HintPath>
    </Reference>
```
Change it to reflect the location of your copy of `Reko.Gui.dll`

Now, just hit `F5` to build and start the shell.
