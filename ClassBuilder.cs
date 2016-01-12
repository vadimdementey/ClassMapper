using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Data;

namespace ClassMapper
{
	/// <summary>
	/// Summary description for ClassBuilder.
	/// </summary>
	/// 

	public enum CodeStyle : int {cnet=1,vbnet=2,vb=4,untyped=8,reporting=16,config=32,efMapping=64,qbMapping = 128,ddlSchema = 256, sqlData = 512};
	public enum Modifiers : int {Public=1,Private=2,Protected=4,Internal=8,Virtual=16,Override=32,Static=64,Const=128,UnSafe=256,Abstract=512,Event=1024,ReadOnly=2048,Sealed=4096,Friend=8192}




	public struct Argument
	{
		internal Type type;
		internal string name;

		public Argument(Type type,string name)
		{
	
			this.type  = type;
			this.name  = name;

		}

	}

	public class ClassBuilder 
	{

        public List<string> IdentityMembers = new List<string>();

        public bool HasAutoProperty;
		
		protected CodeStyle Style;
		protected string TypePrefix;
		protected string TypePostfix;
		public    string Ancestor;

		private  string  CurrentClassName; 

		private  StringBuilder sb;

		public ClassBuilder()
		{
			 Style = CodeStyle.cnet;
			 TypePrefix = null;
			 TypePostfix = null;
			 Ancestor = null;
			 sb = new StringBuilder();
 		}

		public ClassBuilder(string TypePrefix,CodeStyle Style,string TypePostfix,string Ancestor)
		{
			this.TypePrefix = TypePrefix;
			this.Style = Style;
			this.TypePostfix = TypePostfix;
			this.Ancestor = Ancestor;
			sb = new StringBuilder();
		}
       

		public bool IsCNET
		{
			get {return (Style & CodeStyle.cnet)!=0;}
		}

		public bool IsVBNET
		{
			get {return (Style & CodeStyle.vbnet)!=0;}
		}


		public bool IsVB
		{
			get {return (Style & CodeStyle.vb)!=0;}
		}

		public bool IsUntyped
		{
			get {return (Style & CodeStyle.untyped)!=0;}
		}

		public bool IsTyped
		{
			get {return (Style & CodeStyle.untyped)==0;}
		}

		public bool IsReporting
		{
			get {return (Style & CodeStyle.reporting)!=0;}
		}


		public bool IsConfig
		{
			get {return (Style & CodeStyle.config)!=0;}
		}


        public bool IsEFMapping
        {
            get { return ((Style & CodeStyle.efMapping) == CodeStyle.efMapping); }
        }



        public bool IsQbMapping
        {
            get { return ((Style & CodeStyle.qbMapping) == CodeStyle.qbMapping); }
        }


        public bool IsDDLSchema
        {
            get { return ((Style & CodeStyle.ddlSchema) == CodeStyle.ddlSchema); }
        }



        public bool IsSqlData
        {
            get { return ((Style & CodeStyle.sqlData) == CodeStyle.sqlData); }
        }





        public void Clear()
		{
			sb.Length = 0;
		}

		private string ModifiersToString(Modifiers modifiers)
		{
			string result="";
			if ((modifiers & Modifiers.Public)  !=0)   result  += "public ";
			if ((modifiers & Modifiers.Private) !=0)   result  += "private ";
			if ((modifiers & Modifiers.Protected) !=0) result  += "protected ";
			if ((modifiers & Modifiers.Internal) !=0)  result  += "internal ";
			if ((modifiers & Modifiers.Virtual) !=0)   result  += "virtual ";
			if ((modifiers & Modifiers.Override) !=0)  result  += "override ";
			if ((modifiers & Modifiers.Static) !=0)    result  += "static ";
			if ((modifiers & Modifiers.Const) !=0)     result  += "const ";
			if ((modifiers & Modifiers.UnSafe) !=0)    result  += "unsafe ";
			if ((modifiers & Modifiers.Abstract) !=0)  result  += "abstract ";
			if ((modifiers & Modifiers.Event) !=0)     result  += "event ";
			if ((modifiers & Modifiers.ReadOnly) !=0)  result  += "readonly ";
			if ((modifiers & Modifiers.Sealed) !=0)    result  += "sealed ";
			if ((modifiers & Modifiers.Friend) !=0)    result  += "friend ";
			
			return result;

		}

