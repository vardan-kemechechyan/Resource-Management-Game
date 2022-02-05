using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableResource : MonoBehaviour
{
	[SerializeField] ResourceTypeNames r_Type;

	public ResourceTypeNames GetResourceType() { return r_Type; }
}
