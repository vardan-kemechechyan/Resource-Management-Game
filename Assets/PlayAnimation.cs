using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
	[SerializeField] Animator AttachedAnimator;
	[SerializeField] string AnimatonName;
	[SerializeField] string TriggerName;
	[SerializeField] TextMeshProUGUI WarningText;

	public void ExecuteWarningAnimation( string _text)
	{
		WarningText.text = _text;

		AttachedAnimator.SetTrigger( TriggerName );
	}

	public void ExecuteAnimationEnded()
	{
		AttachedAnimator.ResetTrigger( TriggerName );

		WarningText.text = "";
	}
}
