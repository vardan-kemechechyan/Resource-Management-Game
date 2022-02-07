using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Resource", menuName = "ScriptableObjects/ResourceType", order = 1 )]
public class ResourceTypeScriptableObject : ScriptableObject
{
	public ResourceTypeNames r_Type;
	public Color32 r_Color;
	public ResourceCreationDependency[] ProductionDependencies;
}

[System.Serializable]
public struct ResourceCreationDependency
{
	public ResourceTypeNames DependsOnResource;
	public int QuanityNeeded;
}
