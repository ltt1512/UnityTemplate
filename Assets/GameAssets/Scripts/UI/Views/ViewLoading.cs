using Doozy.Runtime.Reactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.UI
{
    public class ViewLoading : MonoBehaviour
    {
        public Progressor progressBar;
        public Progressor progressorTxt;

        private void Start()
        {
            progressBar.SetValueAt(0);
            progressorTxt.SetValueAt(0);
        }

        public void StartLoading()
        {

        }
    }
}