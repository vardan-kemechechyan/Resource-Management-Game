using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableResource : MonoBehaviour
{
	[SerializeField] ResourceTypeScriptableObject ResourceDescription;

    //bool ResourceUsable = true;

	public ResourceTypeNames GetResourceType() { return ResourceDescription.r_Type; }

    Vector3 startMarker;
    Vector3 endMarker;

    float speed;

    private float startTime;

    private float journeyLength;

	private void Start()
	{
        speed = GlobalConfig.Instance.ResourceAnimationSpeed;
    }

	public void Animate( Vector3 _endpoint, Action callBack )
    {
        //if ( !ResourceUsable ) return;

        startTime = Time.time;

        startMarker = transform.localPosition;
        endMarker = _endpoint;

        journeyLength = Vector3.Distance( startMarker, endMarker );

        //ResourceUsable = false;

        StartCoroutine( StartBlockAnimation( callBack ) );
    }

    IEnumerator StartBlockAnimation( Action callBack = null )
    {
        while ( Vector3.Distance( transform.localPosition, endMarker ) > Mathf.Epsilon )
        {
            float distCovered = ( Time.time - startTime ) * speed;

            float fractionOfJourney = distCovered / journeyLength;

            transform.localPosition = Vector3.Lerp( startMarker, endMarker, fractionOfJourney );

            yield return new WaitForSeconds( Time.deltaTime );
        }

        transform.localPosition = endMarker;

        //ResourceUsable = true;

        if( callBack != null ) callBack();
    }
}
