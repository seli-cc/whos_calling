using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "CallDatabase",
    menuName = "Legit Or Shit/Call Database")]
public class CallDatabase : ScriptableObject
{
    public List<CallData> calls = new List<CallData>();
}