using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWarehouseManager
{
	ResourceTypeNames GetStorageResourceType( WarehouseType _whT);
}
