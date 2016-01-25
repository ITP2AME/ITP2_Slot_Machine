//----------------------------------------
//		Unity3D Games Template (C#)
// Copyright © 2015 Lord Meshadieme
// 	   skype : lord_meshadieme
//----------------------------------------

/// <version>
/// 0.2.0
/// </version>
/// <summary>
/// A Parent Class for most of our scripts,
/// To make it easier to manage debugging code and modify debug output as needed.
/// Aswell as avoiding debug messages is a non-development build.
/// </summary>
/// CHANGELOG: 
///	*	0.1.0: JS Game Template Base
///	*	0.2.0: C# Game Template Base
/// TODO: Consolidate Debug Toggles in Editor for easiers access (C#)
/// 

using Meshadieme;
using UnityEngine;
using System.Collections;

namespace Meshadieme {
	public class sceneManager : parentManager {
		
		public GameObject GMPrefab;
		public GameObject GMInstance;
		public Scenes thisScene;
		public GameObject[] buttonRefs;
		public GameObject[] prefabRefs;
		public GameObject[] miscRefs;
		public bool debugOn;

		protected override void Awake () {
			Debug.Log("SM_Awake()");

			if (GM.Get () == null) GMInstance = Instantiate ( GMPrefab ) as GameObject;
			if (GM.Get ().scene == null) GM.Get ().scene = GameObject.FindGameObjectWithTag("_SM").GetComponent<sceneManager>();

			base.Awake(); // Call Parent Awake
		}

		public int getButtonIndex (GameObject gObj) {
			return System.Array.IndexOf(buttonRefs, gObj);
		}

		public void inputProcessing (GameObject go) {
			GM.Get ().inputProcessing (go);
		}
		
	}
}