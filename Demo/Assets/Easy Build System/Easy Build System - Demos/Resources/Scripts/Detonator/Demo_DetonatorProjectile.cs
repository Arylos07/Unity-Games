using EasyBuildSystem.Scripts.Buildings.Internal.Part;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Demo_DetonatorProjectile : MonoBehaviour
{
    #region Public Fields

    [Header("Simple Detonator Settings")]

    public GameObject Sparks;

    public GameObject Explosion;

    public float ExplosionLifeTime = 5f;

    public float DetonationTime = 5f;

    public float ExplosionRadius = 5f;

    public float ExplosionForce = 500f;

    [HideInInspector]
    public Demo_DetonatorController.DetonationMode Detonation;

    #endregion

    #region Private Fields

    private bool Detonating;

    #endregion

    #region Private Methods

    private void Start()
    {
        if (Detonation == Demo_DetonatorController.DetonationMode.Delay)
            StartCoroutine(Detonate(DetonationTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Detonation != Demo_DetonatorController.DetonationMode.TriggerAndSticky)
            return;

        GetComponent<Rigidbody>().isKinematic = true;

        transform.parent = collision.transform;
    }

    private void Update()
    {
        if (Detonation == Demo_DetonatorController.DetonationMode.Trigger || Detonation == Demo_DetonatorController.DetonationMode.TriggerAndSticky)
        {
            Sparks.gameObject.SetActive(Detonating);

            if (Input.GetKey(KeyCode.U))
            {
                Detonating = true;
                StartCoroutine(Detonate(DetonationTime));
            }
        }
        else if (Detonation == Demo_DetonatorController.DetonationMode.Delay)
            Sparks.gameObject.SetActive((Detonation == Demo_DetonatorController.DetonationMode.Delay));
    }

    private IEnumerator Detonate(float time)
    {
        yield return new WaitForSeconds(time);

        Collider[] Colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach (var Collider in Colliders)
        {
            if (Collider.GetComponentInParent<BasePart>())
            {
                Destroy(Collider.GetComponentInParent<BasePart>().gameObject);
            }
        }

        yield return new WaitForSeconds(0.05f);

        Colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach (var Collider in Colliders)
        {
            if (Collider.GetComponent<Rigidbody>() != null)
            {
                Collider.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 3, ForceMode.Force);
            }
        }

        GameObject Temp = Instantiate(Explosion, transform.position, transform.rotation);

        Destroy(Temp, ExplosionLifeTime);

        Destroy(gameObject);
    }

    #endregion
}
