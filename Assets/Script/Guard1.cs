using UnityEngine;
using System.Collections;

public class Guard1 : MonoBehaviour {

    GameObject player;
    GameObject guard;

    enum State {Right=0,Left, MovingRight, MovingLeft};
    State state;
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("carl");
        guard = GameObject.Find("guard");
        state = State.MovingRight;
    }


	
	// Update is called once per frame
	void Update () {
        if (rigidbody.transform.position.x > -15 && state==State.MovingRight)
        {
            rigidbody.AddForce(rigidbody.transform.TransformDirection((new Vector3(0, 0, 1)) * 5));
        }
        else if (rigidbody.transform.position.x < -15 && state==State.MovingRight)
        {
            rigidbody.transform.Rotate(new Vector3(0, 180, 0));
            state = State.MovingLeft;
        }
        else if (rigidbody.transform.position.x > -17.7 && state == State.MovingLeft && rigidbody.transform.position.x < 0)
        {
            rigidbody.AddForce(rigidbody.transform.TransformDirection((new Vector3(0, 0, 1)) * 5));
        }
        else if (rigidbody.transform.position.x > 0 && state == State.MovingLeft) {
            rigidbody.transform.Rotate(new Vector3(0, 180, 0));
            state = State.MovingRight;
        }


        

	}
}
