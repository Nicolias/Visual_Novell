using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public class CharactersPortraiteData
    {
        public CharacterType CharacterType;

        public string Name;

        public Sprite Sprite;

        public CharacterPortraitPosition Position;
    }
}