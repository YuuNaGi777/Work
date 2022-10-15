using UnityEngine;
using System.Collections;

[AddComponentMenu("Player/WidgetCamera")]
public class WidgetCamera : MonoBehaviour {
    public Transform target;
    public float distance = 5.0f; //与目标的间距（z和x方向的）
    public float height = 4.0f;  // 与目标高度间距（y方向）
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    public int distanceDampingX = 1;
    public int distanceDampingZ = 1;

    //The camera controls for looking at the target
    public float camSpeed = 2.0f;
    public bool smoothed = true;

    void LateUpdate()
    {
        if (!target)
            return;

        // Calculate the current rotation angles, positions, and where we want the camera to end up
       float wantedRotationAngle = target.eulerAngles.y;
       float wantedHeight = target.position.y + height;
       float wantedDistanceZ = target.position.z - distance;
       float wantedDistanceX = target.position.x - distance;

       float currentRotationAngle = transform.eulerAngles.y;
       float currentHeight = transform.position.y;
       float currentDistanceZ = transform.position.z;
       float currentDistanceX = transform.position.x;


        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the distance
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        currentDistanceZ = Mathf.Lerp(currentDistanceZ, wantedDistanceZ, distanceDampingZ * Time.deltaTime);
        currentDistanceX = Mathf.Lerp(currentDistanceX, wantedDistanceX, distanceDampingX * Time.deltaTime);

        // Convert the angle into a rotation
       Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the new position of the camera
        transform.position -= currentRotation * Vector3.forward * distance;
        transform.position  =new Vector3(currentDistanceX,currentHeight,currentDistanceZ);
        
        // Make sure the camera is always looking at the target
        LookAtMe();
    }
    void LookAtMe(){
		//check  whether we want the camera to be smoothed or not - can be changed in the Inspector
		if(smoothed)
		 {
			//Find the new rotation value based upon the target and camera's current position.  Then interpolate
			//smoothly between the two using the specified speed setting
			Quaternion camRotation = Quaternion.LookRotation(target.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, camRotation, Time.deltaTime * camSpeed);
		}
		//This default will flatly move with the targeted object
		else{
			transform.LookAt(target);
		}
	}
}
