using UnityEngine;
using System.Collections;

public class MenuBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
