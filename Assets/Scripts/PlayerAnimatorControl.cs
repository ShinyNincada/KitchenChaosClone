using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorControl : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    Player _player;
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _player = GetComponentInParent<Player>();  
        _animator.SetBool(IS_WALKING,  _player.IsWalking());
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool(IS_WALKING,  _player.IsWalking());
    }
}
