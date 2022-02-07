using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse_Base : MonoBehaviour
{
    [SerializeField] public WarehouseType wh_Type;
    [SerializeField] protected Transform ResourceStackingArea;

    [SerializeField] public ResourceTypeNames rs_Type;
    [SerializeField] protected int Capacity;
    [SerializeField] protected int InStock;

    [SerializeField] protected float z_axis_starting_point = 0.5f;

    [SerializeField] protected int NumberOfHorizontalSpawnPoints;
    [SerializeField] protected int NumberOfVerticalSpawnPoints;

    [SerializeField] protected List<GameObject> SpawnPoints = new List<GameObject>();

    [SerializeField] Material ProducedResourceColor;
    [SerializeField] Material ConsumedResourceColor;

    [SerializeField] protected List<CollectableResource> StoreResourceObjects = new List<CollectableResource>();

    bool InitialLoad = true;

    public void EnableThisWarehouse( FactoryWarehouseInformation _resourceManagementOrder)
    {
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

    public bool LoadTheResourceIn( CollectableResource _resource, bool initialStart = false )
    {
        if ( initialStart ) { InStock = 0; InitialLoad = false; }

        _resource.transform.position = SpawnPoints[ InStock++ ].transform.position;

        Transform trans = _resource.transform;

        _resource.transform.position = new Vector3( trans.position.x + trans.localScale.x / 2f, trans.position.y + trans.localScale.y / 2f, trans.position.z - trans.localScale.z / 2f ); 

        StoreResourceObjects.Add( _resource );

        return InStock < Capacity;
    }

    public void ConsumeResource( int quantity )
    {
		for ( int i = 0; i < quantity; i++ )
        {
            Destroy( StoreResourceObjects[ StoreResourceObjects.Count - 1 ].gameObject );
            StoreResourceObjects.RemoveAt( StoreResourceObjects.Count - 1 );
        }

        InStock -= quantity;
    }

    public int GetResourceCount() { return InStock; }

    public bool CheckIfWarehousFull() { return InStock < Capacity; }
}
