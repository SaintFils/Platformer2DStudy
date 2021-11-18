using System;
using UnityEngine;

namespace Code.View
{
    public class LevelObjectView : MonoBehaviour
    {
        public Transform transform;
        public Collider2D collider;
        public SpriteRenderer spriteRenderer;
        public Rigidbody2D rigidbody;


        private void Start()
        {
            transform = GetComponent<Transform>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
