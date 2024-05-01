use CompStore;
Go
create or alter procedure dbo.OLTP_to_staging_incremental
as
begin
DECLARE @lastloaddate datetime;
SET @lastloaddate = (select MAX(load_datetime) from CompStoreMetadata.dbo.data_load_history)

truncate table CompStoreStaging.dbo.shipment
insert into CompStoreStaging.dbo.shipment 
select shipment_id,shipment_date,supply_details_id,store_item_id,quantity 
from CompStore.dbo.shipment
where create_date > @lastloaddate or update_date > @lastloaddate

truncate table CompStoreStaging.dbo.supply_details
insert into CompStoreStaging.dbo.supply_details 
select * from
(
	(
		select supply_details_id,supply_id,computer_device_id,price 
		from CompStore.dbo.supply_details
		where create_date > @lastloaddate or update_date > @lastloaddate
	)
	union
	(
		select supply_details_id,supply_id,computer_device_id,price 
		from CompStore.dbo.supply_details
		where supply_details_id in (select supply_details_id from CompStoreStaging.dbo.shipment)
	)
) as agr

truncate table CompStoreStaging.dbo.supply
insert into CompStoreStaging.dbo.supply 
select * from
(
	(
		select supply_id,supply_number,amount,supply_date,status_id 
		from CompStore.dbo.supply
		where create_date > @lastloaddate or update_date > @lastloaddate
	)
	union
	(
		select supply_id,supply_number,amount,supply_date,status_id 
		from CompStore.dbo.supply
		where supply_id in (select supply_id from CompStoreStaging.dbo.supply_details)
	)
) as agr

truncate table CompStoreStaging.dbo.supply_status
insert into CompStoreStaging.dbo.supply_status 
select * from
(
	(
		select supply_status_id,name 
		from CompStore.dbo.supply_status ss
		where create_date > @lastloaddate or update_date > @lastloaddate
	)
	union
	(
		select supply_status_id,name
		from CompStore.dbo.supply_status 
		where supply_status_id in (select supply_status_id from CompStoreStaging.dbo.supply)
	)
) as agr

truncate table CompStoreStaging.dbo.[order]
insert into CompStoreStaging.dbo.[order] 
select order_id,order_date,client_id 
from CompStore.dbo.[order]
where create_date > @lastloaddate or update_date > @lastloaddate

truncate table CompStoreStaging.dbo.order_details
insert into CompStoreStaging.dbo.order_details 
select order_details_id,od.order_id,store_item_id 
from CompStore.dbo.order_details od
join CompStore.dbo.[order] o on od.order_id = o.order_id
where o.create_date > @lastloaddate or o.update_date > @lastloaddate

truncate table CompStoreStaging.dbo.store_item
insert into CompStoreStaging.dbo.store_item 
select * from
(
	(
		select store_item_id,store_id,computer_device_id 
		from CompStore.dbo.store_item
		where create_date > @lastloaddate or update_date > @lastloaddate
	)
	union
	(
		select store_item_id,store_id,computer_device_id 
		from CompStore.dbo.store_item
		where store_item_id in (select store_item_id from CompStoreStaging.dbo.order_details)
	)
	union
	(
		select store_item_id,store_id,computer_device_id 
		from CompStore.dbo.store_item
		where store_item_id in (select store_item_id from CompStoreStaging.dbo.shipment)

	)
) as agr

truncate table CompStoreStaging.dbo.computer_device
insert into CompStoreStaging.dbo.computer_device 
select * from
(
	(
		select computer_device_id,color_id,material_id,price,manufacturer_id,computer_device_subtype_id,model 
		from CompStore.dbo.computer_device
		where create_date > @lastloaddate or update_date > @lastloaddate
	)
	union
	(
		select computer_device_id,color_id,material_id,price,manufacturer_id,computer_device_subtype_id,model 
		from CompStore.dbo.computer_device
		where computer_device_id in (select computer_device_id from CompStoreStaging.dbo.store_item)
	)
	union
	(
		select computer_device_id,color_id,material_id,price,manufacturer_id,computer_device_subtype_id,model 
		from CompStore.dbo.computer_device
		where computer_device_id in (select computer_device_id from CompStoreStaging.dbo.supply_details)
	)
) as agr

truncate table CompStoreStaging.dbo.color
insert into CompStoreStaging.dbo.color 
select * from
(
	(
	select color_id,name 
	from CompStore.dbo.color
	where create_date > @lastloaddate or update_date > @lastloaddate
	)
	union
	(
	select color_id,name 
	from CompStore.dbo.color
	where color_id in (select color_id from CompStoreStaging.dbo.computer_device)
	)
) as agr


truncate table CompStoreStaging.dbo.material
insert into CompStoreStaging.dbo.material 
select * from
(
	(
	select material_id,name 
	from CompStore.dbo.material
	where create_date > @lastloaddate or update_date > @lastloaddate
	)
	union
	(
	select material_id,name 
	from CompStore.dbo.material
	where material_id in (select material_id from CompStoreStaging.dbo.computer_device)
	)
) as agr



truncate table CompStoreStaging.dbo.manufacturer
insert into CompStoreStaging.dbo.manufacturer 
select * from
(
	(
	select manufacturer_id,name 
	from CompStore.dbo.manufacturer
	where create_date > @lastloaddate or update_date > @lastloaddate
	)
	union
	(
	select manufacturer_id,name 
	from CompStore.dbo.manufacturer
	where manufacturer_id in (select manufacturer_id from CompStoreStaging.dbo.computer_device)
	)
) as agr

truncate table CompStoreStaging.dbo.computer_device_subtype
insert into CompStoreStaging.dbo.computer_device_subtype 
select * from
(
	(
	select computer_device_subtype_id,name 
	from CompStore.dbo.computer_device_subtype
	where create_date > @lastloaddate or update_date > @lastloaddate
	)
	union
	(
	select computer_device_subtype_id,name 
	from CompStore.dbo.computer_device_subtype
	where computer_device_subtype_id in (select computer_device_subtype_id from CompStoreStaging.dbo.computer_device)
	)
) as agr

truncate table CompStoreStaging.dbo.store
insert into CompStoreStaging.dbo.store 
select * from
(
	(
	select store_id,name,city_id,address 
	from CompStore.dbo.store
	where create_date > @lastloaddate or update_date > @lastloaddate
	)
	union
	(
	select store_id,name,city_id,address 
	from CompStore.dbo.store
	where store_id in (select store_id from CompStoreStaging.dbo.store_item)
	)
) as agr

truncate table CompStoreStaging.dbo.city
insert into CompStoreStaging.dbo.city
select * from
(
	(
	select city_id,country_id,name 
	from CompStore.dbo.city
	where create_date > @lastloaddate or update_date > @lastloaddate
	)
	union
	(
	select city_id,country_id,name 
	from CompStore.dbo.city
	where city_id in (select city_id from CompStoreStaging.dbo.store)
	)
) as agr

truncate table CompStoreStaging.dbo.country
insert into CompStoreStaging.dbo.country
select * from
(
	(
	select country_id,name 
	from CompStore.dbo.country
	where create_date > @lastloaddate or update_date > @lastloaddate
	)
	union
	(
	select country_id,name 
	from CompStore.dbo.country
	where country_id in (select country_id from CompStoreStaging.dbo.city)
	)
) as agr
end;