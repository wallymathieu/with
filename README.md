#with [![Build Status](https://travis-ci.org/wallymathieu/with.png?branch=master)](https://travis-ci.org/wallymathieu/with) [![Build status](https://ci.appveyor.com/api/projects/status/d9g3sthe02ikx319?svg=true)](https://ci.appveyor.com/project/wallymathieu/with)

With is a small library written in c# intended for alternative constructions in c# to do things that may look clumsy in regular code.

##Examples
###With readonly value set in a new instance of the same class, but with all the other values from the old instance

```
var customer = new Customer(id:1, name:"Johan Testsson");

var changedNameToErik = customer.With(c => c.Name, "Erik Testsson")

var changedNameToErik = customer.With(c => c.Name == "Erik Testsson")
```

Or to change the type of Customer to VipCustomer (where VipCustomer inherits from Customer).

```
var customer = new Customer(id:1, name:"Johan Testsson");

var vipCustomer = customer.As<VipCustomer>(m => m.Since, DateTime.Now);

var vipCustomer = customer.As<VipCustomer>(DateTime.Now);

var vipCustomer = customer.As<VipCustomer>(m => m.Since == DateTime.Now)
```

Sample usage to add product to a copy of order

```
var order = _repository.GetOrder(command.OrderId);
_repository.Save(order.With(o =>
    o.Products.Add(_repository.GetProduct(command.ProductId))));
```
This creates a clone of the original order (that we assume is a readonly instance) and adds a product
to its list of products. After we got our new instance, we tell our repository that this is the instance
that it's supposed to hold on to.

###Switch on type
You can use this library to switch on type. This is generally considered bad practise. An object (i.e. in c# a type) should generally self determine what to do in most cases. Sometimes you might not want to introduce a dependency, thus want to react to an interface or type in a separete assembly.
```
var result = Switch.Match<Customer,DateTime?>(instance)
    .Case((VipCustomer c) => c.Since)
    .Case((RegularCustomer c) => c.Since)
    .Else(_=>null);
```
###Switch on range, func, etc
Note that switch statements should generally be short to avoid confusing logic jumps. A switch is very similar to a goto (in parser code where goto is missing, it's implemented by switch).
```
string result = Switch.Match<int,string> (v)
    .Case (1, _ => "One!")
    .Case (new []{ 2, 3, 5, 7, 11 }, _ => "This is a prime!")
    .Case (13.To (19), _ => "A teen")
    .Case (i=>i==42, _ =>"Meaning of life")
    .Case (i=>i==52, _ =>"Some other number")
    .Else (_ => "Ain't special");
```
or less fluently
```
string result = Switch.Match<int,string> (v,
    new []{ 1 }, _ => "One!",
    new []{ 2, 3, 5, 7, 11 }, _ => "This is a prime!",
    13.To (19), _ => "A teen")
    .Else (_ => "Ain't special");
```

###Switch on regex
When the ingoing type is a string, you can use a regex to match
```
var result = Switch.Match<string,string>(instance)
    .Case("m", _ => FoundM())
    .Case("s", _ => FoundS())
    .Regex("[A-Z]{1}[a-z]{2}\\d{1,}", m => ParseMatch(m));
```

### Timings to copy readonly class with a different value for a property using [version](https://github.com/wallymathieu/with/commit/c0a778017e77d8e6b36c148f675f467ee4c87b93)
To generate use the Timings project.
Sample times running it on windows 8.1 on .net 4.0 (with .net 4.5 installed)

     Name                     | Elapsed          | Quotient
     ------------------------ | ---------------- | --------:
     Timing by hand           | 00:00:00.0008733 |        1
     Timing fluent            | 00:00:00.0107707 |       12
     Timing propertyname only | 00:00:00.0151869 |       17
     Timing dictionary        | 00:00:00.0316233 |       36
     Timing equalequal        | 00:00:00.5359095 |      613

Sample times running it on mac os x with mono.

     Name                     | Elapsed          | Quotient
     ------------------------ | ---------------- | --------:
     Timing by hand           | 00:00:00.0021484 |        1
     Timing dictionary        | 00:00:00.0394724 |       18
     Timing propertyname only | 00:00:00.0511035 |       23
     Timing fluent            | 00:00:00.0537641 |       25
     Timing equalequal        | 00:00:00.2248041 |      104


##Nuget
<https://www.nuget.org/packages/With>
