using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Terrain {
    public enum TileType {
        Grass = 0,
        FarmLand,
        Water
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
        public TileType Type;
        public Sprite Sprite;
    }
}
