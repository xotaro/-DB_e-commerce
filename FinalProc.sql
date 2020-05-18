use f100;
go
create proc showDeals 
as
select* from Todays_Deals
go

go
create proc showDealsOnProduct
as
select* from Todays_Deals_Product

--page 1,2&recommend trial
go
CREATE PROCEDURE customerRegister
@username varchar(20),@first_name varchar(20), @last_name varchar(20),
@password varchar(20),@email varchar(50)
as
INSERT INTO Users(username,first_name,last_name,password,email)
values(@username,@first_name,@last_name,@password,@email)
INSERT INTO Customer(username)
values(@username)
go


go
CREATE PROCEDURE vendorRegister
@username varchar(20),@first_name varchar(20), @last_name varchar(20),
@password varchar(20),@email varchar(50),@company_name varchar(20),
@bank_acc_no varchar(20)
as
INSERT INTO Users(username,first_name,last_name,password,email)
values(@username,@first_name,@last_name,@password,@email)
INSERT INTO Vendors(username,company_name,bank_acc_no)
values(@username,@company_name,@bank_acc_no)
go


go
CREATE PROCEDURE userLogin 
@username varchar(20),@password varchar(20),
@success bit output ,@type int output
as
set @success=(
select count(1)
from Users
where username=@username and password=@password
)
declare @incustomer bit=0
declare @invendor bit=0
declare @inadmin bit=0
declare @indelivery bit=0
declare @success1 char(1)='0'
set @incustomer=(
select count(1)
from Customer
where username=@username 
)

set @invendor=(
select count(1)
from Vendors
where username=@username 
)
set @inadmin=(
select count(1)
from Admins
where username=@username 
)
set @indelivery=(
select count(1)
from Delivery_Person
where username=@username 
)
set @type=-1
if(@incustomer=1)
	set @type=0

if(@invendor=1)
	set @type=1

if(@inadmin=1)
	set @type=2

if(@indelivery=1)
	set @type=3

if(@success=1)
	set @success1='1'

go

create proc showMobile
as
select * from user_mobile_numbers
go
create procedure addMobile
@username varchar(20),
@mobile_number varchar(20)
as
insert into user_mobile_numbers(username,mobile_number)
values(@username,@mobile_number)
go

go
create procedure addAddress
@username varchar(20),@address varchar(100)
as
insert into User_Addresses(username,address)
values(@username,@address)

go


go
create PROCEDURE showProducts
as 
select *
from Product
where available=1
go




create procedure ShowProductsbyPrice
as
select*
from Product
where available=1
order by Product.final_price
go



create procedure searchbyname
@text varchar(20)
as 
select *
from Product
where product_name like @text+'%'
go


go

create procedure AddQuestion
@serial int,@customer varchar(20),@question varchar(50)
as
insert into Customer_Question_Product(serial_no,customer_name,question)
values(@serial,@customer,@question)

go



go

create procedure addToCart
@customername varchar(20), @serial int
as
insert into CustomerAddstoCartProduct(customer_name,serial_no)
values(@customername,@serial)

go

go



go

create proc removefromCart
@customername varchar(20),@serial int
as
delete 
from CustomerAddstoCartProduct
where customer_name= @customername and serial_no=@serial

go



create proc createWishlist
@customername varchar(20),@name varchar(20)
as
insert into Wishlist(username,name)
values(@customername,@name)


go


go


create proc AddtoWishlist
@customername varchar(20),@wishlistname varchar(20),@serial int	
as
insert into Wishlist_Product
values(@customername,@wishlistname,@serial)

go


go

create proc removefromWishlist
@customername varchar(20),@wishlistname varchar(20),@serial int	
as
DELETE
FROM Wishlist_Product
where username=@customername and serial_no=@serial 
and wish_name=@wishlistname


go

--14


go
----------------------------------------------------------------------------
--DANIELS PART FROM HERE
GO

create Proc showWishlistProduct
@customername VARCHAR (20),
@name VARCHAR(20)

