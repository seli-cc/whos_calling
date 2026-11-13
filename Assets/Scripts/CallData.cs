
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCall", menuName = "Legit Or Shit/Call")]
public class CallData : ScriptableObject
{
    public string callerName = "Unknown Caller";
    [TextArea(2, 4)]
    public string openingMessage;

    public int startingNodeIndex = 0;

    public List<DialogueNode> nodes = new List<DialogueNode>();
}

[Serializable]
public class DialogueNode
{
    [TextArea(2, 5)]
    public string message;

    public List<DialogueOption> options =
        new List<DialogueOption>();
}

[Serializable]
public class DialogueOption
{
    public string optionText;

    // Index of the next dialogue node.
    // Use -1 to end the call.
    public int nextNodeIndex = -1;

    public int scoreChange = 0;

    public bool endsCall = false;

    [TextArea(2, 4)]
    public string feedback;
}
