using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data.SqlTypes;
using System.Text;

namespace ClassMapper
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	/// 

	public class MainForm : System.Windows.Forms.Form
	{
	    
		private System.Windows.Forms.ListView List;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button start;
		private System.Windows.Forms.TextBox classdef;
		private System.Windows.Forms.ComboBox style;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox prefix;
		private System.Windows.Forms.TextBox postfix;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox ancestor;
		private System.Windows.Forms.Label DB;
		private System.Windows.Forms.ComboBox DBList;
		private System.Windows.Forms.Button copy;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox classModifier;
		private System.Windows.Forms.ComboBox fieldModifier;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ImageList images;
        private CheckBox hasAutoProps;
        private System.ComponentModel.IContainer components;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.List = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.images = new System.Windows.Forms.ImageList(this.components);
            this.start = new System.Windows.Forms.Button();
            this.classdef = new System.Windows.Forms.TextBox();
            this.style = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.prefix = new System.Windows.Forms.TextBox();
            this.postfix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ancestor = new System.Windows.Forms.TextBox();
            this.DB = new System.Windows.Forms.Label();
            this.DBList = new System.Windows.Forms.ComboBox();
            this.copy = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.classModifier = new System.Windows.Forms.ComboBox();
            this.fieldModifier = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.hasAutoProps = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // List
            // 
            this.List.BackColor = System.Drawing.Color.Gray;
            this.List.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.List.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.List.ForeColor = System.Drawing.Color.White;
            this.List.HideSelection = false;
            this.List.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.List.Location = new System.Drawing.Point(0, 5);
            this.List.Name = "List";
            this.List.Size = new System.Drawing.Size(370, 332);
            this.List.SmallImageList = this.images;
            this.List.TabIndex = 14;
            this.List.UseCompatibleStateImageBehavior = false;
            this.List.View = System.Windows.Forms.View.Details;
            this.List.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.OnColumn);
            this.List.DoubleClick += new System.EventHandler(this.OnStart);
            this.List.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKey);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Relation entity";
            this.columnHeader1.Width = 350;
            // 
            // images
            // 
            this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
            this.images.TransparentColor = System.Drawing.Color.Transparent;
            this.images.Images.SetKeyName(0, "");
            // 
            // start
            // 
            this.start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.start.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.start.ForeColor = System.Drawing.Color.Red;
            this.start.Location = new System.Drawing.Point(702, 13);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(74, 22);
            this.start.TabIndex = 16;
            this.start.Text = "Generate";
            this.start.Click += new System.EventHandler(this.OnStart);
            // 
            // classdef
            // 
            this.classdef.BackColor = System.Drawing.SystemColors.Info;
            this.classdef.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.classdef.Location = new System.Drawing.Point(382, 116);
            this.classdef.Multiline = true;
            this.classdef.Name = "classdef";
            this.classdef.Size = new System.Drawing.Size(394, 220);
            this.classdef.TabIndex = 15;
            // 
            // style
            // 
            this.style.BackColor = System.Drawing.SystemColors.Info;
            this.style.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.style.ForeColor = System.Drawing.Color.Black;
            this.style.Items.AddRange(new object[] {
            "Typed       C#",
            "UnTyped   C#",
            "Typed       VB.NET",
            "UnTyped   VB.NET",
            "Typed       VB",
            "UnTyped   VB",
            "Reporting",
            "Config",
            "EF Mapping",
            "QueryBuilder Mapping",
            "DDL Schema",
            "SQL Data"});
            this.style.Location = new System.Drawing.Point(492, 37);
            this.style.Name = "style";
            this.style.Size = new System.Drawing.Size(132, 21);
            this.style.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(382, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 14);
            this.label1.TabIndex = 18;
            this.label1.Text = "Type:";
            // 
            // prefix
            // 
            this.prefix.BackColor = System.Drawing.SystemColors.Info;
            this.prefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prefix.Location = new System.Drawing.Point(426, 37);
            this.prefix.Name = "prefix";
            this.prefix.Size = new System.Drawing.Size(65, 20);
            this.prefix.TabIndex = 19;
            // 
            // postfix
            // 
            this.postfix.BackColor = System.Drawing.SystemColors.Info;
            this.postfix.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.postfix.Location = new System.Drawing.Point(624, 37);
            this.postfix.Name = "postfix";
            this.postfix.Size = new System.Drawing.Size(65, 20);
            this.postfix.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(385, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 14);
            this.label2.TabIndex = 21;
            this.label2.Text = "Base:";
            // 
            // ancestor
            // 
            this.ancestor.BackColor = System.Drawing.SystemColors.Info;
            this.ancestor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ancestor.Location = new System.Drawing.Point(426, 62);
            this.ancestor.Name = "ancestor";
            this.ancestor.Size = new System.Drawing.Size(263, 20);
            this.ancestor.TabIndex = 22;
            this.ancestor.Text = "System.Object";
            // 
            // DB
            // 
            this.DB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DB.Location = new System.Drawing.Point(383, 17);
            this.DB.Name = "DB";
            this.DB.Size = new System.Drawing.Size(34, 14);
            this.DB.TabIndex = 23;
            this.DB.Text = "DB:";
            // 
            // DBList
            // 
            this.DBList.BackColor = System.Drawing.SystemColors.Info;
            this.DBList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DBList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DBList.ForeColor = System.Drawing.Color.Black;
            this.DBList.Location = new System.Drawing.Point(426, 12);
            this.DBList.Name = "DBList";
            this.DBList.Size = new System.Drawing.Size(263, 21);
            this.DBList.TabIndex = 24;
            this.DBList.SelectedIndexChanged += new System.EventHandler(this.DBList_SelectedIndexChanged);
            // 
            // copy
            // 
            this.copy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copy.Location = new System.Drawing.Point(701, 37);
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(75, 23);
            this.copy.TabIndex = 25;
            this.copy.Text = "Copy";
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(385, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 14);
            this.label3.TabIndex = 26;
            this.label3.Text = "Class:";
            // 
            // classModifier
            // 
            this.classModifier.BackColor = System.Drawing.SystemColors.Info;
            this.classModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.classModifier.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.classModifier.ForeColor = System.Drawing.Color.Black;
            this.classModifier.Items.AddRange(new object[] {
            "Public",
            "Internal(Friend)",
            "Protected",
            "Private",
            "Sealed"});
            this.classModifier.Location = new System.Drawing.Point(426, 86);
            this.classModifier.Name = "classModifier";
            this.classModifier.Size = new System.Drawing.Size(118, 21);
            this.classModifier.TabIndex = 27;
            // 
            // fieldModifier
            // 
            this.fieldModifier.BackColor = System.Drawing.SystemColors.Info;
            this.fieldModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldModifier.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldModifier.ForeColor = System.Drawing.Color.Black;
            this.fieldModifier.Items.AddRange(new object[] {
            "Public",
            "Internal(Friend)",
            "Protected",
            "Private"});
            this.fieldModifier.Location = new System.Drawing.Point(587, 86);
            this.fieldModifier.Name = "fieldModifier";
            this.fieldModifier.Size = new System.Drawing.Size(101, 21);
            this.fieldModifier.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(548, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 14);
            this.label4.TabIndex = 28;
            this.label4.Text = "Field:";
            // 
            // hasAutoProps
            // 
            this.hasAutoProps.AutoSize = true;
            this.hasAutoProps.Location = new System.Drawing.Point(692, 86);
            this.hasAutoProps.Name = "hasAutoProps";
            this.hasAutoProps.Size = new System.Drawing.Size(78, 17);
            this.hasAutoProps.TabIndex = 30;
            this.hasAutoProps.Text = "Auto Props";
            this.hasAutoProps.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(784, 341);
            this.Controls.Add(this.hasAutoProps);
            this.Controls.Add(this.fieldModifier);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.classModifier);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.copy);
            this.Controls.Add(this.DBList);
            this.Controls.Add(this.DB);
            this.Controls.Add(this.ancestor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.postfix);
            this.Controls.Add(this.prefix);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.style);
            this.Controls.Add(this.List);
            this.Controls.Add(this.start);
            this.Controls.Add(this.classdef);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relation entity  to class mapping";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion



		private void InternalResize()
		{
			List.Height  = this.ClientSize.Height - List.Top ;
			classdef.Height = this.ClientSize.Height - classdef.Top;
			classdef.Width  = this.ClientSize.Width - classdef.Left; 
		}

		private void InternalLoadEntities()
		{
			
			List.Items.Clear();
			IDbCommand    command  = DBFactory.Create("select name from sysobjects where xtype='U'");
			IDataReader reader = command.ExecuteReader();
			while(reader.Read()) List.Items.Add(new EntityListViewItem(reader.GetString(0)));
			DBFactory.Release(command);
	
		}

		private void InternalLoadDataBases()
		{
			DBList.Items.Clear();
			IDbCommand    command  = DBFactory.Create("select name from sysdatabases");
			IDataReader reader = command.ExecuteReader();
			while(reader.Read()) DBList.Items.Add(reader.GetString(0));
			DBFactory.Release(command);
		   DBList.SelectedIndex = DBList.Items.Count>0?0:-1;
	
		}


  

		private void MainForm_Load(object sender, System.EventArgs e)
		{


			classModifier.SelectedIndex = 0;
			fieldModifier.SelectedIndex = 0;
			style.SelectedIndex = 0;
			InternalResize();
			InternalLoadEntities();
			InternalLoadDataBases();
			
	
		}


		private Modifiers[] Modifier = new Modifiers[]{Modifiers.Public,Modifiers.Internal,Modifiers.Protected,Modifiers.Private,Modifiers.Public | Modifiers.Sealed};
		private CodeStyle[] Styles = new CodeStyle[]{CodeStyle.cnet,CodeStyle.cnet|CodeStyle.untyped,CodeStyle.vbnet, CodeStyle.vbnet|CodeStyle.untyped,CodeStyle.vb,CodeStyle.vb | CodeStyle.untyped,CodeStyle.reporting,CodeStyle.config,CodeStyle.efMapping,CodeStyle.qbMapping,CodeStyle.ddlSchema,CodeStyle.sqlData};


		private void MainForm_Resize(object sender, System.EventArgs e)
		{
	      InternalResize();
		}

		private void DBList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (String.Compare(DBFactory.DataBase,DBList.Text)==0) return;
            DBFactory.DataBase = DBList.Text;
			InternalLoadEntities();

		}

		private void copy_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject(classdef.Text,true);
		}

		private void OnStart(object sender, System.EventArgs e)
		{
			classdef.ForeColor  = Color.Red;
			classdef.Text = "In process...";
			ClassBuilder builder = new ClassBuilder(prefix.Text,Styles[style.SelectedIndex],postfix.Text,ancestor.Text);

            builder.HasAutoProperty = hasAutoProps.Checked;

			Modifier[1] = builder.IsCNET?Modifiers.Internal:Modifiers.Friend; 
			foreach(EntityListViewItem entity in List.SelectedItems) entity.WriteTo(Modifier[classModifier.SelectedIndex],Modifier[fieldModifier.SelectedIndex],builder);
			classdef.ForeColor  = Color.Black;
			classdef.Text = builder.ToString();
		}

		private void OnColumn(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			OnStart(sender,e);
		}

		private void OnKey(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) OnStart(sender,e);
		}


	
	}

	public class DBFactory
	{
	



        private static string TryGetAppSetting(string keyName, string defValue)
        {
            string[] appSetting = ConfigurationManager.AppSettings.GetValues(keyName);

            if (appSetting == null || appSetting.Length < 1)
            {
                return defValue;
            }

            return appSetting[0];
        }

        private static SqlConnectionStringBuilder sql_connection_builder = new SqlConnectionStringBuilder();

        public static string DataBase
        {
            get { return sql_connection_builder.InitialCatalog; }
            set
            {
                sql_connection_builder.InitialCatalog = value;
            }
        }


        static DBFactory()
		{

            sql_connection_builder = new SqlConnectionStringBuilder(TryGetAppSetting("ConnectionString", string.Empty));

            if (sql_connection_builder.Count < 1)
            {
                sql_connection_builder.DataSource     = TryGetAppSetting("Server", ".");
                sql_connection_builder.InitialCatalog = TryGetAppSetting("DataBase", "master");
                sql_connection_builder.UserID         = TryGetAppSetting("Admin", "sa");
                sql_connection_builder.Password       = TryGetAppSetting("Password", "1");

            }



        }


		
		public static IDbCommand Create(string sql)
		{
			IDbConnection connection = new SqlConnection(sql_connection_builder.ToString());
			connection.Open();
			IDbCommand    command  = connection.CreateCommand();
			command.CommandText = sql;
			return  command;

		}


		public static void Release(IDbCommand command)
		{
			command.Connection.Close();
		}

	}
    

	public class DBReader
	{
	}

	public class DBWriter
	{
	}

	public class EntityListViewItem : ListViewItem
	{
		public EntityListViewItem(string entity) : base(entity)
		{
			StateImageIndex = 0;
			ImageIndex = 0;
		}
        

	
		public void WriteTo(Modifiers Class,Modifiers Field,ClassBuilder stream)
		{


            StringBuilder  sql =  new StringBuilder("select");

            if (!stream.IsSqlData)
            {
                sql.Append(" top 1");
            }

            sql.Append(" * from [")
               .Append(Text)
               .Append("]");




			IDbCommand  command = DBFactory.Create(sql.ToString());
			IDataReader reader  = command.ExecuteReader();

            var dataTable = reader.GetSchemaTable();

            var isIdentity   = dataTable.Columns["IsIdentity"];
            var isUnique     = dataTable.Columns["IsUnique"];
            var isKey        = dataTable.Columns["IsKey"];
            var columnName   = dataTable.Columns["ColumnName"];
            var columnSize   = dataTable.Columns["ColumnSize"];
            var dataType     = dataTable.Columns["DataType"];
            var allowDBNull  = dataTable.Columns["AllowDBNull"];

            stream.AddBeginClass(Class,this.Text);

            foreach (DataRow row in dataTable.Rows)
            {
                string memberName = row[columnName].ToString();

                var is_key      = row[isKey];
                var is_identity = row[isIdentity];
                var is_unique   = row[isUnique];

                if (true.Equals(is_key) || true.Equals(is_identity))
                {
                    stream.IdentityMembers.Add(memberName);
                }

                stream.AddMember(Field, true.Equals(is_key) || true.Equals(is_identity), row[dataType] as Type, (bool)row[allowDBNull],(int)row[columnSize], memberName);
            }

			if (stream.IsVB)
			{

				stream.AddBeginMethod("ReadAll",new Argument(typeof(DBReader),"Reader"));
				for(int i=0;i<reader.FieldCount;i++) stream.AddCodeLine(reader.GetName(i),"=","Reader!",reader.GetName(i));
				stream.AddEndMethod();

				stream.AddBeginMethod("WriteAll",new Argument(typeof(DBWriter),"Writer"));
				for(int i=0;i<reader.FieldCount;i++) stream.AddCodeLine("Writer!",reader.GetName(i),"=",reader.GetName(i));
				stream.AddEndMethod();

			}

			stream.AddEndClass();


            if (stream.IsSqlData)
            {
                stream.AddInsertData(reader);
            }





			DBFactory.Release(command);
		}
	}

}
