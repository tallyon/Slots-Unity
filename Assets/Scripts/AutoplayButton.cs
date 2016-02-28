using UnityEngine;
using UnityEngine.UI;

public class AutoplayButton : MonoBehaviour
{
    private Text text;
    private bool pressed;

    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Toggle pressed flag and update text on button
    /// </summary>
    public void ButtonPress()
    {
        pressed = !pressed;

        if (pressed)
            text.text = "Autoplay: On";
        else
            text.text = "Autoplay: Off";
    }
}
