using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Demo_UI_SlowMotion : MonoBehaviour
{
    #region Private Methods

    private float MotionValue;
    private const string DisplayFormat = "Slow Motion {0}";
    private Text Text;

    private float StartTimeScale;

    #endregion

    #region Private Methods

    private void Awake()
    {
        Text = GetComponent<Text>();
    }

    private void Start()
    {
        StartTimeScale = Time.timeScale;

        MotionValue = 1;
    }

    private void Update ()
    {
        if (MotionValue == 1)
        {
            Time.timeScale = StartTimeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else if (MotionValue == 0.5f)
        {
            Time.timeScale = StartTimeScale / 2;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        Text.text = string.Format(DisplayFormat, MotionValue == 1 ? "1x" : "2x");

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (MotionValue == 1)
                MotionValue = 0.5f;
            else
                MotionValue = 1;
        }
    }

    #endregion
}
