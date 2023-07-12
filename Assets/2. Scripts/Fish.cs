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

    Image HP; //ü�¹� �̹���
    FishingManager fM;  // ���� �Ŵ���
    GameObject _bobber;

    int maxHP = 10; //�ִ�ü��
    int currHP;  //����ü��
    int currHpPercent;  // ü�¹� �ۼ�Ƽ��
    int atk = 1;  // ���ݷ�
    float maxTime = 5f;  // �ִ� Ÿ��
    float currTime;    // ���� Ÿ��

    private void Awake()
    {
        fM = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<FishingManager>();
        transform.parent = fM.canvas.transform;
    }

    void Start()
    {
        currHP = maxHP;
        currTime = maxTime;

        Vector3 bobberPos = transform.position;
        // �� �̹����� ��ġ�� ���� ����
        _bobber = Instantiate(bobber, bobberPos, Quaternion.identity);
        _bobber.transform.parent = transform;
        // ���˴���� ����� ���η����� �׸���
        Vector3 pos = bobberPos - new Vector3(0, 25, 0);
        // �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(FishingEff(pos));
    }

    private void Update()
    {
    }

    IEnumerator TimeDown()
    {
        while (currTime < 0)
        {
            currTime -= Time.deltaTime;
            Debug.Log("���� �ð� " + currTime);

            if (currTime < 0)
            {
                yield break;
            }
        }
    }

    IEnumerator Atk()
    {
        if (HP == null)
        {
            yield break;
        }

        Debug.Log("���� ü�� " + currHP);

        if (Input.GetMouseButtonDown(0) && currHP > 0)
        {
            currHP -= atk;
            currHpPercent = currHP / maxHP;  //�����ð� ����
            HP.fillAmount = currHpPercent;  //Ÿ�̸Ӵ� �����ð� ������ �°� �پ��
        }
    }

    IEnumerator FishingEff(Vector3 pos)
    {
        // ������� ü�¹ٴ� ��ġ�� �κк��� ����,
        // ���屸 �̹����� ��ġ�� �κк��� �Ʒ��� ��ġ
        Vector3 hpPos = pos + new Vector3(0, 100, 0);
        yield return new WaitForSeconds(Random.Range(1, 4));

        StartCoroutine(TimeDown());
        Debug.Log("���屸 ����");

        GameObject fishHP = Instantiate(hpBarPrefab, hpPos, Quaternion.identity);
        fishHP.transform.parent = transform;
        HP = fishHP.GetComponent<Image>();
        HP.fillAmount = 1f;

        StartCoroutine(Atk());

        // �̹��� �����Ͽ� �����ư��� ���
        GameObject water_ = Instantiate(waterEff[0], pos, Quaternion.identity);
        water_.transform.parent = transform;
        GameObject _water = Instantiate(waterEff[1], pos, Quaternion.identity);
        _water.transform.parent = transform;

        while (true)
        {
            if (currHP <= 0)
            {
                fM.Fish();
                break;
            }

            else if (maxTime < 0)
            {
                fM.Run();
                break;
            }

            // ����⸦ ��ų� ��ĥ ������ �ݺ�
            water_.gameObject.SetActive(true);
            _water.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            water_.gameObject.SetActive(false);
            _water.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("����");
        Destroy(gameObject);
    }
}
