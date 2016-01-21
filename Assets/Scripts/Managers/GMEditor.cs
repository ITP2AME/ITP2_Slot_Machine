using Meshadieme;
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GM))]
[ExecuteInEditMode]
public class GMEditor : MonoBehaviour {
	
#if UNITY_EDITOR
	void Update () {
		if(UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) {
			this.enabled = false;
		} else {

			//Only Executes in editor
			if (GetComponent<GM>().fillManagers) {
				GetComponent<GM>().populateManagers ();
				GetComponent<GM>().fillManagers = false;
			}

		}
	}
#endif

}