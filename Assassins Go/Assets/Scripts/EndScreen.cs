using UnityEngine;
using UnityEngine.PostProcessing;

public class EndScreen : MonoBehaviour
{

    public PostProcessingProfile blurProfile;
    public PostProcessingProfile normalProfile;
    public PostProcessingBehaviour cameraPostProcess;

    public void EnableCameraBlur(bool state)
    {
        if (cameraPostProcess != null && blurProfile != null && normalProfile != null)
        {
            cameraPostProcess.profile = (state) ? blurProfile : normalProfile;
        }
    }

}
