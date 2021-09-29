using System;
using ScrollRectSteps_System.Scripts;
using ScrollRectSteps_System.Scripts.Views;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScrollRectSteps_System.Samples.Scripts
{
    public class DemoManager : MonoBehaviour
    {
        [SerializeField] private ScrollRectMainView scrollRectMainView;


        private void Start()
        {
            var helper = new ScrollRectDataHelper();
            scrollRectMainView.Initialization(helper);
        }

        
       
    }
    
    public class ScrollRectDataHelper:IScrollRectDataHelper
    {
        private int req = 0;
        public void GetItems(int currentMaxNumber, int loadStepCount, Action<IItemInfo[]> action)
        {
            
            
            if(++req > 5)
            {
                action?.Invoke(Array.Empty<IItemInfo>());
                return;
            }

            IItemInfo[] items = new IItemInfo[]
            {
                new ItemInfo()
                {
                    DisplayName = "aaaa",
                    SumGold = Random.Range(1,1000)
                },
                new ItemInfo()
                {
                    DisplayName = "bbbb",
                    SumGold = Random.Range(1,1000)
                },
                new ItemInfo()
                {
                    DisplayName = "bbbb",
                    SumGold = Random.Range(1,1000)
                },
                new ItemInfo()
                {
                    DisplayName = "cccc",
                    SumGold = Random.Range(1,1000)
                },
                new ItemInfo()
                {
                    DisplayName = "ddd",
                    SumGold = Random.Range(1,1000)
                },
                new ItemInfo()
                {
                    DisplayName = "ffff",
                    SumGold = Random.Range(1,1000)
                },
                new ItemInfo()
                {
                    DisplayName = "gggg",
                    SumGold = Random.Range(1,1000)
                },
                new ItemInfo()
                {
                    DisplayName = "hhhh",
                    SumGold = Random.Range(1,1000)
                }
            };
            
            action.Invoke(items);
        }
    }
}