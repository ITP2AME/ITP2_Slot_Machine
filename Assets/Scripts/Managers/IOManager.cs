//----------------------------------------
//		Unity3D Games Template (C#)
// Copyright © 2015 Lord Meshadieme
// 	   skype : lord_meshadieme
//----------------------------------------

/// <version>
/// 0.5.0
/// </version>
/// <summary>
///// IO (Input Output) Manager attached with every Game Manager Script.
///// This manager takes any input recieved and handles them accordingly.
///// </summary>
///// CHANGELOG: 
/////	*	0.1.0: Mantera Version Tree
/////	*	0.2.0: STD Version Tree
/////	*	0.3.0: HitBoar Version Tree
/////	*	0.4.0: JS Template Base
/////	*	0.5.0: C# Template Base
///// TODO: Add Gyro and Accel stuff
///// TODO: Rework Enemy Spawning variables to lists.
///// TODO: Rework the Level Pieces Management for 3D games.
///// TODO: Particle management stuff either here or in its own manager.
///// TODO: Learn to use actual event listeners and implement them, in atleast keyboard instead of checking key input every frame unnecessarily.
///// <contents>
///// initInputs () 
///// inputPress (GameObject)
///// inputRelease (GameObject) 
///// processCommands (inputObject, int, boolean) 
///// clearQue () 
///// addSpawnInputs (GameObject) 
///// delSpawnedInput (GameObject) 
///// Update ()
///// </contents>


//Imports
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Meshadieme;

namespace Meshadieme {
	
	public enum inputType {
		ButtonUI = 0,
		AnalogUI = 1,
		Keyboard = 2,
		Gyro = 3,
		Accel = 4,
		Gestures = 5
	};

	public class IOManager : parentManager {
		
//		this list contains references to all the input buttons
		public GameObject[] UIInputList;
//		Put button that will need combo's together with keyboard / other inputs
		public GameObject[] ComboList;
//		this has the keyboard inputs list
		public KeyCode[] keyboardList;
		
//		IO Manager Objects
		public inputObject[] inputList;
		public chainQue[] commandsList;
		public int seperator;
		public int levelManage;
		
			
		/// <summary>
		/// initInputs (Custom) funtion
		/// This function is called from gameManager during the Start function of a main Level. The objective is to combine both input UIBottons and keyboard arrays into one type of object array.
		/// The inputObject class is made for that, it stores the keycode or reference to the button so that we can trace it back, as well as an inputType reference.
		/// * Create array of inputObject in the size of ComboList + keyInputList arrays
		/// * For every UIInput create an input object with it and store in array
		/// * Mark seperation index number to idetify the first keyboard input index
		/// * For every keyInput create an input object with it and store in array
		/// * Save array into gameManager's inputList
		/// </summary>
		public void initInputs () {
			DebugLog("IOMan.InputsInitiated");
			inputObject[] iObjList = new inputObject[ComboList.Length + keyboardList.Length];
			for (int i = 0; i < iObjList.Length; i++) {
				if (i < ComboList.Length) {
					iObjList[i] = new inputObject(inputType.ButtonUI, ComboList[i]);
				} else {
					if (seperator == 0) {
						seperator = i+1;
					}
					iObjList[i] = new inputObject(inputType.Keyboard, keyboardList[i - ComboList.Length]); 
				}
			}
			inputList = iObjList;
		} 
			
		/// <summary>
		/// inputPress (Custom) funtion - Parameters (obj = the gameObject that called this function)
		/// inputRelease (Custom) funtion - Parameters (obj = the gameObject that called this function)
		/// These are called from UIButtonMessage Script attached to nGUI Buttons. 
		/// Just make sure to attach UIButtonMessage on any buttons and reference inputRelease / inputPress for the one that needs input.
		/// * If button is in ComboList then forward to gameManager (which will sperate processing based on currentLevel)
		/// * If not in List then send Error
		/// </summary>
		public void inputPress ( GameObject obj ) {
			DebugLog("IOMan.inputDown=" + obj);
			if (System.Array.IndexOf(ComboList, obj) != -1) {
//				GM.Get().inputProcessing(obj, true);
			} else {
				DebugLogError("Unknown Input Received, please identify GameObject \n\"" + obj + "\"");
			}
		}
		
