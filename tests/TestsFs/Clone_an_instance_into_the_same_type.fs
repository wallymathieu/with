﻿namespace WithFs
open System.Linq.Expressions

open Ploeh.AutoFixture.Xunit2
open Xunit
open With
type ``Clone an instance into the same type``()=
    let idCopy =lazy Prepare.Copy<Customer,int>( fun m v-> m.id = v)
    let idAndNameCopy =lazy Prepare.Copy<Customer,int,string>( fun m v1 v2-> m.id = v1 && m.name = v2)
    
    [<Theory; AutoData>]
    member this.``A class should be able to create a clone with a property set`` (myClass:Customer, newValue:int)=
        let ret = myClass.With( (fun m -> m.id), newValue)
        Assert.Equal(newValue, ret.id)
        Assert.Equal(myClass.name, ret.name)

    [<Theory; AutoData>]
    member this.``A class should be able to create a clone with a property set using equal`` (myClass:Customer, newValue:int)=
        let ret = idCopy.Value.Copy(myClass,newValue)
        Assert.Equal(newValue, ret.id)
        Assert.Equal(myClass.name, ret.name)

    [<Theory; AutoData>]
    member this.``A class should be able to create a clone with two property set using equal`` (myClass:Customer, newIntValue:int, newStrValue:string)=
        let ret = idAndNameCopy.Value.Copy(myClass,newIntValue,newStrValue)
        Assert.Equal(newIntValue, ret.id)
        Assert.Equal(newStrValue, ret.name)

    [<Theory; AutoData>]
    member this.``A class should be able to create a clone with a property set using eql`` (instance:Customer, newInt:int)=
        let ret = instance.With()
                    .Eql( (fun m -> m.id), newInt)
                    .Copy();
        Assert.Equal(newInt, ret.id);
        Assert.Equal(instance.name, ret.name);
