# CSJ.NET - Comma Separated JSON for .NET

Simple implementation of [Comma Separated JSON](http://www.kirit.com/Comma%20Separated%20JSON)
for C#.

## Installing

Nuget package coming sooner or later. For now, its two files. Just copy-paste.

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
var pocos = deserializer.Deserialize(csk); // returns IEnumerable<MyPoco>
```

## Roadmap

Some features that would be nice to add. I'll create issues for them, feel free to dive in.

 * [ ] Serialize without header
 * [ ] Specify order of columns
 * [ ] Support `[JsonProperty]` attribute
 * [ ] Streaming support