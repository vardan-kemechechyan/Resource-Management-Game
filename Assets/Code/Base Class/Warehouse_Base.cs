using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse_Base : MonoBehaviour
{
    [SerializeField] protected WarehouseType wh_Type;
    [SerializeField] protected Transform ResourceStackingArea;

    [SerializeField] protected ResourceTypeNames rs_Type;
    [SerializeField] protected int Capacity;
    [SerializeField] protected int InStock;

    [SerializeField] protected float z_axis_starting_point = 0.5f;

    [SerializeField] protected int NumberOfHorizontalSpawnPoints;
    [SerializeField] protected int NumberOfVerticalSpawnPoints;

    [SerializeField] protected List<GameObject> SpawnPoints = new List<GameObject>();

    [SerializeField] Material ProducedResourceColor;
    [SerializeField] Material ConsumedResourceColor;

    public void EnableThisWarehouse( FactoryResourceInformation _resourceManagementOrder)
    {
        wh_Type = _resourceManagementOrder.WarehoustType;

        ResourceStackingArea.GetComponent<MeshRenderer>().material = wh_Type == WarehouseType.PRODUCED ? ProducedResourceColor : ConsumedResourceColor;

        rs_Type = _resourceManagementOrder.ResourceType;

        Capacity = _resourceManagementOrder.Capacity;
        InStock = _resourceManagementOrder.InStock;

        CreateStackingSpawnPoints();
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
}
