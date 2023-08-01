using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFish : MonoBehaviour
{
    public GameObject hpBarPrefab; // 물고기 체력바 프리팹
    public GameObject bobber;   // 찌 이미지
    public GameObject[] waterEff;  // 물장구 이미지 1, 2 번갈아서
    public FishData[] fishDatas;

    Image hp; //체력바 이미지
    //FishingManager fm;  // 낚시 매니저
    TutorialFishing tf;
    GameObject _bobber;
    FishData fishData;

    int maxHP; //최대체력
    int currHP;  //현재체력
    int heal;  // 회복력
    int _atk;  // 공격력
    int delayTime;   // 찌 던지고 물고기 나올 때까지 시간
    float maxTime = 15f;  // 최대 타임
    float currTime;    // 현재 타임
    bool fishing = false;  // 낚시중인지

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
            //currTime -= Time.deltaTime;

            /*if (currTime <= 0)
            {
                tf.Run();
                Destroy(gameObject);
            }*/
        }

        // HP가 널이 아니고 현재 HP가 0보다 많으면
        // 한 번 터치할 때마다 공격력만큼 체력 깎고
        // 물고기 체력바 반영, 체력이 0이 되면 함수 호출 후 삭제
        if (hp != null && currHP > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currHP -= _atk;
                hp.fillAmount = (float)currHP / maxHP;  // 남은 체력 비율에 맞게 줄어듬

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
        // 물고기의 체력바는 터치한 부분보다 위에,
        // 물장구 이미지는 터치한 부분보다 아래에 위치
        Vector3 hpPos = pos + new Vector3(0, 150, 0);
        Vector3 dirPos = pos - new Vector3(0, 100, 0);
        yield return new WaitForSeconds(delayTime);

        fishData = fishDatas[tf.count]; // 확률에 따른 물고기 종류 지정
        Setup(fishData);  // 물고기 체력 및 회복력 셋업
        currHP = maxHP;   // 현재 체력을 맥스 체력으로
        fishing = true;   // 낚시 시작
        tf.count++;

        Debug.Log("물장구 시작");

        // 물고기 체력바 생성 후 자신의 자식으로 넣음
        GameObject fishHP = Instantiate(hpBarPrefab, hpPos, Quaternion.identity);
        fishHP.transform.SetParent(transform);
        hp = fishHP.GetComponent<Image>();
        hp.fillAmount = 1f;
        Debug.Log("체력바 : " + hp.fillAmount);

        tf.touchTxt.gameObject.SetActive(true);

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

    IEnumerator DirEff(GameObject dirObj, Vector3 dirScale)
    {
        // 기존 스케일의 1.5배 해주고 작아지게
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
            // 현재 체력 + 회복력이 최대 체력보다 적다면
            // 1초마다 힐량 만큼 체력 회복
            yield return new WaitForSeconds(1f);

            if (currHP + heal <= maxHP)
            {
                currHP += heal;
                hp.fillAmount = (float)currHP / maxHP;  // 남은 체력 비율에 맞게 늘어남
            }
            Debug.Log("현재 체력 " + currHP);
        }
    }
}
