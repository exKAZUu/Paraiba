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
using System.Collections;
using System.Collections.Generic;

namespace Paraiba.Collections {
    /// <summary>
    ///   ���X�g��Ŏ��ۂɕێ����Ă���m�[�h�N���X �p�b�P�[�W�O����̓|�C���^�̂悤�Ɏg�p���邱�Ƃ��ł��A ���������ɂ��ėv�f�̍폜��אڃm�[�h�̎擾���\�ł���B
    /// </summary>
    /// <typeparam name="TKey"> </typeparam>
    /// <typeparam name="TValue"> </typeparam>
    public class SortedListNode<TKey, TValue> {
        internal KeyValuePair<TKey, TValue> item_;
        internal SortedListNode<TKey, TValue> next_;
        internal SortedListNode<TKey, TValue> prev_;

        internal SortedListNode() {
            prev_ = this;
            next_ = this;
        }

        public SortedListNode(TKey key, TValue value) {
            item_ = new KeyValuePair<TKey, TValue>(key, value);
        }

        public KeyValuePair<TKey, TValue> Item {
            get { return item_; }
        }
    }

    public class SortedListEnumerator<TKey, TValue>
            : IEnumerator<KeyValuePair<TKey, TValue>> {
        private readonly SortedLinkedList<TKey, TValue> list;
        private SortedListNode<TKey, TValue> current;

        public SortedListEnumerator(
                SortedLinkedList<TKey, TValue> list,
                SortedListNode<TKey, TValue> current) {
            this.list = list;
            this.current = current;
        }

        #region IEnumerator<KeyValuePair<TKey,TValue>> Members

        ///<summary>
        ///  �񋓎q�̌��݈ʒu�ɂ���R���N�V������̗v�f��擾���܂��B
        ///</summary>
        ///<returns> �R���N�V������́A�񋓎q�̌��݈ʒu�ɂ���v�f�B </returns>
        public KeyValuePair<TKey, TValue> Current {
            get { return current.item_; }
        }

        ///<summary>
        ///  �R���N�V������̌��݂̗v�f��擾���܂��B
        ///</summary>
        ///<returns> �R���N�V������̌��݂̗v�f�B </returns>
        ///<exception cref="TValue:System.InvalidOperationException">�񋓎q���A�R���N�V�����̍ŏ��̗v�f�̑O�A�܂��͍Ō�̗v�f�̌�Ɉʒu���Ă��܂��B
        ///  �܂��� 
        ///  �񋓎q���쐬���ꂽ��ɁA�R���N�V�������ύX����܂����B</exception>
        ///<filterpriority>2</filterpriority>
        object IEnumerator.Current {
            get { return current.item_; }
        }

        ///<summary>
        ///  �A���}�l�[�W ���\�[�X�̉������у��Z�b�g�Ɋ֘A�t�����Ă���A�v���P�[�V������`�̃^�X�N����s���܂��B
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose() {}

        ///<summary>
        ///  �񋓎q��R���N�V�����̎��̗v�f�ɐi�߂܂��B
        ///</summary>
        ///<returns> �񋓎q�����̗v�f�ɐ���ɐi�񂾏ꍇ�� true�B�񋓎q���R���N�V�����̖�����z�����ꍇ�� false�B </returns>
        ///<exception cref="TValue:System.InvalidOperationException">�񋓎q���쐬���ꂽ��ɁA�R���N�V�������ύX����܂����B</exception>
        ///<filterpriority>2</filterpriority>
        public bool MoveNext() {
            var nextNode = current.next_;
            if (nextNode != list.head_) {
                current = nextNode;
                return true;
            }
            return false;
        }

        ///<summary>
        ///  �񋓎q������ʒu�A�܂�R���N�V�����̍ŏ��̗v�f�̑O�ɐݒ肵�܂��B
        ///</summary>
        ///<exception cref="TValue:System.InvalidOperationException">�񋓎q���쐬���ꂽ��ɁA�R���N�V�������ύX����܂����B</exception>
        ///<filterpriority>2</filterpriority>
        public void Reset() {
            current = list.head_;
        }

        #endregion

        /// <summary>
        ///   ���X�g�ɗv�f��ǉ�����
        /// </summary>
        /// <param name="key"> </param>
        /// <param name="value"> </param>
        public void Add(TKey key, TValue value) {
            list.Add(key, value);
        }

        /// <summary>
        ///   Current �v���p�e�B�Ŏ������v�f��폜���� ���̑����s���Ă� MoveNext ���\�b�h�ɉe����^���Ȃ� ���Ȃ킿�A�폜���O�̗v�f�Ɉړ�����
        /// </summary>
        /// <returns> �폜�ɐ����������ǂ��� </returns>
        public bool Remove() {
            var node = current;
            if (node != list.head_) {
                current = current.prev_;
                list.Remove(node);
                return true;
            }
            return false;
        }
            }

