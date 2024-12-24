using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public enum PlayerState
    {
        idle = 0,
        walk = 1,
        walkWithBox = 2,
        idleWithBox = 3,
    }
    public Animator playerAnimator;
    public float walkSpeed;
    public float tempSpeed;
    public float rotationSpeed;
    Vector3 newDir;
    Vector3 direction;
    public FloatingJoystick variableJoystick;
    public Rigidbody rb;
    public bool isCameraMoving;

    private void Awake() 
    {
        instance = this;
    }
    private void Start()
    {
        if(PlayerPrefs.HasKey("PlayerSpeed"))
        {
            tempSpeed = PlayerPrefs.GetFloat("PlayerSpeed");
        }
        else
        {
            tempSpeed = walkSpeed;
        }

    }

    public void FixedUpdate()
    {
        if(isCameraMoving) return;
        direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rb.MovePosition(transform.position + direction * Time.fixedDeltaTime * tempSpeed);
        //rb.AddForce(transform.position + direction * Time.fixedDeltaTime * tempSpeed);
        newDir = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        if (variableJoystick.Horizontal == 0 && variableJoystick.Vertical == 0)
        {
            if(!GlobalData.instance.isFoodOnHand && !GlobalData.instance.isBoxOnHand)
            {
                playerAnimator.SetInteger("Base", (int)PlayerState.idle);
            }
            if(GlobalData.instance.isFoodOnHand || GlobalData.instance.isBoxOnHand)
            {
                playerAnimator.SetInteger("Base", (int)PlayerState.idleWithBox);
            }
            
        }
        else
        {
            if (!GlobalData.instance.isFoodOnHand && !GlobalData.instance.isBoxOnHand)
            {
                playerAnimator.SetInteger("Base", (int)PlayerState.walk);
            }
            else
            {
                playerAnimator.SetInteger("Base", (int)PlayerState.walkWithBox);
            }
        }
    }
}
