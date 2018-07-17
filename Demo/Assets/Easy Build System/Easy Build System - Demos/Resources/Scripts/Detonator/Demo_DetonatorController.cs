using UnityEngine;
using UnityEngine.UI;

public class Demo_DetonatorController : MonoBehaviour
{
    #region Public Enums

    public enum DetonationMode
    {
        None,
        Delay,
        Trigger,
        TriggerAndSticky
    }

    #endregion

    #region Public Fields

    [Header("Detonator Settings")]

    public GameObject Projectile;

    public Text ThrowMode;
    public Image ThrowFill;

    public float MaxThrowingTime;
    public KeyCode ThrowKey = KeyCode.L;

    #endregion

    #region Private Fields

    private DetonationMode CurrentMode;

    private bool Throwing;

    private float ThrowingForce;

    private float Timer;

    #endregion

    #region Private Methods

    private void Awake()
    {
        CurrentMode = DetonationMode.Delay;
    }

    private void Update()
    {
        if (ThrowMode == null || ThrowFill == null)
            return;

        ThrowMode.text = "Detonation Mode : " + CurrentMode.ToString();

        ThrowFill.fillAmount = ThrowingForce / MaxThrowingTime;

        if (Input.GetKeyUp(KeyCode.Y))
        {
            if (CurrentMode == DetonationMode.Delay)
                CurrentMode = DetonationMode.TriggerAndSticky;
            else if (CurrentMode == DetonationMode.Trigger)
                CurrentMode = DetonationMode.Delay;
            else if (CurrentMode == DetonationMode.None)
                CurrentMode = DetonationMode.Trigger;
            else if (CurrentMode == DetonationMode.TriggerAndSticky)
                CurrentMode = DetonationMode.None;
        }

        if (Input.GetKey(ThrowKey))
        {
            if (ThrowingForce < MaxThrowingTime)
                ThrowingForce += Time.deltaTime;

            Throwing = true;
        }

        if (Input.GetKeyUp(ThrowKey))
        {
            if (Throwing)
            {
                GameObject Temp = Instantiate(Projectile, transform.position, Quaternion.identity);

                if (transform.GetComponentInParent<Collider>() != null)
                    Physics.IgnoreCollision(Temp.GetComponent<Collider>(), transform.GetComponentInParent<Collider>());

                Temp.GetComponent<Rigidbody>().AddForce(transform.forward * 500 * ThrowingForce);

                Temp.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.one * Random.Range(0, 360) * 500 * ThrowingForce);

                Temp.GetComponent<Demo_DetonatorProjectile>().Detonation = CurrentMode;

                ThrowingForce = 0;
            }

            Throwing = false;
        }
    }

    #endregion
}
