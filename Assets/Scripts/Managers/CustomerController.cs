using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomerController : MonoBehaviour
{
    private GameObject currentCustumerMesh;
    private Sequence moveCustomerSequence;

    private SkinnedMeshRenderer[] customerMeshes;

    [SerializeField]
    private Customer customer;
    [SerializeField]
    private Transform startMovementTransfrom;
    [SerializeField]
    private Transform endMovementTransfrom;

    private void Awake()
    {
        Subscibe();

        InitData();

        GetCustomerMeshes();
        Reset();
    }
    private void Subscibe()
    {
        Events.OnGameReset += Reset;

        Events.OnGlobalPreGameStateEnter += OnPreGameState;
    }
    private void Reset()
    {
        customer.transform.rotation = Quaternion.Euler(SettingsManager.settings.customerStartRotation);
        SetNewCustomert();
    }

    private void OnPreGameState()
    {
        moveCustomerSequence = DOTween.Sequence();
        moveCustomerSequence.Append(customer.transform.DOMove(endMovementTransfrom.position, SettingsManager.settings.timeCustomerMove));
        moveCustomerSequence.AppendCallback(ChangeStateToGame);

        customer.Move();
    }

    private void ChangeStateToGame()
    {
        GlobalStateMachine.SetState<Game>();
        customer.Rotate();
    }

    private void GetCustomerMeshes()
    {
        customerMeshes = customer.GetComponentsInChildren<SkinnedMeshRenderer>(true);
    }

    private void InitData()
    {
    }

    private void SetNewCustomert()
    {
        currentCustumerMesh?.SetActive(false);
        currentCustumerMesh = null;

        SetRandomCustomer();

        currentCustumerMesh?.SetActive(true);

        customer.transform.position = startMovementTransfrom.position;
    }

    private void SetRandomCustomer()
    {
        currentCustumerMesh = customerMeshes[UnityEngine.Random.Range(0, customerMeshes.Length)].gameObject;
    }

}
