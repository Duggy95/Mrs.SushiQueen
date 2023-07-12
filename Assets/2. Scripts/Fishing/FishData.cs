using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable/FishData", fileName = "Fish Data")]
public class FishData : ScriptableObject
{
    public string fishName;     // 이름 
    public int fishNum;         // 식별번호
    public int hp;              // 체력
    public int heal;            // 회복력 
    public int gold;            // 골드
    public float probability;   // 확률
    public Sprite fishImg;      // 생선 이미지
    public Sprite netaImg;      // 회 이미지
    public Text info;       // 생선 정보
    
    // 잡았을 때 - 이름 이미지, 정보, 골드
    // 수족관 - 생선이미지
    // 요리 - 회이미지
}
