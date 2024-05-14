using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Dots.Systems
{
    public partial class GetPlayerInputSystem : SystemBase
    {
        private Controls _controls;
        private float _nextSpawnTime;

        protected override void OnCreate()
        {
            if (!SystemAPI.TryGetSingleton(out InputComponent cmp))
            {
                EntityManager.CreateEntity(typeof(InputComponent));
            }
            
            _controls = new Controls();
            _controls.Enable();
        }

        protected override void OnUpdate()
        {
            var touch = Input.GetMouseButton(0) && _nextSpawnTime < SystemAPI.Time.ElapsedTime;
            var pos = Vector3.zero;
            
            if (touch)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hitInfo))
                {
                    pos = hitInfo.point;
                    pos.y += 1f;
                    _nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + .01f;
                }
            }
            
            SystemAPI.SetSingleton(new InputComponent { InputValue = pos, PressingLMB = touch});
        }
    }
}