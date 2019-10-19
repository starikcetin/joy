using starikcetin.Joy.Core;
using UnityEngine;

namespace starikcetin.Joy.Example
{
    public class ExampleCompositionRoot : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            Manager.RegisterSystem(new ExampleSystem());
        }

        private void Update()
        {
            Manager.UpdateSystems();
        }
    }
}
