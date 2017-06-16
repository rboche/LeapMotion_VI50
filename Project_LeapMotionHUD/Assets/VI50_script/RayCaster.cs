using UnityEngine;
using System.Collections;
using System;

public class RayCaster {

	public Vector3 Position;
	public Vector3 Direction;
	public float RayLength;
	public int LayerMask = 0;

	public event Action<Collider> OnRayEnter;
	public event Action<Collider> OnRayStay;
	public event Action<Collider> OnRayExit;

	Collider previous;
	RaycastHit hit = new RaycastHit();

	public bool CastRay() {
		Physics.Raycast(Position, Direction, out hit, RayLength, LayerMask);
		Debug.DrawRay (Position, -Direction,Color.red);
		ProcessCollision(hit.collider);
		return hit.collider != null ? true : false;
	}

	private void ProcessCollision(Collider current) {
		// No collision this frame.
		if (current == null) {
			// But there was an object hit last frame.
			if (previous != null) {
				DoEvent(OnRayExit, previous);
			}
		}

		// The object is the same as last frame.
		else if (previous == current) {
			DoEvent(OnRayStay, current);
		}

		// The object is different than last frame.
		else if (previous != null) {
			DoEvent(OnRayExit, previous);
			DoEvent(OnRayEnter, current);
		}

		// There was no object hit last frame.
		else {
			DoEvent(OnRayEnter, current);
		}

		// Remember this object for comparing with next frame.
		previous = current;
	}

	private void DoEvent(Action<Collider> action, Collider collider) {
		if (action != null) {
			action(collider);
		}
	}


}
