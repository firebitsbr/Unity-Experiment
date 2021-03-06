﻿// Copyright Cape Guy Ltd. 2015. http://capeguy.co.uk.
// Provided under the terms of the MIT license -
// http://opensource.org/licenses/MIT. Cape Guy accepts
// no responsibility for any damages, financial or otherwise,
// incurred as a result of using this code.
using UnityEngine;
using UnityEditor;

/// <summary>
/// This script exits play mode whenever script compilation is detected during an editor update.
/// </summary>
[InitializeOnLoad]
class ExitPlayModeOnCompile 
{
	static ExitPlayModeOnCompile _instance = null;

	// Static initialiser called by Unity Editor whenever scripts are loaded (editor or play mode)
	static ExitPlayModeOnCompile()
	{
		Unused(_instance);
		_instance = new ExitPlayModeOnCompile();
	}

	ExitPlayModeOnCompile()
	{
		EditorApplication.update += OnEditorUpdate;
	}

	~ExitPlayModeOnCompile()
	{
		EditorApplication.update -= OnEditorUpdate;
		// Silence the unused variable warning with an if.
		_instance = null;
	}

	// Called each time the editor updates.
	static void OnEditorUpdate()
	{
		if(EditorApplication.isPlaying && EditorApplication.isCompiling) {
			Debug.Log("Exiting play mode due to script compilation.");
			EditorApplication.isPlaying = false;
		}
	}

	// Used to silence the 'is assigned by its value is never used' warning for _instance.
	static void Unused<T>(T unusedVariable)
	{
	}
}