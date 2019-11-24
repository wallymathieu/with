# with [![Build Status](https://travis-ci.org/wallymathieu/with.png?branch=master)](https://travis-ci.org/wallymathieu/with) [![Build status](https://ci.appveyor.com/api/projects/status/d9g3sthe02ikx319/branch/master?svg=true)](https://ci.appveyor.com/project/wallymathieu/with/branch/master) [![NuGet](http://img.shields.io/nuget/v/with.svg)](https://www.nuget.org/packages/with/)

With is a small library written in c# intended for alternative constructions in c# to do things that may look clumsy in regular code.

## What can we learn from "With"

Having access to [expressions](https://msdn.microsoft.com/en-us/library/system.linq.expressions.expression(v=vs.110).aspx) can help with doing extensions to a language in a relatively simple way.

## Examples

### Working with immutable data

If you need to get a copy of a readonly object but with some other value set in the new instance, you can use _With_. This is very similar to f# [copy and update record expression](https://msdn.microsoft.com/en-us/library/dd233184.aspx).

```c#
using With;
...
public class CustomerNameChangeHandler
{
    // start with initializing the copy update expression once (main cost is around parsing expressions)
    private static readonly IPreparedCopy<Customer, string> NameCopy =
        Prepare.Copy<Customer,string>((m,v) => m.Name == v);
    public void Handle()
    {
        // fetch customer, say:
        var customer = new Customer(id:1, name:"Johan Testsson");
        // change the name of that customer:
        var changedNameToErik = NameCopy.Copy(customer, "Erik Testsson");
        // ...
    }
}
```

Another alternative is to use lens to formulate prepared copy expression:

```c#
using With;
...
public class CustomerChangeHandler
{
    // start with initializing the copy update expression once (main cost is around parsing expressions)
    private static readonly IPreparedCopy<Customer, int, string, IEnumerable<string>> CopyUpdate =
        LensBuilder<Customer>.Of(m => m.Id).And(m => m.Name).And(m => m.Preferences)
                          .BuildPreparedCopy();
    public void Handle()
    {
        // fetch customer, say:
        var customer = new Customer(id:1, name:"Johan Testsson");
        // change that customer with new values:
        var change = CopyUpdate.Copy(customer, NextId(), "Erik Testsson", new []{"Swedish fish"});
        // ...
    }
}
```

### Performance impact of working with immutable types (by using With) in c\#

To generate use the Timings project.

``` ini
BenchmarkDotNet=v0.10.10, OS=Windows 10.0.19023
Processor=Intel Core i7-8650U CPU 1.90GHz, ProcessorCount=8
.NET Core SDK=3.0.100
  [Host]     : .NET Core 2.1.13 (Framework 4.6.28008.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.13 (Framework 4.6.28008.01), 64bit RyuJIT
```

|               Method |     Mean |    Error |   StdDev |
|--------------------- |---------:|---------:|---------:|
| Timing using library | 624.4 ns | 16.50 ns | 47.86 ns |
| When writing in code | 548.6 ns | 12.37 ns | 12.70 ns |

As can be seen there is a slight penalty to use the different constructs. The naive hand written version has similar performance why this library might be good enough when you have access to reflection and expression compile on the platform.

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
var pairs = Enumerable.Range(0, 3).Pairwise(Tuple.Create).ToArray(); 
// will be 
Assert.Equal(new[] { Tuple.Create(0, 1), Tuple.Create(1, 2), Tuple.Create(2, 3) },pairs);
```


## Why shouldn't you use this library?

The immutable data support in this library is done as an extensions to the language using the [expression](https://msdn.microsoft.com/en-us/library/system.linq.expressions.expression(v=vs.110).aspx) support in c#. A better way to add these things to c# would be to write some sort of [roslyn](https://github.com/dotnet/roslyn/) extension in order to extend the language in a way that can be optimised during compile time. Problem is that using c# in this manner is that it's not idiomatic c#.  A better way is to write some of your code in [f#](http://fsharp.org/) and be able to use pattern matching, immutable data structures and copy update expressions in for a language designed with these things in mind.

## Nuget

<https://www.nuget.org/packages/With>
