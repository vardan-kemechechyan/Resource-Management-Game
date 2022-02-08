using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceDistribution
{
	CollectableResource UnloadResource( ResourceTypeNames? _resourceType = null );
	void LoadTheResourceIn( CollectableResource _resource, bool initialStart = false );
	bool CheckIfOverloaded();
	int GetResourceCount();
}
