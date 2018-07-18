using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
public class HideCursor : Photon.MonoBehaviour {
    //private bool isShowingCursor;

    // Use this for initialization
    void Start()
    {

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        if (photonView.isMine)
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if the current photon view is the local players.
        if (photonView.isMine)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = !Cursor.visible;
                if (!Cursor.visible)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }

    }
}