AS
Select p.serial_no, p.product_name, p.category, p.product_description, p.price, p.final_price, p.color, p.available, p.rate, p.vendor_username, p.customer_username, p.customer_order_id from product p
INNER JOIN Wishlist_Product wp on p.serial_no = wp.serial_no

where wp.username = @customername and wp.wish_name = @name




GO

create PROC viewMyCart
@customer varchar(20)
AS
Select p.serial_no, p.product_name, p.category, p.product_description, p.price, p.final_price, p.color, p.available, p.rate, p.vendor_username, p.customer_username, p.customer_order_id from product p
inner join CustomerAddstoCartProduct cp on p.serial_no = cp.serial_no

where cp.customer_name = @customer




GO
create proc calculatepriceOrder
@customername varchar(20),
@result decimal(10,2) output
AS

select @result = sum(p.final_price) from product p
	inner join CustomerAddstoCartProduct cp on p.serial_no = cp.serial_no
	
where cp.customer_name = @customername



GO


create proc emptyCart
@customername varchar(20)
AS
delete from CustomerAddstoCartProduct where customer_name = @customername



Go

create proc productsinorder
@customername varchar(20),
@orderID int
AS
update product
set customer_order_id=@orderID,available=0 
where serial_no in (select serial_no from CustomerAddstoCartProduct where @customername = customer_name)

update product
set customer_username = @customername
where serial_no in (select serial_no from CustomerAddstoCartProduct where @customername = customer_name)
delete from CustomerAddstoCartProduct where
customer_name <> @customername AND serial_no in (select serial_no from CustomerAddstoCartProduct 
where @customername = customer_name)




GO


GO
create proc makeOrder
@customername varchar(20)
AS
declare @sum decimal(10,2)
exec calculatepriceOrder @customername, @sum output
insert into Orders(customer_name, total_amount,order_status)values(@customername, @sum, 'not processed')
declare @id Smallint
set @id = (select top(1)order_no from Orders order by order_no DESC)
exec productsinorder @customername, @id
exec emptyCart @customername






GO

create proc ShowproductsIbought
@customername varchar(20)
AS
Select p.serial_no, p.product_name, p.category, p.product_description, p.price, p.final_price, p.color, p.available, p.rate, p.vendor_username, p.customer_username, p.customer_order_id from product p
where customer_username = @customername


GO

create proc rate
@serialno int,
@rate int,
@customername varchar(20)
AS
update product
set rate = @rate where serial_no = @serialno and customer_username = @customername

GO
create proc SpecifyAmount
@customername varchar(20),
@orderID int,
@cash decimal(10,2),
@credit decimal(10,2)
AS
begin
if (select total_amount from Orders where order_no = @orderID) = @credit 
Begin
update Orders
set credit_amount = @credit where order_no = @orderID
update Orders
set payment_type = 'credit' where order_no = @orderID
END
Else if (select total_amount from Orders where order_no = @orderID) = @cash
begin
update Orders
set cash_amount = @cash where order_no = @orderID
update Orders
set payment_type = 'cash' where order_no = @orderID
End
else if (@cash = NULL or @cash = 0) and (select total_amount from Orders where order_no = @orderID) > @credit
begin
if (select points from Customer where username = @customername) + @credit < (select total_amount from Orders where order_no = @orderID)
begin
print('not enough credit and points')
end
else
begin
declare @code varchar(20)
set @code = (select top(1)code from Admin_Customer_Giftcard 
where customer_name = @customername and remaining_points >= ((select total_amount from Orders where order_no = @orderID) - @credit))
update Admin_Customer_Giftcard 
set remaining_points = remaining_points - ((select total_amount from Orders where order_no = @orderID) - @credit)
where code = @code and customer_name = @customername
update Customer
set points = points - ((select total_amount from Orders where order_no = @orderID) - @credit)
where username = @customername
update Orders
set credit_amount = @credit where order_no = @orderID
update Orders
set payment_type = 'credit' where order_no = @orderID
update Orders
set Gift_Card_code_used = @code where order_no = @orderID
end
end
else if (@credit = NULL or @credit = 0) and (select total_amount from Orders where order_no = @orderID) > @cash
begin
if (select points from Customer where username = @customername) + @cash < (select total_amount from Orders where order_no = @orderID)
begin
print('not enough cash and points')
end
else
begin
declare @code2 varchar(20)
set @code2 = (select top(1)code from Admin_Customer_Giftcard 
where customer_name = @customername and remaining_points >= ((select total_amount from Orders where order_no = @orderID) - @cash))
update Admin_Customer_Giftcard 
set remaining_points = remaining_points - ((select total_amount from Orders where order_no = @orderID) - @cash)
where code = @code2 and customer_name = @customername
update Customer
set points = points - ((select total_amount from Orders where order_no = @orderID) - @cash)
where username = @customername
update Orders
set cash_amount = @cash where order_no = @orderID
update Orders
set payment_type = 'cash' where order_no = @orderID
update Orders
set Gift_Card_code_used = @code2 where order_no = @orderID
end
end
end



