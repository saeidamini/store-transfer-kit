CREATE TABLE factor(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    title	        varchar(255) 	null, 
    code	        varchar(255)	not	null,
    totalPrice	            decimal(10) null,
    totalDiscountPrice	decimal(10) null,
    totalTaxAddedValue 	decimal(10) null,
    amountPaid	    decimal(10)		null,
    recycled	    boolean		    null,
    numberItems    integer(10)		null,
    salesDate	    datetime		null,
    customer_id	    bigint(19)		null
)

      

-- Must be Filed :
-- STORE_PERCENT	REAL(7)		NULL
-- CREATE_DATE	    TIMESTAMP(26)		NULL
-- PLACE_ID	    BIGINT(19)		NULL
-- CONTRACT_ID     BIGINT(19)		NULL




-- show columns FROM customer 
CREATE TABLE customer(
    id integer primary key autoincrement,
    lastName	        varchar(255)  not	null,
    code	        varchar(255)  null,
    address	        varchar(255)  null,
    nationalCode	varchar(255)  null,
    birthDate	    date  null,
    marriedDate	date  null,
    mobile	        varchar(255)  null,
    firstName	    varchar(255)  null,
    email	        varchar(255)  null,
    telephone	    varchar(255)  null,
    married	        boolean  null
)

INSERT INTO factor(code ,amountPaid ,recycled ,numberItems ,salesDate ,customer_id)
  VALUES('44Test',5670000, 0, 12,'2019-01-23 12:34',1)

INSERT INTO customer(lastName, code, address, marriedDate, mobile, firstName, birthDate)
  VALUES('Ahmadi', '9701','Niavaran', '2008-08-09','09129873445','Nima', '1998-02-02')


