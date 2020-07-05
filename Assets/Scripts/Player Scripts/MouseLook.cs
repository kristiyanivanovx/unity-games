using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

	[SerializeField]
	private Transform playerRoot, lookRoot;

	[SerializeField]
	private bool invert;

	[SerializeField]
	private bool can_Unlock = true;

	[SerializeField]
	private float sensetivity = 5f;

	[SerializeField]
	private int smooth_Steps = 10;

	[SerializeField]
	private float smooth_weight = 0.4f;

	[SerializeField]
	private float roll_Angle = 0f;

	[SerializeField]
	private float roll_Speed = 3f;

	[SerializeField]
	private Vector2 default_Look_Limits = new Vector2(-90f, 90f);

	private Vector2 look_Angles;
	private Vector2 current_Mouse_Look;
	private Vector2 smooth_move;

	private float current_Roll_Angle;

	private int last_Look_Frame;

	// Use this for initialization
	void Start () {

		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

		LockAndUnlockCursor();

		if(Cursor.lockState == CursorLockMode.Locked) {
			LookAround();
		}
	}
	
	// Lock and Unlock
	void LockAndUnlockCursor() {

		if (Input.GetKeyDown(KeyCode.Escape)) {

			if (Cursor.lockState == CursorLockMode.Locked) {
				Cursor.lockState = CursorLockMode.None;
			}
			else {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
	}

	void LookAround() {
		
		current_Mouse_Look = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y),  // MOUSE_Y is for left and right,
										 Input.GetAxis(MouseAxis.MOUSE_X)); // MOUSE_X is for up and down

		// Still, we are assigning MOUSE_Y to look_Angles.x and
		//                         MOUSE_X to look_Angles.y
		look_Angles.x += current_Mouse_Look.x * sensetivity * (invert ? 1f : -1f); // Left and right
		look_Angles.y += current_Mouse_Look.y * sensetivity;					   // Up and Down

		// Clamp doesnt allow look_angles.x to go lower than default_Look_Limits.x and above  default_Look_Limits.y
		look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y);


		// current_Roll_Angle = Mathf.Lerp(current_Roll_Angle,					    // go from this value
		// Input.GetAxisRaw(MouseAxis.MOUSE_X) * roll_Angle,                    	// to this value
		// Time.deltaTime * roll_Speed);                                            // in this time

		lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, 0f);
		playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);

	}
}

