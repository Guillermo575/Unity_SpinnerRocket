using UnityEngine;
using System.Linq;
public class SmartHUD : MonoBehaviour
{
    void Start()
    {
        //ReLocateHUD();
    }
    void Update()
    {
        //ReLocateHUD();
    }
    public void ReLocateHUD()
    {
        RectTransform objCanvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        var objPivot = objCanvas.pivot;
        Vector3 minValues = new Vector3(objCanvas.position.x - (objPivot.x * objCanvas.sizeDelta.x), objCanvas.position.y - (objPivot.y * objCanvas.sizeDelta.y));
        Vector3 maxValues = new Vector3(objCanvas.position.x + (objPivot.x * objCanvas.sizeDelta.x), objCanvas.position.y + (objPivot.y * objCanvas.sizeDelta.y));
        var lst = objCanvas.GetComponentsInChildren<RectTransform>(true);
        var lstHUD = (from x in lst where x.parent == objCanvas select x).ToList();
        foreach (var x in lstHUD)
        {
            var lstObj = (from y in lst where y.parent == x select y).ToList();
            foreach (var y in lstObj)
            {
                var objT = y.transform.position;
                var MinX = Mathf.Sqrt(Mathf.Pow(objT.x - minValues.x, 2));
                var MinY = Mathf.Sqrt(Mathf.Pow(objT.y - minValues.y, 2));
                var MaxX = Mathf.Sqrt(Mathf.Pow(objT.x - maxValues.x, 2));
                var MaxY = Mathf.Sqrt(Mathf.Pow(objT.y - maxValues.y, 2));
                objT.x = MinX < MaxX ? minValues.x : maxValues.x;
                objT.y = MinY < MaxY ? minValues.y : maxValues.y;
                y.transform.position = objT;
            }
        }
    }
}