use CompStore;
Go
create or alter procedure dbo.OLTP_to_staging_primary
as
begin
truncate table CompStoreStaging.dbo.city
insert into CompStoreStaging.dbo.city select city_id,country_id,name from CompStore.dbo.City

truncate table CompStoreStaging.dbo.country
insert into CompStoreStaging.dbo.country select country_id,name from CompStore.dbo.country

truncate table CompStoreStaging.dbo.supply_status
insert into CompStoreStaging.dbo.supply_status select supply_status_id,name from CompStore.dbo.supply_status

truncate table CompStoreStaging.dbo.supply
insert into CompStoreStaging.dbo.supply select supply_id,supply_number,amount,supply_date,status_id from CompStore.dbo.supply

truncate table CompStoreStaging.dbo.store
insert into CompStoreStaging.dbo.store select store_id,name,city_id,address from CompStore.dbo.store

truncate table CompStoreStaging.dbo.[order]
insert into CompStoreStaging.dbo.[order] select order_id,order_date,client_id from CompStore.dbo.[order]

truncate table CompStoreStaging.dbo.order_details
insert into CompStoreStaging.dbo.order_details select order_details_id,order_id,store_item_id from CompStore.dbo.order_details

truncate table CompStoreStaging.dbo.computer_device_subtype
insert into CompStoreStaging.dbo.computer_device_subtype select computer_device_subtype_id,name from CompStore.dbo.computer_device_subtype

truncate table CompStoreStaging.dbo.manufacturer
insert into CompStoreStaging.dbo.manufacturer select manufacturer_id,name from CompStore.dbo.manufacturer

truncate table CompStoreStaging.dbo.material
insert into CompStoreStaging.dbo.material select material_id,name from CompStore.dbo.material

truncate table CompStoreStaging.dbo.color
insert into CompStoreStaging.dbo.color select color_id,name from CompStore.dbo.color

truncate table CompStoreStaging.dbo.computer_device
insert into CompStoreStaging.dbo.computer_device select computer_device_id,color_id,material_id,price,manufacturer_id,computer_device_subtype_id,model from CompStore.dbo.computer_device

truncate table CompStoreStaging.dbo.supply_details
insert into CompStoreStaging.dbo.supply_details select supply_details_id,supply_id,computer_device_id,price from CompStore.dbo.supply_details

truncate table CompStoreStaging.dbo.store_item
insert into CompStoreStaging.dbo.store_item select store_item_id,store_id,computer_device_id from CompStore.dbo.store_item

truncate table CompStoreStaging.dbo.shipment
insert into CompStoreStaging.dbo.shipment select shipment_id,shipment_date,supply_details_id,store_item_id,quantity from CompStore.dbo.shipment
end;