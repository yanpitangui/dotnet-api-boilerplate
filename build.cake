#addin nuget:?package=Cake.Coverlet

var target = Argument("target", "Test");
var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////


Task("Build")
    .Does(() =>
{
    DotNetBuild("WebApiBoilerplate.sln", new DotNetBuildSettings
    {
        Configuration = configuration,
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
	var coverletSettings = new CoverletSettings {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.opencover | CoverletOutputFormat.json,
        MergeWithFile = MakeAbsolute(new DirectoryPath("./coverage.json")).FullPath,
        CoverletOutputDirectory = MakeAbsolute(new DirectoryPath(@"./coverage")).FullPath
    };
	
	Coverlet(
        "./tests/Boilerplate.Api.IntegrationTests/bin/Debug/net7.0/Boilerplate.Api.IntegrationTests.dll", 
        "./tests/Boilerplate.Api.IntegrationTests/Boilerplate.Api.IntegrationTests.csproj", 
        coverletSettings);
		
	Coverlet(
        "./tests/Boilerplate.Api.UnitTests/bin/Debug/net7.0/Boilerplate.Api.UnitTests.dll", 
        "./tests/Boilerplate.Api.UnitTests/Boilerplate.Api.UnitTests.csproj", 
        coverletSettings);
	
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);