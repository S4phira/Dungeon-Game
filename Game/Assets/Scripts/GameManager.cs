﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
    private void Awake()
    {
		//PlayerPrefs.DeleteAll();
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu.gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
	//Resources
	public List<Sprite> playerSprites;
	public List<Sprite> weaponSprite;
	public List<int> weaponPrices;
	public List<int> xpTable;


	//references
	public Player player;
	public Weapon weapon;
	public FloatingTextManager floatingTextManager;
	public RectTransform hitpointBar;
	public GameObject hud;
	public CharacterMenu menu;


	public int pesos;
	public int experience;


	//Floating text
	public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration){
		floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
	}

	//upgrade weapon
	public bool TryUpgradeWeapon()
    {
        // is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

		//Hitpoint Bar
	public void OnHitpointChange(){
			float ratio = (float)player.hitPoint / (float)player.maxHitpoint;
			hitpointBar.localScale = new Vector3(1, ratio, 1);

		}

	public int GetCurrentLevel(){
		int r=0;
		int add=0;

		while(experience >= add)
		{
			add+= xpTable[r];
			r++;

			if(r== xpTable.Count){
				return r;
			}	
		}
			
		
		return r;
	}

	public int GetXpToLevel(int level){
		int r = 0;
		int xp = 0;
		while (r < level) {
			xp+= xpTable[r];
			r++;
		}
		return xp;
	}
	
	public void GrantXp(int xp){
		int currLevel = GetCurrentLevel();
		experience +=xp;
		if(currLevel < GetCurrentLevel()){
			OnLevelUp();
		}
	}
	public void OnLevelUp(){
		player.OnLevelUp();
	}
	
	public void OnSceneLoaded( Scene s, LoadSceneMode mode){
		player.transform.position = GameObject.Find("SpawnPoint").transform.position;
	}
	
	//Save state
	public void SaveState(){
		
		string s = "";
		s += "0" + "|";
	  	s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();


		PlayerPrefs.SetString("SaveState", s);
		

	}
	public void LoadState( Scene s, LoadSceneMode mode){

		SceneManager.sceneLoaded -=LoadState;

		if (!PlayerPrefs.HasKey("SaveState"))
        return;

		string[] data = PlayerPrefs.GetString("SaveState").Split('|');

		pesos = int.Parse(data[1]);
		experience = int.Parse(data[2]);
		if(GetCurrentLevel() !=1)
			player.SetLevel(GetCurrentLevel());
		weapon.SetWeaponLevel(int.Parse(data[3]));

		

	}
}
