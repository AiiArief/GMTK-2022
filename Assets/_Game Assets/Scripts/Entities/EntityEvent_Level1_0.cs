using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EntityEvent_Level1_0 : EntityEvent
{
    public override void EventOnLoadLevel()
    {
        base.EventOnLoadLevel();

        if (PlayerPrefs.GetInt(ProfileManager.PLAYERPREFS_CURRENTSCENECHECKPOINT) == 0)
        {
            AddBasicStatusEffectOnStartingEvent();
            player.animator.gameObject.SetActive(false);

            um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
            um.AddUIAction(() => { um.AddTutorial(new Tutorial(TutorialType.None, "The Casino - 0"), 3.0f); um.NextAction(); });
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
            um.AddUIAction(() => { em.triggerCheckpoints[0].teleportArea.gameObject.SetActive(true); um.NextAction(); });
            um.AddUIAction(() => StartCoroutine(um.AnimateTransition("flashbang")));
            um.AddUIAction(() => { player.animator.gameObject.SetActive(true); um.NextAction(); });
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(4.0f)));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
            um.AddUIAction(() => { GlobalGameManager.Instance.soundManager.PlayMusic(2); um.NextAction(); });

            um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
            return;
        }

        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { um.AddTutorial(new Tutorial(TutorialType.None, "The Casino - 0"), 5.0f); um.NextAction(); });
        um.AddUIAction(() => { GlobalGameManager.Instance.soundManager.PlayMusic(2, false); um.NextAction(); });
    }

    public void DoorNoSwitchEvent()
    {
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "There's no one in this door..."))));
        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
    }

    public void TeleportToLevel1_1()
    {
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
        um.AddUIAction(() => _TeleportPlayerToScene("Level 1-1"));
    }

    #region Open Door Event
    [SerializeField] CutsceneCamera m_entranceDoorEvent_1_Camera;
    public void EntranceDoorEvent_1(EntityCharacterNPC2D1BitDoor door)
    {
        m_entranceDoorEvent_1_Camera.UseCamera();
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.Hello); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "Welcome to the Casino!", door.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "...", door.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.Angry); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "Wait, you can't go to the Casino.", door.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "This place is only for 3D Sphere Robot!", door.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "3D Humanoid can't go inside!", door.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "And why are you dressing so fancy?", door.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "Did you think you can enter like that?", door.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "...", door.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.Idk); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "Err... Anyway, you can't pass.", door.voicePack))));

        um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.None); um.NextAction(); });
        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_entranceDoorEvent_1_Camera.ReleaseCamera(); um.NextAction(); });
    }

    [SerializeField] CutsceneCamera m_openDoorEvent_1_Camera;
    public void OpenDoorEvent_1(EntityCharacterNPC2D1BitDoor door)
    {
        m_openDoorEvent_1_Camera.UseCamera();
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.Zzz); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "Zzz... Sorry... staff only.", door.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "...", door.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "Zzz... If you roll the dices better than me, I'll let you pass.", door.voicePack),
                new DialogueChoice[2] {
                    new DialogueChoice("Roll the dices.", () => {
                        int npcRng = Random.Range(1, 7);
                        int pcRng = Random.Range(1, 7);
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "NPC dices : " + npcRng))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "PC dices : " + pcRng))));
                        if(pcRng >= npcRng)
                        {
                            string winStr = (pcRng > npcRng) ? "You win... As promised, I'll let you pass." : "It's a tie... whatever, I'll let you pass.";
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, winStr, door.voicePack))));
                            um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.None); um.NextAction(); });
                            um.AddUIAction(() => { door.SetDoorIsClosed(false); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(0.5f)));
                            um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_openDoorEvent_1_Camera.ReleaseCamera(); um.NextAction(); });
                        } else
                        {
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "Try again ...", door.voicePack))));
                            um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.None); um.NextAction(); });
                            um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_openDoorEvent_1_Camera.ReleaseCamera(); um.NextAction(); });
                        }
                    }),
                    new DialogueChoice("(There must be other way!)", () => {
                        um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.None); um.NextAction(); });
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_openDoorEvent_1_Camera.ReleaseCamera(); um.NextAction(); });
                    }),
                })));
    }

    [SerializeField] CutsceneCamera m_openDoorEvent_timing_1_Camera;
    public void OpenDoorEvent_Timing_1(EntityCharacterNPC2D1BitSwitch doorSwitch)
    {
        if (doorSwitch.hasAutoCloseEffect)
            return;

        m_openDoorEvent_timing_1_Camera.UseCamera();
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { doorSwitch.SetExpression(Expression_2D1Bit.Hello); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_SWITCH, "Hello! I'm guarding the door over there!", doorSwitch.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_SWITCH, "That door is staff only!", doorSwitch.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_SWITCH, "...", doorSwitch.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_SWITCH, "But you look like a staff from this casino!", doorSwitch.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_SWITCH, "Do you want me to open the door over there?", doorSwitch.voicePack),
                new DialogueChoice[2] {
                    new DialogueChoice("Yes please.", () => {
                        um.AddUIAction(() => { doorSwitch.SetExpression(Expression_2D1Bit.Cry); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_SWITCH, "Okay, but hurry up! It will close automatically!", doorSwitch.voicePack))));
                        um.AddUIAction(() => { doorSwitch.UseSwitch(40); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(0.25f)));
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_openDoorEvent_timing_1_Camera.ReleaseCamera(); um.NextAction(); });
                    }),
                    new DialogueChoice("Why are you here?", () => {
                        um.AddUIAction(() => { doorSwitch.SetExpression(Expression_2D1Bit.Idk); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_SWITCH, "Hmm... Idk lol.", doorSwitch.voicePack))));
                        um.AddUIAction(() => { doorSwitch.SetExpression(Expression_2D1Bit.None); um.NextAction(); });
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_openDoorEvent_timing_1_Camera.ReleaseCamera(); um.NextAction(); });
                    }),
                })));
    }

    [SerializeField] CutsceneCamera m_openDoorEvent_password_1_Camera;
    public void OpenDoorEvent_Password_1(EntityCharacterNPC2D1BitDoor door)
    {
        string key = ProfileManager.PLAYERPREFS_HAVEPASSWORD + "-" + SceneManager.GetActiveScene().name + "-1";
        bool haveKey = door.CheckPasswordWithKey(key);
        LocalizationString passwordChoice = haveKey ? LocalizationManager.UV1_PASSWORD_CHOICES[1] : LocalizationManager.UV1_PASSWORD_CHOICES[0];

        m_openDoorEvent_password_1_Camera.UseCamera();
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.Angry); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, "You can't pass here! Except you have password. Or maybe you want to roll the dices to pass here?", door.voicePack),
                new DialogueChoice[3] {
                    new DialogueChoice(passwordChoice, () => {
                        door.EnterPassword(new PasswordChoice[] {
                            new PasswordChoice(0, LocalizationManager.UV1_PASSWORD_ANSWER, new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, LocalizationManager.UV1_PASSWORD_QUESTION[0], door.voicePack), haveKey),
                            new PasswordChoice(1, LocalizationManager.UV1_PASSWORD_ANSWER, new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, LocalizationManager.UV1_PASSWORD_QUESTION[1], door.voicePack), haveKey),
                            new PasswordChoice(2, LocalizationManager.UV1_PASSWORD_ANSWER, new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, LocalizationManager.UV1_PASSWORD_QUESTION[2], door.voicePack), haveKey),
                        },
                        () => {
                            um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.Surprise); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, "The password... is true?", door.voicePack))));
                            um.AddUIAction(() => { door.SetDoorIsClosed(false); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(0.5f)));
                            um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_openDoorEvent_password_1_Camera.ReleaseCamera(); um.NextAction(); });
                        },
                        () => {
                            um.AddUIAction(() => { StartCoroutine(GlobalGameManager.Instance.soundManager.FadeOutMusic(1.0f)); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, "WRONG! GUARDS!!!!", door.voicePack),
                                    new DialogueChoice[2] {
                                        new DialogueChoice(LocalizationManager.CAPTURED_CHOICES_1, () => { _RetryLastCheckpointButton(); }),
                                        new DialogueChoice(LocalizationManager.CAPTURED_CHOICES_2, () => { _QuitButton(); })
                                    })));
                        });
                    }),
                    new DialogueChoice(LocalizationManager.UV1_PASSWORD_CHOICES[2], () => {
                        int npcRng = Random.Range(1, 7);
                        int pcRng = Random.Range(1, 7);
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "NPC dices : " + npcRng))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "PC dices : " + pcRng))));
                        if(pcRng > npcRng)
                        {
                            um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.Surprise); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "Y-You... win?", door.voicePack))));
                            um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.None); um.NextAction(); });
                            um.AddUIAction(() => { door.SetDoorIsClosed(false); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(0.5f)));
                            um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_openDoorEvent_password_1_Camera.ReleaseCamera(); um.NextAction(); });
                        } else
                        {
                            um.AddUIAction(() => { StartCoroutine(GlobalGameManager.Instance.soundManager.FadeOutMusic(1.0f)); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, "YOU LOSE! ... or tie.\nGUARDS!!!!", door.voicePack),
                                    new DialogueChoice[2] {
                                        new DialogueChoice(LocalizationManager.CAPTURED_CHOICES_1, () => { _RetryLastCheckpointButton(); }),
                                        new DialogueChoice(LocalizationManager.CAPTURED_CHOICES_2, () => { _QuitButton(); })
                                    })));
                        }
                    }),
                    new DialogueChoice(LocalizationManager.UV1_PASSWORD_CHOICES[3], () => {
                        um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.None); um.NextAction(); });
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_openDoorEvent_password_1_Camera.ReleaseCamera(); um.NextAction(); });
                    }),
                })));
    }

    [SerializeField] CutsceneCamera m_passwordTriggerEvent_1_Camera;
    [SerializeField] EntityCharacterNPC3DBotHeadphone m_passwordTriggerEvent_1_3DBH_1;
    [SerializeField] EntityCharacterNPC3DBotHeadphone m_passwordTriggerEvent_1_3DBH_2;
    public void PasswordTriggerEvent_1()
    {
        int eventId = 1;
        string key = ProfileManager.PLAYERPREFS_HAVEPASSWORD + "-" + SceneManager.GetActiveScene().name + "-" + eventId;

        m_passwordTriggerEvent_1_Camera.UseCamera();
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); player.animator.SetInteger("expression", 2); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "You're trying to listen to their conversation."))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Guard #1", "HEY! DID YOU FORGET TO SET PASSWORD ON THE DOOR?", m_passwordTriggerEvent_1_3DBH_1.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Guard #2", "WHAT? I CAN'T HEAR YOU!", m_passwordTriggerEvent_1_3DBH_2.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Guard #1", "WHAT DID YOU SAY? YOUR HEADPHONE IS TOO LOUD! I SAID DID YOU FORGET TO SET PASSWORD ON THE DOOR???", m_passwordTriggerEvent_1_3DBH_1.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Guard #2", "WHAT? THE PASSWORD ON THE DOOR IS 1 2 3!!", m_passwordTriggerEvent_1_3DBH_2.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Guard #1", "YOU SET 3 PASSWORDS FOR THE DOOR???", m_passwordTriggerEvent_1_3DBH_1.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Guard #2", "SPEAK LOUDER! I CAN'T HEAR YOU!", m_passwordTriggerEvent_1_3DBH_2.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));

        um.AddUIAction(() => { PlayerPrefs.SetString(key, true.ToString()); player.animator.SetInteger("expression", 0); um.NextAction(); });
        um.AddUIAction(() => { em.memoryTriggerEvents[eventId - 1].SetIsAvailable(false); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", LocalizationManager.UV1_MEMORY_REMEMBERED_0))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", LocalizationManager.UV1_MEMORY_REMEMBERED_1))));
        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_passwordTriggerEvent_1_Camera.ReleaseCamera(); um.NextAction(); });
    }
    #endregion
}
