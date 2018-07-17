using EasyBuildSystem.Scripts.Addons;
using EasyBuildSystem.Scripts.Addons.Enums;
using EasyBuildSystem.Scripts.Buildings.Enums.Part;
using EasyBuildSystem.Scripts.Buildings.Internal.Part;
using EasyBuildSystem.Scripts.Buildings.Internal.Utils;
using EasyBuildSystem.Scripts.Constants;
using UnityEngine;

[AddOn(ADDON_NAME, ADDON_AUTHOR, ADDON_DESCRIPTION, AddOnTarget.Part)]
public class AddonDestructibleAppearance : AddOnBehaviour
{
    #region AddOn Fields

    public const string ADDON_NAME = "Add-On Destructible Appearance";
    public const string ADDON_AUTHOR = "Ads Cryptoz 22";
    public const string ADDON_DESCRIPTION = "Allows to instantiate after the destruction, some gameObjects(s) according to the current appearance of the part.";

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

    private bool IsDestruct;

    #endregion AddOn Fields

    #region Public Class

    [System.Serializable]
    public class DestructibleAppearance
    {
        public string Name;
        public int AppearanceIndex = 0;
        public GameObject FracturedAppearance;
        public float FracturedLifeTime;
    }

    #endregion Public Class

    #region Public Fields

    [Tooltip("GameObject at spawn when the placement is performed.")]
    public DestructibleAppearance[] Destructibles;

    #endregion Public Fields

    #region Private Fields

    private bool Quitting;

    private BasePart Part;

    #endregion Private Fields

    #region Private Methods

    private void Awake()
    {
        // We get the part on which this add-on is attached.
        Part = GetComponent<BasePart>();
    }

    private void OnDestroy()
    {
        if (Quitting)
            return;

        //If the part is not placed then we return.
        if (Part.CurrentState != StateType.Placed)
            return;

        // If already destruct we return.
        if (IsDestruct)
            return;

        for (int i = 0; i < Destructibles.Length; i++)
        {
            //If the part appearance index is the same that in the reference list then.
            if (Part.AppearanceIndex == Destructibles[i].AppearanceIndex)
            {
                //We remove the parent.
                Destructibles[i].FracturedAppearance.transform.parent = null;

                //We active object.
                Destructibles[i].FracturedAppearance.SetActive(true);

                //We set recursively "Ignore Raycast" layer.
                EasyBuildSystem.Scripts.Buildings.Internal.Utils.Helper.SetLayerRecursively(Destructibles[i].FracturedAppearance, Physics.IgnoreRaycastLayer);

                //Destroy after the life time.
                Destroy(Destructibles[i].FracturedAppearance, Destructibles[i].FracturedLifeTime);

                //Is destructed.
                IsDestruct = true;

                break;
            }
        }
    }

    private void OnApplicationQuit()
    {
        Quitting = true;
    }

    #endregion Private Methods
}