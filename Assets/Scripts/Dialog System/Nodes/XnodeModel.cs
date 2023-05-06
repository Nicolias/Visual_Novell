using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

public class XnodeModel : Node
{
    [Input, SerializeField] private bool _inPut;
    [Output, SerializeField] private bool _outPut;
}