		public void inputRelease ( GameObject obj ) {
			DebugLog("IOMan.inputUp=" + obj);
			if (System.Array.IndexOf(ComboList, obj) != -1) {
//				GM.Get().inputProcessing(obj, false);
			} else {
				DebugLogError("Unknown Input Received, please identify GameObject \n\"" + obj + "\"");
			}
		}
			
		/// <summary>
		/// processCommands (Custom) funtion - Parameters (iObj = inputObject of the event (either key or uibutton), i = indexNumber in GM.inpulList, state = up or down)
		/// This is the big one here, it takes input and processes them to identify if the input is part of a command (a command is a custom object that wraps around inputObject  
		/// or Objects ( so either can happen eg: keyboard  or UIButton ) to act similar to an event, it holds data for timing of the inputs, state = up / down, 
		/// happening = progress boolean for if it is occuring now and reference to the inputObject)
		/// The timing is done based on 3 variables timeTo(Trigger) (the time BEFORE which you have to press the NEXT button with reference to the previous input), 
		/// timedAt(time At Which It Was Triggered), timeFrom (basically charging time between down and up states)
		/// It also 'sets' ongoing inputs as triggered so that it can identify when a command is either ongoing or completed. The code gets pretty complicated but below is a basic steps taken.
		/// * For every commandQue in commandsList (a commandQue is a group or command Events, predetermined in the gameData)
		/// 	* For every commandObject in the commandQue
		///			* If Not Happening (if this commandQue has not occurred)
		/// 			* For every inputObject in the commandObject
		/// 				* Save the reference Object (keyInput or UI) to a un-typed variable
		///					* If the input received is the input in the inputObject (from the command) and the state (up/down is the same)
		/// 					* If not first item in the que
		///							* If timeTo is not 0 (means if timeTo is 0 we don't check for input time difference between the two inputs) 
		///							  AND timeTo is greater than the time of last command in que ( meaning it the press occurred before timeTo 'limit' was supassed )
		///							  AND button is down
		///								* If this is not the last command Object 
		///									* Set commandObject as happening and timed
		///								* Else If this is the last command Object
		///									* If didnt completely trigger a command yet
		///										* Forward triggered command to framework for triggering actual functions 
		///										* Clear the happening state of this que
		///										* Exit All 3 For Loops
		///									* Else If already triggered a command ( once a command is triggered, the same input event should not trigger another command )
		///										* Clear the happening state of this que
		///										* Exit All 3 For Loops
		///							* Else If timeTo is not 0 (means if timeTo is 0 we don't check for input time difference between the two inputs) 
		///							  AND timeTo is greater than the time of last command in que ( meaning it the release occurred before timeTo 'limit' was surpassed )
		///							  AND button is up
		///							  AND timeFrom is less than the time of the last command in the que ( meaning it the release occurred after timeFrom 'charge' was surpassed )
		///								* If this is not the last command Object 
		///									* Set commandObject as happening and timed
		///									* Exit the last two loops ( to continue checking if any other command has similar inputs, like Combo(A+A) and Combo (A+A+B) )
		///								* Else If this is the last command Object
		///									* If didnt completely trigger a command yet
		///										* Forward triggered command to framework for triggering actual functions 
		///										* Clear the happening state of this que
		///										* Exit All 3 For Loops
		///									* Else If already triggered a command ( once a command is triggered, the same input event should not trigger another command )
		///										* Clear the happening state of this que
		///										* Exit All 3 For Loops
		///							* Else If timeTo = 0 (meaning just no time checking required)
		///								* If this is not the last command Object 
		///									* Set commandObject as happening and timed
		///									* Exit the last loop
		///								* Else If this is the last command Object
		///									* If didnt completely trigger a command yet
		///										* Forward triggered command to framework for triggering actual functions 
		///										* Clear the happening state of this que
		///										* Exit All 3 For Loops
		///									* Else If already triggered a command ( once a command is triggered, the same input event should not trigger another command )
		///										* Clear the happening state of this que
		///										* Exit All 3 For Loops
		///							* Else if not in time (breaking the commandQue)
		///								* Clear the happening state of this que
		///								* Exit the last two loops ( to continue checking if any other command has similar inputs, like Combo(A+A) and Combo (A+A+B) )
		///						* If first item in que
		///							* If this is not the last command Object 
		///								* Set commandObject as happening and timed
		///								* Exit the last two loops ( to continue checking if any other command has similar inputs, like Combo(A+A) and Combo (A+A+B) )
		///							* Else If this is the last command Object
		///								* Forward triggered command to framework for triggering actual functions 
		///								* Exit the last two loops ( to continue checking if any other command has similar inputs, like Combo(A+A) and Combo (A+A+B) )
		///					* If not the input received
		///						* If previous commandObject is the same as this and state is altered (eg: last command was A(down) and now its A(Up)) we ignore this and exit Two Loops.
		///						* If next commandObject is the same as this and state is altered (eg: next command is A(uo) and now its A(Down)) we ignore this and exit Two Loops.
		///						* If not the same object then Clear the happening state of this que and exit Two Loops
		///			* If this is happening (command was performed before)
		///				* Exit the last two loops ( to continue checking if any other command has similar inputs, like Combo(A+A) and Combo (A+A+B) )
		///		* Loop Exiting Methods Here
		/// </summary>
		public void processCommands ( inputObject iObj, int i, bool state ) {
//			int j;
//			int k;
//			int l;
//			bool trigger = false;
//			bool check;
//			bool secondBreak;
//			bool thirdBreak;
//			GameObject switchObject;
//			var switchInput;
//			for (j = 0; j < gameManager.Get().commandsList.length; j++) { //for everycommandque in the commandsList (QUE[])
//				check = false;
//				thirdBreak = false;
//				for (k = 0; k < gameManager.Get().commandsList[j].commands.length; k++) { //for everycommand in the commandque
//					if (!gameManager.Get().commandsList[j].commands[k].happening) { 
//						// if ! happenning
//						secondBreak = false;
//						for (l = 0; l < gameManager.Get().commandsList[j].commands[k].command.length; l++) {
//							if (iObj.referenceObject == null) {
//								switchObject = gameManager.Get().commandsList[j].commands[k].command[l].referenceKey;
//								switchInput = iObj.referenceKey;
//							} else {
//								switchObject = gameManager.Get().commandsList[j].commands[k].command[l].referenceObject;
//								switchInput = iObj.referenceObject;
//							}
//							//DebugLog("CommandsList[" + j + "].commands[" + k + "].command[" + l + "]");
//							//DebugLog("Input=" + switchInput + "_ currentInputObject = " + switchObject);
//							//DebugLog("States = " + state + "__" + gameManager.Get().commandsList[j].commands[k].state );
//							if (switchInput == switchObject && state == gameManager.Get().commandsList[j].commands[k].state) {
//								//if this is the command recieved
//								if (check) {
//									//if  not first item in que
//									if ( gameManager.Get().commandsList[j].commands[k].timeTo >= Time.time-gameManager.Get().commandsList[j].commands[k-1].timedAt && gameManager.Get().commandsList[j].commands[k].timeTo != 0 && state == true) {
//										//if in time  Down (TAP TAP)
//										if ( k != gameManager.Get().commandsList[j].commands.length - 1) {
//											//if not last command in que
//											//DebugLog("if not last command in que");
//											gameManager.Get().commandsList[j].commands[k].timedAt = Time.time;
//											gameManager.Get().commandsList[j].commands[k].happening = true;
//											break;
//										} else {
//											//DebugLog("if last command in que");
//											//if last command in que
//											secondBreak = true;
//											thirdBreak = true;
//											if (!trigger) {
//												gameManager.Get().framework.cmdProcessing(j);
//												trigger = true;
//											}
//											clearQue(gameManager.Get().commandsList[j]);
//											break;
//										}
//									} else if ( gameManager.Get().commandsList[j].commands[k].timeTo >= Time.time-gameManager.Get().commandsList[j].commands[k-1].timedAt && gameManager.Get().commandsList[j].commands[k].timeFrom <= Time.time-gameManager.Get().commandsList[j].commands[k-1].timedAt && gameManager.Get().commandsList[j].commands[k].timeTo != 0 && state == false) {
//										//Key Up and TimeTo is != 0 (Charge)
//										//DebugLog("Key Up and TimeTo is != 0 (Charge)");
//										if ( k != gameManager.Get().commandsList[j].commands.length - 1) {
//											gameManager.Get().commandsList[j].commands[k].timedAt = Time.time;
//											gameManager.Get().commandsList[j].commands[k].happening = true;
//											secondBreak = true;
//											break;
//										} else {
//											secondBreak = true;
//											thirdBreak = true;
//											if (!trigger) {
//												gameManager.Get().framework.cmdProcessing(j);
//												trigger = true;
//											}
//											clearQue(gameManager.Get().commandsList[j]);
//											break;
//										}
//									} else if (gameManager.Get().commandsList[j].commands[k].timeTo == 0) {
//										//Key Down and TimeTo is 0 (SINGLE)
//										//DebugLog("Key Down and TimeTo is 0 ");
//										if ( k != gameManager.Get().commandsList[j].commands.length - 1) {
//											gameManager.Get().commandsList[j].commands[k].timedAt = Time.time;
//											gameManager.Get().commandsList[j].commands[k].happening = true;
//											break;
//										} else {
//											secondBreak = true;
//											thirdBreak = true;
//											if (!trigger) {
//												gameManager.Get().framework.cmdProcessing(j);
//												trigger = true;
//											}
//											clearQue(gameManager.Get().commandsList[j]);
//											break;
//										}
//									} else {
//										//DebugLog("if not in time" + gameManager.Get().commandsList[j].commands[k].timeTo + "=" + (Time.time-gameManager.Get().commandsList[j].commands[k-1].timedAt) + "STATE" + state);
//										//if not in time
//										clearQue(gameManager.Get().commandsList[j]);
//										secondBreak = true;
//										break;
//									}
//								} else {
//									//DebugLog("if first item in que");
//									//if first item in que
//									if ( k != gameManager.Get().commandsList[j].commands.length - 1) {
//										//DebugLog("if not last item in que"); 
//										//if not last item in que
//										gameManager.Get().commandsList[j].commands[k].timedAt = Time.time;
//										gameManager.Get().commandsList[j].commands[k].happening = true;
//										secondBreak = true;
//										break;
//									} else {
//										//DebugLog("if last item in que");
//										//if last item in que
//										if (!trigger) {
//											gameManager.Get().framework.cmdProcessing(j);
//											trigger = true;
//										}
//										secondBreak = true;
//										break;
//									}
//								}
//							} else {
//								//DebugLog("if not the command recieved ");
//								//if not the command recieved 
//								if (l > 0) {
//									if (iObj.referenceObject == null) {
//										switchObject = gameManager.Get().commandsList[j].commands[k].command[l-1].referenceKey;
//									} else {
//										switchObject = gameManager.Get().commandsList[j].commands[k].command[l-1].referenceObject;
//									}
//									if (switchInput == switchObject && !state && gameManager.Get().commandsList[j].commands[k].state) {
//										secondBreak = true;
//										break;
//									} 
//								}
//								if (l < gameManager.Get().commandsList[j].commands[k].command.length-1) {
//									if (iObj.referenceObject == null) {
//										switchObject = gameManager.Get().commandsList[j].commands[k].command[l+1].referenceKey;
//									} else {
//										switchObject = gameManager.Get().commandsList[j].commands[k].command[l+1].referenceObject;
//									}
//									if (switchInput == switchObject && state && !gameManager.Get().commandsList[j].commands[k].state) {
//										secondBreak = true;
//										break;
//									} 
//								}
//								if (switchInput != switchObject) {
//									clearQue(gameManager.Get().commandsList[j]);
//									secondBreak = true;
//								}
//							}
//						}
//					} else { 
//						//DebugLog("if happening ( cmd was recieved earlier )");
//						//if happening ( cmd was recieved earlier )
//						check = true;
//						secondBreak = false;
//					}
//					if (secondBreak) {
//						break;
//					}
//				}
//				if (thirdBreak) {
//					break;
//				}
//			}
		}
		
