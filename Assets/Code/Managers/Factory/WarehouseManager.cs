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

	public void PrepareWarehouses( List<FactoryWarehouseInformation> manifest )
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

	public bool UnloadResource( CollectableResource _resource )
	{
		Warehouse_Base correctWH = AllWarehouses.Find( wh => wh.rs_Type == _resource.GetResourceType() );

		return correctWH.LoadTheResourceIn( _resource );
	}

	public int CheckResourceAvailability( ResourceTypeNames _resName )
	{
		return AllWarehouses.Find( wh => wh.rs_Type == _resName ).GetResourceCount();
	}

	public bool CheckWarehouseCapacityForProducedResource( WarehouseType _whType )
	{
		return AllWarehouses.Find( wh => wh.wh_Type == _whType ).CheckIfWarehousFull();
	}

	public void ConsumeResourcesForProduction( ResourceCreationDependency[] _prdDependencies)
	{
		if( _prdDependencies.Length != 0 )
			foreach ( var res in _prdDependencies )
				AllWarehouses.Find( wh => wh.rs_Type == res.DependsOnResource ).ConsumeResource( res.QuanityNeeded );
	}
}
