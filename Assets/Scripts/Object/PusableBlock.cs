﻿using UnityEngine;
using System.Collections;

public class PusableBlock:MonoBehaviour, IInteractable {
	private Transform targetPosition;

	private bool locked;

	void FixedUpdate() {
		if(locked)
			return;

		if(targetPosition != null) {
			transform.position = targetPosition.position;
			transform.rotation = targetPosition.rotation;
		}
	}

	public void Pickup(Transform position) {
		this.targetPosition = position;
	}

	public void Drop() {
		targetPosition = null;
	}

	public void Throw() {
		Drop();
	}

	public void Lock(bool locked) {
		this.locked = locked;

		Drop();
	}

	public InteractableType GetInteractableType() {
		return InteractableType.PushableBlock;
	}
}
