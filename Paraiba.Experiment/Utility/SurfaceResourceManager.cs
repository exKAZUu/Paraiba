#region License

// Copyright (C) 2011-2012 Kazunori Sakamoto
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using Paraiba.Collections.Generic;
using Paraiba.Core;
using Paraiba.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Linq;
using Paraiba.Wrap;

namespace Paraiba.Utility {
    public class SurfaceResourceManager
            : ResourceManager<string, IList<MutableLazyFlagWrap<Bitmap>>> {
        /// <summary>
        ///   リソースが解放済みの MutableLazyFlagWrap オブジェクトを保持するための辞書
        /// </summary>
        private readonly IDictionary<string, IList<MutableLazyFlagWrap<Bitmap>>>
                _disposedDict;

        public SurfaceResourceManager(int maxUsedMemorySize)
                : this(maxUsedMemorySize, BitmapUtil.Load,
                       (bmp, w, h) => bmp.SplitToBitmaps(w, h)) {}

        public SurfaceResourceManager(
                int maxUsedMemorySize,
                Func<string, Bitmap> defaultLoadBitmapFunc,
                Func<Bitmap, int, int, IList<Bitmap>> defaultSplitBitmapFunc) {
            Contract.Requires(maxUsedMemorySize > 0);
            Contract.Requires(defaultLoadBitmapFunc != null);
            Contract.Requires(defaultSplitBitmapFunc != null);

            MaxUsedMemorySize = maxUsedMemorySize;
            DefaultLoadBitmapFunc = defaultLoadBitmapFunc;
            DefaultSplitBitmapFunc = defaultSplitBitmapFunc;
            _disposedDict =
                    new Dictionary<string, IList<MutableLazyFlagWrap<Bitmap>>>();
        }

        /// <summary>
        ///   Bitmap オブジェクトを生成するためのデリゲート
        /// </summary>
        public Func<string, Bitmap> DefaultLoadBitmapFunc { get; private set; }

        /// <summary>
        ///   一つの Bitmap オブジェクトを分割して、複数の Bitmap オブジェクトを生成するためのデリゲート
        /// </summary>
        public Func<Bitmap, int, int, IList<Bitmap>> DefaultSplitBitmapFunc { get; private set; }

        public int MaxUsedMemorySize { get; private set; }

        public int UsedMemorySize { get; private set; }

        public int ResourceCount { get; private set; }

        protected override void AddingResource(
                IList<MutableLazyFlagWrap<Bitmap>> rc) {
            ResourceCount++;
        }

        protected override void RemovingResource(
                IList<MutableLazyFlagWrap<Bitmap>> rc) {
            ResourceCount--;
        }

        protected override void DisposeResource(
                string key, IList<MutableLazyFlagWrap<Bitmap>> rc) {
            foreach (var wrap in rc) {
                // リソースが生成(評価)されていたら解放する
                if (wrap.Evaluated) {
                    wrap.Value.Dispose();
                    wrap.Evaluated = false;
                }
            }
            // 二重生成防止のため、解放したリソース(Wrap)を記憶しておく
            _disposedDict.Add(key, rc);
        }

        private Bitmap LoadBitmap(
                string filePath, Func<string, Bitmap> loadBitmapFunc) {
            var bmp = loadBitmapFunc(filePath);
            UsedMemorySize += bmp.EstimateMemorySize();
            // 閾値以上のメモリサイズを使用して、かつリソースの数が2つ以上ある場合はリソースを解放
            while (UsedMemorySize > MaxUsedMemorySize && ResourceCount >= 2) {
                Release();
            }
            return bmp;
        }

        private MutableLazyFlagWrap<Bitmap> GetRowWrap(
                string filePath, Func<string, Bitmap> loadBitmapFunc) {
            Contract.Requires(filePath != null);
            Contract.Requires(loadBitmapFunc != null);

            filePath = Path.GetFullPath(filePath);

            IList<MutableLazyFlagWrap<Bitmap>> list = null;
            Func<Bitmap> func = () => {
                // ノードが未登録ならば登録する
                if (GetNode(filePath) == null) {
                    AddNode(filePath, list);
                }
                // 実際に画像を読み込んで使用メモリ量を更新
                return LoadBitmap(filePath, loadBitmapFunc);
            };

            // 既にノードが存在するかチェック
            var node = GetNode(filePath);
            if (node != null) {
                list = node.Value.Resource;
                if (list.Count != 1) {
                    throw new InvalidOperationException(
                            "既存のBitmapリストの長さが 1 ではありません。");
                }
                list[0].Evaluator = func;
                return list[0];
            }

            // 解放済みのリソースが残っているかチェック
            if (_disposedDict.TryGetValue(filePath, out list)) {
                if (list.Count != 1) {
                    throw new InvalidOperationException(
                            "既存のBitmapリストの長さが 1 ではありません。");
                }
                list[0].Evaluator = func;
                _disposedDict.Remove(filePath);
            } else {
                list = new[] { func.ToMutableLazyFlagWrap() };
            }
            AddNode(filePath, list);
            return list[0];
        }

        private IList<MutableLazyFlagWrap<Bitmap>> GetRowList(
                string filePath, Func<string, IList<Bitmap>> loadBitmapsFunc) {
            // filePathを一意なキーとして利用できるようにフルパス化
            filePath = Path.GetFullPath(filePath);

            IList<MutableLazyFlagWrap<Bitmap>> list = null;
            Func<int, Bitmap> lazyFunc = index => {
                // ノードが未登録ならば登録する
                if (GetNode(filePath) == null) {
                    AddNode(filePath, list);
                }

                // リストが評価されて初めて Bitmap オブジェクトの分割を行う
                var newList = loadBitmapsFunc(filePath);
                // リストの長さが一致するかチェック
                if (newList.Count != list.Count) {
                    throw new InvalidOperationException(
                            "新しく生成したBitmapリストは既存のBitmapリストと長さが一致しません。");
                }
                // 分割されて生成されたオブジェクトを MutableLazyFlagWrap の値として設定する
                newList.ForEach((bmp, i) => list[i].Set(bmp));
                return list[index];
            };

            // 既にノードが存在するかチェック
            var node = GetNode(filePath);
            if (node != null) {
                list = node.Value.Resource;
                // lazyFunc を評価してリストの長さが一致するかチェックすることはできない
                list.ForEach(
                        (wrap, i) => wrap.Evaluator = lazyFunc.GetCurrying(i));
                return list;
            }

            // 過去に生成したことがあるかチェック
            if (_disposedDict.TryGetValue(filePath, out list)) {
                // lazyFunc を評価してリストの長さが一致するかチェックすることはできない
                list.ForEach(
                        (wrap, i) => wrap.Evaluator = lazyFunc.GetCurrying(i));
                _disposedDict.Remove(filePath);
            } else {
                list = loadBitmapsFunc(filePath).SelectToArray(
                        (bmp, i) =>
                        new MutableLazyFlagWrap<Bitmap>(
                                bmp, lazyFunc.GetCurrying(i), true));
            }
            AddNode(filePath, list);
            return list;
        }

        private IList<Surface> GetSurfaces(
                string filePath, Func<string, IList<Bitmap>> loadBitmapsFunc) {
            Contract.Requires(filePath != null);
            Contract.Requires(loadBitmapsFunc != null);

            Func<IList<Surface>> func =
                    () =>
                    GetRowList(filePath, loadBitmapsFunc).SelectToArray(
                            wrap => wrap.ToSurface());
            return func.ToLazyWrap().ToReadonlyListWrap();
        }

        #region GetSurface

        public Surface GetSurface(string filePath) {
            return GetSurface(filePath, DefaultLoadBitmapFunc);
        }

        public Surface GetSurface(
                string filePath, Func<string, Bitmap> loadBitmapFunc) {
            return GetRowWrap(filePath, loadBitmapFunc).ToSurface();
        }

        #endregion

        #region GetSurfaces

        public IList<Surface> GetSurfaces(
                string filePath, int width, int height) {
            return GetSurfaces(filePath, width, height, DefaultSplitBitmapFunc);
        }

        public IList<Surface> GetSurfaces(
                string filePath, int width, int height,
                Func<string, Bitmap> loadBitmapFunc) {
            return GetSurfaces(
                    filePath, width, height, loadBitmapFunc,
                    DefaultSplitBitmapFunc);
        }

        public IList<Surface> GetSurfaces(
                string filePath, int width, int height,
                Func<Bitmap, int, int, IList<Bitmap>> splitBitmapFunc) {
            return GetSurfaces(
                    filePath, width, height, DefaultLoadBitmapFunc,
                    splitBitmapFunc);
        }

        public IList<Surface> GetSurfaces(
                string filePath, int width, int height,
                Func<string, Bitmap> loadBitmapFunc,
                Func<Bitmap, int, int, IList<Bitmap>> splitBitmapFunc) {
            return GetSurfaces(
                    filePath,
                    path =>
                    splitBitmapFunc(
                            LoadBitmap(path, loadBitmapFunc), width, height));
        }

        public IList<Surface> GetSurfaces(string filePath, Size size) {
            return GetSurfaces(filePath, size.Width, size.Height);
        }

        public IList<Surface> GetSurfaces(
                string filePath, Size size, Func<string, Bitmap> loadBitmapFunc) {
            return GetSurfaces(
                    filePath, size.Width, size.Height, loadBitmapFunc);
        }

        public IList<Surface> GetSurfaces(
                string filePath, Size size,
                Func<Bitmap, int, int, IList<Bitmap>> splitBitmapFunc) {
            return GetSurfaces(
                    filePath, size.Width, size.Height, splitBitmapFunc);
        }

        public IList<Surface> GetSurfaces(
                string filePath, Size size, Func<string, Bitmap> loadBitmapFunc,
                Func<Bitmap, int, int, IList<Bitmap>> splitBitmapFunc) {
            return GetSurfaces(
                    filePath, size.Width, size.Height, loadBitmapFunc,
                    splitBitmapFunc);
        }

        #endregion
            }
}