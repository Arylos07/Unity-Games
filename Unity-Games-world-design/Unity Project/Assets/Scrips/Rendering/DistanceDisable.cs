using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDisable : MonoBehaviour {

    public Terrain terrain;

	// Use this for initialization
	void Start () {
        terrain.treeDistance = 155;
        terrain.treeBillboardDistance = 30;
        terrain.treeCrossFadeLength = 5;
        terrain.Flush();
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
