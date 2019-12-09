﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {MANA, HEALTH};

public class Item : Collidable {
	public Inventory inventory;
	public ItemType type;

	public Sprite spriteNeutral;
	public Sprite spriteHiighlighted;
	public int maxSize;

	public void Use(){
		switch(type){
			case ItemType.MANA:
			Debug.Log("I use mana potion");
			break;
			case ItemType.HEALTH:
			Debug.Log("I use health potion");
			break;
		}
	}
	
}
