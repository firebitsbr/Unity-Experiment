﻿using UnityEngine;
using UnityEditor;
using System.IO;

public class BetterScriptTemplate
{
	static string boolKey = "ScriptTemplateReplaced";

	[MenuItem("Extras/Simplify C# Template", false, 10)]
	static void Execute()
	{
		EditorPrefs.SetBool(boolKey, true);

		if(EditorUtility.DisplayDialog(
			   "Simplify C# Template",
			   "This will replace C# template with a simpler one and delete the Javascript template.\n\nThis requires you to restart Unity.\n",
			   "OK", "Cancel"
		   )) {
			MoveFiles();
		}
	}

	[MenuItem("Extras/Simplify C# Template", true)]
	static bool CheckReplacement()
	{
		return !EditorPrefs.GetBool(boolKey, false);
	}

	[MenuItem("Extras/Clear EditorPrefs", false, 0)]
	static void ClearEditorPrefs()
	{
		EditorPrefs.DeleteKey(boolKey);
	}

	static void MoveFiles()
	{
		string templatePath = string.Join("/", new [] { EditorApplication.applicationPath, "Contents", "Resources", "ScriptTemplates" });
		string csTemplatePath = templatePath + "/81-C# Script-NewBehaviourScript.cs.txt";
		string jsTemplatePath = templatePath + "/82-Javascript-NewBehaviourScript.js.txt";
		string newCsTemplatePath = Application.dataPath + "/Standard Assets/Editor/Support/ScriptTemplate/81-C# Script-NewBehaviourScript.cs.txt";

		if(!File.Exists(csTemplatePath + ".bak")) {
			File.Move(csTemplatePath, csTemplatePath + ".bak");
		} else {
			Debug.LogWarningFormat("{0}.bak already exists", csTemplatePath);
		}

		if(!File.Exists(jsTemplatePath + ".bak")) {
			File.Move(jsTemplatePath, jsTemplatePath + ".bak");
		} else {
			Debug.LogWarningFormat("{0}.bak already exists", jsTemplatePath);
		}

		File.Copy(newCsTemplatePath, csTemplatePath, true);
	}
}
