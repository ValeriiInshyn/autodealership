-- Create DB
create database AutoDealershipMetadata
go

use AutoDealershipMetadata
go

create table data_load_history(
	id int identity(1,1) not null,
	load_datetime datetime not null,
	load_time time not null,
	load_rows int not null,
	affected_table_count int not null,
	source_table_count int not null,
	primary key(id)
);

create table dwtable (
	id int identity(1,1) not null,
	[name] varchar(50),
	primary key(id),
);

CREATE TABLE dw_table_data_load_history(
    dw_table_id INT NOT NULL,
    data_load_history_id INT NOT NULL,
    PRIMARY KEY (dw_table_id, data_load_history_id),
    FOREIGN KEY (dw_table_id) REFERENCES dwtable(id),
    FOREIGN KEY (data_load_history_id) REFERENCES data_load_history(id)
);

create table dimension(
	id int identity(1,1) not null,
	dw_table_id int not null,
	[key_name] varchar(50),
	[type] varchar(50) null,
	[name] varchar(50),
	primary key(id),
	foreign key(dw_table_id) references dwtable(id)
);

create table dw_attribute_column(
	id int identity(1,1) not null,
	[name] varchar(50) not null,
	datatype varchar(30),
	dwtable_id int not null,
	primary key(id),
	foreign key(dwtable_id) references dwtable(id)
);

create table dimension_attributes(
	dimension_id int not null,
	dw_attribute_column_id int not null,
	primary key(dimension_id, dw_attribute_column_id),
	foreign key(dimension_id) references dimension(id),
	foreign key(dw_attribute_column_id) references dw_attribute_column(id)
);

create table source_db (
	id int identity(1,1) not null,
	[name] varchar(70) not null,
	primary key(id)
);


create table source_table (
	id int identity(1,1) not null,
	source_db_id int not null,
	[key_name] varchar(50),
	[name] varchar(50),
	primary key(id),
	foreign key(source_db_id) references source_db(id)
);

create table source_column (
	id int identity(1,1) not null,
	source_table_id int not null,
	[name] varchar(50),
	data_type varchar(20),
	primary key(id),
	foreign key(source_table_id) references source_table(id)
);

create table fact(
	id int identity(1,1) not null,
	[key_name] varchar(50) not null,
	dw_table_id int not null,
	[type] varchar(50) null,
	primary key(id),
	foreign key(dw_table_id) references dwtable(id)
);

create table fact_metric (
	id int identity(1,1) not null,
	[description] varchar(100) null,
	fact_id int not null,
	dw_attribute_column_id int not null,
	primary key(id),
	foreign key(fact_id) references fact(id),
	foreign key(dw_attribute_column_id) references dw_attribute_column(id)
);

create table transformation(
	id int identity(1,1) not null,
	dw_attribute_column_id int not null,
	[rule] varchar(100) null,
	source_column_id int null,
	primary key(id),
	foreign key(dw_attribute_column_id) references dw_attribute_column(id),
	foreign key(source_column_id) references source_column(id)
);
insert into AutoDealershipMetadata.dbo.source_db(name)
values ('AutoDealership')
--Add fill
Go
create or alter procedure dbo.AutoDealershipMetadataFill
as
begin
begin transaction
begin try
delete from AutoDealershipMetadata.dbo.dw_table_data_load_history
delete from AutoDealershipMetadata.dbo.data_load_history
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.data_load_history',RESEED,1)
delete from AutoDealershipMetadata.dbo.transformation
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.transformation',RESEED,1)
delete from AutoDealershipMetadata.dbo.fact_metric
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.fact_metric',RESEED,1)
delete from AutoDealershipMetadata.dbo.dimension_attributes
delete from AutoDealershipMetadata.dbo.fact
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.fact',RESEED,1)
delete from AutoDealershipMetadata.dbo.dimension
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.dimension',RESEED,1)
delete from AutoDealershipMetadata.dbo.source_column
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.source_column',RESEED,1)
delete from AutoDealershipMetadata.dbo.source_table
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.source_table',RESEED,1)
--delete from AutoDealershipMetadata.dbo.source_db
--DBCC CHECKIDENT('AutoDealershipMetadata.dbo.source_db',RESEED,1)
delete from AutoDealershipMetadata.dbo.dw_attribute_column
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.dw_attribute_column',RESEED,1)
delete from AutoDealershipMetadata.dbo.dwtable
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.dwtable',RESEED,1)


