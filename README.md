#with [![Build status](https://ci.appveyor.com/api/projects/status/d9g3sthe02ikx319?svg=true)](https://ci.appveyor.com/project/wallymathieu/with)

With is a small library written in c# intended to help when using read only classes in c#.

##Examples
###With readonly value set in a new class
```
new MyClass(1, "2").With(m => m.MyProperty, 3)

// NOTE: this is relatively expensive since it compiles expression at runtime
new MyClass(1, "2").With(m => m.MyProperty == 3 && m.MyProperty2 == "value")
```

Or to change the type of MyClass to MyClass2
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
###Switch on regex
Or regular expression switch statements
```
var result = Switch.Regex(instance)
    .Case("m", m => FoundM())
    .Case("s", m => FoundS())
    .Case("[A-Z]{1}[a-z]{2}\\d{1,}", m => ParseMatch(m));
```
###Temporarily set value
```
using (Let.Object(myClass)
		  .Member(obj=>obj.Value)
		  .Be(temporaryValue))
{
	// myClass.Value will be here temporaryValue
}
// but not here
```
The let api can also be used in the following manner
```
using (Let.Member(()=>myClass.Value)
		  .Be(temporaryValue))
{
	// myClass.Value will be here temporaryValue
}
// but not here
```
It can also be used to set static variables.

##Nuget
<https://www.nuget.org/packages/With>
