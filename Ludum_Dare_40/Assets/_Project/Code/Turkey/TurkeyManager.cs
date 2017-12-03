using Project.Game;
using Project.Terrain;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Turkey {
    public enum TurkeyState {
        Idle,
        Walking,
        Hungry,
        Eating,
        Drinking,
        Mating,
        Angry
    }

    public class TurkeyManager : MonoBehaviour {
        private const float SPEED = 0.5f;
        private const float LONGEST_RANDOM_WALK_DIRECTION = 1;

        private TurkeyState state;
        private TurkeyStats stats;

        //Counters
        private float stateCounter;
        private float stateCounterGoal;

        //Walking
        private bool isSearching;
        private Vector3 goalPosition;

        //Hunger
        private float hungerCounter;
        private Plant plant;

        private void Start() {
            stats = new TurkeyStats();
            state = TurkeyState.Idle;            
            stateCounter = 0;
            stateCounterGoal = 1;

            hungerCounter = 0;
        }

        private void Update() {
            updateHunger();
            checkStats();
            switch (state) {
                case TurkeyState.Idle:
                    idleState();
                    break;
                case TurkeyState.Walking:
                    walkingState();
                    break;
                case TurkeyState.Hungry:
                    hungryState();
                    break;
                case TurkeyState.Eating:
                    eatingState();
                    break;
                case TurkeyState.Drinking:
                    break;
                case TurkeyState.Mating:
                    break;
                case TurkeyState.Angry:
                    break;
            }
        }        

        private void checkStats() {
            if(stats.Hunger < 50 && state != TurkeyState.Hungry && state != TurkeyState.Eating && state != TurkeyState.Angry) {
                state = TurkeyState.Hungry;
                isSearching = true;
            }
        }

        private void updateHunger() {
            hungerCounter += Time.deltaTime;
            if(hungerCounter >= 1f/*5*/) {
                float rand = UnityEngine.Random.Range(0, 3);
                stats.SetHunger(stats.Hunger - rand);
                hungerCounter = 0;
            }
        }

        private bool applyTime() {
            stateCounter += Time.deltaTime;

            if(stateCounter >= stateCounterGoal) {
                stateCounter = 0;
                return true;
            }

            return false;
        }

        #region States
        private void idleState() {
            if(applyTime()) {
                //TODO: Figure out best thing to do
                state = TurkeyState.Walking;
                isSearching = true;
                stateCounter = 0;
            }
        }

        private void walkingState() {
            if(isSearching) {
                do {
                    float xPos = UnityEngine.Random.Range(transform.position.x - LONGEST_RANDOM_WALK_DIRECTION, transform.position.x + LONGEST_RANDOM_WALK_DIRECTION);
                    float yPos = UnityEngine.Random.Range(transform.position.y - LONGEST_RANDOM_WALK_DIRECTION, transform.position.y + LONGEST_RANDOM_WALK_DIRECTION);
                    goalPosition = new Vector3(xPos, yPos, 0);
                    isSearching = false;
                } while(World.Instance.IsInBounds(goalPosition));  
                
                float dis = goalPosition.x - transform.position.x;
                transform.GetComponent<SpriteRenderer>().flipX = (dis > 0) ? true : false;             
            }

            Vector3 direction = goalPosition - transform.position;
            transform.position += direction.normalized * SPEED * Time.deltaTime;

            if(Vector3.Distance(goalPosition, transform.position) <= 0.1f) {
                state = TurkeyState.Idle;
                stateCounter = 0;
                stateCounterGoal = UnityEngine.Random.Range(1, 3);
            }
        }

        private void hungryState() {
            //Find some food
            //Then switch to eating state
            if(isSearching) {
                plant = World.Instance.ClosestPlantToMe(transform.position);

                if(plant == null) {
                    state = TurkeyState.Angry;
                    Debug.Log("Turkey is Mad; time to go crazy!");
                } 

                isSearching = false;
            } else {
                //Go to the food source
                goalPosition = plant.GetPosition();
                float dis = goalPosition.x - transform.position.x;
                transform.GetComponent<SpriteRenderer>().flipX = (dis > 0) ? true : false;            
            

                Vector3 direction = goalPosition - transform.position;
                transform.position += direction.normalized * SPEED * Time.deltaTime;

                if(Vector3.Distance(goalPosition, transform.position) <= 0.25f) {
                    state = TurkeyState.Eating;
                }
            }            
        }

        private void eatingState() {
            throw new NotImplementedException();
        }
        #endregion

    }

    [Serializable]
    public class TurkeyStats {
        public float Hunger { get; private set;}
        public float Thirst { get; private set;}
        public float Mating { get; private set;}

        public TurkeyStats() {
            Hunger = 100;
            Thirst = 100;
            Mating = 100;
        }

        public void SetHunger(float Value) {
            Hunger = Value;
            Hunger = Mathf.Clamp(Hunger, 0 , 100);
        }

        public void SetThirst(float Value) {
            Thirst = Value;
            Thirst = Mathf.Clamp(Thirst, 0 , 100);
        }

        public void SetMating(float Value) {
            Mating = Value;
            Mating = Mathf.Clamp(Mating, 0 , 100);
        }
    }
}
