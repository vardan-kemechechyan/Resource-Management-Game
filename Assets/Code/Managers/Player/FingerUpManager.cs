using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerUpManager : MonoBehaviour, ISubscribe
{
	IFingerUpManager _fingerUpManager;
	
	private void Awake()	{ SubscribeToEvents(); }
	private void Start()	{ _fingerUpManager = GetComponent<IFingerUpManager>(); }
	private void OnDisable() { UnsubscribeFromEvents(); }
	private void OnDestroy() { UnsubscribeFromEvents(); }

	public void SubscribeToEvents()
	{
		CustomInputManager.FingerOrMouseIsUp += FireFingerUpEvent;
	}

	public void UnsubscribeFromEvents()
	{
		CustomInputManager.FingerOrMouseIsUp -= FireFingerUpEvent;
	}

	void FireFingerUpEvent(bool _fingerUp)
	{
		print( $"Execute FingerUp Event - { _fingerUp }" );

		_fingerUpManager?.ExecuteFingerUpEvent( _fingerUp );
	}
}
