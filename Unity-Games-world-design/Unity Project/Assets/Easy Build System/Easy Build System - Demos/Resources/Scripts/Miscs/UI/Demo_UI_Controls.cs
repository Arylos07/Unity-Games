using UnityEngine;
using UnityEngine.UI;

public class Demo_UI_Controls : MonoBehaviour
{
    #region Public Fields

    [Header("UI Controls Settings")]
    public Text ControlsText;
    [TextArea(1, 30)]
    public string TextAtDisplayWithArgs = "";

    #endregion Public Fields

    #region Private Fields

    private BaseBuilderExample Builder;

    #endregion

    #region Private Methods

    private void Awake()
    {
        Builder = FindObjectOfType<BaseBuilderExample>();
    }

    private void Update()
    {
        if (Builder == null)
            return;

        if (Builder.Inputs == null)
            return;

        ControlsText.text = string.Format(TextAtDisplayWithArgs,
            Builder.SelectedPrefab != null ? Builder.SelectedPrefab.name : "Empty",
            Builder.CurrentMode.ToString(),
            Builder.Inputs.InputPlacementKey,
            Builder.Inputs.InputDestructionKey,
            Builder.Inputs.InputEditionKey,
            Builder.Inputs.InputActionKey,
            Builder.Inputs.InputCancelKey);
    }

    #endregion Private Methods
}
