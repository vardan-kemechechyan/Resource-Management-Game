using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseManager : MonoBehaviour, IWarehouseManager
{
	[SerializeField] List<Warehouse_Base> AllWarehouses;

	public ResourceTypeNames GetStorageResourceType( WarehouseType _whT )
	{
		throw new System.NotImplementedException();
	}

	public void PrepareWarehouses( List<FactoryResourceInformation> manifest )
	{
		AllWarehouses = new List<Warehouse_Base>( GetComponentsInChildren<Warehouse_Base>());

		for ( int i = 0; i < AllWarehouses.Count; ++i )
		{
			if ( i < manifest.Count )
				AllWarehouses[ i ].EnableThisWarehouse( manifest[ i ] );
			else
				AllWarehouses[ i ].gameObject.SetActive( false );
		}
	}
}
