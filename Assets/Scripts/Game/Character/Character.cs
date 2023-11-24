using UnityEngine;
using Dictionary;
using UnityEngine.Events;

namespace Characters
{
    [CreateAssetMenu(fileName = "Character", menuName = "Characters")]
    public class Character : ScriptableObject, IDataForCell
    {
        [SerializeField] private string _name;
        [SerializeField] private CharacterSympathy _sympathy;
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private DialogSpeechForMeeting _dialogSpeech;

        [SerializeField] private EatingProduct _favoriteFood;
        [SerializeField] private Location _favoriteLocation;

        private int _favoriteFoodModifair = 2;
        private int _favoriteLocationModifair = 3;

        [field: SerializeField] public Dictionary<CharacterPoseType, Sprite> Images { get; private set; } 
        [field: SerializeField] public Dictionary<TimesOfDayType, Location> CurrentLocation { get; private set; }

        public CharacterType Type => _characterType;
        public int SympathyPoints => _sympathy.Points;
        public int SympathyLevel => _sympathy.Level;
        public string Name => _name;
        public DialogSpeechForMeeting DialogAfterMeeting => _dialogSpeech;


        public event UnityAction<int> SympathyPointsChanged;

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

        public void Invite(Location location, int meetingSympathyBonus)
        {
            int sympathyBonus = meetingSympathyBonus;

            if (location == _favoriteLocation)
                sympathyBonus *= _favoriteLocationModifair;

            AccureSympathyPoints(sympathyBonus);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}