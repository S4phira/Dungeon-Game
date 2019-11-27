using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	private void Awake(){

		if (GameManager.instance != null) {
			Destroy(gameObject);
			return;
		}
		instance = this;
		SceneManager.sceneLoaded +=LoadState;
		DontDestroyOnLoad(gameObject);
	}
	//Resources
	public List<Sprite> playerSprite;
	public List<Sprite> weaponSprite;
	public List<int> weaponPrices;
	public List<int> xpTable;

	//public Player player;
	public Player player;
	public FloatingTextManager floatingTextManager;
	public int pesos;
	public int experience;

	//Floating text
	public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration){
		floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
	}

	//Save state
	public void SaveState(){
		string s = "";
		s += "0" + "|";
		s += pesos.ToString() + "|";
		s += experience.ToString() + "|";
		s += "0";

		PlayerPrefs.SetString("SaveSatet", s);

	}
	public void LoadState( Scene s, LoadSceneMode mode){

		if(PlayerPrefs.HasKey("SaveStste"))
		return;

		string[] data = PlayerPrefs.GetString("SaveState").Split('|');

		pesos = int.Parse(data[1]);
		experience = int.Parse(data[2]);

		Debug.Log("LoadState");
	}
}