    public class SortedLinkedList<TKey, TValue>
            : ICollection<KeyValuePair<TKey, TValue>> {
        private readonly IComparer<TKey> _cmp;
        private int _count;
        internal SortedListNode<TKey, TValue> head_;

        public SortedLinkedList()
                : this(Comparer<TKey>.Default) {}

        public SortedLinkedList(IComparer<TKey> cmp) {
            _cmp = cmp;
            _count = 0;
            head_ = new SortedListNode<TKey, TValue>();
        }

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> �Ɋi�[����Ă���v�f�̐���擾���܂��B
        ///</summary>
        ///<returns> <see cref="TValue:System.Collections.Generic.ICollection`1" /> �Ɋi�[����Ă���v�f�̐��B </returns>
        public int Count {
            get { return _count; }
        }

        ///<summary>
        ///  �R���N�V�����𔽕���������񋓎q��Ԃ��܂��B
        ///</summary>
        ///<returns> �R���N�V�����𔽕��������邽�߂Ɏg�p�ł��� <see cref="TValue:System.Collections.Generic.IEnumerator`1" /> �B </returns>
        ///<filterpriority>1</filterpriority>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
            return new SortedListEnumerator<TKey, TValue>(this, head_);
        }

        ///<summary>
        ///  �R���N�V�����𔽕���������񋓎q��Ԃ��܂��B
        ///</summary>
        ///<returns> �R���N�V�����𔽕��������邽�߂Ɏg�p�ł��� <see cref="TValue:System.Collections.IEnumerator" /> �I�u�W�F�N�g�B </returns>
        ///<filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator() {
            return new SortedListEnumerator<TKey, TValue>(this, head_);
        }

        #endregion

        public void Add(TKey key, TValue value) {
            // �ԕ��̐ݒu
            head_.item_ = new KeyValuePair<TKey, TValue>(key, value);

            AddPrivate(key, value, head_);
        }

        public void Add(
                TKey key, TValue value, SortedListNode<TKey, TValue> pivot) {
            // �ԕ��̐ݒu
            head_.item_ = new KeyValuePair<TKey, TValue>(key, value);

            if (_cmp.Compare(pivot.item_.Key, key) > 0) {
                AddPrivate(key, value, pivot);
            } else {
                AddPrivate(key, value, head_);
            }
        }

        private void AddPrivate(
                TKey key, TValue value, SortedListNode<TKey, TValue> node) {
            do {
                node = node.prev_;
            } while (_cmp.Compare(node.item_.Key, key) > 0);
            Insert(node, new SortedListNode<TKey, TValue>(key, value));
        }

        public SortedListNode<TKey, TValue> FindFirstNode(TKey key) {
            // �ԕ���ݒu���ĒT��
            head_.item_ = new KeyValuePair<TKey, TValue>(key, default(TValue));

            var node = head_;
            do {
                node = node.next_;
            } while (_cmp.Compare(node.item_.Key, key) != 0);

            return node != head_ ? node : null;
        }

        public SortedListNode<TKey, TValue> FindFirstNode(TValue value) {
            // �ԕ���ݒu���ĒT��
            head_.item_ = new KeyValuePair<TKey, TValue>(default(TKey), value);

            var node = head_;
            do {
                node = node.next_;
            } while (node.item_.Value.Equals(value) == false);

            return node != head_ ? node : null;
        }

        public SortedListNode<TKey, TValue> FindFirstNode(
                TKey key, TValue value) {
            // �ԕ���ݒu���ĒT��
            head_.item_ = new KeyValuePair<TKey, TValue>(key, value);

            var node = head_;
            do {
                node = node.next_;
            } while (_cmp.Compare(node.item_.Key, key) != 0
                     || node.item_.Value.Equals(value) == false);

            return node != head_ ? node : null;
        }

