using System;
using System.Collections.Generic;
using Hover.Core.Utils;
using UnityEngine;
using UnityEngine.VR;

namespace HoverDemos.CastCubes {

	/*================================================================================================*/
	public class Environment : MonoBehaviour {

		public int RandomSeed = 0;

		private int Count;

		public enum ColorMode {
			Advanced,
			Simple
		}

		public enum LumiMode {
			Custom,
			Night,
			Day	
		}
			
		public Light MainLight;
		public Light Spotlight;

		private readonly GameObject[] vHolds;
		private GameObject[] vObjects;

		private GameObject vObjectSelected;
		private ColorMode vColorMode;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public Environment() {
			vHolds = new GameObject[Count];
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public void Awake() {
			VRSettings.renderScale = 2;
			vObjects = GameObject.FindGameObjectsWithTag("EnvironmentObject");
			Count = vObjects.Length;
			Spotlight.enabled = false;
		}

		/*--------------------------------------------------------------------------------------------*/
		public void Update() {
			if ( Input.GetKey(KeyCode.Escape) ) {
				Application.Quit();
				return;
			}

			if ( Input.GetKey(KeyCode.R) ) {
				InputTracking.Recenter();
			}

			Spotlight.enabled = (Spotlight.intensity > 0);

		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public ColorMode GetColorMode() {
			return vColorMode;
		}

		/*--------------------------------------------------------------------------------------------*/
		public void SetColorMode(ColorMode pMode,float pHue, float pSat,float pVal) {
			vColorMode = pMode;
			Color color = Color.white;

			foreach(GameObject obj in vObjects){
				if (obj.GetComponent<objectHandler> ().isSelected) {
					vObjectSelected = obj;
				}
			}

			//Changement des couleurs suivant le mode choisis
			if ( vColorMode == ColorMode.Simple ) {
				color = DisplayUtil.HsvToColor(pHue, 1, 1);
			}
			if (vColorMode == ColorMode.Advanced) {
				color = DisplayUtil.HsvToColor(pHue,pSat/100,pVal/100);
			}

			if (vObjectSelected != null) {
				Debug.Log ("Il y a un objet selectionnée");
				vObjectSelected.GetComponent<Renderer> ().material.color = color;
			} else {
				//Debug.Log ("Pas d'objet selectionné");
			}
		}
			
		/*--------------------------------------------------------------------------------------------*/
		public void SetLightIntensitiy(LumiMode pMode,float pIntensity) {
			if (pMode == LumiMode.Custom) {
				MainLight.intensity = pIntensity;
			}
			if (pMode == LumiMode.Day) {
				MainLight.intensity = 4;
			}
			if (pMode == LumiMode.Night) {
				MainLight.intensity = 1;
			}
		}
			
		/*--------------------------------------------------------------------------------------------*/
		private void UpdateCube(int pIndex) {
			if (vObjectSelected != null) {
				//Faire ici les changements sur le cube sélectionner (position, rotation,...)

			}


		}

	}

}
