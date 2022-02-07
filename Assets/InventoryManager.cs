using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] List<CollectableResource> StoredResourceObjects = new List<CollectableResource>();
    [SerializeField] List<GameObject> SpawnPoints = new List<GameObject>();

    [SerializeField] float Y_Gap;

    int ResourcesAmountInInventory;
    int InventoryStorageCapacity;

    bool InitialLoad = true;

    public void InitializeTheInventory(int storageCapacity, List<ResourceTypeNames> _manifest)
    {
        InventoryStorageCapacity = storageCapacity;
        ResourcesAmountInInventory = _manifest.Count;

        CreateStackingSpawnPoints();

        int inStock = ResourcesAmountInInventory;

        for ( int i = 0; i < inStock; i++ )
        {
            CollectableResource resAlreadyInWarehouse = ( ( GameObject )Resources.Load( _manifest[ i ].ToString() ) ).GetComponent<CollectableResource>();

            LoadTheResourceIn( Instantiate( resAlreadyInWarehouse ), InitialLoad );
        }
    }

    public void CreateStackingSpawnPoints()
    {
        int NumberOfSpawnPoints = 0;

        for ( int i = 0; i < InventoryStorageCapacity; i++ )
        {
            if ( NumberOfSpawnPoints >= InventoryStorageCapacity ) return;

            SpawnPoints.Add( new GameObject( $"SP-{i}" ) );
            SpawnPoints[ SpawnPoints.Count - 1 ].transform.SetParent( gameObject.transform );
            SpawnPoints[ SpawnPoints.Count - 1 ].transform.localPosition = new Vector3( 0f, i * Y_Gap, 0f );
            NumberOfSpawnPoints++;
        }
    }

    public void LoadTheResourceIn( CollectableResource _resource, bool initialStart = false )
    {
        if ( initialStart ) { ResourcesAmountInInventory = 0; InitialLoad = false; }

        _resource.transform.SetParent( SpawnPoints[ ResourcesAmountInInventory++ ].transform );

        _resource.transform.localPosition = SpawnPoints[ ResourcesAmountInInventory++ ].transform.localPosition;

        StoredResourceObjects.Add( _resource );
    }
}
