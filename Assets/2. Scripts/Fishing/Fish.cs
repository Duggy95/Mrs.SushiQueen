using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Fish : MonoBehaviour
{
    public GameObject hpBarPrefab; // ����� ü�¹� ������
    public GameObject bobber;   // �� �̹���
    public GameObject[] waterEff;  // ���屸 �̹��� 1, 2 �����Ƽ�
    public FishData[] fishDatas;
    public GameObject directionObj; // ���� ������Ʈ

    AudioSource audioSource;

    Image hp; //ü�¹� �̹���
    FishingManager fm;  // ���� �Ŵ���
    GameObject _bobber;
    FishData fishData;
    List<GameObject> dirObj = new List<GameObject>();

    int maxHP; //�ִ�ü��
    int currHP;  //����ü��
    int heal;  // ȸ����
    int _atk;  // ���ݷ�
    int delayTime;   // �� ������ ����� ���� ������ �ð�
    int randomIndex;
    float maxTime = 15f;  // �ִ� Ÿ��
    float currTime;    // ���� Ÿ��
    float fish_Probability;
    bool fishing = false;  // ����������
    bool rightClick = false;
    bool leftClick = false;

    public void Setup(FishData fishData)
    {
        maxHP = fishData.hp;
        heal = fishData.heal;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        fm = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<FishingManager>();
        transform.SetParent(fm.canvas.transform);
        transform.SetSiblingIndex(1);  //2��° �ڽ�.

        _atk = int.Parse(GameManager.instance.data.atk);
    }

    void OnEnable()
    {
        currTime = maxTime;
        audioSource.PlayOneShot(SoundManager.instance.throwBobber, 1);
        Vector3 bobberPos = transform.position;
        // �� �̹����� ��ġ�� ���� ����
        _bobber = Instantiate(bobber, bobberPos, Quaternion.identity);
        _bobber.transform.SetParent(transform);
        // ���˴���� ����� ���η����� �׸���
        Vector3 pos = bobberPos - new Vector3(0, 70, 0);
        delayTime = Random.Range(1, 4);
        // �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(FishingEff(pos));
        StartCoroutine(Heal());
    }

    private void Update()
    {
        if (fm.inventoryFullImg.activeSelf == true)
            return;

        // HP�� ���� �ƴϰ� ���� HP�� 0���� �����鼭
        // �� ���⿡ �°� ��ġ�ϸ� �����Լ� ȣ��
        // ����� ü�¹� �ݿ�, ü���� 0�� �Ǹ� �Լ� ȣ�� �� ����
        if (fishing && hp != null && currHP > 0)
        {
            if (leftClick && Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.x > fm.fishingRod.transform.position.x &&
                    Input.mousePosition.y < fm.lineStartPos.transform.position.y)
                {
                    Atk();
                }
            }
            else if (rightClick && Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.x < fm.fishingRod.transform.position.x &&
                    Input.mousePosition.y < fm.lineStartPos.transform.position.y)
                {
                    Atk();
                }
            }
            /*else if(Input.GetMouseButtonUp(0))
                audioSource.Stop();*/
        }

        // ���� ���̰� ���� �ð��� 0���� ���ٸ�
        // ī��Ʈ �ٿ� ����
        // ī��Ʈ �ٿ��� �����ٸ� fm�� ����� ���� �Լ� ȣ��
        if (fishing && currTime > 0)
        {
            currTime -= Time.deltaTime;

            if (currTime <= 0)
            {
                fm.Run();
                Die();
            }
        }

        if(fm.isFishing == false)
            Die();
    }

    void Atk()
    {
        currHP -= _atk;
        hp.fillAmount = (float)currHP / maxHP;  // ���� ü�� ������ �°� �پ��
        audioSource.PlayOneShot(SoundManager.instance.reelIn, 0.5f);

        if (currHP <= 0)
        {
            Debug.Log(fishData.fishImg);
            Debug.Log(fishData.info);
            fm.Fish(fishData);
            Die();
        }
    }

    void Die()
    {
        // ������ ��� �ؽ�Ʈ ��Ȱ��ȭ
        // ���� ����Ʈ �ʱ�ȭ
        // ������Ʈ ����
        fm.useWhiteItemTxt.gameObject.SetActive(false);
        fm.useRedItemTxt.gameObject.SetActive(false);
        fm.useRareItemTxt.gameObject.SetActive(false);
        fm.useItemPanel.gameObject.SetActive(false);
        fm.touchTxt.gameObject.SetActive(false);
        fm.giveupBtn.gameObject.SetActive(false);
        dirObj.Clear();
        Destroy(gameObject);
    }

    IEnumerator FishingEff(Vector3 pos)
    {
        // ������� ü�¹ٴ� ��ġ�� �κк��� ����,
        // ���屸 �̹����� ��ġ�� �κк��� �Ʒ��� ��ġ
        Vector3 hpPos = pos + new Vector3(0, 150, 0);
        Vector3 dirPos = pos - new Vector3(0, 100, 0);

        yield return new WaitForSeconds(delayTime);

        // Ȯ���� ���� ��������
        List<FishData> fish_Data = new List<FishData>();

        for (int i = 0; i < fishDatas.Length; i++)
        {
            fish_Probability = fishDatas[i].probability;
            // ������ ���� �� Ȯ�� ����
            if (fm.useItem_white)
            {
                if (fishDatas[i].color == 0)
                    fish_Probability *= 1.5f;

                else if (fishDatas[i].color == 1)
                    fish_Probability *= 0.5f;
            }

            else if (fm.useItem_red)
            {
                if (fishDatas[i].color == 0)
                    fish_Probability *= 0.5f;

                else if (fishDatas[i].color == 1)
                    fish_Probability *= 1.5f;
            }

            else if (fm.useItem_rare)
            {
                if (fishDatas[i].grade == 0)
                    fish_Probability *= 0.5f;

                else if (fishDatas[i].grade == 1)
                    fish_Probability *= 2f;
            }
            // Ȯ����ŭ ����Ʈ ����
            for (int j = 0; j < fish_Probability; j++)
            {
                fish_Data.Add(fishDatas[i]);
            }
        }

        // ������ ��� �ʱ�ȭ
        fm.useItem_rare = false;
        fm.useItem_red = false;
        fm.useItem_white = false;

        Debug.Log(fish_Data.Count);

        // ������ �ε��� ����
        int fishNum = Random.Range(0, fish_Data.Count);

        fishData = fish_Data[fishNum]; // Ȯ���� ���� ����� ���� ����
        Setup(fishData);  // ����� ü�� �� ȸ���� �¾�
        currHP = maxHP;   // ���� ü���� �ƽ� ü������
        fishing = true;   // ���� ����

        string targetValue = fishData.fishName; // ã���� �ϴ� ��
        int count = fish_Data.Count(x => x.fishName == targetValue); // ����� Ȯ��

        Debug.Log("����� Ȯ�� : " + count);

        Debug.Log("���屸 ����");

        // ����� ü�¹� ���� �� �ڽ��� �ڽ����� ����
        GameObject fishHP = Instantiate(hpBarPrefab, hpPos, Quaternion.identity);
        fishHP.transform.SetParent(transform);
        hp = fishHP.GetComponent<Image>();
        hp.fillAmount = 1f;

        // ���� ������Ʈ �����Ͽ� �� ������ ����Ʈ�� �߰�
        GameObject directionObjClone = Instantiate(directionObj, dirPos, Quaternion.identity);
        GameObject leftObj = directionObjClone.transform.GetChild(0).gameObject;
        GameObject rightObj = directionObjClone.transform.GetChild(1).gameObject;
        directionObjClone.transform.SetParent(transform);
        Vector3 dirObjScale = rightObj.transform.localScale;
        leftObj.gameObject.SetActive(false);
        rightObj.gameObject.SetActive(false);
        dirObj.Add(leftObj);
        dirObj.Add(rightObj);
        //StartCoroutine(Dir(dirObj));

        GameObject water_ = Instantiate(waterEff[0], pos, Quaternion.identity);
        water_.transform.SetParent(transform);
        GameObject _water = Instantiate(waterEff[1], pos, Quaternion.identity);
        _water.transform.SetParent(transform);

        fm.touchTxt.gameObject.SetActive(true);
        fm.giveupBtn.gameObject.SetActive(true);

        // �̹��� �����Ͽ� �����ư��� ���
        // �ѹ��� �� ������ ���� ���� ����
        while (fishing)
        {
            water_.gameObject.SetActive(true);
            _water.gameObject.SetActive(false);

            randomIndex = Random.Range(0, dirObj.Count); // �迭���� ���� �ε��� ����

            if (randomIndex - 0 > 0)
            {
                dirObj[0].SetActive(false); // ������ ������Ʈ���� ��Ȱ��ȭ
                dirObj[1].SetActive(true); // �������� ���õ� ������Ʈ�� Ȱ��ȭ
                rightClick = true;
                leftClick = false;
                StartCoroutine(DirEff(dirObj[1], dirObjScale));
            }
            else
            {
                dirObj[0].SetActive(true); // �������� ���õ� ������Ʈ�� Ȱ��ȭ
                dirObj[1].SetActive(false); // ������ ������Ʈ���� ��Ȱ��ȭ
                rightClick = false;
                leftClick = true;
                StartCoroutine(DirEff(dirObj[0], dirObjScale));
            }

            yield return new WaitForSeconds(0.5f);

            water_.gameObject.SetActive(false);
            _water.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator DirEff(GameObject dirObj, Vector3 dirScale)
    {
        // ���� �������� 1.5�� ���ְ� �۾�����
        dirObj.gameObject.transform.localScale = dirScale * 1.5f; 

        for (int i = 0; i < 10; i++) 
        {
            dirObj.gameObject.transform.localScale *= 0.95f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Heal()
    {
        yield return new WaitForSeconds(delayTime);

        while (fishing)
        {
            // ���� ü�� + ȸ������ �ִ� ü�º��� ���ٸ�
            // 1�ʸ��� ���� ��ŭ ü�� ȸ��
            yield return new WaitForSeconds(1f);

            if (currHP + heal <= maxHP)
            {
                currHP += heal;
                hp.fillAmount = (float)currHP / maxHP;  // ���� ü�� ������ �°� �þ
            }
            Debug.Log("���� ü�� " + currHP);
        }
    }
}
