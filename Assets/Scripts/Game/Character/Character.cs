using System;
using UnityEngine.Events;

namespace Characters
{
    public class Character : ICharacter, IDisposable
    {
        private readonly CharacterSaveLoader _characterSaveLoader;
        private readonly CharacterSO _characterSo;

        private readonly EatingProduct _favoriteFood;
        private readonly LocationSO _favoriteLocation;

        private readonly int _favoriteFoodModifair = 2;
        private readonly int _favoriteLocationModifair = 3;
        
        private readonly StaticData _staticData;

        private CharacterSympathy _sympathy;
        
        public bool IsMeetingWithPlayer { get; private set; }

        public CharacterSO ScriptableObject => _characterSo;
        
        public CharacterType Type => _characterSo.Type;
        public string Name => _characterSo.Name;

        public int SympathyPoints => _sympathy.Points;
        public int SympathyLevel => _sympathy.Level;

        public event UnityAction<int> SympathyPointsChanged;

        public Character(CharacterSO characterSO, StaticData staticData, SaveLoadServise saveLoadServise, int id)
        {
            _characterSo = characterSO;
            _staticData = staticData;

            _favoriteFood = characterSO.FavoriteFood;
            _favoriteLocation = characterSO.FavoriteLocation;

            IsMeetingWithPlayer = characterSO.IsMeetingWithPlayer;

            _sympathy = new CharacterSympathy(0,0, staticData);

            _characterSaveLoader = new CharacterSaveLoader(this, saveLoadServise, id);
            _characterSaveLoader.Load();
        }

        public void Load(SaveData.CharacterData characterData)
        {
            _sympathy = new CharacterSympathy(characterData.SympathyPoints, characterData.SympathyLevel, _staticData);
        }

        public void Dispose()
        {
            _characterSaveLoader.Save();
        }

        public void AccureSympathyPoints(int points)
        {
            if (points <= 0) throw new System.InvalidOperationException();
            _sympathy.AddPoints(points);

            SympathyPointsChanged?.Invoke(_sympathy.Points);
        }

        public void DecreesSympathyPoints(int points)
        {
            _sympathy.DecreesPoints(_sympathy.Points < points ? _sympathy.Points : points);

            SympathyPointsChanged?.Invoke(_sympathy.Points);
        }

        public void Feed(EatingProduct product)
        {
            int sympathyBonus = product.SympathyPointsBonus;

            if (product == _favoriteFood)
                sympathyBonus *= _favoriteFoodModifair;

            AccureSympathyPoints(sympathyBonus);
        }

        public void Invite(LocationSO location, int meetingSympathyBonus)
        {
            int sympathyBonus = meetingSympathyBonus;
                                                           
            if (location == _favoriteLocation)
                sympathyBonus *= _favoriteLocationModifair;
            
            AccureSympathyPoints(sympathyBonus);
        }

        public void MeetWithPlayer()
        {
            IsMeetingWithPlayer = true;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}