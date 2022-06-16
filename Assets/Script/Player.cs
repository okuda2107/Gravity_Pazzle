using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GravitySwitch graSwi;
    private Rigidbody2D rb = null;

    public float mSpeed;

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

        if (graSwi.mDirect == GravitySwitch.Direct.Down || graSwi.mDirect == GravitySwitch.Direct.Up)
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
        else if (graSwi.mDirect == GravitySwitch.Direct.Left || graSwi.mDirect == GravitySwitch.Direct.Right)
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
