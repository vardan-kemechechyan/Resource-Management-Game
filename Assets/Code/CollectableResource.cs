using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableResource : MonoBehaviour
{
	[SerializeField] ResourceTypeScriptableObject ResourceDescription;

	bool Pickable = true;
	bool OnPlayer = false;

	public ResourceTypeNames GetResourceType() { return ResourceDescription.r_Type; }

	public bool CanBePickedUp()
	{
		if( Pickable && !OnPlayer )
		{
			Pickable = false;
			OnPlayer = true;

			return true;
		}

		return false;
	}
}
