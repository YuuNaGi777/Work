using UnityEngine;
using System.Collections;

public class WidgetController : MonoBehaviour {

    public float rollSpeed = 3.0f;     // 角色的移动速度
    public float fastRollSpeed = 2.0f;  //移动加速度
    public float jumpSpeed = 8.0f;  // 跳跃高度
    public float gravity = 20.0f;  // 下降位移
    public float rotateSpeed = 2.0f; // 旋转速度
    public float duckSpeed = 0.5f;  // 减慢时的速度

    Vector3 moveDirection = Vector3.zero;  // 移动方向
    bool grounded = false;  // 是否在地面上的判断
    float moveHorz = 0.0f;  //水平旋转角度（视角控制）
    float normalHeight = 2.0f;  //控制器的初始高度
    float duckHeight = 1.0f;  //下蹲的控制器高度
    Vector3 rotateDirection = Vector3.zero; //旋转方向

    public bool isControllable = true;  //可否进行控制的判断
    CharacterController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>(); //获取角色的控制器组件
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void FixedUpdate()
    {
        if (!isControllable)
        {
            Input.ResetInputAxes();   
        }
        else
        {
            if (grounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= rollSpeed;
                controller.height = normalHeight; //控制器的高度设置
                controller.center = new Vector3(controller.center.x, controller.height / 2f, controller.center.z); //控制器的中心设置
                moveHorz = Input.GetAxis("Horizontal");
                if (moveHorz > 0)
                    rotateDirection = new Vector3(0, 1, 0);
                else if (moveHorz < 0)
                    rotateDirection = new Vector3(0, -1, 0);
                else
                    rotateDirection = new Vector3(0, 0, 0);

                if (Input.GetButton("Jump"))  // 空格键跳跃
                {
                    moveDirection.y = jumpSpeed;
                }
                if (Input.GetButton("Fire2"))    // 按住鼠标右键，移动加速
                {
                    moveDirection *= fastRollSpeed;  
                }
                if (Input.GetButton("Fire3"))  //按住鼠标中键，控制器下移，速度减慢（可以后续配合下蹲的动画效果）
                {
                    controller.height = duckHeight;
                    controller.center = new Vector3(controller.center.x, controller.height / 2f + 0.25f, controller.center.z);
                    moveDirection *= duckSpeed;
                }
            }
            moveDirection.y -= gravity * Time.deltaTime;   //下移
            CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime); //下移碰撞
            controller.transform.Rotate(rotateDirection * Time.deltaTime, rotateSpeed); //角色旋转
            grounded = ((flags & CollisionFlags.CollidedBelow) != 0);           // 碰撞地面则为true
        }
    }
}
