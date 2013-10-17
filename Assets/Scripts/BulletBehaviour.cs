using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
	
	public float speed = 2;
	public float timeToLive = 4;
	public float range = 1500;
	public float powerShot = 10;
	
	private float currentTimeToLive = 0;
	
	private Ray rayAim;
	private RaycastHit hit;
	
	public GameObject dustShot;
	public GameObject bulletHole;
	public float distanceFromWall = 0.01f;
	public bool canMark = false;
	
	// Use this for initialization
	void Start () {
		rayAim = Camera.mainCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
		
		Vector3 directionShot = transform.TransformDirection(Vector3.forward);
		
		if(Physics.Raycast(rayAim, out hit, range)){
			if(hit.rigidbody != null){		
				if(hit.rigidbody.tag == "TargetShot"){
					hit.rigidbody.AddForceAtPosition(directionShot*powerShot, hit.transform.position);	
				}
			}
			else{
				Instantiate(dustShot, hit.point, dustShot.transform.rotation);
				canMark = true;
				
			}
			
		}
		GameObject tempMark = null;
		//if(canMark)
			 tempMark = Instantiate(bulletHole, hit.point + (hit.normal * distanceFromWall), Quaternion.LookRotation(hit.normal)) as GameObject;
			tempMark.transform.parent = hit.transform;
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		currentTimeToLive += Time.deltaTime;
		
		if(currentTimeToLive > timeToLive)
			Destroy(gameObject);
		
		transform.Translate(Vector3.forward*speed);
		
		if(Physics.Raycast(rayAim, out hit, range)){
			if(hit.rigidbody != null){		
				transform.rotation = Quaternion.LookRotation(hit.point-transform.position);
			}
		}
	}
}	
	