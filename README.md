# with [![Build Status](https://travis-ci.org/wallymathieu/with.png?branch=master)](https://travis-ci.org/wallymathieu/with) [![Build status](https://ci.appveyor.com/api/projects/status/d9g3sthe02ikx319/branch/master?svg=true)](https://ci.appveyor.com/project/wallymathieu/with/branch/master) [![NuGet](http://img.shields.io/nuget/v/with.svg)](https://www.nuget.org/packages/with/)

With is a small library written in c# intended for alternative constructions in c# to do things that may look clumsy in regular code.

## What can we learn from "With"

Having access to [expressions](https://msdn.microsoft.com/en-us/library/system.linq.expressions.expression(v=vs.110).aspx) can help with doing extensions to a language in a relatively simple way.

## Examples

### Working with immutable data

If you need to get a copy of a readonly object but with some other value set in the new instance, you can use _With_. This is very similar to f# [copy and update record expression](https://msdn.microsoft.com/en-us/library/dd233184.aspx). The main abstraction is called a lens. Lenses answers the question "How do you read and update immutable data". It may help to think about them as properties for immutable data that you can combine and compose.

You can think of a [data lens](http://hackage.haskell.org/package/lens-4.18.1/docs/Data-Data-Lens.html) as an instance of a class that implements the interface:

```c#
public interface IDataLens<T, TValue>
{
    TValue Get(T entity);
    T Set(T entity, TValue value);
}
```

There are some heavy to read examples of lenses. These are cool, but perhaps not as useful as what relative simple things you can achieve in business code using immutable data.

The basic operations are combine and compose.

- Combine

    ```c#
    Customer.IdLens.Combine(Customer.NameLens)
    ```

    Gives you a lens that gets/sets both Id and Name on the same class Customer. Given that the static properties IdLens and NameLens are Id and Name lenses for Customer.

    This can be thought of as doing the following for mutable code:

    ```c#
    customer.Id = newId;
    customer.Name = newName;
    ```

- Compose

    ```c#
    Sale.CustomerLens.Compose(Customer.NameLens)
    ```

    Gives you a lens that gets/sets both Name of the property Customer on the class Sale. Given that the static properties CustomerLens and NameLens are Customer and Name lenses for Sale and Customer.

    This can be thought of as doing the following for mutable code:

    ```c#
    sale.Customer.Name = newName;
    ```

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

#### Using pure c# without libraries

The basic operations needed to work with immutable data is get and set, so we need setter equivalents implemented as shown below:

```c#
public class Customer
{
    public Customer(int id, string name, IEnumerable<string> preferences)
    {
        this.Id = id;
        this.Name = name;
        this.Preferences = preferences;
    }
    public int Id { get; }
    public string Name { get; }
    public IEnumerable<string> Preferences { get; }

    public Customer SetId(int v)
    {
        return new Customer(v, Name, Preferences);
    }
    public Customer SetName(string v)
    {
        return new Customer(Id, v, Preferences);
    }
    public Customer SetPreferences(IEnumerable<string> v)
    {
        return new Customer(Id, Name, v);
    }

    // another approach if you do not use nulls would be:
    public With(int? id=null, string name=null, IEnumerable<string> preferences=null)
    {
        return new Customer(id??Id, name??Name, preferences??Preferences);
    }
}
```

## Enumerable extensions provided by the library

### Partition

The partition function takes a predicate and a collection and returns the pair of collections of elements which do and do not satisfy the predicate.

```c#
using With.Collections;
...
var partition = new[] { 1,2,3,4,5,6,7}.Partition(num=>num%2==0).ToArray();
// partition.True will be new[] { 2, 4, 6 }
// partition.False will be new[] { 1, 3, 5, 7 }
```

### Chunk

Enumerates over the items, chunking them together based on the return value of the block.

Consecutive elements which return the same block value are chunked together.

```c#
using With.Collections;
...
var array = new[] {3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5};
var chunked = new List<(bool, int[])>();

array.Chunk(n => n.Even()).Each((even, ary) => chunked.Add((even, ary.ToArray())));
Assert.Equal(new[]
{
    (false, new[] {3, 1}),
    (true, new[] {4}),
    (false, new[] {1, 5, 9}),
    (true, new[] {2, 6}),
    (false, new[] {5, 3, 5})
}, chunked.ToArray());
```

### Flatten

Returns a new array that is a one-dimensional flattening of self (recursively).

That is, for every element that is an array, extract its elements into the new array.

The optional level argument determines the level of recursion to flatten.

### Cycle

Yields each element of collection repeatedly n times or forever if null is given.
If a non-positive number is given or the collection is empty, returns an empty enumerable.

```c#
using With.Collections;
...
Assert.Equal(new[]{
    "a", "b", "c",
    "a", "b", "c"
}, new[] { "a", "b", "c" }.Cycle(2).ToArray());
```

### Pairwise

Used to iterate over collection and get the collection elements pairwise.
Yields the result of the application of the map function over each pair.

```c#
using With.Collections;
...
var pairs = Enumerable.Range(0, 4).Pairwise(Tuple.Create).ToArray();
// will be
Assert.Equal(new[] { Tuple.Create(0, 1), Tuple.Create(1, 2), Tuple.Create(2, 3) },pairs);
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

The language ext approach has some disadvantages that you might be OK with, for instance that it complicates your build and makes it more dependent on specific build environment (thought that could be fixed by contributing to [dotnet codegen](https://www.nuget.org/packages/dotnet-codegen)).

## Why shouldn't you use this library

The immutable data support in this library is done as an extensions to the language using the [expression](https://msdn.microsoft.com/en-us/library/system.linq.expressions.expression(v=vs.110).aspx) support in c#. A different way to add these things to c# would be to write some sort of [roslyn](https://github.com/dotnet/roslyn/) extension in order to extend the language in a way that can be generated at compile time. This is done for instance in [language ext](https://github.com/louthy/language-ext) codegen project. An approach such as that could be useful depending on your requirements.

On the .net platform there is already a language that allows you to write immutable first code in a terse and helpful way, [f#](https://fsharp.org/), you can find out more on: [f# for fun and profit](https://fsharpforfunandprofit.com/). Many programmers prefer to work in c#/java why this library or codegen makes more sense.

## Nuget

<https://www.nuget.org/packages/With>
