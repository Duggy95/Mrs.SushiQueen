using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Fish : MonoBehaviour
{
    public GameObject hpBarPrefab; // 물고기 체력바 프리팹
    public GameObject bobber;   // 찌 이미지
    public GameObject[] waterEff;  // 물장구 이미지 1, 2 번갈아서
    public FishData[] fishDatas;
    public GameObject directionObj; // 방향 오브젝트

    Image hp; //체력바 이미지
    FishingManager fm;  // 낚시 매니저
    GameObject _bobber;
    FishData fishData;
    List<GameObject> dirObj = new List<GameObject>();

    int maxHP; //최대체력
    int currHP;  //현재체력
    int heal;  // 회복력
    int _atk;  // 공격력
    int delayTime;   // 찌 던지고 물고기 나올 때까지 시간
    int randomIndex;
    float maxTime = 60f;  // 최대 타임
    float currTime;    // 현재 타임
    float fish_Probability;
    bool fishing = false;  // 낚시중인지
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
        transform.SetSiblingIndex(1);  //2번째 자식.

        _atk = int.Parse(GameManager.instance.data.atk);
    }

    void OnEnable()
    {
        currTime = maxTime;

        Vector3 bobberPos = transform.position;
        // 찌 이미지를 터치한 곳에 생성
        _bobber = Instantiate(bobber, bobberPos, Quaternion.identity);
        _bobber.transform.SetParent(transform);
        // 낚싯대부터 찌까지 라인렌더러 그리기
        Vector3 pos = bobberPos - new Vector3(0, 30, 0);
        delayTime = Random.Range(1, 4);
        // 코루틴 함수 호출
        StartCoroutine(FishingEff(pos));
        StartCoroutine(Heal());
    }

    private void Update()
    {
        // HP가 널이 아니고 현재 HP가 0보다 많으면
        // 한 번 터치할 때마다 공격력만큼 체력 깎고
        // 물고기 체력바 반영, 체력이 0이 되면 함수 호출 후 삭제
        if (fishing && hp != null && currHP > 0)
        {
            if (leftClick && Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.x > fm.fishingRod.transform.position.x)
                {
                    Atk();
                }
            }
            else if (rightClick && Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.x < fm.fishingRod.transform.position.x)
                {
                    Atk();
                }
            }
        }

        // 낚시 중이고 현재 시간이 0보다 많다면
        // 카운트 다운 시작
        // 카운트 다운이 끝났다면 fm의 물고기 도망 함수 호출
        if (fishing && currTime > 0)
        {
            currTime -= Time.deltaTime;

            if (currTime <= 0)
            {
                fm.Run();
                Die();
            }
        }
    }

    void Atk()
    {
        currHP -= _atk;
        hp.fillAmount = (float)currHP / maxHP;  // 남은 체력 비율에 맞게 줄어듬

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
        fm.useWhiteItemTxt.gameObject.SetActive(false);
        fm.useRedItemTxt.gameObject.SetActive(false);
        fm.useRareItemTxt.gameObject.SetActive(false);
        fm.useItemPanel.gameObject.SetActive(false);
        dirObj.Clear();
        Destroy(gameObject);
    }

    IEnumerator FishingEff(Vector3 pos)
    {
        // 물고기의 체력바는 터치한 부분보다 위에,
        // 물장구 이미지는 터치한 부분보다 아래에 위치
        Vector3 hpPos = pos + new Vector3(0, 100, 0);
        Vector3 dirPos = pos - new Vector3(0, 100, 0);

        yield return new WaitForSeconds(delayTime);

        // 확률에 따라 나오도록
        List<FishData> fish_Data = new List<FishData>();

        for (int i = 0; i < fishDatas.Length; i++)
        {
            fish_Probability = fishDatas[i].probability;
            // 아이템 썼을 때 확률 조정
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
            // 확률만큼 리스트 저장
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

        fishData = fish_Data[fishNum]; // 확률에 따른 물고기 종류 지정
        Setup(fishData);  // 물고기 체력 및 회복력 셋업
        currHP = maxHP;   // 현재 체력을 맥스 체력으로
        fishing = true;   // 낚시 시작

        string targetValue = fishData.fishName; // 찾고자 하는 값
        int count = fish_Data.Count(x => x.fishName == targetValue);

        Debug.Log("물고기 확률 : " + count);

        Debug.Log("물장구 시작");

        // 물고기 체력바 생성 후 자신의 자식으로 넣음
        GameObject fishHP = Instantiate(hpBarPrefab, hpPos, Quaternion.identity);
        fishHP.transform.SetParent(transform);
        hp = fishHP.GetComponent<Image>();
        hp.fillAmount = 1f;

        GameObject directionObjClone = Instantiate(directionObj, dirPos, Quaternion.identity);
        GameObject leftObj = directionObjClone.transform.GetChild(0).gameObject;
        GameObject rightObj = directionObjClone.transform.GetChild(1).gameObject;
        directionObjClone.transform.SetParent(transform);
        leftObj.gameObject.SetActive(false);
        rightObj.gameObject.SetActive(false);
        dirObj.Add(leftObj);
        dirObj.Add(rightObj);
        //StartCoroutine(Dir(dirObj));

        // 이미지 생성하여 번갈아가며 띄움
        GameObject water_ = Instantiate(waterEff[0], pos, Quaternion.identity);
        water_.transform.SetParent(transform);
        GameObject _water = Instantiate(waterEff[1], pos, Quaternion.identity);
        _water.transform.SetParent(transform);

        while (fishing)
        {
            water_.gameObject.SetActive(true);
            _water.gameObject.SetActive(false);

            randomIndex = Random.Range(0, dirObj.Count); // 배열에서 랜덤 인덱스 선택

            if (randomIndex - 0 > 0)
            {
                dirObj[0].SetActive(false); // 나머지 오브젝트들은 비활성화
                dirObj[1].SetActive(true); // 랜덤으로 선택된 오브젝트를 활성화
                rightClick = true;
                leftClick = false;
            }
            else
            {
                dirObj[0].SetActive(true); // 랜덤으로 선택된 오브젝트를 활성화
                dirObj[1].SetActive(false); // 나머지 오브젝트들은 비활성화
                rightClick = false;
                leftClick = true;
            }

            yield return new WaitForSeconds(0.5f);

            water_.gameObject.SetActive(false);
            _water.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);
        }
    }

   /* IEnumerator Dir(List<GameObject> dirObj)
    {
        while (fishing)
        {
            randomIndex = Random.Range(0, dirObj.Count); // 배열에서 랜덤 인덱스 선택

            if (randomIndex - 0 > 0)
            {
                dirObj[1].SetActive(true); // 랜덤으로 선택된 오브젝트를 활성화
                rightClick = true;
            }
            else
            {
                dirObj[0].SetActive(true); // 랜덤으로 선택된 오브젝트를 활성화
                leftClick = true;
            }
            yield return new WaitForSeconds(1f);

            dirObj[0].SetActive(false); // 나머지 오브젝트들은 비활성화
            dirObj[1].SetActive(false); // 나머지 오브젝트들은 비활성화
            leftClick = false;
            rightClick = false;
        }
    }*/

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
