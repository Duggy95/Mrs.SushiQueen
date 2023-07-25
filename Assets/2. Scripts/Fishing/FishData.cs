using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable/FishData", fileName = "Fish Data")]
public class FishData : ScriptableObject
{
    public string fishName;     // 이름 
    public int grade;           // 등급번호(0 = 노말, 1 = 레어)
    public int color;           // 색깔번호(0 = 흰 살, 1 = 붉은 살)
    public int hp;              // 체력
    public int heal;            // 회복력 
    public int gold;            // 골드
    public float probability;   // 확률
    public Sprite fishImg;      // 생선 이미지
    public Sprite netaImg;      // 회 이미지
    public Text info;       // 생선 정보
}
