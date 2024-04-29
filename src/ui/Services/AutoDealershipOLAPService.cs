using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;

using CourseWork.Data;

namespace CourseWork
{
    public partial class AutoDealershipOLAPService
    {
        AutoDealershipOLAPContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly AutoDealershipOLAPContext context;
        private readonly NavigationManager navigationManager;

        public AutoDealershipOLAPService(AutoDealershipOLAPContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportAutoDealershipsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealershipolap/autodealerships/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealershipolap/autodealerships/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAutoDealershipsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealershipolap/autodealerships/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealershipolap/autodealerships/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAutoDealershipsRead(ref IQueryable<CourseWork.Models.AutoDealershipOLAP.AutoDealership> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealershipOLAP.AutoDealership>> GetAutoDealerships(Query query = null)
        {
            var items = Context.AutoDealerships.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAutoDealershipsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAutoDealershipGet(CourseWork.Models.AutoDealershipOLAP.AutoDealership item);
        partial void OnGetAutoDealershipById(ref IQueryable<CourseWork.Models.AutoDealershipOLAP.AutoDealership> items);


        public async Task<CourseWork.Models.AutoDealershipOLAP.AutoDealership> GetAutoDealershipById(int id)
        {
            var items = Context.AutoDealerships
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetAutoDealershipById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAutoDealershipGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAutoDealershipCreated(CourseWork.Models.AutoDealershipOLAP.AutoDealership item);
        partial void OnAfterAutoDealershipCreated(CourseWork.Models.AutoDealershipOLAP.AutoDealership item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.AutoDealership> CreateAutoDealership(CourseWork.Models.AutoDealershipOLAP.AutoDealership autodealership)
        {
            OnAutoDealershipCreated(autodealership);

            var existingItem = Context.AutoDealerships
                              .Where(i => i.Id == autodealership.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AutoDealerships.Add(autodealership);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(autodealership).State = EntityState.Detached;
                throw;
            }

            OnAfterAutoDealershipCreated(autodealership);

            return autodealership;
        }

        public async Task<CourseWork.Models.AutoDealershipOLAP.AutoDealership> CancelAutoDealershipChanges(CourseWork.Models.AutoDealershipOLAP.AutoDealership item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAutoDealershipUpdated(CourseWork.Models.AutoDealershipOLAP.AutoDealership item);
        partial void OnAfterAutoDealershipUpdated(CourseWork.Models.AutoDealershipOLAP.AutoDealership item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.AutoDealership> UpdateAutoDealership(int id, CourseWork.Models.AutoDealershipOLAP.AutoDealership autodealership)
        {
            OnAutoDealershipUpdated(autodealership);

            var itemToUpdate = Context.AutoDealerships
                              .Where(i => i.Id == autodealership.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(autodealership);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAutoDealershipUpdated(autodealership);

            return autodealership;
        }

        partial void OnAutoDealershipDeleted(CourseWork.Models.AutoDealershipOLAP.AutoDealership item);
        partial void OnAfterAutoDealershipDeleted(CourseWork.Models.AutoDealershipOLAP.AutoDealership item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.AutoDealership> DeleteAutoDealership(int id)
        {
            var itemToDelete = Context.AutoDealerships
                              .Where(i => i.Id == id)
                              .Include(i => i.CarSales)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAutoDealershipDeleted(itemToDelete);


            Context.AutoDealerships.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAutoDealershipDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportBrandsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealershipolap/brands/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealershipolap/brands/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBrandsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealershipolap/brands/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealershipolap/brands/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBrandsRead(ref IQueryable<CourseWork.Models.AutoDealershipOLAP.Brand> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealershipOLAP.Brand>> GetBrands(Query query = null)
        {
            var items = Context.Brands.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnBrandsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBrandGet(CourseWork.Models.AutoDealershipOLAP.Brand item);
        partial void OnGetBrandById(ref IQueryable<CourseWork.Models.AutoDealershipOLAP.Brand> items);


        public async Task<CourseWork.Models.AutoDealershipOLAP.Brand> GetBrandById(int id)
        {
            var items = Context.Brands
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetBrandById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnBrandGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBrandCreated(CourseWork.Models.AutoDealershipOLAP.Brand item);
        partial void OnAfterBrandCreated(CourseWork.Models.AutoDealershipOLAP.Brand item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.Brand> CreateBrand(CourseWork.Models.AutoDealershipOLAP.Brand brand)
        {
            OnBrandCreated(brand);

            var existingItem = Context.Brands
                              .Where(i => i.Id == brand.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Brands.Add(brand);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(brand).State = EntityState.Detached;
                throw;
            }

            OnAfterBrandCreated(brand);

            return brand;
        }

        public async Task<CourseWork.Models.AutoDealershipOLAP.Brand> CancelBrandChanges(CourseWork.Models.AutoDealershipOLAP.Brand item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBrandUpdated(CourseWork.Models.AutoDealershipOLAP.Brand item);
        partial void OnAfterBrandUpdated(CourseWork.Models.AutoDealershipOLAP.Brand item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.Brand> UpdateBrand(int id, CourseWork.Models.AutoDealershipOLAP.Brand brand)
        {
            OnBrandUpdated(brand);

            var itemToUpdate = Context.Brands
                              .Where(i => i.Id == brand.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(brand);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterBrandUpdated(brand);

            return brand;
        }

        partial void OnBrandDeleted(CourseWork.Models.AutoDealershipOLAP.Brand item);
        partial void OnAfterBrandDeleted(CourseWork.Models.AutoDealershipOLAP.Brand item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.Brand> DeleteBrand(int id)
        {
            var itemToDelete = Context.Brands
                              .Where(i => i.Id == id)
                              .Include(i => i.Cars)
                              .Include(i => i.CarSales)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBrandDeleted(itemToDelete);


            Context.Brands.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBrandDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCarsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealershipolap/cars/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealershipolap/cars/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCarsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealershipolap/cars/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealershipolap/cars/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCarsRead(ref IQueryable<CourseWork.Models.AutoDealershipOLAP.Car> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealershipOLAP.Car>> GetCars(Query query = null)
        {
            var items = Context.Cars.AsQueryable();

            items = items.Include(i => i.Brand);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCarsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCarGet(CourseWork.Models.AutoDealershipOLAP.Car item);
        partial void OnGetCarById(ref IQueryable<CourseWork.Models.AutoDealershipOLAP.Car> items);


        public async Task<CourseWork.Models.AutoDealershipOLAP.Car> GetCarById(int id)
        {
            var items = Context.Cars
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Brand);
 
            OnGetCarById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCarGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCarCreated(CourseWork.Models.AutoDealershipOLAP.Car item);
        partial void OnAfterCarCreated(CourseWork.Models.AutoDealershipOLAP.Car item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.Car> CreateCar(CourseWork.Models.AutoDealershipOLAP.Car car)
        {
            OnCarCreated(car);

            var existingItem = Context.Cars
                              .Where(i => i.Id == car.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Cars.Add(car);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(car).State = EntityState.Detached;
                throw;
            }

            OnAfterCarCreated(car);

            return car;
        }

        public async Task<CourseWork.Models.AutoDealershipOLAP.Car> CancelCarChanges(CourseWork.Models.AutoDealershipOLAP.Car item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCarUpdated(CourseWork.Models.AutoDealershipOLAP.Car item);
        partial void OnAfterCarUpdated(CourseWork.Models.AutoDealershipOLAP.Car item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.Car> UpdateCar(int id, CourseWork.Models.AutoDealershipOLAP.Car car)
        {
            OnCarUpdated(car);

            var itemToUpdate = Context.Cars
                              .Where(i => i.Id == car.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(car);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCarUpdated(car);

            return car;
        }

        partial void OnCarDeleted(CourseWork.Models.AutoDealershipOLAP.Car item);
        partial void OnAfterCarDeleted(CourseWork.Models.AutoDealershipOLAP.Car item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.Car> DeleteCar(int id)
        {
            var itemToDelete = Context.Cars
                              .Where(i => i.Id == id)
                              .Include(i => i.Leases)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCarDeleted(itemToDelete);


            Context.Cars.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCarDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCarSalesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealershipolap/carsales/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealershipolap/carsales/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCarSalesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealershipolap/carsales/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealershipolap/carsales/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCarSalesRead(ref IQueryable<CourseWork.Models.AutoDealershipOLAP.CarSale> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealershipOLAP.CarSale>> GetCarSales(Query query = null)
        {
            var items = Context.CarSales.AsQueryable();

            items = items.Include(i => i.AutoDealership);
            items = items.Include(i => i.Brand);
            items = items.Include(i => i.Date);
            items = items.Include(i => i.Date1);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCarSalesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCarSaleGet(CourseWork.Models.AutoDealershipOLAP.CarSale item);
        partial void OnGetCarSaleById(ref IQueryable<CourseWork.Models.AutoDealershipOLAP.CarSale> items);


        public async Task<CourseWork.Models.AutoDealershipOLAP.CarSale> GetCarSaleById(int id)
        {
            var items = Context.CarSales
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AutoDealership);
            items = items.Include(i => i.Brand);
            items = items.Include(i => i.Date);
            items = items.Include(i => i.Date1);
 
            OnGetCarSaleById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCarSaleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCarSaleCreated(CourseWork.Models.AutoDealershipOLAP.CarSale item);
        partial void OnAfterCarSaleCreated(CourseWork.Models.AutoDealershipOLAP.CarSale item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.CarSale> CreateCarSale(CourseWork.Models.AutoDealershipOLAP.CarSale carsale)
        {
            OnCarSaleCreated(carsale);

            var existingItem = Context.CarSales
                              .Where(i => i.Id == carsale.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CarSales.Add(carsale);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(carsale).State = EntityState.Detached;
                throw;
            }

            OnAfterCarSaleCreated(carsale);

            return carsale;
        }

        public async Task<CourseWork.Models.AutoDealershipOLAP.CarSale> CancelCarSaleChanges(CourseWork.Models.AutoDealershipOLAP.CarSale item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCarSaleUpdated(CourseWork.Models.AutoDealershipOLAP.CarSale item);
        partial void OnAfterCarSaleUpdated(CourseWork.Models.AutoDealershipOLAP.CarSale item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.CarSale> UpdateCarSale(int id, CourseWork.Models.AutoDealershipOLAP.CarSale carsale)
        {
            OnCarSaleUpdated(carsale);

            var itemToUpdate = Context.CarSales
                              .Where(i => i.Id == carsale.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(carsale);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCarSaleUpdated(carsale);

            return carsale;
        }

        partial void OnCarSaleDeleted(CourseWork.Models.AutoDealershipOLAP.CarSale item);
        partial void OnAfterCarSaleDeleted(CourseWork.Models.AutoDealershipOLAP.CarSale item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.CarSale> DeleteCarSale(int id)
        {
            var itemToDelete = Context.CarSales
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCarSaleDeleted(itemToDelete);


            Context.CarSales.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCarSaleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDatesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealershipolap/dates/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealershipolap/dates/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDatesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealershipolap/dates/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealershipolap/dates/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDatesRead(ref IQueryable<CourseWork.Models.AutoDealershipOLAP.Date> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealershipOLAP.Date>> GetDates(Query query = null)
        {
            var items = Context.Dates.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDatesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDateGet(CourseWork.Models.AutoDealershipOLAP.Date item);
        partial void OnGetDateById(ref IQueryable<CourseWork.Models.AutoDealershipOLAP.Date> items);


        public async Task<CourseWork.Models.AutoDealershipOLAP.Date> GetDateById(int id)
        {
            var items = Context.Dates
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetDateById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDateGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDateCreated(CourseWork.Models.AutoDealershipOLAP.Date item);
        partial void OnAfterDateCreated(CourseWork.Models.AutoDealershipOLAP.Date item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.Date> CreateDate(CourseWork.Models.AutoDealershipOLAP.Date date)
        {
            OnDateCreated(date);

            var existingItem = Context.Dates
                              .Where(i => i.Id == date.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Dates.Add(date);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(date).State = EntityState.Detached;
                throw;
            }

            OnAfterDateCreated(date);

            return date;
        }

        public async Task<CourseWork.Models.AutoDealershipOLAP.Date> CancelDateChanges(CourseWork.Models.AutoDealershipOLAP.Date item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDateUpdated(CourseWork.Models.AutoDealershipOLAP.Date item);
        partial void OnAfterDateUpdated(CourseWork.Models.AutoDealershipOLAP.Date item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.Date> UpdateDate(int id, CourseWork.Models.AutoDealershipOLAP.Date date)
        {
            OnDateUpdated(date);

            var itemToUpdate = Context.Dates
                              .Where(i => i.Id == date.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(date);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDateUpdated(date);

            return date;
        }

        partial void OnDateDeleted(CourseWork.Models.AutoDealershipOLAP.Date item);
        partial void OnAfterDateDeleted(CourseWork.Models.AutoDealershipOLAP.Date item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.Date> DeleteDate(int id)
        {
            var itemToDelete = Context.Dates
                              .Where(i => i.Id == id)
                              .Include(i => i.CarSales)
                              .Include(i => i.CarSales1)
                              .Include(i => i.Leases)
                              .Include(i => i.Leases1)
                              .Include(i => i.Leases2)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDateDeleted(itemToDelete);


            Context.Dates.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDateDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportLeasesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealershipolap/leases/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealershipolap/leases/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLeasesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/autodealershipolap/leases/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/autodealershipolap/leases/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLeasesRead(ref IQueryable<CourseWork.Models.AutoDealershipOLAP.Lease> items);

        public async Task<IQueryable<CourseWork.Models.AutoDealershipOLAP.Lease>> GetLeases(Query query = null)
        {
            var items = Context.Leases.AsQueryable();

            items = items.Include(i => i.Car);
            items = items.Include(i => i.Date);
            items = items.Include(i => i.Date1);
            items = items.Include(i => i.Date2);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnLeasesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLeaseGet(CourseWork.Models.AutoDealershipOLAP.Lease item);
        partial void OnGetLeaseById(ref IQueryable<CourseWork.Models.AutoDealershipOLAP.Lease> items);


        public async Task<CourseWork.Models.AutoDealershipOLAP.Lease> GetLeaseById(int id)
        {
            var items = Context.Leases
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Car);
            items = items.Include(i => i.Date);
            items = items.Include(i => i.Date1);
            items = items.Include(i => i.Date2);
 
            OnGetLeaseById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnLeaseGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnLeaseCreated(CourseWork.Models.AutoDealershipOLAP.Lease item);
        partial void OnAfterLeaseCreated(CourseWork.Models.AutoDealershipOLAP.Lease item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.Lease> CreateLease(CourseWork.Models.AutoDealershipOLAP.Lease lease)
        {
            OnLeaseCreated(lease);

            var existingItem = Context.Leases
                              .Where(i => i.Id == lease.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Leases.Add(lease);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(lease).State = EntityState.Detached;
                throw;
            }

            OnAfterLeaseCreated(lease);

            return lease;
        }

        public async Task<CourseWork.Models.AutoDealershipOLAP.Lease> CancelLeaseChanges(CourseWork.Models.AutoDealershipOLAP.Lease item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLeaseUpdated(CourseWork.Models.AutoDealershipOLAP.Lease item);
        partial void OnAfterLeaseUpdated(CourseWork.Models.AutoDealershipOLAP.Lease item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.Lease> UpdateLease(int id, CourseWork.Models.AutoDealershipOLAP.Lease lease)
        {
            OnLeaseUpdated(lease);

            var itemToUpdate = Context.Leases
                              .Where(i => i.Id == lease.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(lease);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterLeaseUpdated(lease);

            return lease;
        }

        partial void OnLeaseDeleted(CourseWork.Models.AutoDealershipOLAP.Lease item);
        partial void OnAfterLeaseDeleted(CourseWork.Models.AutoDealershipOLAP.Lease item);

        public async Task<CourseWork.Models.AutoDealershipOLAP.Lease> DeleteLease(int id)
        {
            var itemToDelete = Context.Leases
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnLeaseDeleted(itemToDelete);


            Context.Leases.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLeaseDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}