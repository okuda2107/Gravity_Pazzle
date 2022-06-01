using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{

    bool pushFlag;
    bool flag;
    public float gravity;
    public State mState;
    private Order mOrder;
    
    private enum Order
    {
        urdl
    };

    public enum State
    {
        nextDown,
        nextLeft,
        nextRight,
        nextUp
    };

    private State ChangeState()
    {
        switch (mOrder)
        {
            case Order.urdl:
                switch (mState)
                {
                    case State.nextDown:
                        return State.nextLeft;
                        break;
                    case State.nextLeft:
                        return State.nextUp;
                        break;
                    case State.nextUp:
                        return State.nextRight;
                        break;
                    case State.nextRight:
                        return State.nextDown;
                        break;
                    default:
                        return 0;
                }
                break;
            default:
                return 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mState = State.nextLeft;
        pushFlag = false;
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        mOrder = Order.urdl;

        if (Input.GetKey(KeyCode.Space))
        {
            if (!pushFlag)
            {
                pushFlag = true;
                switch (mState)
                {
                    case State.nextUp:
                        Physics2D.gravity = new Vector2 (0.0f, gravity);
                        break;
                    case State.nextDown:
                        Physics2D.gravity = new Vector2 (0.0f, -gravity);
                        break;
                    case State.nextLeft:
                        Physics2D.gravity = new Vector2 (-gravity, 0.0f);
                        break;
                    case State.nextRight:
                        Physics2D.gravity = new Vector2 (gravity, 0.0f);
                        break;
                    default:
                        return;
                }
                mState = ChangeState();
            }
        }
        else
        {
            pushFlag = false;
        }
    }
}
