using System;
using System.Collections.Generic;

public class PastimeSelectionFactory
{
    private readonly ChoicePanel _choicePanel;

    private Dictionary<PastimeOnLocationType, AbstractPastime> _pastimesVariations;

    public PastimeSelectionFactory(ChoicePanel choicePanel, Dictionary<PastimeOnLocationType, AbstractPastime> pastimesVariations)
    {
        _choicePanel = choicePanel;
        _pastimesVariations = pastimesVariations;
    }

    public event Action<AbstractPastime> PastimeSelected;
    public event Action EndMeetingSelected;

    public void Show(IEnumerable<PastimeOnLocationType> availablePastimesType)
    {
        List<ChoiseElement> availablePastimes = new List<ChoiseElement>();

        foreach (var availablePastimeType in availablePastimesType)
            if (_pastimesVariations.ContainsKey(availablePastimeType))
                availablePastimes.Add(CreateChoiceElement(_pastimesVariations[availablePastimeType]));

        availablePastimes.Add(new ChoiseElement("Закончить встречю.", () => EndMeetingSelected?.Invoke()));

        _choicePanel.Show("Чем займемся?", availablePastimes);
    }

    private ChoiseElement CreateChoiceElement(AbstractPastime pastime)
    {
        ChoiseElement choiseElement = new ChoiseElement(pastime.Name, () => PastimeSelected?.Invoke(pastime));

        return choiseElement;
    }
}
