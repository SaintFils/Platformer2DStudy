using System;
using Code.Configs;
using Code.Controller;
using Code.View;
using UnityEngine;

namespace Code
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimationConfig playerConfig;
        [SerializeField] private float animationSpeed = 10.0f; //Потом будет перенесено в другой класс. Тут эта информация храниться не должна
        [SerializeField] private LevelObjectView playerView;

        private SpriteAnimationController _playerAnimator;

        private Player _playerController;

        private void Awake()
        {
            playerConfig = Resources.Load<SpriteAnimationConfig>("PlayerAnimConfig");

            if (playerConfig)
            {
                _playerAnimator = new SpriteAnimationController(playerConfig);
            }

            if (playerView)
            {
                _playerAnimator.StartAnimation(playerView.spriteRenderer, AnimState.Run, true, animationSpeed);
            }

            _playerController = new Player(playerView, _playerAnimator);
        }

        private void Update()
        {
           _playerController.Tick();
        }
    }
}
