//----------------------------------------
//		Unity3D Games Template (C#)
// Copyright © 2015 Lord Meshadieme
// 	   skype : lord_meshadieme
//----------------------------------------

/// <version>
/// 0.5.0
/// </version>
/// <summary>
/// Level Manager and Level Editor attached with every Game Manager Script.
/// This manager can do both 2D and 3D levels based on the gameType variable in gameManager
/// Keep any downcast related functions here avoid making thing complicated. 
/// </summary>
/// CHANGELOG: 
///	*	0.1.0: Mantera Version Tree
///	*	0.2.0: STD Version Tree
///	*	0.3.0: HitBoar Version Tree
///	*	0.4.0: JS Template Base
///	*	0.4.01: Added zOffset for levelObject spawners to avoid collision with background objects
///	*	0.5.0: C# Template Base
/// TODO: Improve 3D level Editor, Loading and Spawning prefab.
/// TODO: Rework Enemy Spawning variables to lists.
/// TODO: Rework the Level Pieces Management for 3D games.
/// TODO: Particle management stuff either here or in its own manager.
/// <contents>
/// initLevel (int, Transform) 
/// init2DLevel (int, Transform)
/// loadLevel (int) 
/// OnSubmit () 
/// Update () 
/// inputs (GameObject) 
/// inputs (KeyCode, boolean) 
/// generate (int, boolean) 
/// newHighlight (int, int)
/// swap ()
/// spawner (locatorObject[], GameObject[], GameObject[])
/// addToRef (GameObject)
/// delToRef (GameObject)
/// returnLevel (levelData, int) 
/// return2DLevel (level2Data) 
/// return2DObj (level2Data) 
/// returnCurrentData () 
/// returnCurrent2Data () 
/// </contents>


//Imports
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Meshadieme;

namespace Meshadieme {

	public class levelManager : parentManager {
		//Pieces / Chunks of the level itself
		GameObject[] levelPiecesPrefabs;
		GameObject[] levelPiecesSpawned;
//		locatorObject[] levelPieces3D;
		int[,] levelPieces2D;
		
		//Other objects on the level like spawners obstacles collectables etc..
		GameObject[] levelObjectsPrefabs;
		GameObject[] levelObjectsSpawned;
//		locatorObject[] levelObject3D;
		
		//Reference object that dont fall into any category, like UI items or spawning parent locations.
		GameObject[] referenceObject;
		
		//Level Editor Global Vars
		public bool levelEditMode = false;
		GameObject levelEditorPrefab ;
		int levelEditorCamIndex;
		//int levelToLoad = 0;
		//bool loadLevelNow = false;
		//bool togglePrintOut = false; //toggle this to print out the variable list to put in gameManager
		
		// Private Level Editor Vars
		private GameObject[,] levelEdit2DTiles;
		private int[,] levelEdit2DNums;
//		private levelData levelEditLoaded;
//		private level2Data levelEdit2DLoaded;
		//private int xSize = 1;
		//private int ySize = 1;
		//private int nWidth = 64;
		//private int nHeight = 64;
		//private int numOfTiles;
		//private int selectX = 0;
		//private int selectY = 0;
		
		//Current level loaded objects
//		private var currentLevelData : levelData;
//		private var currentLevel2Data : level2Data;
		
		//Miscellaneous level object references
//		var particlePrefabs : GameObject[];
//		var enemyPrefabs : GameObject[];
//		var enemySpawned : List.<GameObject> = new List.<GameObject>();
		
		//Spawning Manager for 3D memory control
//		var manageSpawn : boolean = false;
//		var minX : float = 1000;
//		var minY : float = 1000;
//		var minZ : float = 1000;
//		var zOffset : float = -100;

	}

}
