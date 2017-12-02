using Project.Player;
using Project.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.InputControls {
    public class GameInput : Singleton<GameInput> {

        public Action<Vector2> OnMovement = delegate(Vector2 vect) { };
        public Action OnTool = delegate() { };
        public Action OnInteract = delegate() { };

        [Header("Movement")]
        [SerializeField]
        private KeyCode up = KeyCode.W;
        [SerializeField]
        private KeyCode down = KeyCode.S;
        [SerializeField]
        private KeyCode left = KeyCode.A;
        [SerializeField]
        private KeyCode right = KeyCode.D;

        [Header("Tool")]
        [SerializeField]
        private KeyCode tool = KeyCode.Q;

        [Header("Interaction")]
        [SerializeField]
        private KeyCode interact = KeyCode.E;

        public void Update () {
            movement();
            checkTool();
            checkInteract();
		}

        private void checkInteract() {
            if(Input.GetKeyDown(interact)) {
                OnInteract.Invoke();
            }
        }

        private void checkTool() {
            if(Input.GetKeyDown(tool)) {
                OnTool.Invoke();
            }
        }

        private void movement() {
            bool isVertical = false;
            bool isHorizontal = false;
            float speed = PlayerStats.Instance.MovementSpeed();
            Vector2 output = new Vector2();

            if(Input.GetKey(up)) {
                isVertical = true;
                output.y = speed;
            } else if(Input.GetKey(down)) {
                isVertical = true;
                output.y = -speed;
            }

            if (Input.GetKey(left)) {
                isHorizontal = true;
                output.x = -speed;
            } else if (Input.GetKey(right)) {
                isHorizontal = true;
                output.x = speed;
            }

            if(isVertical && isHorizontal) {
                output /= 2;
            }

            OnMovement.Invoke(output);
        }
    }
}
