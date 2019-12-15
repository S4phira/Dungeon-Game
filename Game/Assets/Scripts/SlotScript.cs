using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IPointerClickHandler {

	public Stack<Item> items; // The items that the slot contains
	public Text stackText; // Indicates the number of items stacked on the slot
	public Sprite slotEmpty; // The slot's empty sprite
	public Sprite slotHighlight; // The slot's highlighted sprite


	public bool IsEmpty {
		get { return items.Count == 0; }
	}

	 // Returns the current item in the stack
    public Item CurrentItem {
        get { return items.Peek(); }
    }

 	// Indicates if the slot is avaialble for stacking more items
    public bool IsAvailable {
        get { return CurrentItem.maxSize > items.Count; }
    }

	void Awake() {
	items = new Stack<Item>();
	}
	
	// Use this for initialization
	void Start () {
		//Creates a reference to the slot's and stactText's recttransform
		RectTransform slotRect = GetComponent<RectTransform>();
		RectTransform txtRect = GetComponent<RectTransform>();

		//Calculates the scalefactor of the text by taking 60% of the slots width
		int txtScaleFactor = (int) (slotRect.sizeDelta.x * 0.60);

		stackText.resizeTextMaxSize = txtScaleFactor;
		stackText.resizeTextMinSize = txtScaleFactor;

		txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
		txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
	
	}
	
 	// Adds a single item to inventory
	public void AddItem(Item item) {
		items.Push(item);
		

		if(items.Count > 1) {
			stackText.text = items.Count.ToString();
		}

		ChangeSprite(item.spriteNeutral, item.spriteHiighlighted);
		
	}

 	// Changes the sprite of a slot
	private void ChangeSprite(Sprite neutral, Sprite highlight) {

		GetComponent<Image>().sprite = neutral;
		SpriteState spritestate = new SpriteState();
		spritestate.highlightedSprite = highlight;
		spritestate.pressedSprite = neutral;

		GetComponent<Button>().spriteState = spritestate;

	}

	// Use Item and destroy in inventory
	private void UseItem() {
		if(!IsEmpty) {
			items.Pop().Use();
			stackText.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

			if (IsEmpty) {
				ChangeSprite(slotEmpty, slotHighlight);
				Inventory.EmptySlots++;
			}

		}
	}
	// IPointerClickHandler
	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Right) {
			UseItem();
		}
	}
}
