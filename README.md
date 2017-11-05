# with [![Build Status](https://travis-ci.org/wallymathieu/with.png?branch=master)](https://travis-ci.org/wallymathieu/with) [![Build status](https://ci.appveyor.com/api/projects/status/d9g3sthe02ikx319/branch/master?svg=true)](https://ci.appveyor.com/project/wallymathieu/with/branch/master) [![NuGet](http://img.shields.io/nuget/v/with.svg)](https://www.nuget.org/packages/with/)

With is a small library written in c# intended for alternative constructions in c# to do things that may look clumsy in regular code.

### What can we learn from "With"?

Having access to [expressions](https://msdn.microsoft.com/en-us/library/system.linq.expressions.expression(v=vs.110).aspx) can help with doing extensions to a language in a relatively simple way.

##Examples

### Working with immutable data

If you need to get a copy of a readonly object but with some other value set in the new instance, you can use _With_. This is very similar to f# [copy and update record expression](https://msdn.microsoft.com/en-us/library/dd233184.aspx).

	using With;
	...
	var customer = new Customer(id:1, name:"Johan Testsson");

	var changedNameToErik = customer.With(c => c.Name, "Erik Testsson")

	var changedNameToErik = customer.With(c => c.Name == "Erik Testsson")

	var changedNameToErik = customer.With().Eql(c => c.Name, "Erik Testsson").To()


If you want to get a copy of your Customer but made into a VipCustomer (where VipCustomer inherits from Customer).

	using With;
	...
	var customer = new Customer(id:1, name:"Johan Testsson");

	var vipCustomer = customer.As<VipCustomer>(m => m.Since, DateTime.Now);

	var vipCustomer = customer.As<VipCustomer>(m => m.Since == DateTime.Now)

	var vipCustomer = customer.As<VipCustomer>().Eql(m => m.Since, DateTime.Now).To()


If your object that you want to copy and update has a readonly ienumerable as a property, you can use extension methods in the namespace _ReadonlyEnumerable_ inside the With expression:

	using With;
	using With.ReadonlyEnumerable;
	...

	var order = _repository.GetOrder(command.OrderId);
	_repository.Save(order.With(o =>
	    o.Products.Add(_repository.GetProduct(command.ProductId))));

This creates a clone of the original order (that we assume is a readonly instance) and adds a product to its list of products. After we got our new instance, we tell our repository that this is the instance it's supposed to hold on to.


### Performance impact of working with immutable types (by using With) in c\#

To generate use the Timings project.

``` ini

BenchmarkDotNet=v0.10.10, OS=Windows 10 Redstone 1 [1607, Anniversary Update] (10.0.14393.1770)
Processor=Intel Core i7-4980HQ CPU 2.80GHz (Haswell), ProcessorCount=2
.NET Core SDK=2.0.2
  [Host]     : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT


```
|                   Method |          Mean |         Error |       StdDev |        Median |
|------------------------- |--------------:|--------------:|-------------:|--------------:|
|        Timing_equalequal | 235,200.21 ns | 3,753.0161 ns | 3,133.938 ns | 235,118.21 ns |
| Timing_propertyname_only |  10,929.76 ns |   193.5316 ns |   171.561 ns |  10,874.39 ns |
|        Timing_dictionary |   5,567.59 ns |   113.4770 ns |   292.920 ns |   5,547.14 ns |
|           Timing_ordinal |      32.13 ns |     0.6890 ns |     1.909 ns |      32.04 ns |
|            Timing_fluent |  12,565.05 ns |   337.4210 ns |   962.681 ns |  12,229.30 ns |
|           Timing_by_hand |      32.13 ns |     1.0674 ns |     3.114 ns |      31.12 ns |

``` ini

BenchmarkDotNet=v0.10.10, OS=Mac OS X 10.12
Processor=Intel Core i7-4980HQ CPU 2.80GHz (Haswell), ProcessorCount=8
.NET Core SDK=2.0.2
  [Host]     : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT


```
|                   Method |          Mean |         Error |        StdDev |
|------------------------- |--------------:|--------------:|--------------:|
|        Timing_equalequal | 261,939.94 ns | 5,348.8656 ns | 8,168.2827 ns |
| Timing_propertyname_only |  19,542.39 ns |   388.6640 ns |   415.8661 ns |
|        Timing_dictionary |   9,898.66 ns |   147.0804 ns |   137.5791 ns |
|           Timing_ordinal |      29.01 ns |     0.4138 ns |     0.3668 ns |
|            Timing_fluent |  18,668.42 ns |   365.5301 ns |   448.9038 ns |
|           Timing_by_hand |      31.26 ns |     0.6543 ns |     1.1287 ns |

### Reasoning about performance

As can be seen there is a penalty to use the expression where you need to compile parts of the expression to get values.

As always with performance. Many times, you need to be aware of the impact of different things. Writing in assembler is contraproductive since it's much harder to structure your code.

The benefit of using immutable types and _With_ is that you can get started with multithreaded programming and pass your objects to different threads. Later on if you find that some part of your code is done very often, then that part can be rewritten to something more performant.

## Why shouldn't you use this library?

The immutable data support in this library is done as an extensions to the language using the [expression](https://msdn.microsoft.com/en-us/library/system.linq.expressions.expression(v=vs.110).aspx) support in c#. A better way to add these things to c# would be to write some sort of [roslyn](https://github.com/dotnet/roslyn/) extension in order to extend the language in a way that can be optimised during compile time. Problem is that using c# in this manner is that it's not idiomatic c#.  A better way is to write some of your code in [f#](http://fsharp.org/) and be able to use pattern matching, immutable data structures and copy update expressions in for a language designed with these things in mind.

## Nuget

<https://www.nuget.org/packages/With>
