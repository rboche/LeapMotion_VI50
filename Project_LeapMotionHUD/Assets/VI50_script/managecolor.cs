using UnityEngine;
using System.Collections;

public class managecolor : MonoBehaviour {

	GameObject colorSlider;
	Hover.Core.Items.Types.HoverItemDataSlider colorComponent; 

	void Start(){
		/*GameObject Parent = GameObject.Find("RowA");
		Parent.FindObject ("ColorSlider");
		if (colorSlider.gameObject.tag == "ColorSlider") {
			Debug.Log ("Found object");
		} else {
			Debug.Log ("Not found");
		}
		colorComponent = colorSlider.GetComponent<Hover.Core.Items.Types.HoverItemDataSlider> ();*/
	}

	void Update(){
		/*if (colorSlider != null) {
			
			float? colorValue = colorComponent.HoverValue;
			float colorValueFinal = (float)colorValue / colorComponent.RangeMax;
			print ("La couleur final " + colorValueFinal);
			Color color = Color.HSVToRGB (colorValueFinal, 0.5f, 0.5f);
			GetComponent<Renderer> ().material.color = color;
		}*/
	}
}
