using System.Collections.Generic;
using UnityEngine;

public class ChangeEnabledModel : XnodeModel
{
    [SerializeField] private List<SmartphoneWindows> _smartphoneWindows;
    [SerializeField] private List<bool> _smartphoneWindowsEnabled;

    public List<(SmartphoneWindows, bool)> Windows { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        if (_smartphoneWindows.Count != _smartphoneWindowsEnabled.Count)
            throw new System.InvalidProgramException("Несовпадения по размерам полей");

        Windows = new();

        for (int i = 0; i < _smartphoneWindows.Count; i++)
            Windows.Add((_smartphoneWindows[i], _smartphoneWindowsEnabled[i]));

        visitor.Visit(this);
    }
}