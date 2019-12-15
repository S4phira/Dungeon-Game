﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter {

	protected BoxCollider2D boxCollider;
	protected Vector3 moveDelta;
	protected RaycastHit2D hit;
	protected float ySpeed = 0.75f;
	protected float xSpeed = 1.0f;
	public Animator animator;

	protected virtual void Start() {
		boxCollider = GetComponent<BoxCollider2D>();
	}
	protected virtual void UpdateMotor(Vector3 input){
		animator.SetFloat("speed_x", Mathf.Abs(Input.GetAxis("Horizontal")));
		animator.SetFloat("speed_y", Mathf.Abs(Input.GetAxis("Vertical")));
			//Reset moveDelta
		moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

		// move sprite
		if (moveDelta.x > 0) 
			transform.localScale = new Vector3(1,1,1);
		else if (moveDelta.x < 0 ) 
			transform.localScale =  new Vector3(-1,1,1);


		// add push vector
		moveDelta +=pushDirection;

		//reduce push force every frame
		pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);


	//	transform.Translate( moveDelta * Time.deltaTime);


		hit = Physics2D.BoxCast (transform.position, boxCollider.size, 0, new Vector2 (0, moveDelta.y), Mathf.Abs (moveDelta.y * Time.deltaTime), LayerMask.GetMask ("Actor", "Blocking"));
		if (hit.collider == null) {
			transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
		}

		hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
		if (hit.collider == null) {
			transform.Translate( moveDelta.x * Time.deltaTime, 0, 0);
		}
	}
}
