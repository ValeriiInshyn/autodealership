use CompStoreStorage;
Go
create or alter procedure dbo.staging_to_OLAP_primary
as
begin
-- Clear DW
TRUNCATE TABLE CompStoreStorage.dbo.shipment_fact
DBCC CHECKIDENT('CompStoreStorage.dbo.shipment_fact',RESEED,0)
TRUNCATE TABLE CompStoreStorage.dbo.order_details_fact
DBCC CHECKIDENT('CompStoreStorage.dbo.order_details_fact',RESEED,0)
delete from CompStoreStorage.dbo.computer_device_dim
DBCC CHECKIDENT('CompStoreStorage.dbo.computer_device_dim',RESEED,0)
delete from CompStoreStorage.dbo.supply_dim
DBCC CHECKIDENT('CompStoreStorage.dbo.supply_dim',RESEED,0)
delete from CompStoreStorage.dbo.store_dim
DBCC CHECKIDENT('CompStoreStorage.dbo.store_dim',RESEED,0)
delete from CompStoreStorage.dbo.device_color_dim
DBCC CHECKIDENT('CompStoreStorage.dbo.device_color_dim',RESEED,0)
delete from CompStoreStorage.dbo.date_dim
DBCC CHECKIDENT('CompStoreStorage.dbo.date_dim',RESEED,0)

-- Job variables
DECLARE @rowcount int;
SET @rowcount = 0;
DECLARE @dwtablecount int;
SET @dwtablecount = 0;
DECLARE @dbtablecount int;
SET @dbtablecount = 0;
DECLARE @tempcount int;
SET @tempcount = 0;
DECLARE @startdate datetime;
SET @startdate = GETDATE();
DECLARE @ident int;
SET @ident = IDENT_CURRENT('CompStoreMetadata.dbo.data_load_history') + 1;
DECLARE @metastring varchar(MAX);
SET @metastring = '';

-- Fill DW
insert into CompStoreStorage.dbo.date_dim(year, month, day) 
    (select distinct YEAR(t.chosen_date) as year, MONTH(t.chosen_date) as month, DAY(t.chosen_date) as day from 
        (select supply_date as chosen_date from CompStoreStaging.dbo.supply 
		union select shipment_date from CompStoreStaging.dbo.shipment) as t)
union
    (select distinct YEAR(t.chosen_date) as year, MONTH(t.chosen_date) as month, 1 as day from 
    (select order_date as chosen_date from CompStoreStaging.dbo.[order]) as t
    where 
    (select COUNT(date_id) from CompStoreStorage.dbo.date_dim dd
    where DAY(t.chosen_date)=dd.day and MONTH(t.chosen_date)=dd.month and YEAR(t.chosen_date)=dd.year)=0)
union
    (select distinct YEAR(DATEADD(MONTH, 1, t.chosen_date)) as year, MONTH(DATEADD(MONTH, 1, t.chosen_date)) as month, 1 as day from 
    (select order_date as chosen_date from CompStoreStaging.dbo.[order]) as t
    where 
    (select COUNT(date_id) from CompStoreStorage.dbo.date_dim dd
    where DAY(t.chosen_date)=dd.day and MONTH(t.chosen_date)=dd.month and YEAR(t.chosen_date)=dd.year)=0)

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+3;
        SET @metastring = @metastring + 'insert into CompStoreMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(1,' + CONVERT(varchar(MAX),@ident) + ');'
    END

insert into CompStoreStorage.dbo.device_color_dim(name, bk_color_id)
select name, color_id as bk_color_id from CompStoreStaging.dbo.color

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+1;
        SET @metastring = @metastring + 'insert into CompStoreMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(2,' + CONVERT(varchar(MAX),@ident) + ');'
    END