Go


GO
create proc AddCreditCard
@creditcardnumber varchar(20),
@expirydate date,
@cvv varchar(4),
@customername varchar(20)
AS
insert into Credit_Card values(@creditcardnumber, @expirydate, @cvv)
insert into Customer_CreditCard values(@customername, @creditcardnumber)


go
create proc ChooseCreditCard
@creditcard varchar(20),
@orderid int
AS
update orders
set creditCard_number = @creditcard where order_no = @orderid





Go

create proc cancelOrder
@orderid int
AS
if (select order_status from Orders where order_no = @orderid) in ('not processed', 'in process')
Begin
if (select payment_type from Orders where order_no = @orderid) = 'credit' and ((select credit_amount from Orders where order_no = @orderid) < (select total_amount from Orders where order_no = @orderid))
begin
if (Select Gift_Card_code_used from Orders where order_no = @orderid) Is not null
begin
update Admin_Customer_Giftcard
set remaining_points = remaining_points + ((select total_amount from Orders where order_no = @orderid) - (select credit_amount from Orders where order_no = @orderid))
where code = (select Gift_Card_code_used from Orders where order_no = @orderid) and customer_name = (select customer_name from Orders where order_no = @orderid)
update Customer
set points = points + ((select total_amount from Orders where order_no = @orderid) - (select credit_amount from Orders where order_no = @orderid))
where username = (select customer_name from Orders where order_no = @orderid) 
end
update Product
set available = 1 where customer_order_id = @orderid
update Product
set customer_username = null where customer_order_id = @orderid
update Product
set customer_order_id = null where customer_order_id = @orderid
delete from Orders where order_no = @orderid
end
else if (select payment_type from Orders where order_no = @orderid) = 'cash' and ((select cash_amount from Orders where order_no = @orderid) < (select total_amount from Orders where order_no = @orderid))
begin

if (Select Gift_Card_code_used from Orders where order_no = @orderid) is not NULL
begin

update Admin_Customer_Giftcard
set remaining_points = remaining_points + ((select total_amount from Orders where order_no = @orderid) - (select cash_amount from Orders where order_no = @orderid))
where code = (select Gift_Card_code_used from Orders where order_no = @orderid) and customer_name = (select customer_name from Orders where order_no = @orderid)
update Customer
set points = points + ((select total_amount from Orders where order_no = @orderid) - (select cash_amount from Orders where order_no = @orderid))
where username = (select customer_name from Orders where order_no = @orderid)
end
update Product
set available = 1 where customer_order_id = @orderid
update Product
set customer_username = null where customer_order_id = @orderid
update Product
set customer_order_id = null where customer_order_id = @orderid
delete from Orders where order_no = @orderid
end
else if (select credit_amount from Orders where order_no = @orderid) = (select total_amount from Orders where order_no = @orderid)
begin
update Product
set available = 1 where customer_order_id = @orderid
update Product
set customer_username = null where customer_order_id = @orderid
update Product
set customer_order_id = null where customer_order_id = @orderid
delete from Orders where order_no = @orderid
end
else if (select cash_amount from Orders where order_no = @orderid) = (select total_amount from Orders where order_no = @orderid)
begin
update Product
set available = 1 where customer_order_id = @orderid
update Product
set customer_username = null where customer_order_id = @orderid
update Product
set customer_order_id = null where customer_order_id = @orderid
delete from Orders where order_no = @orderid
end
end

