using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Terrain {
    public enum TileType {
        Grass = 0,
        FarmLand,
        Water,
        Fence_Vertical,
        Fence_Horizontal,
        Fence_Corner_Top_Left,
        Fence_Corner_Top_Right,
        Fence_Corner_Bottom_Left,
        Fence_Corner_Bottom_Right
    }

    public class Tile : MonoBehaviour {
        [SerializeField]
        private TileSetup[] tileSprites;

        private TileType tileType = TileType.Grass; 
        private Vector2Int position;  
        
        public void SetPosition(Vector2Int Vector) {
            position = Vector;
            transform.position = new Vector3(position.x, -position.y, 0);
        }

        public Vector3 GetPosition() {
            return transform.position;
        }

        public void SetType(TileType Type) {
            tileType = Type;
            GetComponent<SpriteRenderer>().sprite = tileSprites.Single(x => x.Type == tileType).Sprite;
        }   
        
        public TileType GetTileType() {
            return tileType;
        }  
	}

    [Serializable]
    public class TileSetup {
        public string TileName;
        public TileType Type;
        public Sprite Sprite;
    }
}
