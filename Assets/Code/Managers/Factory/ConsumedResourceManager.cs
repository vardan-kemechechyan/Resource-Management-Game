using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumedResourceManager : Warehouse_Base, IEnableWarehouses
{
    public int EnableThisWarehouse( WarehouseTypesAvailabe _type )
    {
        bool TypesMatch = _type == WH_MyType;

        gameObject.SetActive( TypesMatch );

        if ( TypesMatch ) CreateStackingSpawnPoints();

        return TypesMatch ? 1 : 0;
    }
}
