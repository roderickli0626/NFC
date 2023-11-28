﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NFC
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="NFC")]
	public partial class MappingDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertAdmin(Admin instance);
    partial void UpdateAdmin(Admin instance);
    partial void DeleteAdmin(Admin instance);
    partial void InsertPlaceAccess(PlaceAccess instance);
    partial void UpdatePlaceAccess(PlaceAccess instance);
    partial void DeletePlaceAccess(PlaceAccess instance);
    partial void InsertPlace(Place instance);
    partial void UpdatePlace(Place instance);
    partial void DeletePlace(Place instance);
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    partial void InsertAccessLog(AccessLog instance);
    partial void UpdateAccessLog(AccessLog instance);
    partial void DeleteAccessLog(AccessLog instance);
    #endregion
		
		public MappingDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MappingDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MappingDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MappingDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Admin> Admins
		{
			get
			{
				return this.GetTable<Admin>();
			}
		}
		
		public System.Data.Linq.Table<PlaceAccess> PlaceAccesses
		{
			get
			{
				return this.GetTable<PlaceAccess>();
			}
		}
		
		public System.Data.Linq.Table<Place> Places
		{
			get
			{
				return this.GetTable<Place>();
			}
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		
		public System.Data.Linq.Table<AccessLog> AccessLogs
		{
			get
			{
				return this.GetTable<AccessLog>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Admin")]
	public partial class Admin : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Email;
		
		private string _Name;
		
		private string _Password;
		
		private string _Note;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    partial void OnNoteChanging(string value);
    partial void OnNoteChanged();
    #endregion
		
		public Admin()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="VarChar(MAX)")]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(MAX)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="VarChar(MAX)")]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Note", DbType="VarChar(MAX)")]
		public string Note
		{
			get
			{
				return this._Note;
			}
			set
			{
				if ((this._Note != value))
				{
					this.OnNoteChanging(value);
					this.SendPropertyChanging();
					this._Note = value;
					this.SendPropertyChanged("Note");
					this.OnNoteChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.PlaceAccess")]
	public partial class PlaceAccess : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private System.Nullable<int> _PlaceID;
		
		private System.Nullable<int> _UserID;
		
		private System.Nullable<System.DateTime> _ExpireDate;
		
		private string _Note;
		
		private EntityRef<Place> _Place;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnPlaceIDChanging(System.Nullable<int> value);
    partial void OnPlaceIDChanged();
    partial void OnUserIDChanging(System.Nullable<int> value);
    partial void OnUserIDChanged();
    partial void OnExpireDateChanging(System.Nullable<System.DateTime> value);
    partial void OnExpireDateChanged();
    partial void OnNoteChanging(string value);
    partial void OnNoteChanged();
    #endregion
		
		public PlaceAccess()
		{
			this._Place = default(EntityRef<Place>);
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PlaceID", DbType="Int")]
		public System.Nullable<int> PlaceID
		{
			get
			{
				return this._PlaceID;
			}
			set
			{
				if ((this._PlaceID != value))
				{
					if (this._Place.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPlaceIDChanging(value);
					this.SendPropertyChanging();
					this._PlaceID = value;
					this.SendPropertyChanged("PlaceID");
					this.OnPlaceIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="Int")]
		public System.Nullable<int> UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExpireDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> ExpireDate
		{
			get
			{
				return this._ExpireDate;
			}
			set
			{
				if ((this._ExpireDate != value))
				{
					this.OnExpireDateChanging(value);
					this.SendPropertyChanging();
					this._ExpireDate = value;
					this.SendPropertyChanged("ExpireDate");
					this.OnExpireDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Note", DbType="VarChar(MAX)")]
		public string Note
		{
			get
			{
				return this._Note;
			}
			set
			{
				if ((this._Note != value))
				{
					this.OnNoteChanging(value);
					this.SendPropertyChanging();
					this._Note = value;
					this.SendPropertyChanged("Note");
					this.OnNoteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Place_PlaceAccess", Storage="_Place", ThisKey="PlaceID", OtherKey="Id", IsForeignKey=true, DeleteRule="CASCADE")]
		public Place Place
		{
			get
			{
				return this._Place.Entity;
			}
			set
			{
				Place previousValue = this._Place.Entity;
				if (((previousValue != value) 
							|| (this._Place.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Place.Entity = null;
						previousValue.PlaceAccesses.Remove(this);
					}
					this._Place.Entity = value;
					if ((value != null))
					{
						value.PlaceAccesses.Add(this);
						this._PlaceID = value.Id;
					}
					else
					{
						this._PlaceID = default(Nullable<int>);
					}
					this.SendPropertyChanged("Place");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_PlaceAccess", Storage="_User", ThisKey="UserID", OtherKey="Id", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.PlaceAccesses.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.PlaceAccesses.Add(this);
						this._UserID = value.Id;
					}
					else
					{
						this._UserID = default(Nullable<int>);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Places")]
	public partial class Place : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _PlaceTitle;
		
		private string _Note;
		
		private EntitySet<PlaceAccess> _PlaceAccesses;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnPlaceTitleChanging(string value);
    partial void OnPlaceTitleChanged();
    partial void OnNoteChanging(string value);
    partial void OnNoteChanged();
    #endregion
		
		public Place()
		{
			this._PlaceAccesses = new EntitySet<PlaceAccess>(new Action<PlaceAccess>(this.attach_PlaceAccesses), new Action<PlaceAccess>(this.detach_PlaceAccesses));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PlaceTitle", DbType="VarChar(MAX)")]
		public string PlaceTitle
		{
			get
			{
				return this._PlaceTitle;
			}
			set
			{
				if ((this._PlaceTitle != value))
				{
					this.OnPlaceTitleChanging(value);
					this.SendPropertyChanging();
					this._PlaceTitle = value;
					this.SendPropertyChanged("PlaceTitle");
					this.OnPlaceTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Note", DbType="VarChar(MAX)")]
		public string Note
		{
			get
			{
				return this._Note;
			}
			set
			{
				if ((this._Note != value))
				{
					this.OnNoteChanging(value);
					this.SendPropertyChanging();
					this._Note = value;
					this.SendPropertyChanged("Note");
					this.OnNoteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Place_PlaceAccess", Storage="_PlaceAccesses", ThisKey="Id", OtherKey="PlaceID")]
		public EntitySet<PlaceAccess> PlaceAccesses
		{
			get
			{
				return this._PlaceAccesses;
			}
			set
			{
				this._PlaceAccesses.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_PlaceAccesses(PlaceAccess entity)
		{
			this.SendPropertyChanging();
			entity.Place = this;
		}
		
		private void detach_PlaceAccesses(PlaceAccess entity)
		{
			this.SendPropertyChanging();
			entity.Place = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[User]")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _UserName;
		
		private string _NFC;
		
		private System.Nullable<int> _Role;
		
		private System.Nullable<System.DateTime> _ExpireDate;
		
		private string _Note;
		
		private EntitySet<PlaceAccess> _PlaceAccesses;
		
		private EntitySet<AccessLog> _AccessLogs;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnUserNameChanging(string value);
    partial void OnUserNameChanged();
    partial void OnNFCChanging(string value);
    partial void OnNFCChanged();
    partial void OnRoleChanging(System.Nullable<int> value);
    partial void OnRoleChanged();
    partial void OnExpireDateChanging(System.Nullable<System.DateTime> value);
    partial void OnExpireDateChanged();
    partial void OnNoteChanging(string value);
    partial void OnNoteChanged();
    #endregion
		
		public User()
		{
			this._PlaceAccesses = new EntitySet<PlaceAccess>(new Action<PlaceAccess>(this.attach_PlaceAccesses), new Action<PlaceAccess>(this.detach_PlaceAccesses));
			this._AccessLogs = new EntitySet<AccessLog>(new Action<AccessLog>(this.attach_AccessLogs), new Action<AccessLog>(this.detach_AccessLogs));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="VarChar(MAX)")]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NFC", DbType="VarChar(MAX)")]
		public string NFC
		{
			get
			{
				return this._NFC;
			}
			set
			{
				if ((this._NFC != value))
				{
					this.OnNFCChanging(value);
					this.SendPropertyChanging();
					this._NFC = value;
					this.SendPropertyChanged("NFC");
					this.OnNFCChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Role", DbType="Int")]
		public System.Nullable<int> Role
		{
			get
			{
				return this._Role;
			}
			set
			{
				if ((this._Role != value))
				{
					this.OnRoleChanging(value);
					this.SendPropertyChanging();
					this._Role = value;
					this.SendPropertyChanged("Role");
					this.OnRoleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExpireDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> ExpireDate
		{
			get
			{
				return this._ExpireDate;
			}
			set
			{
				if ((this._ExpireDate != value))
				{
					this.OnExpireDateChanging(value);
					this.SendPropertyChanging();
					this._ExpireDate = value;
					this.SendPropertyChanged("ExpireDate");
					this.OnExpireDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Note", DbType="VarChar(MAX)")]
		public string Note
		{
			get
			{
				return this._Note;
			}
			set
			{
				if ((this._Note != value))
				{
					this.OnNoteChanging(value);
					this.SendPropertyChanging();
					this._Note = value;
					this.SendPropertyChanged("Note");
					this.OnNoteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_PlaceAccess", Storage="_PlaceAccesses", ThisKey="Id", OtherKey="UserID")]
		public EntitySet<PlaceAccess> PlaceAccesses
		{
			get
			{
				return this._PlaceAccesses;
			}
			set
			{
				this._PlaceAccesses.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_AccessLog", Storage="_AccessLogs", ThisKey="Id", OtherKey="UserID")]
		public EntitySet<AccessLog> AccessLogs
		{
			get
			{
				return this._AccessLogs;
			}
			set
			{
				this._AccessLogs.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_PlaceAccesses(PlaceAccess entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_PlaceAccesses(PlaceAccess entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		
		private void attach_AccessLogs(AccessLog entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_AccessLogs(AccessLog entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.AccessLog")]
	public partial class AccessLog : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _AccessDetail;
		
		private System.Nullable<int> _UserID;
		
		private System.Nullable<System.DateTime> _AccessDate;
		
		private string _Note;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnAccessDetailChanging(string value);
    partial void OnAccessDetailChanged();
    partial void OnUserIDChanging(System.Nullable<int> value);
    partial void OnUserIDChanged();
    partial void OnAccessDateChanging(System.Nullable<System.DateTime> value);
    partial void OnAccessDateChanged();
    partial void OnNoteChanging(string value);
    partial void OnNoteChanged();
    #endregion
		
		public AccessLog()
		{
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccessDetail", DbType="VarChar(MAX)")]
		public string AccessDetail
		{
			get
			{
				return this._AccessDetail;
			}
			set
			{
				if ((this._AccessDetail != value))
				{
					this.OnAccessDetailChanging(value);
					this.SendPropertyChanging();
					this._AccessDetail = value;
					this.SendPropertyChanged("AccessDetail");
					this.OnAccessDetailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="Int")]
		public System.Nullable<int> UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccessDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> AccessDate
		{
			get
			{
				return this._AccessDate;
			}
			set
			{
				if ((this._AccessDate != value))
				{
					this.OnAccessDateChanging(value);
					this.SendPropertyChanging();
					this._AccessDate = value;
					this.SendPropertyChanged("AccessDate");
					this.OnAccessDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Note", DbType="VarChar(MAX)")]
		public string Note
		{
			get
			{
				return this._Note;
			}
			set
			{
				if ((this._Note != value))
				{
					this.OnNoteChanging(value);
					this.SendPropertyChanging();
					this._Note = value;
					this.SendPropertyChanged("Note");
					this.OnNoteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_AccessLog", Storage="_User", ThisKey="UserID", OtherKey="Id", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.AccessLogs.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.AccessLogs.Add(this);
						this._UserID = value.Id;
					}
					else
					{
						this._UserID = default(Nullable<int>);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
