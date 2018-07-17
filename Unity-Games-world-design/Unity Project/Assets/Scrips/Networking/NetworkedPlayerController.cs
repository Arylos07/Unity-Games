using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class NetworkedPlayerController : Photon.MonoBehaviour {

    public float Speed = 10;
    public float RotSpeed = 2;
    public GameObject Camera;
    private CharacterController c_Controller;
    float vertVel = 0;
	// Use this for initialization
	void Start () {
        c_Controller = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            float mX = Input.GetAxis("Mouse X");
            float mY = Input.GetAxis("Mouse Y");
            Vector3 dir = new Vector3(x * Speed, vertVel, z * Speed);
            dir = gameObject.transform.TransformDirection(dir);
            c_Controller.Move(dir * Time.deltaTime);
            gameObject.transform.Rotate(new Vector3(0, mX, 0) * RotSpeed);
            Camera.transform.Rotate(new Vector3(-mY, 0, 0) * RotSpeed);

            if(!c_Controller.isGrounded)
            {
                vertVel = -9;
            }
        }
	}
}
