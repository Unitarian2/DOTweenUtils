

namespace DOTweenUtils
{
    public abstract class DOTweenUtilsItemValueFeed<T> : DOTweenUtilsItem<T> where T : DOTweenUtilsItemValueFeed<T>
    {
        public abstract T SetValue(float value);

    }
}

