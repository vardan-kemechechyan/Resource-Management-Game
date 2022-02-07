using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarManager : MonoBehaviour
{
	[SerializeField] ImageFiller FactoryProgressBar;

	private void Start()
	{
		transform.parent.GetComponent<FactoryManager>().ProducingOneResourceUnit += ExectureProgressBar;
	}

	private void OnDisable()
	{
		transform.parent.GetComponent<FactoryManager>().ProducingOneResourceUnit -= ExectureProgressBar;
	}

	private void OnDestroy()
	{
		transform.parent.GetComponent<FactoryManager>().ProducingOneResourceUnit -= ExectureProgressBar;
	}

	public void ExectureProgressBar( float _value = 0)
	{
		FactoryProgressBar.ChangeProgressBarValue( _value );
	}
}
