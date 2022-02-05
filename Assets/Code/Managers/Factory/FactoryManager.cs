using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
	[SerializeField] WarehouseManager WH_Manager;

	[SerializeField] List<FactoryResourceInformation> ResourceManagementManifest;

	private void Start()
	{
		WH_Manager.PrepareWarehouses( ResourceManagementManifest );
	}
}

[System.Serializable]
public struct FactoryResourceInformation
{
	public ResourceTypeNames ResourceType;
	public WarehouseType WarehoustType;
	public int Capacity;
	public int InStock;

	public FactoryResourceInformation( ResourceTypeNames _rt, WarehouseType _wt, int _cpty, int _stck)
	{
		ResourceType = _rt;
		WarehoustType = _wt;
		Capacity = _cpty;
		InStock = _stck;
	}
}
