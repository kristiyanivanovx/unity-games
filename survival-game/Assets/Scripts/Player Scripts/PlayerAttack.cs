using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	private WeaponManager weapon_manager;

	public float fireRate = 15f;
	private float nextTimeToFire;
	public float damage = 20f;

	private Animator zoomCameraAnim;
	private bool zoomed;

	private Camera mainCam;

	private GameObject crosshair;

	private bool is_Aiming;

	[SerializeField]
	private GameObject arrow_Prefab, spear_Prefab;

	[SerializeField]
	private Transform arrow_Bow_StartPosition;

	// Awake
	void Awake() {

		weapon_manager = GetComponent<WeaponManager>();

		zoomCameraAnim = transform.Find(Tags.LOOK_ROOT)
								  .transform.Find(Tags.ZOOM_CAMERA)
								  .GetComponent<Animator>();

		crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

		mainCam = Camera.main;

	}


	// Use this for initialization
	void Start () {
		
	}
	

	// Update is called once per frame
	void Update () {
		WeaponShoot();
		ZoomInAndOut();
	}


	void WeaponShoot() {
		// If we have an assault rifle
		if (weapon_manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE) {
			
			// If we press and hold left mouse click and
			// If Time is greater than the nextTimeToFire
			if (Input.GetMouseButton(0) && Time.time > nextTimeToFire) {

				nextTimeToFire = Time.time + 1f / fireRate;

				weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();

				BulletFired(); 
			}

		}
		// If the weapon is a regular one
		else {

			if (Input.GetMouseButtonDown(0)) {
	
				// handle axe
				if (weapon_manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG) {
					weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();
				}

				// handle shoot
				if (weapon_manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET) {

					weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();

					BulletFired();
				}
				else {

					// we have an arrow or spear
					if (is_Aiming) {

						weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();

						if (weapon_manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.ARROW) {

							// throw an arrow
							ThrowArrowOrSpear(true);
							
						}
						else if (weapon_manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.SPEAR) {

							// throw a spear
							ThrowArrowOrSpear(false);

						}

					}


				}
				

			}
		}


	}

	void ZoomInAndOut() {

		// we are going to aim with our camera on the weapon
		if (weapon_manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM) {

			// if we press and hold right mouse button
			if (Input.GetMouseButtonDown(1)) {

				zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);

				crosshair.SetActive(false);
			}

			// when we release the right mouse button click
			if (Input.GetMouseButtonUp(1)) {

				zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);

				crosshair.SetActive(true);
			}

		}

		// weapon self aim
		if(weapon_manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.SELF_AIM) {

			if (Input.GetMouseButtonDown(1)) {

				weapon_manager.GetCurrentSelectedWeapon().Aim(true);
				is_Aiming = true;

			}

			if (Input.GetMouseButtonUp(1)) {

				weapon_manager.GetCurrentSelectedWeapon().Aim(false);
				is_Aiming = false;

			}

		}


	}

	void ThrowArrowOrSpear(bool throwArrow) {

		// throw an arrow or spear
		if (throwArrow) {

			GameObject arrow = Instantiate(arrow_Prefab);
			arrow.transform.position = arrow_Bow_StartPosition.position;

			arrow.GetComponent<ArrowAndBow>().Launch(mainCam);
		}
		else {

			GameObject spear = Instantiate(spear_Prefab);
			spear.transform.position = arrow_Bow_StartPosition.position;

			spear.GetComponent<ArrowAndBow>().Launch(mainCam);
		}

	}

	void BulletFired() {

		RaycastHit hit;

		//                      from our positon            to forward             until hit    
		if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)) {

			if (hit.transform.tag == Tags.ENEMY_TAG) {
				hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
			}

		}

	}

}
