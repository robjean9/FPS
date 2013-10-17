using UnityEngine;
using System.Collections;

public class EnemyBehaviour : CharacterBase {
	
	public float walkVelocity = 15;
	public float timeToChangeAction = 10;
	private float currentTimeToChangeAction = 0;
	
	// Use this for initialization
	void Start () {
		base.Awake();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}
	
	
	public override void ActionCharacter ()
	{
		currentTimeToChangeAction += Time.deltaTime;
		
		if(currentTimeToChangeAction < timeToChangeAction){
			currentTimeToChangeAction = 0;
			axiHorizontal = Time.deltaTime*walkVelocity;
			axiVertical = Time.deltaTime*walkVelocity;
		}
	}
}
