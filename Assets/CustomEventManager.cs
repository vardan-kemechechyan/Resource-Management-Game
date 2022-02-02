using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEventManager : Singleton<CustomEventManager>
{
	public static Action StartTheGameButtonTriggered = delegate () { };

	public static Action<GameState> GameStateChanged = delegate ( GameState _value ) { };
}
