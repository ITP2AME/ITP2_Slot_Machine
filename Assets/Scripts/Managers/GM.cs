//----------------------------------------
//		Unity3D Games Template (C#)
// Copyright © 2015 Lord Meshadieme
// 	   skype : lord_meshadieme
//----------------------------------------


/// <version>
/// 0.6.0
/// </version>

/// <summary>
/// Singleton Referenced gameManager
/// Attach this to a game object and prefab it, then reference the prefab in SceneManager.
/// Keep any downcast related functions here avoid making things complicated.
/// This is the script you reference to get any
/// other sort of information from outside the 
/// particular script you are in. Example usage below.
/// <example>
/// GM.Get().someVariableOrFunctionHere.
/// </example>
/// </summary>
/// CHANGELOG: 
///	*	0.1.0: Mantera Version Tree
///	*	0.2.0: STD Version Tree
///	*	0.3.0: HitBoar Version Tree
///	*	0.4.0: JS Template Base
///	*	0.4.0: C# Template Base
///	*   0.5.0: Chariot Wars Version Tree
///	*   0.6.0: Cleaning for Goldsmith Projects
/// <contents>
/// Get () 
/// Awake ()
/// Start () 
/// setAtlas () 
/// moveLocalObj (GameObject, Vector3, Vector3, Vector3) 
/// moveLocalObj (GameObject, Vector3) 
/// moveObj (GameObject, Vector3, Vector3, Vector3) 
/// moveObj (GameObject, Vector3) 
/// destroyAllChild (GameObject)
/// destroyAfter (Object) 
/// inputProcessing (GameObject, boolean) 
/// inputProcessing (KeyCode, boolean) 
/// </contents>

//Imports
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Meshadieme;
using UnityEngine.SceneManagement;

namespace Meshadieme
{

    public class GM : parentManager
    {


        public enum gVersion
        {//Game Version to identify what atlas to load
            Standard = 0,
            HighDefinition = 1,
        }

        public enum gType
        {//Game Types just for identifying different applicable mechanics
            Basic3D = 0,
            Side2D = 1,
            Top2D = 2,
        }

        static GM gameManager; //Static GameManager Object (accessible by all)
        //static bool gamePause = false;
        //static gType gameType = gType.Side2D;
        //static gVersion gameVersion = gVersion.Standard;

        [HideInInspector]
        public parentManager[] managers;
        public debugToggle[] debug;
        public bool debugToggles;
        public bool fillManagers;
        private Scenes currentScene = Scenes.MAIN;
        [HideInInspector]
        public Scenes nextScene = Scenes.MAIN;
        public bool readyScene = false;

        //Various Manager Scripts
        public sceneManager scene;
        //[HideInInspector]
        public slotsGF framework;
        //[HideInInspector]
        public slotsGD data;
        //[HideInInspector]
        //public slotsGM menus;
        [HideInInspector]
        public IOManager IOMan;
        //[HideInInspector]
        //public levelManager levelMan;
        //	var collision : gameCollision;
        //	var audioMan : soundManager;
        //	var storyMan : storyManager;
        //	var charMan : characterManager;
        //	var camMan : cameraManager;
        //	
        //	//Atlas's
        //UIAtlas[] refAtlas;
        //UIAtlas[] HDSRAtlas;
        //UIAtlas[] HDHRAtlas;
        //UIAtlas[] SDSRAtlas;
        //UIAtlas[] SDHRAtlas;

        //Script Usage Toggles
        public bool useAtlas = false;
        public bool useStory = false;
        public bool useHero = false;
        public bool useChar = false;
        public bool useAI = false;
        public bool useIChains = false;
        public bool useLevelMan = false;
        public bool deleteSave = false;
        public bool debugSaveMode = true;

        //Game World Vals
        // N/A

        //On callBack function for other scripts to reference theGameManager.
        public static GM Get()
        {
            return gameManager;
        }


