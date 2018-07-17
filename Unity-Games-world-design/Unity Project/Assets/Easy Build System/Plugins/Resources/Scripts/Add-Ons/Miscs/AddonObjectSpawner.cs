using EasyBuildSystem.Scripts.Addons;
using EasyBuildSystem.Scripts.Addons.Enums;
using EasyBuildSystem.Scripts.Buildings.Enums.Part;
using EasyBuildSystem.Scripts.Buildings.Events;
using EasyBuildSystem.Scripts.Buildings.Internal.Part;
using EasyBuildSystem.Scripts.Buildings.Internal.Socket;
using UnityEngine;

[AddOn(ADDON_NAME, ADDON_AUTHOR, ADDON_DESCRIPTION, AddOnTarget.Part)]
public class AddonObjectSpawner : AddOnBehaviour
{
    #region AddOn Fields

    public const string ADDON_NAME = "Add-On Object(s) Spawner";
    public const string ADDON_AUTHOR = "R.Andrew";

    public const string ADDON_DESCRIPTION = "Spawn gameObject(s) when the placement/destruction/edition is performed.";

    [HideInInspector]
    public string _Name = ADDON_NAME;

    public override string Name
    {
        get
        {
            return _Name;
        }

        protected set
        {
            _Name = value;
        }
    }

    [HideInInspector]
    public string _Author = ADDON_AUTHOR;

    public override string Author
    {
        get
        {
            return _Author;
        }

        protected set
        {
            _Author = value;
        }
    }

    [HideInInspector]
    public string _Description = ADDON_DESCRIPTION;

    public override string Description
    {
        get
        {
            return _Description;
        }

        protected set
        {
            _Description = value;
        }
    }

    #endregion AddOn Fields

    #region Public Fields

    [Tooltip("GameObject at spawn when the placement is performed.")]
    public GameObject PlacementObject;

    [Tooltip("GameObject at spawn when the destruction is performed.")]
    public GameObject DestructionObject;

    [Tooltip("GameObject at spawn when the edition is performed.")]
    public GameObject EditionObject;

    [Tooltip("GameObject life time after destruction.")]
    public float LifeTime = 5f;

    #endregion Public Fields

    #region Private Fields

    private bool Quitting;

    private BasePart Part;

    #endregion Private Fields

    #region Private Methods

    private void Awake()
    {
        Part = GetComponent<BasePart>();
    }

    private void OnEnable()
    {
        if (Part.CurrentState != StateType.Placed)
            return;

        Events.OnPlacedPart += OnPartPlaced;
        Events.OnEditedPart += OnPartEdited;
    }

    private void OnDisable()
    {
        Events.OnPlacedPart -= OnPartPlaced;
        Events.OnEditedPart -= OnPartEdited;
    }

    private void OnDestroy()
    {
        if (Quitting)
            return;

        if (Part == null)
            return;

        if (Part.CurrentState != StateType.Placed)
            return;

        if (DestructionObject != null)
        {
            GameObject Temp = Instantiate(DestructionObject, Part.transform.position, Part.transform.rotation);

            Destroy(Temp, LifeTime);
        }
    }

    private void OnApplicationQuit()
    {
        Quitting = true;
    }

    private void OnPartPlaced(BasePart part, BaseSocket socket)
    {
        if (part != Part)
            return;

        if (PlacementObject != null)
        {
            GameObject Temp = Instantiate(PlacementObject, part.transform.position, part.transform.rotation);

            Destroy(Temp, LifeTime);
        }
    }

    private void OnPartEdited(BasePart part, BaseSocket socket)
    {
        if (part != Part)
            return;

        if (EditionObject != null)
        {
            GameObject Temp = Instantiate(EditionObject, part.transform.position, part.transform.rotation);

            Destroy(Temp, LifeTime);
        }
    }

    #endregion Private Methods
}