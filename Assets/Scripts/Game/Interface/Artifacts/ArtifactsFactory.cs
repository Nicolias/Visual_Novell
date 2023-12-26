using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Factory.Artifacts
{
    public class ArtifactsFactory : ISaveLoadObject
    {
        private readonly LocationsManager _locationsManager;
        private readonly SaveLoadServise _saveLoadServise;

        private readonly CollectionPanel _collectionPanel;

        private readonly Artifact _artifactData;

        private readonly int _artifactsForCollectionCount = 5;
        private readonly string _saveKey = "artrifactsSave";

        private CollectionArtifactsQuest _collectionQuest;
        private int _collectedArtifactsCount = 0;

        private List<Artifact> _artifacts = new List<Artifact>();
                
        public ArtifactsFactory(LocationsManager locationsManager, SaveLoadServise saveLoadServise, CollectionPanel collectionPanel, Artifact artifactData)
        {
            _locationsManager = locationsManager;
            _saveLoadServise = saveLoadServise;

            _collectionPanel = collectionPanel;

            _artifactData = artifactData;

            if (_saveLoadServise.HasSave(_saveKey))
                Load();

            for (int i = 0; i < _artifactsForCollectionCount - _collectedArtifactsCount; i++)
                _artifacts.Add(artifactData);

            Add();
        }

        public event Action AllArtifactsCollected;

        public void Dispose()
        {
            if (_collectionQuest != null)
                _collectionQuest.ItemCollected -= OnItemCollected;
        }

        public void ResetCollection()
        {
            _collectedArtifactsCount = 0;

            List<ILocation> availableLocations = new List<ILocation>(_locationsManager.AvailableLocations.Where(location => location.Data.IsForArtifacts == true));

            if (availableLocations.Count == 0)
                return;

            foreach (var location in _locationsManager.AllLocations)
                location.DeleteArtifacts();

            for (int i = 0; i < _artifactsForCollectionCount - _collectedArtifactsCount; i++)
                _artifacts.Add(_artifactData);

            for (int i = 0; i < _artifactsForCollectionCount; i++)
            {
                int locationIndex = UnityEngine.Random.Range(0, availableLocations.Count);
                availableLocations[locationIndex].Add(_artifacts[i]);
                Debug.Log(availableLocations[locationIndex].Name);
                availableLocations.RemoveAt(locationIndex);
            }

            CreateCollectionQuest();
        }

        public void CreateCollectionQuest()
        {
            if (_collectionQuest != null)
                _collectionQuest.Dispose();

            if (_collectedArtifactsCount < _artifactsForCollectionCount)
            {
                _collectionQuest = new CollectionArtifactsQuest(_artifacts, _collectionPanel);
                _collectionQuest.ItemCollected += OnItemCollected;
            }
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

        public void Add()
        {
            _saveLoadServise.Add(this);
        }
    }
}