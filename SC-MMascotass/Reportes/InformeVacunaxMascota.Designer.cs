namespace SC_MMascotass.Reportes
{
    partial class InformeVacunaxMascota
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.VacunaxMascotaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetPrincipal = new SC_MMascotass.Reportes.DataSetPrincipal();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.VacunaxMascotaTableAdapter = new SC_MMascotass.Reportes.DataSetPrincipalTableAdapters.VacunaxMascotaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.VacunaxMascotaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetPrincipal)).BeginInit();
            this.SuspendLayout();
            // 
            // VacunaxMascotaBindingSource
            // 
            this.VacunaxMascotaBindingSource.DataMember = "VacunaxMascota";
            this.VacunaxMascotaBindingSource.DataSource = this.DataSetPrincipal;
            // 
            // DataSetPrincipal
            // 
            this.DataSetPrincipal.DataSetName = "DataSetPrincipal";
            this.DataSetPrincipal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.VacunaxMascotaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SC_MMascotass.Reportes.InformeVacunaxMascota.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // VacunaxMascotaTableAdapter
            // 
            this.VacunaxMascotaTableAdapter.ClearBeforeFill = true;
            // 
            // InformeVacunaxMascota
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "InformeVacunaxMascota";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InformeVacunaxMascota";
            this.Load += new System.EventHandler(this.InformeVacunaxMascota_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VacunaxMascotaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetPrincipal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource VacunaxMascotaBindingSource;
        private DataSetPrincipal DataSetPrincipal;
        private DataSetPrincipalTableAdapters.VacunaxMascotaTableAdapter VacunaxMascotaTableAdapter;
    }
}