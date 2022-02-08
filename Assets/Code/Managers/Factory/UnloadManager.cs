using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadManager : MonoBehaviour
{
    [SerializeField] Warehouse_Base Warehouse;
	[SerializeField] WarehouseType wh_type;
	[SerializeField] ResourceTypeNames rsc_type;

	IResourceDistribution StorageManagement;

	private void Start() 
	{
		StorageManagement = Warehouse.GetComponent<IResourceDistribution>();
		wh_type = Warehouse.GetWarehouseType(); 
		rsc_type = Warehouse.GetResourceType();
	}
	
	public bool CanBePickedUp() { return StorageManagement.GetResourceCount() != 0; }
	public bool CanBeLoadedIn() { return !StorageManagement.CheckIfOverloaded(); }

	public WarehouseType GetWarehouseType() 
	{ 
		if( wh_type == WarehouseType.NONE ) 
			wh_type = Warehouse.GetWarehouseType();
			
		return wh_type; 
	}
	public ResourceTypeNames GetWarehouseResourceType() 
	{
		if ( rsc_type == ResourceTypeNames.NONE )
			rsc_type = Warehouse.GetResourceType();

		return rsc_type;
	}

	public CollectableResource PickedUpResource() { return StorageManagement.UnloadResource(); }
	public void TakeResourceFromPlayer( CollectableResource _resourceFromPlayer) 
	{
		StorageManagement.LoadTheResourceIn( _resourceFromPlayer );
	}
}
