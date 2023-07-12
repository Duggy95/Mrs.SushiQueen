using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable/FishData", fileName = "Fish Data")]
public class FishData : ScriptableObject
{
    public string fishName;     // �̸� 
    public int fishNum;         // �ĺ���ȣ
    public int hp;              // ü��
    public int heal;            // ȸ���� 
    public int gold;            // ���
    public float probability;   // Ȯ��
    public Sprite fishImg;      // ���� �̹���
    public Sprite netaImg;      // ȸ �̹���
    public Text info;       // ���� ����
    
    // ����� �� - �̸� �̹���, ����, ���
    // ������ - �����̹���
    // �丮 - ȸ�̹���
}
