using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriving : MonoBehaviour {
	[SerializeField] private float MotorForce, SteerForce, BrakeForce;
	[SerializeField] private WheelCollider frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;
	[SerializeField] private Drunk drunk;

	private void Update() {
		float vertical = Input.GetAxis("Vertical") * MotorForce ;
		float horizontal = (Input.GetAxis("Horizontal") ) * SteerForce;

		rearLeftWheel.motorTorque = vertical;
		rearRightWheel.motorTorque = vertical;

		frontLeftWheel.steerAngle = horizontal;
		frontRightWheel.steerAngle = horizontal;

		if (Input.GetKey(KeyCode.Space)) {
			rearLeftWheel.brakeTorque = BrakeForce;
			rearRightWheel.brakeTorque = BrakeForce;
		}

		if (Input.GetKeyUp(KeyCode.Space)) {
			rearLeftWheel.brakeTorque = 0;
			rearRightWheel.brakeTorque = 0;
		}

		if (Input.GetAxis("Vertical") == 0) {
			rearLeftWheel.brakeTorque = BrakeForce;
			rearRightWheel.brakeTorque = BrakeForce;
		}
		else {
			rearLeftWheel.brakeTorque = 0;
			rearRightWheel.brakeTorque = 0;
		}
	}
}
