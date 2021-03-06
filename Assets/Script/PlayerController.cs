﻿/*
 * PlayerController.cs - Handles the PlayerMovement
 * 			
 * 
 * 		Developer: Avraam Mavridis 12-Mar-2014
 * 
 */

using UnityEngine;
using System;
using System.Collections;
using HumanBones;
using Assets.Script;

public class PlayerController : MonoBehaviour
{

    #region Variables
    enum Direction { up_idle, left_idle, right_idle, left, right };
    private Direction dir;
    private float OldRightHandPosition = 0;
    private float NewRightHandPosition = 0;
    private float OldLeftHandPosition = 0;
    private float NewLeftHandPosition = 0;
    private float DifferenceBetweenOldAndNewLeftHandPosition = 0;
    private float DifferenceBetweenOldAndNewRightHandPosition = 0;
    private float DifferenceBetweenOldAndNewHipPosition = 0;
    private float DifferenceBetweenOldAndNewHipZetPosition = 0;
    private float OldHipPosition = 0;
    private float NewHipXPosition = 0;
    private float NewHipZetPosition = 0;
    private float OldHipZetPosition = 0;
    private AnimatorStateInfo AnimationState;
    public SkeletonWrapper sw;
    public Animator anim;
    GameObject player;
    Guard1 guard;
    Guard2 guard2;
    public static float speedFactor;
    public static float rotationFactor;
    public static float circleradious; //for CenterPointKinectWithoutHands
    public static bool slide = false;
    public static float maximumSpeed = 3;
    public enum MoveType { KeyboardMovement, KinectMovement, CenterPointKinectMovement, CenterPointKinectWithoutHands };
    public static MoveType movement;
    GameObject playercamera;
    //System.IO.StreamWriter file;
    #endregion


    // Use this for initialization

    void Start()
    {
        speedFactor = 150;
        rotationFactor = 134;
        movement = MoveType.KinectMovement;
        playercamera = GameObject.Find("carlCamera");
        player = GameObject.Find("carl");
        guard = GameObject.FindObjectOfType<Guard1>();
        guard2 = GameObject.FindObjectOfType<Guard2>();
        //using (file = new System.IO.StreamWriter(@"C:\result.txt"));
               
    }

    void Update()
    {
        if (movement == MoveType.KinectMovement)
        {
            KinectMovement();
        }
        else if (movement == MoveType.KeyboardMovement)
        {
            KeyboardMovement();
        }
        else if (movement == MoveType.CenterPointKinectMovement)
        {
            CenterPointKinectMovement();
        }
        else if (movement == MoveType.CenterPointKinectWithoutHands)
        {
            CenterPointKinectWithoutHands();
        }
 
    }

    //Move player using Keyboard
    void KeyboardMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetInteger("Animation", 1);
            print(speedFactor);
           
