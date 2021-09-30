using System;
using JetBrains.Annotations;
using MVVMBase.ViewBase;
using ScrollRectSteps_System.Scripts.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace ScrollRectSteps_System.Scripts.Views
{
    public class ScrollRectMainView : GuiView<ScrollRectMainViewModel>
    {
        [SerializeField] private Transform itemTransforms;
        
        [SerializeField,Space] private GameObject itemViewPrefab;
        [SerializeField] private GameObject loaderPrefab;
        
        
        [SerializeField,CanBeNull, Header("Can Be Null")] private Button loadButtonPrefab;
        
        [SerializeField,Space] private ScrollRect scrollRect;
        [SerializeField] private ScrollRectSettings dRectSettings;


        private GameObject _loader;
        private Button _loaderButton;

        private void Awake()
        {
            BindingContext = new ScrollRectMainViewModel();
            Reveal();
        }

        private void OnEnable() 
            => scrollRect.onValueChanged.AddListener(ValueChanged);

        private void OnDisable() 
            => scrollRect.onValueChanged.AddListener(ValueChanged);


        protected override void OnInitialize()
        {
            base.OnInitialize(); 
            Binder.Add<IItemInfo[]>(nameof(BindingContext.ItemsInfoAll), OnPropertyValueItemsInfo);
            Binder.Add<IItemInfo[]>(nameof(BindingContext.ItemsInfoAdd), OnPropertyValueItemsInfoAdd);
            Binder.Add<bool>(nameof(BindingContext.Loader), OnPropertyValueLoader);
            Binder.Add<bool>(nameof(BindingContext.ButtonLoader), OnPropertyValueButtonLoader);
        }

      

        public void Initialization(IScrollRectDataHelper scrollRectData)
            => BindingContext.Initialization(scrollRectData, dRectSettings);
        
       

        private void OnPropertyValueItemsInfo(IItemInfo[] oldValue, IItemInfo[] newValue)
        {
            foreach (Transform child in itemTransforms)
            {
                Destroy(child.gameObject);
            }

            OnPropertyValueItemsInfoAdd(oldValue,newValue);
        }
        
        private void OnPropertyValueItemsInfoAdd(IItemInfo[] oldValue, IItemInfo[] newValue)
        {
            Array.ForEach(newValue, itemsInfo =>
            {
                var referrerItemView = Instantiate(itemViewPrefab, itemTransforms);
                referrerItemView.GetComponent<IItemView>().Initialization(itemsInfo);
            });
        }



        
        private void OnPropertyValueLoader(bool oldValue, bool newValue)
        {
            if (newValue == false)
            {
                if(_loader) Destroy(_loader);
                return;
            }
            
            _loader = Instantiate(loaderPrefab, itemTransforms);
        }  
        
        private void OnPropertyValueButtonLoader(bool oldValue, bool newValue)
        {
            if (newValue == false)
            {
                if(_loaderButton != null && _loaderButton.gameObject != null) Destroy(_loaderButton.gameObject);
                return;
            }
            
            if(loadButtonPrefab == null) return;
            _loaderButton = Instantiate(loadButtonPrefab, itemTransforms);
            _loaderButton.onClick.AddListener(() =>
            {
                BindingContext.ButtonLoader.Value = false;
                BindingContext.AddItems();
            });
        }
        
        private void ValueChanged(Vector2 arg0)
        {
            if(_loaderButton != null) return;
            Debug.Log(arg0);
            if (scrollRect.vertical && arg0.y <= 0)
            {
                BindingContext.AddItems();
            }

            if (scrollRect.horizontal && arg0.x >= 1)
            {
                BindingContext.AddItems();
            }
        }
    }
}