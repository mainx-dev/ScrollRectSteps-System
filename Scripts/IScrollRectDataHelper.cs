using System;
using ScrollRectSteps_System.Scripts.ViewModels;

namespace ScrollRectSteps_System.Scripts
{
    public interface IScrollRectDataHelper
    {
        void GetItems(int currentMaxIndex, ScrollRectSettings settings, Action<IItemInfo[]> action);
    }
}