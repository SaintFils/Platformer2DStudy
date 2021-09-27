using System;
using System.Collections.Generic;
using Code.Configs;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Code.Controller
{
    public class SpriteAnimationController : IDisposable
    {
        private sealed class MyAnimation
        {
            public AnimState Track;
            public List<Sprite> Sprites;
            public bool Loop;
            public bool Sleep;
            public float Speed = 10.0f;
            public float Counter = 0.0f;

            public void Tick()
            {
                if (Sleep) return;

                Counter += Time.deltaTime * Speed;

                if (Loop)
                {
                    while (Counter > Sprites.Count)
                    {
                        Counter -= Sprites.Count;
                    }
                }
                else if(Counter > Sprites.Count)
                {
                    Counter = Sprites.Count;
                    Sleep = true;
                }
            }
        }

        private SpriteAnimationConfig _config;

        private Dictionary<SpriteRenderer, MyAnimation> _activeAnimations =
            new Dictionary<SpriteRenderer, MyAnimation>();

        public SpriteAnimationController(SpriteAnimationConfig config)
        {
            _config = config;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, AnimState track, bool loop, float speed) //it's start in video
        {
            if (_activeAnimations.TryGetValue(spriteRenderer, out var animation))
            {
                animation.Loop = loop;
                animation.Speed = speed;
                animation.Sleep = false;

                if (animation.Track != track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.sequences.Find(sequence => sequence.track == track).sprites;
                    animation.Counter = 0;
                }
            }
            else
            {
                _activeAnimations.Add(spriteRenderer, new MyAnimation()
                {
                    Track = track,
                    Loop = loop,
                    Speed = speed,
                    Sprites = _config.sequences.Find(sequence => sequence.track == track).sprites
                });
            }
        }

        public void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimations.ContainsKey(sprite))
            {
                _activeAnimations.Remove(sprite);
            }
        }

        public void Tick() // it's update in video
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.Tick();

                if (animation.Value.Counter < animation.Value.Sprites.Count)
                {
                    animation.Key.sprite = animation.Value.Sprites[(int) animation.Value.Counter];
                }
            }
        }

        public void Dispose()
        {
            _activeAnimations.Clear();
        }
    }
}
