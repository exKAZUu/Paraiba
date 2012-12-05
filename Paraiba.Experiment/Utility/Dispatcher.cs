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

namespace Paraiba.Utility {
	public class ActionDispatcher<T1, T2> {
		private readonly List<Delegate> _actions = new List<Delegate>();
		private readonly Action<T1, T2> _defaultAction;

		public ActionDispatcher()
				: this(
						(_1, _2) => {
							throw new ArgumentException(
									"与えられた引数に対応するデリゲートがありません.");
						}) {}

		public ActionDispatcher(Action<T1, T2> defaultAction) {
			_defaultAction = defaultAction;
		}

		public void Add<TSub1, TSub2>(Action<TSub1, TSub2> action)
				where TSub1 : T1
				where TSub2 : T2 {
			_actions.Add(action);
		}

		public void Clear() {
			_actions.Clear();
		}

		public void Remove<TSub1, TSub2>(Action<TSub1, TSub2> action)
				where TSub1 : T1
				where TSub2 : T2 {
			_actions.Remove(action);
		}

		public void Invoke<TSub1, TSub2>(TSub1 value1, TSub2 value2)
				where TSub1 : T1
				where TSub2 : T2 {
			foreach (var action in _actions) {
				var paramList = action.Method.GetParameters();
				if (paramList[0].ParameterType.IsAssignableFrom(typeof(TSub1)) &&
						paramList[1].ParameterType.IsAssignableFrom(typeof(TSub2))) {
					action.DynamicInvoke(value1, value2);
					return;
				}
			}
			_defaultAction(value1, value2);
		}
	}

	public class FuncDispatcher<T1, T2, TResult> {
		private readonly List<Delegate> _actions = new List<Delegate>();
		private readonly Func<T1, T2, TResult> _defaultAction;

		public FuncDispatcher()
				: this(
						(_1, _2) => {
							throw new ArgumentException(
									"与えられた引数に対応するデリゲートがありません.");
						}) {}

		public FuncDispatcher(Func<T1, T2, TResult> defaultAction) {
			_defaultAction = defaultAction;
		}

		public void Add<TSub1, TSub2>(Func<TSub1, TSub2, TResult> action)
				where TSub1 : T1
				where TSub2 : T2 {
			_actions.Add(action);
		}

		public void Clear() {
			_actions.Clear();
		}

		public void Remove<TSub1, TSub2>(Func<TSub1, TSub2, TResult> action)
				where TSub1 : T1
				where TSub2 : T2 {
			_actions.Remove(action);
		}

		public TResult Invoke<TSub1, TSub2>(TSub1 value1, TSub2 value2)
				where TSub1 : T1
				where TSub2 : T2 {
			foreach (var action in _actions) {
				var paramList = action.Method.GetParameters();
				if (paramList[0].ParameterType.IsAssignableFrom(typeof(TSub1)) &&
						paramList[1].ParameterType.IsAssignableFrom(typeof(TSub2))) {
					return (TResult)action.DynamicInvoke(value1, value2);
				}
			}
			return _defaultAction(value1, value2);
		}
	}
}