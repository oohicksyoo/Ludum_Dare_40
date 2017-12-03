using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Game {
    public class Plant : MonoBehaviour {

        private const float TIME_TO_GROW = 10;

        [HideInInspector]
        public bool IsDone {get; private set;}

        [SerializeField]
        private SpriteRenderer sprite;
        [SerializeField]
        private Sprite[] stages;

        private float time;
        private int stage;

		public void Start () {
            sprite.sprite = stages[0];
            time = 0;
            IsDone = false;
		}
		
		public void Update () {
            if(!IsDone) {
                time += Time.deltaTime;
                if(time >= TIME_TO_GROW / stages.Length) {
                    time = 0;
                    stage++;
                    sprite.sprite = stages[stage];

                    if(stage == stages.Length - 1) {
                        IsDone = true;
                    }
                }
            }
		}

        public Vector3 GetPosition() {
            return transform.position;
        }
	}
}
