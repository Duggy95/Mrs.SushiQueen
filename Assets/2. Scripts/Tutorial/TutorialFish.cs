using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFish : MonoBehaviour
{
    public GameObject hpBarPrefab; // ����� ü�¹� ������
    public GameObject bobber;   // �� �̹���
    public GameObject[] waterEff;  // ���屸 �̹��� 1, 2 �����Ƽ�
    public FishData[] fishDatas;

    Image hp; //ü�¹� �̹���
    //FishingManager fm;  // ���� �Ŵ���
    TutorialFishing tf;
    GameObject _bobber;
    FishData fishData;

    int maxHP; //�ִ�ü��
    int currHP;  //����ü��
    int heal;  // ȸ����
    int _atk;  // ���ݷ�
    int delayTime;   // �� ������ ����� ���� ������ �ð�
    float maxTime = 15f;  // �ִ� Ÿ��
    float currTime;    // ���� Ÿ��
    bool fishing = false;  // ����������

    public void Setup(FishData fishData)
    {
        maxHP = fishData.hp;
        heal = fishData.heal;
    }

    private void Awake()
    {
        //fm = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<FishingManager>();
        tf = GameObject.FindGameObjectWithTag("TUTORIAL").GetComponent<TutorialFishing>();
        transform.SetParent(tf.fishCanvas.transform);
        //_atk = int.Parse(GameManager.instance.data.atk);
        _atk = 1;
    }

    void OnEnable()
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
            //currTime -= Time.deltaTime;

            /*if (currTime <= 0)
            {
                tf.Run();
                Destroy(gameObject);
            }*/
        }

        // HP�� ���� �ƴϰ� ���� HP�� 0���� ������
        // �� �� ��ġ�� ������ ���ݷ¸�ŭ ü�� ���
        // ����� ü�¹� �ݿ�, ü���� 0�� �Ǹ� �Լ� ȣ�� �� ����
        if (hp != null && currHP > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currHP -= _atk;
                hp.fillAmount = (float)currHP / maxHP;  // ���� ü�� ������ �°� �پ��

                if (currHP <= 0)
                {
                    Debug.Log(fishData.fishImg);
                    Debug.Log(fishData.info);

                    tf.touchTxt.gameObject.SetActive(false);
                    tf.Fish(fishData);
                    tf.fishCome = true;
                    Destroy(gameObject);
                }
            }
        }
    }

    IEnumerator FishingEff(Vector3 pos)
    {
        // ������� ü�¹ٴ� ��ġ�� �κк��� ����,
        // ���屸 �̹����� ��ġ�� �κк��� �Ʒ��� ��ġ
        Vector3 hpPos = pos + new Vector3(0, 150, 0);
        Vector3 dirPos = pos - new Vector3(0, 100, 0);
        yield return new WaitForSeconds(delayTime);

        fishData = fishDatas[tf.count]; // Ȯ���� ���� ����� ���� ����
        Setup(fishData);  // ����� ü�� �� ȸ���� �¾�
        currHP = maxHP;   // ���� ü���� �ƽ� ü������
        fishing = true;   // ���� ����
        tf.count++;

        Debug.Log("���屸 ����");

        // ����� ü�¹� ���� �� �ڽ��� �ڽ����� ����
        GameObject fishHP = Instantiate(hpBarPrefab, hpPos, Quaternion.identity);
        fishHP.transform.SetParent(transform);
        hp = fishHP.GetComponent<Image>();
        hp.fillAmount = 1f;
        Debug.Log("ü�¹� : " + hp.fillAmount);

        tf.touchTxt.gameObject.SetActive(true);

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
