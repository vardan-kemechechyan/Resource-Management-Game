using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementJoystick : MonoBehaviour, ISubscribe, IMoveJoystickManager
{
	JoystickSentData inputData;

	GameState _GameState = GameState.PLAYING;

	IMovementManager _movementManager;

	Vector3 CurrentDirection = Vector3.zero;

	private void Awake()	 { SubscribeToEvents(); }
	private void Start()	 { _movementManager = GetComponent<IMovementManager>(); }
	private void OnDisable() { UnsubscribeFromEvents(); }
	private void OnDestroy() { UnsubscribeFromEvents(); }

	public void ExecuteMoveEvent( object _data )
	{
			inputData = ( JoystickSentData )_data;

			float x = inputData.Direction.x; 
			float z = inputData.Direction.y;

			Vector3 MoveDirection = new Vector3( x, 0f, z );

			if ( CurrentDirection != MoveDirection )
			{
				CurrentDirection = MoveDirection;
				_movementManager.ExecuteMoveEvent( CurrentDirection * inputData.Magnitude );
			}
	}

	void ChangeGameState( GameState _value ) { _GameState = _value; }

	public void SubscribeToEvents()
	{
		CustomEventManager.GameStateChanged	+= ChangeGameState;
		CustomInputManager.OnInputTriggered += ExecuteMoveEvent;
	}
	public void UnsubscribeFromEvents()
	{
		CustomEventManager.GameStateChanged	-= ChangeGameState;
		CustomInputManager.OnInputTriggered -= ExecuteMoveEvent;
	}
}
