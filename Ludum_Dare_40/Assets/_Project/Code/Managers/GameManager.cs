using Project.Player;
using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Manager {
    public class GameManager : Singleton<GameManager> {
        public Transform Player;

        private float time = 0;
        private bool isRunning = true;
        public void Update() {
            if(isRunning) {
                time += Time.deltaTime;
                if(time >= 1) {
                    time = 0;
                    PlayerStats.Instance.SetHealth(PlayerStats.Instance.GetHealth() - 5);

                    if(PlayerStats.Instance.GetHealth() == 0) {
                        isRunning = false;
                        PlayerStats.Instance.SetHealth(100);
                    }
                }
            }
        }
    }
}
