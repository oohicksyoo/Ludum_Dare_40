using Project.Manager;
using Project.Terrain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player {
    public class PlayerManager : MonoBehaviour {

        [SerializeField]
        private GameObject eUI;

        private bool isColliding;

		public void Start () {
            isColliding = false;
            eUI.SetActive(false);
		}
		
		public void Update () {
            if(!isColliding) {
                bool isActive = false;
                Tile t = World.Instance.GetTileAtPosition(GameManager.Instance.Player.position);

                if(t.GetTileType() == TileType.FarmLand && !World.Instance.PlantAtPosition(t.GetPosition())) {
                    isActive = true;
                }

                eUI.SetActive(isActive);
            }
		}

        public void OnCollisionEnter2D(Collision2D collision) {
            Debug.Log("Collision");
            if(collision.gameObject.tag == "Barrel") {
                eUI.SetActive(true);
                isColliding = true;
            }            
        }

        public void OnCollisionExit2D(Collision2D collision) {
            if(collision.gameObject.tag == "Barrel") {
                eUI.SetActive(true);
                isColliding = false;
            }    
        }
    }
}