GO



Go
create proc viewDeliveryTypes
AS
select type as 'Type', time_duration as 'Duration in days', fees as 'fees'
from Delivery

Go
create proc specifydeliverytype
@orderID int,
@deliveryID int
AS
declare @days int
set @days = (select time_duration from Delivery where id = @deliveryID)
update Orders
set remaining_days = @days where order_no = @orderID
update Orders
set delivery_id = @deliveryID where order_no = @orderID

Go
create proc trackRemainingDays
@orderid int,
@customername varchar(20),
@out smallint output
AS
declare @days smallint
declare @date date
set @date = (select order_date from Orders where order_no = @orderid and customer_name = @customername)
set @days = (select remaining_days from Orders where order_no = @orderid and customer_name = @customername)
set @out = (@days + Day(@date)) - Day(CURRENT_TIMESTAMP)

--------------------------------------------------------------------
go
create proc postProduct
@vendorUsername varchar(20),
@product_name varchar(20) ,
@category varchar(20),
@product_description text ,
@price decimal(10,2), 
@color varchar(20)
as
declare @active bit
set @active =(select activated from Vendors where @vendorUsername = username)
if (@active = 1)
begin
insert into Product (vendor_username,product_name,category,product_description,price,color,available,final_price)
values (@vendorUsername,@product_name,@category,@product_description,@price,@color,1,@price)
end
go
go
create proc vendorviewProducts
@vendorname varchar(20)
as
declare @active bit
set @active =(select activated from Vendors where @vendorname = username)
if (@active = 1)
begin
select * 
from Product p
where p.vendor_username=@vendorname
end
go
go
create proc EditProduct
@vendorname varchar(20),
@serialnumber int, 
@product_name varchar(20),
@category varchar(20),
@product_description text,
@price decimal(10,2),
@color varchar(20),
@feserial int =0 output
as
declare @active bit
set @active =(select activated from Vendors where @vendorname = username)
if (@active = 1)
begin

set @feserial = (select  1 from Product where @serialnumber = serial_no)

if(@color is not  null)
begin
update product
set color =  @color
where serial_no = @serialnumber and vendor_username = @vendorname 
end
if(@price  is not  null)
begin
update product
set price =  @price ,  final_price = @price
where vendor_username = @vendorname and serial_no = @serialnumber

end
if(@product_description is not null)
begin
update product
set product_description =  @product_description
where vendor_username = @vendorname and serial_no = @serialnumber

end
if(@product_name  is not  null)
begin
update product
set product_name =  @product_name
where vendor_username = @vendorname and serial_no = @serialnumber

end
if(@category  is not  null)
begin
update product
set category =  @category
where vendor_username = @vendorname and serial_no = @serialnumber

end

end
go
go
create proc deleteProduct
@vendorname varchar(20), 
@serialnumber int
as
declare @active bit
set @active =(select activated from Vendors where @vendorname = username)
if exists (select 1 from Customer_Question_Product where serial_no = @serialnumber)
begin
delete 
from Customer_Question_Product
where serial_no=@serialnumber
end
if exists (select 1 from Wishlist_Product where serial_no = @serialnumber)
begin
delete 
from Wishlist_Product
where serial_no=@serialnumber
end
if (@active = 1)
begin
delete  
From Product 
where vendor_username=@vendorname and serial_no=@serialnumber
end
go
go
create proc viewQuestions 
@vendorname varchar(20)
as
declare @active bit
set @active =(select activated from Vendors where @vendorname = username)
if (@active = 1)
begin
select c.serial_no ,  c.customer_name,c.question, c.answer
from Customer_Question_Product  c
	INNER JOIN Product p ON p.serial_no = c.serial_no
	where p.vendor_username=@vendorname
