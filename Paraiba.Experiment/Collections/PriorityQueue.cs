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
using System.Diagnostics.Contracts;

namespace Paraiba.Collections {
	/// <summary>
	///   �D��x�t���L���[��\���܂��B
	/// </summary>
	/// <typeparam name="TValue"> </typeparam>
	public class PriorityQueue<T> {
		private readonly List<T> _array;
		private readonly IComparer<T> _cmp;

		public PriorityQueue()
				: this(Comparer<T>.Default) {}

		public PriorityQueue(IComparer<T> cmp) {
			Contract.Requires(cmp != null);

			// �ŏ��̗v�f�͔ԕ��p�Ɋm��
			_array = new List<T> { default(T) };
			_cmp = cmp;
		}

		/// <summary>
		///   <see cref="TValue:System.Collections.Generic.ICollection`1" /> �Ɋi�[����Ă���v�f�̐���擾���܂��B
		/// </summary>
		/// <returns> <see cref="TValue:System.Collections.Generic.ICollection`1" /> �Ɋi�[����Ă���v�f�̐��B </returns>
		public int Count {
			get { return _array.Count - 1; }
		}

		public void Clear() {
			_array.RemoveRange(1, _array.Count - 1);
		}

		public bool Contains(T item) {
			return _array.IndexOf(item, 1) != -1;
		}

		public void CopyTo(T[] array, int arrayIndex) {
			Contract.Requires(0 <= arrayIndex);
			Contract.Requires(arrayIndex + Count <= array.Length);

			var arr = _array;
			int n = arr.Count;
			if (n <= 2) {
				if (n == 2) {
					array[arrayIndex] = arr[1];
				}
				return;
			}

			var cmp = _cmp;
			int endIndex = n + arrayIndex - 1;

			for (int i = 1; i < n; i++) {
				array[endIndex - (i)] = arr[i];
			}

			for (n--; n >= 3; n--) {
				// �D�揇�ʂ̍ł�����v�f��L������
				var result = array[endIndex - (1)];

				// �����̗v�f����Ƃ��čl���ăR�s�[����
				var item = array[endIndex - (n)];
				// downheap ����ɂ�荪�̒l��K�؂Ȉʒu�ֈړ�����
				int i = 1, j = 2,
						k = j
								-
								(cmp.Compare(
										array[endIndex - (j + 1)],
										array[endIndex - (j)]) >> 31);
				T tmp;
				while ((j = k * 2) < n
						&& cmp.Compare(item, (tmp = array[endIndex - (j)])) > 0) {
					array[endIndex - (i)] = array[endIndex - (k)];
					i = k;
					k = j - (cmp.Compare(array[endIndex - (j + 1)], tmp) >> 31);
				}
				array[endIndex - (i)] = array[endIndex - (k)];
				// �K�؂Ȉʒu�ɖ����̗v�f��z�u
				array[endIndex - (k)] = item;

				// �ԕ��Ƃ��Ďc���Ă����������̗v�f�̃R�s�[���ɁA�D�揇�ʂ̍ł�����v�f��ړ�����
				array[endIndex - (n)] = result;
			}
			{
				T item = array[endIndex - (2)];
				array[endIndex - (2)] = array[endIndex - (1)];
				array[endIndex - (1)] = item;
			}
		}

		public T Dequeue() {
			var arr = _array;

			// �v�f���̃`�F�b�N
			int n = arr.Count - 1;
			if (n < 1) {
				throw new InvalidOperationException("PriorityQueue ����ł��B");
			}

			var cmp = _cmp;

			// �D�揇�ʂ̍ł�����v�f��L������
			T result = arr[1];

			if (n <= 2) {
				// �v�f��2�ȉ��̏ꍇ�A�c��̗v�f����Ɉړ����邾��
				arr[1] = arr[n];
			} else {
				// �����̗v�f����Ƃ��čl���ăR�s�[����
				var item = arr[n];
				// downheap ����ɂ�荪�̒l��K�؂Ȉʒu�ֈړ�����
				int i = 1, j = 2,
						k = j - (cmp.Compare(arr[j + 1], arr[j]) >> 31);
				T tmp;
				while ((j = k * 2) < n && cmp.Compare(item, (tmp = arr[j])) > 0) {
					arr[i] = arr[k];
					i = k;
					k = j - (cmp.Compare(arr[j + 1], tmp) >> 31);
				}
				arr[i] = arr[k];
				// �K�؂Ȉʒu�ɖ����̗v�f��z�u
				arr[k] = item;
			}

			// �D�揇�ʂ̍ł�Ⴂ�v�f�̃R�s�[���i�ԕ��j��폜
			arr.RemoveAt(n);

			return result;
		}

		public void Enqueue(T item) {
			var arr = _array;
			var cmp = _cmp;

			// �ԕ�
			arr[0] = item;
			// �Ƃ肠���������̗v�f�Ƃ��Ĕz�u����
			arr.Add(item);

			// upheap ����ɂ�薖���̗v�f��K�؂Ȉʒu�ֈړ�����
			int i = arr.Count - 1, j;
			if (cmp.Compare(item, arr[j = i >> 1]) < 0) {
				do {
					arr[i] = arr[j];
					i = j;
				} while (cmp.Compare(item, arr[j = i >> 1]) < 0);
				// �K�؂Ȉʒu�ɖ����̗v�f��z�u
				arr[i] = item;
			}
		}

		public bool Exists(Predicate<T> match) {
			var arr = _array;
			int length = arr.Count;
			for (int i = 1; i < length; i++) {
				if (match(arr[i])) {
					return true;
				}
			}
			return false;
		}

		public T Peek() {
			// �v�f���̃`�F�b�N
			if (_array.Count < 2) {
				throw new InvalidOperationException("PriorityQueue ����ł��B");
			}
			return _array[1];
		}

		public T[] ToArray() {
			var arr = new T[_array.Count - 1];
			CopyTo(arr, 0);
			return arr;
		}

		public void TrimExcess() {
			_array.TrimExcess();
		}
	}
}