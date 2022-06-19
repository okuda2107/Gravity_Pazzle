using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    bool pushFlag; //押しているかどうかのフラグ
    public Order mOrder = Order.urdl; //重力切り替えの順番のフラグ
    public Direct mDirect = Direct.Down; //今の重力方向
    private Direct firstDirect; //初期重力方向
    private Rigidbody2D rb = null;
    public float gravity = 9.81f;
    public Tag mTag = Tag.capture; //オブジェクトの属性(重力方向が切り替えられるかどうか)

    public GroundTrigger ground;

    public enum Direct
    {
        Down,
        Up,
        Left,
        Right
    };
    
    public enum Tag
    {
        change, //必ず重力方向が変更される
        capture, //自分でオンオフできる．オンの状態
        release, //自分でオンオフできる．オフの状態
        unchange //重力方向が変えられない
    };
    
    public enum Order
    {
        urdl //時計回り
    };

    public void GravityForce() //重力を作用させる関数
    {
        switch (mDirect)
        {
            case Direct.Up:
                rb.AddForce(new Vector2(0.0f, gravity));
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f));
                break;
            case Direct.Down:
                rb.AddForce(new Vector2(0.0f, -gravity));
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
                break;
            case Direct.Left:
                rb.AddForce(new Vector2(-gravity, 0.0f));
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 270.0f));
                break;
            case Direct.Right:
                rb.AddForce(new Vector2(gravity, 0.0f));
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
                break;
        }
    }

    private Direct ChangeDirect() //重力方向を変更する関数
    {
        switch (mOrder)
        {
            case Order.urdl:
                switch (mDirect)
                {
                    case Direct.Up:
                        return Direct.Right;
                        break;
                    case Direct.Down:
                        return Direct.Left;
                        break;
                    case Direct.Left:
                        return Direct.Up;
                        break;
                    case Direct.Right:
                        return Direct.Down;
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
        rb = GetComponent<Rigidbody2D>();
        pushFlag = false;
        firstDirect = mDirect;
    }

    // Update is called once per frame
    void Update()
    {
        GravityForce();

        if (Input.GetKey(KeyCode.Space) 
            && (mTag == Tag.change || mTag == Tag.capture)
            && ground.IsGround())
        {
            if (!pushFlag)
            {
                pushFlag = true;
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
}
