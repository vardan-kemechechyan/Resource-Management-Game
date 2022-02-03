using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseManager : MonoBehaviour
{
	[SerializeField] Transform TriggerZone;

	[SerializeField] Transform[] Warehouses;

	public void ConstructWarehouses( WarehouseTypesAvailabe[] wh_Type )
	{
		int NumberOfWarehouses = -1;

		for ( int i = 0; i < Warehouses.Length; ++i )
			NumberOfWarehouses += Warehouses[ i ].GetComponent<IEnableWarehouses>().EnableThisWarehouse( wh_Type[i] );

		foreach ( var warehouse in Warehouses )
			warehouse.localPosition = new Vector3( warehouse.localPosition.x * NumberOfWarehouses, warehouse.localPosition.y, warehouse.localPosition.z);
	}
}
