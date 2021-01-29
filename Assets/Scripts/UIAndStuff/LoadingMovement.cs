using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingMovement : MonoBehaviour {
	[SerializeField] private GameObject[] images;
	[SerializeField] private float moveSpeed;
	[SerializeField] private Transform leftPos;
	[SerializeField] private Transform rightPos;

	private void Update() {
		for (int i = 0; i < images.Length; i++) {
			Move(images[i]);
		}
	}

	private void Move(GameObject image) {
		if (image.transform.position.x >= rightPos.position.x) {
			image.transform.position = leftPos.position;
		}
		image.transform.position = new Vector3(image.transform.position.x + (moveSpeed * Time.deltaTime),
			image.transform.position.y, image.transform.position.z);
	}
}
