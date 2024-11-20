using UnityEngine;

public class LevelEvents : MonoBehaviour
{
    int numberOfCrystals = 3;

    [SerializeField] GameObject lightN01, lightN02, lightN03;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MyEvents.CrystalOne.AddListener(ActivateLightN01);
		MyEvents.CrystalOne.AddListener(ActivateLightN02);
		MyEvents.CrystalOne.AddListener(ActivateLightN03);
		MyEvents.CrystalOne.AddListener(OpenLevelExit);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateLightN01()
    {
        lightN01.GetComponent<Renderer>().material.color = Color.green;
    }

	void ActivateLightN02()
	{
        lightN02.GetComponent<Renderer>().material.color = Color.green;
	}

	void ActivateLightN03()
	{
        lightN03.GetComponent<Renderer>().material.color = Color.green;
	}

    void OpenLevelExit()
    {
        if(numberOfCrystals <= 0)
        {

        }
    }
}
