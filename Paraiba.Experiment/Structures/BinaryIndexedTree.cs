namespace Paraiba.Structures
{
	public class BinaryIndexedTree
	{
		int[] _frequencyTree;

		public BinaryIndexedTree(int size)
		{
			_frequencyTree = new int[size];
		}

		public int sum(int from, int to)
		{
			if (from == 0)
			{
				int s = 0;
				for (int i = to; i >= 0; i = (i & (i + 1)) - 1)
					s += _frequencyTree[i];
				return s;
			}

			return sum(0, to) - sum(0, from - 1);
		}

		public void add(int place, int incVal)
		{
			for (int i = place; i < _frequencyTree.Length; i |= i + 1)
				_frequencyTree[i] += incVal;
		}
	}
}


