using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Camera.mainCamera.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
	}
}
