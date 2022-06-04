using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    MeshRenderer mRenderer;
    CapsuleCollider capsuleCollider;
    void Start()
    {
        mRenderer = GetComponent<MeshRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        mRenderer.enabled = true;
        capsuleCollider.enabled =true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player") {
            mRenderer.enabled = false;
            capsuleCollider.enabled =false;
        }
    }
}
