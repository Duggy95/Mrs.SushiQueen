using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNetaBtn : MonoBehaviour
{
    public GameObject netaPrefab; //ȸ ������
    public GameObject board;  //����
    public Text text;
    public FishData fishData;  //���� ������
    public int count;
    public bool isEmpty = true;

    public TutorialCook tc;
    Text iconTxt;

    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    public void FishBtn()
    {
        if (fishData == null)
            return;

        /*if (board == null)
        {
            board = GameObject.Find("Board_RawImage");  //���� ������Ʈ ã��.
        }*/

        Transform riceTr = board.transform.Find("Rice(Clone)").transform;  //���� ������Ʈ�� �ڽ����� �ִ� Rice ã��.

        bool hasFish = false;  //������ �̹� �����ϴ� ��

        //�ߺ�Ȯ��. ���� ȸ�� ���� �ִ��� Ȯ��.
        foreach (Transform child in riceTr)
        {
            if (child.CompareTag("FISH"))
            {
                hasFish = true;
                break;
            }
        }

        if (riceTr != null && !hasFish && count > 0 && tc.isReady)  //������ ���� ���� 
        {
            Vector3 netaTr = new Vector3(riceTr.position.x, riceTr.position.y + 10, 0);  //�� ���� ����
            GameObject neta = Instantiate(netaPrefab,
                                                            netaTr, Quaternion.identity, riceTr); //�� ���� ���ʿ� ȸ�����ϰ� ���� �ڽ����� �ֱ�.
            count--;
            UpdateUI();
            neta.GetComponent<Neta>().fishData = fishData;  //���������� �Ѱ��ֱ�.
            riceTr.gameObject.AddComponent<DragSushi>();  //�� ������Ʈ�� DragSushi ��ũ��Ʈ Add.
        }
    }

    public void UpdateUI()
    {
        text.text = fishData.fishName + "     " + count;
        iconTxt.text = fishData.fishName + "     " + count;
    }

    public void Txt(Text text)
    {
        iconTxt = text.GetComponent<Text>();
    }
}