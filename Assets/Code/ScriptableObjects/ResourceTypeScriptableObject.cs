using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Resource", menuName = "ScriptableObjects/ResourceType", order = 1 )]
public class ResourceTypeScriptableObject : ScriptableObject
{
	[SerializeField] GameObject ResourceObject;
	
	[SerializeField] ResourceTypeNames r_Type;
	[SerializeField] Color32 r_Color;

	bool Pickable = false;
	bool OnPlayer = false;
	bool Unloaded = false;
}
