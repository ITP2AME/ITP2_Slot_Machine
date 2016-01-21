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

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

namespace Meshadieme {
	public class parentManager : MonoBehaviour {

		[HideInInspector]
		public debugToggle debugMode;
		
		/// <summary>
		/// Awake (Unity) funtions
		/// This is my failed attempted to make the debug toggle accessible in one place for all manager scripts
		/// Ignore this for now, will implement in C# template.
		/// </summary>
		protected virtual void Awake () {
			DebugLog ("parentAwake on = " + this + "and DebugToggle = " + debugMode);

		}
		 
		/// <summary>
		/// DebugLog (Custom) funtions
		/// DebugLogError (Custom) funtions
		/// DebugLogWarning (Custom) funtions
		/// DebugDrawLine (Custom) funtions
		/// DebugDrawRay (Custom) funtions
		/// These replace and maintain our needed toggle switches while passing the same values to the unity debug functions.
		/// * Pass function value to Unity default debug functions if debug is enabled.
		/// </summary>

		protected void DebugLog (object message) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.Log (message);
			}
		}
		
		protected void DebugLog (object message, UnityEngine.Object context) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.Log (message, context);
			}
		}
		
		protected void DebugLogError (object message) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.LogError (message);
			}
		}
		
		protected void DebugLogError (object message, UnityEngine.Object context) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.LogError (message, context);
			}
		}
		
		protected void DebugLogWarning (object message) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.LogWarning (message.ToString ());
			}
		}
		
		protected void DebugLogWarning (object message, UnityEngine.Object context) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.LogWarning (message.ToString (), context);
			}
		}
		
		protected void DebugDrawLine(Vector3 start, Vector3 end, Color color, float duration, bool depthTest) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.DrawLine(start, end, color, duration, depthTest);
			}
		}
		
		protected void DebugDrawLine(Vector3 start, Vector3 end, Color color, float duration) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.DrawLine(start, end, color, duration, true);
			}
		}
		
		protected void DebugDrawLine(Vector3 start, Vector3 end, Color color) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.DrawLine(start, end, color, 0.0f, true);
			}
		}
		
		protected void DebugDrawRay(Vector3 start, Vector3 dir, Color color, float duration, bool depthTest) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.DrawRay(start, dir, color, duration, depthTest);
			}
		}
		
		protected void DebugDrawRay(Vector3 start, Vector3 dir, Color color, float duration) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.DrawRay(start, dir, color, duration, true);
			}
		}
		
		protected void DebugDrawRay(Vector3 start, Vector3 dir, Color color) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.DrawRay(start, dir, color, 0.0f, true);
			}
		}

		protected void DebugLogException(System.Exception e) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.LogException(e);
			}
		}

		protected void DebugLogException(System.Exception e, UnityEngine.Object context) {
			if ( Debug.isDebugBuild && debugMode.localDebug) {
				Debug.LogException(e, context);
			}
		}
	}

	
	[Serializable]
	public class debugToggle {

		public string named;
		public bool localDebug;

	}
	
	#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(debugToggle))]
	public class debugToggleDrawer : PropertyDrawer {

		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
			Rect contentPosition = EditorGUI.PrefixLabel(position, label);
			EditorGUI.ToggleLeft(contentPosition, property.FindPropertyRelative("named").stringValue, property.FindPropertyRelative("localDebug").boolValue);
		}

	}
	#endif
}