insert into AutoDealershipMetadata.dbo.source_table(source_db_id, name, [key_name])
select 1, t.Table_Name, 
	(select Top(1)
	col.[name] as [key_name]
	from AutoDealership.sys.tables tab
		inner join AutoDealership.sys.indexes pk
			on tab.object_id = pk.object_id 
			and pk.is_primary_key = 1
		inner join AutoDealership.sys.index_columns ic
			on ic.object_id = pk.object_id
			and ic.index_id = pk.index_id
		inner join AutoDealership.sys.columns col
			on pk.object_id = col.object_id
			and col.column_id = ic.column_id
		where tab.name = t.Table_Name)
	as [key_name]
from AutoDealership.INFORMATION_SCHEMA.tables t

insert into AutoDealershipMetadata.dbo.source_column(source_table_id, name, data_type)
select
	(select id from AutoDealershipMetadata.dbo.source_table
	where source_table.name = t.table_name),
	t.column_name,
	t.data_type
from AutoDealership.INFORMATION_SCHEMA.columns t

insert into AutoDealershipMetadata.dbo.dwtable(name)
select
	t.table_name
from AutoDealershipOLAP.INFORMATION_SCHEMA.tables t

insert into AutoDealershipMetadata.dbo.dw_attribute_column(name, datatype, dwtable_id)
select
	c.column_name,
	c.data_type,
	t.id
from AutoDealershipOLAP.INFORMATION_SCHEMA.columns c
inner join AutoDealershipMetadata.dbo.dwtable t on c.table_name = t.name

insert into AutoDealershipMetadata.dbo.fact([key_name], dw_table_id)
select
	(select Top(1)
	col.[name] as [key_name]
	from AutoDealershipOLAP.sys.tables tab
		inner join AutoDealershipOLAP.sys.indexes pk
			on tab.object_id = pk.object_id 
			and pk.is_primary_key = 1
		inner join AutoDealershipOLAP.sys.index_columns ic
			on ic.object_id = pk.object_id
			and ic.index_id = pk.index_id
		inner join AutoDealershipOLAP.sys.columns col
			on pk.object_id = col.object_id
			and col.column_id = ic.column_id
		where tab.name = t.Table_Name)
	as [key_name],
		(select id from AutoDealershipMetadata.dbo.dwtable
		where name = t.table_name)
	as dw_table_id
from AutoDealershipOLAP.INFORMATION_SCHEMA.tables t
where t.table_name LIKE '%fact'

insert into AutoDealershipMetadata.dbo.dimension([key_name], dw_table_id)
select
	(select Top(1)
	col.[name] as [key_name]
	from AutoDealershipOLAP.sys.tables tab
		inner join AutoDealershipOLAP.sys.indexes pk
			on tab.object_id = pk.object_id 
			and pk.is_primary_key = 1
		inner join AutoDealershipOLAP.sys.index_columns ic
			on ic.object_id = pk.object_id
			and ic.index_id = pk.index_id
		inner join AutoDealershipOLAP.sys.columns col
			on pk.object_id = col.object_id
			and col.column_id = ic.column_id
		where tab.name = t.Table_Name)
	as [key_name],
		(select id from AutoDealershipMetadata.dbo.dwtable
		where name = t.table_name)
	as dw_table_id
from AutoDealershipOLAP.INFORMATION_SCHEMA.tables t
where t.table_name LIKE '%dim'

insert into AutoDealershipMetadata.dbo.dimension_attributes(dimension_id, dw_attribute_column_id)
select
		(select TOP(1) di.id from AutoDealershipMetadata.dbo.dimension di
		inner join AutoDealershipMetadata.dbo.dwtable dw on di.dw_table_id = dw.id
		where dw.name = t.table_name),
		(select TOP(1) id from AutoDealershipMetadata.dbo.dw_attribute_column da
		where da.name = t.column_name)
from AutoDealershipOLAP.INFORMATION_SCHEMA.columns t
where t.table_name LIKE '%dim'

insert into AutoDealershipMetadata.dbo.fact_metric(fact_id, dw_attribute_column_id)
select
		(select TOP(1) di.id from AutoDealershipMetadata.dbo.fact di
		inner join AutoDealershipMetadata.dbo.dwtable dw on di.dw_table_id = dw.id
		where dw.name = t.table_name),
		(select TOP(1) id from AutoDealershipMetadata.dbo.dw_attribute_column
		where name = t.column_name)
from AutoDealershipOLAP.INFORMATION_SCHEMA.columns t
where t.table_name LIKE '%fact'

insert into AutoDealershipMetadata.dbo.transformation(dw_attribute_column_id,source_column_id)
select
id,
	(select TOP(1) id from AutoDealershipMetadata.dbo.Source_Column
	where name = t.name)
from AutoDealershipMetadata.dbo.dw_attribute_column t
commit transaction;
end try
begin catch
if @@TRANCOUNT > 0
	rollback transaction;
throw;
end catch;
end;
