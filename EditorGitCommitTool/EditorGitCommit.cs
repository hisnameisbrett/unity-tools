// File: EditorGitCommit.cs
// Author: Brett Cunningham
// GitHub: hisnameisbrett
// Date: 01/22/2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor window for committing and pushing changes to your git repo.
/// Runs the "commit.bat" script in root of project and passes
/// given commit message. Process commits all local changes and 
/// pushes them to your current remote branch. 
/// </summary>
public class EditorGitCommit : EditorWindow
{
    static string _message;

    [MenuItem("Tools/Commit")]
    static void Init()
    {
        EditorGitCommit window = (EditorGitCommit)GetWindow(typeof(EditorGitCommit));
        window.maxSize = new Vector2(600, 80);
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Message:");
        _message = EditorGUILayout.TextField(_message);
        EditorGUILayout.BeginHorizontal();
        GUI.SetNextControlName("ClearButton");
        if (GUILayout.Button("Clear", GUILayout.MaxWidth(60)))
        {
            Clear();
        }
        if (GUILayout.Button("Commit", GUILayout.MaxWidth(80)))
        {
            System.Diagnostics.Process.Start("commit.bat", string.Format("\"{0}\"", _message));
            Clear();
        }
        EditorGUILayout.EndHorizontal();
    }

    void Clear()
    {
        _message = "";
        Repaint();
        GUI.FocusControl("ClearButton");
    }
}
