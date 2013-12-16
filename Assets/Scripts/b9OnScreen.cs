﻿
//Stolen from Attila

using UnityEngine;
using System.Collections;

public class b9OnScreen : MonoBehaviour {

    public Color guiTextColor;
    public Color guiTitleColor;

	void OnGUI () {
        guiTextColor= new Color(0.94F, 0.6F, 0.2F, .92F);
        guiTitleColor = new Color(1F, 1F, 1F, .85F);

        // Make a background box
		GUIStyle infoStyle = new GUIStyle();	
		infoStyle.fontSize = 9;
		infoStyle.font = GUI.skin.font;

        GUIStyle smallStyle = new GUIStyle();
        smallStyle.normal.textColor = guiTitleColor;
        smallStyle.fontSize = 13;
        //smallStyle.fontStyle = FontStyle.Bold;
        smallStyle.font = GUI.skin.font;

        GUIStyle mainStyle = new GUIStyle();
        mainStyle.normal.textColor = guiTextColor;
        //mainStyle.fontStyle = FontStyle.Bold;
        mainStyle.fontSize = 13;
        mainStyle.font = GUI.skin.font;


//		GUI.Box(new Rect(10,10,100,90), "Hotkeys");

//		string s = player.speed.ToString();
//		string d = player.direction.ToString();
		
//		GUI.Box (new Rect (8,8,220,260), "Avatar Controller  (v"+version+")");
		
//		GUI.Label(new Rect(10, 25, 160,20), "Avatar: "+player.avatar.name);		
//		GUI.Label(new Rect(10,45, 100,20), "Speed:    "+s);		
//		GUI.Label(new Rect(10,55, 100,40), "Direction:"+d);
		
		GUI.Label(new Rect(10, 20, 200, 120), "CAMERA", smallStyle);
        GUI.Label(new Rect(10, 40, 200, 120), "Camera Left/Right : < >", mainStyle);
        GUI.Label(new Rect(10, 60, 200, 120), "Camera Up/Down : \" ?", mainStyle);
        GUI.Label(new Rect(10, 80, 200, 120), "Camera Zoom : PgUp PgDn", mainStyle);
        GUI.Label(new Rect(10, 100, 200, 120), "Reset Camera: Home", mainStyle);

        GUI.Label(new Rect(10, 140, 200, 120), "KEYBOARD", smallStyle);
        GUI.Label(new Rect(10, 160, 200, 120), "Move avatar : Arrows", mainStyle);
        GUI.Label(new Rect(10, 180, 200, 120), "SpeedUp : LeftShift+Arrows", mainStyle);
        GUI.Label(new Rect(10, 200, 200, 120), "Strafe/SideStep: Alt+Arrows", mainStyle);
        GUI.Label(new Rect(10, 220, 200, 120), "Alert : L key", mainStyle);

        GUI.Label(new Rect(10, 260, 200, 120), "GAMEPAD", smallStyle);
        GUI.Label(new Rect(10, 280, 200, 120), "Camera : DPad", mainStyle);
        GUI.Label(new Rect(10, 300, 200, 120), "Camera Reset : Back", mainStyle);

        GUI.Label(new Rect(10, 320, 200, 120), "Move : Left Stick", mainStyle);
        GUI.Label(new Rect(10, 340, 200, 120), "Strafe/SideStep: Right Stick", mainStyle);
        GUI.Label(new Rect(10, 360, 200, 120), "Alert : Left Bumper", mainStyle);


//		GUI.Label(new Rect(10,130, 160,120), "Z/X: Zoom camera");
//		GUI.Label(new Rect(10,150, 160,120), "R  : Reset avatar");

	}
}