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
    public GameObject directionObj; // 방향 오브젝트

    Image hp; //체력바 이미지
    TutorialFishing tf;
    GameObject _bobber;
    FishData fishData;
    AudioSource audioSource;
    List<GameObject> dirObj = new List<GameObject>();

    int maxHP; //최대체력
    int currHP;  //현재체력
    int heal;  // 회복력
    int _atk;  // 공격력
    int delayTime;   // 찌 던지고 물고기 나올 때까지 시간
    int randomIndex;
    float maxTime = 15f;  // 최대 타임
    float currTime;    // 현재 타임
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
        tf = GameObject.FindGameObjectWithTag("TUTORIAL").GetComponent<TutorialFishing>();
        transform.SetParent(tf.fishCanvas.transform);
        transform.SetSiblingIndex(1);  //2번째 자식.
        audioSource = GetComponent<AudioSource>();  

        _atk = 1;
    }

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
        /*if (fm.inventoryFullImg.activeSelf == true)
            return;*/

        // HP가 널이 아니고 현재 HP가 0보다 많으면서
        // 각 방향에 맞게 터치하면 공격함수 호출
        // 물고기 체력바 반영, 체력이 0이 되면 함수 호출 후 삭제
        if (fishing && hp != null && currHP > 0)
        {
            if (leftClick && Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.x > tf.fishingRod.transform.position.x &&
                    Input.mousePosition.y < tf.lineStartPos.transform.position.y)
                {
                    Atk();
                }
            }
            else if (rightClick && Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.x < tf.fishingRod.transform.position.x &&
                    Input.mousePosition.y < tf.lineStartPos.transform.position.y)
                {
                    Atk();
                }
            }
        }

        if (tf.isFishing == false)
            Die();
    }

    void Atk()
    {
        currHP -= _atk;
        hp.fillAmount = (float)currHP / maxHP;  // 남은 체력 비율에 맞게 줄어듬
        audioSource.PlayOneShot(SoundManager.instance.reelIn, 0.5f);

        if (currHP <= 0)
        {
            tf.Fish(fishData);
            tf.fishCome = true;
            Die();
        }
    }

    void Die()
    {
        // 터치설명 텍스트 비활성화
        // 방향 리스트 초기화
        // 오브젝트 삭제
        tf.touchTxt.gameObject.SetActive(false);
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

        fishData = fishDatas[tf.count]; // 확률에 따른 물고기 종류 지정
        Setup(fishData);  // 물고기 체력 및 회복력 셋업
        currHP = maxHP;   // 현재 체력을 맥스 체력으로
        fishing = true;   // 낚시 시작
        tf.count++;

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

        tf.touchTxt.gameObject.SetActive(true);  //터치 설명 텍스트 활성화

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
        }
    }
}
