using UnityEngine;
using System.Collections;

public class objectHandler : MonoBehaviour {

	public bool isSelected;
	public bool isHovered;
	Shader startShader;

	// Use this for initialization
	void Start () {
		startShader = gameObject.GetComponent<Renderer> ().material.shader;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (gameObject.GetComponent<objectHandler> ().isHovered == true) {
			gameObject.GetComponent<Renderer> ().material.shader = Shader.Find ("Unlit/SkyReflection Per Pixel");
		} else {
			gameObject.GetComponent<Renderer>().material.shader = startShader;
		}

	}
}
