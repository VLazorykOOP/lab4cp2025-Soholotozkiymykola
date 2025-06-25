create database breads;
use breads;

create table bread(
	id INT AUTO_INCREMENT PRIMARY KEY,
    type varchar(100) not null,
    sort varchar(100) not null,
    material date not null,
    supplier int not null,
    expiration_date varchar(100) not null,
    price double not null,
	amount int default 50
);