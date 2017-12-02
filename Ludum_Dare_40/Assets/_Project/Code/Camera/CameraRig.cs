using Project.InputControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Project.Manager;

namespace Project.Camera {
    public class CameraRig : MonoBehaviour {

		public void Start () {
            GameInput.Instance.OnMovement += onMovement;
		}

        private void onMovement(Vector2 obj) {
            transform.position = GameManager.Instance.Player.position + new Vector3(0, 0, -1);
        }
	}
}
