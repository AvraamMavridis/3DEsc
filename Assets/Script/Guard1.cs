using UnityEngine;
using System.Collections;
using Assets.Script;


public class Guard1 : MonoBehaviour{

    GameObject player;
    GameObject guard;


    public static States state;
    private float starttime;

 
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("carl");
        guard = GameObject.Find("guard");
        state = States.MovingRight; 
        
    }


	
	// Update is called once per frame
	void Update () {


        if (rigidbody.transform.position.x > -15 && state == States.MovingRight)
        {
            //rigidbody.AddForce(rigidbody.transform.TransformDirection((new Vector3(0, 0, 1)) * 5));
            rigidbody.transform.position = new Vector3(rigidbody.transform.position.x - 0.05f, rigidbody.transform.position.y, rigidbody.transform.position.z);       
        }
        else if ((int)rigidbody.transform.position.x == -15 && state == States.MovingRight)
        {
            rigidbody.transform.Rotate(new Vector3(0, 180, 0));
            state = States.MovingLeft;
            print("Edo");
        }
        else if (rigidbody.transform.position.x < 0 && state == States.MovingLeft)
        {
           rigidbody.transform.position = new Vector3(rigidbody.transform.position.x + 0.05f, rigidbody.transform.position.y, rigidbody.transform.position.z);
        }
        else if ((int)rigidbody.transform.position.x == 0 && state == States.MovingLeft)
        {
            rigidbody.transform.Rotate(new Vector3(0, 180, 0));
            state = States.MovingRight;
        }
      
	}

    public void setState(States s)
    {
        state = s;
    }
}
