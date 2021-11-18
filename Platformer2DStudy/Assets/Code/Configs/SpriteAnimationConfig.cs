using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "SpriteAnimationConfig", menuName = "Configs/ Animation", order = 1)]
    public class SpriteAnimationConfig : ScriptableObject
    {
        [Serializable]
        public sealed class SpriteSequence
        {
            public AnimState track;
            public List<Sprite> sprites = new List<Sprite>();
        }

        public List<SpriteSequence> sequences = new List<SpriteSequence>();
    }

    public enum AnimState
    {
        Idle,
        Run,
        Jump
    }
}
