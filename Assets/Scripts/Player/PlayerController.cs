using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(PlayerPawn))]
[AddComponentMenu("NetGame/Player Controller")]
public class PlayerController : MonoBehaviour {

    CharacterMotor motor;
	bool isFiring = false;
	PlayerPawn pawn;
	bool lockCursor = false;

	// Use this for initialization
	void Awake () {
        motor = GetComponent<CharacterMotor>();
		pawn = GetComponent<PlayerPawn>();
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
		if (Screen.lockCursor != lockCursor)
		{
			if (lockCursor)
				Screen.lockCursor = true;
			else if (!lockCursor)
				Screen.lockCursor = false;
		}
		
		if (Input.GetMouseButton(0))
			lockCursor = true;
		if (Input.GetKeyDown(KeyCode.Escape))
			lockCursor = false;


        if (Screen.lockCursor)
        {
			if (Input.GetButtonDown("Map"))
			{
				//pawn.ToggleMap();
			}

            // Get the input vector from keyboard or analog stick
            Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (directionVector != Vector3.zero)
            {
                // Get the length of the directon vector and then normalize it
                // Dividing by the length is cheaper than normalizing when we already have the length anyway
                float directionLength = directionVector.magnitude;
                directionVector = directionVector / directionLength;

                // Make sure the length is no bigger than 1
                directionLength = Mathf.Min(1, directionLength);

                // Make the input vector more sensitive towards the extremes and less sensitive in the middle
                // This makes it easier to control slow speeds when using analog sticks
                directionLength = directionLength * directionLength;

                // Multiply the normalized direction vector by the modified length
                directionVector = directionVector * directionLength;
            }

            // Apply the direction to the CharacterMotor
            motor.inputMoveDirection = transform.rotation * directionVector;
            motor.inputJump = Input.GetButton("Jump");
			//motor.movement.maxForwardSpeed = PlayerBase.Instance.playerData.MoveSpeed;
			//motor.movement.maxSidewaysSpeed = PlayerBase.Instance.playerData.MoveSpeed * 0.5f;
			//motor.movement.maxBackwardsSpeed = PlayerBase.Instance.playerData.MoveSpeed * 0.5f;

			if ((Input.GetButtonDown("Fire1") || Input.GetAxis("Fire1") > 0.5f) && !pawn.mapActive)
			{
				BeginFire();
			}
			else if ((Input.GetButtonUp("Fire1") || Input.GetAxis("Fire1") < 0.5f) || pawn.mapActive)
			{
				EndFire();
			}

			if (isFiring)
			{
				//pawn.CurrentWeapon.Fire();
			}
        }
        else if (!Screen.lockCursor)
        {
            motor.inputMoveDirection = Vector3.zero;
            motor.inputJump = false;
        }
	}

	public void BeginFire()
	{
		isFiring = true;
	}
	
	public void EndFire()
	{
		isFiring = false;
	}
}
