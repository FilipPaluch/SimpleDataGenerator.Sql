var target = Argument("target", "Default");

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore("../Source/SimpleDataGenerator.Sql.sln");
});

Task("Clean")
    .Does(() =>
{
    CleanDirectories("../Source/**/bin");
    CleanDirectories("../Source/**/obj");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    MSBuild("../Source/SimpleDataGenerator.Sql.sln", settings => settings.SetConfiguration("Release"));
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit("../Source/SimpleDataGenerator.Sql.Tests/bin/Release/SimpleDataGenerator.Tests.Sql.dll", new NUnitSettings {
        ToolPath = "../Source/packages/NUnit.ConsoleRunner.3.2.1/tools/nunit3-console.exe"
    });
	
});

Task("Create-NuGet-Package")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{
    var nuGetPackSettings = new NuGetPackSettings {
        Id                      = "SimpleDataGenerator.Sql",
        Version                 = EnvironmentVariable("APPVEYOR_BUILD_VERSION"),
        Title                   = "SimpleDataGenerator.Sql",
        Authors                 = new[] {"Filip Paluch"},
        Owners                  = new[] {"Filip Paluch"},
        Description             = "SimpleDataGenerator.Sql is an open source library for .NET allow fill SQL Database tables with a larage amount of test data. Its primary goal is to allow developers testing database performance and scaling.",
        ProjectUrl              = new Uri("https://github.com/filippaluch/SimpleDataGenerator.Sql"),
        LicenseUrl              = new Uri("https://github.com/filippaluch/SimpleDataGenerator.Sql/blob/master/LICENSE"),
        Copyright               = "Filip Paluch 2016",
        Dependencies            = new []{
            new NuSpecDependency{
                Id              = "SimpleDataGenerator.Core",
                Version         = "1.0.0.5"
            }
        },        
        RequireLicenseAcceptance= false,
        Symbols                 = false,
        NoPackageAnalysis       = true,
        Files                   = new[] { new NuSpecContent {Source = "bin/Release/SimpleDataGenerator.Sql.dll", Target = "lib/net45"} },
        BasePath                = "../Source/SimpleDataGenerator.Sql",
        OutputDirectory         = ".."
    };
    
    NuGetPack(nuGetPackSettings);
});

 Task("Push-NuGet-Package")
     .IsDependentOn("Create-NuGet-Package")
     .Does(() =>
 {
     var package = "../SimpleDataGenerator.Sql." + EnvironmentVariable("APPVEYOR_BUILD_VERSION") +".nupkg";
                
     NuGetPush(package, new NuGetPushSettings {
         Source = "https://nuget.org/",
         ApiKey = EnvironmentVariable("NUGET_API_KEY")
     });
 });

Task("Default")
	.IsDependentOn("Create-NuGet-Package")
    .Does(() =>
{
    Information("SimpleDataGenerator.Sql building finished.");
});

RunTarget(target);