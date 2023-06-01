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
            if (_nodegraph.nodes[i] is PortraitModel)
            {
                (_nodegraph.nodes[i] as PortraitModel).Set(CharacterType.Dey, _name);
            }
        }
    }
}