            rigidbody.AddForce(rigidbody.transform.TransformDirection((new Vector3(0,0,1))* Input.GetAxis("Vertical") * speedFactor));
            
        }
        else
        {
            anim.SetInteger("Animation", 0);
            rigidbody.velocity = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.RightArrow)))
        {
            rigidbody.AddTorque(new Vector3(0, rotationFactor * Input.GetAxis("Horizontal"), 0));
        }
        else
        {
            rigidbody.angularVelocity = Vector3.zero;
        }
    }


    //Move character by moving to the sides and use the hands for movement
    void KinectMovement() {

       

        if (sw.pollSkeleton())
        {
            NewRightHandPosition = sw.bonePos[0, (int)Bones.HandRight].y;
            NewLeftHandPosition = sw.bonePos[0, (int)Bones.HandLeft].y;
            NewHipXPosition = sw.bonePos[0, (int)Bones.HipCenter].x;
            NewHipZetPosition = sw.bonePos[0, (int)Bones.HipCenter].z;

            //Rotate the player based on hip position
            rigidbody.AddTorque(new Vector3(0, rotationFactor * (NewHipXPosition - OldHipPosition), 0));
            if (DifferenceBetweenOldAndNewHipPosition < 0.05)
            {
                rigidbody.angularVelocity = Vector3.zero;

            }

            changeLight();
            //Get the difference between old a new positions
            DifferenceBetweenOldAndNewRightHandPosition = Math.Abs(OldRightHandPosition - NewRightHandPosition);
            DifferenceBetweenOldAndNewLeftHandPosition = Math.Abs(OldLeftHandPosition - NewLeftHandPosition);
            DifferenceBetweenOldAndNewHipPosition = Math.Abs(OldHipZetPosition - NewHipZetPosition);
            DifferenceBetweenOldAndNewHipZetPosition = Math.Abs(OldHipPosition - NewHipXPosition);

            float forceByHands = Math.Max(DifferenceBetweenOldAndNewRightHandPosition, DifferenceBetweenOldAndNewLeftHandPosition);
            rigidbody.AddForce(rigidbody.transform.TransformDirection((new Vector3(0, 0, 1)) * forceByHands * speedFactor));


          
            //Animate
            if (forceByHands > 0.03)
            {
                anim.SetInteger("Animation", 1);
            }
            else
            {
                anim.SetInteger("Animation", 0);
            }


            OldRightHandPosition = NewRightHandPosition;
            OldLeftHandPosition = NewLeftHandPosition;
            OldHipPosition = NewHipXPosition;
            OldHipZetPosition = NewHipZetPosition;
        } 

    
    }

    //Move character by moving to the sides
    void KinectMovementWithoutHands()
    {



        if (sw.pollSkeleton())
        {
            NewRightHandPosition = sw.bonePos[0, (int)Bones.HandRight].y;
            NewLeftHandPosition = sw.bonePos[0, (int)Bones.HandLeft].y;
            NewHipXPosition = sw.bonePos[0, (int)Bones.HipCenter].x;
            NewHipZetPosition = sw.bonePos[0, (int)Bones.HipCenter].z;

            //Rotate the player based on hip position
            rigidbody.AddTorque(new Vector3(0, rotationFactor * (NewHipXPosition - OldHipPosition), 0));
            if (DifferenceBetweenOldAndNewHipPosition < 0.05)
            {
                rigidbody.angularVelocity = Vector3.zero;

            }

            changeLight();
            //Get the difference between old a new positions
            DifferenceBetweenOldAndNewRightHandPosition = Math.Abs(OldRightHandPosition - NewRightHandPosition);
            DifferenceBetweenOldAndNewLeftHandPosition = Math.Abs(OldLeftHandPosition - NewLeftHandPosition);
            DifferenceBetweenOldAndNewHipPosition = Math.Abs(OldHipZetPosition - NewHipZetPosition);
            DifferenceBetweenOldAndNewHipZetPosition = Math.Abs(OldHipPosition - NewHipXPosition);

            float forceByHands = Math.Max(DifferenceBetweenOldAndNewRightHandPosition, DifferenceBetweenOldAndNewLeftHandPosition);
            rigidbody.AddForce(rigidbody.transform.TransformDirection((new Vector3(0, 0, 1)) * forceByHands * speedFactor));



            //Animate
            if (forceByHands > 0.03)
            {
                anim.SetInteger("Animation", 1);
            }
            else
            {
                anim.SetInteger("Animation", 0);
            }


            OldRightHandPosition = NewRightHandPosition;
            OldLeftHandPosition = NewLeftHandPosition;
            OldHipPosition = NewHipXPosition;
            OldHipZetPosition = NewHipZetPosition;
        }


    }

    void CenterPointKinectMovement()
    {
        if (sw.pollSkeleton())
        {
            NewRightHandPosition = sw.bonePos[0, (int)Bones.HandRight].y;
            NewLeftHandPosition = sw.bonePos[0, (int)Bones.HandLeft].y;
            NewHipXPosition = sw.bonePos[0, (int)Bones.HipCenter].x;
            NewHipZetPosition = sw.bonePos[0, (int)Bones.HipCenter].z;


            //Get the difference between old a new positions
            DifferenceBetweenOldAndNewRightHandPosition = Math.Abs(OldRightHandPosition - NewRightHandPosition);
            DifferenceBetweenOldAndNewLeftHandPosition = Math.Abs(OldLeftHandPosition - NewLeftHandPosition);
            DifferenceBetweenOldAndNewHipPosition = Math.Abs(OldHipZetPosition - NewHipZetPosition);
            DifferenceBetweenOldAndNewHipZetPosition = Math.Abs(OldHipPosition - NewHipXPosition);

            float forceByHands = Math.Max(DifferenceBetweenOldAndNewRightHandPosition, DifferenceBetweenOldAndNewLeftHandPosition);

            float force = (float)Math.Sqrt(Math.Pow(NewHipXPosition, 2) + Math.Pow(NewHipZetPosition, 2));

            
            double hypotenusePower2 = Math.Pow(NewHipXPosition, 2) + Math.Pow(NewHipZetPosition, 2);
            double hypotenuse = Math.Sqrt(hypotenusePower2);
            double angle = Math.Asin(NewHipZetPosition / hypotenuse);
            double degrees = angle * (180 / Math.PI);
            
            if (NewHipZetPosition > 0 && NewHipXPosition > 0)
            {
                rigidbody.transform.rotation = Quaternion.Euler(0, (float)(90 - Math.Abs(degrees)), 0);
            }
            else if (NewHipZetPosition < 0 && NewHipXPosition > 0)
            {
                rigidbody.transform.rotation = Quaternion.Euler(0, (float)(90 + Math.Abs(degrees)), 0);
            }
            else if (NewHipZetPosition < 0 && NewHipXPosition < 0)
            {
                rigidbody.transform.rotation = Quaternion.Euler(0, (float)(-90 - Math.Abs(degrees)), 0);
            }
            else if (NewHipZetPosition > 0 && NewHipXPosition < 0)
            {
                rigidbody.transform.rotation = Quaternion.Euler(0, (float)(-90 + Math.Abs(degrees)), 0);
            }


            //file.WriteLine(player.rigidbody.velocity.magnitude);
            if (player.rigidbody.velocity.magnitude < maximumSpeed)
            {
                rigidbody.AddForce(rigidbody.transform.TransformDirection((new Vector3(0, 0, 1)) * forceByHands * speedFactor));
            }

            changeLight();

            //Animate
            if (forceByHands > 0.03)
            {
                anim.SetInteger("Animation", 1);
            }
            else
            {
                anim.SetInteger("Animation", 0);
                if (PlayerController.slide == true)
                {
                    rigidbody.velocity = new Vector3(0f, 0f, 0f);
                }
            }


            OldRightHandPosition = NewRightHandPosition;
            OldLeftHandPosition = NewLeftHandPosition;
            OldHipPosition = NewHipXPosition;
            OldHipZetPosition = NewHipZetPosition;
        } 
    
    }

    void CenterPointKinectWithoutHands()
    {
        if (sw.pollSkeleton())
        {

            NewHipXPosition = sw.bonePos[0, (int)Bones.HipCenter].x;
            NewHipZetPosition = sw.bonePos[0, (int)Bones.HipCenter].z;


            //Get the difference between old a new positions
            DifferenceBetweenOldAndNewHipPosition = Math.Abs(OldHipZetPosition - NewHipZetPosition);
            DifferenceBetweenOldAndNewHipZetPosition = Math.Abs(OldHipPosition - NewHipXPosition);


            //Distance between two points, between the center(0,0) and the position of the players body(x,z)
            float force = (float)Math.Sqrt(Math.Pow(NewHipXPosition, 2) + Math.Pow(NewHipZetPosition, 2));
              

            //Rotation
            double hypotenusePower2 = Math.Pow(NewHipXPosition, 2) + Math.Pow(NewHipZetPosition, 2);
            double hypotenuse = Math.Sqrt(hypotenusePower2);
            double angle = Math.Asin(NewHipZetPosition / hypotenuse);
            double degrees = angle * (180 / Math.PI);

            if (NewHipZetPosition > 0 && NewHipXPosition > 0)
            {
                rigidbody.transform.rotation = Quaternion.Euler(0, (float)(90 - Math.Abs(degrees)), 0);
            }
            else if (NewHipZetPosition < 0 && NewHipXPosition > 0)
            {
                rigidbody.transform.rotation = Quaternion.Euler(0, (float)(90 + Math.Abs(degrees)), 0);
            }
            else if (NewHipZetPosition < 0 && NewHipXPosition < 0)
            {
                rigidbody.transform.rotation = Quaternion.Euler(0, (float)(-90 - Math.Abs(degrees)), 0);
            }
            else if (NewHipZetPosition > 0 && NewHipXPosition < 0)
            {
                rigidbody.transform.rotation = Quaternion.Euler(0, (float)(-90 + Math.Abs(degrees)), 0);
            }

            changeLight();

            

            //Animate
            if (force > PlayerController.circleradious)
            {
                //Speed
                if (player.rigidbody.velocity.magnitude < maximumSpeed)
                    rigidbody.AddForce(rigidbody.transform.TransformDirection((new Vector3(0, 0, 1)) * speedFactor * force));
                anim.SetInteger("Animation", 1);
            }
            else
            {
                anim.SetInteger("Animation", 0);
                if (PlayerController.slide == true)
                {
                    rigidbody.velocity = new Vector3(0f, 0f, 0f);
                }
            }
        
            OldHipPosition = NewHipXPosition;
            OldHipZetPosition = NewHipZetPosition;
        }

    }

    


    void changeLight()
    {

        if (NewHipZetPosition < 0.25 && NewHipZetPosition > -0.25 && NewHipXPosition < 0.25 && NewHipXPosition > -0.2)
        {
            playercamera.light.color = new Color(1F - Math.Abs((float)NewHipZetPosition), Math.Abs((float)NewHipZetPosition), 0F, 1F);
        }
        else
        {
            playercamera.light.color = new Color(Math.Abs((float)NewHipZetPosition), 1F - Math.Abs((float)NewHipZetPosition), 0F, 1F);
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        if (collision.collider.name == "guard" || collision.collider.name == "guard2")
        {
            reset();
        }
    }

    void reset()
    {
        player.transform.position = new Vector3(1f, 0.5f, -45f);
        player.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

        guard.transform.position = new Vector3(-2f, 0.5f, -32f);
        Quaternion guardrotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));
        guard.transform.rotation = guardrotation;
        guard.setState(States.MovingRight);

        guard2.transform.position = new Vector3(-35f, 0.5f, -33f);
        Quaternion guard2rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        guard2.transform.rotation = guard2rotation;
        guard2.setState(States.MovingUp);

    }
}