end

go
go
--create proc answerQuestions
--@vendorname varchar(20),
--@serialno int,
--@customername varchar(20),
--@answer text
--as
--declare @active bit
--set @active =(select activated from Vendors where @vendorname = username)
--if (@active = 1)
--begin
--declare @scheck int
--set @scheck = (select c.serial_no
--from Customer_Question_Product  c
	--INNER JOIN Product p ON p.serial_no = c.serial_no
	--where p.vendor_username=@vendorname and p.serial_no = @serialno)
--if (@serialno = @scheck)
--begin
--update Customer_Question_Product
--set Customer_Question_Product.answer=@answer
--where serial_no= @serialno and customer_name =@customername 
--end
--end
go
create proc showOffer
as
select  *  from offer
go

go
go

create proc addOffer	
@offeramount int,
@expiry_date datetime
as
insert into offer (offer_amount,expiry_date) values (@offeramount,@expiry_date)
go

go


create proc checkOfferonProduct
@serial int,
@activeoffer bit out
as
declare @flag bit=0
set @flag=(
select count(1)
from offersOnProduct
where serial_no=@serial
)

if (@flag = 1 )
	set @activeoffer = 1
ELSE
	set @activeoffer = 0

go

--- edited checkandremoveExpiredoffer


go
create proc showOfferProduct
as
select * from offersOnProduct

go
create proc checkandremoveExpiredoffer
@offerid int,
@faild int output,
@notexp int output
as
declare @active bit
declare @expy datetime
declare @flag bit =0
declare @ctime datetime
set @ctime =(CURRENT_TIMESTAMP
)
declare @lol int 
if exists (select 1 from offer where expiry_date  > @ctime and offer_id = @offerid)
begin
set @notexp = (1)
end
else
begin
set @notexp= (0)
end

if exists (select 1 from offer where offer_id = @offerid)
begin
set @faild = (0)
end
else
begin
set @faild =(1)
end


update product 
  set final_price = price
from Product p 
inner join offersOnProduct o on  p.serial_no = o.serial_no 
inner join offer x on x.offer_id = o.offer_id
where expiry_date  < @ctime and @offerid = x.offer_id

delete 
from offer
where offer_id = @offerid and expiry_date <@ctime


go

go

go

create proc applyOffer
@vendorname varchar(20),
@offerid int, 
@serial int ,
@notyou int =0 output ,	
@activeoffer int = 0 output,
@feserial int =0 output,
@feoffer int=0 output,
@feexpire int =0 output
as
declare @flag bit =0
declare @checkoffer bit = 0
declare @price real
declare @offer_amount real
declare @updated_price real
declare @message varchar(50) = 'The product has an active offer'
declare @active bit
declare @scheck bit
declare @avoffer int
declare @test1 int
declare @test2 int
declare @ctime datetime
set @ctime =(CURRENT_TIMESTAMP
)
declare @lol int 
if exists (select 1 from offer where expiry_date  < @ctime and offer_id = @offerid)
begin
set @feexpire = (1)
end
else
begin
set @feexpire= (0)
end
set @scheck = (select count(1) from product where @vendorname=vendor_username and @serial=serial_no)
set @notyou = (select count(1) from product where @vendorname=vendor_username and @serial=serial_no)
set @feserial = (select count(1) from product where @serial=serial_no)
set @feoffer = (select count(1) from offer where offer_id=@offerid)
if(@scheck = 1)
begin
set @active =(select activated from Vendors where @vendorname = username )
if (@active = 1)
begin
set @flag = (select count(1)
from offersOnProduct p
where p.serial_no = @serial
)
if (@flag = 1)
set @activeoffer = (1)
	print @message
	
