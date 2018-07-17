using System.Collections;
using UnityEngine;

public class Demo_Door_Interaction : MonoBehaviour
{
    #region Public Fields

    [HideInInspector]
    public bool DoorStatus;

    public float DoorSpeed;
    public float DoorOpenAngle = 90.0f;
    public float DoorCloseAngle = 0.0f;

    public AudioSource Source;
    public AudioClip DoorSound;

    [HideInInspector]
    public Quaternion DoorOpen = Quaternion.identity;
    [HideInInspector]
    public Quaternion DoorClose = Quaternion.identity;

    #endregion

    #region Private Fields

    private bool CanUse = true;

    #endregion

    #region Private Methods

    private void Start()
    {
        DoorOpen = Quaternion.Euler(0, DoorOpenAngle, 0);
        DoorClose = Quaternion.Euler(0, DoorCloseAngle, 0);
    }

    #endregion

    #region Public Methods

    public void Interaction()
    {
        if (!CanUse)
        {
            return;
        }

        if (DoorStatus)
        {
            StartCoroutine(MoveDoor(DoorClose));
        }
        else
        {
            StartCoroutine(MoveDoor(DoorOpen));
        }
    }

    public IEnumerator MoveDoor(Quaternion dest)
    {
        CanUse = false;

        GetComponentInChildren<BoxCollider>().isTrigger = true;

        if (Source != null)
        {
            if (DoorSound != null)
            {
                Source.PlayOneShot(DoorSound);
            }
        }

        while (Quaternion.Angle(transform.localRotation, dest) > 1.0f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, dest, Time.deltaTime * DoorSpeed);
            yield return null;
        }

        GetComponentInChildren<BoxCollider>().isTrigger = false;

        DoorStatus = !DoorStatus;

        CanUse = true;

        yield return null;
    }

    #endregion
}