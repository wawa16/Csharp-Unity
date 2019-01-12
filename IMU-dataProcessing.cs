//This code uses serial communication to obtain IMU-data values from sensor and map it into a virtual robot manipulator in unity

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class mpudata : MonoBehaviour {
	GameObject base_joint;
	GameObject should_joint;
	GameObject elbow_joint;
	GameObject wrist_joint;
	GameObject cube1, cube2, cube3, bound1, bound2;
	float old_t = 0;
	float new_t = 0;
	public float[] timeArray;
	public int i = 0;
	bool count = true;
	bool logflag = true;

	SerialPort stream = new SerialPort("COM3", 115200);
	void Start(){
		base_joint = GameObject.Find("robotic_arm/Base_Joint");
		should_joint = GameObject.Find("robotic_arm/Base_Joint/Shoulder_Joint");
		elbow_joint = GameObject.Find ("robotic_arm/Base_Joint/Shoulder_Joint/Elbow_Joint");
		wrist_joint = GameObject.Find ("robotic_arm/Base_Joint/Shoulder_Joint/Elbow_Joint/Wrist_Joint");
		cube1 = GameObject.Find ("Cube_1");
		cube2 = GameObject.Find ("Cube_2");
		cube3 = GameObject.Find ("Cube_3");
		bound1 = GameObject.Find ("Bound1");
		bound2 = GameObject.Find ("Bound2");
		cube1.GetComponent<cubecol> ().cube1_collide = false;
		cube2.GetComponent<cubecol> ().cube1_collide = false;
		cube3.GetComponent<cubecol> ().cube1_collide = false;

		stream.Open ();
		stream.ReadTimeout = 10000;
		timeArray = new float[100];
		timeArray [0] = 0;
	}
	void Update(){
		
		if (!stream.IsOpen)
			stream.Open ();
	   
		stream.Write ("B");
		float baseyaw = int.Parse (stream.ReadLine ());

		stream.Write ("S");
		float baseroll = int.Parse (stream.ReadLine ());
		if (bound2.GetComponent<boundcol> ().bound_collide) {
			stream.Write ("L");
//			cube2.transform.GetComponent<Renderer> ().material.color = Color.red;
		} else {
			cube2.transform.GetComponent<Renderer>().material.color = Color.grey;
		}

		if (bound1.GetComponent<boundcol> ().bound_collide) {
			stream.Write ("R");
//			cube3.transform.GetComponent<Renderer>().material.color = Color.red;
		}  else {
			cube3.transform.GetComponent<Renderer>().material.color = Color.grey;
		}

//		Debug.Log(cube1.GetComponent<cubecol>().cube1_collide);
		if (cube1.GetComponent<cubecol> ().cube1_collide || cube2.GetComponent<cubecol> ().cube1_collide || cube3.GetComponent<cubecol> ().cube1_collide) {
			new_t = Time.time - old_t;
			if (count) {
				i = i + 1;
			}
			count = false;
			logflag = true;
		} else {
			old_t = Time.time;
			count = true;
			if (i <= 100 && logflag) {
				timeArray [i] = new_t;
//				Debug.Log (i + "," + timeArray [i]);
			}
			logflag = false;
		}

//		stream.Write ("E");
//		float elbowroll = int.Parse (stream.ReadLine ());
//
//		stream.Write ("W");
//		float wristroll = int.Parse (stream.ReadLine ());
		base_joint.transform.localEulerAngles = new Vector3 (0, (baseyaw*0.01f)+20, 0);
		should_joint.transform.localEulerAngles = new Vector3 (0, 0, -((baseroll*0.01f)+10));
//		elbow_joint.transform.localEulerAngles = new Vector3 (0, 0, -(elbowroll-180));
//		wrist_joint.transform.localEulerAngles = new Vector3 (wristroll,-93.758f, 1.662994f);

	}

}
