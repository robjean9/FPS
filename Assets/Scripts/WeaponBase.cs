using UnityEngine;
using System.Collections;

public abstract class WeaponBase : MonoBehaviour {
	
	public BulletBehaviour bullet;
	public Transform canoArma;
	public Animation animationGun;
	public AnimationClip reloadAnimation;
	public AnimationClip fireAnimation;
	public ParticleSystem fireParticle;
	public AudioClip shotSound;
	public AudioClip reloadSound;
	
	//Zoom
	public AnimationClip zoomIn;
	public AnimationClip zoomOut;
	public float zoomAim = 30;
	public float SensibityGun = 5;
	private float startZoom;
	private bool inZoom = false;
	private float initSensibity;
	///

	//Fire
	public float fireRate = 2;
	private float currentTimeToFire = 0;
	private bool canFire = true;
	public int amountBullets = 16;
	public int munition = 64;
	public float powerShot = 100;
	public float range = 500;
	private int initBullets;
	public float timeToPlayReload = 0.5f;
	private float currentTimeToPlayReload = 0;
	public float recoil = 0.2f;
	public MouseLook axiY;
	public MouseLook AxiX;
	private float startRecoil;
	public GUIText aimDisplay;
	private GUIText tempAim;
	public bool useByEnemy = false;
	
	// Use this for initialization
	protected void Start () {
		initBullets = amountBullets;
		if(!useByEnemy){
			startZoom = Camera.mainCamera.fieldOfView;
			initSensibity = AxiX.sensitivityX;
			startRecoil = recoil;
			tempAim = Instantiate(aimDisplay) as GUIText;
			tempAim.enabled = true;
		}
		
		
		
		
	}
	
	// Update is called once per frame
	protected void Update () {
		
		if(canFire == false){
			currentTimeToFire += Time.deltaTime;
			if(currentTimeToFire > fireRate){
				currentTimeToFire = 0;
				canFire = true;
			}
		}
		
		currentTimeToPlayReload += Time.deltaTime;
		
		if(animationGun.isPlaying && animationGun.clip == reloadAnimation){
			canFire = false;	
		}
		
//		if(animationGun.isPlaying == true){
//			canFire = false;
//		}
//		else{
			//canFire = true;
//		}
	
	}
	
	public void Shoot(){
		if(canFire == true && amountBullets > 0){
			animationGun.clip = fireAnimation;
			animationGun.Play();
			fireParticle.Emit(1);
			canFire = false;
			amountBullets--;
			audio.clip = shotSound;
			audio.Play();
			bullet.powerShot = powerShot;
			bullet.range = range;
			OnShoot();
			currentTimeToPlayReload = 0;
			
			if(!useByEnemy){
				
			AxiX.recoil = recoil;
			axiY.recoil = recoil;
			}
		}
		else if(amountBullets == 0){
			ReloadWeapon();
		}
	}
	
	abstract public void OnShoot();

	
	public void ReloadWeapon(){
		if(amountBullets < initBullets && currentTimeToPlayReload > timeToPlayReload){
			if(munition > 0){
				CancelZoom();
				
				if(amountBullets <= initBullets){
					int tempBullets = initBullets-amountBullets;
					if(tempBullets >= munition)
						tempBullets = munition;
					amountBullets += tempBullets;
					munition -= tempBullets;
				}
				
				animationGun.clip = reloadAnimation;
				animationGun.Play();
				
				audio.clip = reloadSound;
				audio.Play();
			}
		}
	}
	
	public void Zoom(){
		if(inZoom == false && !animation.isPlaying && !animationGun.isPlaying){
			UseZoom();
		}
		else if(inZoom == true && !animation.isPlaying && !animationGun.isPlaying){
			CancelZoom();
		}
	}
	
	private void UseZoom(){
		if(inZoom == false){
			animation.clip = zoomIn;
			animation.Play();
			inZoom = true;
			
			if(!useByEnemy){
				AxiX.sensitivityX = SensibityGun;
				axiY.sensitivityY = SensibityGun;
				tempAim.enabled = false;
				Camera.mainCamera.fieldOfView = zoomAim;
				recoil = startRecoil/2;
			}
			
		}
	}
	
	private void CancelZoom(){
		
		if(inZoom == true){
			animation.clip = zoomOut;
			animation.Play();
			inZoom = false;
			if(!useByEnemy){
				Camera.mainCamera.fieldOfView = startZoom;
				recoil = startRecoil;
				AxiX.sensitivityX = initSensibity;
				tempAim.enabled = true;
			}
		}
		
	}
	
//	void OnGUI() {
//		if(!inZoom)
//       	 GUI.Label(new Rect(Screen.width/2, Screen.height/2, 100, 20), "+");
//    }
}
