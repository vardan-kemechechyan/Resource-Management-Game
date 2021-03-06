using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PLAYING,
    PAUSE,
    GAMEOVER
}

public enum PlayInputRestriction
{
    CANT_PLAY,
    NO_RESTRICTIONS
}

public enum ResourceTypeNames
{
    TypeA,
    TypeB,
    TypeC,
    NONE
}

public enum WarehouseType
{
    PRODUCED,
    CONSUMED,
    NONE
}

public class GlobalConfig : Singleton<GlobalConfig>
{
    public float PlayerSpeed = 10f;
    public float ResourceAnimationSpeed = 100f;

    public override void Awake() { base.Awake(); Application.targetFrameRate = 60; }
}
