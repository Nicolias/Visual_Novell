using UnityEngine;

namespace Factory
{
    public class ChoicePanelFactory : MonoBehaviour, IChoicePanelFactory
    {
        [SerializeField] private ChoicePanel _choicePanelTemplate;

        public ChoicePanel CreateChoicePanel(Transform containter)
        {
            return Instantiate(_choicePanelTemplate, containter);
        }
    }
}