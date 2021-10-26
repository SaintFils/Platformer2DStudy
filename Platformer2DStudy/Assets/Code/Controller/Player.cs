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

        private float _walkSpeed = 10.0f;
        private float _animationSpeed = 8.0f;
        private float _movingThreshold = 0.1f;
        private float _jumpForce = 150.0f;
        private float _jumpThreshold = 1.0f;
        private float _groundLevel = 0.5f; //временное решение для изучения, будет заменено
        private float _xVelocity = 0f;
        private float _yVelocity = 0f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        private Vector3 _vectorRight = new Vector3(1, 0, 0);
        private Vector3 _vectorLeft = new Vector3(-1, 0, 0);
        private Vector3 _vectorUp = new Vector3(0, 1, 0);

        private LevelObjectView _view;
        private SpriteAnimationController _spriteAnimator;

        public Player(LevelObjectView player, SpriteAnimationController animator)
        {
            _view = player;
            _spriteAnimator = animator;
            _spriteAnimator.StartAnimation(_view.spriteRenderer, AnimState.Idle, true, _animationSpeed);
        }

        public void Tick()
        {
            _spriteAnimator.Tick();

            _xAxisInput = Input.GetAxis(Constants.Constants.StringConstants.Horizontal);
            _isMoving = Mathf.Abs(_xAxisInput) > _movingThreshold;
            /*if (Mathf.Abs(_xAxisInput) > _movingThreshold)
            {
                _isMoving = true;
            }*/

            _isJump = Input.GetAxis(Constants.Constants.StringConstants.Vertical) > 0;
            /*_yAxisInput = Input.GetAxis(Constants.Constants.StringConstants.Vertical);
            if (_yAxisInput > 0)
            {
                _isJump = true;
            }*/

            if (_isMoving)
            {
                Move();
                _spriteAnimator.StartAnimation(_view.spriteRenderer, AnimState.Run, true, _animationSpeed);
            }
            else
            {
                _spriteAnimator.StartAnimation(_view.spriteRenderer, AnimState.Idle, true, _animationSpeed);
            }

            if (IsGrounded())
            {
                if (_isJump && _yVelocity == 0)
                {
                    _yVelocity = _jumpForce;
                }
                else if(_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _view.transform.position = _view.transform.position.Change(y: _groundLevel);
                }

            }
            else
            {
                if (Mathf.Abs(_yVelocity) > _jumpThreshold)
                {
                    _spriteAnimator.StartAnimation(_view.spriteRenderer, AnimState.Jump, true, _animationSpeed);
                    _yVelocity += Constants.Constants.FloatConstants.G * Time.deltaTime;
                    _view.transform.position += Vector3.up /*_vectorUp*/ * (Time.deltaTime * _yVelocity);
                }
            }
        }

        public void Move()
        {
            _view.transform.position += _vectorRight * (Time.deltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1));
            if (_xAxisInput < 0)
            {
                //_view.transform.position += _vectorLeft * (Time.deltaTime * _walkSpeed);
                _view.transform.localScale = _leftScale;
            }
            else
            {
                //_view.transform.position += _vectorRight * (Time.deltaTime * _walkSpeed);
                _view.transform.localScale = _rightScale;
            }
        }

        public bool IsGrounded()
        {
            /*if (_view.transform.position.y <= _groundLevel && _yVelocity <=0)
            {
                return true;
            }
            return false;*/
            return _view.transform.position.y <= _groundLevel && _yVelocity <= 0;
        }
    }
}
