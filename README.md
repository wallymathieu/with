#with [![Build Status](https://travis-ci.org/wallymathieu/with.png?branch=master)](https://travis-ci.org/wallymathieu/with) [![Build status](https://ci.appveyor.com/api/projects/status/d9g3sthe02ikx319/branch/master?svg=true)](https://ci.appveyor.com/project/wallymathieu/with/branch/master) [![NuGet](http://img.shields.io/nuget/v/with.svg)](https://www.nuget.org/packages/with/)

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

###Switch on type
You can use this library to switch on type. This is generally considered bad practise. An object (i.e. in c# a type) should generally self determine what to do in most cases. Sometimes you might not want to introduce a dependency, thus want to react to an interface or type in a separete assembly.

	using With;
	...
	var result = Switch.Match<Customer,DateTime?>(instance)
	    .Case((VipCustomer c) => c.Since)
	    .Case((RegularCustomer c) => c.Since)
	    .Else(_=>null);

###Switch on range, func, etc
Note that switch statements should generally be short to avoid confusing logic jumps. A switch is very similar to a goto (in parser code where goto is missing, it's implemented by switch).

	using With;
	...
	string result = Switch.Match<int,string> (v)
	    .Case (1, _ => "One!")
	    .Case (new []{ 2, 3, 5, 7, 11 }, _ => "This is a prime!")
	    .Case (13.To (19), _ => "A teen")
	    .Case (i=>i==42, _ =>"Meaning of life")
	    .Case (i=>i==52, _ =>"Some other number")
	    .Else (_ => "Ain't special");

or less fluently

	using With;
	...
	string result = Switch.Match<int,string> (v,
	    new []{ 1 }, _ => "One!",
	    new []{ 2, 3, 5, 7, 11 }, _ => "This is a prime!",
	    13.To (19), _ => "A teen")
	    .Else (_ => "Ain't special");


###Switch on regex
When the ingoing type is a string, you can use a regex to match

	using With;
	...
	var result = Switch.Match<string,string>(instance)
	    .Case("m", _ => FoundM())
	    .Case("s", _ => FoundS())
	    .Regex("[A-Z]{1}[a-z]{2}\\d{1,}", m => ParseMatch(m));

### Destructure tuples

	using With.Destructure;
	...
	var tuple = Tuple.Create(customerName, customerCode);
	...
	tuple.Let((customerName, customerCode) => { ... });

The benefit of using this form is that you can give the tuple fields relevant names directly.

### Destructure IEnumerable

	using With.Destructure;
	...
	var list = new []{ 
		"CustomerName", customerName, 
		"CustomerCode", customerCode 
	};
	...
	var enumerator = list.ToEnumerator();

	enumerator.Let((customerName, customerCode) => { ... });
	enumerator.Let((customerName, customerCode) => { ... });


### Performance impact of working with immutable types (by using With) in c# [for version](https://github.com/wallymathieu/with/tree/immutable_object_graph)
To generate use the Timings project.
Sample times running it on windows 10 on .net 4.5.2 (with .net 4.6.1 installed)

     Name                     | Elapsed          | Quotient
     ------------------------ | ---------------- | --------:
     Timing by hand           | 00:00:00.0011304 |        1
     Timing [immutable object graph](https://github.com/AArnott/ImmutableObjectGraph)| 00:00:00.0073137|   6
     Timing dictionary        | 00:00:00.0088569 |        7
     Timing fluent            | 00:00:00.0145144 |       12
     Timing propertyname only | 00:00:00.0414962|       36
     Timing equalequal        | 00:00:00.5723422|       506

Sample times running it on mac os x with mono.

     Name                     | Elapsed          | Quotient
     ------------------------ | ---------------- | --------:
     Timing by hand           | 00:00:00.0019968 |        1
     Timing [immutable object graph](https://github.com/AArnott/ImmutableObjectGraph)| 00:00:00.0036995|   1
     Timing dictionary        | 00:00:00.0269027 |       13
     Timing propertyname only | 00:00:00.0379708 |       19
     Timing fluent            | 00:00:00.0427954 |       21
     Timing equalequal        | 00:00:00.1443597 |       72

### Reasoning about performance
As can be seen there is a penalty to use the expression where you need to compile parts of the expression to get values.

As always with performance. Many times, you need to be aware of the impact of different things. Writing in assembler is contraproductive since it's much harder to structure your code.

The benefit of using immutable types and _With_ is that you can get started with multithreaded programming and pass your objects to different threads. Later on if you find that some part of your code is done very often, then that part can be rewritten to something more performant.

## Why shouldn't you use this library?

Some of the functionality here looks to be obsolete with [c# 7 or later](https://github.com/dotnet/roslyn/blob/future/docs/features/patterns.md).

The immutable data support in this library is done as an extensions to the language using the [expression](https://msdn.microsoft.com/en-us/library/system.linq.expressions.expression(v=vs.110).aspx) support in c#. A better way to add these things to c# would be to write some sort of [roslyn](https://github.com/dotnet/roslyn/) extension in order to extend the language in a way that can be optimised during compile time. Problem is that using c# in this manner is that it's not idiomatic c#.  A better way is to write some of your code in [f#](http://fsharp.org/) and be able to use pattern matching, immutable data structures and copy update expressions in for a language designed with these things in mind.

##Nuget
<https://www.nuget.org/packages/With>
