using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Utility {
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        public static T Instance { get; private set; }

        virtual public void Awake() {
            if (Instance != null) {
                DestroyImmediate(this);
                return;
            }
            Instance = this as T;
            DontDestroyOnLoad(this);
        }
	}
}
