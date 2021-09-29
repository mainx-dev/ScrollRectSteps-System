using MVVMBase.DataBinding;
using ScrollRectSteps_System.Scripts;

namespace ScrollRectSteps_System.Samples.Scripts
{
    public class ItemViewModel:ViewModelBase
    {   
        public readonly BindableProperty<ItemInfo> ItemInfo = new BindableProperty<ItemInfo>();
       
        
        public void Initialization(IItemInfo itemInfo)
        {
            ItemInfo.Value = itemInfo as ItemInfo;
        }
    }
    
    
    
    public class ItemInfo:IItemInfo {
        public string DisplayName { get; set; }
        public int SumGold { get; set; }
    }

  
}
