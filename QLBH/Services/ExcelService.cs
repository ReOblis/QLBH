using OfficeOpenXml;
using QLBH.Models;
using QLBH.Data;
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace QLBH.Services
{

    public class ExcelService
    {
        private readonly AppDbContext _dbContext;

        public ExcelService(AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        /* public void CreateNovelDataExcel(string filePath)
         {
             FileInfo fileInfo = new FileInfo(filePath);

             using (ExcelPackage package = new ExcelPackage(fileInfo))
             {
                 // Tạo một worksheet mới

                 ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("NovelData");
                 // Đặt tiêu đề cho các cột
                 worksheet.Cells[1, 1].Value = "ID";
                 worksheet.Cells[1, 2].Value = "Title";
                 worksheet.Cells[1, 3].Value = "Author";
                 worksheet.Cells[1, 4].Value = "Description";
                 worksheet.Cells[1, 5].Value = "CoverImageURL";
                 worksheet.Cells[1, 6].Value = "Price";
                 worksheet.Cells[1, 7].Value = "DiscountPercentage";
                 worksheet.Cells[1, 8].Value = "Quantity";

                 ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("GenresData");
                 worksheet.Cells[1, 1].Value = "GenreID";
                 worksheet.Cells[1, 2].Value = "Name";

             }
         }*/
        public void ImportDataFromExcel(string filePath)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            _dbContext.Database.ExecuteSqlRaw("DELETE FROM NovelGenres");
            _dbContext.Database.ExecuteSqlRaw("DELETE FROM Novels; DBCC CHECKIDENT ('Novels', RESEED, 0)");
            _dbContext.Database.ExecuteSqlRaw("DELETE FROM Genres; DBCC CHECKIDENT ('Genres', RESEED, 0)");
            transaction.Commit();

            FileInfo fileInfo = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                // Đọc dữ liệu cho bảng Novels
                ImportNovelData(package);
                ImportGenreData(package);
                ImportNovelGenreData(package);
            }
        }

        private void ImportNovelData(ExcelPackage package)
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets["NovelData"];

            if (worksheet == null)
            {
                // Bảng Novels không tồn tại trong file Excel
                return;
            }

            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                // Đọc dữ liệu cho bảng Novels từ các cột tương ứng
                string title = worksheet.Cells[row, 1].Text;
                string author = worksheet.Cells[row, 2].Text;
                string description = worksheet.Cells[row, 3].Text;
                string coverImageURL = worksheet.Cells[row, 4].Text;
                decimal price = Convert.ToDecimal(worksheet.Cells[row, 5].Text);
                decimal discountPercentage = Convert.ToDecimal(worksheet.Cells[row, 6].Text);
                int quantity = Convert.ToInt16(worksheet.Cells[row, 7].Text);

                Novel novel = new Novel
                {
                    Title = title,
                    Author = author,
                    Description = description,
                    CoverImageURL = coverImageURL,
                    Price = price,
                    DiscountPercentage = discountPercentage,
                    Quantity = quantity,
                };

                _dbContext.Novels.Add(novel);
            }

            var transaction = _dbContext.Database.BeginTransaction();
            _dbContext.SaveChanges();
            transaction.Commit();
        }

        private void ImportGenreData(ExcelPackage package)
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets["GenreData"];

            if (worksheet == null)
            {
                // Bảng Genres không tồn tại trong file Excel
                return;
            }

            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                // Đọc dữ liệu cho bảng Genres từ các cột tương ứng
                string name = worksheet.Cells[row, 1].Text; // Giả sử cột Genres bắt đầu từ cột 8

                Genre genre = new Genre
                {
                    Name = name
                };

                _dbContext.Genres.Add(genre);
            }

            var transaction = _dbContext.Database.BeginTransaction();
            _dbContext.SaveChanges();
            transaction.Commit();
        }
        private void ImportNovelGenreData(ExcelPackage package)
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets["NovelGenreData"];

            if (worksheet == null)
            {
                // Bảng NovelGenres không tồn tại trong file Excel
                return;
            }

            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                // Đọc dữ liệu cho bảng NovelGenres từ các cột tương ứng
                int novelId = Convert.ToInt32(worksheet.Cells[row, 1].Text);
                int genreId = Convert.ToInt32(worksheet.Cells[row, 2].Text);

                NovelGenre novelGenre = new NovelGenre
                {
                    NovelID = novelId,
                    GenreID = genreId
                };

                _dbContext.NovelGenres.Add(novelGenre);
            }

            var transaction = _dbContext.Database.BeginTransaction();
            _dbContext.SaveChanges();
            transaction.Commit();
        }




    }
}

/*List<string> tablesToEnableIdentityInsert = new List<string> { "Novels", "Genres", "Ratings", "Orders", "Users", "Carts", "CartDetails" };
foreach (var tableName in tablesToEnableIdentityInsert)
{
    _dbContext.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT [dbo].[{tableName}] ON");
}
*/