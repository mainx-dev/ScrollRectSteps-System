using System;
using MVVMBase.DataBinding;
using UnityEngine;

namespace ScrollRectSteps_System.Scripts.ViewModels
{
    public class ScrollRectMainViewModel:ViewModelBase
    {
        public readonly BindableProperty<IItemInfo[]> ItemsInfoAll = new BindableProperty<IItemInfo[]>();
        public readonly BindableProperty<IItemInfo[]> ItemsInfoAdd = new BindableProperty<IItemInfo[]>();
        public readonly BindableProperty<bool> Loader = new BindableProperty<bool>();
        public readonly BindableProperty<bool> ButtonLoader = new BindableProperty<bool>();
        
        private ScrollRectSettings scrollRectSettings;
        private CurrentScrollRectInfo currentScrollRectInfo;
        private IScrollRectDataHelper scrollRectDataHelper;

        private bool itemsEnd;

        

        public void Initialization(IScrollRectDataHelper scrollRectData, ScrollRectSettings settings)
        {
            scrollRectSettings = settings;
            currentScrollRectInfo = new CurrentScrollRectInfo();
            scrollRectDataHelper = scrollRectData;
            GetItems(SetItemsAll);
        }

        public void AddItems() 
            => GetItems(AddItems);
        
        private void SetItemsAll(IItemInfo[] itemInfos) 
            => AddPropertyItems(ItemsInfoAll, itemInfos);

        private void AddItems(IItemInfo[] itemInfos) 
            => AddPropertyItems(ItemsInfoAdd, itemInfos);

        private void GetItems(Action<IItemInfo[]> action)
        {
            if(Loader.Value || (!scrollRectSettings.forceGet && itemsEnd)) return;
            Loader.Value = true;
            scrollRectDataHelper.GetItems(currentScrollRectInfo.CurrentMaxNumber, scrollRectSettings, action);
        }

        private void AddPropertyItems(BindableProperty<IItemInfo[]> property,IItemInfo[] itemInfos)
        {
            Loader.Value = false;
            currentScrollRectInfo.CurrentMaxNumber += itemInfos.Length;
            itemsEnd = scrollRectSettings.loadStepCount > itemInfos.Length;
            property.Value = itemInfos;
            ButtonLoader.Value = !itemsEnd || scrollRectSettings.forceGet;        }
    }


    [Serializable]
    public class ScrollRectSettings
    {
        public bool forceGet;
        
        [Range(1,500)]
        public int loadStepCount;
    }

    
    public class CurrentScrollRectInfo
    {
        public int CurrentMaxNumber;
    }
}