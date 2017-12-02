using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Utility;
using System;
using Project.InputControls;
using Project.Manager;

namespace Project.Terrain {
    public class World : Singleton<World> {

        [Header("Tiles")]
        [SerializeField]
        private Transform tileHolder;
        [SerializeField]
        private GameObject tilePrefab;

        [Header("World Information")]
        [SerializeField]
        private int width = 10;
        [SerializeField]
        private int height = 10;

        private List<Tile> gameTiles;

		public void Start () {
            GameInput.Instance.OnTool += onToolEvent;
            GameInput.Instance.OnInteract += onInteractEvent;

            gameTiles = new List<Tile>();
            generateWorld();
		}

        #region Events
        private void onToolEvent() {
            //Get tile the player is on
            //Change tile to new type (Farm land)
            //Error check if the tile cant be changed (Water)
            Tile t = getTileAtPosition(GameManager.Instance.Player.position);

            if(t.GetTileType() == TileType.Grass) {
                t.SetType(TileType.FarmLand);
            }
        }

        private void onInteractEvent() {
            //TODO: Based on tile we are on show UI events?
        }
        #endregion

        #region Utility
        private Tile getTileAtPosition(Vector3 Position) {
            Tile closest = gameTiles[0];
            float dist = Mathf.Infinity;

            foreach (Tile tile in gameTiles) {
                float d = Vector3.Distance(tile.GetPosition(), Position);
                if(d < dist) {
                    dist = d;
                    closest = tile;
                }
            }

            return closest;
        }
        #endregion

        private void generateWorld() {
            int tileAmount = width * height;
            int x = 0;
            int y = 0;

            for (int i = 0; i < tileAmount; i++) {
                GameObject temp = Instantiate(tilePrefab, tileHolder);
                temp.name = string.Format("[ {0},{1} ]", x, y);
                Tile t = temp.GetComponent<Tile>();
                t.SetType(TileType.Grass);
                t.SetPosition(new Vector2Int(x, y));

                gameTiles.Add(t);

                x++;
                if(x % width == 0) {
                    x = 0;
                    y++;
                }
            }
        }
    }
}
