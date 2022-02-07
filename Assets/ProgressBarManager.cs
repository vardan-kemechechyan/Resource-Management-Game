using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressBarManager : MonoBehaviour
{
	[SerializeField] ImageFiller FactoryProgressBar;

	[SerializeField] string[] WarningTexts;					// 0 - Lack of Resources, 1 - Storage Full
	[SerializeField] TextMeshProUGUI[] StaticWarningTexts;	// 0 - Lack of Resources, 1 - Storage Full

	PlayAnimation WarningAnimation;

	Queue<Action> WarningShowQueue = new Queue<Action>();

	float AnimationInterval = 1.25f;

	private void Start()
	{
		transform.parent.GetComponent<FactoryManager>().ProducingOneResourceUnit += ExectureProgressBar;
		transform.parent.GetComponent<FactoryManager>().ProductionStopped += ExecuteFailureNotification;
		transform.parent.GetComponent<FactoryManager>().PlayStackedWarnings += PlayAllStackedWarning;
	}

	private void OnDisable()
	{
		transform.parent.GetComponent<FactoryManager>().ProducingOneResourceUnit -= ExectureProgressBar;
		transform.parent.GetComponent<FactoryManager>().ProductionStopped -= ExecuteFailureNotification;
		transform.parent.GetComponent<FactoryManager>().PlayStackedWarnings -= PlayAllStackedWarning;
	}

	private void OnDestroy()
	{
		transform.parent.GetComponent<FactoryManager>().ProducingOneResourceUnit -= ExectureProgressBar;
		transform.parent.GetComponent<FactoryManager>().ProductionStopped -= ExecuteFailureNotification;
		transform.parent.GetComponent<FactoryManager>().PlayStackedWarnings -= PlayAllStackedWarning;
	}

	public void ExectureProgressBar( float _value = 0)
	{
		FactoryProgressBar.ChangeProgressBarValue( _value );
	}

	void ExecuteFailureNotification( int errType )
	{
		WarningShowQueue.Enqueue(delegate() {
			if ( WarningAnimation == null ) WarningAnimation = GetComponentInChildren<PlayAnimation>();

			WarningAnimation?.ExecuteWarningAnimation( WarningTexts[ errType ] );

			StaticWarningTexts[ errType ].gameObject.SetActive(true);
		} );
	}

	void PlayAllStackedWarning()
	{
		StartCoroutine( AnimationEnumerator() );
	}

	IEnumerator AnimationEnumerator()
	{
		while ( WarningShowQueue.Count != 0 )
		{
			WarningShowQueue.Dequeue()();

			yield return new WaitForSeconds( AnimationInterval );
		}
	}
}
