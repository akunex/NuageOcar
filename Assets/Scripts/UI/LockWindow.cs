using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockWindow : MonoBehaviour
{
    public Image frame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        RectTransform rect = frame.GetComponent<RectTransform>();

        float width = rect.rect.width;
        float height = rect.rect.height;        
        
        Vector3 framePosition = frame.transform.position;

        float positionPlusX = framePosition.x + (width / 2);
        float positionMoinsX = framePosition.x - (width / 2);
        float positionPlusY = framePosition.y + (height / 2);
        float positionMoinsY = framePosition.y - (height / 2);

        float maxX = Screen.width - (width / 2);
        float minX = 0 + (width / 2);
        float maxY = Screen.height - (height / 2);
        float minY = 0 + (height / 2);

        if (positionMoinsX <= 0 && positionMoinsY <= 0)
            frame.transform.position = new Vector3(minX, minY, framePosition.z);
        if (positionMoinsX <= 0 && positionPlusY >= Screen.height)
            frame.transform.position = new Vector3(minX, maxY, framePosition.z);
        if (positionPlusX >= Screen.width && positionPlusY >= Screen.height)
            frame.transform.position = new Vector3(maxX, maxY, framePosition.z);
        if (positionPlusX >= Screen.width && positionMoinsY <= 0)
            frame.transform.position = new Vector3(maxX, minY, framePosition.z);

        framePosition = frame.transform.position;
        positionPlusX = framePosition.x + (width / 2);
        positionMoinsX = framePosition.x - (width / 2);
        positionPlusY = framePosition.y + (height / 2);
        positionMoinsY = framePosition.y - (height / 2);

        if (positionMoinsX <= 0)
            frame.transform.position = new Vector3(minX, framePosition.y, framePosition.z);
        if (positionPlusX >= Screen.width)
            frame.transform.position = new Vector3(maxX, framePosition.y, framePosition.z);
        if (positionMoinsY <= 0)
            frame.transform.position = new Vector3(framePosition.x, minY, framePosition.z);
        if (positionPlusY >= Screen.height)
            frame.transform.position = new Vector3(framePosition.x, maxY, framePosition.z);


    }
}