		private string TypeToString(Type FieldType)
		{   
		    
			if (FieldType==typeof(bool))    return  "bool";
			if (FieldType==typeof(byte))    return  "byte";
			if (FieldType==typeof(sbyte))   return  "sbyte";
			if (FieldType==typeof(char))    return  "char";
			if (FieldType==typeof(decimal)) return  "decimal";
			if (FieldType==typeof(double))  return  "double";
			if (FieldType==typeof(float))   return  "float";
			if (FieldType==typeof(int))     return  "int";
			if (FieldType==typeof(uint))    return  "uint";
			if (FieldType==typeof(long))    return  "long";
			if (FieldType==typeof(ulong))   return  "ulong";
			if (FieldType==typeof(object))  return  "object";
			if (FieldType==typeof(short))   return "short";
			if (FieldType==typeof(ushort))  return "ushort";
			if (FieldType==typeof(string))  return "string";
		
			return FieldType.Name;
		}



        private string TypeToSql(Type FieldType)
        {

            if (FieldType == typeof(bool)) return "bit";
            if (FieldType == typeof(byte)) return "byte";
            if (FieldType == typeof(sbyte)) return "sbyte";
            if (FieldType == typeof(char)) return "char";
            if (FieldType == typeof(decimal)) return "decimal";
            if (FieldType == typeof(double)) return "float";
            if (FieldType == typeof(float)) return "float";
            if (FieldType == typeof(int)) return "int";
            if (FieldType == typeof(uint)) return "int";
            if (FieldType == typeof(long)) return "bigint";
            if (FieldType == typeof(ulong)) return "bigint";
            if (FieldType == typeof(object)) return "image";
            if (FieldType == typeof(short)) return "short";
            if (FieldType == typeof(ushort)) return "ushort";
            if (FieldType == typeof(string)) return "nvarchar";

            if (FieldType == typeof(Guid)) return "uniqueidentifier";

            if (FieldType == typeof(byte[])) return "image";



            return FieldType.Name;
        }


        public void AddBeginClass(string Name)
		{
			AddBeginClass(Modifiers.Public,Name);
		}
		
		public void AddBeginClass(Modifiers modifiers,string Name)
		{
			CurrentClassName = Name;

			if (sb.Length >0) sb.AppendLine();


            if (IsDDLSchema)
            {
                sb.Append("create table ").Append(Name).AppendLine().Append(" (");
                return;
            }

            if (IsSqlData)
            {
                sb.Append("insert into ").Append(Name).Append(" (");
                return;
            }


            if (IsQbMapping)
            {
                sb.Append(ModifiersToString(modifiers)).Append(" class ").Append(CurrentClassName).Append("Repository:QueryBuilder<").Append(CurrentClassName).Append(">")
                  .AppendLine()
                  .Append("{")
                  .AppendLine()
                  .Append(" public ").Append(CurrentClassName).Append("Repository():base(").Append('"').Append(CurrentClassName).Append('"').Append(",").Append('"').Append(CurrentClassName.Substring(0, 1)).Append('"').Append(")")
                  .AppendLine()
                  .Append("{");
                return;
            }




            if (IsEFMapping)
            {

                sb.Append(ModifiersToString(modifiers)).Append(" class ").Append(CurrentClassName).Append("Map:EntityTypeConfiguration<").Append(CurrentClassName).Append(">")
                  .AppendLine()
                  .Append("{")
                  .AppendLine()
                  .Append(" public ").Append(CurrentClassName).Append("Map()")
                  .AppendLine()
                  .Append("{")
                  .AppendLine()
                  .Append("ToTable(\"")
                  .Append(CurrentClassName)
                  .Append("\");");
                return;
            }


			if (IsConfig) return;
			
			if (IsReporting)
			{
				sb.Append("<%BAND " + Name + "%>");
				return;
			}

			if ((Style & CodeStyle.cnet)!=0)
			{
				sb.Append(ModifiersToString(modifiers));
				sb.Append("class ");
				sb.Append(Name);
				if (Ancestor!=null && Ancestor.Length >0)
				{
					sb.Append(" : ");
					sb.Append(Ancestor);
				}

				sb.Append("\r\n{");
				return;
			}

	
				if (IsVB) sb.Append("''");
			    sb.Append(ModifiersToString(modifiers));
			    sb.Append("Class ");
				sb.Append(Name);

				if (Ancestor!=null && Ancestor.Length >0)
				{
					sb.Append("\r\n");
					if (IsVB) sb.Append("''");
					sb.Append(" Inherits ");
					sb.Append(Ancestor);
				}
				return;
		}

