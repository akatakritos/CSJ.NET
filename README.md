# CSJ.NET - Comma Separated JSON for .NET
[![Build status (develop)](https://ci.appveyor.com/api/projects/status/f3qha9spaw9yv2c7/branch/develop?svg=true)](https://ci.appveyor.com/project/akatakritos/csj-net/branch/master)
[![NuGet](https://img.shields.io/nuget/v/CSJ.NET.svg?maxAge=2592000)](https://www.nuget.org/packages/CSJ.NET/)

Simple implementation of [Comma Separated JSON](http://www.kirit.com/Comma%20Separated%20JSON)
for C#.

## Installing

CSJ.NET is available on Nuget and can be installed with the Manage Nuget Packages command, or via
the Package Manager Console:

```powershell
PM> Install-Package CSJ.NET
```

## Usage

### Serializing

```csharp
var pocos = new List<MyPoco>(){ /* ... */ };
var serializer = new CsjSerializer<MyPoco>();
var output = serializer.Serialize(pocos); // returns CSJ
```

### Deserializing

```csharp
string csj = ""; /* whatever */
var deserializer = new CsjDeserializer<MyPoco>();
var pocos = deserializer.Deserialize(csj); // returns IEnumerable<MyPoco>
```

## Roadmap

Some features that would be nice to add. I'll create issues for them, feel free to dive in.

 * [ ] Serialize without header
 * [ ] Specify order of columns
 * [ ] Support `[JsonProperty]` attribute
 * [ ] Streaming support
