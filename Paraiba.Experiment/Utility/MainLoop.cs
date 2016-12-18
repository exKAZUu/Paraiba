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
using System.Threading;
using System.Windows.Forms;
using Paraiba.TaskList;

namespace Paraiba.Utility {
    public class MainLoop {
        private int _fps;
        private double _mspf;

        public MainLoop()
            : this(60) {}

        public MainLoop(int fps) {
            TickEvents = new TaskList<int>();
            Fps = fps;
        }

        public int Fps {
            get { return _fps; }
            set {
                _fps = value;
                _mspf = 1000.0 / value;
            }
        }

        public double Mspf {
            get { return _mspf; }
            set {
                _mspf = value;
                _fps = (int)((1000.0 / value) + 0.5);
            }
        }

        public int FrameCount { get; private set; }

        public int MaxSkip { get; set; } = 2;

        public TaskList<int> TickEvents { get; }

        public void Do(Func<bool> isContinuousFunc) {
            // アニメーション用のFPS制御
            int lastTime = Environment.TickCount;
            double nextTime = Environment.TickCount;
            while (isContinuousFunc()) {
                int skipCount = MaxSkip;
                int sleepTime;
                do {
                    nextTime += Mspf;

                    Application.DoEvents();
                    FrameCount++;
                    TickEvents.Evoke(Environment.TickCount - lastTime);

                    lastTime = Environment.TickCount;

                    if (--skipCount < 0) {
                        nextTime = lastTime;
                        goto REFRESH;
                    }
                } while ((sleepTime = (int)nextTime - lastTime) < 0);

                Thread.Sleep(sleepTime);

                REFRESH:
                ;
            }
        }
    }
}