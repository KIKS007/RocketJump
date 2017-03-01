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

	SerializedProperty RightBreakableBloc;
	SerializedProperty LeftBreakableBloc;

	void OnEnable ()
	{
		Lane = serializedObject.FindProperty ("Lane");

		RightWall = serializedObject.FindProperty ("RightWall");
		LeftWall = serializedObject.FindProperty ("LeftWall");

		RightBreakableBloc = serializedObject.FindProperty ("RightBreakableBloc");
		LeftBreakableBloc = serializedObject.FindProperty ("LeftBreakableBloc");
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

		EditorGUILayout.PropertyField (RightBreakableBloc, true);	
		EditorGUILayout.PropertyField (LeftBreakableBloc, true);

		serializedObject.ApplyModifiedProperties ();
	}
}
