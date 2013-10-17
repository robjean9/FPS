using UnityEngine;
using System.Collections;

public class PlayerBehaviour : CharacterBase {
	
	// Use this for initialization
	void Start () {
		base.Awake();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		
	}
	
	public override void ActionCharacter(){
		
		if(Input.GetButton("Fire1")){
			currentGun.Shoot();
		}
		
		if(Input.GetButtonDown("Fire2")){
			currentGun.Zoom();	
		}
		
		if(Input.GetKeyDown(KeyCode.R)){
			currentGun.ReloadWeapon();	
		}
		
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.LoadLevel("Principal");
		}
		
		if(Input.GetButtonDown("Jump")){
			callJump = true;
		}
		
		axiHorizontal = Input.GetAxis("Horizontal");
		axiVertical = Input.GetAxis("Vertical");
	}
	
	void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "MunitionCubeGun"){
			currentGun.munition += other.GetComponent<MunitionCubeGunBehaviour>().munition;
			if(currentGun.amountBullets == 0){
				currentGun.ReloadWeapon();
			}
			Destroy(other.gameObject);
		}
    }
}
