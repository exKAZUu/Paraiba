namespace Paraiba.Wrap {
	public class CastedWrap<T, TOrg> : Wrap<T>
			where TOrg : T
	{
		private readonly IWrap<TOrg> _wrap;

		public CastedWrap(IWrap<TOrg> wrap)
		{
			_wrap = wrap;
		}

		public override T Value
		{
			get { return _wrap.Value; }
		}
	}
}