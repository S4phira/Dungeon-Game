using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterMenu : MonoBehaviour {
	public Text levelText, hitpointText, pesosText, upgradeCostText, xpText;


	//Logic
	private int curentCharacterSelection = 0;
	public Image characterSelectionSprite, weaponSprite;
	public RectTransform xpBar;

	//Character Selection
	public void OnArrowClick(bool right){

		if(right){
			curentCharacterSelection++;

			if(curentCharacterSelection == GameManager.instance.playerSprites.Count)
			curentCharacterSelection = 0;

			OnSelectionChanged();
		}
		else
		{
			curentCharacterSelection--;

			if(curentCharacterSelection < 0 )
			curentCharacterSelection = GameManager.instance.playerSprites.Count -1;

			OnSelectionChanged();
		}
	}
	private void OnSelectionChanged(){
		characterSelectionSprite.sprite = GameManager.instance.playerSprites[curentCharacterSelection];
	}

	//weapon upgrade
	public void OnUpgradeClick(){
		if(GameManager.instance.TryUpgradeWeapon())
		UpdateMenu();
	}

	//update the character information
	public void UpdateMenu(){

		///////
		GameManager.instance.SaveState();
		//weapon
		weaponSprite.sprite = GameManager.instance.weaponSprite[GameManager.instance.weapon.weaponLevel];
		
		if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count){
			upgradeCostText.text = "MAX";		
		}
		else{
			upgradeCostText.text  = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
		}
		
		//meta
		levelText.text = "Not implemented";
		hitpointText.text = GameManager.instance.player.hitPoint.ToString();
		pesosText.text = GameManager.instance.pesos.ToString();

		//xp bar
		xpText.text = "Not implemented";
		xpBar.localScale = new Vector3(0.5f, 0, 0);

	}


}
