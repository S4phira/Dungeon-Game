using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	private RectTransform inventoryRect; // A reference to the inventorys RectTransform
	private float inventoryWidth, inventoryHight; //The width and height of the inventory
	public int slots;
	public int rows;
	public float slotPaddingLeft, slotPaddingTop;
	public float slotSize;
	public GameObject slotPrefab;

	private List<GameObject> allSlots; // List of all the slots

	private static int emptySlots;

	public static int EmptySlots {
		get { return emptySlots; }
		set { emptySlots = value; }
	}



	void Start () {
		CreateLayout();
	}

	private void CreateLayout() {
		allSlots = new List<GameObject>();

		emptySlots = slots;

		inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
		inventoryHight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

		inventoryRect = GetComponent<RectTransform>();

		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight);

		int columns = slots / rows;
		for (int y = 0; y < rows; y++) {
			for (int x = 0; x < columns; x++) {
				GameObject newSlot = (GameObject)Instantiate(slotPrefab);

				RectTransform slotRect = newSlot.GetComponent<RectTransform>();

				newSlot.name = "Slot";

				newSlot.transform.SetParent(this.transform.parent);

				slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft* (x+1) + (slotSize *x), -slotPaddingTop * (y+1) -(slotSize * y));
				
				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

				allSlots.Add(newSlot);
			}
		}
	}

    // Adds an item to the inventory
	public bool AddItem(Item item) {
		if (item.maxSize == 1) { //If the item isn't stackable
			PlaceEmpty(item);
			return true;
		}
		else {
			foreach (GameObject slot in allSlots) {
				SlotScript currentSlot = slot.GetComponent<SlotScript>();

				if (!currentSlot.IsEmpty) {
					if(currentSlot.CurrentItem.type == item.type && currentSlot.IsAvailable) {
						currentSlot.AddItem(item);
						return true;
					}
				}
			}
			if (emptySlots > 0) {
				PlaceEmpty(item);
			}
		}
		return false;
	}
	 // Places an item on an empty slot
	private bool PlaceEmpty(Item item) {
		if (emptySlots > 0) {
			foreach (GameObject slot in allSlots) {
				SlotScript currentSlot = slot.GetComponent<SlotScript>(); //Creates a reference to the slot
				if (currentSlot.IsEmpty) { //If the slot is empty
					currentSlot.AddItem(item);
					emptySlots--;
					return true;
				}
			}
			
		}
		return false;
	}


}
