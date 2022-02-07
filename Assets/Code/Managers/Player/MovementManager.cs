using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour, IMovementManager, ISubscribe, IFingerUpManager
{
	float Speed;

	GameState _GameState = GameState.PLAYING;

	Vector3 MoveDirection = Vector3.zero;

	[SerializeField] bool StopWhenFingerUp;

	[SerializeField] Transform VisualContainer;

	public bool FingerUp { get; set; }

	private void Start()
	{
		Speed = GlobalConfig.Instance.PlayerSpeed;
	}

	private void FixedUpdate()
	{
		if ( StopWhenFingerUp )
			if ( FingerUp )
				return;

		if ( _GameState == GameState.PLAYING )
		{
			VisualContainer.rotation = Quaternion.LookRotation( MoveDirection );
			transform.Translate( MoveDirection * Speed * Time.fixedDeltaTime );
		}
	}

	public void ExecuteMoveEvent( Vector3 EndPosition ) { MoveDirection = EndPosition; }

	private void Awake()	 { SubscribeToEvents(); }
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

	public void ExecuteFingerUpEvent( bool _fingerUp ) { FingerUp = _fingerUp; }
}
