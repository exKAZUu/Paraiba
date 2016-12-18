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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Paraiba.IO {
    /// <summary>
    ///   Java �� Scanner ��^���č쐬�����N���X�ł��B �������AJava�łƂ͈قȂ�A���K�\���ɑΉ����Ă��܂���B
    /// </summary>
    public class Scanner {
        // --------------------------�����o�ϐ�--------------------------
        private readonly StringBuilder _buffer;
        private readonly TextReader _reader;
        private Object _cache;

        // ------------------------�R���X�g���N�^------------------------
        public Scanner(TextReader reader)
            : this(reader, Char.IsWhiteSpace, isMyPunctuation) {}

        public Scanner(
            TextReader reader, ICollection<char> whiteSpaces,
            ICollection<char> delimiters)
            : this(reader, whiteSpaces.Contains, delimiters.Contains) {}

        public Scanner(
            TextReader reader, IEnumerable<char> whiteSpaces,
            IEnumerable<char> delimiters)
            : this(reader, whiteSpaces.Contains, delimiters.Contains) {}

        public Scanner(
            TextReader reader, Func<char, bool> isWhiteSpace,
            Func<char, bool> isDelimiter) {
            _reader = reader;
            IsWhiteSpaceDelegate = isWhiteSpace;
            IsDelimiterDelegate = isDelimiter;
            _buffer = new StringBuilder();
        }

        // --------------------------�v���p�e�B--------------------------
        public String LastToken { get; private set; }

        public Int32 LastDelimiter { get; private set; }

        public Func<char, bool> IsWhiteSpaceDelegate { get; set; }

        public Func<char, bool> IsDelimiterDelegate { get; set; }

        // ---------------------------���\�b�h---------------------------
        private static bool isMyPunctuation(Char c) {
            return (c != '-' && Char.IsPunctuation(c));
        }

        private bool ReadNextToken() {
            Int32 c;
            // 1�����ڂ�EOF�̏ꍇ�A���͊����Ƃ���
            if ((c = _reader.Read()) < 0) {
                return false;
            }

            // �o�b�t�@���N���A����
            _buffer.Length = 0;
            // �󔒋�؂蕶����ǂݔ�΂�
            while (IsWhiteSpaceDelegate((Char)c) && (c = _reader.Read()) >= 0) {}

            Char ch;
            // ��؂蕶���������́AEOF���󂯎��܂Ń��[�v
            while (c >= 0 && !IsDelimiterDelegate(ch = (Char)c)) {
                if (IsWhiteSpaceDelegate(ch)) {
                    // �󔒋�؂蕶����ǂݔ�΂�
                    while ((c = _reader.Peek()) >= 0 && IsWhiteSpaceDelegate((Char)c)) {
                        _reader.Read();
                    }
                    // ��؂蕶���͂P�������ǂݔ�΂�
                    if (IsDelimiterDelegate((Char)c)) {
                        _reader.Read();
                    } else {
                        c = ch; // ��؂蕶�����Ȃ��ꍇ�͍ŏ��̋󔒕�������؂蕶���Ƃ���
                    }
                    break;
                }
                // �o�b�t�@�ɗ��߂�
                _buffer.Append(ch);
                // ���̕����̓ǂݍ���
                c = _reader.Read();
            }
            LastToken = _buffer.ToString();
            LastDelimiter = c;
            return true;
        }

        public bool HasNext() {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            _cache = null;
            return true;
        }

        public bool HasNextBoolean() {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            Boolean value;
            if (!Boolean.TryParse(LastToken, out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public bool HasNextChar() {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            Char value;
            if (!Char.TryParse(LastToken, out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public bool HasNextByte(NumberStyles style = NumberStyles.Integer) {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            Byte value;
            if (
                !Byte.TryParse(
                    LastToken, style, NumberFormatInfo.CurrentInfo,
                    out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public bool HasNextUInt16(NumberStyles style = NumberStyles.Integer) {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            UInt16 value;
            if (
                !UInt16.TryParse(
                    LastToken, style, NumberFormatInfo.CurrentInfo,
                    out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public bool HasNextUInt32(NumberStyles style = NumberStyles.Integer) {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            UInt32 value;
            if (
                !UInt32.TryParse(
                    LastToken, style, NumberFormatInfo.CurrentInfo,
                    out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public bool HasNextUInt64(NumberStyles style = NumberStyles.Integer) {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            UInt64 value;
            if (
                !UInt64.TryParse(
                    LastToken, style, NumberFormatInfo.CurrentInfo,
                    out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public bool HasNextSByte(NumberStyles style = NumberStyles.Integer) {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            SByte value;
            if (
                !SByte.TryParse(
                    LastToken, style, NumberFormatInfo.CurrentInfo,
                    out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public bool HasNextInt16(NumberStyles style = NumberStyles.Integer) {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            Int16 value;
            if (
                !Int16.TryParse(
                    LastToken, style, NumberFormatInfo.CurrentInfo,
                    out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public bool HasNextInt32(NumberStyles style = NumberStyles.Integer) {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            Int32 value;
            if (
                !Int32.TryParse(
                    LastToken, style, NumberFormatInfo.CurrentInfo,
                    out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public bool HasNextInt64(NumberStyles style = NumberStyles.Integer) {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            Int64 value;
            if (
                !Int64.TryParse(
                    LastToken, style, NumberFormatInfo.CurrentInfo,
                    out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public bool HasNextSingle(
            NumberStyles style =
                    NumberStyles.Float | NumberStyles.AllowThousands) {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            Single value;
            if (
                !Single.TryParse(
                    LastToken, style, NumberFormatInfo.CurrentInfo,
                    out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public bool HasNextDouble(
            NumberStyles style =
                    NumberStyles.Float | NumberStyles.AllowThousands) {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            Double value;
            if (
                !Double.TryParse(
                    LastToken, style, NumberFormatInfo.CurrentInfo,
                    out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public bool HasNextDecimal(NumberStyles style = NumberStyles.Number) {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            Decimal value;
            if (
                !Decimal.TryParse(
                    LastToken, style, NumberFormatInfo.CurrentInfo,
                    out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public bool HasNextBigInteger(NumberStyles style = NumberStyles.Integer) {
            // ���̃g�[�N���̓ǂݍ���
            if (LastToken == null && !ReadNextToken()) {
                return false;
            }
            // �ϊ�
            BigInteger value;
            if (!BigInteger.TryParse(LastToken, out value)) {
                return false;
            }
            _cache = value;
            return true;
        }

        public String Current() {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            }
            return LastToken;
        }

        public Boolean CurrentBoolean() {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Boolean) {
                return (Boolean)_cache;
            }

            var value = Boolean.Parse(LastToken);
            _cache = value;
            return value;
        }

        public Char CurrentChar() {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Char) {
                return (Char)_cache;
            }

            var value = Char.Parse(LastToken);
            _cache = value;
            return value;
        }

        public Byte CurrentByte(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Byte) {
                return (Byte)_cache;
            }
            var value = Byte.Parse(LastToken, style);
            _cache = value;
            return value;
        }

        public UInt16 CurrentUInt16(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is UInt16) {
                return (UInt16)_cache;
            }

            var value = UInt16.Parse(LastToken, style);
            _cache = value;
            return value;
        }

        public UInt32 CurrentUInt32(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is UInt32) {
                return (UInt32)_cache;
            }

            var value = UInt32.Parse(LastToken, style);
            _cache = value;
            return value;
        }

        public UInt64 CurrentUInt64(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is UInt64) {
                return (UInt64)_cache;
            }

            var value = UInt64.Parse(LastToken, style);
            _cache = value;
            return value;
        }

        public SByte CurrentSByte(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is SByte) {
                return (SByte)_cache;
            }

            var value = SByte.Parse(LastToken, style);
            _cache = value;
            return value;
        }

        public Int16 CurrentInt16(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Int16) {
                return (Int16)_cache;
            }

            var value = Int16.Parse(LastToken, style);
            _cache = value;
            return value;
        }

        public Int32 CurrentInt32(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Int32) {
                return (Int32)_cache;
            }

            var value = Int32.Parse(LastToken, style);
            _cache = value;
            return value;
        }

        public Int64 CurrentInt64(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Int64) {
                return (Int64)_cache;
            }

            var value = Int64.Parse(LastToken, style);
            _cache = value;
            return value;
        }

        public Single CurrentSingle(
            NumberStyles style =
                    NumberStyles.Float | NumberStyles.AllowThousands) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Single) {
                return (Single)_cache;
            }

            var value = Single.Parse(LastToken, style);
            _cache = value;
            return value;
        }

        public Double CurrentDouble(
            NumberStyles style =
                    NumberStyles.Float | NumberStyles.AllowThousands) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Double) {
                return (Double)_cache;
            }

            var value = Double.Parse(LastToken, style);
            _cache = value;
            return value;
        }

        public Decimal CurrentDecimal(NumberStyles style = NumberStyles.Number) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Decimal) {
                return (Decimal)_cache;
            }

            var value = Decimal.Parse(LastToken, style);
            _cache = value;
            return value;
        }

        public BigInteger CurrentBigInteger(
            NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is BigInteger) {
                return (BigInteger)_cache;
            }

            var value = BigInteger.Parse(LastToken);
            _cache = value;
            return value;
        }

        public String Next() {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            }
            var token = LastToken;
            LastToken = null;
            return token;
        }

        public String NextLine() {
            var token = LastToken;

            // �L���b�V���̃`�F�b�N
            if (token == null) {
                return _reader.ReadLine();
            }

            LastToken = null;

            if (LastDelimiter == '\r') {
                // �R���X�g���N�^�̈������ȗ������ꍇ�A\n �͓ǂݔ�΂���Ă���͂�
                if (_reader.Peek() == '\n') {
                    _reader.Read();
                }
                return token;
            }

            if (LastDelimiter == '\n') {
                return token;
            }

            return token + _reader.ReadLine();
        }

        public Boolean NextBoolean() {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Boolean) {
                LastToken = null;
                return (Boolean)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return Boolean.Parse(token);
        }

        public Char NextChar() {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Char) {
                LastToken = null;
                return (Char)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return Char.Parse(token);
        }

        public Byte NextByte(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Byte) {
                LastToken = null;
                return (Byte)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return Byte.Parse(token, style);
        }

        public UInt16 NextUInt16(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is UInt16) {
                LastToken = null;
                return (UInt16)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return UInt16.Parse(token, style);
        }

        public UInt32 NextUInt32(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is UInt32) {
                LastToken = null;
                return (UInt32)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return UInt32.Parse(token, style);
        }

        public UInt64 NextUInt64(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is UInt64) {
                LastToken = null;
                return (UInt64)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return UInt64.Parse(token, style);
        }

        public SByte NextSByte(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is SByte) {
                LastToken = null;
                return (SByte)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return SByte.Parse(token, style);
        }

        public Int16 NextInt16(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Int16) {
                LastToken = null;
                return (Int16)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return Int16.Parse(token, style);
        }

        public Int32 NextInt32(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Int32) {
                LastToken = null;
                return (Int32)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return Int32.Parse(token, style);
        }

        public Int64 NextInt64(NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Int64) {
                LastToken = null;
                return (Int64)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return Int64.Parse(token, style);
        }

        public Single NextSingle(
            NumberStyles style =
                    NumberStyles.Float | NumberStyles.AllowThousands) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Single) {
                LastToken = null;
                return (Single)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return Single.Parse(token, style);
        }

        public Double NextDouble(
            NumberStyles style =
                    NumberStyles.Float | NumberStyles.AllowThousands) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Double) {
                LastToken = null;
                return (Double)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return Double.Parse(token, style);
        }

        public Decimal NextDecimal(NumberStyles style = NumberStyles.Number) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is Decimal) {
                LastToken = null;
                return (Decimal)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return Decimal.Parse(token, style);
        }

        public BigInteger NextBigInteger(
            NumberStyles style = NumberStyles.Integer) {
            // �L���b�V���̃`�F�b�N
            if (LastToken == null) {
                if (!ReadNextToken()) {
                    throw new InvalidOperationException();
                }
            } else if (_cache is BigInteger) {
                LastToken = null;
                return (BigInteger)_cache;
            }

            var token = LastToken;
            LastToken = null;

            return BigInteger.Parse(token);
        }
    }
}