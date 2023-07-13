using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable/FishData", fileName = "Fish Data")]
public class FishData : ScriptableObject
{
    public string fishName;     // �̸� 
    public int grade;           // ��޹�ȣ(0 = �븻, 1 = ����)
    public int color;           // �����ȣ(0 = �� ��, 1 = ���� ��)
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
