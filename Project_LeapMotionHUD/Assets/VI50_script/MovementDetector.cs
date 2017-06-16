using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class MovementDetector : MonoBehaviour {


	public Controller con;
	public Frame frame;

	public Transform[] LfingersTransforms, RfingersTransforms;
	public GameObject LeftHand, RightHand, LPalm, RPalm;
	public float GraspThreshold=250f;
	public float rotfactor=1f;
	public float transfactor=1f;

	public GameObject selected;
	public GameObject[] allObj;

	void Start( )
	{
		con=new Controller();

		selected=null;

		allObj = GameObject.FindGameObjectsWithTag("EnvironmentObject");

	}


	void Update () {

		foreach (GameObject obj in allObj) {
			if (obj.GetComponent<objectHandler> ().isSelected == true) {
				selected = obj;
			}
		}

		frame = con.Frame ();

		if(frame.Hands.Count > 0){

			List<Hand> hands = frame.Hands;

			Hand firstHand = hands [0];

			GraspThreshold=firstHand.PalmWidth*3;

			if(hands.Count==1)
			{
				bool grasp=detectGrasp(firstHand);
				bool point=detectIndexPointing(firstHand);
				Debug.Log("Pointing active : "+point+"   GraspActive : "+grasp);
			}


			if(hands.Count==2)
			{
				Hand Lh, Rh;
				if(hands[0].IsLeft)
				{
					Lh=hands[0];
					Rh=hands[1];
				}
				else
				{
					Lh=hands[1];
					Rh=hands[0];
				}

				bool Lgrasp=detectGrasp(Lh);
				bool Lpoint=detectIndexPointing(Lh);
				bool Rgrasp=detectGrasp(Rh);
				bool Rpoint=detectIndexPointing(Rh);
				Debug.Log("Lpointing : "+Lpoint+"   Lgrasp : "+Lgrasp+"   Rpointing : "+Rpoint+"   Rgrasp : "+Rgrasp);

				if(Lgrasp && !Rgrasp)
				{
					rotateSelected(Rh);
				}

				if(Rgrasp && !Lgrasp)
				{
					translateSelected(Lh);
				}
			}

		}
		else
		{
			Debug.Log("No visible hands");
		}
	}



	//rotation and translation

	public void rotateSelected(Hand h)
	{
		selected.transform.Rotate(0,h.PalmVelocity.x*rotfactor*0.01f,0);
	}
	
	public void translateSelected(Hand h)
	{
		selected.transform.Translate(0.01f*transfactor*new Vector3(h.PalmVelocity.x, 0, -h.PalmVelocity.z));
	}


	//movement detection

	public bool detectGrasp(Hand h)
	{
		float distsum=0;

		foreach( Finger f in h.Fingers )
		{
			distsum+=f.TipPosition.DistanceTo(h.PalmPosition);
		}
			
		return distsum<GraspThreshold;

		//amélioration : ajouter des coefficients aux doigts
		//petit coef pour le pouce
	}

	public bool detectGraspImproved(Hand h)
	{
		bool grasp=true;

		foreach( Finger f in h.Fingers )
		{
			if(f.IsExtended && f.Type!=Finger.FingerType.TYPE_THUMB)
			{
				grasp=false;
			}
		}

		return grasp;
	}

	public bool detectIndexPointing(Hand h)
	{
		Finger index=getByType(h, Finger.FingerType.TYPE_INDEX);
		Debug.Log(index.Type);

		bool ispointing=index.IsExtended;
		bool c=true;

		foreach( Finger f in h.Fingers )
		{
			if(f.Type!=Finger.FingerType.TYPE_INDEX)
			{
				if(f.IsExtended && f.Type!=Finger.FingerType.TYPE_THUMB)
				{
					c=false;
				}
			}

		}

		return ispointing && c;
	}


	//util

	public Finger getByType(Hand h, Finger.FingerType type)
	{
		foreach( Finger f in h.Fingers )
		{
			if(f.Type==type)
			{
				return f;
			}
		}

		return null;
	}

}