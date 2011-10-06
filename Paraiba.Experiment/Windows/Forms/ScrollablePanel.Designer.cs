namespace Paraiba.Windows.Forms
{
	partial class ScrollablePanel
	{
		/// <summary> 
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナで生成されたコード

		/// <summary> 
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this._hScrollBar = new System.Windows.Forms.HScrollBar();
			this._vScrollBar = new System.Windows.Forms.VScrollBar();
			this.SuspendLayout();
			// 
			// _hScrollBar
			// 
			this._hScrollBar.Anchor = System.Windows.Forms.AnchorStyles.None;
			this._hScrollBar.Location = new System.Drawing.Point(0, 133);
			this._hScrollBar.Name = "_hScrollBar";
			this._hScrollBar.Size = new System.Drawing.Size(133, 17);
			this._hScrollBar.TabIndex = 0;
			this._hScrollBar.ValueChanged += new System.EventHandler(this.ScrollBar_ValueChanged);
			// 
			// _vScrollBar
			// 
			this._vScrollBar.Anchor = System.Windows.Forms.AnchorStyles.None;
			this._vScrollBar.Location = new System.Drawing.Point(133, 0);
			this._vScrollBar.Name = "_vScrollBar";
			this._vScrollBar.Size = new System.Drawing.Size(17, 133);
			this._vScrollBar.TabIndex = 1;
			this._vScrollBar.ValueChanged += new System.EventHandler(this.ScrollBar_ValueChanged);
			// 
			// ScrollablePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this._vScrollBar);
			this.Controls.Add(this._hScrollBar);
			this.Name = "ScrollablePanel";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.HScrollBar _hScrollBar;
		private System.Windows.Forms.VScrollBar _vScrollBar;
	}
}