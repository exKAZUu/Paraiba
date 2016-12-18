#region License

// Copyright (C) 2011-2016 Kazunori Sakamoto
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

using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Wrap;

namespace Paraiba.Drawing.Surfaces {
    /// <summary>
    ///   Bitmap インスタンスを利用して表示可能な部分画像サーフェイスを表します。 ただし、部分画像の描画は10倍程度の時間効率の低下を招きます。
    /// </summary>
    public class BitmapPartSurface : ImagePartSurface<Bitmap> {
        public BitmapPartSurface(Wrap<Bitmap> image, Rectangle srcRect)
            : base(image, srcRect) {}
    }

    /// <summary>
    ///   Image インスタンスを利用して表示可能な部分画像サーフェイスを表します。 ただし、部分画像の描画は10倍程度の時間効率の低下を招きます。
    /// </summary>
    public class ImagePartSurface : ImagePartSurface<Image> {
        public ImagePartSurface(Wrap<Image> image, Rectangle srcRect)
            : base(image, srcRect) {}
    }

    /// <summary>
    ///   TImage インスタンスを利用して表示可能な部分画像サーフェイスを表します。 ただし、部分画像の描画は10倍程度の時間効率の低下を招きます。
    /// </summary>
    public class ImagePartSurface<TImage> : Surface
            where TImage : Image {
        public ImagePartSurface(Wrap<TImage> image, Rectangle srcRect) {
            Contract.Requires(image != null);

            Image = image;
            SrcRect = srcRect;
        }

        public Wrap<TImage> Image { get; }

        public Rectangle SrcRect { get; }

        public override Size Size {
            get { return SrcRect.Size; }
        }

        public override void Draw(Graphics g, int x, int y) {
            g.DrawImage(
                Image, new Rectangle(x, y, SrcRect.Width, SrcRect.Height),
                SrcRect, GraphicsUnit.Pixel);
        }
    }
}