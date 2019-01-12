//This code logs the data to a txt file using StreamWriter class

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class LogText : MonoBehaviour
{ 

	GameObject GO;

	void Start ()
	{
		GO = GameObject.Find("GameObject");
	}

	void OnApplicationQuit ()
	{
		StreamWriter sw = new StreamWriter("table.txt");

		for (int m = 1; m <= GO.GetComponent<mpudata> ().i; m++) {
			sw.WriteLine(m + "," + GO.GetComponent<mpudata> ().timeArray[m]);
		}
		sw.Close();
	}


}