        public SortedListNode<TKey, TValue> FindLastNode(TKey key) {
            // �ԕ���ݒu���ĒT��
            head_.item_ = new KeyValuePair<TKey, TValue>(key, default(TValue));

            var node = head_;
            do {
                node = node.prev_;
            } while (_cmp.Compare(node.item_.Key, key) != 0);

            return node != head_ ? node : null;
        }

        public SortedListNode<TKey, TValue> FindLastNode(TValue value) {
            // �ԕ���ݒu���ĒT��
            head_.item_ = new KeyValuePair<TKey, TValue>(default(TKey), value);

            var node = head_;
            do {
                node = node.prev_;
            } while (node.item_.Value.Equals(value) == false);

            return node != head_ ? node : null;
        }

        public SortedListNode<TKey, TValue> FindLastNode(TKey key, TValue value) {
            // �ԕ���ݒu���ĒT��
            head_.item_ = new KeyValuePair<TKey, TValue>(key, value);

            var node = head_;
            do {
                node = node.prev_;
            } while (_cmp.Compare(node.item_.Key, key) != 0
                     || node.item_.Value.Equals(value) == false);

            return node != head_ ? node : null;
        }

        public SortedListNode<TKey, TValue> GetNextNode(
                SortedListNode<TKey, TValue> node) {
            node = node.next_;
            return node != head_ ? node : null;
        }

        public SortedListNode<TKey, TValue> GetPrevNode(
                SortedListNode<TKey, TValue> node) {
            node = node.prev_;
            return node != head_ ? node : null;
        }

        private void Insert(
                SortedListNode<TKey, TValue> prev,
                SortedListNode<TKey, TValue> newNext) {
            newNext.next_ = prev.next_;
            newNext.prev_ = prev;
            prev.next_.prev_ = newNext;
            prev.next_ = newNext;
            _count++;
        }

        public void Remove(SortedListNode<TKey, TValue> node) {
            node.prev_.next_ = node.next_;
            node.next_.prev_ = node.prev_;
            _count--;
        }

        public void Remove(TKey key) {
            // �ԕ���ݒu���ĒT��
            head_.item_ = new KeyValuePair<TKey, TValue>(key, default(TValue));

            var node = head_;
            do {
                node = node.next_;
            } while (_cmp.Compare(node.item_.Key, key) != 0);

            // �ԕ��łȂ���΍폜
            if (node != head_) {
                Remove(node);
            }
        }

        public void Remove(TValue value) {
            // �ԕ���ݒu���ĒT��
            head_.item_ = new KeyValuePair<TKey, TValue>(default(TKey), value);

            var node = head_;
            do {
                node = node.next_;
            } while (node.item_.Value.Equals(value) == false);

            // �ԕ��łȂ���΍폜
            if (node != head_) {
                Remove(node);
            }
        }

        public void Remove(TKey key, TValue value) {
            // �ԕ���ݒu���ĒT��
            head_.item_ = new KeyValuePair<TKey, TValue>(key, value);

            var node = head_;
            do {
                node = node.next_;
            } while (_cmp.Compare(node.item_.Key, key) != 0
                     || node.item_.Value.Equals(value) == false);

            // �ԕ��łȂ���΍폜
            if (node != head_) {
                Remove(node);
            }
        }

        public SortedListEnumerator<TKey, TValue> GetListEnumerator() {
            return new SortedListEnumerator<TKey, TValue>(this, head_);
        }