		public void AddEndClass()
		{


            if (IsDDLSchema)
            {
                if (IdentityMembers.Count > 0)
                {

                    sb.Append("primary key(");

                    foreach (string pk in IdentityMembers)
                    {
                        sb.Append(pk);
                        sb.Append(',');
                    }

                    sb.Length--;
                    sb.Append(")");
                }
                else
                {
                    sb.Length--;
                }

                sb.AppendLine().Append(")");
                return;

            }


            if (IsSqlData)
            {
                sb.Length--;
                sb.Append(")values(");
                return;
            }



            if (IsQbMapping)
            {

                sb.AppendLine()
                  .Append("}}");
                return;
            }


            if (IsEFMapping)
            {


                if (IdentityMembers.Count == 1)
                {
                      sb.Append("HasKey(x=>x.")
                      .Append(IdentityMembers[0])
                      .Append(");");
                }
                else
                if (IdentityMembers.Count > 0)
                {
                   sb.Append("HasKey(x=>new{");

                    int startLen = sb.Length;

                    foreach (string keyPart in IdentityMembers)
                    {
                        if (sb.Length > startLen)
                        {
                            sb.Append(",");
                        }

                        sb.Append("x.").Append(keyPart);
                    }

                    sb.Append("});");
                }


                sb.AppendLine()
                  .Append("}}");

                return;
            }

			if (IsReporting)
			{
				sb.Append("\r\n<%END%>\r\n");
				return;
			}
			
			if ((Style & CodeStyle.cnet)!=0)
			{
				sb.Append("\r\n}\r\n");
				return;
			}

		
			sb.Append("\r\n");
			if (IsConfig) return;
			if (IsVB) sb.Append("''");
			sb.Append(" End Class \r\n");
		
		}


		private string VBTypeToString(Type FieldType)
		{
			if (FieldType==typeof(bool))    return  "boolean";
			if (FieldType==typeof(int))     return  "integer";
			if (FieldType==typeof(System.DateTime) && IsVB ) return "Date";
			if (FieldType.IsArray) return VBTypeToString(FieldType.GetElementType()) + "()";
			return  TypeToString(FieldType);

		}

		public void AddMember(Type FieldType,string FieldName)
		{
			AddMember(Modifiers.Public,false,FieldType,false,0, FieldName); 
		}

