#with [![Build Status](https://travis-ci.org/wallymathieu/with.png?branch=master)](https://travis-ci.org/wallymathieu/with) [![Build status](https://ci.appveyor.com/api/projects/status/d9g3sthe02ikx319?svg=true)](https://ci.appveyor.com/project/wallymathieu/with)

With is a small library written in c# intended for alternative constructions in c# to do things that may look clumsy in regular code.

##Examples
###With readonly value set in a new instance of the same class, but with all the other values from the old instance
```
new MyClass(1, "2").With(m => m.MyProperty, 3)

// NOTE: this is relatively expensive since it compiles expression at runtime
new MyClass(1, "2").With(m => m.MyProperty == 3 && m.MyProperty2 == "value")
```

Or to change the type of MyClass to MyClass2 (where MyClass2 inherits from MyClass)
```
new MyClass(1, "2").As<MyClass2>(m => m.MyProperty3, 3)

new MyClass(1, "2").As<MyClass2>(valueOnlyInMyClass2)

// NOTE: this is relatively expensive since it compiles expression at runtime
new MyClass(1, "2").As<MyClass2>(m => m.MyProperty3 == 3 && m.MyProperty2 == "value")
```
###Switch on type
You can use this library to switch on type
```
var result = Switch.On(instance)
    .Case((ClassWithMethodX c) => c.X)
    .Case((ClassWithMethodY c) => c.Y)
    .Case((ClassWithMethodZ c) => c.Z);
```
Or less fluently
```
var result = Switch.On(instance,
    (ClassWithMethodX c) => c.X,
    (ClassWithMethodY c) => c.Y,
    (ClassWithMethodZ c) => c.Z);
```
###Switch on range, func, etc
```
string result = Switch.Match<int,string> (v)
    .Case (1, () => "One!")
    .Case (new []{ 2, 3, 5, 7, 11 }, p => "This is a prime!")
    .Case (13.To (19), t => "A teen")
    .Case (i=>i==42,(i)=>"Meaning of life")
    .Case (i=>i==52,()=>"Some other number")
    .Else (_ => "Ain't special");
```
or less fluently
```
string result = Switch.Match<int,string> (v,
    1.AsArray(), _ => "One!",
    new []{ 2, 3, 5, 7, 11 }, _ => "This is a prime!",
    13.To (19), _ => "A teen")
    .Else (_ => "Ain't special");
```

###Switch on regex
When the ingoing type is a string, you can use a regex to match
```
var result = Switch.Match<string,string>(instance)
    .Case("m", m => FoundM())
    .Case("s", m => FoundS())
    .Regex("[A-Z]{1}[a-z]{2}\\d{1,}", m => ParseMatch(m));
```
###Temporarily set value
```
using (myClass.SetTemporary(obj => obj.Value, temporaryValue))
{
    // myClass.Value will return temporaryValue
}
// but not here
```

```
using (Let.Object(myClass)
		  .Member(obj=>obj.Value)
		  .Be(temporaryValue))
{
	// myClass.Value will return temporaryValue
}
// but not here
```
The let api can also be used in the following manner
```
using (Let.Member(()=>myClass.Value)
		  .Be(temporaryValue))
{
	// myClass.Value will return temporaryValue
}
// but not here
```
It can also be used to set static variables.

##Nuget
<https://www.nuget.org/packages/With>
