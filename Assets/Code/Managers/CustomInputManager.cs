using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputManager : Singleton<CustomInputManager>, ISubscribe
{
	public static event Action<object> OnInputTriggered = delegate { };
	public static event Action<bool> FingerOrMouseIsUp = delegate { };

	[SerializeField] Joystick_Types _jsType;

	[SerializeField] GameObject js_Dynamic;
	[SerializeField] GameObject js_Fixed;
	[SerializeField] GameObject js_Floating;
	[SerializeField] GameObject js_Variable;

	GameState _GameState = GameState.PLAYING;

	private void Start()
	{
		js_Dynamic.SetActive( _jsType == Joystick_Types.Dynamic );
		js_Fixed.SetActive( _jsType == Joystick_Types.Fixed );
		js_Floating.SetActive( _jsType == Joystick_Types.Floating );
		js_Variable.SetActive( _jsType == Joystick_Types.Variable );

		SubscribeToEvents();
	}

	private void OnDisable() { UnsubscribeFromEvents(); }
	private void OnDestroy() { UnsubscribeFromEvents(); }

	void ChangeGameState( GameState _value ) { _GameState = _value; }

	public void SubscribeToEvents()
	{
		CustomEventManager.GameStateChanged += ChangeGameState;
	}

	public void UnsubscribeFromEvents()
	{
		CustomEventManager.GameStateChanged -= ChangeGameState;
	}

	public void FingerOrMouseButtonIsUp( bool _fingerUp ) { FingerOrMouseIsUp( _fingerUp ); }

	public void SendInputMessage( JoystickSentData _inputMessage )
	{
		if ( _GameState == GameState.PLAYING ) 
			OnInputTriggered( _inputMessage );
	}
}

public enum Joystick_Types
{
	Dynamic,
	Fixed,
	Floating,
	Variable,
	NONE
}

public struct JoystickSentData
{
	public Vector2 Direction;
	public float Magnitude;
}
