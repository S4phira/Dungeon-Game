using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable {

	public Sprite emptyChest;
	public int pesosAmout = 5;

	protected override void OnCollect() {
		if(!collected) {
			collected  = true;
			GetComponent<SpriteRenderer>().sprite = emptyChest;
			GameManager.instance.pesos += pesosAmout;
			GameManager.instance.ShowText("+" + pesosAmout + " pesos!", 25, Color.yellow, transform.position, Vector3.up *25, 1.5f);
		}
	}

}
