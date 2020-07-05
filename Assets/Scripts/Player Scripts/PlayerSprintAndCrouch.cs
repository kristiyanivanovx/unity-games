using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour {

	private PlayerMovement playerMovement;

	public float sprint_Speed = 10f;
	public float move_Speed = 5f;
	public float crouch_Speed = 1.5f;

	private Transform look_Root;
	private float stand_Height = 1.6f;
	private float crouch_Height = 1f;

	private bool is_Crouching;

	private PlayerFootsteps player_Footsteps;

	private float sprint_Volume = 1f;
	private float crouch_Volume = 0.1f;
	private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;

	private float walk_Step_Distance = 0.4f;
	private float sprint_Step_Distance = 0.25f;
	private float crouch_Step_Distance = 0.5f;

	private PlayerStats player_Stats;

	private float sprint_Value = 100f;
	public float sprint_Treshold = 15f;


	void Awake () {

		playerMovement = GetComponent<PlayerMovement>();

		look_Root = transform.GetChild(0);

		player_Footsteps = GetComponentInChildren<PlayerFootsteps>();

		player_Stats = GetComponent<PlayerStats>();

	}

	void Start() {
		player_Footsteps.volume_Min = walk_Volume_Min;
		player_Footsteps.volume_Max = walk_Volume_Max;
		player_Footsteps.step_Distance = walk_Step_Distance;
	}

	// Update is called once per frame
	void Update () {
		Sprint();
		Crouch();
	}

	// Sprint
	void Sprint() {

		// if we have stamina, we can sprint
		if (sprint_Value > 0f) {

			if (!is_Crouching && Input.GetKeyDown(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))) {


				playerMovement.speed = sprint_Speed;

				player_Footsteps.step_Distance = sprint_Step_Distance;

				player_Footsteps.volume_Min = sprint_Volume;

				player_Footsteps.volume_Max = sprint_Volume;

			}

		}

		if (Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching) {

			playerMovement.speed = move_Speed;

			player_Footsteps.step_Distance = walk_Step_Distance;
			player_Footsteps.volume_Min = walk_Volume_Min;
			player_Footsteps.volume_Max = walk_Volume_Max;
			
		}

		if (Input.GetKey(KeyCode.LeftShift) && !is_Crouching) {

			sprint_Value -= sprint_Treshold * Time.deltaTime;

			if (sprint_Value <= 0f) {

				sprint_Value = 0f;

				// reset the speed and sound
				playerMovement.speed = move_Speed;
				player_Footsteps.step_Distance = walk_Step_Distance;
				player_Footsteps.volume_Min = walk_Volume_Min;
				player_Footsteps.volume_Max = walk_Volume_Max;

			}

			player_Stats.Display_StaminaStats(sprint_Value);

		}
		else {

			if (sprint_Value != 100) {

				sprint_Value += (sprint_Treshold / 2f) * Time.deltaTime;

				player_Stats.Display_StaminaStats(sprint_Value);

				if (sprint_Value > 100f)
					sprint_Value = 100f;

			}

		}
	}

	// Crouch
	void Crouch() {

		if (Input.GetKeyDown(KeyCode.LeftControl)){

			if (is_Crouching) {

				look_Root.localPosition = new Vector3(0F, stand_Height, 0f);
				playerMovement.speed = move_Speed;

				player_Footsteps.step_Distance = walk_Step_Distance;
				player_Footsteps.volume_Min = walk_Volume_Min;
				player_Footsteps.volume_Max = walk_Volume_Max;

				is_Crouching = false;

			}
			else {

				look_Root.localPosition = new Vector3(0F, crouch_Height, 0f);
				playerMovement.speed = crouch_Speed;

				player_Footsteps.step_Distance = crouch_Step_Distance;
				player_Footsteps.volume_Min = crouch_Volume;
				player_Footsteps.volume_Max = crouch_Volume;

				is_Crouching = true;

			}
		}
	}
}
