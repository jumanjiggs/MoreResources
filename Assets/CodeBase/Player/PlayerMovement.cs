using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float movementSpeed;

        private Vector3 moveDirection;
        private IInputService inputService;

        [Inject]
        private void Construct(IInputService input) =>
            inputService = input;

        private void Update() => 
            Moving();

        private void Moving()
        {
            ResetMoveDirection();
            UpdateMoveDirection();
            SetGravity();
            characterController.Move(moveDirection * Time.deltaTime);
        }

        private void UpdateMoveDirection()
        {
            Vector3 movement = new Vector3(inputService.Axis.x, 0, inputService.Axis.y);
            movement = transform.TransformDirection(movement);
            moveDirection = movement * movementSpeed;
        }

        private void ResetMoveDirection() =>
            moveDirection = Vector3.zero;

        private void SetGravity() =>
            moveDirection += Physics.gravity;
    }
}