using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Collectable : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Paddle")
            {
                ApplyEffect();
            }
            if(collision.tag == "Paddle"|| collision.tag == "DeathCollider")
            {
                Destroy(this.gameObject);
            }
        }

        protected abstract void ApplyEffect();
    }
}