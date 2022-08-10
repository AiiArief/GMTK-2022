using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EntityEvent_Level1_1 : EntityEvent
{
    public override void EventOnLoadLevel()
    {
        base.EventOnLoadLevel();

        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { um.AddTutorial(new Tutorial(TutorialType.None, "The Casino - 1"), 5.0f); um.NextAction(); });
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

    public void TutorialSkipTurn()
    {
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { um.AddTutorial(new Tutorial(TutorialType.MoveMod, LocalizationManager.TUTORIAL_SKIP), 10.0f); um.NextAction(); });
    }

    #region Open Door Event
    [SerializeField] CutsceneCamera m_closeDoorEvent_1_Camera;
    bool m_closeDoorEvent_1_firstTime = true;
    public void CloseDoorEvent_1(EntityCharacterNPC2D1BitSwitch doorSwitch)
    {
        if (m_closeDoorEvent_1_firstTime)
        {
            m_closeDoorEvent_1_Camera.UseCamera();
            um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
            um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
            um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
            um.AddUIAction(() => { doorSwitch.SetExpression(Expression_2D1Bit.Hello); um.NextAction(); });
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_SWITCH, LocalizationManager.UV1_CLOSEDOOR1_0, doorSwitch.voicePack),
                    new DialogueChoice[2] {
                    new DialogueChoice(LocalizationManager.UV1_CLOSEDOOR1_0_CHOICES_0, () => {
                        um.AddUIAction(() => { doorSwitch.SetExpression(Expression_2D1Bit.Idk); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_SWITCH, LocalizationManager.UV1_CLOSEDOOR1_0_CHOICES_0_0, doorSwitch.voicePack))));
                        um.AddUIAction(() => { doorSwitch.UseSwitch(); m_closeDoorEvent_1_firstTime = false; um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(0.5f)));
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_closeDoorEvent_1_Camera.ReleaseCamera(); um.NextAction(); });
                    }),
                    new DialogueChoice(LocalizationManager.UV1_CLOSEDOOR1_0_CHOICES_1, () => {
                        um.AddUIAction(() => { doorSwitch.SetExpression(Expression_2D1Bit.None); um.NextAction(); });
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_closeDoorEvent_1_Camera.ReleaseCamera(); um.NextAction(); });
                    }),
            })));
        }
        else
        {
            doorSwitch.UseSwitch();
        }
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
        um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.ThumbUp); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, "Well, sorry. You can't pass here! Except you have password. Or... by playing dice with me.", door.voicePack),
                new DialogueChoice[3] {
                    new DialogueChoice(passwordChoice, () => {
                        door.EnterPassword(new PasswordChoice[] {
                            new PasswordChoice(2, LocalizationManager.UV1_PASSWORD_ANSWER, new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, LocalizationManager.UV1_PASSWORD_QUESTION[0], door.voicePack), haveKey),
                            new PasswordChoice(0, LocalizationManager.UV1_PASSWORD_ANSWER, new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, LocalizationManager.UV1_PASSWORD_QUESTION[1], door.voicePack), haveKey),
                            new PasswordChoice(2, LocalizationManager.UV1_PASSWORD_ANSWER, new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, LocalizationManager.UV1_PASSWORD_QUESTION[2], door.voicePack), haveKey),
                        },
                        () => {
                            um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.Surprise); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, "Well, you're right!", door.voicePack))));
                            um.AddUIAction(() => { door.SetDoorIsClosed(false); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(0.5f)));
                            um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_openDoorEvent_password_1_Camera.ReleaseCamera(); um.NextAction(); });
                        },
                        () => {
                            um.AddUIAction(() => { StartCoroutine(GlobalGameManager.Instance.soundManager.FadeOutMusic(1.0f)); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, "Nope, wrong! Sorry, you should go with the guards! Bye Bye!", door.voicePack),
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
                            um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.Cry); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "I LOSEEE, WHYYYYYYY!", door.voicePack))));
                            um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.None); um.NextAction(); });
                            um.AddUIAction(() => { door.SetDoorIsClosed(false); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(0.5f)));
                            um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_openDoorEvent_password_1_Camera.ReleaseCamera(); um.NextAction(); });
                        } else
                        {
                            um.AddUIAction(() => { StartCoroutine(GlobalGameManager.Instance.soundManager.FadeOutMusic(1.0f)); um.NextAction(); });
                            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOORPASSWORD, "Pfft. Noob. Go play dice with the guards!", door.voicePack),
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
    public void PasswordTriggerEvent_1()
    {
        int eventId = 1;
        string key = ProfileManager.PLAYERPREFS_HAVEPASSWORD + "-" + SceneManager.GetActiveScene().name + "-" + eventId;

        m_passwordTriggerEvent_1_Camera.UseCamera();
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); player.animator.SetInteger("expression", 2); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "You're trying to listen what dice man say."))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));;
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Dice man", "If you want to get information, you have to roll the dice first.", m_passwordTriggerEvent_1_3DBH_1.voicePack),
            new DialogueChoice[2] {
                new DialogueChoice("Okay.", ()=> {
                    int pcRng = Random.Range(1, 7);
                    um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                    um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
                    um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                    um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "PC dices : " + pcRng))));
                    um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                    um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
                    um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                    if(pcRng == 5 || pcRng == 6)
                    {
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Dice man", "I see, you got "+ pcRng+".", m_passwordTriggerEvent_1_3DBH_1.voicePack))));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Dice man", "Well, you got information number 3.", m_passwordTriggerEvent_1_3DBH_1.voicePack))));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Dice man", "The password for the door is 3 1 3.", m_passwordTriggerEvent_1_3DBH_1.voicePack))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));

                        um.AddUIAction(() => { PlayerPrefs.SetString(key, true.ToString()); player.animator.SetInteger("expression", 0); um.NextAction(); });
                        um.AddUIAction(() => { em.memoryTriggerEvents[eventId - 1].SetIsAvailable(false); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", LocalizationManager.UV1_MEMORY_REMEMBERED_0))));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", LocalizationManager.UV1_MEMORY_REMEMBERED_1))));
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_passwordTriggerEvent_1_Camera.ReleaseCamera(); um.NextAction(); });
                    } else if(pcRng == 3 || pcRng == 4)
                    {
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Dice man", "I see, you got "+ pcRng+".", m_passwordTriggerEvent_1_3DBH_1.voicePack))));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Dice man", "Well, you got information number 2.", m_passwordTriggerEvent_1_3DBH_1.voicePack))));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Dice man", "This is the value if you print Mathf.Infinity: " + Mathf.Infinity, m_passwordTriggerEvent_1_3DBH_1.voicePack))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                        um.AddUIAction(() => { PlayerPrefs.SetString(key, true.ToString()); player.animator.SetInteger("expression", 0); um.NextAction(); });
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_passwordTriggerEvent_1_Camera.ReleaseCamera(); um.NextAction(); });
                    }else
                    {
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Dice man", "I see, you got "+ pcRng+".", m_passwordTriggerEvent_1_3DBH_1.voicePack))));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Dice man", "Well, you got information number 1.", m_passwordTriggerEvent_1_3DBH_1.voicePack))));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(3D Headphone Sphere Robot) Dice man", "The president went to Saw Con yesterday.", m_passwordTriggerEvent_1_3DBH_1.voicePack))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
                        um.AddUIAction(() => { PlayerPrefs.SetString(key, true.ToString()); player.animator.SetInteger("expression", 0); um.NextAction(); });
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_passwordTriggerEvent_1_Camera.ReleaseCamera(); um.NextAction(); });
                    }
                }),
                new DialogueChoice("Nah.", ()=> {
                    um.AddUIAction(() => { PlayerPrefs.SetString(key, true.ToString()); player.animator.SetInteger("expression", 0); um.NextAction(); });
                    um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_passwordTriggerEvent_1_Camera.ReleaseCamera(); um.NextAction(); });
                })
            })));
    }

    [SerializeField] CutsceneCamera m_entranceDoorEvent_1_Camera;
    public void ExitDoorEvent_1(EntityCharacterNPC2D1BitDoor door)
    {
        m_entranceDoorEvent_1_Camera.UseCamera();
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.Hello); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "Hello!", door.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "You can't pass here!", door.voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_2D1BIT_DOOR, "Because this is the exit!", door.voicePack))));

        um.AddUIAction(() => { door.SetExpression(Expression_2D1Bit.None); um.NextAction(); });
        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_entranceDoorEvent_1_Camera.ReleaseCamera(); um.NextAction(); });
    }

    [SerializeField] CutsceneCamera m_talkEvent_Camera;
    public void TalkEvent(EntityCharacterNPC2DHumanoidBobaKotakLaptop bobaKotak)
    {
        m_talkEvent_Camera.UseCamera(0);
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "This is probably the president's personal laptop.", bobaKotak.voicePack),
        new DialogueChoice[3] {
                    new DialogueChoice(LocalizationManager.UV2_TALK_CHOICES_1, () => {
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "Checking president's browsing history..."))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "The president likes to open NHenTie site."),
                            new DialogueChoice[4]
                            {
                                new DialogueChoice("(Annoyed)", () => player.animator.SetInteger("expression", 1)),
                                new DialogueChoice("(Smirk)", () => player.animator.SetInteger("expression", 3)),
                                new DialogueChoice("(Surprised)", () => player.animator.SetInteger("expression", 4)),
                                new DialogueChoice("...", () => player.animator.SetInteger("expression", 0)),
                            }
                            )));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
                        um.AddUIAction(() => { player.animator.SetInteger("expression", 0); um.NextAction(); });
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_talkEvent_Camera.ReleaseCamera(); um.NextAction(); });
                    }),
                    new DialogueChoice("Look at president's diary.", () => {
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", LocalizationManager.UV2_TALK_1_0))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
                        um.AddUIAction(() => { bobaKotak.GetComponent<AudioSource>().Play(); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(2D Humanoid) The President", "July, 17."))));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(2D Humanoid) The President", "I kidnapped the so called dice man."))));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(2D Humanoid) The President", "Reason? Uhh... Idk lol. Somehow I have to do that."))));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("(2D Humanoid) The President", "But suprisingly I can't control dice man, so I put dice man in the cell over there."))));
                        um.AddUIAction(() => { bobaKotak.GetComponent<AudioSource>().Stop(); um.NextAction(); });

                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."),
                            new DialogueChoice[4]
                            {
                                new DialogueChoice("(Annoyed)", () => player.animator.SetInteger("expression", 1)),
                                new DialogueChoice("(Smirk)", () => player.animator.SetInteger("expression", 3)),
                                new DialogueChoice("(Surprised)", () => player.animator.SetInteger("expression", 4)),
                                new DialogueChoice("...", () => player.animator.SetInteger("expression", 0)),
                            }
                            )));

                        string key = ProfileManager.PLAYERPREFS_HAVEPASSWORD + "-" + SceneManager.GetActiveScene().name + "-2";
                        um.AddUIAction(() => { PlayerPrefs.SetString(key, true.ToString()); um.NextAction(); });

                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
                        um.AddUIAction(() => { player.animator.SetInteger("expression", 0); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
                        um.AddUIAction(() => { player.animator.SetInteger("expression", 3); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "This probably can be evidence of something!"))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
                        um.AddUIAction(() => { player.animator.SetInteger("expression", 0); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", LocalizationManager.UV2_TALK_1_22))));
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
                        um.AddUIAction(() => { player.animator.SetInteger("expression", 4); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "!"))));
                        um.AddUIAction(() => { em.triggerCheckpoints[1].teleportArea.gameObject.SetActive(true); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.AnimateTransition("flashbang")));
                        um.AddUIAction(() => { player.animator.gameObject.SetActive(false); um.NextAction(); });
                        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(4.0f)));
                        um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
                        um.AddUIAction(() => SceneManager.LoadScene("Void World"));
                    }),
                    new DialogueChoice(LocalizationManager.UV2_TALK_CHOICES_3, () => {
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_talkEvent_Camera.ReleaseCamera(); um.NextAction(); });
                    }),
        })));
    }
    #endregion
}
