#with

With is a small library written in c# intended to help when using read only classes in c#.

##Examples

	new MyClass(1, "2").With(m => m.MyProperty, 3)
	
	new MyClass(1, "2").With(m => m.MyProperty == 3)

	new MyClass(1, "2").With<MyClass2>(valueOnlyInMyClass2)

