using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] InventoryManager Inventory;
    [SerializeField] List<ResourceTypeNames> AllResourcesOnPlayer;

    [SerializeField] int InventoryStorageCapacity;

    IResourceDistribution InventoryManagement;

    Coroutine LoadUnloadInProgress;

    private void Start()
	{
        Inventory.InitializeTheInventory( InventoryStorageCapacity, AllResourcesOnPlayer );
        InventoryManagement = Inventory.GetComponent<IResourceDistribution>();
    }
	private void OnTriggerExit( Collider other )
	{
        if ( other.GetComponent<UnloadManager>() != null )
        {
            if( LoadUnloadInProgress != null )
            {
                StopCoroutine( LoadUnloadInProgress );

                LoadUnloadInProgress = null;
			}
		}
    }

	private void OnTriggerEnter( Collider other )
	{
		if(other.GetComponent<UnloadManager>() != null )
        {
            UnloadManager WarehousLoadManager = other.GetComponent<UnloadManager>();
            
            if ( WarehousLoadManager.GetWarehouseType() == WarehouseType.PRODUCED )
                LoadUnloadInProgress = StartCoroutine( PickUpResource( WarehousLoadManager ) );
            else
                LoadUnloadInProgress = StartCoroutine( LoadTheResourceIn( WarehousLoadManager ) );
		}
	}

	IEnumerator PickUpResource( UnloadManager WarehousLoadManager )
    {
        while( !InventoryManagement.CheckIfOverloaded() )
        {
            if( WarehousLoadManager.CanBePickedUp() )
            {
                CollectableResource pickRes = WarehousLoadManager.PickedUpResource();

                InventoryManagement.LoadTheResourceIn( pickRes );
                AllResourcesOnPlayer.Add( pickRes.GetResourceType() );
			}

            yield return new WaitForSeconds(.5f);
        }

        LoadUnloadInProgress = null;

        /*if( !InventoryManagement.CheckIfOverloaded() )
            if ( WarehousLoadManager.CanBePickedUp() )
            {
                CollectableResource pickRes = WarehousLoadManager.PickedUpResource();

                InventoryManagement.LoadTheResourceIn( pickRes );
                AllResourcesOnPlayer.Add( pickRes.GetResourceType() );
            }*/
    }

    IEnumerator LoadTheResourceIn( UnloadManager WarehousLoadManager ) 
    { 
        while( InventoryManagement.GetResourceCount() != 0 )
        {
            if( CheckResourceMatch( WarehousLoadManager.GetWarehouseResourceType() ) )
                if ( WarehousLoadManager.CanBeLoadedIn())
                {
                    WarehousLoadManager.TakeResourceFromPlayer( InventoryManagement.UnloadResource( WarehousLoadManager.GetWarehouseResourceType() ) );
                    AllResourcesOnPlayer.Remove( WarehousLoadManager.GetWarehouseResourceType() );
                }

            yield return new WaitForSeconds( .5f );
        }

        LoadUnloadInProgress = null;
    }

    bool CheckResourceMatch( ResourceTypeNames _warehouseResoureType )
    {
        foreach ( var resourceType in AllResourcesOnPlayer )
            if ( resourceType == _warehouseResoureType ) return true;

        return false;
    }
}