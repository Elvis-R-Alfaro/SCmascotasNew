namespace SC_MMascotass.Reportes
{
    partial class ReporteBuscarCargo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.BuscarPersonalxCargoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetPrincipal = new SC_MMascotass.Reportes.DataSetPrincipal();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.BuscarPersonalxCargoTableAdapter = new SC_MMascotass.Reportes.DataSetPrincipalTableAdapters.BuscarPersonalxCargoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.BuscarPersonalxCargoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetPrincipal)).BeginInit();
            this.SuspendLayout();
            // 
            // BuscarPersonalxCargoBindingSource
            // 
            this.BuscarPersonalxCargoBindingSource.DataMember = "BuscarPersonalxCargo";
            this.BuscarPersonalxCargoBindingSource.DataSource = this.DataSetPrincipal;
            // 
            // DataSetPrincipal
            // 
            this.DataSetPrincipal.DataSetName = "DataSetPrincipal";
            this.DataSetPrincipal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.BuscarPersonalxCargoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SC_MMascotass.Reportes.ReporteEmpleadoxCargo.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // BuscarPersonalxCargoTableAdapter
            // 
            this.BuscarPersonalxCargoTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteBuscarCargo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteBuscarCargo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReporteBuscarCargo";
            this.Load += new System.EventHandler(this.ReporteBuscarCargo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BuscarPersonalxCargoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetPrincipal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource BuscarPersonalxCargoBindingSource;
        private DataSetPrincipal DataSetPrincipal;
        private DataSetPrincipalTableAdapters.BuscarPersonalxCargoTableAdapter BuscarPersonalxCargoTableAdapter;
    }
}