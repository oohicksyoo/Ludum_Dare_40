using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player {
    public class PlayerStats : Singleton<PlayerStats> {
        [SerializeField]
        private float movementSpeed = 1.0f;

        public float MovementSpeed() {
            return movementSpeed;
        }
    }
}
