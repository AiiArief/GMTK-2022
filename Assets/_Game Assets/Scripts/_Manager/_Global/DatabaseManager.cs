using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    // character list --> suara list, kalo pintu password list
    // level list --> dapetin nama-nama scene
    [SerializeField] AudioClip m_music_frostWaltz;
    public AudioClip music_FrostWaltz { get { return m_music_frostWaltz; } }

    [SerializeField] AudioClip m_music_teddyBearWaltz;
    public AudioClip music_teddyBearWaltz { get { return m_music_teddyBearWaltz; } }

    [SerializeField] AudioClip m_music_spyGlass;
    public AudioClip music_spyGlass { get { return m_music_spyGlass; } }

    [SerializeField] AudioClip m_passwordTrue;
    public AudioClip passwordTrue { get { return m_passwordTrue; } }

    [SerializeField] AudioClip m_passwordWrong;
    public AudioClip passwordWrong { get { return m_passwordWrong; } }

    [SerializeField] AudioClip m_tikTok;
    public AudioClip tikTok { get { return m_tikTok; } }
}
