using UnityEngine;
using System.Collections;

public class DestroyAfterTimeBehaviour: MonoBehaviour {
	
	public float timeToDestroy = 1;
	
	private float currentTime = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		currentTime += Time.deltaTime;
		
		if(currentTime > timeToDestroy)
			Destroy(gameObject);
		
	}
}
