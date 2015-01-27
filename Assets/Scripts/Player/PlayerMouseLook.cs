using UnityEngine;
using System.Collections;
/// <summary>
/// Mouse look designed for network players. Makes sure that only the owning player can interact with it.
/// </summary>
[AddComponentMenu("NetGame/Mouse Look")]
public class PlayerMouseLook : MonoBehaviour
{

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;


    void Awake()
    {

    }

    void Start()
    {
        // Make the rigid body not change rotation
        if (rigidbody)
            rigidbody.freezeRotation = true;
    }

    void Update()
    {
        if (Screen.lockCursor)
        {
            if (axes == RotationAxes.MouseXAndY)
            {
				float rotationX = transform.localEulerAngles.y;

				if (Input.GetAxis("Joy X") != 0)
				{
					rotationX += Input.GetAxis("Joy X") * sensitivityX;
				}
				else if (Input.GetAxis("Mouse X") != 0)
				{
					rotationX += Input.GetAxis("Mouse X") * sensitivityX;
				}

				if (Input.GetAxis("Joy Y") != 0)
				{
					rotationY += Input.GetAxis("Joy Y") * sensitivityY;
				}
				else if (Input.GetAxis("Mouse Y") != 0)
				{
					rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				}

                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
				if (Input.GetAxis("Joy X") != 0)
				{
					transform.Rotate(0, Input.GetAxis("Joy X") * sensitivityX, 0);
				}
				else if (Input.GetAxis("Mouse X") != 0)
				{
					transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
				}				             
            }
            else
            {
				if (Input.GetAxis("Joy Y") != 0)
				{
					rotationY += Input.GetAxis("Joy Y") * sensitivityY;
				}
				else if (Input.GetAxis("Mouse Y") != 0)
				{
					rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				}

                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
}