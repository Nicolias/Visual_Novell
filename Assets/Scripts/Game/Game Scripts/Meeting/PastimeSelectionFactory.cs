using System;
using System.Collections.Generic;

public class PastimeSelectionFactory
{
    private readonly Meeting _meeting;
    private readonly ChoicePanel _choicePanel;

    private Dictionary<PastimeOnLocationType, AbstractPastime> _pastimesVariations;

    public PastimeSelectionFactory(Meeting meeting, ChoicePanel choicePanel, Dictionary<PastimeOnLocationType, AbstractPastime> pastimesVariations)
    {
        _meeting = meeting;
        _choicePanel = choicePanel;
        _pastimesVariations = pastimesVariations;
    }

    public event Action<AbstractPastime> PastimeSelected;

    public void Show(IEnumerable<PastimeOnLocationType> availablePastimesType)
    {
        List<ChoiseElement> availablePastimes = new List<ChoiseElement>();

        foreach (var availablePastimeType in availablePastimesType)
            if (_pastimesVariations.ContainsKey(availablePastimeType))
                availablePastimes.Add(CreateChoiceElement(_pastimesVariations[availablePastimeType]));

        availablePastimes.Add(new ChoiseElement("Закончить встречю.", () => _meeting.Exit()));

        _choicePanel.Show("Чем займемся?", availablePastimes);
    }

    private ChoiseElement CreateChoiceElement(AbstractPastime pastime)
    {
        ChoiseElement choiseElement = new ChoiseElement(pastime.Name, () => PastimeSelected?.Invoke(pastime));

        return choiseElement;
    }
}
