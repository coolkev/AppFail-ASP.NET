echo OFF

:: directories
set DestinationDirectory=destination
set SourceDirectory=source

:: set commands
set MakeDirectoryCommand=mkdir
set DeleteDirectoryCommand=rmdir /s /q
set CopyFilesCommand=copy /y

:: delete existing build files and directories
IF EXIST %SourceDirectory% %DeleteDirectoryCommand% %SourceDirectory%

:: copy the files to the source directory
set SourceLocation=..\src\AppFail\bin\Release\
set SourceFile=AppFail.dll

IF NOT EXIST %SourceLocation%%SourceFile% (
	echo Source files were not found. Did you compile in release?
	pause
	exit
) 

:: create the directory for future files
%MakeDirectoryCommand% %SourceDirectory%

%CopyFilesCommand% %SourceLocation%%SourceFile% %SourceDirectory%

:: copy the manifest
set ManifestLocation=..\nuspec\
set ManifestFile=AppFail.nuspec
set WebConfigTransformFile=web.config.transform

%CopyFilesCommand% %ManifestLocation%%WebConfigTransformFile% %SourceDirectory%

:: set the nuget file location
set NugetLocation=..\tools\nuget\
set NugetFileCommand=NuGet.exe pack
 
:: build the package
%NugetLocation%%NugetFileCommand% %ManifestLocation%%ManifestFile% -BasePath %SourceDirectory% -Verbose

:: delete source directory
%DeleteDirectoryCommand% %SourceDirectory%






