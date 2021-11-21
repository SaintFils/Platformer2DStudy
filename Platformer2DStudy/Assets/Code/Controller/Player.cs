using Code.Configs;
using Code.Utils;
using Code.View;
using UnityEngine;

namespace Code.Controller
{
    public class Player
    {
        private float _xAxisInput;
        private float _yAxisInput;
        private bool _isJump;
        private bool _isMoving;

        private float _walkSpeed = 150.0f;
        private float _animationSpeed = 8.0f;
        private float _movingThreshold = 0.1f;
        private float _jumpForce = 5.0f;
        private float _jumpThreshold = 1.0f;
        private float _xVelocity = 0f;
        private float _yVelocity = 0f;

        private Vector2 _tempVelocity = Vector3.zero;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private LevelObjectView _view;
        private SpriteAnimationController _spriteAnimator;
        private readonly ContactPooler _contactPooler;

        public Player(LevelObjectView player, SpriteAnimationController animator)
        {
            _view = player;
            _spriteAnimator = animator;
            _spriteAnimator.StartAnimation(_view.spriteRenderer, AnimState.Idle, true, _animationSpeed);
            _contactPooler = new ContactPooler(_view.collider);
        }

        public void Tick()
        {
            _contactPooler.Tick();
            _spriteAnimator.Tick();

            _xAxisInput = Input.GetAxis(Constants.Constants.StringConstants.Horizontal);
            _isMoving = Mathf.Abs(_xAxisInput) > _movingThreshold;
            _isJump = Input.GetAxis(Constants.Constants.StringConstants.Vertical) > 0;

            if (_isMoving)
            {
                Move();
            }

            if (_contactPooler.IsGrounded)
            {
                _spriteAnimator.StartAnimation(_view.spriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);
                if (_isJump && Mathf.Abs(_view.rigidbody.velocity.y)<=_jumpThreshold)
                {
                    _view.rigidbody.AddForce(Vector2.up*_jumpForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                if (Mathf.Abs(_view.rigidbody.velocity.y) > _jumpThreshold)
                {
                    _spriteAnimator.StartAnimation(_view.spriteRenderer, AnimState.Jump, true, _animationSpeed);
                }
            }
        }

        private void Move()
        {
            _xVelocity = Time.fixedDeltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1);
            _view.rigidbody.velocity = _view.rigidbody.velocity.Change(x: _xVelocity);
            _view.transform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }

    }
}
