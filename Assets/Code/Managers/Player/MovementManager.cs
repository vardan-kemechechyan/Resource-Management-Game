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

	[SerializeField] Rigidbody rb;

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
			Vector3 NewPos = rb.position + MoveDirection * Speed * Time.fixedDeltaTime;

			Vector3 direction = NewPos - rb.position;
			Ray ray = new Ray( rb.position, direction );
			RaycastHit hit;
			
			if ( !Physics.Raycast( ray, out hit, direction.magnitude ) || hit.collider.isTrigger )
				rb.MovePosition( NewPos );
			else
				rb.MovePosition( hit.point );

			VisualContainer.rotation = Quaternion.LookRotation( MoveDirection );
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
