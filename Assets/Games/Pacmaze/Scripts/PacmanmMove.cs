﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanmMove : MonoBehaviour
{

    public float velocidade;
    public Vector2 direcao;
    public Collider2D colliderUp;
    public Collider2D colliderDown;
    public Collider2D colliderLeft;
    public Collider2D colliderRight;
    public Animator animator;

    public bool up;
    public bool down;
    public bool left;
    public bool right;

    // Use this for initialization
    void Start()
    {
        direcao = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        up = colliderUp.IsTouchingLayers();
        down = colliderDown.IsTouchingLayers();
        left = colliderLeft.IsTouchingLayers();
        right = colliderRight.IsTouchingLayers();

        if (direcao.Equals(Vector2.zero))
        {
            if (Input.GetKeyDown(Keys.left))
                if (!colliderLeft.IsTouchingLayers()) mover(Vector2.left);

            if (Input.GetKeyDown(Keys.right))
                if (!colliderRight.IsTouchingLayers()) mover(Vector2.right);

            if (Input.GetKeyDown(Keys.up))
                if (!colliderUp.IsTouchingLayers()) mover(Vector2.up);

            if (Input.GetKeyDown(Keys.down))
                if (!colliderDown.IsTouchingLayers()) mover(Vector2.down);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(direcao * velocidade);
        animator.SetFloat("x", direcao.x);
        animator.SetFloat("y", direcao.y);
    }

    private void mover(Vector2 direcao)
    {
        this.direcao = direcao;
    }

    private void parar()
    {
        direcao = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (
            direcao.y > 0 && colliderUp.IsTouchingLayers() ||
            direcao.y < 0 && colliderDown.IsTouchingLayers() ||
            direcao.x < 0 && colliderLeft.IsTouchingLayers() ||
            direcao.x > 0 && colliderRight.IsTouchingLayers()
        ) parar();
    }
}