        /// <summary>
        /// Awake (Unity) function
        /// This is called FIRST, you may have to increase priority to load this in the Script 
        /// Execution Order (already done in template project). Below are goals of this function:
        /// * Verify attached files to game Manager for referencing other managers
        ///   other sort of information from outside the particular script you are in.
        /// * Identify and store the current device / OS that the game is running on and store it.
        /// * Update Scene To Load Next, This is used for development to quickly change the first scene when you hit play.
        /// * If this is not Unity Editor make sure levelEditMode is false, to avoid problems where wrong states are called (like input processing sent to levelEditor)
        /// </summary>
        protected override void Awake()
        {
            Debug.Log("GM_Awake()");
            gameManager = GetComponent<GM>() as GM;

            //if (useAtlas) setAtlas();
            if (scene == null) scene = GameObject.FindGameObjectWithTag("_SM").GetComponent<sceneManager>();

            #if UNITY_EDITOR
            if (scene.thisScene != currentScene)
            {
                currentScene = scene.thisScene;
            }
            if (scene.debugOn)
            {
                framework.gMode = (int)GameMode.Slots;
            }
            #else
			//levelMan.levelEditMode = false;
            #endif
            DontDestroyOnLoad(this.gameObject);
            base.Awake(); // Call Parent Awake
        }


        /// <summary>
        /// Start (Unity) function
        /// This function is called SECOND, things to do when starting a new scene:
        /// * SaveGame Data on new scenes
        /// * load Game Database
        /// * Instantiate Background music if needed (if not still alive from previous scene)
        /// * Run Scene specific spawning / inititate game functions from framework/level/data etc.
        /// * If loading scene then load next scene
        /// * If menu scenes or any other then initMenu and initInputs
        /// * If Level scene then:
        /// * 	- If should spawn hero, Spawn Hero and init Hero and move him
        /// * 	- Initiate Inputs (all possible individual inputs, buttons, keypress etc..)
        /// * 	- Initiate Commandslist from framework (all possible predefined combinations of inputs ( down and up ), defined in framework)
        /// * 	- Set GUI
        /// * 	- Spawn level from level data
        /// * 	- Any extra game specific stuff to do (UI spawning etc..) in framework.initGame
        /// * 	- Spawn objects from level data
        /// </summary>
        void Start()
        {

            //if (debugToggles)
            //{
            //    for (int i = 0; i < debug.Length; i++)
            //    {
            //        Debug.Log("DebugToggle[" + i + "] = " + debug[i] + " / " + managers[i]);
            //    }
            //}

            if (currentScene != Scenes.MAIN)
            {
                data.initGameData();
                //audioMan.initSound();
                if (useIChains) IOMan.initInputs();
                #if UNITY_EDITOR
                if (!debugSaveMode) data.saveGame();
                #else
				data.saveGame();
                #endif
            }

            Debug.Log("GM_Start_SCENE = " + currentScene);
            secondStart();
        }

        
        //initiating scenes by type
        private void secondStart()
        {
            switch (currentScene)
            {
                case Scenes.MAIN:
                    Debug.Log("Menu Init = " + currentScene + framework);
                    framework.gMode = GameMode.Slots;
                    framework.loadSelectedGame();
                    break;
            }

        }

        public IEnumerator loadingWait()
        {
            yield return new WaitForSeconds(0.01f);
            yield return new WaitForEndOfFrame();
        }

        public IEnumerator loadLevelAsync()
        {
            AsyncOperation async;
            Debug.Log("LoadLevelAsync = " + (int)nextScene + " = " + nextScene.ToString());
            #if UNITY_EDITOR
            if (!debugSaveMode) data.saveGame();
            #else
				data.saveGame();
            #endif
            async = SceneManager.LoadSceneAsync(nextScene.ToString());
            yield return async;
            currentScene = nextScene;
            secondStart();
        }

        public void loadLevel()
        {
            Debug.Log("LoadLevel = " + (int)Scenes.MAIN + " = " + Scenes.MAIN.ToString());
            #if UNITY_EDITOR
            if (!debugSaveMode) data.saveGame();
            #else
				data.saveGame();
            #endif
            SceneManager.LoadScene((int) Scenes.MAIN);
            //			yield return new WaitForSeconds(0.1f);
            currentScene = Scenes.MAIN;
            secondStart();
        }

        /// <summary>
        /// Populate Managers (Custom) function
        /// This is called from the Awake function, it will load up all the managers attached to this Object and the Scene Manager Object.
        /// </summary>
        public void populateManagers()
        {
            DebugLog("GM_Pop()");
            List<parentManager> list = new List<parentManager>();
            List<debugToggle> toggles = new List<debugToggle>();
            gameFramework newfw;
            //gameMenus newgm;
            gameData newgd;
            //IOManager newio;
            foreach (parentManager joint in gameObject.GetComponents<parentManager>())
            {
                list.Add(joint);
                joint.debugMode = new debugToggle();
                joint.debugMode.named = joint.ToString();
                toggles.Add(joint.debugMode);
                newfw = joint as gameFramework;
                //newgm = joint as gameMenus;
                newgd = joint as gameData;
                //newio = joint as IOManager;
                if (newfw != null)
                {
                    framework = newfw as slotsGF;
                }
                //else if (newgm != null)
                //{
                //    menus = newgm as slotsGM;
                //}
                else if (newgd != null)
                {
                    data = newgd as slotsGD;
                }
                //else if (newio != null)
                //{
                //    IOMan = newio;
                //}
            }
            managers = list.ToArray();
            debug = toggles.ToArray();
        }

