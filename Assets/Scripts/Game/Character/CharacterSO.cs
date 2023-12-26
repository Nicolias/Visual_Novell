using UnityEngine;
using Dictionary;
using UnityEngine.Events;

namespace Characters
{
    [CreateAssetMenu(fileName = "Character", menuName = "Characters")]
    public class CharacterSO : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private DialogSpeechForMeeting _dialogSpeech;

        [SerializeField] private EatingProduct _favoriteFood;
        [SerializeField] private LocationSO _favoriteLocation;
        
        [SerializeField] private bool _isMeetingWithPlayer = false;

        [field: SerializeField] public Dictionary<CharacterPoseType, Sprite> Images { get; private set; } 
        [field: SerializeField] public Dictionary<TimesOfDayType, LocationSO> CurrentLocation { get; private set; }

        public CharacterType Type => _characterType;
        
        public string Name => _name;
        public DialogSpeechForMeeting DialogAfterMeeting => _dialogSpeech;

        public EatingProduct FavoriteFood => _favoriteFood;
        public LocationSO FavoriteLocation => _favoriteLocation;

        public bool IsMeetingWithPlayer => _isMeetingWithPlayer;

        public event UnityAction<int> SympathyPointsChanged;
    }
}