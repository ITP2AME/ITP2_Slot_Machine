
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
/// An Editor Tool Window to build asset bundles from selected scenes. 
/// The bundles go into the root folder of the project atm.
/// This is a work in progress.
/// </summary>
/// CHANGELOG: 
///	*	0.0.1: First Version
/// TODO: N/A (place holder)
/// <contents>
/// OnEnable () 
/// OnGUI()
/// performMode ()
/// setMode ()
/// Init ()
/// </contents>

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using System;

public class AssetBundleWindow : EditorWindow {
	
	enum mode
	{
		FROM_SCENES = 0
	}

	private Vector2 scrollPos;// position of scroll view
	private SceneContainer objs;
	private mode bm = mode.FROM_SCENES;
	private SerializedObject so;
	private SerializedProperty sp;

	void OnEnable() {
		hideFlags = HideFlags.HideAndDontSave;
		if (objs == null) {	
			objs = ScriptableObject.CreateInstance<SceneContainer>();
		}
		if (so == null) {
			so = new SerializedObject (objs);
		}
	}

	void OnGUI() {
		// start the scroll view
		scrollPos = EditorGUILayout.BeginScrollView (scrollPos);

		// store if the GUI is enabled so we can restore it later
		bool guiEnabled = GUI.enabled;

		//Enum Drop Down Followed by fucntion that sets relative GUI elements
		bm = (mode) EditorGUI.EnumPopup ( EditorGUILayout.GetControlRect(), "Bundle Mode:", bm);
			setMode ();

		//Button to perform task
		if(GUI.Button(EditorGUILayout.GetControlRect(), "BUILD BUNDLES!"))
			performMode ();

		// restore the GUI
		GUI.enabled = guiEnabled;
		
		// close the scroll view
		EditorGUILayout.EndScrollView();
	} 

	void performMode () {
		switch (bm) {
			case mode.FROM_SCENES:
				UnityEngine.Object obj;
				string[] path = new string[1];
				string error;
				List<string> path2 = new List<String>();
				List<int> index = new List<int>();
				//Store path names in array because assembly reload clears reference
				for (int i = 0; i < sp.arraySize; i++) {
					obj = sp.GetArrayElementAtIndex(i).objectReferenceValue;
					if (AssetDatabase.GetAssetPath(obj).EndsWith( ".unity" ) && obj.ToString().EndsWith( " (UnityEngine.SceneAsset)" ) && EditorUtility.IsPersistent(obj) ) {
						path2.Add (AssetDatabase.GetAssetPath(obj));
						index.Add (i);
					} else {
						Debug.LogWarning (" Array Object #" + i + " is not a Scene to build assets from!  \n -- Meshadieme/EditorToolWindows/AssetBundleWindow");
					}
				}
				//use previous list to build asset bundles
				for (int i = 0; i < path2.Count; i++) {
					obj = sp.GetArrayElementAtIndex(index[i]).objectReferenceValue;
					path[0] = path2[i];
					error = BuildPipeline.BuildStreamedSceneAssetBundle( path, PlayerSettings.productName + "_" + obj.name + "_Bundle.unity3d", BuildTarget.WebPlayer);
					if (error == "") {
						Debug.LogWarning (" Array Object #" + i + " Asset Bundle built!  \n -- Meshadieme/EditorToolWindows/AssetBundleWindow");
					} else {
						Debug.LogWarning (" Array Object #" + i + " Asset Bundle Error : " + error + "!  \n -- Meshadieme/EditorToolWindows/AssetBundleWindow");
					}
				}
				break;
		}
	}

	void setMode() {
		switch (bm) {
			case mode.FROM_SCENES:
				so.Update();
				sp = so.FindProperty("Scenes");
				EditorGUILayout.PropertyField(sp, true);
				so.ApplyModifiedProperties(); 
				objs.OnGUI();
				break;
		}
	}
	
	[MenuItem("Meshadieme/EditorToolWindows/AssetBundleWindow")]
	static void Init() {
		// get the window, show it, and hand it focus
		var window = EditorWindow.GetWindow<AssetBundleWindow>("Asset Bundles", false);
		window.Show();
		window.Focus();
	}
//
//	public static T GetFieldByName<T>(string fieldName, BindingFlags bindingFlags, object obj)
//	{
//		FieldInfo fieldInfo = obj.GetType ().GetField (fieldName, bindingFlags);
//		if(fieldInfo == null)
//			return default(T);
//		return (T)fieldInfo.GetValue (obj);
//	}
}
#endif