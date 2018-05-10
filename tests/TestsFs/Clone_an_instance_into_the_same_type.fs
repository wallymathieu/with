namespace WithFs
open Ploeh.AutoFixture.Xunit2
open Xunit
open With
type ``Clone an instance into the same type``()=
    [<Theory; AutoData>]
    member this.``A class should be able to create a clone with a property set`` (myClass:Customer, newValue:int)=
        let ret = myClass.With( (fun m -> m.id), newValue)
        Assert.Equal(newValue, ret.id)
        Assert.Equal(myClass.name, ret.name)

    (*
    [<Theory; AutoData>]
    member this.``A class should be able to create a clone with a property set using equal`` (myClass:Customer, newValue:int)=
        let ret = myClass.With( fun m -> m.id = newValue)
        Assert.Equal(newValue, ret.id)
        Assert.Equal(myClass.name, ret.name)
        *)

    (*
    [<Theory; AutoData>]
    member this.``A class should be able to create a clone with two property set using equal`` (myClass:Customer, newIntValue:int, newStrValue:string)=
        let ret = myClass.With( fun m -> m.id = newIntValue && m.name = newStrValue)
        Assert.Equal(newIntValue, ret.id)
        Assert.Equal(newStrValue, ret.name)
        *)

    (*
    [<Theory; AutoData>]
    member this.``A class should be able to create a clone with a property set using equal and another propertyvalue``(anInstance:Customer, anotherInstance:Customer)=
        let ret = anotherInstance.With(fun m -> m.id = anInstance.id)
        Assert.Equal(anInstance.id, ret.id)
        Assert.Equal(anotherInstance.name, ret.name)
        *)

    [<Theory; AutoData>]
    member this.``A class should be able to create a clone with a property set using eql`` (instance:Customer, newInt:int)=
        let ret = instance.With()
                    .Eql( (fun m -> m.id), newInt)
                    .To();
        Assert.Equal(newInt, ret.id);
        Assert.Equal(instance.name, ret.name);
