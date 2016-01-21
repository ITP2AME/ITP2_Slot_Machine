
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
/// An Editor Tool Window to help to perform actions on a large number of selected objects, like replace parent prefabs or rename etc.
/// This is a work in progress.
/// </summary>
/// CHANGELOG: 
///	*	0.0.1: First Version
/// TODO: N/A (place holder)
/// <contents>
/// OnGUI () 
/// performMode ()
/// setMode ()
/// Init ()
/// </contents>

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;


public class CustomSelection : EditorWindow {

	enum sMode
	{
		REPLACE = 0,
		FILTER = 1,
		ADD_AS_CHILD = 2,
		RENAME = 3,
		SET_TO_SINGLE_PREFAB = 4
	}

	enum renameMode
	{
		SAME_NAME = 0,
		NAME_NUMBER = 1,
		NUMBER_FROM = 2
	}

	// position of scroll view
	private Vector2 scrollPos;
	private GameObject obj = null;
	private sMode sm = sMode.REPLACE;
	private renameMode rm = renameMode.SAME_NAME;
	private string newName = "";
	private int num = 0;
	
	void OnGUI()
	{
		// start the scroll view
		scrollPos = EditorGUILayout.BeginScrollView (scrollPos);

		// store if the GUI is enabled so we can restore it later
		bool guiEnabled = GUI.enabled;

		sm = (sMode) EditorGUI.EnumPopup ( EditorGUILayout.GetControlRect(), "Selection Mode:", sm);
			setMode ();
		
		if(GUI.Button(EditorGUILayout.GetControlRect(), "DO IT!"))
			performMode();

		// restore the GUI
		GUI.enabled = guiEnabled;
		
		// close the scroll view
		EditorGUILayout.EndScrollView();
	} 

	void performMode () {
		Transform[] tempTrans;
		List<Transform> trans;
		GameObject[] tempObj;
		int i;
		UnityEngine.Object prefab;
		Transform cache;
		GameObject go;
		GameObject go2;
		PrefabType pref;
		switch (sm) {
			case sMode.REPLACE:
				if (obj) {
					tempTrans = Selection.GetTransforms( SelectionMode.Editable | SelectionMode.TopLevel );
					//Set to GameObject List
					i = 0;
					prefab = (UnityEngine.Object) obj;
					pref = PrefabUtility.GetPrefabType(obj);
					foreach(Transform newTrans in tempTrans) {
						//Setting Old GO
						go2 = newTrans.gameObject;
						//Instantiate new GO
						if (pref == PrefabType.PrefabInstance) {
							go = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
							PrefabUtility.SetPropertyModifications(go, PrefabUtility.GetPropertyModifications(prefab));
						} else if (pref == PrefabType.Prefab || pref == PrefabType.ModelPrefab) {
							go = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
						} else {
							go = (GameObject)Instantiate(prefab);
						}         
						Undo.RegisterCreatedObjectUndo(go, "created prefab");
						//Copy old GO to new GO
						go.transform.parent = go2.transform.parent;
						go.transform.position = go2.transform.position;
						go.transform.localScale = go2.transform.localScale;
						go.transform.rotation = go2.transform.rotation;
						go.name = go2.name;
						Undo.DestroyObjectImmediate(go2);
					}
					Debug.Log ("All Objects overwritten by Prefab! \n -- Meshadieme/EditorToolWindows/CustomSelection");
				} else {
					Debug.LogWarning ("No Prefab Selected! \n -- Meshadieme/EditorToolWindows/CustomSelection");
				}
				break;
			case sMode.FILTER:
				break;
			case sMode.ADD_AS_CHILD:
				break;
		case sMode.RENAME:
				tempTrans = Selection.GetTransforms( SelectionMode.Editable | SelectionMode.TopLevel );
				trans = new List<Transform>(tempTrans);
				trans.Sort(new SortByHierarchy());
				//Set to GameObject List
				switch (rm) {
					case renameMode.NAME_NUMBER:
						num = 0;
						foreach(Transform newTrans in trans) {
							newTrans.gameObject.name = name + num;
							num++;
						}
						break;
					case renameMode.NUMBER_FROM:
						foreach(Transform newTrans in trans) {
							newTrans.gameObject.name = name + num;
							num++;
						}
					break;
					case renameMode.SAME_NAME:
						foreach(Transform newTrans in trans) {
							newTrans.gameObject.name = name;
						}
						break;
				}
				break;
			case sMode.SET_TO_SINGLE_PREFAB:
				if (obj) {
					tempTrans = Selection.GetTransforms( SelectionMode.TopLevel );
					//Set to GameObject List
					tempObj = new GameObject[tempTrans.Length];
				    i = 0;
					foreach(Transform newTrans in tempTrans) {
						tempObj[i] = newTrans.gameObject;
						i++;
					}
					//Loop and add to prefab
					prefab = (UnityEngine.Object) obj;
					cache = new GameObject().GetComponent<Transform>();
//					foreach (GameObject go in tempObj) {
//						name = go.name;
//						cache.localPosition = go.transform.localPosition;
//						cache.localScale = go.transform.localScale;
//						cache.localRotation = go.transform.localRotation;
//						//go.name = prefab.name;
//						//PrefabUtility.DisconnectPrefabInstance(go);
//						prefab = PrefabUtility.ReplacePrefab(go, prefab, ReplacePrefabOptions.ReplaceNameBased);
//						go.name = name;
//						go.transform.localPosition = cache.localPosition;
//						go.transform.localScale = cache.localScale;
//						go.transform.localRotation = cache.localRotation;
//					}
					DestroyImmediate (cache.gameObject);
					Debug.Log ("All Objects Connected to Prefab! \n -- Meshadieme/EditorToolWindows/CustomSelection");
					//Selection.objects = ;
				} else {
					Debug.LogWarning ("No Prefab Selected! \n -- Meshadieme/EditorToolWindows/CustomSelection");
				}
			    break;
		}
	}

