using Joy.Core;
using RollABall.Components;
using UnityEngine;

namespace RollABall.Systems
{
    public class MoveWithKeyboardInput : ISystem
    {
        public Filter[] Filters { get; } =
        {
            Filter
        };

        private static readonly Filter Filter = new Filter()
            .AllOf(typeof(Speed), typeof(KeyboardInput), typeof(Rigidbody));

        public void Update()
        {
            var group = Manager.Query(Filter);
            var dt = Time.deltaTime;

            foreach (var entity in group)
            {
                var rb = entity.GetComponent<Rigidbody>();
                var speed = entity.GetComponent<Speed>().Value;
                var keyboardInput = entity.GetComponent<KeyboardInput>();

                rb.AddTorque(keyboardInput.Vertical * speed * dt, 0, -keyboardInput.Horizontal * speed * dt);
            }
        }
    }
}
