# with [![Build Status](https://travis-ci.org/wallymathieu/with.png?branch=master)](https://travis-ci.org/wallymathieu/with) [![Build status](https://ci.appveyor.com/api/projects/status/d9g3sthe02ikx319/branch/master?svg=true)](https://ci.appveyor.com/project/wallymathieu/with/branch/master) [![NuGet](http://img.shields.io/nuget/v/with.svg)](https://www.nuget.org/packages/with/)

With is a small library written in c# intended for alternative constructions in c# to do things that may look clumsy in regular code.

Why is this library small? Parts of the library has been removed as c# has evolved (and my understanding of what can be useful in c#).

## What can we learn from "With"

Having access to [expressions](https://msdn.microsoft.com/en-us/library/system.linq.expressions.expression(v=vs.110).aspx) can help with doing extensions to a language in a relatively simple way.

## Examples

### Working with immutable data

If you need to get a copy of a readonly object but with some other value set in the new instance, you can use _With_. This is very similar to f# [copy and update record expression](https://msdn.microsoft.com/en-us/library/dd233184.aspx). The main abstraction is called a lens. Lenses answers the question "How do you read and update immutable data". It may help to think about them as properties for immutable data that you can combine and compose.  For further reading see the [Basic lens operation part of the wiki](https://github.com/wallymathieu/with/wiki/Basic-lens-operations)

#### Simplest example

```c#
using With;
using With.Lenses;
...
public class CustomerNameChangeHandler
{
    // start with initializing the lens expression once (main cost is around parsing expressions)
    private static readonly DataLens<Customer, string> NameLens =
        LensBuilder<Customer>
            .Of(m => m.Name)
            .Build();
    public void Handle()
    {
        // fetch customer, say:
        var customer = new Customer(id:1, name:"Johan Testsson");
        // get a new instance of that customer but with changed name:
        var changedNameToErik = CustomerNameLens.Set(customer, "Erik Testsson");
        // ...
    }
}
```

#### Settings several properties at the same time

```c#
using System;
using With;
using With.Lenses;
...
public class CustomerChangeHandler
{
    // start with initializing the lens expression once (main cost is around parsing expressions)
    private static readonly DataLens<Customer, (int, string, IEnumerable<string>)> CustomerIdNamePreferencesLens =
        LensBuilder<Customer>
            .Of(m => m.Id)
            .And(m => m.Name)
            .And(m => m.Preferences)
            .Build();
    public void Handle()
    {
        // fetch customer, say:
        var customer = new Customer(id:1, name:"Johan Testsson");
        // get a new instance of that customer but with id, name and preferences changed:
        var change = CustomerIdNamePreferencesLens.Set(customer, (NextId(), "Erik Testsson", new []{"Swedish fish"}) );
        // ...
    }
}
```

## Performance impact of working with immutable types (by using With) in c\#

To generate use the Timings project.

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19033
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 2.1.13 (CoreCLR 4.6.28008.01, CoreFX 4.6.28008.01), X64 RyuJIT
  DefaultJob : .NET Core 2.1.13 (CoreCLR 4.6.28008.01, CoreFX 4.6.28008.01), X64 RyuJIT

```

|                                     Method |     Mean |    Error |   StdDev |
|------------------------------------------- |---------:|---------:|---------:|
|      Using_static_prepered_copy_expression | 478.1 ns |  9.39 ns | 14.06 ns |
| Hand_written_method_returning_new_instance | 457.3 ns | 11.15 ns | 10.95 ns |
|                     Language_ext_generated | 476.0 ns |  9.42 ns | 14.38 ns |

As can be seen there is a slight penalty to use the different approaches. The naive hand written version has similar performance why this library might be good enough when you have access to reflection and expression compile on the platform.

The language ext approach has some disadvantages that you might be OK with, for instance that it complicates your build and makes it more dependent on specific build environment (though that could be fixed by contributing to [dotnet codegen](https://www.nuget.org/packages/dotnet-codegen)).

## Why shouldn't you use this library

The immutable data support in this library is done as an extensions to the language using the [expression](https://msdn.microsoft.com/en-us/library/system.linq.expressions.expression(v=vs.110).aspx) support in c#. A different way to add these things to c# would be to write some sort of [roslyn](https://github.com/dotnet/roslyn/) extension in order to extend the language in a way that can be generated at compile time. This is done for instance in [language ext](https://github.com/louthy/language-ext) codegen project. An approach such as that could be useful depending on your requirements.

On the .net platform there is already a language that allows you to write immutable first code in a terse and helpful way, [f#](https://fsharp.org/), you can find out more on: [f# for fun and profit](https://fsharpforfunandprofit.com/). Many programmers prefer to work in c#/java why this library or codegen makes more sense.

## Nuget

<https://www.nuget.org/packages/With>
