using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    public GameObject hpBarPrefab; // ����� ü�¹� ������
    public GameObject bobber;   // �� �̹���
    public GameObject[] waterEff;  // ���屸 �̹��� 1, 2 �����Ƽ�
    public FishData[] fishDatas;

    Image hp; //ü�¹� �̹���
    FishingManager fm;  // ���� �Ŵ���
    GameObject _bobber;
    FishData fishData;

    int maxHP; //�ִ�ü��
    int currHP;  //����ü��
    int heal;  // ȸ����
    int atk = 1;  // ���ݷ�
    int delayTime;   // �� ������ ����� ���� ������ �ð�
    float maxTime = 30f;  // �ִ� Ÿ��
    float currTime;    // ���� Ÿ��
    bool fishing = false;  // ����������

    public void Setup(FishData fishData)
    {
        maxHP = fishData.hp;
        heal = fishData.heal;
    }

    private void Awake()
    {
        fm = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<FishingManager>();
        transform.SetParent(fm.canvas.transform);
    }

    void Start()
    {
        currTime = maxTime;

        Vector3 bobberPos = transform.position;
        // �� �̹����� ��ġ�� ���� ����
        _bobber = Instantiate(bobber, bobberPos, Quaternion.identity);
        _bobber.transform.SetParent(transform);
        // ���˴���� ����� ���η����� �׸���
        Vector3 pos = bobberPos - new Vector3(0, 25, 0);
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
                Destroy(gameObject);
            }
        }

        // HP�� ���� �ƴϰ� ���� HP�� 0���� ������
        // �� �� ��ġ�� ������ ���ݷ¸�ŭ ü�� ���
        // ����� ü�¹� �ݿ�, ü���� 0�� �Ǹ� �Լ� ȣ�� �� ����
        if (hp != null && currHP > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currHP -= atk;
                hp.fillAmount = (float)currHP / maxHP;  // ���� ü�� ������ �°� �پ��

                if (currHP <= 0)
                {
                    Debug.Log(fishData.fishImg);
                    Debug.Log(fishData.info);

                    fm.Fish(fishData);

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
        yield return new WaitForSeconds(delayTime);
        int fishNum = Random.Range(0, fishDatas.Length);
        fishData = fishDatas[fishNum];
        Setup(fishData);
        currHP = maxHP;
        fishing = true;

        Debug.Log("���屸 ����");

        // ����� ü�¹� ���� �� �ڽ��� �ڽ����� ����
        GameObject fishHP = Instantiate(hpBarPrefab, hpPos, Quaternion.identity);
        fishHP.transform.SetParent(transform);
        hp = fishHP.GetComponent<Image>();
        hp.fillAmount = 1f;
        Debug.Log("ü�¹� : " + hp.fillAmount);

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

            water_.gameObject.SetActive(false);
            _water.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);
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

            if (currHP + heal < maxHP)
            {
                currHP += heal;
                hp.fillAmount = (float)currHP / maxHP;  // ���� ü�� ������ �°� �þ
            }
            Debug.Log("���� ü�� " + currHP);
        }
    }
}
