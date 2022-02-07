using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableResource : MonoBehaviour
{
	[SerializeField] ResourceTypeScriptableObject ResourceDescription;

	bool Pickable = false;
	bool OnPlayer = false;
	bool Unloaded = false;

	public ResourceTypeNames GetResourceType() { return ResourceDescription.r_Type; }
}
