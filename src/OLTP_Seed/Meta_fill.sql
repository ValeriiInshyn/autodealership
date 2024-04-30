use AutoDealershipMetadata;
Go
create or alter procedure dbo.CompStoreMetadataFill
as
begin
begin transaction
begin try
delete from AutoDealershipMetadata.dbo.dw_table_data_load_history
delete from AutoDealershipMetadata.dbo.data_load_history
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.data_load_history',RESEED,0)
delete from AutoDealershipMetadata.dbo.transformation
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.transformation',RESEED,0)
delete from AutoDealershipMetadata.dbo.fact_metric
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.fact_metric',RESEED,0)
delete from AutoDealershipMetadata.dbo.dimension_attributes
delete from AutoDealershipMetadata.dbo.fact
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.fact',RESEED,0)
delete from AutoDealershipMetadata.dbo.dimension
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.dimension',RESEED,0)
delete from AutoDealershipMetadata.dbo.source_column
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.source_column',RESEED,0)
delete from AutoDealershipMetadata.dbo.source_table
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.source_table',RESEED,0)
delete from AutoDealershipMetadata.dbo.source_db
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.source_db',RESEED,0)
delete from AutoDealershipMetadata.dbo.dw_attribute_column
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.dw_attribute_column',RESEED,0)
delete from AutoDealershipMetadata.dbo.dwtable
DBCC CHECKIDENT('AutoDealershipMetadata.dbo.dwtable',RESEED,0)

insert into AutoDealershipMetadata.dbo.source_db(name)
values ('AutoDealership')

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
