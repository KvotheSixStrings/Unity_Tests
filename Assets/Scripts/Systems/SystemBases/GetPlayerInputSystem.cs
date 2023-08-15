using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = System.Diagnostics.Debug;

namespace TMG.Zombies
{
    [UpdateInGroup(typeof(InitializationSystemGroup),OrderLast = true)]
    public partial class GetPlayerInputSystem : SystemBase
    {
        private PlayerControls _Input;
        private Entity _player;

        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTagComponent>();
            RequireForUpdate<PlayerDataComponent>();
            
            _Input = new PlayerControls();
        }

        protected override void OnStartRunning()
        {
            Debug.Assert(_Input != null, "_Input == null");
            Debug.Assert(_Input.KeyboardMouseInput.Spawn != null, "_Input.KeyboardMouseInput.Spawn == null");
            
            _Input.Enable();
            _Input.KeyboardMouseInput.Spawn.performed += OnSpawn;
            _player = SystemAPI.GetSingletonEntity<PlayerTagComponent>();
        }

        protected override void OnUpdate()
        {
            if (PlayerRef.Player == null) return;
            if (PlayerRef.Transform == null) return;
            
            Debug.Assert(_Input != null, "_Input == null");
            Debug.Assert(_Input.KeyboardMouseInput.PlayerMovement != null, "_Input.KeyboardMouseInput.PlayerMovement == null");
            Debug.Assert(_Input.KeyboardMouseInput.Rotation != null, "_Input.KeyboardMouseInput.Rotation == null");
            
            var curMovementInput = _Input.KeyboardMouseInput.PlayerMovement.ReadValue<Vector3>();
            var curRotationInput = _Input.KeyboardMouseInput.Rotation.ReadValue<Vector2>();
            
            var curControlInput = SystemAPI.GetComponent<PlayerDataComponent>(_player);
            
            var moveBy = curMovementInput * curControlInput.MovementSpeed * SystemAPI.Time.DeltaTime;
            
            PlayerRef.Transform.Translate(moveBy, Space.Self);
            var newPosition = PlayerRef.Transform.position;

            var rotateBy = new Vector3(curRotationInput.y * -1, curRotationInput.x, 0) * curControlInput.RotationSpeed * SystemAPI.Time.DeltaTime;
            PlayerRef.Transform.Rotate(Vector3.up * rotateBy.y, Space.World);
            PlayerRef.Transform.Rotate(Vector3.right * rotateBy.x, Space.World);
            var rotation = PlayerRef.Transform.rotation;
            rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
            PlayerRef.Transform.rotation = rotation;
            var newRotation = rotation;

            curControlInput.Position = newPosition;
            curControlInput.Rotation = newRotation;
            curControlInput.Scale = PlayerRef.Transform.localScale;

            SystemAPI.SetComponent(_player, curControlInput);
        }

        protected override void OnStopRunning()
        {
            _Input.KeyboardMouseInput.Spawn.performed -= OnSpawn;
            _Input.Disable();
            _player = Entity.Null;
        }

        private void OnSpawn(InputAction.CallbackContext obj)
        {
            if (!SystemAPI.Exists(_player)) return;

            SystemAPI.SetComponentEnabled<PlayerSpawnTagInputComponent>(_player, true);
        }
    }
}