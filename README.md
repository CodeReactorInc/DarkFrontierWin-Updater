# Dark Frontier by Code Reactor

## Description

This is the autoupdater embed application

## Requirements

Visual Studio 2019
.Net Framework

## Warns

Don't use this sources in your project/application, you can create your own version but use like a lib isnt recommended

## Downloads

*Not available at moment*

## Changelog

### Crow - v1.0.0.0
```
v1.0.0.0 - Crow Update
- Initial release
```

## Docs

### How to use

- Create a DarkFrontierXml in a website to Dark Frontier can use the file to compare the version of local program with the newest
- Use the argument "test" with DFTestXml to only get the exit code contenting info if the local program are updated or outdated
- Use the argument "install" with DFInstallXml to automatically download the update

### DarkFrontierXml

Example:
```xml
<DarkFrontier>
	<DownloadUrl>https://example.com/download/product-updated.zip</DownloadUrl>
	<DFPath>update.exe</DFPath>
	<Filename>InstallThis.zip</Filename>
	<Version>
		<Major>1</Major>
		<Minor>1</Minor>
		<Patch>0</Patch>
		<Revision>0</Revision>
	</Version>
</DarkFrontier>
```

### DFTestXml

Example:
```xml
<DFTest>
	<ServerUrl>https://example.com/DarkFrontier.xml</ServerUrl>
	<Version>
		<Major>1</Major>
		<Minor>0</Minor>
		<Patch>0</Patch>
		<Revision>0</Revision>
	</Version>
</DFTest>
```

### DFInstallXml

Example:
```xml
<DFInstall>
	<ProductName>Example Name</ProductName>
	<ServerUrl>https://example.com/DarkFrontier.xml</ServerUrl>
	<InstallPath>C:/Program Files/Example</InstallPath>
</DFInstall>
```

## Libs

*Not created at moment, you can help us creating your own lib!*