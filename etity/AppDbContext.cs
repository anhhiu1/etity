using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace etity
{
    public class AppDbContext
    {
        public void CreateDatabase()
        {
            using var dbContext = new ProductDbcontext();
            bool databaseCreated = dbContext.Database.EnsureCreated();
            if (databaseCreated)
                Console.WriteLine("Cơ sở dữ liệu đã được tạo thành công.");
            else
                Console.WriteLine("Cơ sở dữ liệu đã tồn tại.");
        }
        public void DropDatabase()
        {
            using var dbContext = new ProductDbcontext();
            bool databaseCreated = dbContext.Database.EnsureDeleted();
            if (databaseCreated)
                Console.WriteLine("Cơ sở dữ liệu đã được xoas thành công.");
            else
                Console.WriteLine("Cơ sở dữ liệu đã xoa.");
        }
        //
        public void InsertProduct()
        {
            using (var dbContext = new ProductDbcontext())
            {
                var newProduct = new Object[]
                {
                    new Product { Name = "Thanh Hao", Price = 20, congtyid =2 },
                    new Product { Name = "Thanh minh", Price = 21, congtyid =3 },
                    new Product { Name = "quan Hao", Price = 22, congtyid =4},
                    new Product { Name = "Thanh cong", Price = 20, congtyid =5 },
                    new Product { Name = "van Hao", Price = 20, congtyid =6 },
                    new Product { Name = "Thanh miinh", Price = 20, congtyid =7 },
                    new Product { Name = "Thanh Hao", Price = 20, congtyid =8 },
                    new Product { Name = "Thanh Hao", Price = 20, congtyid =9 },
                    new Product { Name = "Thanh Hao", Price = 20, congtyid =10 },
                    new Product { Name = "Thanh Hao", Price = 20, congtyid =11},

                };

                dbContext.AddRange(newProduct);
                dbContext.SaveChanges();
            }
        }

        public void UpdateProduct(int id, string name, decimal price)
        {
            using (var dbcontext = new ProductDbcontext())
            {
                var product = dbcontext.Products.FirstOrDefault(p => p.Id == id);

                if (product != null)
                {
                    product.Name = name;
                    product.Price = price;
                    int s = dbcontext.SaveChanges();
                    if (s > 0)
                        Console.WriteLine("cap nhat thanh cong ");
                }
                else
                    Console.WriteLine("chua cap nhat duoc dau cu");

                HienThiProduct();
            }
        }
        public void DeleteProduct (int id)
        {
            using (var dbcontext = new ProductDbcontext())
            {
                var del = dbcontext.Products.FirstOrDefault(p => p.Id == id);
                if(del != null)
                {
                    dbcontext.Remove(del);
                    int res = dbcontext.SaveChanges();
                    if (res > 0)
                        Console.WriteLine($"tim thay product co id{id}: da xoa khoi csdl");
                }
                else
                    Console.WriteLine($"khong tim product co id {id} thay de xoa");
            }
            HienThiProduct();
        }


        public void HienThiProduct()
        {
            using (var dbcontext = new ProductDbcontext())
            {
                //var pro = dbcontext.Products.ToList();
                //pro.ForEach(p => p.hienthi());
                var kq = from p in dbcontext.Products
                         orderby p.Name
                         where p.Id  >=1
                         select p;
                kq.ToList().ForEach(p => p.hienthi());
            }
        }
        //public class ProductDbcontext : DbContext
        //{
        //    public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        //    {
        //        // Cấu hình ghi log với bộ lọc và console
        //        builder.AddFilter((category, level) => level >= LogLevel.Information) // Chỉ ghi log từ mức Information trở lên
        //               .AddConsole();  // Ghi log ra console
        //    });

        //    string Connections = "Data Source=FF01\\SQLEXPRESS;Initial Catalog=qlproduct;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True";
        //    public DbSet<Product> Products { get; set; }
        //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    {
        //        base.OnConfiguring(optionsBuilder);
        //        optionsBuilder.UseSqlServer(Connections);
        //         optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        //    }
        //}
        public class ProductDbcontext : DbContext
        {
            // Tạo LoggerFactory để ghi log
            public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
            {
                // Cấu hình để ghi log các truy vấn SQL từ mức độ Information trở lên
                builder.AddFilter((category, level) => level >= LogLevel.Information)
                       .AddConsole(); // Ghi log ra console
            });

            // Chuỗi kết nối tới cơ sở dữ liệu
            private string _connectionString = "Data Source=FF01\\SQLEXPRESS;Initial Catalog=qlproduct;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True";

            // Khai báo DbSet cho bảng Product
            public DbSet<Product> Products { get; set; }

            // Cấu hình DbContext
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);

                // Sử dụng SQL Server với chuỗi kết nối
                optionsBuilder.UseSqlServer(_connectionString);

                // Thêm Logger để ghi log truy vấn SQL và các hoạt động liên quan
                optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            }
        }
    }
}
