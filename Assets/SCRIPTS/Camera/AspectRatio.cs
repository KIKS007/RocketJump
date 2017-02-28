using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatio : MonoBehaviour  
{
	private float baseAspect = 09f / 16f;

	void Start() 
	{
		float currAspect = 1.0f * Screen.width / Screen.height;

		//Debug.Log(Camera.main.projectionMatrix);
		//Debug.Log(baseAspect + ", " + currAspect + ", " + baseAspect / currAspect);

		Camera.main.projectionMatrix = Matrix4x4.Scale(new Vector3(currAspect / baseAspect, 1.0f, 1.0f)) * Camera.main.projectionMatrix;
	}

}