        /// <summary>
        /// Set Atlas (Custom) function
        /// This is called from the Awake function, it will Validate the Atlas's in the arrays and 
        /// apply a suitable atlas. Below are goals of this function:
        /// * Determine aspect ratio
        /// * In HD version if aspect ratio is SD post DebugWarning for (possibility to warn player about version) and vice versa for SD version.
        /// * In HD version is High DPI and screen size is large then use 1080p otherwise use 720p
        /// * In SD version is High DPI and screen size is large then use 1536p otherwise use 768p
        /// * Validate Atlas quantity (ref / SR / HR)
        /// </summary>
        //void setAtlas()
        //{
        //    int i;
        //    float aspectRatio = Screen.width / Screen.height;
        //    if (gameVersion == gVersion.Standard)
        //    {//Standard Definition
        //        if (aspectRatio > 1.45)
        //        {
        //            DebugLogWarning("We have detected your device is using a Screen more compatible with the HighDefinition version of this Game/App, for a better experience consider installing the appropriate version (Do not show this again) ETC...");
        //        }
        //        //A bit higher aspect ratio than 4:3 which is 1.33
        //        //Used mainly for 4:3 (iPhone3/4/iPad1/2/3) with all Atlas's at 4:3!
        //        if (refAtlas.Length != SDHRAtlas.Length && refAtlas.Length != SDSRAtlas.Length)
        //        {
        //            DebugLogError("Atlas Validation Error! \n Please confirm all the atlas's are in the gameManager Arrays, the quantity does not match.");
        //        }
        //        if (Screen.dpi > 215 && Screen.height > 1000)
        //        {
        //            //Artwork is 2048x1536 (SDHR)
        //            DebugLog("GM_Atlas=SDHR");
        //            for (i = 0; i < refAtlas.Length; i++)
        //            {
        //                refAtlas[i].replacement = SDHRAtlas[i];
        //            }
        //        }
        //        else {
        //            //Artwork is 1024x768 (SDSR)
        //            DebugLog("GM_Atlas=SDSR");
        //            for (i = 0; i < refAtlas.Length; i++)
        //            {
        //                refAtlas[i].replacement = SDSRAtlas[i];
        //            }
        //        }
        //    }
        //    else if (gameVersion == gVersion.HighDefinition)
        //    {// High Definition
        //        if (aspectRatio < 1.45)
        //        {
        //            DebugLogWarning("We have detected your device is using a Screen more compatible with the Standard version of this Game/App, for a better experience consider installing the appropriate version (Do not show this again) ETC...");
        //        }
        //        //This aspect ratio range is valid for 3:2, 5:3, 16:9, 16:10 and others in between.
        //        //Used mainly for 16:9 (1080p/720p) with all Atlas's at 16:9!
        //        if (refAtlas.Length != HDHRAtlas.Length && refAtlas.Length != HDSRAtlas.Length)
        //        {
        //            DebugLogError("Atlas Validation Error! \n Please confirm all the atlas's are in the gameManager Arrays, the quantity does not match.");
        //        }
        //        if (Screen.dpi > 215 && Screen.height > 1000)
        //        {
        //            //Artwork is 1920x1080 (HDHR)
        //            DebugLog("GM_Atlas=HDHR");
        //            for (i = 0; i < refAtlas.Length; i++)
        //            {
        //                refAtlas[i].replacement = HDHRAtlas[i];
        //            }
        //        }
        //        else {
        //            //Artwork is 1280x720 (HDSR)
        //            DebugLog("GM_Atlas=HDSR");
        //            for (i = 0; i < refAtlas.Length; i++)
        //            {
        //                refAtlas[i].replacement = HDSRAtlas[i];
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Move Local Object (Custom, UTILITY) function - Parameters (gObj = Object to move, To = Position to move it, ToOrient = Rotation to apply, ToScale = Scale to apply)
        /// Move Local Object (Custom, UTILITY) function - Parameters (gObj = Object to move, To = Position to move it)
        /// Move Object (Custom, UTILITY) function - Parameters (gObj = Object to move, To = Position to move it, ToOrient = Rotation to apply, ToScale = Scale to apply)
        /// Move Object (Custom, UTILITY) function - Parameters (gObj = Object to move, To = Position to move it)
        /// This is a Utility function for global easy access, helps translate scale 
        /// and/or rotate gameObjects Locally and Globally.
        /// </summary>
        void moveLocalObj(GameObject gObj, Vector3 To, Vector3 ToOrient, Vector3 ToScale)
        {
            gObj.transform.localPosition = To;
            gObj.transform.localEulerAngles = ToOrient;
            gObj.transform.localScale = ToScale;
        }

