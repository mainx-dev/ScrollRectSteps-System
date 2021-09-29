using MVVMBase.ViewBase;
using ScrollRectSteps_System.Scripts;
using TMPro;
using UnityEngine;

namespace ScrollRectSteps_System.Samples.Scripts
{
    public class ItemView:GuiView<ItemViewModel>,IItemView
    {
        [SerializeField]
        private TextMeshProUGUI displayName;
        
        [SerializeField]
        private TextMeshProUGUI sumGold;
        
        
        private void Awake()
        {
            BindingContext = new ItemViewModel();
            Reveal();
        }
        
       
        
        protected override void OnInitialize()
        {
            base.OnInitialize();
            Binder.Add<ItemInfo>(nameof(BindingContext.ItemInfo),OnClansEntityPropertyValueChanged);
        }


        private void OnClansEntityPropertyValueChanged(ItemInfo oldValue, ItemInfo newValue)
        {
            displayName.text = newValue.DisplayName;
            sumGold.text = newValue.SumGold.ToString();
        }

        
        public void Initialization(IItemInfo itemInfo) 
            => BindingContext.Initialization(itemInfo);
    }
}