		public void AddMember(Modifiers modifiers,bool hasPrimaryKey, Type FieldType,bool isNullable , int fieldSize ,   string FieldName)
		{

            if (IsDDLSchema)
            {
                sb.AppendLine()
                  .Append(FieldName)
                  .Append(" ")
                  .Append(TypeToSql(FieldType));

                if (FieldType == typeof(string) && fieldSize > 0)
                {
                    sb.Append("(").Append(fieldSize>2000?"max":fieldSize.ToString()).Append(")");
                }


                if (isNullable)
                {
                    sb.Append(" NULL");
                }
                else
                {
                    sb.Append(" NOT NULL");
                }

                sb.Append(",");
                return;
            }


            if (IsSqlData)
            {
                sb.Append(FieldName).Append(",");
                return;
            }


            if (IsQbMapping)
            {
                sb.AppendLine()
                .Append("Property(x=>x.")
                .Append(FieldName)
                .Append(",stored:\"")
                .Append(FieldName)
                .Append('"');

                if (FieldType == typeof(string) && fieldSize > 0)
                {
                     sb.Append(",size:").Append(fieldSize);
                }

                if (hasPrimaryKey)
                {
                    sb.Append(",identity:true");
                }

                sb.Append(");");
                return;
            }


            if (IsEFMapping)
            {
                sb.AppendLine()
                 .Append("Property(x=>x.")
                  .Append(FieldName)
                  .Append(")");

                if (FieldType == typeof(string) && fieldSize > 0)
                {
                    sb.Append(".HasMaxLength(").Append(fieldSize).Append(")");
                }

                 sb.Append(".HasColumnName(\"")
                    .Append(FieldName)
                    .Append("\");");

                return;
            }

			if (IsConfig)
			{
				sb.Append("\r\n    <add key=\"");
				sb.Append(CurrentClassName);
				sb.Append('.');
				sb.Append(FieldName);
				sb.Append("\" value=\"");
				sb.Append(FieldName);
				sb.Append("\" />");
				return;
		
			}

			if (IsReporting)
			{
				sb.Append("\r\n    <%");sb.Append(FieldName);sb.Append("%>");
				return;
			}

			if ((Style&CodeStyle.untyped)!=0) FieldType = typeof(object);

			if ((Style&CodeStyle.cnet)!=0)
			{

				sb.Append("\r\n    ");
				sb.Append(ModifiersToString(modifiers));
				if (TypePrefix!=null && TypePrefix.Length>0) sb.Append(TypePrefix);
				sb.Append(TypeToString(FieldType));

                if (isNullable && !FieldType.IsClass)
                {
                    sb.Append("?");
                }

				if (TypePostfix!=null && TypePostfix.Length>0) sb.Append(TypePostfix);
				sb.Append(' ');
				sb.Append(FieldName);


                if (HasAutoProperty)
                {
                    sb.Append(" {get;set;}");
                }
                else
                {
                    sb.Append(';');
                }

                return;
			}


			sb.Append("\r\n    ");
			sb.Append(ModifiersToString(modifiers));
			sb.Append(FieldName);
			if ((Style&CodeStyle.untyped)!=0 && (Style&CodeStyle.vb)!=0) return;
			sb.Append(" As ");
			if (TypePrefix!=null && TypePrefix.Length>0) sb.Append(TypePrefix);
			sb.Append(VBTypeToString(FieldType));
			if (TypePostfix!=null && TypePostfix.Length>0) sb.Append(TypePostfix);

		}


		public void AddBeginMethod(string Name,params Argument[] Arguments)
		{
			char token = '\0';

			if (IsCNET)
			{

				sb.Append("\r\n\r\n    public void ");
				sb.Append(Name);
				sb.Append('(');
				
				foreach(Argument A in Arguments) 
				{
					if (token=='\0') token = ','; else sb.Append(token); 
	
					sb.Append(TypeToString(A.type));
					sb.Append(' ');
					sb.Append(A.name);

				}
				sb.Append(')');
				sb.Append("\r\n    {");
				return;
			}


			sb.Append("\r\n\r\n    Public Sub ");
			sb.Append(Name);
			sb.Append('(');
			foreach(Argument A in Arguments) 
			{
				if (token=='\0') token = ','; else sb.Append(token); 
				sb.Append(A.name);
				sb.Append(" as ");
				sb.Append(VBTypeToString(A.type));
		
			}
			
			sb.Append(')');


		}

		public void AddCodeLine(params object[] tokens)
		{
			sb.Append("\r\n      ");
			foreach(object token in tokens) sb.Append(token);
			if (IsCNET)  sb.Append(';');

		}

		public void AddEndMethod()
		{
			if (IsCNET) sb.Append("\r\n    }"); else sb.Append("\r\n    End Sub");

		}



        public void AddInsertData(IDataReader reader)
        {
            string insertPattern = sb.ToString();

            sb.Length = 0;

            while (reader.Read())
            {
                sb.AppendLine().Append(insertPattern);


                for (int i = 0; i < reader.FieldCount; i++)
                {

                    if (i > 0)
                    {
                        sb.Append(',');
                    }

                    sb.AppendConstant(reader.GetFieldType(i), reader.GetValue(i));
                }

                sb.Append(')');



            }
        }



		public override string ToString()
		{
			return sb.ToString();
		}


	}
}
