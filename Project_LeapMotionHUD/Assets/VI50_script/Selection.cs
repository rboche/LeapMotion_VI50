using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Unity;

public class Selection : MonoBehaviour {

	[SerializeField]
	public float raycastDistance;
	private RayCaster ray;
	private float rayHitStart = 0f;

	bool rycst;
	HandModel hand_model;

	public Hand rayCastHand;
	public Finger rayCastFinger;

	private LeapProvider provider = null;

	// Use this for initialization
	void Start () {
		
		/*hand_model = GameObject.Find ("CapsuleHand_R").GetComponentInChildren<HandModel> ();
		if (hand_model == null)
			Debug.Log ("no hand_model");
		rayCastHand = hand_model.GetLeapHand ();
		if (rayCastHand == null)
			Debug.Log ("No hand found");
		ray.OnRayEnter += Ray_OnRayEnter;
		ray.OnRayStay += Ray_OnRayStay;
		ray.OnRayExit += Ray_OnRayExit;
		ray.RayLength = raycastDistance;

	}

	void LateUpdate() {
		/*if (hand_model != null) {
			foreach (Hand h in handList) {
				if (h.IsRight) {
					rayCastHand = h;
				}
			}
			if (rayCastHand.Fingers.Count > 0) {
				List<Finger> fingerList = rayCastHand.Fingers;
				foreach (Finger f in fingerList) {
					if (f.Type == Finger.FingerType.TYPE_INDEX) {
						rayCastFinger = f;
					}
				}
			}
		}
			
		ray.Position = rayCastFinger.StabilizedTipPosition.ToVector3();
		ray.Direction = rayCastFinger.Direction.ToVector3();
		Debug.Log(ray.CastRay ());*/

/*		for (int i = 0; i < 5; i++) {

			if (hand_model.fingers [i] == null)
				Debug.Log ("No fingers");
			FingerModel finger = hand_model.fingers[i];
			Debug.DrawRay (finger.GetTipPosition (), finger.GetRay ().direction, Color.red);
			RaycastHit hit;
			rycst = Physics.Raycast (finger.GetTipPosition (), finger.GetRay ().direction, out hit);

			if (rycst) {
				if (hit.collider.tag == "EnvironmentObject") {
				}
					
			}
		}
	*/


	}

	void Ray_OnRayEnter (Collider collider)
	{
		if (collider.tag == "EnvironmentObject") {
			rayHitStart = Time.time;
			Debug.DrawRay (transform.position, transform.forward,Color.red,20,true);
			collider.GetComponent<objectHandler> ().isHovered = true;
		}
	}

	void Ray_OnRayStay (Collider collider)
	{
		if (collider.tag == "EnvironmentObject") {
			if (Time.time - rayHitStart > 5f) {
				collider.GetComponent<objectHandler> ().isSelected = true;
				Debug.Log ("Objet Sélectionné !!!!!!!!!!!!!" + collider.name);
			}
		}
	}

	void Ray_OnRayExit (Collider collider)
	{
		if (collider.tag == "EnvironmentObject") {
			rayHitStart = 0f;
		}
	}
}
