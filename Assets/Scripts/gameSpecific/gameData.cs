//----------------------------------------
//		Unity3D Games Template (C#)
// Copyright © 2015 Lord Meshadieme
// 	   skype : lord_meshadieme
//----------------------------------------

/// <version>
/// 0.5.0
/// </version>
/// <summary>
/// game Data attached with every GameManager (GM) Script. (previously included in framework / game specific framework name)
/// This manager is going to have all the hard coded data like locations, dialogue, story details, spawner setting (might remove this later), commandQues, database etc..
/// Most of this is called during the start of the scene and not after that.
/// </summary>
/// CHANGELOG:
///	*	0.4.0: JS Template Base
///	*	0.5.0: C# Template Base
/// TODO: Implement Game Specific Code Here
/// <contents>
/// initLevel2Data (int) 
/// initLevelData (int)
/// initcommandQues () 
/// initStory () 
/// initSpawners (GameObject)
/// initGameData ()
/// </contents>


using Meshadieme;
using UnityEngine;
using System.Collections;

namespace Meshadieme {
	public class gameData : parentManager {
		
		public virtual void initGameData() {

//			initSaveGame();
			//Other Game Data = ScriptableObjects / Custom Classes / Character Stats etc.
		}

		public virtual void initSaveGame() {
//			if (PlayerPrefs.HasKey("music")) {
//				music = PlayerPrefsX.GetBool("music");
//			} else {
//				PlayerPrefsX.SetBool("music", music);
//			}
//

//			saveGame();
		}

		public virtual void resetSave () {
//			music = true;

			
//			saveGame();
		}

		public virtual void saveGame () {
//			PlayerPrefsX.SetBool("music", music);

//			PreviewLabs.PlayerPrefs.Flush();
		}
		
	}
}
