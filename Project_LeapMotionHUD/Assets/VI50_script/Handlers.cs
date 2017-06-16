using Hover.Core.Items;
using Hover.Core.Items.Types;
using UnityEngine;

namespace HoverDemos.CastCubes {

	/*================================================================================================*/
	[RequireComponent(typeof(Environment))]
	public class Handlers : MonoBehaviour {

		private float pHue;
		private float pVal;
		private float pSat;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public Environment Enviro {
			get {
				return GetComponent<Environment>();
			}
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public void InitColorSimple(IItemData pItemData){
			if(IsItemAvailable(pItemData)){
				SetColorSimple((IItemDataSlider)pItemData);
			}
		}

		public void SetColorSimple(IItemDataSelectable<float> pItemData) {
			//Stockage de la valeur pour le choix de couleur avancé
			pHue = ((IItemDataSlider)pItemData).SnappedRangeValue;
			Enviro.SetColorMode(Environment.ColorMode.Simple,1,1,pHue);
		}

		/*--------------------------------------------------------------------------------------------*/
		public void GetColorAdvancedSat(IItemDataSelectable<float> pItemData) {
			pSat = ((IItemDataSlider)pItemData).SnappedRangeValue;
		}
	
		public void GetColorAdvancedHue(IItemDataSelectable<float> pItemData){
			pHue = ((IItemDataSlider)pItemData).SnappedRangeValue;
		}

		public void GetColorAdvancedVal(IItemDataSelectable<float> pItemData) {
			pVal = ((IItemDataSlider)pItemData).SnappedRangeValue;
		}

		public void SetColorAdvanced(IItemDataSelectable<float> pItemData){
			Enviro.SetColorMode (Environment.ColorMode.Advanced, pHue, pSat, pVal);
		}
			
		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public void SetLightingPower(IItemDataSelectable<float> pItemData) {
			Enviro.SetLightIntensitiy(Environment.LumiMode.Custom,((IItemDataSlider)pItemData).RangeValue);
		}

		public void setDayLight(){
			Enviro.SetLightIntensitiy (Environment.LumiMode.Day,0);
		}

		public void setNightLight(){
			Enviro.SetLightIntensitiy (Environment.LumiMode.Night, 0);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		private bool IsItemAvailable(IItemData pItemData) {
			return (pItemData.IsEnabled && pItemData.gameObject.activeInHierarchy);
		}

	}

}
