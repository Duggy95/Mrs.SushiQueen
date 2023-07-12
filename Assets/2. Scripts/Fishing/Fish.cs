using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    public GameObject hpBarPrefab; // 물고기 체력바 프리팹
    public GameObject bobber;   // 찌 이미지
    public GameObject[] waterEff;  // 물장구 이미지 1, 2 번갈아서
    public FishData[] fishDatas;

    Image hp; //체력바 이미지
    FishingManager fm;  // 낚시 매니저
    GameObject _bobber;
    FishData fishData;

    int maxHP; //최대체력
    int currHP;  //현재체력
    int heal;  // 회복력
    int atk = 1;  // 공격력
    int delayTime;   // 찌 던지고 물고기 나올 때까지 시간
    float maxTime = 30f;  // 최대 타임
    float currTime;    // 현재 타임
    bool fishing = false;  // 낚시중인지

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
        // 찌 이미지를 터치한 곳에 생성
        _bobber = Instantiate(bobber, bobberPos, Quaternion.identity);
        _bobber.transform.SetParent(transform);
        // 낚싯대부터 찌까지 라인렌더러 그리기
        Vector3 pos = bobberPos - new Vector3(0, 25, 0);
        delayTime = Random.Range(1, 4);
        // 코루틴 함수 호출
        StartCoroutine(FishingEff(pos));
        StartCoroutine(Heal());
    }

    private void Update()
    {
        // 낚시 중이고 현재 시간이 0보다 많다면
        // 카운트 다운 시작
        // 카운트 다운이 끝났다면 fm의 물고기 도망 함수 호출
        if (fishing && currTime > 0)
        {
            currTime -= Time.deltaTime;

            if (currTime <= 0)
            {
                fm.Run();
                Destroy(gameObject);
            }
        }

        // HP가 널이 아니고 현재 HP가 0보다 많으면
        // 한 번 터치할 때마다 공격력만큼 체력 깎고
        // 물고기 체력바 반영, 체력이 0이 되면 함수 호출 후 삭제
        if (hp != null && currHP > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currHP -= atk;
                hp.fillAmount = (float)currHP / maxHP;  // 남은 체력 비율에 맞게 줄어듬

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
        // 물고기의 체력바는 터치한 부분보다 위에,
        // 물장구 이미지는 터치한 부분보다 아래에 위치
        Vector3 hpPos = pos + new Vector3(0, 100, 0);
        yield return new WaitForSeconds(delayTime);
        int fishNum = Random.Range(0, fishDatas.Length);
        fishData = fishDatas[fishNum];
        Setup(fishData);
        currHP = maxHP;
        fishing = true;

        Debug.Log("물장구 시작");

        // 물고기 체력바 생성 후 자신의 자식으로 넣음
        GameObject fishHP = Instantiate(hpBarPrefab, hpPos, Quaternion.identity);
        fishHP.transform.SetParent(transform);
        hp = fishHP.GetComponent<Image>();
        hp.fillAmount = 1f;
        Debug.Log("체력바 : " + hp.fillAmount);

        // 이미지 생성하여 번갈아가며 띄움
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
            // 현재 체력 + 회복력이 최대 체력보다 적다면
            // 1초마다 힐량 만큼 체력 회복
            yield return new WaitForSeconds(1f);

            if (currHP + heal < maxHP)
            {
                currHP += heal;
                hp.fillAmount = (float)currHP / maxHP;  // 남은 체력 비율에 맞게 늘어남
            }
            Debug.Log("현재 체력 " + currHP);
        }
    }
}
