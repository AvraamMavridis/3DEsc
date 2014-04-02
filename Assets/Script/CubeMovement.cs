using UnityEngine;
using System.Collections;

public class CubeMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        rigidbody.transform.position = new Vector3(rigidbody.transform.position.x + Input.GetAxis("Horizontal"), rigidbody.transform.position.y, rigidbody.transform.position.z + Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.Space))
        {
            print("rotate");
            rigidbody.AddTorque(new Vector3(0, 1000000, 0));
        }
     
	}
}
