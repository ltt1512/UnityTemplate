using UnityEngine;

namespace Gameplay
{
    public class CameraCtrl : BaseCtrl
    {
        public Camera cam;
        public Transform camPosSpawn;
        public Transform camPosGameplay;
        public override void Init()
        {
           // cam.transform.position = camPosSpawn.position;
           // MoveToGameplayPos();
        }

        public void MoveToGameplayPos()
        {
          //  cam.transform.position = camPosGameplay.position;   
        }
        

        public override void Reset()
        {
        }

        public override void StartGame()
        {
        }

        public void ZoomPlayAnim()
        {
            //var curSize = cam.orthographicSize;
            //cam.orthographicSize = curSize + 5f;
            //cam.DOOrthoSize(curSize, 0.5f);
        }
    }

}