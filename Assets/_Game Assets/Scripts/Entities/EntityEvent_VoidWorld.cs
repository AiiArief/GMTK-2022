using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EntityEvent_VoidWorld : EntityEvent
{
    public override void EventOnLoadLevel()
    {
        _BasicOnLoadLevel();
        AddBasicStatusEffectOnStartingEvent();

        #region End Game
        string evidence = PlayerPrefs.GetString(ProfileManager.PLAYERPREFS_HAVEPASSWORD + "-Level 1-1-2", false.ToString());
        if (evidence == true.ToString())
        {
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
            um.AddUIAction(() => { GlobalGameManager.Instance.soundManager.PlayMusic(0); um.NextAction(); });
            um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_ENDGAME_0, m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "I see that you have found evidence... of something.", m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "...", m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f))); 
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "Hmm...", m_voicePack))));
            um.AddUIAction(() => { StartCoroutine(GlobalGameManager.Instance.soundManager.FadeOutMusic(1.0f)); um.NextAction(); });
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));

            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_ENDGAME_18, m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_ENDGAME_19, m_voicePack))));
            um.AddUIAction(() => { GlobalGameManager.Instance.soundManager.PlayMusic(2); um.NextAction(); });
            um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "Developed by Narendra Arief Nugraha"))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "Powered by Unity Engine"))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "Music :\nKevin Macleod - Frost Waltz, Teddy Bear Waltz, Spy Glass"))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "Audio & Sound :\nRPG Maker MV\nMechvibes.com - jsfxr"))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "Animation :\nMixamo"))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "3D Tools :\nVROID\nUnity Pro Builder"))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "2D Tools :\nAseprite - GIMP\nMedibang - Inkscape"))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));

            um.AddUIAction(() => { StartCoroutine(GlobalGameManager.Instance.soundManager.FadeOutMusic(1.0f)); um.NextAction(); });
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_ENDGAME_20, m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_ENDGAME_21, m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_ENDGAME_22, m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_ENDGAME_23, m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_ENDGAME_24, m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_ENDGAME_25, m_voicePack))));

            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_ENDGAME_26, m_voicePack),
                new DialogueChoice[2] {
                    new DialogueChoice(LocalizationManager.VW_ONLOAD_ENDGAME_26_0, () => {
                        GlobalGameManager.Instance.profileManager.ClearProfile();
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }),
                    new DialogueChoice(LocalizationManager.VW_ONLOAD_ENDGAME_26_1, () => {
                        um.AddUIAction(() => Application.Quit());
                    })
                })));
            return;
        }
        #endregion

        um.AddUIAction(() => StartCoroutine(um.AnimateTransition("logoSSV", 5.0f)));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));

        #region Tengah Game
        string currentScene = PlayerPrefs.GetString(ProfileManager.PLAYERPREFS_CURRENTSCENE, "Void World");
        if (currentScene != "Void World")
        {
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_LOADGAME, m_voicePack),
                    new DialogueChoice[3] {
                        new DialogueChoice(LocalizationManager.VW_ONLOAD_LOADGAME_CHOICES_1, () => _LoadGameButton(currentScene)),
                        new DialogueChoice(LocalizationManager.VW_ONLOAD_LOADGAME_CHOICES_2, () => _ClearSaveGameButton(currentScene)),
                        new DialogueChoice(LocalizationManager.VW_ONLOAD_LOADGAME_CHOICES_3, () => _QuitButton(false))
                    })));
            return;
        }
        #endregion

        #region Start Game
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_MYSTERIOUSVOICES, LocalizationManager.VW_ONLOAD_0, m_voicePack),
                new DialogueChoice[1] {
                    new DialogueChoice(LocalizationManager.VW_ONLOAD_0_1, () => { 
                        PlayerPrefs.SetInt(ProfileManager.PLAYERPREFS_LANGUAGEID, (int)LocalizationLanguage.English); 
                    })
                })));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_MYSTERIOUSVOICES, LocalizationManager.VW_ONLOAD_1, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_MYSTERIOUSVOICES, LocalizationManager.VW_ONLOAD_2, m_voicePack))));
        um.AddUIAction(() => { GlobalGameManager.Instance.soundManager.PlayMusic(0); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_MYSTERIOUSVOICES, LocalizationManager.VW_ONLOAD_3, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_MYSTERIOUSVOICES, LocalizationManager.VW_ONLOAD_4, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_5, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_6, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_7, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_8, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_9, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "...", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_10, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "YOUR MISSION IS TO INFILTRATE A CASINO!", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "BUT I HAVEN'T THOUGHT THE STORY YET SO I DON'T KNOW WHAT ARE YOU GOING TO DO OVER THERE.", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "JUST FIND EVIDENCE OF SOMETHING LOL.", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "...", m_voicePack)))); 
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_15, m_voicePack),
                new DialogueChoice[2] {
                    new DialogueChoice(LocalizationManager.VW_ONLOAD_15_0, () => { 
                        _NewGameButton(); 
                    }),
                    new DialogueChoice(LocalizationManager.VW_ONLOAD_15_1, () => { 
                        _QuitButton(); 
                    })
                })));
        #endregion
    }

    private void _NewGameButton()
    {
        int pcRNG = Random.Range(1, 7);
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "Alright, because we're going to play in the casino, there will be a lot of rolling dices.", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "Because of that, I'll give you a chance.", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "If you roll a dice and get 6, you will win this game instantly without playing it.", m_voicePack), 
            new DialogueChoice[4]
            {
                new DialogueChoice("(Annoyed)", () => player.animator.SetInteger("expression", 1)),
                new DialogueChoice("(Smirk)", () => player.animator.SetInteger("expression", 3)),
                new DialogueChoice("(Surprised)", () => player.animator.SetInteger("expression", 4)),
                new DialogueChoice("...", () => player.animator.SetInteger("expression", 0)),
            }
            )));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "...", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => { player.animator.SetInteger("expression", 0); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "Here we go!", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "...", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "You got " + pcRNG + "!", m_voicePack))));
        if (pcRNG >= 6)
        {
            um.AddUIAction(() => { StartCoroutine(GlobalGameManager.Instance.soundManager.FadeOutMusic(1.0f)); um.NextAction(); });
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "...", m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));

            string evidence = PlayerPrefs.GetString(ProfileManager.PLAYERPREFS_HAVEPASSWORD + "-Level 1-1-2", false.ToString());
            um.AddUIAction(() => { PlayerPrefs.SetString(evidence, true.ToString()); um.NextAction(); });

            um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
            um.AddUIAction(() => SceneManager.LoadScene("Void World"));
        }
        else
        {
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "NYEHEHEHEHEHE.", m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "Well, it's time to play this game then.", m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "I'm preparing the teleportation system for you to teleport you to the Hub World.", m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_20, m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_21, m_voicePack))));
            um.AddUIAction(() => { StartCoroutine(GlobalGameManager.Instance.soundManager.FadeOutMusic(3.0f)); um.NextAction(); });
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_22, m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_ONLOAD_23, m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "3...", m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "2...", m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
            um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, "1...", m_voicePack))));
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
            um.AddUIAction(() => { em.triggerCheckpoints[0].teleportArea.gameObject.SetActive(true); um.NextAction(); });
            um.AddUIAction(() => StartCoroutine(um.AnimateTransition("flashbang")));
            um.AddUIAction(() => { player.animator.gameObject.SetActive(false); um.NextAction(); });
            um.AddUIAction(() => StartCoroutine(um.DelayNextAction(4.0f)));
            um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
            um.AddUIAction(() => _TeleportPlayerToScene("Level 0"));
        }
    }

    private void _LoadGameButton(string sceneName)
    {
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => SceneManager.LoadScene(sceneName));
    }

    private void _ClearSaveGameButton(string currentScene)
    {
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.VW_CLEARSAVEGAME, m_voicePack),
            new DialogueChoice[2] {
                new DialogueChoice(LocalizationManager.VW_CLEARSAVEGAME_CHOICES_1, () => {
                    GlobalGameManager.Instance.profileManager.ClearProfile();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
                }),
                new DialogueChoice(LocalizationManager.VW_CLEARSAVEGAME_CHOICES_2, () => { _LoadGameButton(currentScene); })
            })));
    }
}
