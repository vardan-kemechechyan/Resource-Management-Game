using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IResourceDistribution
{
    [SerializeField] List<CollectableResource> StoredResourceObjects = new List<CollectableResource>();
    [SerializeField] List<GameObject> SpawnPoints = new List<GameObject>();

    [SerializeField] float Y_Gap;

    int InStock;
    int InventoryStorageCapacity;

    bool InitialLoad = true;

    public void InitializeTheInventory(int storageCapacity, List<ResourceTypeNames> _manifest)
    {
        InventoryStorageCapacity = storageCapacity;
        InStock = _manifest.Count;

        CreateStackingSpawnPoints();

        int inStock = InStock;

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
    
    public CollectableResource UnloadResource( ResourceTypeNames? _resourceType ) 
    {
        CollectableResource ResourceToUnload = null;

        if ( _resourceType != null )
			for ( int i = 0; i < StoredResourceObjects.Count; i++ )
                if( StoredResourceObjects[i].GetResourceType() == _resourceType )
                {
                    ResourceToUnload = StoredResourceObjects[ i ];

                    StoredResourceObjects.RemoveAt( i );

                    InStock--;

                    for ( int j = i; j < StoredResourceObjects.Count; j++ )
					{
                        if( StoredResourceObjects[j] != null )
                        {
                            StoredResourceObjects[ j ].transform.SetParent( SpawnPoints[ j ].transform );
                            StoredResourceObjects[ j ].transform.localPosition = SpawnPoints[ j ].transform.localPosition;               
						}
                    }

                    break;
                }

        return ResourceToUnload;
    }
    public bool LoadTheResourceIn( CollectableResource _resource, bool initialStart = false )
    {
        if ( initialStart ) { InStock = 0; InitialLoad = false; }

        _resource.transform.SetParent( SpawnPoints[ InStock ].transform );

        _resource.transform.localPosition = SpawnPoints[ InStock ].transform.localPosition;

        _resource.transform.localRotation = Quaternion.Euler( Vector3.zero );

        InStock++;

        StoredResourceObjects.Add( _resource );

        return InStock < InventoryStorageCapacity;
    }
    public bool CheckIfOverloaded()
    {
        return InStock >= InventoryStorageCapacity;
    }
    public int GetResourceCount() { return InStock; }
}
