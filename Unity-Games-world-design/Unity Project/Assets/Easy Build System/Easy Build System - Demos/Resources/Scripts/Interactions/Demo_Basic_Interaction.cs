using EasyBuildSystem.Scripts.Buildings.Internal.Part;
using UnityEngine;

public class Demo_Basic_Interaction : MonoBehaviour
{
    #region Public Events

    public delegate void EventHandler(BasePart part);

    public static event EventHandler OnInteracted;

    public static void Interacted(BasePart part)
    {
        if (OnInteracted != null)
        {
            OnInteracted.Invoke(part);
        }
    }

    #endregion

    #region Public Fields

    [Header("Interaction Settings")]
    public float InteractionDistance = 3.0f;

    public KeyCode InteractionKey = KeyCode.E;
    public GUIStyle Font;
    public LayerMask Layers;

    #endregion

    #region Private Fields

    private bool IsOver;

    #endregion

    #region Private Methods

    private void OnGUI()
    {
        if (IsOver)
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 40, 300, 40), "Press <b>" + InteractionKey + "</b> to interacted.", Font);
    }

    private void Update()
    {
        Ray Ray = new Ray(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.1f)), transform.forward);
        RaycastHit Hit;

        if (Physics.Raycast(Ray, out Hit, InteractionDistance, Layers))
        {
            if (Hit.collider.GetComponent<Demo_Door_Interaction>())
            {
                IsOver = true;

                if (Input.GetKeyUp(InteractionKey))
                {
                    Interacted(Hit.collider.GetComponentInParent<BasePart>());

                    Hit.collider.GetComponent<Demo_Door_Interaction>().Interaction();
                }
            }
            else
                IsOver = false;
        }
        else
            IsOver = false;
    }

    #endregion
}