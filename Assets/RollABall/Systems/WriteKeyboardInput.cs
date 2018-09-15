using Joy.Core;
using RollABall.Components;
using UnityEngine;

namespace RollABall.Systems
{
    public class WriteKeyboardInput : ISystem
    {
        public Filter[] Filters { get; } =
        {
            Filter
        };

        private static readonly Filter Filter = new Filter().AllOf(typeof(KeyboardInput));

        public void Update()
        {
            var group = Manager.Query(Filter);
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            foreach (var entity in group)
            {
                var keyboardInput = entity.GetComponent<KeyboardInput>();
                keyboardInput.Horizontal = horizontal;
                keyboardInput.Vertical = vertical;
            }
        }
    }
}
