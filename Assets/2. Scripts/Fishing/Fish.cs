using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Fish : MonoBehaviour
{
    public GameObject hpBarPrefab; // 물고기 체력바 프리팹
    public GameObject bobber;   // 찌 이미지
    public FishData[] fishDatas;  // 물고기 정보 스크립터블
    public FishData[] easyFishDatas;  // 초보자용 전용 스크립터블
    public GameObject directionObj; // 방향 오브젝트

    AudioSource audioSource;

    Image hp; //체력바 이미지
    FishingManager fm;  // 낚시 매니저
    GameObject _bobber;  // 생성된 찌 이미지
    FishData fishData;  // 생성된 물고기의 정보
    List<GameObject> dirObj = new List<GameObject>();

    int maxHP; //최대체력
    int currHP;  //현재체력
    int heal;  // 회복력
    int _atk;  // 공격력
    int delayTime;   // 찌 던지고 물고기 나올 때까지 시간
    int randomIndex;  // 좌우클릭에 대한 랜덤인덱스
    float maxTime = 15f;  // 최대 타임
    float currTime;    // 현재 타임
    float fish_Probability;  // 물고기 확률
    bool fishing = false;  // 낚시중인지
    bool rightClick = false;  // 우측 클릭 가능 여부
    bool leftClick = false;  // 좌측 클릭 가능 여부

    // 생성된 물고기 정보를 토대로 설정
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
        transform.SetSiblingIndex(1);  //2번째 자식.

        _atk = int.Parse(GameManager.instance.data.atk);
    }

    // 찌가 생성됨과 동시에 물고기 정보 초기 셋팅
    void OnEnable()
    {
        currTime = maxTime;
        audioSource.PlayOneShot(SoundManager.instance.throwBobber, 1);
        Vector3 bobberPos = transform.position;
        // 찌 이미지를 터치한 곳에 생성
        _bobber = Instantiate(bobber, bobberPos, Quaternion.identity);
        _bobber.transform.SetParent(transform);
        // 낚싯대부터 찌까지 라인렌더러 그리기
        Vector3 pos = bobberPos - new Vector3(0, 70, 0);
        delayTime = Random.Range(1, 4);
        // 코루틴 함수 호출
        StartCoroutine(FishingEff(pos));
        StartCoroutine(Heal());
    }

    private void Update()
    {
        // 인벤토리 연 경우 터치 안되게
        if (fm.inventoryFullImg.activeSelf == true)
            return;

        // HP가 널이 아니고 현재 HP가 0보다 많으면서
        // 각 방향에 맞게 터치하면 공격함수 호출
        // 물고기 체력바 반영, 체력이 0이 되면 함수 호출 후 삭제
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

        if(fm.isFishing == false)
            Die();
    }

    void Atk()
    {
        currHP -= _atk;
        hp.fillAmount = (float)currHP / maxHP;  // 남은 체력 비율에 맞게 줄어듬
        audioSource.PlayOneShot(SoundManager.instance.reelIn, 0.5f);

        // 체력을 다 깎은 경우 각 함수 호출
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
        // 아이템 사용 텍스트 비활성화
        // 방향 리스트 초기화
        // 오브젝트 삭제
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
        // 물고기의 체력바는 터치한 부분보다 위에,
        // 물장구 이미지는 터치한 부분보다 아래에 위치
        Vector3 hpPos = pos + new Vector3(0, 150, 0);
        Vector3 dirPos = pos - new Vector3(0, 100, 0);

        yield return new WaitForSeconds(delayTime);

        // 확률에 따라 나오도록
        List<FishData> fish_Data = new List<FishData>();

        // 5일차 이하에는 노말 물고기가 나오도록
        if (int.Parse(GameManager.instance.data.dateCount) <= 5)
        {
            for (int i = 0; i < easyFishDatas.Length; i++)
            {
                fish_Probability = easyFishDatas[i].probability;
                // 아이템 썼을 때 확률 조정
                if (fm.useItem_white)
                {
                    if (easyFishDatas[i].color == 0)
                        fish_Probability *= 2f;

                    else if (easyFishDatas[i].color == 1)
                        fish_Probability *= 0f;
                }

                else if (fm.useItem_red)
                {
                    if (easyFishDatas[i].color == 0)
                        fish_Probability *= 0f;

                    else if (easyFishDatas[i].color == 1)
                        fish_Probability *= 2f;
                }

                // 확률만큼 리스트 저장
                for (int j = 0; j < fish_Probability; j++)
                {
                    fish_Data.Add(easyFishDatas[i]);
                }
            }
        }
        // 6일차부턴 기존 물고기 전부 생성 가능
        else
        {
            for (int i = 0; i < fishDatas.Length; i++)
            {
                fish_Probability = fishDatas[i].probability;
                // 아이템 썼을 때 확률 조정
                if (fm.useItem_white)
                {
                    if (fishDatas[i].color == 0)
                        fish_Probability *= 2f;

                    else if (fishDatas[i].color == 1)
                        fish_Probability *= 0f;
                }

                else if (fm.useItem_red)
                {
                    if (fishDatas[i].color == 0)
                        fish_Probability *= 0f;

                    else if (fishDatas[i].color == 1)
                        fish_Probability *= 2f;
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
        }

        // 아이템 사용 초기화
        fm.useItem_rare = false;
        fm.useItem_red = false;
        fm.useItem_white = false;

        Debug.Log(fish_Data.Count);

        // 랜덤한 인덱스 추출
        int fishNum = Random.Range(0, fish_Data.Count);

        fishData = fish_Data[fishNum]; // 확률에 따른 물고기 종류 지정

        Setup(fishData);  // 물고기 체력 및 회복력 셋업
        currHP = maxHP;   // 현재 체력을 맥스 체력으로
        fishing = true;   // 낚시 시작

        string targetValue = fishData.fishName; // 찾고자 하는 값
        int count = fish_Data.Count(x => x.fishName == targetValue); // 물고기 확률

        Debug.Log("물고기 확률 : " + count);

        Debug.Log("물장구 시작");

        // 물고기 체력바 생성 후 자신의 자식으로 넣음
        GameObject fishHP = Instantiate(hpBarPrefab, hpPos, Quaternion.identity);
        fishHP.transform.SetParent(transform);
        hp = fishHP.GetComponent<Image>();
        hp.fillAmount = 1f;

        // 방향 오브젝트 생성하여 각 방향을 리스트에 추가
        GameObject directionObjClone = Instantiate(directionObj, dirPos, Quaternion.identity);
        GameObject leftObj = directionObjClone.transform.GetChild(0).gameObject;
        GameObject rightObj = directionObjClone.transform.GetChild(1).gameObject;
        directionObjClone.transform.SetParent(transform);
        Vector3 dirObjScale = rightObj.transform.localScale;
        leftObj.gameObject.SetActive(false);
        rightObj.gameObject.SetActive(false);
        dirObj.Add(leftObj);
        dirObj.Add(rightObj);

        fm.touchTxt.gameObject.SetActive(true);
        fm.giveupBtn.gameObject.SetActive(true);

        // 이미지 생성하여 번갈아가며 띄움
        // 한바퀴 돌 때마다 방향 랜덤 설정
        while (fishing)
        {
            randomIndex = Random.Range(0, dirObj.Count); // 배열에서 랜덤 인덱스 선택

            if (randomIndex - 0 > 0)
            {
                dirObj[0].SetActive(false); // 나머지 오브젝트들은 비활성화
                dirObj[1].SetActive(true); // 랜덤으로 선택된 오브젝트를 활성화
                rightClick = true;
                leftClick = false;
                StartCoroutine(DirEff(dirObj[1], dirObjScale));
            }
            else
            {
                dirObj[0].SetActive(true); // 랜덤으로 선택된 오브젝트를 활성화
                dirObj[1].SetActive(false); // 나머지 오브젝트들은 비활성화
                rightClick = false;
                leftClick = true;
                StartCoroutine(DirEff(dirObj[0], dirObjScale));
            }

            yield return new WaitForSeconds(1f);
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
