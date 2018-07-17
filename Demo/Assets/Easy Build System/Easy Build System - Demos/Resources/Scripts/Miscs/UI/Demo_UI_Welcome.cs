using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Demo_UI_Welcome : MonoBehaviour
{
    #region Public Fields

    [Header("UI Welcome Settings")]
    public GameObject[] GameObjectAtDisabled;

    public Demo_FirstPersonController First;
    public Demo_FreeLookCam Third;

    public GameObject Content;

    public Button CloseButton;

    #endregion

    #region Private Methods

    private void OnEnable()
    {
        foreach (var Blur in FindObjectsOfType<CustomBlurOptimized>())
            Blur.enabled = true;

        for (int i = 0; i < GameObjectAtDisabled.Length; i++)
            if (GameObjectAtDisabled[i] != null)
                GameObjectAtDisabled[i].SetActive(false);
    }

    private void OnDisable()
    {
        if (First != null)
            First.enabled = false;

        if (Third != null)
            Third.enabled = false;

        Active();
    }

    private void Start()
    {
        if (First != null)
            First.enabled = false;

        if (Third != null)
            Third.enabled = false;

        CloseButton.onClick.AddListener(() => { Active(); });
    }

    private void Active()
    {
        if (First != null)
            First.enabled = true;

        if (Third != null)
            Third.enabled = true;

        Content.SetActive(false);

        foreach (var Blur in FindObjectsOfType<CustomBlurOptimized>())
            Blur.enabled = false;

        for (int i = 0; i < GameObjectAtDisabled.Length; i++)
            if (GameObjectAtDisabled[i] != null)
                GameObjectAtDisabled[i].SetActive(true);
    }

    #endregion
}