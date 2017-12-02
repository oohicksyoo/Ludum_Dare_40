using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Player {
    public class PlayerStats : Singleton<PlayerStats> {
        [SerializeField]
        private float movementSpeed = 1.0f;

        [Header("Hearts")]
        [SerializeField]
        private Image[] hearts;
        [SerializeField]
        private Sprite[] heartPieces;

        private float health = 100;
        private float amountPerHeart;
        private float amountPerPiece;

        public void Start() {
            amountPerHeart = 100 / hearts.Length;
            amountPerPiece = amountPerHeart / 4;
        }

        public float MovementSpeed() {
            return movementSpeed;
        }

        public float GetHealth() {
            return health;
        }

        public void SetHealth(float Value) {
            health = Value;
            health = Mathf.Clamp(health, 0 , 100);

            //TODO: Determine how many hearts should show
            //TODO: What sub heart should show on the last one

            int heartsShowing = (int)(health / amountPerHeart);
            float remainder = (health - (heartsShowing * amountPerHeart));
            int pieces = (int)(remainder / amountPerPiece);
            Debug.Log(string.Format("Hearts Showing: {0} | {1} | {2}", heartsShowing, remainder, pieces));
            
            for (int i = 0; i < heartsShowing; i++) {
                hearts[i].sprite = heartPieces[hearts.Length - 1];
            }

            if(heartsShowing != hearts.Length) {
                hearts[heartsShowing].sprite = heartPieces[pieces];

                for (int i = heartsShowing + 1; i < hearts.Length; i++) {
                    hearts[i].sprite = heartPieces[0];
                }
            }
        }
    }
}
