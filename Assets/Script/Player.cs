using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Gravity
{
    public float mSpeed;
    [System.NonSerialized] public bool gravityFlag = false;

    enum State
    {
        Normal,
        Item,
        Dead
    }

    public void CheckStatus()
    {
        
    }

    public void Move()
    {
        float xSpeed = 0.0f;
        float ySpeed = 0.0f;
        bool leftFlag = Input.GetKey(KeyCode.LeftArrow);
        bool rightFlag = Input.GetKey(KeyCode.RightArrow);
        bool upFlag = Input.GetKey(KeyCode.UpArrow);
        bool downFlag = Input.GetKey(KeyCode.DownArrow);

        if (mDirect == Gravity.Direct.Down || mDirect == Gravity.Direct.Up)
        {
            if (leftFlag && !rightFlag)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                xSpeed = -mSpeed;
            }
            if (rightFlag && !leftFlag)
            {
                transform.localScale = new Vector3(1, 1, 1);
                xSpeed = mSpeed;
            }         
            ySpeed = rb.velocity.y;
        }
        else if (mDirect == Gravity.Direct.Left || mDirect == Gravity.Direct.Right)
        {
            if (upFlag && !downFlag)
            {
                transform.localScale = new Vector3(1, 1, 1);
                ySpeed = mSpeed;
            }
            if (downFlag && !upFlag)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                ySpeed = -mSpeed;
            }
            xSpeed = rb.velocity.x;
        }

        rb.velocity = new Vector2(xSpeed, ySpeed);         
    }

    private void GravityChange()
    {
        if (Input.GetKey(KeyCode.Space) 
            && (mTag == Tag.change || mTag == Tag.capture))
        {
            if ((!pushFlag) && ground.IsGround())
            {
                pushFlag = true;
                gravityFlag = true;
                mDirect = ChangeDirect();
            }
        }
        else
        {
            pushFlag = false;
        }

        if (mTag == Tag.release)
        {
            mDirect = firstDirect;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ground.IsGround());
        GravityForce();
        GravityChange();
        Move();
    }
}
