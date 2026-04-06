using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    public TypewriterEffect titleTypewriter;

    public TypewriterEffect person1RoleTypewriter;
    public TypewriterEffect person1NameTypewriter;

    public TypewriterEffect person2RoleTypewriter;
    public TypewriterEffect person2NameTypewriter;

    public TypewriterEffect person3RoleTypewriter;
    public TypewriterEffect person3NameTypewriter;

    void Start()
    {
        if (titleTypewriter != null)
            titleTypewriter.StartTyping("credits");

        if (person1RoleTypewriter != null)
            person1RoleTypewriter.StartTyping("2D ARTIST, UX");

        if (person1NameTypewriter != null)
            person1NameTypewriter.StartTyping("Pavarisa Vorachun");

        if (person2RoleTypewriter != null)
            person2RoleTypewriter.StartTyping("GAME PROGRAMMER, BACKEND ENGINEER");

        if (person2NameTypewriter != null)
            person2NameTypewriter.StartTyping("Yanisa Khramsaeng");

        if (person3RoleTypewriter != null)
            person3RoleTypewriter.StartTyping("UX/UI, SOUND DESIGNER, 2D ARTIST");

        if (person3NameTypewriter != null)
            person3NameTypewriter.StartTyping("Warintorn Thapanakulsak");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("StartScene");
    }
}