if (@flag =0)
begin
insert into offersOnProduct (offer_id,serial_no) values (@offerid , @serial)
execute checkandremoveExpiredoffer @offerid ,@test1 output , @test2 output
Execute  checkOfferonProduct @serial ,@checkoffer out 
print @checkoffer
if (@checkoffer = 1)
begin
set @price = (select price
from product p
where  p.serial_no=@serial and  p.vendor_username= @vendorname )
set @offer_amount = ( select offer_amount
from offer
where offer_id = @offerid)
set @updated_price = (@price - (@price * ( @offer_amount / 100)))

update product
set final_price = @updated_price 
where serial_no = @serial 

end
end
end
end
go
go



go
create proc showVendors 
as
select * from Vendors

go
create proc activateVendors
@admin_username varchar(20),
@vendor_username varchar(20),
@exvendor int = 0 output,
@already int =0 output
as
set @already = (select count(1) from vendors where @vendor_username= username and activated='1')
if exists (select 1 from Vendors where username=@vendor_username)
begin
set @exvendor =(1)
end
else
begin
set @exvendor = (0)
end


if exists (select 1 from Admins where @admin_username=username)
begin
update Vendors
set activated = 1 where Vendors.username = @vendor_username
Update Vendors
set admin_username = @admin_username where Vendors.username = @vendor_username
end
go

go
create proc inviteDeliveryPerson  
@delivery_username varchar(20),
@delivery_email varchar(50)
as
insert into Users (username,email) values (@delivery_username,@delivery_email)
insert into Delivery_Person(is_activated , username) values (0,@delivery_username)

go

create proc updateOrderStatusInProcess
@order_no int,
@exorder int =0 output
as
set @exorder = (select count(1) from Orders where order_no = @order_no)

update Orders
set order_status ='In Process'
where Orders.order_no = @order_no
go
---------------------------------------------------------------------

go
create proc recommmend
@customername varchar(20)
as
 select * into temp from (select top(3)customer_name  ,count(*) as freq
 from CustomerAddstoCartProduct
 where customer_name <> @customername and serial_no in (select serial_no 
 from CustomerAddstoCartProduct 
 where customer_name = @customername)
 group by customer_name
 order by count(*) desc ) h

 select * 
FROM  (

select top(3)*
from Product
where product_name in(
select top(3)p1.product_name
from Wishlist_Product w inner join Product p1 on p1.serial_no=w.serial_no
where p1.category in(
    select top(3)p.category
    from CustomerAddstoCartProduct c inner join Product p on p.serial_no=c.serial_no
    group by p.category
    order by count(p.category)
    )
    group by p1.product_name
Order by count (*)desc)
union 

select p.*
from ( select TOP(3)serial_no , count(*) as freq
 from Wishlist_Product , temp where temp.customer_name  = username
 group by serial_no
 order by count(*) desc) b
 INNER JOIN product p on p.serial_no = b.serial_no
 ) a


 drop table temp;
 

 go
 create proc addDelivery
 
 @delivery_type varchar(20),
 @time_duration int,
 @fees decimal(5,3),
 @admin_username varchar(20)

as
if exists (select 1 from Admins where @admin_username=username)begin
insert into Delivery (type,time_duration , fees , username ) values  (@delivery_type , @time_duration , @fees , @admin_username)	end
go
--drop proc addDelivery
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
create proc  assignOrdertoDelivery
@delivery_username varchar(20),
@order_no int,
@admin_username varchar(20)
as
if exists (select 1 from Orders where @order_no=order_no) and exists (select 1 from Admins where @admin_username=username)
begin
insert into Admin_Delivery_Order (delivery_username,order_no,admin_username)
values (@delivery_username,@order_no,@admin_username)
end
else 
begin print ('this order no doesnt exist')
end
--drop proc assignOrdertoDelivery
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
go
create proc createTodaysDeal
@deal_amount int,
@admin_username varchar(20),
@expiry_date datetime
as
if exists (select 1 from Admins where @admin_username=username)
begin
insert into Todays_Deals values (@deal_amount,@expiry_date,@admin_username)
end
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
go
go
create proc checkTodaysDealOnProduct
@serial_no INT,
@exist bit output
as
declare @id int 
select @id=deal_id
from Todays_Deals_Product
where @serial_no=serial_no
Execute removeExpiredDeal @id