		/// <summary>
		/// clearQue (Custom) funtion - Parameters (que = the commandQue to clear)
		/// This simple function can clear the boolean 'happening' state of a commandQue, it should be called when a particular commandQue is finished or invalidated.
		/// * For each commandObject in commandQue, if happening is true set it to false.
		/// </summary>
//		function clearQue( que : commandQue ) {
//			var i : int;
//			for ( i = 0; i < que.commands.length; i++ ) {
//				if (que.commands[i].happening) {
//					que.commands[i].happening = false;
//				}
//			}
//		}
		
		/// <summary>
		/// addSpawnedInput (Custom) funtion - Parameters (newInput = the object to add to the inputlist)
		/// delSpawnedInput (Custom) funtion - Parameters (gObj = the object to remove from inputlist)
		/// Simple function to add / remove from the inputList, basically used for spawned inputs for example LevelEditor Buttons. so far called by nGUIReceiverSetter, and nGUITargetSetter.
		/// * Create a List with ComboList in it
		/// * Add / Remove the object to/from the list
		/// * Update ComboList with the updated List
		/// </summary>
//		function addSpawnedInput( newInput : GameObject, whichList : boolean ) {
//			DebugLog("IOMan.addSpawnedInput=" + newInput);
//			var tempArray : List.<GameObject>;
//			if (whichList) {
//				tempArray = new List.<GameObject>(ComboList);
//				tempArray.Add(newInput);
//				ComboList = tempArray.ToArray();
//			} else {
//				tempArray = new List.<GameObject>(UIInputList);
//				tempArray.Add(newInput);
//				UIInputList = tempArray.ToArray();
//			}
//		}
//		
//		function delSpawnedInput( gObj : GameObject, whichList : boolean ) {
//			DebugLog("IOMan.delSpawnedInput=" + gObj);
//			var tempArray : List.<GameObject>;
//			if (whichList) {
//				tempArray = new List.<GameObject>(ComboList);
//				tempArray.Remove(gObj);
//				ComboList = tempArray.ToArray();
//			} else {
//				tempArray = new List.<GameObject>(UIInputList);
//				tempArray.Remove(gObj);
//				UIInputList = tempArray.ToArray();
//			}
//		}
		
