using System.Linq;

public class LocationSaveLoader : ISaveLoadObject
{
    private SaveLoadServise _saveLoadServise;
    private Location _location;
    private string _saveKey;

    public LocationSaveLoader(SaveLoadServise saveLoadServise, Location location, int id)
    {
        _saveLoadServise = saveLoadServise;
        _location = location;
        _saveKey = $"Location{id}";
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.LocationData()
        {
            Quest = _location.CurrentQuest,
            Items = _location.ItemsOnLocationData.ToList()
        });
    }

    public void Load()
    {
        if (_saveLoadServise.HasSave(_saveKey) == false) return;
        
        _location.Load(_saveLoadServise.Load<SaveData.LocationData>(_saveKey));
    }

    public void Add()
    {
        throw new System.NotImplementedException();
    }
}