insert into CompStoreStorage.dbo.store_dim(name, city, country, address, br_store_id)
select s.name as name, ct.name as city, cntr.name as country, address, store_id as br_store_id 
from CompStoreStaging.dbo.store s
join CompStoreStaging.dbo.city ct on s.city_id = ct.city_id
join CompStoreStaging.dbo.country cntr on ct.country_id = cntr.country_id

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+3;
        SET @metastring = @metastring + 'insert into CompStoreMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(3,' + CONVERT(varchar(MAX),@ident) + ');'
    END

insert into CompStoreStorage.dbo.computer_device_dim(color_id, material, price, manufacturer, computer_device_subtype, model, bk_computer_device_id)
select 
	c_dim.color_id as color_id,
	mt.name as material,
	cd.price as price,
	mf.name as manufacturer,
	cds.name as computer_device_subtype,
	cd.model as model,
	cd.computer_device_id as bk_computer_device_id
from CompStoreStaging.dbo.computer_device cd
join CompStoreStorage.dbo.device_color_dim c_dim on cd.color_id = c_dim.bk_color_id
join CompStoreStaging.dbo.material mt on cd.material_id = mt.material_id
join CompStoreStaging.dbo.manufacturer mf on cd.manufacturer_id = mf.manufacturer_id
join CompStoreStaging.dbo.computer_device_subtype cds on cd.computer_device_subtype_id = cds.computer_device_subtype_id


SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+4;
        SET @metastring = @metastring + 'insert into CompStoreMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(5,' + CONVERT(varchar(MAX),@ident) + ');'
    END

insert into CompStoreStorage.dbo.supply_dim(supply_number, amount, supply_date_id, status, bk_supply_id)
select
	s.supply_number as supply_number,
	s.amount as amount,
	dd.date_id as supply_date_id,
	ss.name as status,
	s.supply_id as bk_supply_id
from CompStoreStaging.dbo.supply s
join CompStoreStaging.dbo.supply_status ss on s.status_id = ss.supply_status_id
join CompStoreStorage.dbo.date_dim dd on
	DAY(s.supply_date)=dd.day and MONTH(s.supply_date)=dd.month and YEAR(s.supply_date)=dd.year

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+1;
        SET @metastring = @metastring + 'insert into CompStoreMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(6,' + CONVERT(varchar(MAX),@ident) + ');'
    END

insert into CompStoreStorage.dbo.shipment_fact(supply_id, store_id, computer_device_id, shipment_date_id, previous_shipment_date_id, shipment_period, price, quantity, fraction_of_supply_cost)
select
s_dim.supply_id as supply_id,
st_dim.store_id as store_id,
c_dim.computer_device_id as computer_device_id,
dd.date_id as shipmet_date_id,
	(select TOP(1)
		dd2.date_id
	from 
		(select 
			COALESCE(
				MAX(sh2.shipment_date),
				sh.shipment_date
			) as prev_ship_date
		from CompStoreStaging.dbo.shipment sh2
		join CompStoreStaging.dbo.store_item si2 on sh2.store_item_id = si2.store_item_id
		join CompStoreStaging.dbo.supply_details sd2 on sh2.supply_details_id = sd2.supply_details_id
		where si2.store_id = si.store_id
			AND sh2.shipment_date < sh.shipment_date) as psd
	join CompStoreStorage.dbo.date_dim dd2 on
	DAY(psd.prev_ship_date)=dd2.day and MONTH(psd.prev_ship_date)=dd2.month and YEAR(psd.prev_ship_date)=dd2.year) as previous_shipment_date_id,

	(select DATEDIFF(day, COALESCE(MAX(sh3.shipment_date), sh.shipment_date), sh.shipment_date)
		from CompStoreStaging.dbo.shipment sh3
		join CompStoreStaging.dbo.store_item si3 on sh3.store_item_id = si3.store_item_id
		join CompStoreStaging.dbo.supply_details sd3 on sh3.supply_details_id = sd3.supply_details_id
		where si3.store_id = si.store_id
			AND sh3.shipment_date < sh.shipment_date) as shipment_period,
