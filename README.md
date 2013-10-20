#with

With is a small library written in c# intended to help when using read only classes in c#.

##Examples

	new MyClass(1, "2").With(m => m.MyProperty, 3)
	
	new MyClass(1, "2").With(m => m.MyProperty == 3 && m.MyProperty2 == "value")

Or to change the type of MyClass to MyClass2

	new MyClass(1, "2").As<MyClass2>(m => m.MyProperty3, 3)
	
	new MyClass(1, "2").As<MyClass2>(m => m.MyProperty3 == 3 && m.MyProperty2 == "value")

	new MyClass(1, "2").As<MyClass2>(valueOnlyInMyClass2)

Nuget:
<https://www.nuget.org/packages/With>
