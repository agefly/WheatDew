﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class CharacterPrepareActsSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        
    }

    private void CharacterActJob()
    {
        Entities.ForEach((CharacterPrepareActsProperty p_PrepareActs,DialogueProperty p_dialogue) =>
        {
            if (p_PrepareActs.PrepareDialogueActs.Contains("回答"))
            {
                p_dialogue.dialogueChance = true;

            }
        });
    }
}
