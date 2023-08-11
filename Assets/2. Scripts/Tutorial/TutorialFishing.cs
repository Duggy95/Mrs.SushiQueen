using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialFishing : TutorialBase
{
    public GameObject fishObj;
    public GameObject fishCanvas;
    public GameObject fishContent;
    public GameObject fishingRod;
    public GameObject lineStartPos;
    public GameObject fishInfo;
    public GameObject fishInfoImg;
    public GameObject inventoryBtn;
    public Text full_Txt;
    public Text fishRun;
    public Text touchTxt;
    public Text fishInfo_Txt;
    public Image fish_Img;
    FishData data;
    AudioSource audioSource;

    public int count = 0;
    public bool isFishing;
    public bool fishCome;
    public bool fishFull;
    Vector3 fishInfoOriginPos;
    Vector3 fishInfoOriginScale;

    public override void Enter()
    {
        audioSource = GetComponent<AudioSource>();
        print("fishing tutorial");
        fishInfoOriginScale = Vector3.one;
        fishInfoOriginPos = fishInfoImg.transform.position;
    }

    public override void Execute(TutorialManager tutorialManager)
    {
        if (fishCome)
        {
            tutorialManager.SetNextTutorial();
            fishCome = false;
        }
    }

    public override void Exit()
    {

    }

    public void Fishing()
    {
        if (isFishing == false)
        {
            // 물고기 잡는 중으로 변경
            isFishing = true;
            audioSource.PlayOneShot(SoundManager.instance.swing, 1);
            StartCoroutine(ThrowBobber(Input.mousePosition));
        }
        else
            return;
    }

    IEnumerator ThrowBobber(Vector3 mousePos)
    {
        yield return new WaitForSeconds(1);
        // 물고기 잡는 중으로 변경
        // 물고기 도망 텍스트 비활성화
        fishRun.gameObject.SetActive(false);
        Instantiate(fishObj, mousePos, Quaternion.identity);
        //transform.position = Input.mousePosition;
    }

    public void Fish(FishData fishData)
    {
        data = fishData;
        fishInfo.gameObject.SetActive(true);
        fishInfoImg.gameObject.SetActive(true);
        fishInfoImg.transform.parent = fishInfo.transform;
        fishInfoImg.transform.position = fishInfoOriginPos;
        fishInfoImg.transform.localScale = fishInfoOriginScale;
        fishInfoImg.transform.SetSiblingIndex(0);
        fishInfo_Txt.text = fishData.info.text;
        fish_Img.sprite = fishData.fishImg;
        audioSource.PlayOneShot(SoundManager.instance.fish, 1);
    }

    public void Run()
    {
        StartCoroutine(FishRun());
    }

    IEnumerator FishRun()
    {
        fishRun.gameObject.SetActive(true);
        isFishing = false;
        // 화면 누르면 텍스트 비활성화
        yield return new WaitForSeconds(2f);
        Debug.Log("다시");
        fishRun.gameObject.SetActive(false);
    }

    public void Get()
    {
        // 수족관에 이미지 추가
        Image[] _fishs = fishContent.gameObject.GetComponentsInChildren<Image>();

        bool isFull = true;
        bool isChange = false;

        for (int i = 0; i < _fishs.Length; i++)
        {
            if (_fishs[i].gameObject.name.Contains("Slot"))
            {
                if (_fishs[i].GetComponentInChildren<Text>().text.Contains(data.fishName))
                {
                    string valueToFind = data.fishName;
                    int newValue = 1;

                    // 특정 값(valueToFind)을 만족하는 첫 번째 요소의 인덱스를 찾기
                    int index = GameManager.instance.inventory_Fishs.FindIndex(fish => fish.fish_Name == valueToFind);

                    if (index != -1)
                    {
                        newValue += int.Parse(GameManager.instance.inventory_Fishs[index].fish_Count);

                        // 해당 인덱스(index)의 값 변경
                        //GameManager.instance.inventory_Fishs[index].fish_Count = newValue.ToString();
                        _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + newValue.ToString() + " " + "마리";
                        Debug.Log("중복 종류 " + index + " changed to " + newValue);
                        fishInfoImg.transform.parent = inventoryBtn.transform;
                        StartCoroutine(Eff());
                        StartCoroutine(EffMove());
                        isChange = true;
                        isFull = false;
                        break;
                    }
                }
            }
        }
        if (isChange == false)
        {
            for (int i = 0; i < _fishs.Length; i++)
            {
                if(_fishs[i].gameObject.name.Contains("Slot"))
                {
                    FishSlot _slot = _fishs[i].GetComponent<FishSlot>();
                    Image Img = null;
                    Image[] fishImgs = _fishs[i].GetComponentsInChildren<Image>();
                    foreach (Image fishImg in fishImgs)
                    {
                        if (fishImg.name.Contains("Img"))
                            Img = fishImg;
                    }

                    if (_slot.isEmpty == false)
                    {
                        Img.sprite = data.fishImg;
                        _slot.fish_ColorNum = data.color;
                        _slot.fish_GradeNum = data.grade;
                        _slot.fish_Name = data.fishName;

                        _fishs[i].GetComponentInChildren<Text>().text = data.fishName + "   " + "1 마리";
                        Debug.Log("안찼고 다른 종류");
                        //GameManager.instance.Save("f");
                        fishInfoImg.transform.parent = inventoryBtn.transform;
                        StartCoroutine(Eff());
                        StartCoroutine(EffMove());
                        _slot.isEmpty = true;
                        isFull = false;
                        break;
                    }
                }
            }
        }
        if (isFull)
        {
            full_Txt.gameObject.SetActive(true);
        }
        else
        {
            isFishing = false;
            fishInfo.gameObject.SetActive(false);
        }
        audioSource.PlayOneShot(SoundManager.instance.buttonClick, 1);
    }

    IEnumerator Eff()
    {
        Vector3 initialScale = new Vector3(1.1f, 1.1f, 1.1f);
        Vector3 targetScale = new Vector3(0.3f, 0.3f, 0.3f);
        float duration = 1f; // 크기 변화에 걸리는 시간

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산
            fishInfoImg.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
        audioSource.PlayOneShot(SoundManager.instance.getFish, 1);
    }

    IEnumerator EffMove()
    {
        float elapsedTime = 0f;
        float duration = 1f; // 이동 시간 (초)

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            fishInfoImg.transform.position = Vector3.Lerp(fishInfoImg.transform.position, inventoryBtn.transform.position, elapsedTime / duration);

            if (elapsedTime / duration >= 1)
            {
                fishInfoImg.transform.position = inventoryBtn.transform.position;
                fishInfoImg.gameObject.SetActive(false);
            }
            yield return null;
        }

        this.gameObject.SetActive(false);
    }

    public void Sell()
    {
        full_Txt.gameObject.SetActive(false);
        fishInfo.gameObject.SetActive(false);
        audioSource.PlayOneShot(SoundManager.instance.levelUp, 1);
        isFishing = false;
    }
}
