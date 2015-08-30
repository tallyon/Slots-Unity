using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoplayButton : MonoBehaviour
{
	Text text;
	bool pressed;

	void Start()
	{
		text = GetComponentInChildren<Text> ();
	}

	//	Funkcja wywołana podczas wciśnięcia przycisku Autoplay - zmienia napis w jego potomku
	public void ButtonPress()
	{
		pressed = !pressed;

		if (pressed)
			text.text = "Autoplay: On";
		else
			text.text = "Autoplay: Off";
	}
}
