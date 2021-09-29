using System;

namespace ScrollRectSteps_System.Scripts
{
    public interface IScrollRectDataHelper
    {
        void GetItems(int currentMaxIndex, int loadStepCount, Action<IItemInfo[]> action);
    }
}