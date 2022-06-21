using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGravity : Gravity
{
    public Player mPlayer;

    private void GravityChange()
    {
        if (mTag == Tag.change || mTag == Tag.capture)
        {
            if (mPlayer.gravityFlag)
            {
                mDirect = mPlayer.mDirect;
                mPlayer.gravityFlag = false;
            }
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
        firstDirect = mDirect;
        mOrder = mPlayer.mOrder;
    }

    // Update is called once per frame
    void Update()
    {
        GravityForce();
        GravityChange();
    }
}
