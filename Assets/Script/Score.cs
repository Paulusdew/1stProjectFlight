using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Score 
{
        public class Score : MonoBehaviour
    {
        int crashed =0;
        private void OnCollisionEnter(Collision other) 
        {
            if ( other.gameObject.tag != "Finish" && other.gameObject.tag != "Friendly" && other.gameObject.tag != "Fuel")
            {
                crashed++;
            }
            else if ( other.gameObject.tag == "Finish")
            {
                Debug.Log("You've crashed "+crashed+" times!");
            }
        }
    }
}
