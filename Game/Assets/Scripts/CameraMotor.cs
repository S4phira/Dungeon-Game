using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

	public Transform lookAt;
	public float boundX = 0.15f;
	public float boundY = 0.05f;

	private void LateUpdate() {

		Vector3 detla = Vector3.zero;
		
		//check if we're inside the bounds on the X axis
		float detlaX = lookAt.position.x - transform.position.x;
		if (detlaX > boundX || detlaX < -boundX) {
			if (transform.position.x < lookAt.position.x) {
				detla.x = detlaX - boundX;
			}
			else {
				detla.x = detlaX + boundX;
			} 
		}

		//check if we're inside the bounds on the Y axis
		float detlaY = lookAt.position.y - transform.position.y;
		if (detlaY > boundY || detlaY < -boundY) {
			if (transform.position.y < lookAt.position.y) {
				detla.y = detlaY - boundY;
			}
			else {
				detla.y = detlaY + boundY;
			} 
		}
		transform.position += new Vector3(detla.x, detla.y, 0);
	}
}