        void moveLocalObj(GameObject gObj, Vector3 To)
        {
            gObj.transform.localPosition = To;
        }

        void moveObj(GameObject gObj, Vector3 To, Vector3 ToOrient, Vector3 ToScale)
        {
            gObj.transform.position = To;
            gObj.transform.eulerAngles = ToOrient;
            gObj.transform.localScale = ToScale;
        }

        void moveObj(GameObject gObj, Vector3 To)
        {
            gObj.transform.position = To;
        }

        /// <summary>
        /// Destroy All Children (Custom, UTILITY) function
        /// This is another Utility function for global easy access, helps Destroy all children
        /// of a gameObject without having reference to any of the children.
        /// </summary>
        void destroyAllChild(GameObject theParent)
        {
            foreach (Transform child in theParent.transform)
            {
                Transform typedChild = child as Transform;
                Destroy(typedChild.gameObject);
            }
        }

        /// <summary>
        /// destroyAfter (Custom, UTILITY) funtions - Parameters (tempPos = the object to destroy)
        /// This is a simple function to destroy an object after a while to "wait" for it to instantiate properly and do its awake function or something similar.
        /// * Wait for a short time and destroy object
        /// </summary>
        IEnumerator destroyAfter(Object tempPos)
        {
            yield return new WaitForSeconds(0.001f);
            Destroy(tempPos);
        }

        /// <summary>
        /// Input Processing (Custom) function - Parameters (gObj = UIButton Object, state = Up or Down (true/false))
        /// Input Processing (Custom) function - Parameters (kC = KeyCode is Unity key class, state = Up or Down (true/false))
        /// This is called from the IOManager, Once a SINGLE input is received (is UI Button press or release, or keyboard press or release). 
        /// This will forward the input to the appropriate processing Function for both (currently only Keyboard and UI Buttons) types of Input, function breakdown below:
        /// * If levelEdit Mode is Activated send to LevelManager's inputs function.
        /// * If current scene is Normal Level Scene send to IOManager Command Processing function. (commands are pre-defined sequences of inputs)
        /// * If scene is any other scene (logically by current design it should be a menu scene of some kind) so send to gameMenus input Processing.
        /// </summary>
        public void inputProcessing(GameObject go)
        {
            Debug.Log("GM_InputProc() = " + go);
            if (currentScene == Scenes.MAIN)
            {
                //// Out of game scenes (all considered menus)
                //menus.inputProcessing(currentScene, button);
                if (useIChains) {
                    //IOMan.processCommands(currentScene, scene.getButtonIndex(go), true);
                } else {
                    framework.procCmds(scene.getButtonIndex(go));
                }
            }
            //else {
            //    // In menu scenes
            //    menus.inputProcessing(currentScene, button);
            //    
            //}
        }

        //		public void keyProcessing (KeyCode kC, bool state ) {
        //			if (currentScene == Scenes.MAIN_MENU) {
        //				// Out of game scenes (all considered menus)
        //				menus.inputProcessing ( currentScene, kC );
        //			} else {
        //				// In game scenes
        //				if (useIChains) {
        ////					IOMan.processCommands(currentScene, button);
        //				} else {
        ////					framework.processCommands(currentScene, button);
        //				}
        //			}
        //			for (int i = IOMan.seperator-1; i < IOMan.inputList.Length; i++) {
        //				if (kC == IOMan.inputList[i].referenceKey) {
        //					if (useLevelMan && levelMan.levelEditMode) {
        //						DebugLog("GM_InputKEY=level");
        //						levelMan.inputs ( kC, state );
        //					} else if (currentScene == Scenes.) {
        //						DebugLog("GM_InputKEY=main");
        //						IOMan.processCommands(IOMan.inputList[i], i, state);
        //					} else {
        //						DebugLog("GM_InputKEY=menu");
        //						menus.inputProcessing ( i, currentScene );
        //					}
        //					break;
        //				}
        //			}
        //		}
    }
}