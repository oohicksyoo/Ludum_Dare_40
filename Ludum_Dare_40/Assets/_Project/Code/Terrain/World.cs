using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Utility;
using System;
using Project.InputControls;
using Project.Manager;
using Project.Turkey;

namespace Project.Terrain {
    public class World : Singleton<World> {

        [Header("Tiles")]
        [SerializeField]
        private Transform tileHolder;
        [SerializeField]
        private GameObject tilePrefab;

        [Header("Turkey")]
        [SerializeField]
        private GameObject turkeyPrefab;
        [SerializeField]
        private float startTurkeyAmount = 2;
        [SerializeField]
        private Vector2Int spawnTurkeyLocation = new Vector2Int();
        [SerializeField]
        private Transform turkeyParent;

        [Header("World Information")]
        [SerializeField]
        private int width = 10;
        [SerializeField]
        private int height = 10;

        private List<Tile> gameTiles;
        private List<TurkeyManager> turkeys;
        private Rect bounds;

		public void Start () {
            GameInput.Instance.OnTool += onToolEvent;
            GameInput.Instance.OnInteract += onInteractEvent;

            bounds = new Rect(1, 1, width - 1, height - 1);

            gameTiles = new List<Tile>();
            turkeys = new List<TurkeyManager>();
            generateWorld();
            spawnStartTurkeys();
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

        public bool IsInBounds(Vector3 Position) {
            if(bounds.Contains(Position)) {
                return true;
            }
            return false;
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

                if(x == 0 && y == 0) {
                    t.SetType(TileType.Fence_Corner_Top_Left);
                } else if(x == width - 1 && y == 0) {
                    t.SetType(TileType.Fence_Corner_Top_Right);
                } else if(x == 0 && y == height - 1) {
                    t.SetType(TileType.Fence_Corner_Bottom_Left);
                } else if(x == width - 1 && y == height - 1) {
                    t.SetType(TileType.Fence_Corner_Bottom_Right);
                } else if(y == 0 || y == width - 1) {
                    t.SetType(TileType.Fence_Horizontal);
                } else if(x == 0 || x == height - 1) {
                    t.SetType(TileType.Fence_Vertical);
                } else {
                    t.SetType(TileType.Grass);
                }

                

                
                t.SetPosition(new Vector2Int(x, y));

                gameTiles.Add(t);

                x++;
                if(x % width == 0) {
                    x = 0;
                    y++;
                }
            }
        }

        private void spawnStartTurkeys() {
            for (int i = 0; i < startTurkeyAmount; i++) {
                GameObject temp = Instantiate(turkeyPrefab, turkeyParent);
                temp.transform.position = new Vector3(spawnTurkeyLocation.x, spawnTurkeyLocation.y, 0);
                TurkeyManager tm = temp.GetComponent<TurkeyManager>();
                turkeys.Add(tm);
            }
        }

    }
}