if exists (select 1 from Todays_Deals_Product where @serial_no=serial_no)
begin
set @exist=1
end
else begin set @exist=0 end 




----------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------
go 
create proc removeExpiredDeal
@deal_iD int
as
--
declare @date_of_deal datetime
select @date_of_deal=expiry_date
from Todays_Deals
where @deal_iD=deal_id
--
declare @serial_no int 
select @serial_no=serial_no
from Todays_Deals_Product
where @deal_iD=deal_id
--
if (@date_of_deal < GETDATE() )
begin 
delete from Todays_Deals_Product where @deal_iD=deal_id
delete from Todays_Deals where @deal_iD=deal_id
update Product
set final_price = price
where serial_no=@serial_no
end
go
create proc addTodaysDealOnProduct 
@deal_id int, 
@serial_no int,
@alreadyapply int =0 output ,
@feexpire int output
as
declare @ctime datetime
set @ctime =(CURRENT_TIMESTAMP
)
if exists (select 1 from Todays_Deals where expiry_date  < @ctime and deal_id = @deal_id)
begin
set @feexpire = (1)
end
else
begin
set @feexpire= (0)
end

set @alreadyapply  = (select 1 from Todays_Deals_Product where serial_no= @serial_no)
if exists (select 1 from Product where serial_no=@serial_no)

begin
declare @exist bit 
EXECUTE checkTodaysDealOnProduct @serial_no ,@exist output
declare @amount decimal(10,2)
select @amount = deal_amount
from Todays_Deals
where @deal_id=deal_id
set @amount = 100-@amount 
set @amount = @amount/100

if (@exist = 0)
begin
insert into Todays_Deals_Product values (@deal_id,@serial_no)
update Product
set final_price = final_price * @amount
where @serial_no=serial_no
end
else 
begin
print ('This product already has a deal on it ')
end
end
--drop proc addTodaysDealOnProduct 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
go 

--drop proc removeExpiredDeal
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
go
create proc createGiftCard
@code varchar(10),
@expiry_date datetime,
@amount int,
@admin_username varchar(20)
as 
if exists (select 1 from Admins where @admin_username=username)
insert into Giftcard values (@code,@expiry_date,@amount,@admin_username)
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
go
create proc removeExpiredGiftCard
@code varchar(10)

as
--
if exists (select 1 from Giftcard where @code=code)
begin
declare @expire datetime
select @expire=expiry_date
from Giftcard
where @code=code
--
declare @amount int
select @amount=amount
from Giftcard
where code = @code
--
declare @username varchar(20)
select @username=customer_name
from Admin_Customer_Giftcard
where code =@code
--
if(@expire <GETDATE())
begin
delete from Admin_Customer_Giftcard where @code=code
delete from Giftcard where code =@code
update Customer
set points=0
where @username=username
end 
end
--drop proc removeExpiredGiftCard
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
go

create  proc checkGiftCardOnCustomer --leave this proce
@code varchar(10),
@activeGiftCard BIT output

as
Execute removeExpiredGiftCard @code
if exists (select 1 from Admin_Customer_Giftcard where @code=code)
begin set @activeGiftCard =1 end
else set @activeGiftCard =0

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
go
create proc checkCustomer 
@customer_name varchar(20),
@exists bit output
as 

declare @code varchar(20)
select @code=code
from Admin_Customer_Giftcard
where customer_name=@customer_name
Execute removeExpiredGiftCard @code
if exists (select 1 from Admin_Customer_Giftcard where @customer_name=customer_name)
	begin
	set @exists = 1
	end
	else begin set @exists=0 end
go


