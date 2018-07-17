using EasyBuildSystem.Scripts.Buildings.Internal.Builder;
using UnityEngine;
using UnityEngine.UI;

public class Demo_UI_Stability : MonoBehaviour
{
    #region Public Fields

    public GameObject StabilityContent;

    public Text StabilityText;

    #endregion

    #region Private Fields

    private BaseBuilder Builder;

    #endregion

    #region Private Methods

    private void Awake()
    {
        Builder = FindObjectOfType<BaseBuilder>();
    }

    private void Update ()
    {
		if (Builder != null)
        {
            if (Builder.CurrentMode == EasyBuildSystem.Scripts.Buildings.Enums.Builder.BuildMode.Placement)
            {
                if (Builder.CurrentPreview != null && Builder.CurrentPreview.UseConditionalPhysics)
                {
                    StabilityContent.SetActive(true);

                    StabilityText.text = Builder.CurrentPreview.CheckStability() ? "Stable" : "Unstable";
                }
                else
                {
                    StabilityContent.SetActive(false);
                }
            }
            else
                StabilityContent.SetActive(false);
        }
	}

    #endregion
}
