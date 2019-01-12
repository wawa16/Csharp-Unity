using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using Leap;
using Leap.Unity;

public class leapscript : MonoBehaviour
{
    public Hand hand;
    public Controller controller;
    public Frame frame;
    public Finger finger;

    private float palmposX, palmposY, palmposZ;
    //GameObject mmyCube;

    // Use this for initialization
    void Start()
    {
        //mmyCube = GameObject.Find("Cube");

        // Vector velocity = hand.PalmVelocity;
        //Vector direction = hand.Direction;
    }

    // Update is called once per frame
    void Update()
    {
        controller = new Controller();
        frame = controller.Frame();
        
        List<Hand> hands = frame.Hands;
        if (frame.Hands.Count > 0)
        {
            Hand firstHand = hands[0];
           
        }
        finger = new Finger();
        Vector direction = finger.Direction;

        palmposX = -(1.5f * (hands[0].PalmPosition.x * 0.001f) - 0.1f);
        string posx = Math.Round(palmposX, 3).ToString("000.000");
        float posxpar = float.Parse(posx);

        palmposY = 1.3f * (hands[0].PalmPosition.z * 0.001f) - 0.3f;
        string posy = Math.Round(palmposY, 3).ToString("000.000");
        float posypar = float.Parse(posy);

        palmposZ = -(1.3f * (hands[0].PalmPosition.y * 0.001f) + 0.13f);
        string posz = Math.Round(palmposZ, 3).ToString("000.000");
        float poszpar = float.Parse(posz);


        float fist = hands[0].GrabAngle;
        float palmYaw = hands[0].Direction.Yaw;
        
        UnityEngine.Debug.Log(fist);

    }
}
