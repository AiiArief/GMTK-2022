using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EntityEvent_Level0 : EntityEvent
{
    public override void EventOnLoadLevel()
    {
        base.EventOnLoadLevel();

        AddBasicStatusEffectOnStartingEvent();
        player.animator.gameObject.SetActive(false);

        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { um.AddTutorial(new Tutorial(TutorialType.None, LocalizationManager.TUTORIAL_HUBWORLD), 3.0f); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
        um.AddUIAction(() => { em.triggerCheckpoints[0].teleportArea.gameObject.SetActive(true); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition("flashbang")));
        um.AddUIAction(() => { player.animator.gameObject.SetActive(true); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(4.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => { GlobalGameManager.Instance.soundManager.PlayMusic(1); um.NextAction(); });
        um.AddUIAction(() => { um.AddTutorial(new Tutorial(TutorialType.Move, LocalizationManager.TUTORIAL_MOVE), 3.0f); um.NextAction(); });

        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); um.NextAction(); });
    }

    public void InteractTutorialEvent()
    {
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { um.AddTutorial(new Tutorial(TutorialType.None, LocalizationManager.TUTORIAL_INTERACT), 10.0f); um.NextAction(); });
    }

    public void RunTutorialEvent()
    {
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => { um.AddTutorial(new Tutorial(TutorialType.MoveMod, LocalizationManager.TUTORIAL_RUN), 5.0f); um.NextAction(); });
    }

    [SerializeField] CutsceneCamera m_cutsceneCamera1;
    public void WinCheckpointEvent()
    {
        m_cutsceneCamera1.UseCamera(0);

        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => { StartCoroutine(GlobalGameManager.Instance.soundManager.FadeOutMusic(3.0f)); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "..."))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "?"))));
        um.AddUIAction(() =>
        {
            m_cutsceneCamera1.UseCamera(1);
            player.animator.SetInteger("expression", 1);
            um.NextAction();
        });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", LocalizationManager.HW_WIN))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(3.0f)));
        um.AddUIAction(() =>
        {
            player.animator.SetInteger("expression", 4);
            um.NextAction();
        });
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue("", "!"))));
        um.AddUIAction(() => { em.triggerCheckpoints[1].teleportArea.gameObject.SetActive(true); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition("flashbang")));
        um.AddUIAction(() =>
        {
            m_cutsceneCamera1.ReleaseCamera();
            player.animator.gameObject.SetActive(false);
            um.NextAction();
        });
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(4.0f)));
        um.AddUIAction(() => StartCoroutine(um.AnimateTransition()));
        um.AddUIAction(() => _TeleportPlayerToScene("Level 1-0"));
    }

    [SerializeField] CutsceneCamera m_cutsceneCamera_note3DSphereRobot;
    public void DetailEvent_3DSphereRobot()
    {
        m_cutsceneCamera_note3DSphereRobot.UseCamera();

        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DSR_0, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DSR_1, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, "They're the enemies of this game, but you can't kill / takedown them.", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DSR_4, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DSR_5, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DSR_6, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DSR_7, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DSR_8, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DSR_9, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DSR_10, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DSR_11, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DSR_12, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_cutsceneCamera_note3DSphereRobot.ReleaseCamera(); um.NextAction(); });
    }

    [SerializeField] CutsceneCamera m_cutsceneCamera_note3DHumanoid;
    public void DetailEvent_3DHumanoid()
    {
        m_cutsceneCamera_note3DHumanoid.UseCamera();

        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_0, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_1, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_2, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_3, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_4, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_5, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_6, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_7, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_8, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_9, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, "Sigh...", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_10, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_11, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_12, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_13, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_14, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_3DH_15, m_voicePack))));
        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_cutsceneCamera_note3DHumanoid.ReleaseCamera(); um.NextAction(); });
    }

    [SerializeField] CutsceneCamera m_cutsceneCamera_note2DHumanoid;
    public void DetailEvent_2DHumanoid()
    {
        m_cutsceneCamera_note2DHumanoid.UseCamera();

        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.AfterInput)));
        um.AddUIAction(() => { AddBasicStatusEffectOnStartingEvent(); um.NextAction(); });
        um.AddUIAction(() => StartCoroutine(um.DelayUntilPhaseInput(PhaseEnum.WaitInput)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_2DH_0, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_2DH_1, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, "This is the interface for class 2D Humanoid", m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_2DH_3, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_2DH_4, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_2DH_5, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_2DH_6, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_2DH_7, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_2DH_8, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_2DH_9, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_2DH_10, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_2DH_11, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.AddDialogue(new Dialogue(LocalizationManager.CHARACTER_DEVELOPERNOTE, LocalizationManager.HW_DETAIL_2DH_12, m_voicePack))));
        um.AddUIAction(() => StartCoroutine(um.DelayNextAction(1.0f)));
        um.AddUIAction(() => { RemoveBasicStatusEffectOnFinishEvent(); m_cutsceneCamera_note2DHumanoid.ReleaseCamera(); um.NextAction(); });
    }
}
