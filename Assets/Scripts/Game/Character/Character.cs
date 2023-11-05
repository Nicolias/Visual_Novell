using System;
using UnityEngine;
using Dictionary;

namespace Characters
{
    [CreateAssetMenu(fileName = "Character", menuName = "Characters")]
    public class Character : ScriptableObject, IDataForCell
    {
        [SerializeField] private string _name;
        [SerializeField] private CharacterSympathy _sympathy;
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private DialogSpeechForMeeting _dialogSpeech;

        [field: SerializeField] public Dictionary<CharacterPoseType, Sprite> Images { get; private set; } 
        public CharacterType Type => _characterType;
        public int SympathyPoints => _sympathy.Points;
        public int SympathyLevel => _sympathy.Level;
        public string Name => _name;
        public DialogSpeechForMeeting DialogAfterMeeting => _dialogSpeech;


        public event Action<int> SympathyPointsChanged;

        public void AccureSympathyPoints(int points)
        {
            if (points <= 0) throw new InvalidOperationException();
            _sympathy.AddPoints(points);

            SympathyPointsChanged?.Invoke(_sympathy.Points);
        }

        public void DecreesSympathyPoints(int points)
        {
            _sympathy.DecreesPoints(_sympathy.Points < points ? _sympathy.Points : points);

            SympathyPointsChanged?.Invoke(_sympathy.Points);
        }
    }

    public enum CharacterPoseType
    { 
        sitting = 1,
        staying = 2
    }
}