// See https://aka.ms/new-console-template for more information
using etity;
using Microsoft.EntityFrameworkCore;

AppDbContext appDbContext = new AppDbContext();
//appDbContext.CreateDatabase();
//appDbContext.DropDatabase();
//appDbContext.InsertProduct();

Console.WriteLine("Danh sach product: ");
appDbContext.HienThiProduct();
Console.WriteLine("--------");
appDbContext.UpdateProduct(2,"Ha Thanh Hao", 123);
Console.WriteLine("--------");
//xoA   
appDbContext.DeleteProduct(10);

Console.ReadLine();