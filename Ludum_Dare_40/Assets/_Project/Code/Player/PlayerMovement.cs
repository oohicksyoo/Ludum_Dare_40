using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Project.InputControls;

namespace Project.Player {
    public class PlayerMovement : MonoBehaviour {

        [SerializeField]
        private Transform playerTrans;

		public void Start () {
            GameInput.Instance.OnMovement += onMovement;
		}

        private void onMovement(Vector2 obj) {
            Vector3 pos = new Vector3(obj.x, obj.y, 0);
            playerTrans.position += pos * Time.deltaTime;
        }
	}
}
