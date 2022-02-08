using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse_Base : MonoBehaviour
{
    WarehouseManager SuperVisorWarehouseManager;

    [SerializeField] WarehouseType wh_Type;
    [SerializeField] Transform ResourceStackingArea;

    [SerializeField] ResourceTypeNames rs_Type;
    [SerializeField] int Capacity;
    [SerializeField] int InStock;

    [SerializeField] protected float z_axis_starting_point = 0.5f;

    [SerializeField] int NumberOfHorizontalSpawnPoints;
    [SerializeField] int NumberOfVerticalSpawnPoints;

    [SerializeField] List<GameObject> SpawnPoints = new List<GameObject>();

    [SerializeField] Material ProducedResourceColor;
    [SerializeField] Material ConsumedResourceColor;

    [SerializeField] List<CollectableResource> StoreResourceObjects = new List<CollectableResource>();

    bool InitialLoad = true;

    public void EnableThisWarehouse( FactoryWarehouseInformation _resourceManagementOrder)
    {
        SuperVisorWarehouseManager = transform.parent.GetComponent<WarehouseManager>();

        wh_Type = _resourceManagementOrder.WarehoustType;

        ResourceStackingArea.GetComponent<MeshRenderer>().material = wh_Type == WarehouseType.PRODUCED ? ProducedResourceColor : ConsumedResourceColor;

        rs_Type = _resourceManagementOrder.ProducedResource.r_Type;

        Capacity = _resourceManagementOrder.Capacity;
        InStock = _resourceManagementOrder.InStock;

        CreateStackingSpawnPoints();

        int inStock = _resourceManagementOrder.InStock;

        for ( int i = 0; i < inStock; i++ )
		{
            CollectableResource resAlreadyInWarehouse = ( ( GameObject )Resources.Load( _resourceManagementOrder.ProducedResource.r_Type.ToString() ) ).GetComponent<CollectableResource>();

            LoadTheResourceIn( Instantiate( resAlreadyInWarehouse ), InitialLoad );
        }
    }
    public void CreateStackingSpawnPoints()
    {
        float StackAreaWidth    = ResourceStackingArea.transform.localScale.x;
        float StackAreaHeight   = ResourceStackingArea.transform.localScale.z;

        float x_axis_starting_point = StackAreaWidth / 2f;

        float SpaceX = StackAreaWidth / NumberOfHorizontalSpawnPoints;
        float SpaceY = StackAreaHeight / NumberOfVerticalSpawnPoints;

        int NumberOfSpawnPoints = 0;

        for ( int i = 0; i < NumberOfHorizontalSpawnPoints; i++ )
		{
			for ( int j = 0; j < NumberOfVerticalSpawnPoints; j++ )
			{
                if ( NumberOfSpawnPoints >= Capacity ) return;

                SpawnPoints.Add(new GameObject($"{i}_{j}"));
                SpawnPoints[ SpawnPoints.Count - 1 ].transform.SetParent( gameObject.transform );
                SpawnPoints[ SpawnPoints.Count - 1 ].transform.localPosition = new Vector3( x_axis_starting_point - i * SpaceX, 0f, z_axis_starting_point + j * SpaceY );
                NumberOfSpawnPoints++;
            }
		}
    }
    public void ConsumeResource( int quantity = 1 )
    {
		for ( int i = 0; i < quantity; i++ )
        {
            Destroy( StoreResourceObjects[ StoreResourceObjects.Count - 1 ].gameObject );
            StoreResourceObjects.RemoveAt( StoreResourceObjects.Count - 1 );
        }

        InStock -= quantity;
    }
    public bool CheckIfResourceTypeMatchesWharehouseType( ResourceTypeNames _incomingResource)
    {
        return _incomingResource == rs_Type;
    }
    public WarehouseType GetWarehouseType() { return wh_Type; }
    public ResourceTypeNames GetResourceType() { return rs_Type; }
    
    //TODO: redo into interface
    public CollectableResource UnloadResource( ResourceTypeNames? _resourceType = null )
    {
        CollectableResource ResourceToUnload = StoreResourceObjects[ StoreResourceObjects.Count - 1 ];
        StoreResourceObjects.RemoveAt( StoreResourceObjects.Count - 1 );

        InStock --;

        SuperVisorWarehouseManager.CheckIfFactoryCanProduce();

        return ResourceToUnload;
    }
    public bool LoadTheResourceIn( CollectableResource _resource, bool initialStart = false )
    {
        if ( !(!CheckIfOverloaded() && CheckIfResourceTypeMatchesWharehouseType( _resource.GetResourceType() )) ) return false;

        if ( initialStart ) { InStock = 0; InitialLoad = false; }

        _resource.transform.SetParent( SpawnPoints[ InStock ].transform );

        _resource.transform.localPosition = Vector3.zero;

        Transform trans = _resource.transform;

        //_resource.transform.localPosition = new Vector3( trans.localPosition.x + trans.localScale.x / 2f, trans.localPosition.y + trans.localScale.y / 2f, trans.localPosition.z - trans.localScale.z / 2f );

        _resource.transform.localRotation = Quaternion.Euler( Vector3.zero );

        StoreResourceObjects.Add( _resource );

        InStock++;

        SuperVisorWarehouseManager.CheckIfFactoryCanProduce();

        return InStock < Capacity;
    }

    public bool CheckIfOverloaded() { return InStock >= Capacity; }
    public int GetResourceCount() { return InStock; }
}
