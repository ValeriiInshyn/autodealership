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