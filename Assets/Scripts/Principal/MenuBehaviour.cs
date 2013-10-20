using UnityEngine;
using System.Collections;

public class MenuBehaviour : MonoBehaviour {
	public float amountOfLight = 20;
	public GameObject spotLight;
	public bool lightBackGroundOn = false;
	public GameObject plane;
	
	
	// Use this for initialization
	void Start () {
		
		lightBackGroundOn = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!lightBackGroundOn){
			spotLight.light.range += -1;
		}
		if(spotLight.light.range == 0){
			spotLight.light.range = 20;
			lightBackGroundOn = true;
		}
		
		plane.transform.Rotate(new Vector3(0,1,0));
			
	}
	
	
	void OnGUI(){
		GUI.BeginGroup(new Rect(Screen.width/2-100,Screen.height/2-150,1500,1500));
		if(GUI.Button(new Rect(0, 30, 100, 30), "GamePlay")){
			Application.LoadLevel("GamePlay");
		}
		if(GUI.Button(new Rect(0, 70, 100, 30), "Sair")){
			Application.Quit();	
		}
		GUI.EndGroup();
	}
}
