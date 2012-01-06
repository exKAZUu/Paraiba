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

using System.Drawing;
using Paraiba.Wrap;

namespace Paraiba.Drawing.Surfaces {
    /// <summary>
    ///   Bitmap インスタンスを利用して表示可能な画像サーフェイスを表します。
    /// </summary>
    public class BitmapSurface : ImageSurface<Bitmap> {
        public BitmapSurface(Wrap<Bitmap> image)
                : base(image) {}
    }

    /// <summary>
    ///   Image インスタンスを利用して表示可能な画像サーフェイスを表します。
    /// </summary>
    public class ImageSurface : ImageSurface<Image> {
        public ImageSurface(Wrap<Image> image)
                : base(image) {}
    }

    /// <summary>
    ///   TImage インスタンスを利用して表示可能な画像サーフェイスを表します。
    /// </summary>
    public class ImageSurface<TImage> : Surface
            where TImage : Image {
        private readonly Wrap<TImage> _image;

        public ImageSurface(Wrap<TImage> image) {
            _image = image;
        }

        public Wrap<TImage> Image {
            get { return _image; }
        }

        /// <summary>
        ///   オブジェクトの縦横のサイズを取得します。
        /// </summary>
        public override Size Size {
            get { return _image.Value.Size; }
        }

        /// <summary>
        ///   オブジェクトの左上原点が指定した位置に来るように描画します。
        /// </summary>
        /// <param name="g"> 描画で利用する Graphics </param>
        /// <param name="x"> 描画位置のX座標 </param>
        /// <param name="y"> 描画位置のY座標 </param>
        public override void Draw(Graphics g, int x, int y) {
            var image = _image.Value;
            g.DrawImage(image, x, y, image.Width, image.Height);
        }

        public override Image GetImage() {
            return _image;
        }

        // TODO: ISurfaceで宣言
        public void PreLoad() {
            var tmp = _image.Value;
        }
            }
}