		/// <summary>
		/// Update (Unity) funtion
		/// This is called every frame to check for inputs from the keyboard, might remove this in the future to use event listeners. Check if input is pressed and pass to GM to process based on level.
		/// * For every input in keyInputsList
		/// 	* Check if key is down or up recieved, if so then pass to gameManager.
		/// </summary>
		void Update () {
			for (int i = 0; i < keyboardList.Length; i++) {
				if (Input.GetKeyDown(keyboardList[i])) {
					DebugLog("IOMan.keyDown=" + GM.Get() + keyboardList[i]);
//					GM.Get().inputProcessing(keyboardList[i], true);
				}
				if (Input.GetKeyUp(keyboardList[i])) {
					DebugLog("IOMan.keyUp=" + keyboardList[i]);
//					GM.Get().inputProcessing(keyboardList[i], false);
				}
			}
		}

	}

	//InputObject for all kinds of input
	/* Needs to add gestures, Key combinations gyro etc*/
	public class inputObject {
		
		public inputType iType;
		public GameObject referenceObject;
		public KeyCode referenceKey;
		
		public inputObject( inputType type, GameObject refObj) {
			iType = type;
			referenceObject = refObj;
		}		
		
		public inputObject( inputType type, KeyCode refKey) {
			iType = type;
			referenceKey = refKey;
		}	
	}

	//Input are receieved commands are performed from those inputs
	public class inputChain {
		public inputObject[] command;
		public float timeTo;
		public float timeFrom;
		public float timedAt;
		public bool state;
		public bool happening = false;
		
		public inputChain ( inputObject[] iO , float tA, float tF, bool uD ) {
			command = iO;
			timeTo = tA;
			timeFrom = tF;
			state = uD;
		}
	}

	public class chainQue {
		inputChain[] chain;
		
		public chainQue ( inputChain[] cO ) {
			chain = cO;
		}
	}
}
