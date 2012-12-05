﻿#region License

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

using System;

namespace Paraiba.Core {
    /// <summary>
    /// A utility class for enhancing <see cref="Environment" />.
    /// </summary>
    public static class EnvironmentUtils {
        /// <summary>
        /// Returns the boolean whether this program runs on Mono or not.
        /// </summary>
        /// <returns>The boolean whether this program runs on Mono or not.</returns>
        public static bool OnMono() {
            int p = (int)Environment.OSVersion.Platform;
            return (p == 4) || (p == 6) || (p == 128);
        }
    }
}