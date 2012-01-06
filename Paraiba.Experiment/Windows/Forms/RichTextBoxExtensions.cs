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

using System.Windows.Forms;

namespace Paraiba.Windows.Forms {
    public static class RichTextBoxExtensions {
        public static void Select(
                this RichTextBox richTextBox, int startLine, int endLine,
                int startPos, int endPos) {
            var text = richTextBox.Text;

            var startLineHeadPos = 0;
            var iLines = 1;
            for (; iLines < startLine; iLines++) {
                startLineHeadPos = text.IndexOf('\n', startLineHeadPos) + 1;
            }

            var endLineHeadPos = startLineHeadPos;
            for (; iLines < endLine; iLines++) {
                endLineHeadPos = text.IndexOf('\n', endLineHeadPos) + 1;
            }

            var start = startLineHeadPos + startPos;
            var end = endLineHeadPos + endPos;

            richTextBox.Select(start, end - start);
        }
    }
}