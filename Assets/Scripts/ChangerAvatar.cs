using UnityEngine;
using XNode;

public class ChangerAvatar : MonoBehaviour
{
    [SerializeField] private NodeGraph _nodegraph;
    [SerializeField] private string _name;
    [SerializeField] public Sprite _sprite;

    private void Start()
    {
        for (int i = 0; i < _nodegraph.nodes.Count; i++)
        {
            if (_nodegraph.nodes[i] is DialogSpeechModel)
            {
                (_nodegraph.nodes[i] as DialogSpeechModel).SetAvatar(_sprite, _name);
            }
        }
    }
}
