
//----------------------------------------
//		Unity3D Template Script
// 	   Coded by Lord Meshadieme
// 	   skype : lord_meshadieme
//----------------------------------------

/// <version>
/// 0.0.1
/// </version>
/// <summary>
/// For use in OM Entertainment (Chariot Wars)
/// 
/// A Container Class to store a collection of scene references, and display in the editor window using the serializedobject/property fields.
/// </summary>
/// CHANGELOG: 
///	*	0.0.1: First Version
/// TODO: N/A (place holder)
/// <contents>
/// OnGUI ()
/// </contents>

#if UNITY_EDITOR
using UnityEngine; 
using System.Collections;
using System;

[Serializable]
public class SceneContainer : ScriptableObject {
	
	[SerializeField]
	public UnityEngine.Object[] Scenes;// = new LevelStorage[1];
	
	public void OnGUI ()
	{
		//levels.OnGUI ();
	}
}
#endif