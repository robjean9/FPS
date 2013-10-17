using UnityEngine;
using System.Collections;

public abstract class CharacterBase : MonoBehaviour {
	
	private CharacterMotor motor;
	
	public WeaponBase currentGun;
	
	public float runVelocity = 12;
	private float startVelocity;
	protected bool callJump = false;
	protected float axiHorizontal;
	protected float axiVertical;
	public bool isEnemy = false;
	
	protected void Awake () {
		motor = GetComponent<CharacterMotor>();
		currentGun = GetComponentInChildren<WeaponBase>();
		startVelocity = motor.movement.maxForwardSpeed;
		if(!isEnemy){	
			currentGun.AxiX = GetComponent<MouseLook>();
			currentGun.axiY = Camera.mainCamera.GetComponent<MouseLook>();
		}
	}

	
	// Update is called once per frame
	protected void Update () {
		// Get the input vector from kayboard or analog stick
		Vector3 directionVector = new Vector3(axiHorizontal, 0, axiVertical);
		
		if (directionVector != Vector3.zero) {
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			var directionLength = directionVector.magnitude;
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
		motor.inputJump = callJump;
		if(callJump){
			callJump = false;
		}	
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
			motor.movement.maxForwardSpeed = runVelocity;
			motor.movement.maxBackwardsSpeed = runVelocity;
			motor.movement.maxSidewaysSpeed = runVelocity;
		}
		else{
			motor.movement.maxForwardSpeed = startVelocity;
			motor.movement.maxBackwardsSpeed = startVelocity;
			motor.movement.maxSidewaysSpeed = startVelocity;
		}
		
		ActionCharacter();
	}
	
	public abstract void ActionCharacter();
}
