//----------------------------------------
//		Unity3D Games Template (C#)
// Copyright © 2015 Lord Meshadieme
// 	   skype : lord_meshadieme
//----------------------------------------

/// <version>
/// 0.5.0
/// </version>
/// <summary>
/// gameMenus attached with every Game Manager Script. (previously menuManager)
/// This manager is going to handle all the events in all other scenes except the main level scene and loading scene.
/// Basically spawning / unspawning of UI elements, taking inputs and displaying information are the general tasks for this script.
/// Much of this is basically hard coding what to do for each button for each scene etc. 
/// Uses nGUI as a base, must have it in the project.
/// The Buttons need to be placed in the UIInputsList in each scene manually.
/// </summary>
/// CHANGELOG: 
///	*	0.1.0: Mantera Version Tree
///	*	0.2.0: STD Version Tree
///	*	0.3.0: HitBoar Version Tree
///	*	0.4.0: JS Template Base
///	*	0.5.0: C# Template Base
/// TODO: Implement Game Specific Code Here
/// <contents>
/// inputProcessing (int, scenes) 
/// inputProcessing (KeyCode, scenes)
/// MenuInputs (int) 
/// LevelInputs (int) 
/// BriefInputs (int) 
/// initMenu (scenes)
/// </contents>

using Meshadieme;
using UnityEngine;
using System.Collections;

namespace Meshadieme {
	public class gameMenus : parentManager {

		/// <summary>
		/// initMenu (Custom) funtion - Parameters (index = tthe current scene)
		/// This called by GM during its start function in the event the scene is not a level or the loading scene
		/// The objective of this function is to spawn / activate / deactive scene UI and set text etc. I will give some examples of possible code below
		/// * Using database to fill Label text
		/// <example>
		///		var dbNum : int = gameManager.Get().framework.levelSelect - 1;
		///		var timeMin : int = gameManager.Get().data.videoLength[dbNum] / 60;
		///		var timeSec : int = gameManager.Get().data.videoLength[dbNum] % 60;
		///		
		///		gameManager.Get().levelMan.referenceObject[0].GetComponent(UILabel).text = timeMin + ":" + timeSec;
		///		gameManager.Get().levelMan.referenceObject[1].GetComponent(UILabel).text = "" + gameManager.Get().data.numDiff[dbNum];
		///		gameManager.Get().levelMan.referenceObject[2].GetComponent(UILabel).text = "" + gameManager.Get().data.chancePause[dbNum];
		///		gameManager.Get().levelMan.referenceObject[3].GetComponent(UILabel).text = "" + gameManager.Get().data.chanceSpot[dbNum];
		///		
		/// </example>
		/// * Using database to spawn images / titles, you should make a reference to the GameObject in the referenceObject array
		/// <example>
		///		var dbNum : int = gameManager.Get().framework.levelSelect - 1;
		///		switch (dbNum) {
		///			case 0: 
		///				gameManager.Get().levelMan.referenceObject[4].SetActiveRecursively(true);
		///				break;
		///			case 1:
		///				gameManager.Get().levelMan.referenceObject[5].SetActiveRecursively(true);
		///				break;
		///		}
		/// </example>
		/// * Spawning UI Buttons from database and add them to inputlist, Make sure the prefabs have nGUITargetSetter script 
		///		so that it can add the button to input List and reset the target of the UIButtonMessage
		/// <example>
		///		var dbNum : int = gameManager.Get().framework.levelSelect - 1;
		///		var i : int;
		///		var buttonObj : GameObject;
		///		var parentObj : GameObject = gameManager.Get().levelMan.referenceObject[0] as GameObject;
		///		var parent : Transform = parentObj.transform;
		///		var msgUI : Component[];
		///		
		///		for (i = 0; i < gameManager.Get().framework.mainLevels.length; i++) {
		///			buttonObj = Instantiate(gameManager.Get().levelMan.referenceObject[1], parent.position, parent.rotation);
		///			buttonObj.transform.parent = parent;
		///			gameManager.Get().moveObj(buttonObj, Vector3(0, 50 * (i+1), 0), Vector3(0, 0, 0), Vector3(1, 1, 1)); // Adjusting position independently based on spawning index, a little math needed here
		///			buttonObj.GetComponentInChildren(UILabel).text = gameManager.Get().framework.videoTitle[i]; // Set UILabel within the new object
		///		}
		/// </example>
		/// </summary>
		public virtual void initMenu ( Scenes index ) {
			DebugLog("gMenus.initMenu=" + index);
		}

		/// <summary>
		/// inputProcessing (Custom) funtion - Parameters (gObj = the reference to the button in GM.inputList, scene = the current scene)
		/// inputProcessing (Custom) funtion - Parameters (kC = the keyCode of the keyboard input, scene = the current scene)
		/// Basically once input is recieved it SHOULD be handled by seperate functions for seperate scenes, so YOU CAN ADD more functions as needed.
		/// I say should because you can just add all the code in one function but most people can't manage too many nested if's switch statement without struggling so keep things neat.
		/// I have a seperate function for inputs from keyboard if needed although most likely won't, its just pressing a UI Button why need keyboard for that.
		/// * For each scene call seperate scene related function to handle inputs
		/// </summary>
		//nGUI Button Commands
		public virtual void inputProcessing (Scenes scene, GameObject gObj) {
			DebugLog("gMenus.inputProcessing=" + gObj);
		}
		
	}
}