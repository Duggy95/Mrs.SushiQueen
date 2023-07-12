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

    Image HP; //체력바 이미지
    FishingManager fM;  // 낚시 매니저
    GameObject _bobber;

    int maxHP = 10; //최대체력
    int currHP;  //현재체력
    int currHpPercent;  // 체력바 퍼센티지
    int atk = 1;  // 공격력
    float maxTime = 5f;  // 최대 타임
    float currTime;    // 현재 타임

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
        // 찌 이미지를 터치한 곳에 생성
        _bobber = Instantiate(bobber, bobberPos, Quaternion.identity);
        _bobber.transform.parent = transform;
        // 낚싯대부터 찌까지 라인렌더러 그리기
        Vector3 pos = bobberPos - new Vector3(0, 25, 0);
        // 코루틴 함수 호출
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
            Debug.Log("현재 시간 " + currTime);

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

        Debug.Log("현재 체력 " + currHP);

        if (Input.GetMouseButtonDown(0) && currHP > 0)
        {
            currHP -= atk;
            currHpPercent = currHP / maxHP;  //남은시간 비율
            HP.fillAmount = currHpPercent;  //타이머는 남은시간 비율에 맞게 줄어듬
        }
    }

    IEnumerator FishingEff(Vector3 pos)
    {
        // 물고기의 체력바는 터치한 부분보다 위에,
        // 물장구 이미지는 터치한 부분보다 아래에 위치
        Vector3 hpPos = pos + new Vector3(0, 100, 0);
        yield return new WaitForSeconds(Random.Range(1, 4));

        StartCoroutine(TimeDown());
        Debug.Log("물장구 시작");

        GameObject fishHP = Instantiate(hpBarPrefab, hpPos, Quaternion.identity);
        fishHP.transform.parent = transform;
        HP = fishHP.GetComponent<Image>();
        HP.fillAmount = 1f;

        StartCoroutine(Atk());

        // 이미지 생성하여 번갈아가며 띄움
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

            // 물고기를 잡거나 놓칠 때까지 반복
            water_.gameObject.SetActive(true);
            _water.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            water_.gameObject.SetActive(false);
            _water.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("삭제");
        Destroy(gameObject);
    }
}
