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

namespace Paraiba.TaskList {
	public class TaskNode<TTask> {
		internal TaskNode<TTask> _next;
		internal TaskNode<TTask> _prev;

		/// <summary>
		///   �D��x
		/// </summary>
		internal double _priority;

		/// <summary>
		///   �^�X�N
		/// </summary>
		internal TTask _task;

		public TaskNode() {
			_next = this;
			_prev = this;
			_priority = double.NaN;
		}

		public TaskNode(double priority, TTask task) {
			_task = task;
			_priority = priority;
		}
	}

	public abstract class BasicTaskList<TTask>
			where TTask : class {
		private int _count;
		protected TaskNode<TTask> _head;

		protected BasicTaskList() {
			_head = new TaskNode<TTask>();
		}

		/// <summary>
		///   ���X�g���̃^�X�N�����擾���܂��B
		/// </summary>
		public int Count {
			get { return _count; }
		}

		/// <summary>
		///   �w�肵���D��x�Ŏw�肵���^�X�N��ǉ����܂��B
		/// </summary>
		/// <param name="priority"> �^�X�N�̗D��x </param>
		/// <param name="task"> �ǉ�����^�X�N </param>
		public void Add(double priority, TTask task) {
			var node = _head._next;

			while (node._priority <= priority) {
				node = node._next;
			}
			Insert(node._prev, new TaskNode<TTask>(priority, task));
		}

		private void Insert(TaskNode<TTask> prev, TaskNode<TTask> newNext) {
			newNext._next = prev._next;
			newNext._prev = prev;
			prev._next._prev = newNext;
			prev._next = newNext;
			_count++;
		}

		/// <summary>
		///   �w�肵���D��x�̃^�X�N���폜���܂��B
		/// </summary>
		/// <param name="priority"> �폜����^�X�N�̗D��x </param>
		/// <returns> �폜�ɐ����������ǂ��� </returns>
		public bool Remove(double priority) {
			var node = _head;

			while (node._priority < priority) {
				node = node._next;
			}
			if (node._priority == priority) {
				return Remove(node);
			}
			return false;
		}

		/// <summary>
		///   �w�肵���^�X�N���폜���܂��B
		/// </summary>
		/// <param name="task"> �폜����^�X�N </param>
		/// <returns> �폜�ɐ����������ǂ��� </returns>
		public bool Remove(TTask task) {
			var node = _head._next;
			// �ԕ�
			_head._task = task;

			while (node._task.Equals(task) == false) {
				node = node._next;
			}
			if (node != _head) {
				return Remove(node);
			}
			return false;
		}

		/// <summary>
		///   �w�肵���D��x�̎w�肵���^�X�N���폜���܂��B
		/// </summary>
		/// <param name="priority"> �폜����^�X�N�̗D��x </param>
		/// <param name="task"> �폜����^�X�N </param>
		/// <returns> �폜�ɐ����������ǂ��� </returns>
		public bool Remove(double priority, TTask task) {
			var node = _head._next;
			// �ԕ�
			_head._task = task;

			while (node._priority < priority || node._task.Equals(task) == false) {
				node = node._next;
			}
			if (node._priority == priority && node != _head) {
				return Remove(node);
			}
			return false;
		}

		private bool Remove(TaskNode<TTask> node) {
			node._prev._next = node._next;
			node._next._prev = node._prev;
			_count--;
			return true;
		}
	}

	// ------------------------------- ����0�� -------------------------------

	public struct TaskArgs {
		public TaskList List;
		public TaskNode<TaskList.Task> ThisTask;

		public TaskArgs(TaskList list, TaskNode<TaskList.Task> thisTask) {
			List = list;
			ThisTask = thisTask;
		}
	}

	/// <summary>
	///   TaskList �ɒǉ��\�ȃ^�X�N��\������C���^�t�F�[�X
	/// </summary>
	public interface ITask {
		void Task(TaskArgs taskArgs);
	}

	public class TaskList : BasicTaskList<TaskList.Task> {
		#region Delegates

		public delegate void Task(TaskArgs taskArgs);

		#endregion

		public void Add(double priority, ITask task) {
			Add(priority, task.Task);
		}

		public void Evoke() {
			var node = _head._next;
			while (node != _head) {
				node._task(new TaskArgs(this, node));
				node = node._next;
			}
		}
	}

	// ------------------------------- ����1�� -------------------------------

	public struct TaskArgs<TArg1> {
		public TaskList<TArg1> List;
		public TaskNode<TaskList<TArg1>.Task> ThisTask;