        #region ICollection<KeyValuePair<TKey,TValue>> �����o

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> �ɍ��ڂ�ǉ����܂��B
        ///</summary>
        ///<param name="item"> <see cref="TValue:System.Collections.Generic.ICollection`1" /> �ɒǉ�����I�u�W�F�N�g�B </param>
        ///<exception cref="TValue:System.NotSupportedException">
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" />
        ///  �͓ǂݎ���p�ł��B</exception>
        public void Add(KeyValuePair<TKey, TValue> item) {
            Add(item.Key, item.Value);
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> ���炷�ׂĂ̍��ڂ�폜���܂��B
        ///</summary>
        ///<exception cref="TValue:System.NotSupportedException">
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" />
        ///  �͓ǂݎ���p�ł��B</exception>
        public void Clear() {
            head_.next_ = head_;
            head_.prev_ = head_;
            _count = 0;
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> �ɓ���̒l���i�[����Ă��邩�ǂ����𔻒f���܂��B
        ///</summary>
        ///<returns> <paramref name="item" /> �� <see cref="TValue:System.Collections.Generic.ICollection`1" /> �ɑ��݂���ꍇ�� true�B����ȊO�̏ꍇ�� false�B </returns>
        ///<param name="item"> <see cref="TValue:System.Collections.Generic.ICollection`1" /> ��Ō�������I�u�W�F�N�g�B </param>
        public bool Contains(KeyValuePair<TKey, TValue> item) {
            return FindFirstNode(item.Key, item.Value) != null;
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> �̗v�f�� <see cref="TValue:System.Array" /> �ɃR�s�[���܂��B <see
        ///   cref="TValue:System.Array" /> �̓���̃C���f�b�N�X����R�s�[���J�n����܂��B
        ///</summary>
        ///<param name="array"> <see cref="TValue:System.Collections.Generic.ICollection`1" /> ����v�f���R�s�[����� 1 ������ <see
        ///   cref="TValue:System.Array" /> �B <see cref="TValue:System.Array" /> �ɂ́A0 ����n�܂�C���f�b�N�X�ԍ����K�v�ł��B </param>
        ///<param name="arrayIndex"> �R�s�[�̊J�n�ʒu�ƂȂ�A <paramref name="array" /> �� 0 ����n�܂�C���f�b�N�X�ԍ��B </param>
        ///<exception cref="TValue:System.ArgumentNullException">
        ///  <paramref name="array" />
        ///  �� null �ł��B</exception>
        ///<exception cref="TValue:System.ArgumentOutOfRangeException">
        ///  <paramref name="arrayIndex" />
        ///  �� 0 �����ł��B</exception>
        ///<exception cref="TValue:System.ArgumentException">
        ///  <paramref name="array" />
        ///  ���������ł��B
        ///  �܂���
        ///  <paramref name="arrayIndex" />
        ///  �� array �̒����ȏ�ł��B
        ///  �܂���
        ///  �R�s�[����
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" />
        ///  �̗v�f�����A
        ///  <paramref name="arrayIndex" />
        ///  ����R�s�[���
        ///  <paramref name="array" />
        ///  �̖����܂łɊi�[�ł��鐔�𒴂��Ă��܂��B
        ///  �܂���
        ///  �^
        ///  <paramref name="TValue" />
        ///  ��R�s�[���
        ///  <paramref name="array" />
        ///  �̌^�Ɏ����I�ɃL���X�g���邱�Ƃ͂ł��܂���B</exception>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
            if (array == null) {
                throw new ArgumentNullException();
            }
            if (arrayIndex < 0) {
                throw new ArgumentOutOfRangeException();
            }
            if (array.Length > arrayIndex + _count) {
                throw new ArgumentException();
            }

            var node = head_.next_;
            while (node != head_) {
                array[arrayIndex++] = node.item_;
            }
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> ���ǂݎ���p���ǂ���������l��擾���܂��B
        ///</summary>
        ///<returns> <see cref="TValue:System.Collections.Generic.ICollection`1" /> ���ǂݎ���p�̏ꍇ�� true�B����ȊO�̏ꍇ�� false�B </returns>
        public bool IsReadOnly {
            get { return false; }
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> ��ōŏ��Ɍ�����������̃I�u�W�F�N�g��폜���܂��B
        ///</summary>
        ///<returns> <paramref name="item" /> �� <see cref="TValue:System.Collections.Generic.ICollection`1" /> ���琳��ɍ폜���ꂽ�ꍇ�� true�B����ȊO�̏ꍇ�� false�B���̃��\�b�h�́A <paramref
        ///   name="item" /> ������ <see cref="TValue:System.Collections.Generic.ICollection`1" /> �Ɍ�����Ȃ��ꍇ�ɂ� false ��Ԃ��܂��B </returns>
        ///<param name="item"> <see cref="TValue:System.Collections.Generic.ICollection`1" /> ����폜����I�u�W�F�N�g�B </param>
        ///<exception cref="TValue:System.NotSupportedException">
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" />
        ///  �͓ǂݎ���p�ł��B</exception>
        public bool Remove(KeyValuePair<TKey, TValue> item) {
            var node = FindFirstNode(item.Key, item.Value);
            if (node != null) {
                Remove(node);
                return true;
            }
            return false;
        }

        #endregion
            }
}