using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : Collectable {

	public Sprite emptyChest;
	public int pesosAmout = 5;

	protected override void OnCollect() {
		if(!collected) {
			collected  = true;
			GetComponent<SpriteRenderer>().sprite = emptyChest;
			Debug.Log("Grant " + pesosAmout + " pesos");
		}
	}

}