sd.price as price,
sh.quantity as quantity,
(sd.price * sh.quantity * 100) / s_dim.amount as fraction_of_supply_cost

from CompStoreStaging.dbo.shipment sh
join CompStoreStaging.dbo.supply_details sd on sh.supply_details_id = sd.supply_details_id
join CompStoreStorage.dbo.supply_dim s_dim on sd.supply_id = s_dim.bk_supply_id
join CompStoreStaging.dbo.store_item si on sh.store_item_id = si.store_item_id
join CompStoreStorage.dbo.store_dim st_dim on si.store_id = st_dim.br_store_id
join CompStoreStorage.dbo.computer_device_dim c_dim on sd.computer_device_id = c_dim.bk_computer_device_id
join CompStoreStorage.dbo.date_dim dd on
	DAY(sh.shipment_date)=dd.day and MONTH(sh.shipment_date)=dd.month and YEAR(sh.shipment_date)=dd.year

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+2;
        SET @metastring = @metastring + 'insert into CompStoreMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(7,' + CONVERT(varchar(MAX),@ident) + ');'
    END


insert into CompStoreStorage.dbo.order_details_fact(color_id, store_id, start_period_date_id, end_period_date_id, order_count, unique_client_count)
select
c_dim.color_id as color_id,
s_dim.store_id as store_id,
sd.date_id as start_period_date_id,
ed.date_id as end_period_date_id,
count(distinct od.order_id) as order_count,
count(distinct o.client_id) as unique_client_count
from CompStoreStaging.dbo.order_details od
join CompStoreStaging.dbo.[order] o on od.order_id = o.order_id
join CompStoreStaging.dbo.store_item si on od.store_item_id = si.store_item_id
join CompStoreStaging.dbo.computer_device cd on si.computer_device_id = cd.computer_device_id
join CompStoreStorage.dbo.device_color_dim c_dim on cd.color_id = c_dim.bk_color_id
join CompStoreStorage.dbo.store_dim s_dim on si.store_id = s_dim.br_store_id
join CompStoreStorage.dbo.date_dim sd on
1=sd.day and MONTH(o.order_date)=sd.month and YEAR(o.order_date)=sd.year
join CompStoreStorage.dbo.date_dim ed on
1=ed.day and MONTH(DATEADD(MONTH, 1, o.order_date))=ed.month and YEAR(DATEADD(MONTH, 1, o.order_date))=ed.year
group by s_dim.store_id, c_dim.color_id, sd.date_id, ed.date_id

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+1;
        SET @metastring = @metastring + 'insert into CompStoreMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(4,' + CONVERT(varchar(MAX),@ident) + ');'
    END

-- Fill Metadata
insert into CompStoreMetadata.dbo.data_load_history(load_datetime,load_time,load_rows,affected_table_count,source_table_count)
values(GETDATE(), CONVERT(TIME(7), GETDATE()),@rowcount,@dwtablecount,@dbtablecount)
EXEC(@metastring)

-- Clear Staging
TRUNCATE TABLE CompStoreStaging.dbo.city;
TRUNCATE TABLE CompStoreStaging.dbo.country;
TRUNCATE TABLE CompStoreStaging.dbo.supply_status;
TRUNCATE TABLE CompStoreStaging.dbo.supply;
TRUNCATE TABLE CompStoreStaging.dbo.store;
TRUNCATE TABLE CompStoreStaging.dbo.[order];
TRUNCATE TABLE CompStoreStaging.dbo.computer_device_subtype;
TRUNCATE TABLE CompStoreStaging.dbo.manufacturer;
TRUNCATE TABLE CompStoreStaging.dbo.material;
TRUNCATE TABLE CompStoreStaging.dbo.color;
TRUNCATE TABLE CompStoreStaging.dbo.computer_device;
TRUNCATE TABLE CompStoreStaging.dbo.supply_details;
TRUNCATE TABLE CompStoreStaging.dbo.store_item;
TRUNCATE TABLE CompStoreStaging.dbo.shipment;
end;