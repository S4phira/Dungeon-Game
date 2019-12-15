using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {MANA, HEALTH};

public class Item : Collectable {
	public Inventory inventory;
	public ItemType type;

	public Sprite spriteNeutral;
	public Sprite spriteHiighlighted;
	public int maxSize;

	public int healingAmount = 5;

	//TODO: implement mana 
	//public int manaRegenerationAmount = 10;

	protected override void OnCollide(Collider2D coll) {
		if (coll.name == "Player") 
		Destroy(this.gameObject);
	}
	public void Use() {
		switch(type) {
			case ItemType.MANA:
			Debug.Log("I use mana potion");
			break;
			
			case ItemType.HEALTH:
			Debug.Log("I use health potion");
			GameManager.instance.player.Heal(healingAmount);
			break;
		}
	}
	
}
