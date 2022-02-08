using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] InventoryManager Inventory;
    [SerializeField] List<ResourceTypeNames> AllResourcesOnPlayer;

    [SerializeField] int InventoryStorageCapacity;

    IResourceDistribution InventoryManagement;

    private void Start()
	{
        Inventory.InitializeTheInventory( InventoryStorageCapacity, AllResourcesOnPlayer );
        InventoryManagement = Inventory.GetComponent<IResourceDistribution>();
    }

	private void OnTriggerEnter( Collider other )
	{
		if(other.GetComponent<UnloadManager>() != null )
        {
            UnloadManager WarehousLoadManager = other.GetComponent<UnloadManager>();
            
            if ( WarehousLoadManager.GetWarehouseType() == WarehouseType.PRODUCED )
                PickUpResource( WarehousLoadManager );
            else 
                LoadTheResourceIn( WarehousLoadManager );
		}
	}

    public void PickUpResource( UnloadManager WarehousLoadManager )
    {
        if( !InventoryManagement.CheckIfOverloaded() )
            if ( WarehousLoadManager.CanBePickedUp() )
            {
                CollectableResource pickRes = WarehousLoadManager.PickedUpResource();

                InventoryManagement.LoadTheResourceIn( pickRes );
                AllResourcesOnPlayer.Add( pickRes.GetResourceType() );
            }
    }

    public void LoadTheResourceIn( UnloadManager WarehousLoadManager ) 
    { 
        if( InventoryManagement.GetResourceCount() != 0 )
            if( CheckResourceMatch( WarehousLoadManager.GetWarehouseResourceType() ) )
                if ( WarehousLoadManager.CanBeLoadedIn())
                {
                    WarehousLoadManager.TakeResourceFromPlayer( InventoryManagement.UnloadResource( WarehousLoadManager.GetWarehouseResourceType() ) );
                    AllResourcesOnPlayer.Remove( WarehousLoadManager.GetWarehouseResourceType() );
                }
    }

    bool CheckResourceMatch( ResourceTypeNames _warehouseResoureType )
    {
        foreach ( var resourceType in AllResourcesOnPlayer )
            if ( resourceType == _warehouseResoureType ) return true;

        return false;
    }
}