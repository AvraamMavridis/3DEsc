/*
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
    private float NewHipPosition = 0;
    private float NewHipZetPosition = 0;
    private float OldHipZetPosition = 0;
    private AnimatorStateInfo AnimationState;
    public SkeletonWrapper sw;
    public Animator anim;
    public static float speedFactor;
    public static float rotationFactor;
    public enum MoveType { KeyboardMovement, KinectMovement };
    public static MoveType movement;

    #endregion


    // Use this for initialization

    void Start()
    {
        speedFactor = 150;
        rotationFactor = 134;
        movement = MoveType.KinectMovement;
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


    //Move player using Kinect
    void KinectMovement() {

        if (sw.pollSkeleton())
        {
            NewRightHandPosition = sw.bonePos[0, (int)Bones.HandRight].y;
            NewLeftHandPosition = sw.bonePos[0, (int)Bones.HandLeft].y;
            NewHipPosition = sw.bonePos[0, (int)Bones.HipCenter].x;
            NewHipZetPosition = sw.bonePos[0, (int)Bones.HipCenter].z;

            //Rotate the player based on hip position
            rigidbody.AddTorque(new Vector3(0, rotationFactor * (NewHipPosition - OldHipPosition), 0));
            if (DifferenceBetweenOldAndNewHipPosition < 0.05)
            {
                rigidbody.angularVelocity = Vector3.zero;

            }


            //Get the difference between old a new positions
            DifferenceBetweenOldAndNewRightHandPosition = Math.Abs(OldRightHandPosition - NewRightHandPosition);
            DifferenceBetweenOldAndNewLeftHandPosition = Math.Abs(OldLeftHandPosition - NewLeftHandPosition);
            DifferenceBetweenOldAndNewHipPosition = Math.Abs(OldHipZetPosition - NewHipZetPosition);
            DifferenceBetweenOldAndNewHipZetPosition = Math.Abs(OldHipPosition - NewHipPosition);

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
            OldHipPosition = NewHipPosition;
            OldHipZetPosition = NewHipZetPosition;
        } 

    
    }
    
    
}