		public TaskArgs(
				TaskList<TArg1> list, TaskNode<TaskList<TArg1>.Task> thisTask) {
			List = list;
			ThisTask = thisTask;
		}
	}

	/// <summary>
	///   TaskList �ɒǉ��\�ȃ^�X�N��\������C���^�t�F�[�X
	/// </summary>
	/// <typeparam name="TArg1"> </typeparam>
	public interface ITask<TArg1> {
		void Task(TaskArgs<TArg1> taskArgs, TArg1 arg1);
	}

	public class TaskList<TArg1> : BasicTaskList<TaskList<TArg1>.Task> {
		#region Delegates

		public delegate void Task(TaskArgs<TArg1> taskArgs, TArg1 arg1);

		#endregion

		public void Add(double priority, ITask<TArg1> task) {
			Add(priority, task.Task);
		}

		public void Evoke(TArg1 arg1) {
			var node = _head._next;
			while (node != _head) {
				node._task(new TaskArgs<TArg1>(this, node), arg1);
				node = node._next;
			}
		}
	}

	// ------------------------------- ����2�� -------------------------------

	public struct TaskArgs<TArg1, TArg2> {
		public TaskList<TArg1, TArg2> List;
		public TaskNode<TaskList<TArg1, TArg2>.Task> ThisTask;

		public TaskArgs(
				TaskList<TArg1, TArg2> list,
				TaskNode<TaskList<TArg1, TArg2>.Task> thisTask) {
			List = list;
			ThisTask = thisTask;
		}
	}

	/// <summary>
	///   TaskList �ɒǉ��\�ȃ^�X�N��\������C���^�t�F�[�X
	/// </summary>
	/// <typeparam name="TArg1"> </typeparam>
	/// <typeparam name="TArg2"> </typeparam>
	public interface ITask<TArg1, TArg2> {
		void Task(TaskArgs<TArg1, TArg2> taskArgs, TArg1 arg1, TArg2 arg2);
	}

	public class TaskList<TArg1, TArg2>
			: BasicTaskList<TaskList<TArg1, TArg2>.Task> {
		#region Delegates

		public delegate void Task(
				TaskArgs<TArg1, TArg2> taskArgs, TArg1 arg1, TArg2 arg2);

		#endregion

		public void Add(double priority, ITask<TArg1, TArg2> task) {
			Add(priority, task.Task);
		}

		public void Evoke(TArg1 arg1, TArg2 arg2) {
			var node = _head._next;
			while (node != _head) {
				node._task(new TaskArgs<TArg1, TArg2>(this, node), arg1, arg2);
				node = node._next;
			}
		}
	}

	// ------------------------------- ����3�� -------------------------------

	public struct TaskArgs<TArg1, TArg2, TArg3> {
		public TaskList<TArg1, TArg2, TArg3> List;
		public TaskNode<TaskList<TArg1, TArg2, TArg3>.Task> ThisTask;

		public TaskArgs(
				TaskList<TArg1, TArg2, TArg3> list,
				TaskNode<TaskList<TArg1, TArg2, TArg3>.Task> thisTask) {
			List = list;
			ThisTask = thisTask;
		}
	}

	/// <summary>
	///   TaskList �ɒǉ��\�ȃ^�X�N��\������C���^�t�F�[�X
	/// </summary>
	/// <typeparam name="TArg1"> </typeparam>
	/// <typeparam name="TArg2"> </typeparam>
	/// <typeparam name="TArg3"> </typeparam>
	public interface ITask<TArg1, TArg2, TArg3> {
		void Task(
				TaskArgs<TArg1, TArg2, TArg3> taskArgs, TArg1 arg1, TArg2 arg2,
				TArg3 arg3);
	}

	public class TaskList<TArg1, TArg2, TArg3>
			: BasicTaskList<TaskList<TArg1, TArg2, TArg3>.Task> {
		#region Delegates

		public delegate void Task(
				TaskArgs<TArg1, TArg2, TArg3> taskArgs, TArg1 arg1, TArg2 arg2,
				TArg3 arg3);

		#endregion

		public void Add(double priority, ITask<TArg1, TArg2, TArg3> task) {
			Add(priority, task.Task);
		}

		public void Evoke(TArg1 arg1, TArg2 arg2, TArg3 arg3) {
			var node = _head._next;
			while (node != _head) {
				node._task(
						new TaskArgs<TArg1, TArg2, TArg3>(this, node), arg1,
						arg2, arg3);
				node = node._next;
			}
		}
	}
}