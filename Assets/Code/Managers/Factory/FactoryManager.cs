using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
	[SerializeField] WarehouseTypesAvailabe[] WH_Type;
	
	[SerializeField] WarehouseManager WH_Manager;

	private void Start()
	{
		WH_Manager.ConstructWarehouses( WH_Type );
	}
}

public enum WarehouseTypesAvailabe
{
	PRODUCED_ITEMS,
	CONSUMABLE_ITEMS,
	NONE
}
