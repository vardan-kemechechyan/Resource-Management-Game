using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
	public Action<float> ProducingOneResourceUnit = delegate(float _progress) { }; 

	CollectableResource ProducedResourcePrefab;

	[SerializeField] float ProductionTime;

	[SerializeField] WarehouseManager WH_Manager;

	[SerializeField] List<FactoryWarehouseInformation> ResourceManagementManifest;
	
	FactoryWarehouseInformation WarehouseForProducedResource;

	bool FactoryHasEnoughCapacity = true;
	bool FactoryHasEnoughResources = true;

	private void Start()
	{
		WarehouseForProducedResource = ResourceManagementManifest.Find( x => x.WarehoustType == WarehouseType.PRODUCED );

		ProducedResourcePrefab = ((GameObject) Resources.Load( WarehouseForProducedResource.ProducedResource.r_Type.ToString() )).GetComponent<CollectableResource>();

		WH_Manager.PrepareWarehouses( ResourceManagementManifest );

		CheckResourceAvailability();

		CheckWarehouseCapacityForProducedResource();

		StartCoroutine( ProduceElement() );

		print($"FactoryHasEnoughCapacity: {FactoryHasEnoughCapacity}\nFactoryHasEnoughResources: {FactoryHasEnoughResources}" );
	}

	IEnumerator ProduceElement()
	{
		float productionInProgress = 0f;

		while( FactoryHasEnoughCapacity && FactoryHasEnoughResources ) //true -> there are resources or place
		{
			print( $"Producing Resource {WarehouseForProducedResource.ProducedResource.r_Type}" );

			while ( (productionInProgress += Time.deltaTime) < ProductionTime )
			{
				ProducingOneResourceUnit.Invoke( productionInProgress / ProductionTime );

				yield return true;
			}

			productionInProgress = 0f;

			WH_Manager.ConsumeResourcesForProduction( WarehouseForProducedResource.ProducedResource.ProductionDependencies );

			WH_Manager.UnloadResource ( Instantiate( ProducedResourcePrefab ) );

			ProducingOneResourceUnit.Invoke( 0f );

			CheckResourceAvailability();

			CheckWarehouseCapacityForProducedResource();

			yield return new WaitForSeconds(0.5f);

			print($"Produced Resource {WarehouseForProducedResource.ProducedResource.r_Type}; In Stock {WH_Manager.CheckResourceAvailability( WarehouseForProducedResource.ProducedResource.r_Type )}");	
		}
	}

	void CheckResourceAvailability()
	{
		if ( WarehouseForProducedResource.ProducedResource.ProductionDependencies.Length != 0 )
		{
			foreach ( var resType in WarehouseForProducedResource.ProducedResource.ProductionDependencies )
			{
				int resAmount = WH_Manager.CheckResourceAvailability( resType.DependsOnResource );

				FactoryHasEnoughResources = resAmount >= resType.QuanityNeeded;

				if ( !FactoryHasEnoughResources ) break;
			}
		}
	}

	void CheckWarehouseCapacityForProducedResource()
	{
		FactoryHasEnoughCapacity = WH_Manager.CheckWarehouseCapacityForProducedResource( WarehouseForProducedResource.WarehoustType );
	}
}

[System.Serializable]
public struct FactoryWarehouseInformation
{
	public ResourceTypeScriptableObject ProducedResource;
	public WarehouseType WarehoustType;
	public int Capacity;
	public int InStock;

	public FactoryWarehouseInformation( ResourceTypeScriptableObject _prdRes, WarehouseType _wt, int _cpty, int _stck)
	{
		ProducedResource = _prdRes;
		WarehoustType = _wt;
		Capacity = _cpty;
		InStock = _stck;
	}
}
