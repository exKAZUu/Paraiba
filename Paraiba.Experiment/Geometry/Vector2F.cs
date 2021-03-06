﻿#region License

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

using System;
using System.Drawing;

namespace Paraiba.Geometry {
    public struct Vector2F {
        #region Copy & Paste from Vector2F

        public Vector2F(float x, float y) {
            X = x;
            Y = y;
        }

        public Vector2F(Point2F p) {
            X = p.X;
            Y = p.Y;
        }

        public Vector2F(Point2F to, Point2F from) {
            X = to.X - from.X;
            Y = to.Y - from.Y;
        }

        public float X { get; }

        public float Y { get; }

        public double Angle {
            get { return Math.Atan2(Y, X); }
        }

        public double Length {
            get { return Math.Sqrt(X * X + Y * Y); }
        }

        public float LengthSq {
            get { return (X * X + Y * Y); }
        }

        public static Vector2F operator +(Vector2F v1, Vector2F v2) {
            return new Vector2F(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2F operator -(Vector2F v1, Vector2F v2) {
            return new Vector2F(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static PointF operator +(PointF p, Vector2F v) {
            return new PointF(p.X + v.X, p.Y + v.Y);
        }

        public static Point2F operator +(Point2F p, Vector2F v) {
            return new Point2F(p.X + v.X, p.Y + v.Y);
        }

        public static Vector2F operator -(Vector2F v) {
            return new Vector2F(-v.X, -v.Y);
        }

        public static PointF operator -(PointF p, Vector2F v) {
            return new PointF(p.X - v.X, p.Y - v.Y);
        }

        public static Point2F operator -(Point2F p, Vector2F v) {
            return new Point2F(p.X - v.X, p.Y - v.Y);
        }

        public static Vector2F operator *(Vector2F v, float factor) {
            return new Vector2F(v.X * factor, v.Y * factor);
        }

        public static Vector2F operator *(float factor, Vector2F v) {
            return new Vector2F(v.X * factor, v.Y * factor);
        }

        public static Vector2F operator /(Vector2F v, float modulus) {
            return new Vector2F((v.X / modulus), (v.Y / modulus));
        }

        public static bool operator ==(Vector2F v1, Vector2F v2) {
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector2F v1, Vector2F v2) {
            return !v1.Equals(v2);
        }

        public override bool Equals(object obj) {
            if (!(obj is Vector2F)) {
                return false;
            }
            return Equals((Vector2F)obj);
        }

        public bool Equals(Vector2F v) {
            return v.X == X && v.Y == Y;
        }

        public override int GetHashCode() {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public static explicit operator Vector2F(Point2F p) {
            return new Vector2F(p.X, p.Y);
        }

        public static explicit operator Vector2F(PointF p) {
            return new Vector2F(p.X, p.Y);
        }

        public static explicit operator Point2F(Vector2F v) {
            return new Point2F(v.X, v.Y);
        }

        public static explicit operator PointF(Vector2F v) {
            return new PointF(v.X, v.Y);
        }

        public override string ToString() {
            return "{X=" + X + ",Y=" + Y + "}";
        }

        /// <summary>
        ///   外積のZ成分（X, Y成分は0）を求める。
        /// </summary>
        /// <param name="v"> </param>
        /// <returns> </returns>
        public float CrossProduct(Vector2F v) {
            return X * v.Y - Y * v.X;
        }

        /// <summary>
        ///   内積を求める。
        /// </summary>
        /// <param name="v"> </param>
        /// <returns> </returns>
        public float InnerProduct(Vector2F v) {
            return X * v.X + Y * v.Y;
        }

        public Vector2F Add(Vector2F dv) {
            return new Vector2F(X + dv.X, Y + dv.Y);
        }

        public Vector2F Rotate90() {
            return new Vector2F(Y, -X);
        }

        public Vector2F Rotate90(Vector2F pivot) {
            return Add(-pivot)
                    .Rotate90()
                    .Add(pivot);
        }

        public Vector2F Rotate180() {
            return new Vector2F(-X, -Y);
        }

        public Vector2F Rotate180(Vector2F pivot) {
            return Add(-pivot)
                    .Rotate180()
                    .Add(pivot);
        }

        public Vector2F Rotate270() {
            return new Vector2F(-Y, X);
        }

        public Vector2F Rotate270(Vector2F pivot) {
            return Add(-pivot)
                    .Rotate270()
                    .Add(pivot);
        }

        #endregion

        public static implicit operator Vector2F(Vector2 v) {
            return new Vector2F(v.X, v.Y);
        }

        public static explicit operator Vector2(Vector2F v) {
            return new Vector2((int)v.X, (int)v.Y);
        }

        public static explicit operator Vector2F(Point2 p) {
            return new Vector2F(p.X, p.Y);
        }

        public static explicit operator Vector2F(Point p) {
            return new Vector2F(p.X, p.Y);
        }

        public static explicit operator Point2(Vector2F v) {
            return new Point2((int)v.X, (int)v.Y);
        }

        public static explicit operator Point(Vector2F v) {
            return new Point((int)v.X, (int)v.Y);
        }

        public Vector2 ToRoundedVector2() {
            return new Vector2((int)(X + 0.5), (int)(Y + 0.5));
        }

        public bool Equals(Vector2 v) {
            return false;
        }
    }
}