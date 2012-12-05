#region License

// Copyright (C) 2008-2012 Kazunori Sakamoto
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

#region OriginalLicense

/*
  Copyright (c) 2000-2012  Shiro Kawai  <shiro@acm.org>

  Redistribution and use in source and binary forms, with or without
  modification, are permitted provided that the following conditions
  are met:

   1. Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.

   2. Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.

   3. Neither the name of the authors nor the names of its contributors
      may be used to endorse or promote products derived from this
      software without specific prior written permission.

  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
  OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
  SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
  TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

#endregion

using System.Text;

namespace Paraiba.Text.Gauche {
    public static partial class GaucheGuess {
        public static readonly Encoding EucJp =
                Encoding.GetEncoding("EUC-JP");

        public static readonly Encoding ShiftJis =
                Encoding.GetEncoding("Shift_JIS");

        public static readonly Encoding Iso2022Jp =
                Encoding.GetEncoding("iso-2022-jp");

        public static readonly Encoding Utf8 = Encoding.UTF8;

        private static GuessDfa InitializeDfa(sbyte[,] states, GuessArc[] arcs) {
            return new GuessDfa {
                    States = states,
                    Arcs = arcs,
                    State = 0,
                    Score = 1.0,
            };
        }

        private static void CalculateNextDfa(GuessDfa dfa, int ch) {
            if (dfa.State < 0) {
                return;
            }
            var arc = dfa.States[dfa.State, ch];
            if (arc < 0) {
                dfa.State = -1;
            } else {
                dfa.State = dfa.Arcs[arc].Next;
                dfa.Score *= dfa.Arcs[arc].Score;
            }
        }

        private static bool IsAliveDfa(GuessDfa dfa) {
            return dfa.State >= 0;
        }

        public static Encoding GuessEncodings(byte[] bytes) {
            int i;
            var eucj = InitializeDfa(GuessEucjSt, GuessEucjAr);
            var sjis = InitializeDfa(GuessSjisSt, GuessSjisAr);
            var utf8 = InitializeDfa(GuessUtf8St, GuessUtf8Ar);
            GuessDfa top = null;

            for (i = 0; i < bytes.Length; i++) {
                int c = bytes[i];

                /* special treatment of jis escape sequence */
                if (c == 0x1b) {
                    if (i < bytes.Length - 1) {
                        c = bytes[++i];
                        if (c == '$' || c == '(') {
                            return Iso2022Jp;
                        }
                    }
                }

                if (IsAliveDfa(eucj)) {
                    if (!IsAliveDfa(sjis) && !IsAliveDfa(utf8)) {
                        return EucJp;
                    }
                    CalculateNextDfa(eucj, c);
                }
                if (IsAliveDfa(sjis)) {
                    if (!IsAliveDfa(eucj) && !IsAliveDfa(utf8)) {
                        return ShiftJis;
                    }
                    CalculateNextDfa(sjis, c);
                }
                if (IsAliveDfa(utf8)) {
                    if (!IsAliveDfa(sjis) && !IsAliveDfa(eucj)) {
                        return Utf8;
                    }
                    CalculateNextDfa(utf8, c);
                }

                if (!IsAliveDfa(eucj) && !IsAliveDfa(sjis) && !IsAliveDfa(utf8)) {
                    /* we ran out the possibilities */
                    return null;
                }
            }

            /* Now, we have ambigous code.  Pick the highest score.
			 * If more than one candidate tie, pick the default encoding.
			 */
            if (IsAliveDfa(eucj)) {
                top = eucj;
            }
            if (IsAliveDfa(utf8)) {
                if (top != null) {
                    if (top.Score <= utf8.Score) {
                        top = utf8;
                    }
                } else {
                    top = utf8;
                }
            }
            if (IsAliveDfa(sjis)) {
                if (top != null) {
                    if (top.Score < sjis.Score) {
                        top = sjis;
                    }
                } else {
                    top = sjis;
                }
            }

            if (top == eucj) {
                return EucJp;
            }
            if (top == utf8) {
                return Utf8;
            }
            if (top == sjis) {
                return ShiftJis;
            }
            return null;
        }
    }
}