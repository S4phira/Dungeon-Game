using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	private void Awake(){

	}
	//Resources
	public List<Sprite> playerSprite;
	public List<Sprite> weaponSprite;
	public List<int> weaponPrices;
	public List<int> xpTable;

	public Player player;

	public int pesos;
	public int experience;

	public void SaveState(){
		Debug.Log("SaveState");
	}
	public void LoadState(){
		Debug.Log("LoadState");
	}
}
