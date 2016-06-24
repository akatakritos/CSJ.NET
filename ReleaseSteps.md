## Release Steps

1. Cut release branch from develop
2. Bump version in `AssemblyInfo.cs`
3. Update changelog
4. Update nuget release notes in CSJ.NET.nuspec
5. PR release branch to master
6. If CI passes, merge PR to master
7. Tag master with new version
8. Build nuget: `nuget pack CSJ.NET.csproj`
9. Push nuget: `nuget push CSJ.NET.$VERSION.nupkg`
10. Merge release branch back into develop
11. Delete release branch