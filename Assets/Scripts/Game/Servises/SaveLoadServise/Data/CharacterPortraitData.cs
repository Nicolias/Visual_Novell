using System;
using UnityEngine;
using UnityEngine.UI;

namespace SaveData
{
    [Serializable]
    public class CharacterPortraitData
    {
        public CharacterType CharacterType;

        public string Name;

        public Sprite Sprite;

        public Image Image;

        public CharacterPortraitPosition Position;

        public Vector2 PositionOffset;

        public Vector3 ScaleOffset;

        public bool IsIntaractable;

        public LocationSO Location;
    }
}