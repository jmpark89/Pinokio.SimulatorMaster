

namespace Pinokio.Layout
{
    partial class MainFrame
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrame));
            this._3DModelDesigner1 = new Pinokio.Designer.ModelDesigner();
            this.SuspendLayout();
            // 
            // _3DModelDesigner1
            // 
            this._3DModelDesigner1.MouseSnapPointBeforeMouseMove = null;
            this._3DModelDesigner1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._3DModelDesigner1.EntityIndex = -1;
            this._3DModelDesigner1.Location = new System.Drawing.Point(0, 0);
            this._3DModelDesigner1.Name = "_3DModelDesigner1";
//            this._3DModelDesigner1.Plane = new devDept.Geometry.Plane(new Point3D(0D, 0D, 0D), new Vector3D(0D, 0D, 1D));
            this._3DModelDesigner1.Size = new System.Drawing.Size(1585, 988);
            this._3DModelDesigner1.TabIndex = 0;
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1585, 988);
            this.Controls.Add(this._3DModelDesigner1);
            this.IconOptions.Image = global::Pinokio.SIM.Properties.Resources.PINOKIO_SIM;
            this.Name = "MainFrame";
            this.Text = "PINOKIO Simulator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrame_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrame_FormClosed);
            this.Shown += MainFrame_Shown;
            this.ResumeLayout(false);

        }

        #endregion
        private Pinokio.Designer.ModelDesigner _3DModelDesigner1;
    }
}

