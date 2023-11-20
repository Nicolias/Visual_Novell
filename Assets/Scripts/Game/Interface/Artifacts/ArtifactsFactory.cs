using System;
using System.Collections.Generic;
using UnityEngine;

namespace Factory.Artifacts
{
    public class ArtifactsFactory : ISaveLoadObject
    {
        private readonly LocationsManager _locationsManager;
        private readonly SaveLoadServise _saveLoadServise;

        private readonly CollectionPanel _collectionPanel;

        private readonly int _artifactsForCollectionCount = 5;
        private readonly string _saveKey = "artrifactsSave";

        private CollectionArtifactsQuest _collectionQuest;
        private int _collectedArtifactsCount = 0;

        private List<Artifact> _artifacts = new List<Artifact>();
                
        public ArtifactsFactory(LocationsManager locationsManager, SaveLoadServise saveLoadServise, CollectionPanel collectionPanel)
        {
            _locationsManager = locationsManager;
            _saveLoadServise = saveLoadServise;

            _collectionPanel = collectionPanel;

            if (_saveLoadServise.HasSave(_saveKey))
                Load();
        }

        public event Action AllArtifactsCollected;

        public void CreateCollectionQuest(Artifact artifactData)
        {
            _collectedArtifactsCount = 0;

            List<Location> availableLocations = new List<Location>(_locationsManager.AvailableLocations);

            if (availableLocations.Count == 0)
                return;

            for (int i = 0; i < _artifactsForCollectionCount - _collectedArtifactsCount; i++)
                _artifacts.Add(artifactData);

            foreach (var location in _locationsManager.AllLocations)
                location.DeleteArtifacts();

            for (int i = 0; i < _artifactsForCollectionCount - _collectedArtifactsCount; i++)
            {
                int locationIndex = UnityEngine.Random.Range(0, availableLocations.Count);
                availableLocations[locationIndex].Add(artifactData);
                Debug.Log(availableLocations[locationIndex].Name);
                availableLocations.RemoveAt(locationIndex);
            }

            _collectionQuest = new CollectionArtifactsQuest(_artifacts, _collectionPanel);
            _collectionQuest.ItemCollected += OnItemCollected;
        }

        private void OnItemCollected(Artifact itemForCollection)
        {
            if (_artifacts.Contains(itemForCollection))
                _collectedArtifactsCount++;
            else
                throw new InvalidOperationException();

            if (_collectedArtifactsCount == _artifactsForCollectionCount)
            {
                _collectionQuest.ItemCollected -= OnItemCollected;;
                AllArtifactsCollected?.Invoke();
            }
        }

        public void Save()
        {
            _saveLoadServise.Save(_saveKey, new SaveData.IntData() { Int = _collectedArtifactsCount });
        }

        public void Load()
        {
            _collectedArtifactsCount = _saveLoadServise.Load<SaveData.IntData>(_saveKey).Int;
        }
    }
}