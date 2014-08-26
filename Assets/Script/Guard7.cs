using UnityEngine;
using System.Collections;
using Assets.Script;

public class Guard7 : MonoBehaviour
{

    GameObject player;
    GameObject guard;


    public static States state;
    private float starttime;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("carl");
        guard = GameObject.Find("guard2");
        state = States.MovingUp;

    }



    // Update is called once per frame
    void Update()
    {


        if (rigidbody.transform.position.z < -13 && state == States.MovingUp)
        {
            //rigidbody.AddForce(rigidbody.transform.TransformDirection((new Vector3(0, 0, 1)) * 5));
            rigidbody.transform.position = new Vector3(rigidbody.transform.position.x, rigidbody.transform.position.y, rigidbody.transform.position.z + 0.05f);
        }
        else if ((int)rigidbody.transform.position.z == -12 && state == States.MovingUp)
        {
            rigidbody.transform.Rotate(new Vector3(0, 180, 0));
            state = States.MovingDown;
        }
        else if (rigidbody.transform.position.z > -33 && state == States.MovingDown)
        {
            rigidbody.transform.position = new Vector3(rigidbody.transform.position.x, rigidbody.transform.position.y, rigidbody.transform.position.z - 0.05f);
        }
        else if ((int)rigidbody.transform.position.z == -33 && state == States.MovingDown)
        {
            rigidbody.transform.Rotate(new Vector3(0, 180, 0));
            state = States.MovingUp;
        }

    }

    public void setState(States s)
    {
        state = s;
    }

}
