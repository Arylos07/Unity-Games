using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public class MPScriptsManager : Photon.MonoBehaviour {

    public UnityEngine.MonoBehaviour[] LocalBehaviours;
    public GameObject[] LocalGameObjects;
	// Use this for initialization
	void Start () {
		if(!photonView.isMine)
        {
            foreach(UnityEngine.MonoBehaviour o in LocalBehaviours)
            {
                UnityEngine.MonoBehaviour mb = (UnityEngine.MonoBehaviour)o;
                mb.enabled = false;
            }
        }
        else
        {
            foreach (GameObject o in LocalGameObjects)
            {
                o.SetActive(true);
            }
        }
	}
	

}
