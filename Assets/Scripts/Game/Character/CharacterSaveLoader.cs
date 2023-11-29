using JetBrains.Annotations;

namespace Characters
{
    public class CharacterSaveLoader : ISaveLoadObject
    {
        private readonly Character _character;
        private readonly SaveLoadServise _saveLoaderServise;

        private readonly string _saveKey;

        public CharacterSaveLoader(Character character, SaveLoadServise saveLoaderServise, int id)
        {
            _character = character;
            _saveLoaderServise = saveLoaderServise;

            _saveKey = $"CharacterSave{id}";
        }

        public void Save()
        {
            _saveLoaderServise.Save(_saveKey, new SaveData.CharacterData()
            {
                SympathyPoints = _character.SympathyPoints,
                SympathyLevel = _character.SympathyLevel,
                IsMeeting = _character.IsMeetingWithPlayer
            });
        }

        public void Load()
        {
            if(_saveLoaderServise.HasSave(_saveKey) == false) return;
            
            _character.Load(_saveLoaderServise.Load<SaveData.CharacterData>(_saveKey));
        }
    }
}