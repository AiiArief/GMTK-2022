using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityEvent : Entity
{
    protected UIManager um;
    protected EntityManagerEvent em;
    protected EntityCharacterPlayer player;

    [SerializeField] protected VoicePack m_voicePack;

    public virtual void EventOnLoadLevel()
    {
        _BasicOnLoadLevel();
        _TeleportPlayerToCheckpoint(em.triggerCheckpoints[em.currentcheckpoint]);
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
    }

    public void CheckpointEvent()
    {
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddTutorial(new Tutorial(TutorialType.None, LocalizationManager.TUTORIAL_CHECKPOINT), 5.0f);
    }

    public void PlayerPause()
    {
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."),
                new DialogueChoice[3] {
                    new DialogueChoice(LocalizationManager.PAUSE_CHOICES_1, () => { 
                        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); }); 
                    }),
                    new DialogueChoice(LocalizationManager.PAUSE_CHOICES_2, () => {
                        um.AddUIAction(() => { StartCoroutine(GlobalGameManager.Instance.soundManager.FadeOutMusic(1.0f)); um.NextAction(); });
                        _RetryLastCheckpointButton(); 
                    }),
                    new DialogueChoice(LocalizationManager.PAUSE_CHOICES_3, () => { 
                        _QuitButton(); 
                    })
                })));
    }

    public void PlayerIsCapturedEvent(EntityCharacterNPC byWhom)
    {
        um.ClearUIAction();
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => { StartCoroutine(GlobalGameManager.Instance.soundManager.FadeOutMusic(1.0f)); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_3DHEADPHONESPHEREROBOT_GUARD, LocalizationManager.CAPTURED, byWhom.voicePack),
                new DialogueChoice[2] {
                    new DialogueChoice(LocalizationManager.CAPTURED_CHOICES_1, () => { _RetryLastCheckpointButton(); }),
                    new DialogueChoice(LocalizationManager.CAPTURED_CHOICES_2, () => { _QuitButton(); })
                })));
    }

    StoredStatusEffect[] m_eventStatusEffects;
    public void AddBasicStatusEffectOnStartingEvent()
    {
        m_eventStatusEffects = new StoredStatusEffect[2] { new StoredStatusEffectEventControl(player), new StoredStatusEffectAutoSkip(player) };
        player.storedStatusEffects.AddRange(m_eventStatusEffects);
    }

    public void RemoveBasicStatusEffectOnFinishEvent()
    {
        if (m_eventStatusEffects.Length > 0)
        {
            foreach (StoredStatusEffect eventStatusEffect in m_eventStatusEffects)
            {
                eventStatusEffect.isGoingToBeRemovedFlag = true;
            }
        }
    }

    protected void _BasicOnLoadLevel()
    {
        um = GameManager.Instance.uiManager;
        em = GameManager.Instance.eventManager;
        player = GameManager.Instance.playerManager.GetMainPlayer();
    }

    // handle kalo ga ada childnya
    protected void _TeleportPlayerToCheckpoint(TriggerCheckpoint checkpoint)
    {
        LevelManager levelManager = GameManager.Instance.levelManager;
        LevelGrid grid = levelManager.GetClosestGridFromPosition(checkpoint.transform.position);
        LevelGridNode checkpointNode = grid.ConvertPosToNode(checkpoint.transform.position);
        player.currentNode.entityListOnThisNode.Remove(player);
        player.AssignToLevelGrid(checkpointNode);
        player.transform.position = checkpointNode.realWorldPos;
        player.transform.rotation = Quaternion.Euler(new Vector3(0.0f, checkpoint.startRotation, 0.0f));
    }

    protected void _TeleportPlayerToScene(string sceneName, int checkpointId = 0)
    {
        PlayerPrefs.SetString(ProfileManager.PLAYERPREFS_CURRENTSCENE, sceneName); // wajib di save ga ya?
        PlayerPrefs.SetInt(ProfileManager.PLAYERPREFS_CURRENTSCENECHECKPOINT, checkpointId);

        SceneManager.LoadScene(sceneName);
    }

    protected void _QuitButton(bool fromGameplay = true)
    {
        if(fromGameplay) um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
        um.AddUIAction(() => { StartCoroutine(GlobalGameManager.Instance.soundManager.FadeOutMusic(1.0f)); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_THEDEVELOPER, LocalizationManager.QUIT, m_voicePack))));
        um.AddUIAction(() => Application.Quit());
    }

    protected void _RetryLastCheckpointButton()
    {
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
        um.AddUIAction(() => _TeleportPlayerToScene(SceneManager.GetActiveScene().name, em.currentcheckpoint));
    }
}
