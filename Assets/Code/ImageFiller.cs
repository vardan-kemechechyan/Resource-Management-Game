using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFiller : MonoBehaviour
{
    Image ProgressBarImage;

	private void Awake()
	{
		ProgressBarImage = GetComponent<Image>();

		ProgressBarImage.fillAmount = 0f;
	}

	public void ChangeProgressBarValue(float _value)
	{
		ProgressBarImage.fillAmount = _value;
	}
}
