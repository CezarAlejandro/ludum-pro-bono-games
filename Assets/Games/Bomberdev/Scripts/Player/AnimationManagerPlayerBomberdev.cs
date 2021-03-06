﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagerPlayerBomberdev : MonoBehaviour {
    private Animator animator;
    private Rigidbody2D rigidbody2D;
    
    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (rigidbody2D.velocity == Vector2.zero) {
            animator.SetBool("stopped", true);
            animator.enabled = false;
        } else {
            animator.enabled = true;
            animator.SetBool("stopped", false);
            animator.SetFloat("velocityX", rigidbody2D.velocity.x);
            animator.SetFloat("velocityY", rigidbody2D.velocity.y);
        }
    }
}
