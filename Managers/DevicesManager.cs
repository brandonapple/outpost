using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevicesManager : MonoBehaviour
{
    public static DevicesManager instance;

    public TemporaryShieldController temporaryShieldController;
    public BodyCollectorDevice bodyCollectorDevice;
    public FastRepairDevice fastRepairDevice;
    public SpikeController spikeController;
    public MindControllerDevice mindController;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SwitchOffAllDevices();
    }

    public void ActiveDevice(string deviceName)
    {
        switch (deviceName)
        {
            case "temporaryShield":
                temporaryShieldController.gameObject.SetActive(true);
                temporaryShieldController.ControllerOn();
                break;
            case "bodyCollector":
                bodyCollectorDevice.gameObject.SetActive(true);
                bodyCollectorDevice.DeviceSwitchOn();
                break;
            case "fastRepair":
                fastRepairDevice.gameObject.SetActive(true);
                break;
            case "spikeController":
                spikeController.gameObject.SetActive(true);
                break;
            case "mindController":
                mindController.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SwitchOffAllDevices()
    {
        temporaryShieldController.ControllerOff();
        bodyCollectorDevice.DeviceSwitchOff();

        temporaryShieldController.gameObject.SetActive(false);
        bodyCollectorDevice.gameObject.SetActive(false);
        fastRepairDevice.gameObject.SetActive(false);

        spikeController.gameObject.SetActive(false);
        mindController.gameObject.SetActive(false);
    }


}
