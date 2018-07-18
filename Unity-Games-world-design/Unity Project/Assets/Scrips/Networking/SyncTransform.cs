using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public class SyncTransform : Photon.MonoBehaviour {

    public Vector3 ObjPosition;
    public Quaternion ObjRotation;
    public Vector3 ObjScale;
    public float LerpSpeed = 3f;

    void Update () {
        //Check to see if this photon view is not mine
        if(!photonView.isMine)
        {
            //If it is another players Update there transform
            UpdateTransform();
        }	
	}

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(gameObject.transform.position);
            stream.SendNext(gameObject.transform.rotation);
            stream.SendNext(gameObject.transform.localScale);
        }
        else if (stream.isReading)
        {
            ObjPosition = (Vector3)stream.ReceiveNext();
            ObjRotation = (Quaternion)stream.ReceiveNext();
            ObjScale = (Vector3)stream.ReceiveNext();
        }
    }
    private void UpdateTransform()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, ObjPosition, LerpSpeed * Time.deltaTime);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, ObjRotation, LerpSpeed * Time.deltaTime);
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, ObjScale, LerpSpeed * Time.deltaTime);
    }
}
