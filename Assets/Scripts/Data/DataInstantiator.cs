using UnityEngine;
using System;
using System.Collections.Generic;
/// <summary>
/// Example Usage of Instantiator:
/// ObjectOfAnyType exampleObject = DataInstantiator.GetNewInstace( () => new ObjectOfAnyType() );
/// </summary>
public class DataInstantiator {
    public static T GetNewInstance<T> ( Func<T> createNewInstance ) {
        return createNewInstance ( );
    }
}
