using UnityEngine;

[ExecuteInEditMode]
public class Demo_Debris : MonoBehaviour
{
    #region Private Methods

    private void OnEnable()
    {
        CharacterController Controller = FindObjectOfType<CharacterController>();

        foreach (Rigidbody Rigidbody in GetComponentsInChildren<Rigidbody>())
            Rigidbody.maxDepenetrationVelocity = 1f;

        if (Controller != null)
            foreach (Collider Collider in GetComponentsInChildren<Collider>())
                Physics.IgnoreCollision(Collider, Controller);
    }

    #endregion
}
