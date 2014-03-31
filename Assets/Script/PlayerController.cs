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


    #endregion


    // Use this for initialization

    void Start()
    {
   
    }

    void Update()
    {
      

        if (sw.pollSkeleton())
        {
            NewRightHandPosition = sw.bonePos[0, (int)Bones.HandRight].y;
            NewLeftHandPosition = sw.bonePos[0, (int)Bones.HandLeft].y;
            NewHipPosition = sw.bonePos[0, (int)Bones.HipCenter].x;
            NewHipZetPosition = sw.bonePos[0, (int)Bones.HipCenter].z;

            //Rotate the player based on hip position

            rigidbody.AddTorque(new Vector3(0, 500F*(NewHipPosition - OldHipPosition), 0));

            if (DifferenceBetweenOldAndNewHipPosition < 0.05)
            {
                rigidbody.angularVelocity = Vector3.zero;

            }
            

            //Get the difference between old a new position
            DifferenceBetweenOldAndNewRightHandPosition = Math.Abs(OldRightHandPosition - NewRightHandPosition);
            DifferenceBetweenOldAndNewLeftHandPosition = Math.Abs(OldLeftHandPosition - NewLeftHandPosition);
            DifferenceBetweenOldAndNewHipPosition = Math.Abs(OldHipZetPosition - NewHipZetPosition);
            DifferenceBetweenOldAndNewHipZetPosition = Math.Abs(OldHipPosition - NewHipPosition);

            float speed = Math.Max(DifferenceBetweenOldAndNewRightHandPosition, DifferenceBetweenOldAndNewLeftHandPosition);
            rigidbody.AddForce(rigidbody.transform.TransformDirection(Vector3.forward * speed*100));

           


            ////if (DifferenceBetweenOldAndNewHipZetPosition > 0.10F)
            ////{
            ////    print("Mpika" + DifferenceBetweenOldAndNewHipZetPosition);
            ////    rigidbody2D.AddTorque(180);
            ////}
 
            //if (DifferenceBetweenOldAndNewHipPosition < 0.05)
            //{
            //    rigidbody2D.angularVelocity = 0;
                
            //}
            
            ////get the current animation clip
            //AnimationState = anim.GetCurrentAnimatorStateInfo(0);
            
            ////if the hands are not moving the player is standing
            //if (DifferenceBetweenOldAndNewRightHandPosition < 0.01 && DifferenceBetweenOldAndNewLeftHandPosition < 0.01)
            //{
            //    anim.SetInteger("Direction", 1);
            //}
            //else if(DifferenceBetweenOldAndNewRightHandPosition > 0.1 && DifferenceBetweenOldAndNewLeftHandPosition < 0.1)
            //{
            //    //if animation is not on moving state, make the player move
            //    if(!AnimationState.IsName("Base.Player_moving"))
            //    {
            //        anim.SetInteger("Direction", 2);
            //    }
            //}

            //The maximum difference of left or right hand position is the speed
            //float speed = Math.Max(DifferenceBetweenOldAndNewRightHandPosition, DifferenceBetweenOldAndNewLeftHandPosition);
            //rigidbody2D.AddForce(rigidbody2D.transform.TransformDirection(Vector2.up) * speed * 15);
           
            //rigidbody2D.AddForce(new Vector2(Math.Abs(oldposition - newposition)*10, 0));
            //rigidbody2D.AddForce(rigidbody2D.transform.TransformDirection(Vector2.up) * Math.Abs(oldposition - newposition) * 10);
            //rigidbody2D.AddForce(new Vector2(Math.Abs(oldposition - newposition),0));
            OldRightHandPosition = NewRightHandPosition;
            OldLeftHandPosition = NewLeftHandPosition;
            OldHipPosition = NewHipPosition;
            OldHipZetPosition = NewHipZetPosition;
        } 

      
    }

    void FixedUpdate()
    {

        

    }

    void changeDirection()
    {
        float Hip_Center_X = sw.bonePos[0, (int)Bones.HipCenter].x;

        if (sw.pollSkeleton())
        {
            if (Hip_Center_X > 0.25)
            {
                anim.SetInteger("direction", (int)Direction.right_idle);
                dir = Direction.right_idle;
            }
            else if (Hip_Center_X < -0.25)
            {
                anim.SetInteger("direction", (int)Direction.left_idle);
                dir = Direction.left_idle;
            }
            else if (Hip_Center_X < 0.25 && Hip_Center_X > -0.25)
            {
                anim.SetInteger("direction", (int)Direction.up_idle);
                dir = Direction.up_idle;
            }

        }
    }
    
    
}


