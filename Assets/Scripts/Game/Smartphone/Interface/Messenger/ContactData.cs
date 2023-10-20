using System;
using System.Collections.Generic;
using XNode;

public class ContactData
{
    public string Name { get; private set; }

    public ContactData(string name)
    {
        Name = name;
    }
}
