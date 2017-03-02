using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Chunk))]
[CanEditMultipleObjects]

public class ChunkEditor : Editor 
{
	SerializedProperty Lane;

	SerializedProperty RightWall;
	SerializedProperty LeftWall;

	SerializedProperty RightBreakableBlocs;
	SerializedProperty LeftBreakableBlocs;

	SerializedProperty _rightLaneChange;
	SerializedProperty _leftLaneChange;

	void OnEnable ()
	{
		Lane = serializedObject.FindProperty ("Lane");

		RightWall = serializedObject.FindProperty ("RightWall");
		LeftWall = serializedObject.FindProperty ("LeftWall");

		RightBreakableBlocs = serializedObject.FindProperty ("RightBreakableBlocs");
		LeftBreakableBlocs = serializedObject.FindProperty ("LeftBreakableBlocs");

		_rightLaneChange = serializedObject.FindProperty ("_rightLaneChange");
		_leftLaneChange = serializedObject.FindProperty ("_leftLaneChange");
	}

	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();

		EditorGUILayout.Space ();

		EditorGUILayout.PropertyField (Lane, true);

		if(Lane.enumValueIndex == (int)LaneType.Second || Lane.enumValueIndex == (int)LaneType.First)
			EditorGUILayout.PropertyField (RightWall, true);	
		
		if(Lane.enumValueIndex == (int)LaneType.Second || Lane.enumValueIndex == (int)LaneType.Third)
			EditorGUILayout.PropertyField (LeftWall, true);			

		/*EditorGUILayout.PropertyField (RightBreakableBlocs, true);	
		EditorGUILayout.PropertyField (LeftBreakableBlocs, true);

		EditorGUILayout.PropertyField (_rightLaneChange, true);
		EditorGUILayout.PropertyField (_leftLaneChange, true);*/

		serializedObject.ApplyModifiedProperties ();
	}
}
