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
		GameManager.instance.player.SwapSprite(curentCharacterSelection);
	
	}

	//weapon upgrade
	public void OnUpgradeClick(){
		if(GameManager.instance.TryUpgradeWeapon())
		UpdateMenu();
	}
	public void SaveMenu() {
		///////
		GameManager.instance.SaveState();
	}
	//update the character information
	public void UpdateMenu(){

		
		//weapon
		weaponSprite.sprite = GameManager.instance.weaponSprite[GameManager.instance.weapon.weaponLevel];
		
		if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count){
			upgradeCostText.text = "MAX";		
		}
		else{
			upgradeCostText.text  = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
		}
		
		//meta
		levelText.text = GameManager.instance.GetCurrentLevel().ToString();
		hitpointText.text = GameManager.instance.player.hitPoint.ToString();
		pesosText.text = GameManager.instance.pesos.ToString();

		//xp bar
		int currLevel = GameManager.instance.GetCurrentLevel();
		if(currLevel  == GameManager.instance.xpTable.Count){
			xpText.text = GameManager.instance.experience.ToString() + " total experince points";
			xpBar.localScale = Vector3.one;
		}
		else{
			int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
			int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

			int diff = currLevelXp - prevLevelXp;
			int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

			float completionRadio = (float)currXpIntoLevel / (float)diff;
			xpBar.localScale = new Vector3(completionRadio, 1, 1);
			xpText.text = currXpIntoLevel.ToString() + " / " + diff;
		}
	
	}


}
