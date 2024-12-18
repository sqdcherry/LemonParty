//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/PlayerInputs/Main.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputs : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs ()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Main"",
    ""maps"": [
        {
            ""name"": ""Phone"",
            ""id"": ""6b32dc30-d346-4fc2-b3dc-190ce2695545"",
            ""actions"": [
                {
                    ""name"": ""Moving"",
                    ""type"": ""Value"",
                    ""id"": ""b8f94641-7877-40a9-8286-99d04ace3f93"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a96eed00-ce92-4987-8146-6f51343d2202"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Phone"",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""247bc3b0-7171-46d2-a8b9-8440c279095a"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Phone"",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Phone"",
            ""bindingGroup"": ""Phone"",
            ""devices"": []
        }
    ]
}");
        // Phone
        m_Phone = asset.FindActionMap("Phone", throwIfNotFound: true);
        m_Phone_Moving = m_Phone.FindAction("Moving", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Phone
    private readonly InputActionMap m_Phone;
    private List<IPhoneActions> m_PhoneActionsCallbackInterfaces = new List<IPhoneActions>();
    private readonly InputAction m_Phone_Moving;
    public struct PhoneActions
    {
        private PlayerInputs  m_Wrapper;
        public PhoneActions(PlayerInputs  wrapper) { m_Wrapper = wrapper; }
        public InputAction @Moving => m_Wrapper.m_Phone_Moving;
        public InputActionMap Get() { return m_Wrapper.m_Phone; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PhoneActions set) { return set.Get(); }
        public void AddCallbacks(IPhoneActions instance)
        {
            if (instance == null || m_Wrapper.m_PhoneActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PhoneActionsCallbackInterfaces.Add(instance);
            @Moving.started += instance.OnMoving;
            @Moving.performed += instance.OnMoving;
            @Moving.canceled += instance.OnMoving;
        }

        private void UnregisterCallbacks(IPhoneActions instance)
        {
            @Moving.started -= instance.OnMoving;
            @Moving.performed -= instance.OnMoving;
            @Moving.canceled -= instance.OnMoving;
        }

        public void RemoveCallbacks(IPhoneActions instance)
        {
            if (m_Wrapper.m_PhoneActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPhoneActions instance)
        {
            foreach (var item in m_Wrapper.m_PhoneActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PhoneActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PhoneActions @Phone => new PhoneActions(this);
    private int m_PhoneSchemeIndex = -1;
    public InputControlScheme PhoneScheme
    {
        get
        {
            if (m_PhoneSchemeIndex == -1) m_PhoneSchemeIndex = asset.FindControlSchemeIndex("Phone");
            return asset.controlSchemes[m_PhoneSchemeIndex];
        }
    }
    public interface IPhoneActions
    {
        void OnMoving(InputAction.CallbackContext context);
    }
}
