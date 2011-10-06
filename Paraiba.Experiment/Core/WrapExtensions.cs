using System;
using System.Collections.Generic;
using System.ComponentModel;
using Paraiba.Collections;
using Paraiba.Utility;

namespace Paraiba.Core
{
    public static class WrapExtensions
    {
        public static CastedWrap<TTo, TFrom> Cast<TTo, TFrom>(this IWrap<TFrom> wrap)
            where TFrom : TTo
        {
            return new CastedWrap<TTo, TFrom>(wrap);
        }

        public static ValueWrap<T> ToValueWrap<T>(this T value)
        {
            return new ValueWrap<T>(value);
        }

        public static LazyFlagWrap<T> ToLazyFlagWrap<T>(this Func<T> func)
        {
            return new LazyFlagWrap<T>(func);
        }

        public static LazyWrap<T> ToLazyWrap<T>(this Func<T> func)
        {
            return new LazyWrap<T>(func);
        }

        public static ReadonlyLazyWrap<T> ToReadonlyLazyWrap<T>(this Func<T> func)
        {
            return new ReadonlyLazyWrap<T>(func);
        }

        public static ReadonlyLazyFlagWrap<T> ToReadonlyLazyFlagWrap<T>(this Func<T> func)
        {
            return new ReadonlyLazyFlagWrap<T>(func);
        }

        public static FuncWrap<T> ToFuncWrap<T>(this Func<T> func)
        {
            return new FuncWrap<T>(func);
        }

        public static ReadonlyFuncWrap<T> ToReadonlyFuncWrap<T>(this Func<T> func)
        {
            return new ReadonlyFuncWrap<T>(func);
        }

        public static MutableLazyFlagWrap<T> ToMutableLazyFlagWrap<T>(this Func<T> func)
        {
            return new MutableLazyFlagWrap<T>(func);
        }

        #region ListWrapper

        // TODO: より良い名前を
        public static ListWrapper<TWrap, TList, T> ToListWrap<TWrap, TList, T>(this TWrap wrap)
            where TWrap : IWrap<TList>
            where TList : IList<T>
        {
            return new ListWrapper<TWrap, TList, T>(wrap);
        }

        public static ListWrapper<TList, T> ToListWrap<TList, T>(this IWrap<TList> wrap)
            where TList : IList<T>
        {
            return new ListWrapper<TList, T>(wrap);
        }

        public static ListWrapper<T[], T> ToListWrap<T>(this IWrap<T[]> wrap)
        {
            return new ListWrapper<T[], T>(wrap);
        }

        public static ListWrapper<IList<T>, T> ToListWrap<T>(this IWrap<IList<T>> wrap)
        {
            return new ListWrapper<IList<T>, T>(wrap);
        }

        public static ListWrapper<List<T>, T> ToListWrap<T>(this IWrap<List<T>> wrap)
        {
            return new ListWrapper<List<T>, T>(wrap);
        }

        public static ListWrapper<BindingList<T>, T> ToListWrap<T>(this IWrap<BindingList<T>> wrap)
        {
            return new ListWrapper<BindingList<T>, T>(wrap);
        }

        public static ListWrapper<DisposableList<T>, T> ToListWrap<T>(this IWrap<DisposableList<T>> wrap)
            where T : IDisposable
        {
            return new ListWrapper<DisposableList<T>, T>(wrap);
        }

        #endregion

        #region ReadonlyListWrapper

        public static ReadonlyListWrapper<TWrap, TList, T> ToReadonlyListWrap<TWrap, TList, T>(this TWrap wrap)
            where TWrap : IWrap<TList>
            where TList : IList<T>
        {
            return new ReadonlyListWrapper<TWrap, TList, T>(wrap);
        }

        public static ReadonlyListWrapper<TList, T> ToReadonlyListWrap<TList, T>(this IWrap<TList> wrap)
            where TList : IList<T>
        {
            return new ReadonlyListWrapper<TList, T>(wrap);
        }

