using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTeacherController : MonoBehaviour
{
    [Header("Teaching UI")]
    public GameObject trainingPan;
    public Text lPs;
    public Text popup;

    [Header("Stats UI")]
    public Text statTxtAtk;
    public Text statTxtDef;
    public Text statTxtMag;
    public Text statTxtMdef;
    public Text statTxtSpd;
    public Text statTxtHP;
    public Text statTxtMP;

    [Header("Skills UI")]
    public Button[] aSkill = new Button[3];
    public Button nextPage;
    public Button prevPage;

    [Header("Party UI")]
    public GameObject charaPickPan;
    public Button[] party = new Button[3];

    private List<Skill> skillsToLearn = new List<Skill>();
    private List<Skill> learntSkills = new List<Skill>();
    private InfoCarrier carrier;

    private int page = 0;

    private int nAtk;
    private int nDef;
    private int nMag;
    private int nMdef;
    private int nSpd;
    private int nHP;
    private int nMP;
    private int nLp;

    private Character curChara;

    private bool isEntered = false;
    private bool isExited = false;

    // Start is called before the first frame update
    void Start()
    {
        carrier = GameObject.Find("ObjectCarrier").GetComponent<InfoCarrier>();
        for (int i =0; i < 3; i++)
        {
            party[i].GetComponentInChildren<Text>().text = carrier.getCharacter(i).unitName;
        }
        charaPickPan.SetActive(false);
        trainingPan.SetActive(false);

        skillsToLearn.Add(new Skill("Fireball", false, true, false, true, 5, 1, 1.25f));
        skillsToLearn.Add(new Skill("Greater Heal", true, true, false, true, 40, 1, 2f));
        skillsToLearn.Add(new Skill("Barrage", false, false, false, false, 20, 5, 0.7f));
        skillsToLearn.Add(new Skill("Test Skill 1", false, false, false, false, 1999, 12, 0.2f));
    }

    // Update is called once per frame
    void Update()
    {
        isEntered = Input.GetButtonDown("Submit");
        isExited = Input.GetButtonDown("Cancel");
    }

    bool isSkillLearnt(Skill template)
    {
        foreach (Skill skill in learntSkills)
        {
            if (skill.Compare(template)) return true;
        }
        return false;
    }

    public void showSkills()
    {
        if (page <= 0) prevPage.gameObject.SetActive(false);
        else prevPage.gameObject.SetActive(true);
        if (page >= (skillsToLearn.Count - 1) / 3) nextPage.gameObject.SetActive(false);
        else nextPage.gameObject.SetActive(true);
        for (int i=0; i< 3; i++)
        {
            if (page * 3 + i >= skillsToLearn.Count) aSkill[i].gameObject.SetActive(false);
            else
            {
                aSkill[i].gameObject.SetActive(true);
                if (curChara.hasSkill(skillsToLearn[page * 3 + i]) || ((nLp <= 0) && !(isSkillLearnt(skillsToLearn[page * 3 + i])))) aSkill[i].interactable = false;
                else aSkill[i].interactable = true;
                if (isSkillLearnt(skillsToLearn[page * 3 + i])) aSkill[i].GetComponent<Image>().color = Color.blue;
                else aSkill[i].GetComponent<Image>().color = Color.white;
                aSkill[i].GetComponentInChildren<Text>().text = skillsToLearn[page * 3 + i].Name;
            }
        }
    }

    public void showStats()
    {
        statTxtAtk.text = "" + nAtk;
        statTxtDef.text = "" + nDef;
        statTxtMag.text = "" + nMag;
        statTxtMdef.text = "" + nMdef;
        statTxtSpd.text = "" + nSpd;
        statTxtHP.text = "" + nHP;
        statTxtMP.text = "" + nMP;
        lPs.text = nLp + "/" + curChara.Lp;

        if (curChara.atk < nAtk) statTxtAtk.color = Color.blue;
        else statTxtAtk.color = Color.black;
        if (curChara.def < nDef) statTxtDef.color = Color.blue;
        else statTxtDef.color = Color.black;
        if (curChara.mag < nMag) statTxtMag.color = Color.blue;
        else statTxtMag.color = Color.black;
        if (curChara.mdef < nMdef) statTxtMdef.color = Color.blue;
        else statTxtMdef.color = Color.black;
        if (curChara.spd < nSpd) statTxtSpd.color = Color.blue;
        else statTxtSpd.color = Color.black;
        if (curChara.maxHP < nHP) statTxtHP.color = Color.blue;
        else statTxtHP.color = Color.black;
        if (curChara.maxMP < nMP) statTxtMP.color = Color.blue;
        else statTxtMP.color = Color.black;
    }

    void showTrainingWin()
    {
        trainingPan.SetActive(true);
        page = 0;

        nAtk = curChara.atk;
        nDef = curChara.def;
        nMag = curChara.mag;
        nMdef = curChara.mdef;
        nSpd = curChara.spd;
        nHP = curChara.maxHP;
        nMP = curChara.maxMP;
        nLp = curChara.Lp;

        showSkills();
        showStats();
    }

    void hideTrainingWin()
    {
        trainingPan.SetActive(false);
        page = 0;
    }

    public void OnCharaBtn(int i)
    {
        curChara = carrier.getCharacter(i);
        charaPickPan.SetActive(false);
        showTrainingWin();
    }

    public void OnStatAddBtn(int i)
    {
        switch (i)
        {
            case 1:
                if (nLp > 0)
                {
                    nAtk++;
                    nLp--;
                    showStats();
                }
                break;
            case 2:
                if (nLp > 0)
                {
                    nDef++;
                    nLp--;
                    showStats();
                }
                break;
            case 3:
                if (nLp > 0)
                {
                    nMag++;
                    nLp--;
                    showStats();
                }
                break;
            case 4:
                if (nLp > 0)
                {
                    nMdef++;
                    nLp--;
                    showStats();
                }
                break;
            case 5:
                if (nLp > 0)
                {
                    nSpd++;
                    nLp--;
                    showStats();
                }
                break;
            case 6:
                if (nLp > 0)
                {
                    nHP += 2;
                    nLp--;
                    showStats();
                }
                break;
            case 7:
                if (nLp > 0)
                {
                    nMP++;
                    nLp--;
                    showStats();
                }
                break;
            default:
                break;
        }
        showSkills();
    }

    public void OnStatSubBtn(int i)
    {
        switch (i)
        {
            case 1:
                if (nAtk > curChara.atk)
                {
                    nAtk--;
                    nLp++;
                    showStats();
                }
                break;
            case 2:
                if (nDef > curChara.def)
                {
                    nDef--;
                    nLp++;
                    showStats();
                }
                break;
            case 3:
                if (nMag > curChara.mag)
                {
                    nMag--;
                    nLp++;
                    showStats();
                }
                break;
            case 4:
                if (nMdef > curChara.mdef)
                {
                    nMdef--;
                    nLp++;
                    showStats();
                }
                break;
            case 5:
                if (nSpd > curChara.spd)
                {
                    nSpd--;
                    nLp++;
                    showStats();
                }
                break;
            case 6:
                if (nHP > curChara.maxHP)
                {
                    nHP -= 2;
                    nLp++;
                    showStats();
                }
                break;
            case 7:
                if (nMP > curChara.maxMP)
                {
                    nMP--;
                    nLp++;
                    showStats();
                }
                break;
            default:
                break;
        }
    }

    public void OnNextPageBtn()
    {
        page++;
        showSkills();
    }

    public void OnPrevPageBtn()
    {
        page--;
        showSkills();
    }

    public void OnSkillBtn(int i)
    {
        if (isSkillLearnt(skillsToLearn[page * 3 + i]))
        {
            learntSkills.Remove(skillsToLearn[page * 3 + i]);
            nLp++;
        }
        else
        {
            learntSkills.Add(skillsToLearn[page * 3 + i]);
            nLp--;
        }
        showStats();
        showSkills();
    }

    public void OnConfirmBtn()
    {
        curChara.setNewStats(nAtk, nDef, nMag, nMdef, nSpd, nHP, nMP, nLp);
        foreach (Skill skill in learntSkills)
        {
            curChara.addSkill(skill);
        }
        learntSkills = new List<Skill>();
        showSkills();
        showStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            popup.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            popup.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isEntered)
            {
                charaPickPan.SetActive(true);
                popup.gameObject.SetActive(false);
            }
            if (isExited)
            {
                popup.gameObject.SetActive(true);
                hideTrainingWin();
                charaPickPan.SetActive(false);
            }
        }
    }
}