	void setMode() {
		GameObject pObj;
		switch (sm) {
			case sMode.REPLACE:
				pObj = (GameObject) EditorGUI.ObjectField(EditorGUILayout.GetControlRect(),"Replace with : ",obj,typeof(GameObject), true);
				if (pObj != null && (PrefabUtility.GetPrefabType(pObj) == PrefabType.Prefab || PrefabUtility.GetPrefabType(pObj) == PrefabType.PrefabInstance || PrefabUtility.GetPrefabType(pObj) == PrefabType.ModelPrefab)) {
					obj = pObj;
				} else {
					obj = null;
				}
				break;
			case sMode.FILTER:
				break;
			case sMode.ADD_AS_CHILD:
				break;
			case sMode.RENAME:
				rm = (renameMode) EditorGUI.EnumPopup ( EditorGUILayout.GetControlRect(), "Rename Mode:", rm);
				switch (rm) {
					case renameMode.NAME_NUMBER:
						newName = EditorGUILayout.TextField ("Name:", newName);
						break;
					case renameMode.NUMBER_FROM:
						newName = EditorGUILayout.TextField ("Name:", newName);
						num = EditorGUILayout.IntField ("Starting Number:", num);
						break;
					case renameMode.SAME_NAME:
						newName = EditorGUILayout.TextField ("Name:", newName);
						break;
				}
				break;
			case sMode.SET_TO_SINGLE_PREFAB:
				pObj = (GameObject) EditorGUI.ObjectField(EditorGUILayout.GetControlRect(),"Connect to : ",obj,typeof(GameObject), true);
				if (pObj != null && PrefabUtility.GetPrefabType(pObj) == PrefabType.Prefab) {
					obj = pObj;
				} else {
					obj = null;
				}
				break;
		}
	}
	
	[MenuItem("Meshadieme/EditorToolWindows/CustomSelection")]
	static void Init()
	{
		// get the window, show it, and hand it focus
		var window = EditorWindow.GetWindow<CustomSelection>("Custom Selection", false);
		window.Show();
		window.Focus();
	}
}
#endif