        public static ReadonlyListWrapper<T[], T> ToReadonlyListWrap<T>(this IWrap<T[]> wrap)
        {
            return new ReadonlyListWrapper<T[], T>(wrap);
        }

        public static ReadonlyListWrapper<IList<T>, T> ToReadonlyListWrap<T>(this IWrap<IList<T>> wrap)
        {
            return new ReadonlyListWrapper<IList<T>, T>(wrap);
        }

        public static ReadonlyListWrapper<List<T>, T> ToReadonlyListWrap<T>(this IWrap<List<T>> wrap)
        {
            return new ReadonlyListWrapper<List<T>, T>(wrap);
        }

        public static ReadonlyListWrapper<BindingList<T>, T> ToReadonlyListWrap<T>(this IWrap<BindingList<T>> wrap)
        {
            return new ReadonlyListWrapper<BindingList<T>, T>(wrap);
        }

        public static ReadonlyListWrapper<DisposableList<T>, T> ToReadonlyListWrap<T>(this IWrap<DisposableList<T>> wrap)
            where T : IDisposable
        {
            return new ReadonlyListWrapper<DisposableList<T>, T>(wrap);
        }

        #endregion

        #region XReadonlyListWrapper

        public static XReadonlyListWrapper<TWrap, TList, TOrgItem, TItem> ToReadonlyListWrap<TWrap, TList, TOrgItem, TItem>(
            this TWrap wrap)
            where TWrap : IWrap<TList>
            where TList : IList<TOrgItem>
            where TOrgItem : TItem
        {
            return new XReadonlyListWrapper<TWrap, TList, TOrgItem, TItem>(wrap);
        }

        public static XReadonlyListWrapper<TList, TOrgItem, TItem> ToReadonlyListWrap<TList, TOrgItem, TItem>(
            this IWrap<TList> wrap)
            where TList : IList<TOrgItem>
            where TOrgItem : TItem
        {
            return new XReadonlyListWrapper<TList, TOrgItem, TItem>(wrap);
        }

        public static XReadonlyListWrapper<TOrgItem[], TOrgItem, TItem> ToReadonlyListWrap<TOrgItem, TItem>(
            this IWrap<TOrgItem[]> wrap)
            where TOrgItem : TItem
        {
            return new XReadonlyListWrapper<TOrgItem[], TOrgItem, TItem>(wrap);
        }

        public static XReadonlyListWrapper<IList<TOrgItem>, TOrgItem, TItem> ToReadonlyListWrap<TOrgItem, TItem>(
            this IWrap<IList<TOrgItem>> wrap)
            where TOrgItem : TItem
        {
            return new XReadonlyListWrapper<IList<TOrgItem>, TOrgItem, TItem>(wrap);
        }

        public static XReadonlyListWrapper<List<TOrgItem>, TOrgItem, TItem> ToReadonlyListWrap<TOrgItem, TItem>(
            this IWrap<List<TOrgItem>> wrap)
            where TOrgItem : TItem
        {
            return new XReadonlyListWrapper<List<TOrgItem>, TOrgItem, TItem>(wrap);
        }

        public static XReadonlyListWrapper<BindingList<TOrgItem>, TOrgItem, TItem> ToReadonlyListWrap<TOrgItem, TItem>(
            this IWrap<BindingList<TOrgItem>> wrap)
            where TOrgItem : TItem
        {
            return new XReadonlyListWrapper<BindingList<TOrgItem>, TOrgItem, TItem>(wrap);
        }

        public static XReadonlyListWrapper<DisposableList<TOrgItem>, TOrgItem, TItem> ToReadonlyListWrap<TOrgItem, TItem>(
            this IWrap<DisposableList<TOrgItem>> wrap)
            where TOrgItem : TItem, IDisposable
        {
            return new XReadonlyListWrapper<DisposableList<TOrgItem>, TOrgItem, TItem>(wrap);
        }

        #endregion
    }
}