create proc giveGiftCardtoCustomer
@code varchar(10),
@customer_name varchar(20),
@admin_username varchar(20)
as 
if exists (select 1 from Customer where @customer_name=username)
begin
declare @exist bit
declare @amount int 
select @amount=amount
from Giftcard
where code=@code
execute checkCustomer @customer_name,@exist output
if (@exist =0)
begin 
if exists (select 1 from Admins where @admin_username=username)
begin
insert into Admin_Customer_Giftcard values (@code,@customer_name,@admin_username,@amount)
update Customer
set points = @amount
where @customer_name=username
end
end
else print('Customer has a Gift Card')
end
go

create proc reviewOrders 
as

select * from Orders
go
--drop proc giveGiftCardtoCustomer
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
go
create proc acceptAdminInvitation
@delivery_username varchar(20)

as
if exists (select 1 from Delivery_Person where @delivery_username=username)
begin
update Delivery_Person
set is_activated=1
where @delivery_username=username
end
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
go
create proc deliveryPersonUpdateInfo
@username varchar(20),
@first_name varchar(20),
@last_name varchar(20),
@password varchar(20),
@email varchar(50)
as
if exists (select 1 from Delivery_Person where @username=username and is_activated=1)
begin
update Users
set first_name=@first_name,last_name=@last_name,password=@password,
email=@email
where username=@username
end
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
go
create proc viewmyorders
@deliveryperson varchar(20)
as
if exists (select 1 from Delivery_Person where username=@deliveryperson and is_activated=1)
begin	
select	 O.order_no, O.order_date, O.total_amount, O.cash_amount, O.credit_amount, O.payment_type,
		 O.order_status, O.remaining_days,O.time_limit,O.Gift_Card_code_used,O. customer_name,O. delivery_id, O.creditCard_number
from	Admin_Delivery_Order ADO,		Orders O
where	 @deliveryperson=ADO.delivery_username and O.order_no=ADO.order_no
end
--drop proc viewmyorders
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
go 
create proc specifyDeliveryWindow
@delivery_username varchar(20),
@order_no int,
@delivery_window varchar(50)
as
if exists (select 1 from Delivery_Person where @delivery_username=username and is_activated=1) 
and exists (select 1 from Admin_Delivery_Order where @delivery_username=delivery_username and order_no=@order_no)
begin
update Admin_Delivery_Order
set delivery_window=@delivery_window
where @delivery_username=delivery_username and @order_no=order_no
end
go--drop proc specifyDeliveryWindow
create proc updateOrderStatusOutforDelivery
@order_no int
as
update Orders
set order_status = 'Out for delivery'
where @order_no=order_no

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
go
create proc updateOrderStatusDelivered
@order_no int 
as 
update Orders
set order_status='Delivered'
where @order_no=order_no
----------------------------------------------------------------------------------------------------------------------------------------------------------
go
alter proc AddCreditCard
@creditcardnumber varchar(20),
@expirydate date,
@cvv varchar(4),
@customername varchar(20)
AS
  
BEGIN TRY  
   insert into Credit_Card values(@creditcardnumber, @expirydate, @cvv)
END TRY  
BEGIN CATCH   
END CATCH;   

insert into Customer_CreditCard values(@customername, @creditcardnumber)
go


alter proc checkTodaysDealOnProduct
@serial_no INT,
@exist bit output
as
if exists (select 1 from Todays_Deals_Product where @serial_no=serial_no)
declare @id int 
select @id=deal_id
from Todays_Deals_Product
where @serial_no=serial_no
execute removeExpiredDeal @id;

if exists (select 1 from Todays_Deals_Product where @serial_no=serial_no)
begin
set @exist=1
end
else
begin set @exist=0 
end
go

create proc isactive
@username varchar(20),
@flag int  output
as
if exists (select 1 from Vendors where @username=username and activated='1')
begin
set @flag=1
end
else
begin set @flag=0 
end
go

----------------------------------------------------------------------------

INSERT INTO Users (username,first_name,last_name,password,email)
VALUES('hana.aly','hana','aly','pass1','hana.aly@guc.edu.eg')

INSERT INTO Admins(username)
VALUES('hana.aly')





