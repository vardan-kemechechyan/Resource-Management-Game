using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] InventoryManager Inventory;
    [SerializeField] List<ResourceTypeNames> AllResourcesOnPlayer;

    [SerializeField] int InventoryStorageCapacity;

    private void Start()
	{
        Inventory.InitializeTheInventory( InventoryStorageCapacity, AllResourcesOnPlayer );
    }
}