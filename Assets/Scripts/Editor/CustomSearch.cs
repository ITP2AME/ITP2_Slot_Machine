
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
/// An Editor Tool Window to help search for objects on scene and for assets in the project. 
/// This is a work in progress.
/// </summary>
/// CHANGELOG: 
///	*	0.0.1: First Version
/// TODO: N/A (place holder)
/// <contents>
/// OnGUI () 
/// Init ()
/// </contents>

using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

public class CustomSearch : EditorWindow {
	
	// position of scroll view
	private Vector2 scrollPos;
	public bool SFGameObj;
	public bool SFPrefab;
	public bool SFOfType;
	public bool SFComponent;
	public bool SFComponentWithX;
	public bool SIProject;
	public bool SIHierarchy;
	public string SFName;
	public string SFVarName;
	public string SFVarVal;
	public string SFType;

	void OnGUI()
	{
		// start the scroll view
		scrollPos = EditorGUILayout.BeginScrollView (scrollPos);

		// store if the GUI is enabled so we can restore it later
		bool guiEnabled = GUI.enabled;
		
		SIHierarchy = EditorGUILayout.ToggleLeft ("SearchInHierarchy", SIHierarchy); 
		SIProject = EditorGUILayout.ToggleLeft ("SearchInProject", SIProject); 

		if (SIHierarchy && !SIProject ) {
			SFGameObj = EditorGUILayout.ToggleLeft ("SearchForGameObject", SFGameObj);
		}

		if ( SIProject && !SIHierarchy ) {
			SFPrefab = EditorGUILayout.ToggleLeft ("SearchForPrefab", SFPrefab);
			SFOfType = EditorGUILayout.ToggleLeft ("SearchForOfType", SFOfType);
		}

		if ( ( SFPrefab && SIProject && !SIHierarchy) || ( SFGameObj && !SIProject && SIHierarchy) ) {
			SFComponent = EditorGUILayout.ToggleLeft ("SearchForComponent", SFComponent);
			if (SFComponent) {
				SFComponentWithX = EditorGUILayout.ToggleLeft ("SearchForComponentWithX", SFComponentWithX); 
			}
		}

		SFName = EditorGUILayout.TextField ("SearchForName", SFName);
		if (SFComponentWithX) {
			SFVarName = EditorGUILayout.TextField ("SearchForVariable", SFVarName);
			SFVarVal = EditorGUILayout.TextField ("SearchForValue", SFVarVal);
		}
		if (SFOfType) {
			SFType = EditorGUILayout.TextField ("SearchForTypeName", SFType);
		}

		if (GUILayout.Button ("Search")) {
			if (SIHierarchy && !SIProject) {
				if (SFOfType) {
//					Selection.objects = FindObjectsOfType(Type.GetType(SFOfType))().ToArray();
				} else {

				}
			} else if (SIProject && !SIHierarchy) {
//				Selection.objects = FindObjectsOfType<MeshCollider>().Where(mc => mc.isTrigger && !mc.convex).Select(mc => mc.gameObject).ToArray();
			} else if (SIProject && SIHierarchy) {
//				Selection.objects = FindObjectsOfType<MeshCollider>().Where(mc => mc.isTrigger && !mc.convex).Select(mc => mc.gameObject).ToArray();
//				Selection.objects = Resources.FindObjectsOfTypeAll(Type.GetType("Mathf") );
			} else {
				Debug.LogWarning("You did not select where to search! \n -- Meshadieme/EditorToolWindows/CustomSearch");
			}
		}

		// restore the GUI
		GUI.enabled = guiEnabled;
		
		// close the scroll view
		EditorGUILayout.EndScrollView();
	} 

	[MenuItem("Meshadieme/EditorToolWindows/CustomSearch")]
	static void Init()
	{
		// get the window, show it, and hand it focus
		var window = EditorWindow.GetWindow<CustomSearch>("CustomSearch", false);
		window.Show();
		window.Focus();
	}
}
