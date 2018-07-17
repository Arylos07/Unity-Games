using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDisable : MonoBehaviour {

    public Terrain terrain;

	// Use this for initialization
	void Start () {
        terrain.treeDistance = 120;
        terrain.treeBillboardDistance = 10;
        terrain.treeCrossFadeLength = 7;
        terrain.Flush();
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
