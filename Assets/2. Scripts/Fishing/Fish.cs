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
        fm = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<FishingManager>();
        transform.SetParent(fm.canvas.transform);
        transform.SetSiblingIndex(1);  //2��° �ڽ�.

        _atk = int.Parse(GameManager.instance.data.atk);
    }

    void OnEnable()
    {
        currTime = maxTime;

        Vector3 bobberPos = transform.position;
        // �� �̹����� ��ġ�� ���� ����
        _bobber = Instantiate(bobber, bobberPos, Quaternion.identity);
        _bobber.transform.SetParent(transform);
        // ���˴���� ����� ���η����� �׸���
        Vector3 pos = bobberPos - new Vector3(0, 30, 0);
        delayTime = Random.Range(1, 4);
        // �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(FishingEff(pos));
        StartCoroutine(Heal());
    }

    private void Update()
    {
        // ���� ���̰� ���� �ð��� 0���� ���ٸ�
        // ī��Ʈ �ٿ� ����
        // ī��Ʈ �ٿ��� �����ٸ� fm�� ����� ���� �Լ� ȣ��
        if (fishing && currTime > 0)
        {
            currTime -= Time.deltaTime;

            if (currTime <= 0)
            {
                fm.Run();
                dirObj.Clear();
                Destroy(gameObject);
            }
        }
        // HP�� ���� �ƴϰ� ���� HP�� 0���� ������
        // �� �� ��ġ�� ������ ���ݷ¸�ŭ ü�� ���
        // ����� ü�¹� �ݿ�, ü���� 0�� �Ǹ� �Լ� ȣ�� �� ����
        /*if (fm.isFishing && hp != null && currHP > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (leftClick && Input.mousePosition.x > 0)
                {
                    currHP -= _atk;
                    hp.fillAmount = (float)currHP / maxHP;  // ���� ü�� ������ �°� �پ��

                    if (currHP <= 0)
                    {
                        Debug.Log(fishData.fishImg);
                        Debug.Log(fishData.info);
                        fm.Fish(fishData);
                        dirObj.Clear();
                        Destroy(gameObject);
                    }
                }
                else if (rightClick && Input.mousePosition.x < 0)
                {
                    currHP -= _atk;
                    hp.fillAmount = (float)currHP / maxHP;  // ���� ü�� ������ �°� �پ��

                    if (currHP <= 0)
                    {
                        Debug.Log(fishData.fishImg);
                        Debug.Log(fishData.info);
                        fm.Fish(fishData);
                        dirObj.Clear();
                        Destroy(gameObject);
                    }
                }
            }
        }*/
    }

    public void LeftClick()
    {
        if (fm.isFishing && hp != null && currHP > 0)
        {

            if (leftClick)
            {
                currHP -= _atk;
                hp.fillAmount = (float)currHP / maxHP;  // ���� ü�� ������ �°� �پ��

                if (currHP <= 0)
                {
                    Debug.Log(fishData.fishImg);
                    Debug.Log(fishData.info);
                    fm.Fish(fishData);
                    dirObj.Clear();
                    Destroy(gameObject);
                }
            }
        }
    }

    public void RightClick()
    {
        if (fm.isFishing && hp != null && currHP > 0)
        {

            if (rightClick)
            {
                currHP -= _atk;
                hp.fillAmount = (float)currHP / maxHP;  // ���� ü�� ������ �°� �پ��

                if (currHP <= 0)
                {
                    Debug.Log(fishData.fishImg);
                    Debug.Log(fishData.info);
                    fm.Fish(fishData);
                    dirObj.Clear();
                    Destroy(gameObject);
                }
            }
        }
    }

    IEnumerator FishingEff(Vector3 pos)
    {
        // ������� ü�¹ٴ� ��ġ�� �κк��� ����,
        // ���屸 �̹����� ��ġ�� �κк��� �Ʒ��� ��ġ
        Vector3 hpPos = pos + new Vector3(0, 100, 0);
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

        fm.useItem_rare = false;
        fm.useItem_red = false;
        fm.useItem_white = false;

        Debug.Log(fish_Data.Count);

        int fishNum = Random.Range(0, fish_Data.Count);

        fishData = fish_Data[fishNum]; // Ȯ���� ���� ����� ���� ����
        Setup(fishData);  // ����� ü�� �� ȸ���� �¾�
        currHP = maxHP;   // ���� ü���� �ƽ� ü������
        fishing = true;   // ���� ����

        string targetValue = fishData.fishName; // ã���� �ϴ� ��
        int count = fish_Data.Count(x => x.fishName == targetValue);

        Debug.Log("����� Ȯ�� : " + count);

        Debug.Log("���屸 ����");

        // ����� ü�¹� ���� �� �ڽ��� �ڽ����� ����
        GameObject fishHP = Instantiate(hpBarPrefab, hpPos, Quaternion.identity);
        fishHP.transform.SetParent(transform);
        hp = fishHP.GetComponent<Image>();
        hp.fillAmount = 1f;

        GameObject directionObjClone = Instantiate(directionObj, dirPos, Quaternion.identity);
        GameObject leftObj = directionObjClone.transform.GetChild(0).gameObject;
        GameObject rightObj = directionObjClone.transform.GetChild(1).gameObject;
        directionObjClone.transform.SetParent(transform);

        dirObj.Add(leftObj);
        dirObj.Add(rightObj);
        StartCoroutine(Dir(dirObj));
        //StartCoroutine(Click());

        // �̹��� �����Ͽ� �����ư��� ���
        GameObject water_ = Instantiate(waterEff[0], pos, Quaternion.identity);
        water_.transform.SetParent(transform);
        GameObject _water = Instantiate(waterEff[1], pos, Quaternion.identity);
        _water.transform.SetParent(transform);

        while (fishing)
        {
            water_.gameObject.SetActive(true);
            _water.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            /*randomIndex = Random.Range(0, dirObj.Length); // �迭���� ���� �ε��� ����
            for (int i = 0; i < dirObj.Length; i++)
            {
                if (i == randomIndex)
                {
                    dirObj[i].SetActive(true); // �������� ���õ� ������Ʈ�� Ȱ��ȭ

                    if (i == 0)
                    {
                        leftClick = true;
                        rightClick = false;
                    }
                    else
                    {
                        leftClick = false;
                        rightClick = true;
                    }
                }
                else
                {
                    dirObj[i].SetActive(false); // ������ ������Ʈ���� ��Ȱ��ȭ
                }
            }*/
            water_.gameObject.SetActive(false);
            _water.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Dir(List<GameObject> dirObj)
    {
        while (fishing)
        {
            randomIndex = Random.Range(0, dirObj.Count); // �迭���� ���� �ε��� ����

            if (randomIndex - 0 > 0)
            {
                dirObj[1].SetActive(true); // �������� ���õ� ������Ʈ�� Ȱ��ȭ
                dirObj[0].SetActive(false); // ������ ������Ʈ���� ��Ȱ��ȭ
                leftClick = false;
                rightClick = true;
            }
            else
            {
                dirObj[0].SetActive(true); // �������� ���õ� ������Ʈ�� Ȱ��ȭ
                dirObj[1].SetActive(false); // ������ ������Ʈ���� ��Ȱ��ȭ
                leftClick = true;
                rightClick = false;

            }

        }
        yield return new WaitForSeconds(1f);
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
