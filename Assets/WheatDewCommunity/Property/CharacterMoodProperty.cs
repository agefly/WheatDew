﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoodProperty : MonoBehaviour
{
    public Dictionary<string, float> Mood = new Dictionary<string, float>();
    public HashSet<string> Expression = new HashSet<string>();
    public Dictionary<string, float> Tendency = new Dictionary<string, float>();
}
